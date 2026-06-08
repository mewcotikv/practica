using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.Views
{
    public class StocMaterialItem : Material
    {
        public decimal ValoareStoc => Pret * StocDisponibil;
        public bool StocScazut => StocDisponibil < 20;
        public string StatusStoc => StocDisponibil == 0 ? "Epuizat" : StocScazut ? "Stoc scazut" : "Disponibil";
    }

    public partial class StocuriView : UserControl
    {
        private IUnitOfWork? _unitOfWork;
        private ObservableCollection<StocMaterialItem> _stocuri = new();

        public StocuriView()
        {
            InitializeComponent();
            Loaded += StocuriView_Loaded;
        }

        private async void StocuriView_Loaded(object sender, RoutedEventArgs e)
        {
            _unitOfWork = Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
            await LoadStocuriAsync();
        }

        private async Task LoadStocuriAsync()
        {
            if (_unitOfWork == null)
            {
                StatusText.Text = "Serviciile bazei de date nu sunt initializate.";
                return;
            }

            var materiale = await _unitOfWork.MaterialRepository.GetAllAsync();
            _stocuri = new ObservableCollection<StocMaterialItem>(materiale
                .OrderBy(m => m.StocDisponibil)
                .ThenBy(m => m.Denumire)
                .Select(m => new StocMaterialItem
                {
                    IdMaterial = m.IdMaterial,
                    Denumire = m.Denumire,
                    Tip = m.Tip,
                    Pret = m.Pret,
                    Unitate = m.Unitate,
                    DensitateKgM3 = m.DensitateKgM3,
                    ConductivitateTermica = m.ConductivitateTermica,
                    StocDisponibil = m.StocDisponibil,
                    Activ = m.Activ,
                    DataAdaugarii = m.DataAdaugarii
                }));

            StocuriGrid.ItemsSource = _stocuri;
            StatusText.Text = $"Total materiale in stoc: {_stocuri.Count}";
        }

        private async Task ChangeStockAsync(int direction)
        {
            if (_unitOfWork == null || StocuriGrid.SelectedItem is not StocMaterialItem selected)
            {
                StatusText.Text = "Selectati un material.";
                return;
            }

            if (!int.TryParse(CantitateInput.Text, out var quantity) || quantity <= 0)
            {
                StatusText.Text = "Cantitatea trebuie sa fie mai mare decat 0.";
                return;
            }

            var material = await _unitOfWork.MaterialRepository.GetByIdAsync(selected.IdMaterial);
            if (material == null)
            {
                StatusText.Text = "Materialul nu a fost gasit.";
                return;
            }

            material.StocDisponibil = direction > 0
                ? material.StocDisponibil + quantity
                : System.Math.Max(0, material.StocDisponibil - quantity);

            _unitOfWork.MaterialRepository.Update(material);
            await _unitOfWork.SaveChangesAsync();
            await LoadStocuriAsync();
        }

        private async void AddStockButton_Click(object sender, RoutedEventArgs e)
        {
            await ChangeStockAsync(1);
        }

        private async void RemoveStockButton_Click(object sender, RoutedEventArgs e)
        {
            await ChangeStockAsync(-1);
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadStocuriAsync();
        }
    }
}
