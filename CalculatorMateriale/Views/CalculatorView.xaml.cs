using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CalculatorMateriale.Data;
using CalculatorMateriale.Helpers;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.Views
{
    public partial class CalculatorView : UserControl
    {
        private ObservableCollection<CalculationResult> _results;
        private IUnitOfWork _unitOfWork;

        public class CalculationResult
        {
            public string Material { get; set; }
            public string Dimensiuni { get; set; }
            public string Grosime { get; set; }
            public decimal Suprafata { get; set; }
            public decimal Consum { get; set; }
            public decimal PretUnitar { get; set; }
            public decimal PretTotal { get; set; }
            public decimal PretCuTVA { get; set; }
            public DateTime DataCalcul { get; set; }
        }

        public CalculatorView()
        {
            InitializeComponent();
            _results = new ObservableCollection<CalculationResult>();
            Loaded += (s, e) => {
                if (ResultsDataGrid != null)
                    ResultsDataGrid.ItemsSource = _results;
            };
        }

        private void TipMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if controls are initialized
            if (GrosimeLabel == null || DimensiuniLabel == null) return;

            // Actualizează dimensiuni și grosimi în funcție de material selectat
            if (TipMaterialCombo?.SelectedItem is ComboBoxItem item)
            {
                string tipMaterial = item.Content?.ToString() ?? "";
                
                // Actualizează label și combobox-urile în funcție de tip
                switch (tipMaterial)
                {
                    case "Caparol":
                    case "EPS-80":
                        GrosimeLabel.Text = "Grosime (mm):";
                        GrosimeLabel.Visibility = Visibility.Visible;
                        GrosimePolistirenCombo.Visibility = Visibility.Visible;
                        DimensiuniLabel.Visibility = Visibility.Visible;
                        DimensiuniCombo.Visibility = Visibility.Visible;
                        break;
                    case "Polistiren Extrudat Gias":
                        GrosimeLabel.Text = "Grosime (mm):";
                        GrosimeLabel.Visibility = Visibility.Visible;
                        GrosimePolistirenCombo.Visibility = Visibility.Visible;
                        DimensiuniLabel.Visibility = Visibility.Visible;
                        DimensiuniCombo.Visibility = Visibility.Visible;
                        break;
                    case "Alt Material":
                    default:
                        GrosimeLabel.Visibility = Visibility.Collapsed;
                        GrosimePolistirenCombo.Visibility = Visibility.Collapsed;
                        DimensiuniLabel.Visibility = Visibility.Collapsed;
                        DimensiuniCombo.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!decimal.TryParse(SuprafataInput.Text, out decimal suprafata))
                {
                    MessageBox.Show("Suprafata trebuie sa fie un numar valid!", "EROARE", MessageBoxButton.OK, MessageBoxImage.Error);
                    SuprafataInput.Focus();
                    return;
                }

                if (suprafata <= 0)
                {
                    MessageBox.Show("Suprafata trebuie sa fie mai mare decat 0 m2!", "EROARE", MessageBoxButton.OK, MessageBoxImage.Error);
                    SuprafataInput.Focus();
                    return;
                }

                if (!decimal.TryParse(PretUnitarInput.Text, out decimal pretUnitar) || pretUnitar < 0)
                {
                    MessageBox.Show("Pretul trebuie sa fie >= 0 MDL!", "EROARE", MessageBoxButton.OK, MessageBoxImage.Error);
                    PretUnitarInput.Focus();
                    return;
                }

                string tipMaterial = (TipMaterialCombo.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Caparol";
                string formulaType = NormalizeFormulaType(tipMaterial);
                string dimensiuni = "";
                if (DimensiuniCombo.Visibility == Visibility.Visible)
                    dimensiuni = (DimensiuniCombo.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";
                
                string grosimeText = "";
                if (GrosimePolistirenCombo.Visibility == Visibility.Visible)
                    grosimeText = (GrosimePolistirenCombo.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "100 mm";

                decimal consumTotal = MaterialCalculator.CalculateConsumByType(formulaType, suprafata);
                decimal pretTotal = MaterialCalculator.CalculatePretTotal(consumTotal, pretUnitar);
                decimal pretCuTVA = MaterialCalculator.CalculatePretCuTVA(pretTotal, MaterialCalculator.TVA_PERCENT_DEFAULT);

                string rezultat = $"{consumTotal:F2} unitati";
                if (!string.IsNullOrEmpty(grosimeText))
                    rezultat += $" ({grosimeText})";
                if (!string.IsNullOrEmpty(dimensiuni))
                    rezultat += $"\n{dimensiuni}";
                
                ConsumTotalResult.Text = rezultat;
                PretTotalResult.Text = $"{pretTotal:F2} MDL";
                PretCuTVAResult.Text = $"{pretCuTVA:F2} MDL (TVA inclus)";
                RightMaterialResult.Text = $"{tipMaterial} {grosimeText}".Trim();
                RightConsumTotalResult.Text = rezultat;
                RightPretTotalResult.Text = $"{pretTotal:F2} MDL";
                RightPretCuTVAResult.Text = $"{pretCuTVA:F2} MDL";

                var result = new CalculationResult
                {
                    Material = tipMaterial,
                    Dimensiuni = dimensiuni,
                    Grosime = grosimeText,
                    Suprafata = suprafata,
                    Consum = consumTotal,
                    PretUnitar = pretUnitar,
                    PretTotal = pretTotal,
                    PretCuTVA = pretCuTVA,
                    DataCalcul = DateTime.Now
                };

                _results.Add(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"EROARE: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_results.Count == 0)
            {
                MessageBox.Show("Nu sunt calcule de salvat!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _unitOfWork ??= Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
                if (_unitOfWork == null)
                {
                    MessageBox.Show("Serviciile bazei de date nu sunt initializate.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var client = (await _unitOfWork.ClientRepository.GetAllAsync()).FirstOrDefault()
                    ?? new Client { Nume = "RED Construct SRL", CUI = "10000001", Localitate = "Chisinau" };
                if (client.IdClient == 0)
                {
                    await _unitOfWork.ClientRepository.AddAsync(client);
                    await _unitOfWork.SaveChangesAsync();
                }

                var obiectiv = (await _unitOfWork.ObiectivRepository.GetAllAsync()).FirstOrDefault()
                    ?? new Obiectiv
                    {
                        Denumire = "Obiectiv demo termoizolatie",
                        IdClient = client.IdClient,
                        SuprafataM2 = _results.Last().Suprafata,
                        Status = "Activ"
                    };
                if (obiectiv.IdObiectiv == 0)
                {
                    await _unitOfWork.ObiectivRepository.AddAsync(obiectiv);
                    await _unitOfWork.SaveChangesAsync();
                }

                foreach (var result in _results)
                {
                    string formulaType = NormalizeFormulaType(result.Material);
                    var material = (await _unitOfWork.MaterialRepository.GetAllAsync())
                        .FirstOrDefault(m => m.Tip.Equals(formulaType, StringComparison.OrdinalIgnoreCase) ||
                                             m.Denumire.Equals(result.Material, StringComparison.OrdinalIgnoreCase));

                    if (material == null)
                    {
                        material = new Material
                        {
                            Denumire = result.Material,
                            Tip = formulaType,
                            Pret = result.PretUnitar,
                            Unitate = formulaType == "dibluri" ? "buc" : "mp",
                            DensitateKgM3 = 1,
                            ConductivitateTermica = 1,
                            StocDisponibil = 0
                        };
                        await _unitOfWork.MaterialRepository.AddAsync(material);
                        await _unitOfWork.SaveChangesAsync();
                    }

                    await _unitOfWork.CalculConsumRepository.AddAsync(new CalculConsum
                    {
                        IdObiectiv = obiectiv.IdObiectiv,
                        IdMaterial = material.IdMaterial,
                        ConsumPeM2 = result.Suprafata > 0 ? result.Consum / result.Suprafata : 0,
                        ConsumTotal = result.Consum,
                        PretUnitar = result.PretUnitar,
                        PretTotal = result.PretTotal,
                        Observatii = $"Calcul UI: {result.Material}, {result.Dimensiuni}, {result.Grosime}"
                    });
                }

                await _unitOfWork.SaveChangesAsync();
                MessageBox.Show($"SUCCES: {_results.Count} calcule salvate in CalculConsum!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la salvarea calculelor: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultsDataGrid.SelectedItem is CalculationResult selected)
            {
                _results.Remove(selected);
                MessageBox.Show("SUCCES: Rand sters!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Selectati un rand!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (_results.Count == 0) return;
            if (MessageBox.Show("Stergeti toate calculele?", "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _results.Clear();
                MessageBox.Show("SUCCES: Tabel sters!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private static string NormalizeFormulaType(string material)
        {
            var text = material?.ToLowerInvariant() ?? string.Empty;
            if (text.Contains("caparol") || text.Contains("eps") || text.Contains("polistiren"))
                return "polistiren";
            if (text.Contains("diblu"))
                return "dibluri";
            if (text.Contains("adeziv"))
                return "adeziv";
            if (text.Contains("plasa") || text.Contains("plasă"))
                return "plasa";
            if (text.Contains("tencu"))
                return "tencuiala";
            if (text.Contains("amorsa") || text.Contains("grund"))
                return "amorsa";
            return text;
        }
    }
}
