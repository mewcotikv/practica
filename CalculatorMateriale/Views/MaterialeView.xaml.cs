using System;
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
                StatusText.Text = "Serviciile bazei de date nu sunt initializate.";
                return;
            }

            var materiale = await _unitOfWork.MaterialRepository.GetAllAsync();
            _materiale = new ObservableCollection<Material>(materiale.OrderBy(m => m.Tip).ThenBy(m => m.Denumire));
            MaterialeGrid.ItemsSource = _materiale;
            StatusText.Text = $"Total materiale: {_materiale.Count}";
        }

        private bool TryReadMaterial(out Material material)
        {
            material = new Material();

            if (string.IsNullOrWhiteSpace(DenumireInput.Text) || string.IsNullOrWhiteSpace(TipInput.Text))
            {
                StatusText.Text = "Denumirea si tipul sunt obligatorii.";
                return false;
            }

            if (!TryReadDecimal(PretInput.Text, out var pret) || pret <= 0)
            {
                StatusText.Text = "Pretul trebuie sa fie mai mare decat 0.";
                return false;
            }

            if (!int.TryParse(StocInput.Text, out var stoc) || stoc < 0)
            {
                StatusText.Text = "Stocul trebuie sa fie un numar pozitiv.";
                return false;
            }

            if (!TryReadDecimal(DensitateInput.Text, out var densitate) || densitate <= 0)
            {
                StatusText.Text = "Densitatea trebuie sa fie mai mare decat 0.";
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
            await LoadMaterialeAsync();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || MaterialeGrid.SelectedItem is not Material selected || !TryReadMaterial(out var form))
            {
                StatusText.Text = "Selectati un material pentru actualizare.";
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
            await LoadMaterialeAsync();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || MaterialeGrid.SelectedItem is not Material selected)
            {
                StatusText.Text = "Selectati un material pentru stergere.";
                return;
            }

            if (MessageBox.Show($"Stergeti materialul {selected.Denumire}?", "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            try
            {
                _unitOfWork.MaterialRepository.Delete(selected);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                selected.Activ = false;
                _unitOfWork.MaterialRepository.Update(selected);
                await _unitOfWork.SaveChangesAsync();
                MessageBox.Show("Materialul are date legate si a fost dezactivat in loc sa fie sters.", "Stergere", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            await LoadMaterialeAsync();
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadMaterialeAsync();
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
    }
}
