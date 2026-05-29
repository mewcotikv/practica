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
            LoadClientsCommand = new RelayCommand(_ => LoadClientsAsync());
            AddClientCommand = new RelayCommand(_ => AddClient());
            EditClientCommand = new RelayCommand(_ => EditClient(), _ => SelectedClient != null);
            DeleteClientCommand = new RelayCommand(_ => DeleteClientAsync(), _ => SelectedClient != null);
            SearchClientsCommand = new RelayCommand(_ => SearchClients());

            // Încarcă clienții la inițializare
            _ = LoadClientsAsync();
        }

        /// <summary>
        /// Încarcă toți clienții din baza de date
        /// </summary>
        private async Task LoadClientsAsync()
        {
            try
            {
                IsLoading = true;
                _logger.LogInformation("Se încarcă clienții din baza de date...");

                var clientiList = await _unitOfWork.ClientRepository.GetAllAsync();
                Clienti.Clear();

                foreach (var client in clientiList.OrderBy(c => c.Nume))
                {
                    Clienti.Add(client);
                }

                _logger.LogInformation($"S-au încărcat cu succes {Clienti.Count} clienți");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la încărcarea clienților: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Caută clienți după nume sau CUI
        /// </summary>
        private void SearchClients()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    // Reîncarcă toți clienții dacă textul de căutare este gol
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

                _logger.LogInformation($"Căutare: găsit {filtered.Count} clienți pentru '{SearchText}'");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la căutarea clienților: {ex.Message}");
            }
        }

        /// <summary>
        /// Adaugă un nou client
        /// </summary>
        private void AddClient()
        {
            try
            {
                _logger.LogInformation("Se deschide formularul pentru adăugarea unui nou client");
                MessageBox.Show("Formularul pentru adăugarea unui nou client va fi deschis.", 
                               "Adaugă Client", MessageBoxButton.OK, MessageBoxImage.Information);
                // TODO: Deschide ClientFormView pentru adăugare
                // Aceasta va fi implementată la integrarea cu MainWindow
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la adăugarea clientului: {ex.Message}");
                MessageBox.Show($"Eroare: {ex.Message}", "Adaugă Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Editează clientul selectat
        /// </summary>
        private void EditClient()
        {
            if (SelectedClient == null)
                return;

            try
            {
                _logger.LogInformation($"Se deschide formularul pentru editarea clientului: {SelectedClient.Nume}");
                // TODO: Deschide ClientFormView cu datele clientului selectat
                // Aceasta va fi implementată la integrarea cu MainWindow
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la editarea clientului: {ex.Message}");
            }
        }

        /// <summary>
        /// Șterge clientul selectat cu confirmare
        /// </summary>
        private async Task DeleteClientAsync()
        {
            if (SelectedClient == null)
            {
                MessageBox.Show("Selectați un client pentru a-l șterge.", 
                               "Ștergere Client", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var clientName = SelectedClient.Nume;
                
                // Afișează dialog de confirmare
                var result = MessageBox.Show(
                    $"Sunteți sigur că doriți să ștergeți clientul \"{clientName}\"?\n\nAceastă acțiune nu poate fi anulată.",
                    "Confirmare Ștergere",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No);

                if (result != MessageBoxResult.Yes)
                {
                    _logger.LogInformation($"Ștergerea clientului {clientName} a fost anulată de utilizator");
                    return;
                }

                _logger.LogInformation($"Se șterge clientul: {clientName}");

                _unitOfWork.ClientRepository.Delete(SelectedClient);
                await _unitOfWork.SaveChangesAsync();

                await LoadClientsAsync();
                _logger.LogInformation($"Clientul {clientName} a fost șters cu succes");
                
                MessageBox.Show($"Clientul \"{clientName}\" a fost șters cu succes.",
                               "Ștergere Client", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la ștergerea clientului: {ex.Message}");
                MessageBox.Show($"Eroare la ștergerea clientului: {ex.Message}",
                               "Ștergere Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Adaugă un client la colecție (utilizat din formular)
        /// </summary>
        public async Task AddClientToCollectionAsync(Client client)
        {
            try
            {
                if (client == null)
                    return;

                _logger.LogInformation($"Se adaugă clientul: {client.Nume}");

                await _unitOfWork.ClientRepository.AddAsync(client);
                await _unitOfWork.SaveChangesAsync();

                await LoadClientsAsync();
                _logger.LogInformation($"Clientul {client.Nume} a fost adăugat cu succes");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la salvarea clientului: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualizează clientul (utilizat din formular)
        /// </summary>
        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                if (client == null)
                    return;

                _logger.LogInformation($"Se actualizează clientul: {client.Nume}");

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
