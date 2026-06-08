using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CalculatorMateriale.Data;
using CalculatorMateriale.Helpers;
using CalculatorMateriale.Models;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CalculatorMateriale.Views
{
    public class DevizItem
    {
        public string Material { get; set; } = string.Empty;
        public decimal Suprafata { get; set; }
        public decimal Consum { get; set; }
        public decimal PretUnitar { get; set; }
        public decimal PretTotal { get; set; }
    }

    public partial class DevizView : UserControl
    {
        private readonly ObservableCollection<DevizItem> materialsCollection;
        private decimal totalMateriale;
        private IUnitOfWork? _unitOfWork;

        public DevizView()
        {
            InitializeComponent();
            materialsCollection = new ObservableCollection<DevizItem>();
            MaterialsGrid.ItemsSource = materialsCollection;
            QuestPDF.Settings.License = LicenseType.Community;
            DataDevizPicker.SelectedDate = DateTime.Today;
        }

        private void AddMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var material = MaterialInput.Text;
                if (string.IsNullOrWhiteSpace(material))
                {
                    MessageBox.Show("Introduceți denumirea materialului.", "Avertisment", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!TryReadDecimal(SuprafataDevizInput.Text, out var suprafata) || suprafata <= 0)
                {
                    MessageBox.Show("Introduceți o suprafață validă.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!TryReadDecimal(PretUnitarDevizInput.Text, out var pretUnitar) || pretUnitar < 0)
                {
                    MessageBox.Show("Introduceți un preț unitar valid.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var consum = MaterialCalculator.CalculateConsumByType(material, suprafata);
                var pretTotal = MaterialCalculator.CalculatePretTotal(consum, pretUnitar);

                materialsCollection.Add(new DevizItem
                {
                    Material = material,
                    Suprafata = suprafata,
                    Consum = consum,
                    PretUnitar = pretUnitar,
                    PretTotal = pretTotal
                });

                MaterialInput.Clear();
                SuprafataDevizInput.Text = "100";
                PretUnitarDevizInput.Text = "150";
                ExportStatusText.Text = "Material adăugat. Recalculează devizul înainte de export.";
                MaterialInput.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (materialsCollection.Count == 0)
                {
                    MessageBox.Show("Adăugați cel puțin un material înainte de calcul.", "Avertisment", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                totalMateriale = materialsCollection.Sum(item => item.PretTotal);
                var (materiale, manopera, subtotal, tva, total) =
                    MaterialCalculator.CalculateDevizDetailed(totalMateriale, MaterialCalculator.TVA_PERCENT_DEFAULT);

                TotalMateriale.Text = $"{materiale:F2} MDL";
                TotalManopera.Text = $"{manopera:F2} MDL";
                Subtotal.Text = $"{subtotal:F2} MDL";
                TotalTVA.Text = $"{tva:F2} MDL";
                TotalFinal.Text = $"{total:F2} MDL";
                ExportStatusText.Text = "Deviz calculat. Poți exporta PDF sau Excel.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la calcul: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ExportPDFButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!TryReadTotals(out var totalMat, out _, out _, out _, out var totalFinal))
                    return;

                var fileName = $"Deviz_{SafeFileName(ClientInput.Text)}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                await SaveDevizAsOrderAsync(totalMat, totalFinal);
                GeneratePDF(filePath, totalMat, totalFinal);
                ExportStatusText.Text = $"PDF exportat: {fileName}";

                MessageBox.Show($"Deviz exportat cu succes:\n{filePath}", "Export PDF", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la export PDF: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!TryReadTotals(out var totalMat, out var manopera, out var subtotal, out var tva, out var totalFinal))
                    return;

                var fileName = $"Deviz_{SafeFileName(ClientInput.Text)}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                GenerateExcel(filePath, totalMat, manopera, subtotal, tva, totalFinal);
                ExportStatusText.Text = $"Excel exportat: {fileName}";

                MessageBox.Show($"Deviz Excel exportat cu succes:\n{filePath}", "Export Excel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la export Excel: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool TryReadTotals(out decimal totalMat, out decimal manopera, out decimal subtotal, out decimal tva, out decimal totalFinal)
        {
            totalMat = 0;
            manopera = 0;
            subtotal = 0;
            tva = 0;
            totalFinal = 0;

            if (materialsCollection.Count == 0)
            {
                MessageBox.Show("Adăugați cel puțin un material înainte de export.", "Avertisment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!ParseMoney(TotalMateriale.Text, out totalMat) ||
                !ParseMoney(TotalManopera.Text, out manopera) ||
                !ParseMoney(Subtotal.Text, out subtotal) ||
                !ParseMoney(TotalTVA.Text, out tva) ||
                !ParseMoney(TotalFinal.Text, out totalFinal) ||
                totalFinal <= 0)
            {
                MessageBox.Show("Calculați devizul înainte de export.", "Avertisment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private static bool ParseMoney(string text, out decimal value)
        {
            return TryReadDecimal(text.Replace("MDL", string.Empty).Trim(), out value);
        }

        private static bool TryReadDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result)
                || decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }

        private async System.Threading.Tasks.Task SaveDevizAsOrderAsync(decimal totalMat, decimal totalFinal)
        {
            _unitOfWork ??= Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
            if (_unitOfWork == null)
                throw new InvalidOperationException("Serviciile bazei de date nu sunt inițializate.");

            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var clientName = string.IsNullOrWhiteSpace(ClientInput.Text) ? "Client deviz" : ClientInput.Text.Trim();
                var clients = await _unitOfWork.ClientRepository.GetAllAsync();
                var client = clients.FirstOrDefault(c => c.Nume == clientName);
                if (client == null)
                {
                    client = new Client
                    {
                        Nume = clientName,
                        CUI = DateTime.Now.ToString("HHmmssff"),
                        Localitate = "Chișinău"
                    };
                    await _unitOfWork.ClientRepository.AddAsync(client);
                    await _unitOfWork.SaveChangesAsync();
                }

                var comanda = new Comanda
                {
                    IdClient = client.IdClient,
                    DataComanda = DateTime.Now,
                    Status = "Confirmata",
                    ValoareTotala = totalFinal,
                    TVA = (totalMat + totalMat * 0.35m) * 0.20m,
                    Observatii = $"Deviz PDF: {ProiectInput.Text}"
                };

                await _unitOfWork.ComandaRepository.AddAsync(comanda);
                await _unitOfWork.SaveChangesAsync();

                foreach (var item in materialsCollection)
                {
                    var allMaterials = await _unitOfWork.MaterialRepository.GetAllAsync();
                    var material = allMaterials.FirstOrDefault(m => m.Denumire == item.Material);
                    if (material == null)
                    {
                        material = new Material
                        {
                            Denumire = item.Material,
                            Tip = NormalizeFormulaType(item.Material),
                            Pret = item.PretUnitar,
                            Unitate = "mp",
                            DensitateKgM3 = 1,
                            ConductivitateTermica = 1
                        };
                        await _unitOfWork.MaterialRepository.AddAsync(material);
                        await _unitOfWork.SaveChangesAsync();
                    }

                    await _unitOfWork.DetaliiComandaRepository.AddAsync(new DetaliiComanda
                    {
                        IdComanda = comanda.IdComanda,
                        IdMaterial = material.IdMaterial,
                        Cantitate = item.Consum,
                        PretUnitar = item.PretUnitar,
                        PretTotal = item.PretTotal
                    });
                }
            });
        }

        private static string NormalizeFormulaType(string material)
        {
            var text = material?.ToLowerInvariant() ?? string.Empty;
            if (text.Contains("polistiren") || text.Contains("eps") || text.Contains("caparol"))
                return "Polistiren";
            if (text.Contains("diblu"))
                return "Dibluri";
            if (text.Contains("adeziv"))
                return "Adeziv";
            if (text.Contains("plasa") || text.Contains("plasă"))
                return "Plasa";
            if (text.Contains("tencu"))
                return "Tencuiala";
            if (text.Contains("amorsa") || text.Contains("grund"))
                return "Amorsa";
            return "Material";
        }

        private static string SafeFileName(string? value)
        {
            var name = string.IsNullOrWhiteSpace(value) ? "Client" : value.Trim().Replace(" ", "_");
            foreach (var invalid in Path.GetInvalidFileNameChars())
                name = name.Replace(invalid, '_');
            return name;
        }

        private void GenerateExcel(string filePath, decimal totalMateriale, decimal manopera, decimal subtotal, decimal tva, decimal totalFinal)
        {
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Deviz");

            sheet.Cell("A1").Value = "RED CONSTRUCT";
            sheet.Cell("A1").Style.Font.Bold = true;
            sheet.Cell("A1").Style.Font.FontSize = 16;
            sheet.Range("A1:E1").Merge();

            sheet.Cell("A3").Value = "Client";
            sheet.Cell("B3").Value = ClientInput.Text;
            sheet.Cell("A4").Value = "Proiect";
            sheet.Cell("B4").Value = ProiectInput.Text;
            sheet.Cell("A5").Value = "Data";
            sheet.Cell("B5").Value = DataDevizPicker.SelectedDate ?? DateTime.Today;
            sheet.Cell("B5").Style.DateFormat.Format = "dd.MM.yyyy";

            var row = 7;
            sheet.Cell(row, 1).Value = "Material";
            sheet.Cell(row, 2).Value = "Suprafață";
            sheet.Cell(row, 3).Value = "Consum";
            sheet.Cell(row, 4).Value = "Preț unitar";
            sheet.Cell(row, 5).Value = "Total";
            sheet.Range(row, 1, row, 5).Style.Font.Bold = true;
            sheet.Range(row, 1, row, 5).Style.Fill.BackgroundColor = XLColor.FromHtml("#D71920");
            sheet.Range(row, 1, row, 5).Style.Font.FontColor = XLColor.White;

            foreach (var item in materialsCollection)
            {
                row++;
                sheet.Cell(row, 1).Value = item.Material;
                sheet.Cell(row, 2).Value = item.Suprafata;
                sheet.Cell(row, 3).Value = item.Consum;
                sheet.Cell(row, 4).Value = item.PretUnitar;
                sheet.Cell(row, 5).Value = item.PretTotal;
            }

            row += 2;
            sheet.Cell(row, 4).Value = "Total materiale";
            sheet.Cell(row, 5).Value = totalMateriale;
            sheet.Cell(row + 1, 4).Value = "Manoperă (35%)";
            sheet.Cell(row + 1, 5).Value = manopera;
            sheet.Cell(row + 2, 4).Value = "Subtotal";
            sheet.Cell(row + 2, 5).Value = subtotal;
            sheet.Cell(row + 3, 4).Value = "TVA (20%)";
            sheet.Cell(row + 3, 5).Value = tva;
            sheet.Cell(row + 4, 4).Value = "TOTAL FINAL";
            sheet.Cell(row + 4, 5).Value = totalFinal;
            sheet.Range(row + 4, 4, row + 4, 5).Style.Font.Bold = true;

            sheet.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }

        private void GeneratePDF(string filePath, decimal totalMateriale, decimal totalFinal)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);

                    page.Header().Column(col =>
                    {
                        col.Item().Text("RED CONSTRUCT").FontSize(16).Bold().FontColor("#D71920");
                        col.Item().Text("DEVIZ PROIECT IZOLAȚIE TERMICĂ").FontSize(14).Bold();
                    });

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Client: {ClientInput.Text}");
                            row.RelativeItem().Text($"Proiect: {ProiectInput.Text}");
                        });

                        col.Item().Text($"Data: {DataDevizPicker.SelectedDate:dd.MM.yyyy}");

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Material").Bold();
                            row.RelativeItem().Text("Suprafață").Bold();
                            row.RelativeItem().Text("Consum").Bold();
                            row.RelativeItem().Text("Preț unit.").Bold();
                            row.RelativeItem().Text("Total").Bold();
                        });

                        foreach (var item in materialsCollection)
                        {
                            col.Item().Row(row =>
                            {
                                row.RelativeItem().Text(item.Material);
                                row.RelativeItem().Text($"{item.Suprafata:F2}");
                                row.RelativeItem().Text($"{item.Consum:F2}");
                                row.RelativeItem().Text($"{item.PretUnitar:F2} MDL");
                                row.RelativeItem().Text($"{item.PretTotal:F2} MDL");
                            });
                        }

                        var manopera = totalMateriale * 0.35m;
                        col.Item().PaddingTop(20).Row(row =>
                        {
                            row.RelativeItem().Text("Total materiale:");
                            row.RelativeItem().Text($"{totalMateriale:F2} MDL").Bold();
                        });
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Manoperă (35%):");
                            row.RelativeItem().Text($"{manopera:F2} MDL").Bold();
                        });
                        col.Item().PaddingTop(10).Row(row =>
                        {
                            row.RelativeItem().Text("TOTAL (cu TVA 20%):").Bold().FontSize(14);
                            row.RelativeItem().Text($"{totalFinal:F2} MDL").Bold().FontSize(14).FontColor("#D71920");
                        });
                    });

                    page.Footer().AlignCenter().Text($"Document generat: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                });
            }).GeneratePdf(filePath);
        }
    }
}
