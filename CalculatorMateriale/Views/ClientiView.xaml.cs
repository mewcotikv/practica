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

            var formData = ShowClientDialog("Adaugă client");
            if (formData == null)
                return;

            var client = new Client
            {
                Nume = formData["Nume"].Trim(),
                CUI = formData["CUI"].Trim(),
                Adresa = ToNullable(formData["Adresa"]),
                Localitate = ToNullable(formData["Localitate"]),
                CodPostal = ToNullable(formData["CodPostal"]),
                Telefon = formData["Telefon"].Trim(),
                Email = ToNullable(formData["Email"])
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

            var initialData = new System.Collections.Generic.Dictionary<string, string>
            {
                { "Nume", client.Nume },
                { "CUI", client.CUI },
                { "Adresa", client.Adresa ?? string.Empty },
                { "Localitate", client.Localitate ?? string.Empty },
                { "CodPostal", client.CodPostal ?? string.Empty },
                { "Telefon", client.Telefon ?? string.Empty },
                { "Email", client.Email ?? string.Empty }
            };

            var formData = ShowClientDialog("Editează client", initialData);
            if (formData == null)
                return;

            client.Nume = formData["Nume"].Trim();
            client.CUI = formData["CUI"].Trim();
            client.Adresa = ToNullable(formData["Adresa"]);
            client.Localitate = ToNullable(formData["Localitate"]);
            client.CodPostal = ToNullable(formData["CodPostal"]);
            client.Telefon = formData["Telefon"].Trim();
            client.Email = ToNullable(formData["Email"]);

            _unitOfWork.ClientRepository.Update(client);
            await _unitOfWork.SaveChangesAsync();
            await LoadClientsAsync();
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
                                     (c.Telefon ?? string.Empty).ToLowerInvariant().Contains(search) ||
                                     (c.Localitate ?? string.Empty).ToLowerInvariant().Contains(search));

            _clienti = new ObservableCollection<Client>(filtered.OrderBy(c => c.Nume));
            ClientiDataGrid.DataContext = _clienti;
            TotalClientsLabel.Text = _clienti.Count.ToString();
        }

        private System.Collections.Generic.Dictionary<string, string>? ShowClientDialog(
            string title,
            System.Collections.Generic.Dictionary<string, string>? initialData = null)
        {
            var form = new ClientFormView();
            if (initialData != null)
                form.SetFormData(initialData);

            var dialog = new Window
            {
                Title = title,
                Content = form,
                Owner = Window.GetWindow(this),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };

            form.SaveRequested += (_, _) => dialog.DialogResult = true;
            form.CancelRequested += (_, _) => dialog.DialogResult = false;

            return dialog.ShowDialog() == true ? form.GetFormData() : null;
        }

        private static string? ToNullable(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}

