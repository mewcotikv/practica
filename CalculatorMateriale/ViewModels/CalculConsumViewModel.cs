using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

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

        public ICommand LoadCalculConsume { get; }
        public ICommand AddCalculCommand { get; }
        public ICommand EditCalculCommand { get; }
        public ICommand DeleteCalculCommand { get; }
        public ICommand FilterByProjectCommand { get; }
        public ICommand ExportPDFCommand { get; }

        private async Task LoadCalculConsumeAsync()
        {
            var calcule = await _unitOfWork.CalculConsumRepository.GetAllAsync();
            CalculConsume = new ObservableCollection<CalculConsum>(calcule.OrderByDescending(c => c.DataCalcul));
            
            ConsumTotal = CalculConsume.Sum(c => c.ConsumTotal);
            PretTotal = CalculConsume.Sum(c => c.PretTotal);
        }

        private async Task AddCalcul()
        {
            // TODO: Implement add calculation logic
            await Task.CompletedTask;
        }

        private async Task EditCalcul()
        {
            // TODO: Implement edit calculation logic
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
            // TODO: Implement PDF export logic
        }
    }
}
