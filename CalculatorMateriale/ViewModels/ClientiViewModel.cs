using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CalculatorMateriale.Data;
using CalculatorMateriale.Helpers;
using CalculatorMateriale.Models;
using Microsoft.Extensions.Logging;

namespace CalculatorMateriale.ViewModels
{
    public class ClientiViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClientiViewModel> _logger;
        private ObservableCollection<Client> _clienti;
        private Client _selectedClient;
        private string _searchText = string.Empty;
        private bool _isLoading;

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
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    SearchClientsCommand.Execute(null);
                }
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand LoadClientsCommand { get; }
        public ICommand AddClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand SearchClientsCommand { get; }

        public ClientiViewModel(IUnitOfWork unitOfWork, ILogger<ClientiViewModel> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Clienti = new ObservableCollection<Client>();
            LoadClientsCommand = new RelayCommand(_ => {
                _ = LoadClientsAsync();
            });
            AddClientCommand = new RelayCommand(_ => AddClient());
            EditClientCommand = new RelayCommand(_ => EditClient(), _ => SelectedClient != null);
            DeleteClientCommand = new RelayCommand(_ => {
                _ = DeleteClientAsync();
            }, _ => SelectedClient != null);
            SearchClientsCommand = new RelayCommand(_ => SearchClients());

            // ГЋncarcДѓ clienИ›ii la iniИ›ializare
            _ = LoadClientsAsync();
        }

        /// <summary>
        /// ГЋncarcДѓ toИ›i clienИ›ii din baza de date
        /// </summary>
        private async Task LoadClientsAsync()
        {
            try
            {
                IsLoading = true;
                _logger.LogInformation("Se Г®ncarcДѓ clienИ›ii din baza de date...");

                var clientiList = await _unitOfWork.ClientRepository.GetAllAsync();
                Clienti.Clear();

                foreach (var client in clientiList.OrderBy(c => c.Nume))
                {
                    Clienti.Add(client);
                }

                _logger.LogInformation($"S-au Г®ncДѓrcat cu succes {Clienti.Count} clienИ›i");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la Г®ncДѓrcarea clienИ›ilor: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// CautДѓ clienИ›i dupДѓ nume sau CUI
        /// </summary>
        private void SearchClients()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    // ReГ®ncarcДѓ toИ›i clienИ›ii dacДѓ textul de cДѓutare este gol
                    _ = LoadClientsAsync();
                    return;
                }

                var searchLower = SearchText.ToLower();
                var filtered = Clienti
                    .Where(c => c.Nume.ToLower().Contains(searchLower) ||
                                c.CUI.ToLower().Contains(searchLower) ||
                                (c.Localitate != null && c.Localitate.ToLower().Contains(searchLower)))
                    .ToList();

                Clienti.Clear();
                foreach (var client in filtered)
                {
                    Clienti.Add(client);
                }

                _logger.LogInformation($"CДѓutare: gДѓsit {filtered.Count} clienИ›i pentru '{SearchText}'");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la cДѓutarea clienИ›ilor: {ex.Message}");
            }
        }

        /// <summary>
        /// AdaugДѓ un nou client
        /// </summary>
        private void AddClient()
        {
            try
            {
                _logger.LogInformation("Se deschide formularul pentru adДѓugarea unui nou client");
                MessageBox.Show("Formularul pentru adДѓugarea unui nou client va fi deschis.", 
                               "AdaugДѓ Client", MessageBoxButton.OK, MessageBoxImage.Information);
                // Deschide ClientFormView pentru adДѓugare
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la adДѓugarea clientului: {ex.Message}");
                MessageBox.Show($"Eroare: {ex.Message}", "AdaugДѓ Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// EditeazДѓ clientul selectat
        /// </summary>
        private void EditClient()
        {
            if (SelectedClient == null)
                return;

            try
            {
                _logger.LogInformation($"Se deschide formularul pentru editarea clientului: {SelectedClient.Nume}");
                // Deschide ClientFormView cu datele clientului selectat
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la editarea clientului: {ex.Message}");
            }
        }

        /// <summary>
        /// Иterge clientul selectat cu confirmare
        /// </summary>
        private async Task DeleteClientAsync()
        {
            if (SelectedClient == null)
            {
                MessageBox.Show("SelectaИ›i un client pentru a-l И™terge.", 
                               "Иtergere Client", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var clientName = SelectedClient.Nume;
                
                // AfiИ™eazДѓ dialog de confirmare
                var result = MessageBox.Show(
                    $"SunteИ›i sigur cДѓ doriИ›i sДѓ И™tergeИ›i clientul \"{clientName}\"?\n\nAceastДѓ acИ›iune nu poate fi anulatДѓ.",
                    "Confirmare Иtergere",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No);

                if (result != MessageBoxResult.Yes)
                {
                    _logger.LogInformation($"Иtergerea clientului {clientName} a fost anulatДѓ de utilizator");
                    return;
                }

                _logger.LogInformation($"Se И™terge clientul: {clientName}");

                _unitOfWork.ClientRepository.Delete(SelectedClient);
                await _unitOfWork.SaveChangesAsync();

                await LoadClientsAsync();
                _logger.LogInformation($"Clientul {clientName} a fost И™ters cu succes");
                
                MessageBox.Show($"Clientul \"{clientName}\" a fost И™ters cu succes.",
                               "Иtergere Client", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la И™tergerea clientului: {ex.Message}");
                MessageBox.Show($"Eroare la И™tergerea clientului: {ex.Message}",
                               "Иtergere Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// AdaugДѓ un client la colecИ›ie (utilizat din formular)
        /// </summary>
        public async Task AddClientToCollectionAsync(Client client)
        {
            try
            {
                if (client == null)
                    return;

                _logger.LogInformation($"Se adaugДѓ clientul: {client.Nume}");

                await _unitOfWork.ClientRepository.AddAsync(client);
                await _unitOfWork.SaveChangesAsync();

                await LoadClientsAsync();
                _logger.LogInformation($"Clientul {client.Nume} a fost adДѓugat cu succes");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la salvarea clientului: {ex.Message}");
            }
        }

        /// <summary>
        /// ActualizeazДѓ clientul (utilizat din formular)
        /// </summary>
        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                if (client == null)
                    return;

                _logger.LogInformation($"Se actualizeazДѓ clientul: {client.Nume}");

                _unitOfWork.ClientRepository.Update(client);
                await _unitOfWork.SaveChangesAsync();

                await LoadClientsAsync();
                _logger.LogInformation($"Clientul {client.Nume} a fost actualizat cu succes");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la actualizarea clientului: {ex.Message}");
            }
        }
    }
}


