using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.Views
{
    public partial class MaterialeView : UserControl
    {
        private IUnitOfWork? _unitOfWork;
        private List<Material> _allMateriale = new();
        private ObservableCollection<Material> _materiale = new();

        public MaterialeView()
        {
            InitializeComponent();
            Loaded += MaterialeView_Loaded;
        }

        private async void MaterialeView_Loaded(object sender, RoutedEventArgs e)
        {
            _unitOfWork = Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
            await LoadMaterialeAsync();
        }

        private async Task LoadMaterialeAsync()
        {
            if (_unitOfWork == null)
            {
                StatusText.Text = "Serviciile bazei de date nu sunt inițializate.";
                return;
            }

            var materiale = await _unitOfWork.MaterialRepository.GetAllAsync();
            _allMateriale = materiale.OrderBy(m => m.Tip).ThenBy(m => m.Denumire).ToList();
            ApplyMaterialFilter();
        }

        private void ApplyMaterialFilter()
        {
            var search = MaterialSearchInput?.Text?.Trim().ToLowerInvariant();
            var materiale = _allMateriale.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                materiale = materiale.Where(m =>
                    m.Denumire.ToLowerInvariant().Contains(search) ||
                    m.Tip.ToLowerInvariant().Contains(search) ||
                    m.Unitate.ToLowerInvariant().Contains(search));
            }

            _materiale = new ObservableCollection<Material>(materiale);
            MaterialeGrid.ItemsSource = _materiale;
            StatusText.Text = $"Total materiale: {_materiale.Count}";
        }

        private bool TryReadMaterial(out Material material)
        {
            material = new Material();

            if (string.IsNullOrWhiteSpace(DenumireInput.Text) || string.IsNullOrWhiteSpace(TipInput.Text))
            {
                StatusText.Text = "Denumirea și tipul sunt obligatorii.";
                return false;
            }

            if (!TryReadDecimal(PretInput.Text, out var pret) || pret <= 0)
            {
                StatusText.Text = "Prețul trebuie să fie mai mare decât 0.";
                return false;
            }

            if (!int.TryParse(StocInput.Text, out var stoc) || stoc < 0)
            {
                StatusText.Text = "Stocul trebuie să fie un număr pozitiv.";
                return false;
            }

            if (!TryReadDecimal(DensitateInput.Text, out var densitate) || densitate <= 0)
            {
                StatusText.Text = "Densitatea trebuie să fie mai mare decât 0.";
                return false;
            }

            material.Denumire = DenumireInput.Text.Trim();
            material.Tip = TipInput.Text.Trim();
            material.Pret = pret;
            material.Unitate = string.IsNullOrWhiteSpace(UnitateInput.Text) ? "buc" : UnitateInput.Text.Trim();
            material.StocDisponibil = stoc;
            material.DensitateKgM3 = densitate;
            material.ConductivitateTermica = 0.035m;
            material.Activ = true;
            material.DataAdaugarii = DateTime.Now;
            return true;
        }

        private static bool TryReadDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result)
                || decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || !TryReadMaterial(out var material))
                return;

            await _unitOfWork.MaterialRepository.AddAsync(material);
            await _unitOfWork.SaveChangesAsync();
            ClearForm();
            await LoadMaterialeAsync();
            StatusText.Text = "Materialul a fost adăugat.";
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || MaterialeGrid.SelectedItem is not Material selected || !TryReadMaterial(out var form))
            {
                StatusText.Text = "Selectați un material pentru actualizare.";
                return;
            }

            selected.Denumire = form.Denumire;
            selected.Tip = form.Tip;
            selected.Pret = form.Pret;
            selected.Unitate = form.Unitate;
            selected.StocDisponibil = form.StocDisponibil;
            selected.DensitateKgM3 = form.DensitateKgM3;

            _unitOfWork.MaterialRepository.Update(selected);
            await _unitOfWork.SaveChangesAsync();
            ClearForm();
            await LoadMaterialeAsync();
            StatusText.Text = "Materialul a fost actualizat.";
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || MaterialeGrid.SelectedItem is not Material selected)
            {
                StatusText.Text = "Selectați un material pentru ștergere.";
                return;
            }

            if (MessageBox.Show($"Ștergeți materialul {selected.Denumire}?", "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            try
            {
                _unitOfWork.MaterialRepository.Delete(selected);
                await _unitOfWork.SaveChangesAsync();
                StatusText.Text = "Materialul a fost șters.";
            }
            catch
            {
                selected.Activ = false;
                _unitOfWork.MaterialRepository.Update(selected);
                await _unitOfWork.SaveChangesAsync();
                MessageBox.Show("Materialul are date legate și a fost dezactivat în loc să fie șters.", "Ștergere", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            ClearForm();
            await LoadMaterialeAsync();
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadMaterialeAsync();
        }

        private void MaterialSearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
                ApplyMaterialFilter();
        }

        private void ClearFormButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            MaterialeGrid.SelectedItem = null;
            MaterialSearchInput.Clear();
            ApplyMaterialFilter();
        }

        private void MaterialeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaterialeGrid.SelectedItem is not Material material)
                return;

            DenumireInput.Text = material.Denumire;
            TipInput.Text = material.Tip;
            PretInput.Text = material.Pret.ToString("0.##");
            UnitateInput.Text = material.Unitate;
            StocInput.Text = material.StocDisponibil.ToString();
            DensitateInput.Text = material.DensitateKgM3.ToString("0.##");
        }

        private void ClearForm()
        {
            DenumireInput.Clear();
            TipInput.Clear();
            PretInput.Clear();
            UnitateInput.Clear();
            StocInput.Clear();
            DensitateInput.Clear();
        }
    }
}
