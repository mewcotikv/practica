using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.Views
{
    /// <summary>
    /// Interaction logic for ClientiView.xaml
    /// </summary>
    public partial class ClientiView : UserControl
    {
        private IUnitOfWork _unitOfWork;
        private ObservableCollection<Client> _clienti = new ObservableCollection<Client>();

        public ClientiView()
        {
            InitializeComponent();
            
            // Set up event handlers
            this.Loaded += ClientiView_Loaded;
            ClientiDataGrid.SelectionChanged += ClientiDataGrid_SelectionChanged;
        }

        /// <summary>
        /// Initialize the view when loaded
        /// </summary>
        private void ClientiView_Loaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ClientiView loaded");
            _unitOfWork = Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
            _ = LoadClientsAsync();
        }

        /// <summary>
        /// Handle DataGrid selection changes
        /// </summary>
        private void ClientiDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientiDataGrid.SelectedItem != null)
            {
                // Update UI to show selected client details
                SelectedClientLabel.Text = "1 selectat";
            }
            else
            {
                SelectedClientLabel.Text = "Niciun";
            }
        }

        private async System.Threading.Tasks.Task LoadClientsAsync()
        {
            if (_unitOfWork == null)
                return;

            var clients = await _unitOfWork.ClientRepository.GetAllAsync();
            _clienti = new ObservableCollection<Client>(clients.OrderBy(c => c.Nume));
            ClientiDataGrid.DataContext = _clienti;
            TotalClientsLabel.Text = _clienti.Count.ToString();
        }

        private async void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null)
                return;

            var next = _clienti.Count + 1;
            var client = new Client
            {
                Nume = $"Client nou {next}",
                CUI = $"{10000000 + next}{_clienti.Count}",
                Localitate = "Chisinau",
                Telefon = "+373 22 000 000",
                Email = $"client{next}@example.md"
            };

            await _unitOfWork.ClientRepository.AddAsync(client);
            await _unitOfWork.SaveChangesAsync();
            await LoadClientsAsync();
        }

        private async void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || ClientiDataGrid.SelectedItem is not Client client)
            {
                MessageBox.Show("Selectati un client pentru editare.", "Editare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            client.Activ = !client.Activ;
            _unitOfWork.ClientRepository.Update(client);
            await _unitOfWork.SaveChangesAsync();
            ClientiDataGrid.Items.Refresh();
        }

        private async void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null || ClientiDataGrid.SelectedItem is not Client client)
            {
                MessageBox.Show("Selectati un client pentru stergere.", "Stergere", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Stergeti clientul {client.Nume}?", "Confirmare", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            try
            {
                _unitOfWork.ClientRepository.Delete(client);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                client.Activ = false;
                _unitOfWork.ClientRepository.Update(client);
                await _unitOfWork.SaveChangesAsync();
                MessageBox.Show("Clientul are date legate si a fost dezactivat in loc sa fie sters.", "Stergere", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            await LoadClientsAsync();
        }

        private async void SearchClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (_unitOfWork == null)
                return;

            var search = SearchTextBox.Text?.Trim().ToLowerInvariant();
            var clients = await _unitOfWork.ClientRepository.GetAllAsync();
            var filtered = string.IsNullOrWhiteSpace(search)
                ? clients
                : clients.Where(c => c.Nume.ToLowerInvariant().Contains(search) ||
                                     c.CUI.ToLowerInvariant().Contains(search) ||
                                     (c.Localitate ?? string.Empty).ToLowerInvariant().Contains(search));

            _clienti = new ObservableCollection<Client>(filtered.OrderBy(c => c.Nume));
            ClientiDataGrid.DataContext = _clienti;
            TotalClientsLabel.Text = _clienti.Count.ToString();
        }
    }
}

