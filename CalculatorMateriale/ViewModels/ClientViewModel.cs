using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.ViewModels
{
    public class ClientViewModel : Helpers.ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ObservableCollection<Client> _clienti;
        private Client _selectedClient;
        private string _searchText;

        public ClientViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _clienti = new ObservableCollection<Client>();

            LoadClientiCommand = new Helpers.RelayCommand(async _ => await LoadClienti());
            AddClientCommand = new Helpers.RelayCommand(async _ => await AddClient());
            EditClientCommand = new Helpers.RelayCommand(async _ => await EditClient(), _ => SelectedClient != null);
            DeleteClientCommand = new Helpers.RelayCommand(async _ => await DeleteClient(), _ => SelectedClient != null);
            SearchCommand = new Helpers.RelayCommand(async _ => await Search());
        }

        public ObservableCollection<Client> Clienti
        {
            get => _clienti;
            set => SetProperty(ref _clienti, value);
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ICommand LoadClientiCommand { get; }
        public ICommand AddClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SearchCommand { get; }

        private async Task LoadClienti()
        {
            var clienti = await _unitOfWork.ClientRepository.GetAllAsync();
            Clienti = new ObservableCollection<Client>(clienti);
        }

        private async Task AddClient()
        {
            // TODO: Implement add client logic
            await Task.CompletedTask;
        }

        private async Task EditClient()
        {
            // TODO: Implement edit client logic
            await Task.CompletedTask;
        }

        private async Task DeleteClient()
        {
            if (SelectedClient != null)
            {
                _unitOfWork.ClientRepository.Delete(SelectedClient);
                await _unitOfWork.SaveChangesAsync();
                await LoadClienti();
            }
        }

        private async Task Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadClienti();
            }
            else
            {
                var allClienti = await _unitOfWork.ClientRepository.GetAllAsync();
                var filtered = allClienti
                    .Where(c => c.Nume.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                               c.CUI.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                Clienti = new ObservableCollection<Client>(filtered);
            }
        }
    }
}
