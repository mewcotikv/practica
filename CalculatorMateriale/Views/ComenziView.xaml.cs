using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.Views
{
    public partial class ComenziView : UserControl
    {
        private IUnitOfWork? _unitOfWork;
        private List<Comanda> _allOrders = new();
        private List<Material> _materials = new();
        private ObservableCollection<Comanda> comenziCollection = new();
        private ObservableCollection<DetaliiComanda> pozitiiCollection = new();

        public ComenziView()
        {
            InitializeComponent();
            Loaded += ComenziView_Loaded;
        }

        private async void ComenziView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _unitOfWork = Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
                if (_unitOfWork == null)
                {
                    MessageBox.Show("Serviciile nu sunt inițializate", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await LoadLookupsAsync();
                await LoadOrdersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la inițializare: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async System.Threading.Tasks.Task LoadLookupsAsync()
        {
            if (_unitOfWork == null)
                return;

            var clients = (await _unitOfWork.ClientRepository.GetAllAsync())
                .Where(c => c.Activ)
                .OrderBy(c => c.Nume)
                .ToList();

            ClientCombo.ItemsSource = clients;
            if (clients.Count > 0)
                ClientCombo.SelectedIndex = 0;

            _materials = (await _unitOfWork.MaterialRepository.GetAllAsync())
                .Where(m => m.Activ)
                .OrderBy(m => m.Denumire)
                .ToList();
            MaterialCombo.ItemsSource = _materials;
            if (_materials.Count > 0)
            {
                MaterialCombo.SelectedIndex = 0;
                UpdateSelectedMaterialPrice();
            }
            else
            {
                PretPozitieInput.Clear();
                StatusBarText.Text = "Nu există materiale active pentru poziții.";
            }
        }

        private async System.Threading.Tasks.Task LoadOrdersAsync()
        {
            if (_unitOfWork == null)
                return;

            try
            {
                StatusBarText.Text = "Se încarcă comenzile...";
                var orders = await _unitOfWork.ComandaRepository.GetAllAsync();
                _allOrders = orders.OrderByDescending(c => c.DataComanda).ToList();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcare comenzi: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusBarText.Text = "Eroare la încărcare";
            }
        }

        private async void NewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null)
                return;

            try
            {
                if (!TryReadOrderForm(out var order))
                    return;

                await _unitOfWork.ComandaRepository.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();
                ClearOrderForm();
                await LoadOrdersAsync();
                SelectOrder(order.IdComanda);
                StatusBarText.Text = "Comanda a fost adăugată.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la creare comandă: {ex.Message}", "Comandă nouă", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || ComenziGrid.SelectedItem is not Comanda selected)
            {
                StatusBarText.Text = "Selectați o comandă pentru actualizare.";
                return;
            }

            if (!TryReadOrderForm(out var form))
                return;

            selected.IdClient = form.IdClient;
            selected.DataLivrare = form.DataLivrare;
            selected.Status = form.Status;
            selected.ValoareTotala = form.ValoareTotala;
            selected.TVA = form.TVA;

            _unitOfWork.ComandaRepository.Update(selected);
            await _unitOfWork.SaveChangesAsync();
            await LoadOrdersAsync();
            StatusBarText.Text = $"Comanda #{selected.IdComanda} a fost actualizată.";
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Comanda comanda)
            {
                ComenziGrid.SelectedItem = comanda;
                FillOrderForm(comanda);
                StatusBarText.Text = $"Editare comandă #{comanda.IdComanda}.";
            }
        }

        private async void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is not Comanda comanda || _unitOfWork == null)
                return;

            var result = MessageBox.Show(
                $"Ești sigur că dorești să ștergi comanda {comanda.IdComanda}?",
                "Confirmare ștergere",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                _unitOfWork.ComandaRepository.Delete(comanda);
                await _unitOfWork.SaveChangesAsync();
                await LoadOrdersAsync();
                ClearPositions();
                MessageBox.Show("Comandă ștearsă cu succes", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la ștergere: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is not Comanda comanda || _unitOfWork == null)
                return;

            try
            {
                var currentStatus = comanda.Status ?? "Noua";
                var newStatus = currentStatus switch
                {
                    "Noua" => "Confirmata",
                    "Confirmata" => "Finalizata",
                    "Finalizata" => "Noua",
                    _ => "Noua"
                };

                comanda.Status = newStatus;
                _unitOfWork.ComandaRepository.Update(comanda);
                await _unitOfWork.SaveChangesAsync();
                await LoadOrdersAsync();
                StatusBarText.Text = $"Status schimbat în: {newStatus}.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la schimbarea statusului: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            var total = comenziCollection.Count;
            var confirmate = comenziCollection.Count(c => c.Status == "Confirmata");
            var finalizate = comenziCollection.Count(c => c.Status == "Finalizata");
            var valoare = comenziCollection.Sum(c => c.ValoareTotala);

            MessageBox.Show(
                $"Total comenzi: {total}\nConfirmate: {confirmate}\nFinalizate: {finalizate}\nValoare totală: {valoare:F2} MDL",
                "Raport Comenzi",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            if (IsLoaded)
                ApplyFilters();
        }

        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Clear();
            StatusFilter.SelectedIndex = 0;
            DateFilter.SelectedDate = null;
            ApplyFilters();
        }

        private void ComenziGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComenziGrid.SelectedItem is Comanda comanda)
            {
                FillOrderForm(comanda);
                _ = LoadPositionsAsync(comanda.IdComanda);
            }
        }

        private void ApplyFilters()
        {
            var query = _allOrders.AsEnumerable();
            var search = SearchBox?.Text?.Trim().ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    c.IdComanda.ToString().Contains(search) ||
                    (c.Client?.Nume ?? string.Empty).ToLowerInvariant().Contains(search) ||
                    (c.Observatii ?? string.Empty).ToLowerInvariant().Contains(search));
            }

            var status = (StatusFilter?.SelectedItem as ComboBoxItem)?.Tag?.ToString();
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(c => c.Status == status);

            if (DateFilter?.SelectedDate is DateTime date)
                query = query.Where(c => c.DataComanda.Date == date.Date || c.DataLivrare?.Date == date.Date);

            comenziCollection = new ObservableCollection<Comanda>(query.OrderByDescending(c => c.DataComanda));
            ComenziGrid.ItemsSource = comenziCollection;
            RecordCountText.Text = $"Total: {comenziCollection.Count} comenzi";
            StatusBarText.Text = "Gata";
        }

        private bool TryReadOrderForm(out Comanda order)
        {
            order = new Comanda();

            if (ClientCombo.SelectedValue is not int clientId || clientId <= 0)
            {
                StatusBarText.Text = "Selectați clientul comenzii.";
                return false;
            }

            decimal valoare;
            if (string.IsNullOrWhiteSpace(ValoareInput.Text))
            {
                valoare = 0;
            }
            else if (!TryReadDecimal(ValoareInput.Text, out valoare) || valoare < 0)
            {
                StatusBarText.Text = "Valoarea comenzii trebuie să fie un număr pozitiv.";
                return false;
            }

            decimal? tva;
            if (string.IsNullOrWhiteSpace(TvaInput.Text))
            {
                tva = decimal.Round(valoare * 0.20m, 2);
            }
            else if (TryReadDecimal(TvaInput.Text, out var tvaValue) && tvaValue >= 0)
            {
                tva = tvaValue;
            }
            else
            {
                StatusBarText.Text = "TVA trebuie să fie un număr pozitiv.";
                return false;
            }

            order.IdClient = clientId;
            order.DataComanda = DateTime.Now;
            order.DataLivrare = DataLivrareInput.SelectedDate;
            order.Status = (OrderStatusInput.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Noua";
            order.ValoareTotala = valoare;
            order.TVA = tva;
            return true;
        }

        private void FillOrderForm(Comanda comanda)
        {
            ClientCombo.SelectedValue = comanda.IdClient;
            ValoareInput.Text = comanda.ValoareTotala.ToString("0.##");
            TvaInput.Text = (comanda.TVA ?? 0).ToString("0.##");
            DataLivrareInput.SelectedDate = comanda.DataLivrare;

            foreach (ComboBoxItem item in OrderStatusInput.Items)
            {
                if (item.Content?.ToString() == comanda.Status)
                {
                    OrderStatusInput.SelectedItem = item;
                    break;
                }
            }
        }

        private void ClearOrderForm()
        {
            ValoareInput.Clear();
            TvaInput.Clear();
            DataLivrareInput.SelectedDate = null;
            OrderStatusInput.SelectedIndex = 0;
            if (ClientCombo.Items.Count > 0)
                ClientCombo.SelectedIndex = 0;
        }

        private async System.Threading.Tasks.Task LoadPositionsAsync(int idComanda)
        {
            if (_unitOfWork == null)
                return;

            var details = (await _unitOfWork.DetaliiComandaRepository.GetAllAsync())
                .Where(d => d.IdComanda == idComanda)
                .OrderBy(d => d.IdDetaliiComanda)
                .ToList();

            foreach (var detail in details)
            {
                detail.Material = _materials.FirstOrDefault(m => m.IdMaterial == detail.IdMaterial) ?? detail.Material;
            }

            pozitiiCollection = new ObservableCollection<DetaliiComanda>(details);
            PozitiiGrid.ItemsSource = pozitiiCollection;
            PozitiiTitle.Text = $"Poziții comandă #{idComanda}";
        }

        private async void AddPositionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || ComenziGrid.SelectedItem is not Comanda order)
            {
                StatusBarText.Text = "Selectați o comandă înainte de a adăuga poziții.";
                return;
            }

            if (MaterialCombo.SelectedValue is not int materialId || materialId <= 0)
            {
                StatusBarText.Text = "Selectați materialul poziției.";
                return;
            }

            if (string.IsNullOrWhiteSpace(PretPozitieInput.Text))
                UpdateSelectedMaterialPrice();

            if (!TryReadDecimal(CantitateInput.Text, out var cantitate) || cantitate <= 0)
            {
                StatusBarText.Text = "Cantitatea trebuie să fie mai mare decât 0.";
                return;
            }

            if (!TryReadDecimal(PretPozitieInput.Text, out var pretUnitar) || pretUnitar <= 0)
            {
                StatusBarText.Text = "Prețul unitar trebuie să fie mai mare decât 0.";
                return;
            }

            var detail = new DetaliiComanda
            {
                IdComanda = order.IdComanda,
                IdMaterial = materialId,
                Cantitate = cantitate,
                PretUnitar = pretUnitar,
                PretTotal = decimal.Round(cantitate * pretUnitar, 2)
            };

            await _unitOfWork.DetaliiComandaRepository.AddAsync(detail);
            await _unitOfWork.SaveChangesAsync();
            await RecalculateOrderTotalAsync(order.IdComanda);
            await LoadOrdersAsync();
            SelectOrder(order.IdComanda);
            CantitateInput.Clear();
            StatusBarText.Text = "Poziția a fost adăugată.";
        }

        private async void DeletePositionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || ComenziGrid.SelectedItem is not Comanda order || PozitiiGrid.SelectedItem is not DetaliiComanda detail)
            {
                StatusBarText.Text = "Selectați poziția pe care doriți să o ștergeți.";
                return;
            }

            _unitOfWork.DetaliiComandaRepository.Delete(detail);
            await _unitOfWork.SaveChangesAsync();
            await RecalculateOrderTotalAsync(order.IdComanda);
            await LoadOrdersAsync();
            SelectOrder(order.IdComanda);
            StatusBarText.Text = "Poziția a fost ștearsă.";
        }

        private async System.Threading.Tasks.Task RecalculateOrderTotalAsync(int idComanda)
        {
            if (_unitOfWork == null)
                return;

            var order = await _unitOfWork.ComandaRepository.GetByIdAsync(idComanda);
            if (order == null)
                return;

            var details = (await _unitOfWork.DetaliiComandaRepository.GetAllAsync())
                .Where(d => d.IdComanda == idComanda)
                .ToList();
            var total = details.Sum(d => d.PretTotal);

            order.ValoareTotala = total;
            order.TVA = decimal.Round(total * 0.20m, 2);
            _unitOfWork.ComandaRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        private void MaterialCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectedMaterialPrice();
        }

        private void UpdateSelectedMaterialPrice()
        {
            var material = MaterialCombo.SelectedItem as Material;
            if (material == null && MaterialCombo.SelectedValue is int materialId)
                material = _materials.FirstOrDefault(m => m.IdMaterial == materialId);

            if (material == null)
            {
                PretPozitieInput.Clear();
                return;
            }

            PretPozitieInput.Text = material.Pret.ToString("0.##");
            StatusBarText.Text = $"Preț completat automat: {material.Pret:0.##} MDL/{material.Unitate}.";
        }

        private void SelectOrder(int idComanda)
        {
            var order = comenziCollection.FirstOrDefault(c => c.IdComanda == idComanda);
            if (order != null)
                ComenziGrid.SelectedItem = order;
        }

        private void ClearPositions()
        {
            pozitiiCollection.Clear();
            PozitiiGrid.ItemsSource = pozitiiCollection;
            PozitiiTitle.Text = "Poziții comandă";
        }

        private static bool TryReadDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result)
                || decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }
    }
}
