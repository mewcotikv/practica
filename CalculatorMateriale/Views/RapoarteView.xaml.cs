using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;
using ClosedXML.Excel;

namespace CalculatorMateriale.Views
{
    public class SalesReportItem
    {
        public int IdComanda { get; set; }
        public DateTime DataComanda { get; set; }
        public string Client { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal ValoareTotala { get; set; }
        public decimal TVA { get; set; }
        public string Materiale { get; set; } = string.Empty;
        public string Observatii { get; set; } = string.Empty;
    }

    public class RankingItem
    {
        public string Name { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }

    public partial class RapoarteView : UserControl
    {
        private IUnitOfWork? _unitOfWork;
        private ObservableCollection<SalesReportItem> _allItems = new();
        private ObservableCollection<SalesReportItem> _currentItems = new();
        private List<MaterialSaleItem> _allMaterialSales = new();

        public RapoarteView()
        {
            InitializeComponent();
            Loaded += RapoarteView_Loaded;
        }

        private async void RapoarteView_Loaded(object sender, RoutedEventArgs e)
        {
            _unitOfWork = Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
            if (_unitOfWork == null)
            {
                MessageBox.Show("Serviciile nu sunt initializate.", "Rapoarte vanzari", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StartDatePicker.SelectedDate = DateTime.Today.AddMonths(-1);
            EndDatePicker.SelectedDate = DateTime.Today;
            await LoadSalesAsync();
        }

        private async System.Threading.Tasks.Task LoadSalesAsync()
        {
            if (_unitOfWork == null)
                return;

            try
            {
                StatusText.Text = "Se incarca raportul...";

                var orders = (await _unitOfWork.ComandaRepository.GetAllAsync()).ToList();
                var details = (await _unitOfWork.DetaliiComandaRepository.GetAllAsync()).ToList();
                var materials = (await _unitOfWork.MaterialRepository.GetAllAsync()).ToDictionary(m => m.IdMaterial);

                var detailsByOrder = details
                    .GroupBy(d => d.IdComanda)
                    .ToDictionary(g => g.Key, g => g.ToList());

                _allItems = new ObservableCollection<SalesReportItem>(
                    orders
                        .OrderByDescending(o => o.DataComanda)
                        .Select(o => CreateSalesItem(o, detailsByOrder, materials)));

                var ordersById = orders.ToDictionary(o => o.IdComanda);
                _allMaterialSales = details
                    .Where(d => ordersById.ContainsKey(d.IdComanda))
                    .Select(d =>
                    {
                        var order = ordersById[d.IdComanda];
                        var name = materials.TryGetValue(d.IdMaterial, out var material) ? material.Denumire : $"Material #{d.IdMaterial}";
                        return new MaterialSaleItem
                        {
                            Name = name,
                            Total = d.PretTotal,
                            OrderId = d.IdComanda,
                            OrderDate = order.DataComanda,
                            Status = order.Status ?? string.Empty,
                            SearchText = $"{order.Client?.Nume} {name} {order.Observatii}".ToLowerInvariant()
                        };
                    })
                    .ToList();

                ApplyFilters();
                StatusText.Text = "Gata";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la incarcare raport vanzari: {ex.Message}", "Rapoarte vanzari",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Eroare la incarcare";
            }
        }

        private static SalesReportItem CreateSalesItem(
            Comanda order,
            Dictionary<int, List<DetaliiComanda>> detailsByOrder,
            Dictionary<int, Material> materials)
        {
            detailsByOrder.TryGetValue(order.IdComanda, out var orderDetails);
            var materialNames = (orderDetails ?? new List<DetaliiComanda>())
                .Select(d => materials.TryGetValue(d.IdMaterial, out var material)
                    ? $"{material.Denumire} ({d.Cantitate:N2} {material.Unitate})"
                    : $"Material #{d.IdMaterial} ({d.Cantitate:N2})")
                .ToList();

            var detailsTotal = (orderDetails ?? new List<DetaliiComanda>()).Sum(d => d.PretTotal);
            var orderTotal = order.ValoareTotala > 0 ? order.ValoareTotala : detailsTotal;

            return new SalesReportItem
            {
                IdComanda = order.IdComanda,
                DataComanda = order.DataComanda,
                Client = order.Client?.Nume ?? $"Client #{order.IdClient}",
                Status = order.Status ?? string.Empty,
                ValoareTotala = orderTotal,
                TVA = order.TVA ?? decimal.Round(orderTotal * 0.20m, 2),
                Materiale = materialNames.Count == 0 ? "-" : string.Join(", ", materialNames),
                Observatii = order.Observatii ?? string.Empty
            };
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var start = (StartDatePicker.SelectedDate ?? DateTime.MinValue).Date;
            var end = (EndDatePicker.SelectedDate ?? DateTime.MaxValue).Date.AddDays(1).AddTicks(-1);
            var status = (StatusCombo.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Toate";
            var search = (SearchBox.Text ?? string.Empty).Trim().ToLowerInvariant();

            var filtered = _allItems
                .Where(i => i.DataComanda >= start && i.DataComanda <= end)
                .Where(i => status == "Toate" || string.Equals(i.Status, status, StringComparison.OrdinalIgnoreCase))
                .Where(i => string.IsNullOrWhiteSpace(search)
                            || i.Client.ToLowerInvariant().Contains(search)
                            || i.Materiale.ToLowerInvariant().Contains(search)
                            || i.Observatii.ToLowerInvariant().Contains(search))
                .ToList();

            _currentItems = new ObservableCollection<SalesReportItem>(filtered);
            SalesGrid.ItemsSource = _currentItems;
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var totalSales = _currentItems.Sum(i => i.ValoareTotala);
            var totalTva = _currentItems.Sum(i => i.TVA);
            var topClient = _currentItems
                .GroupBy(i => i.Client)
                .Select(g => new RankingItem { Name = g.Key, Total = g.Sum(x => x.ValoareTotala) })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            TotalSalesText.Text = $"{totalSales:N2} MDL";
            TotalTvaText.Text = $"{totalTva:N2} MDL";
            OrdersCountText.Text = _currentItems.Count.ToString();
            TopClientText.Text = topClient?.Name ?? "-";
            RecordCountText.Text = $"Total: {_currentItems.Count} comenzi";

            TopClientsList.ItemsSource = _currentItems
                .GroupBy(i => i.Client)
                .Select(g => new RankingItem { Name = g.Key, Total = g.Sum(x => x.ValoareTotala) })
                .OrderByDescending(x => x.Total)
                .Take(6)
                .ToList();

            TopMaterialsList.ItemsSource = _allMaterialSales
                .Where(m => _currentItems.Any(i => i.IdComanda == m.OrderId))
                .GroupBy(m => m.Name)
                .Select(g => new RankingItem { Name = g.Key, Total = g.Sum(x => x.Total) })
                .OrderByDescending(x => x.Total)
                .Take(8)
                .ToList();
        }

        private void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentItems.Count == 0)
                {
                    MessageBox.Show("Nu sunt date pentru export.", "Rapoarte vanzari", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var fileName = $"Raport_Vanzari_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                using var workbook = new XLWorkbook();
                var sheet = workbook.Worksheets.Add("Vanzari");
                sheet.Cell(1, 1).Value = "Nr. comanda";
                sheet.Cell(1, 2).Value = "Data";
                sheet.Cell(1, 3).Value = "Client";
                sheet.Cell(1, 4).Value = "Status";
                sheet.Cell(1, 5).Value = "Valoare MDL";
                sheet.Cell(1, 6).Value = "TVA MDL";
                sheet.Cell(1, 7).Value = "Materiale";
                sheet.Cell(1, 8).Value = "Observatii";

                var header = sheet.Range(1, 1, 1, 8);
                header.Style.Font.Bold = true;
                header.Style.Font.FontColor = XLColor.White;
                header.Style.Fill.BackgroundColor = XLColor.FromArgb(0xD71920);

                var row = 2;
                foreach (var item in _currentItems)
                {
                    sheet.Cell(row, 1).Value = item.IdComanda;
                    sheet.Cell(row, 2).Value = item.DataComanda;
                    sheet.Cell(row, 2).Style.DateFormat.Format = "dd.mm.yyyy";
                    sheet.Cell(row, 3).Value = item.Client;
                    sheet.Cell(row, 4).Value = item.Status;
                    sheet.Cell(row, 5).Value = item.ValoareTotala;
                    sheet.Cell(row, 6).Value = item.TVA;
                    sheet.Cell(row, 7).Value = item.Materiale;
                    sheet.Cell(row, 8).Value = item.Observatii;
                    row++;
                }

                sheet.Cell(row + 1, 4).Value = "Total";
                sheet.Cell(row + 1, 5).Value = _currentItems.Sum(i => i.ValoareTotala);
                sheet.Cell(row + 1, 6).Value = _currentItems.Sum(i => i.TVA);
                sheet.Range(row + 1, 4, row + 1, 6).Style.Font.Bold = true;
                sheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);

                MessageBox.Show($"Raport exportat:\n{filePath}", "Rapoarte vanzari", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la export: {ex.Message}", "Rapoarte vanzari", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    internal class MaterialSaleItem : RankingItem
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SearchText { get; set; } = string.Empty;
    }
}
