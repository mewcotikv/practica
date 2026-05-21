using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.ViewModels
{
    public class ObiectivViewModel : Helpers.ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ObservableCollection<Obiectiv> _obiective;
        private Obiectiv _selectedObiectiv;
        private Client _selectedClient;

        public ObiectivViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _obiective = new ObservableCollection<Obiectiv>();

            LoadObiectiveCommand = new Helpers.RelayCommand(async _ => await LoadObiective());
            AddObiectivCommand = new Helpers.RelayCommand(async _ => await AddObiectiv(), _ => SelectedClient != null);
            EditObiectivCommand = new Helpers.RelayCommand(async _ => await EditObiectiv(), _ => SelectedObiectiv != null);
            DeleteObiectivCommand = new Helpers.RelayCommand(async _ => await DeleteObiectiv(), _ => SelectedObiectiv != null);
            FilterByClientCommand = new Helpers.RelayCommand(async _ => await FilterByClient());
        }

        public ObservableCollection<Obiectiv> Obiective
        {
            get => _obiective;
            set => SetProperty(ref _obiective, value);
        }

        public Obiectiv SelectedObiectiv
        {
            get => _selectedObiectiv;
            set => SetProperty(ref _selectedObiectiv, value);
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        public ICommand LoadObiectiveCommand { get; }
        public ICommand AddObiectivCommand { get; }
        public ICommand EditObiectivCommand { get; }
        public ICommand DeleteObiectivCommand { get; }
        public ICommand FilterByClientCommand { get; }

        private async Task LoadObiective()
        {
            var obiective = await _unitOfWork.ObiectivRepository.GetAllAsync();
            Obiective = new ObservableCollection<Obiectiv>(obiective.OrderByDescending(o => o.DataCrearii));
        }

        private async Task AddObiectiv()
        {
            // TODO: Implement add project logic
            await Task.CompletedTask;
        }

        private async Task EditObiectiv()
        {
            // TODO: Implement edit project logic
            await Task.CompletedTask;
        }

        private async Task DeleteObiectiv()
        {
            if (SelectedObiectiv != null)
            {
                _unitOfWork.ObiectivRepository.Delete(SelectedObiectiv);
                await _unitOfWork.SaveChangesAsync();
                await LoadObiective();
            }
        }

        private async Task FilterByClient()
        {
            if (SelectedClient != null)
            {
                var obiective = await _unitOfWork.ObiectivRepository.GetAllAsync();
                var filtered = obiective
                    .Where(o => o.IdClient == SelectedClient.IdClient)
                    .OrderByDescending(o => o.DataCrearii)
                    .ToList();
                Obiective = new ObservableCollection<Obiectiv>(filtered);
            }
            else
            {
                await LoadObiective();
            }
        }
    }
}
