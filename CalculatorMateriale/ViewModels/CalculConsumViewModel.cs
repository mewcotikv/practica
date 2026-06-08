using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;
using CalculatorMateriale.Helpers;

namespace CalculatorMateriale.ViewModels
{
    public class CalculConsumViewModel : Helpers.ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ObservableCollection<CalculConsum> _calculConsume;
        private CalculConsum _selectedCalcul;
        private Obiectiv _selectedObiectiv;
        private decimal _consumTotal;
        private decimal _pretTotal;
        private decimal _suprafata;
        private string _tipMaterial = "Polistiren";
        private decimal _pretUnitar;
        private string _grosimePolistiren = "100 mm";

        public CalculConsumViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _calculConsume = new ObservableCollection<CalculConsum>();

            LoadCalculConsume = new Helpers.RelayCommand(async _ => await LoadCalculConsumeAsync());
            AddCalculCommand = new Helpers.RelayCommand(async _ => await AddCalcul(), _ => SelectedObiectiv != null);
            EditCalculCommand = new Helpers.RelayCommand(async _ => await EditCalcul(), _ => SelectedCalcul != null);
            DeleteCalculCommand = new Helpers.RelayCommand(async _ => await DeleteCalcul(), _ => SelectedCalcul != null);
            FilterByProjectCommand = new Helpers.RelayCommand(async _ => await FilterByProject());
            ExportPDFCommand = new Helpers.RelayCommand(_ => ExportPDF(), _ => SelectedObiectiv != null);
            CalculateConsumCommand = new Helpers.RelayCommand(_ => CalculateConsumption());
        }

        public ObservableCollection<CalculConsum> CalculConsume
        {
            get => _calculConsume;
            set => SetProperty(ref _calculConsume, value);
        }

        public CalculConsum SelectedCalcul
        {
            get => _selectedCalcul;
            set => SetProperty(ref _selectedCalcul, value);
        }

        public Obiectiv SelectedObiectiv
        {
            get => _selectedObiectiv;
            set => SetProperty(ref _selectedObiectiv, value);
        }

        public decimal ConsumTotal
        {
            get => _consumTotal;
            set => SetProperty(ref _consumTotal, value);
        }

        public decimal PretTotal
        {
            get => _pretTotal;
            set => SetProperty(ref _pretTotal, value);
        }

        public decimal Suprafata
        {
            get => _suprafata;
            set => SetProperty(ref _suprafata, value);
        }

        public string TipMaterial
        {
            get => _tipMaterial;
            set => SetProperty(ref _tipMaterial, value);
        }

        public decimal PretUnitar
        {
            get => _pretUnitar;
            set => SetProperty(ref _pretUnitar, value);
        }

        public string GrosimePolistiren
        {
            get => _grosimePolistiren;
            set => SetProperty(ref _grosimePolistiren, value);
        }

        public ICommand LoadCalculConsume { get; }
        public ICommand AddCalculCommand { get; }
        public ICommand EditCalculCommand { get; }
        public ICommand DeleteCalculCommand { get; }
        public ICommand FilterByProjectCommand { get; }
        public ICommand ExportPDFCommand { get; }
        public ICommand CalculateConsumCommand { get; }

        private async Task LoadCalculConsumeAsync()
        {
            var calcule = await _unitOfWork.CalculConsumRepository.GetAllAsync();
            CalculConsume = new ObservableCollection<CalculConsum>(calcule.OrderByDescending(c => c.DataCalcul));
            
            ConsumTotal = CalculConsume.Sum(c => c.ConsumTotal);
            PretTotal = CalculConsume.Sum(c => c.PretTotal);
        }

        private async Task AddCalcul()
        {
            // Implement add calculation logic
            await Task.CompletedTask;
        }

        private async Task EditCalcul()
        {
            // Implement edit calculation logic
            await Task.CompletedTask;
        }

        private async Task DeleteCalcul()
        {
            if (SelectedCalcul != null)
            {
                _unitOfWork.CalculConsumRepository.Delete(SelectedCalcul);
                await _unitOfWork.SaveChangesAsync();
                await LoadCalculConsumeAsync();
            }
        }

        private async Task FilterByProject()
        {
            if (SelectedObiectiv != null)
            {
                var calcule = await _unitOfWork.CalculConsumRepository.GetAllAsync();
                var filtered = calcule
                    .Where(c => c.IdObiectiv == SelectedObiectiv.IdObiectiv)
                    .OrderByDescending(c => c.DataCalcul)
                    .ToList();
                CalculConsume = new ObservableCollection<CalculConsum>(filtered);
                
                ConsumTotal = CalculConsume.Sum(c => c.ConsumTotal);
                PretTotal = CalculConsume.Sum(c => c.PretTotal);
            }
        }

        private void ExportPDF()
        {
            // Implement PDF export logic
        }

        /// <summary>
        /// CalculeazДѓ consumul materialelor Г®n funcИ›ie de tip
        /// </summary>
        private void CalculateConsumption()
        {
            if (Suprafata <= 0)
                return;

            decimal consum = 0;
            string unitati = "";

            switch (TipMaterial?.ToLower())
            {
                case "polistiren":
                    consum = CalculatePolistirenConsumption(Suprafata);
                    unitati = "mp";
                    break;
                case "dibluri":
                    consum = CalculateDibluriConsumption(Suprafata);
                    unitati = "buc";
                    break;
                case "adeziv":
                    consum = CalculateAdezivConsumption(Suprafata);
                    unitati = "kg";
                    break;
                case "plasa":
                    consum = CalculatePlasaConsumption(Suprafata);
                    unitati = "mp";
                    break;
                case "tencuiala":
                    consum = CalculateTencuialaConsumption(Suprafata);
                    unitati = "kg";
                    break;
                case "amorsa":
                    consum = CalculateAmorsaConsumption(Suprafata);
                    unitati = "l";
                    break;
            }

            ConsumTotal = consum;
            PretTotal = MaterialCalculator.CalculatePretTotal(consum, PretUnitar);
        }

        /// <summary>
        /// CalculeazДѓ consumul de Polistiren: SuprafaИ›Дѓ Г— 1.10
        /// </summary>
        private decimal CalculatePolistirenConsumption(decimal suprafata)
        {
            return MaterialCalculator.CalculatePolistiren(suprafata);
        }

        /// <summary>
        /// CalculeazДѓ consumul de Dibluri: SuprafaИ›Дѓ Г— 6
        /// </summary>
        private decimal CalculateDibluriConsumption(decimal suprafata)
        {
            return MaterialCalculator.CalculateDibluri(suprafata);
        }

        /// <summary>
        /// CalculeazДѓ consumul de Adeziv: SuprafaИ›Дѓ Г· 6
        /// </summary>
        private decimal CalculateAdezivConsumption(decimal suprafata)
        {
            return MaterialCalculator.CalculateAdeziv(suprafata);
        }

        /// <summary>
        /// CalculeazДѓ consumul de Plasa: SuprafaИ›Дѓ Г— 1.15
        /// </summary>
        private decimal CalculatePlasaConsumption(decimal suprafata)
        {
            return MaterialCalculator.CalculatePlasa(suprafata);
        }

        /// <summary>
        /// CalculeazДѓ consumul de Tencuiala: SuprafaИ›Дѓ Г· 4
        /// </summary>
        private decimal CalculateTencuialaConsumption(decimal suprafata)
        {
            return MaterialCalculator.CalculateTencuiala(suprafata);
        }

        /// <summary>
        /// CalculeazДѓ consumul de Amorsa: SuprafaИ›Дѓ Г· 10
        /// </summary>
        private decimal CalculateAmorsaConsumption(decimal suprafata)
        {
            return MaterialCalculator.CalculateAmorsa(suprafata);
        }
    }
}

