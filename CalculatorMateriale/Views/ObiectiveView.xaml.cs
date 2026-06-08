using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.Views
{
    public partial class ObiectiveView : UserControl
    {
        private IUnitOfWork? _unitOfWork;
        private ObservableCollection<Obiectiv> _obiective = new ObservableCollection<Obiectiv>();

        public ObiectiveView()
        {
            InitializeComponent();
            Loaded += ObiectiveView_Loaded;
        }

        private async void ObiectiveView_Loaded(object sender, RoutedEventArgs e)
        {
            _unitOfWork = Application.Current.Properties["UnitOfWork"] as IUnitOfWork;
            if (_unitOfWork == null)
            {
                ValidationText.Text = "Serviciile bazei de date nu sunt initializate.";
                return;
            }

            await LoadClientsAsync();
            await LoadObiectiveAsync();
        }

        private async System.Threading.Tasks.Task LoadClientsAsync()
        {
            if (_unitOfWork == null)
                return;

            var clients = (await _unitOfWork.ClientRepository.GetAllAsync()).OrderBy(c => c.Nume).ToList();
            if (!clients.Any())
            {
                var client = new Client { Nume = "RED Construct SRL", CUI = "10000001", Localitate = "Chisinau" };
                await _unitOfWork.ClientRepository.AddAsync(client);
                await _unitOfWork.SaveChangesAsync();
                clients.Add(client);
            }

            ClientCombo.ItemsSource = clients;
            ClientCombo.SelectedIndex = 0;
        }

        private async System.Threading.Tasks.Task LoadObiectiveAsync()
        {
            if (_unitOfWork == null)
                return;

            var obiective = await _unitOfWork.ObiectivRepository.GetAllAsync();
            _obiective = new ObservableCollection<Obiectiv>(obiective.OrderByDescending(o => o.DataCrearii));
            ObiectiveGrid.ItemsSource = _obiective;
        }

        private bool TryReadForm(out Obiectiv obiectiv)
        {
            obiectiv = null;
            ValidationText.Text = string.Empty;

            if (ClientCombo.SelectedItem is not Client client)
            {
                ValidationText.Text = "Selectati un client.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(DenumireInput.Text))
            {
                ValidationText.Text = "Denumirea obiectivului este obligatorie.";
                return false;
            }

            if (!TryReadDecimal(SuprafataInput.Text, out var suprafata) || suprafata <= 0)
            {
                ValidationText.Text = "Suprafata trebuie sa fie mai mare decat 0.";
                return false;
            }

            obiectiv = new Obiectiv
            {
                Denumire = DenumireInput.Text.Trim(),
                IdClient = client.IdClient,
                SuprafataM2 = suprafata,
                Descriere = DenumireInput.Text.Trim(),
                Adresa = string.IsNullOrWhiteSpace(LocalitateInput.Text) ? "Nespecificat" : LocalitateInput.Text.Trim(),
                Localitate = LocalitateInput.Text.Trim(),
                Status = "Activ",
                DataCrearii = DateTime.Now
            };
            return true;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_unitOfWork == null || !TryReadForm(out var obiectiv))
                    return;

                await _unitOfWork.ObiectivRepository.AddAsync(obiectiv);
                await _unitOfWork.SaveChangesAsync();
                await LoadObiectiveAsync();
                ValidationText.Text = "Proiectul a fost adăugat.";
            }
            catch (Exception ex)
            {
                ValidationText.Text = $"Eroare la adăugare proiect: {ex.Message}";
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_unitOfWork == null || ObiectiveGrid.SelectedItem is not Obiectiv selected || !TryReadForm(out var form))
                    return;

                selected.Denumire = form.Denumire;
                selected.IdClient = form.IdClient;
                selected.SuprafataM2 = form.SuprafataM2;
                selected.Descriere = form.Descriere;
                selected.Adresa = form.Adresa;
                selected.Localitate = form.Localitate;
                _unitOfWork.ObiectivRepository.Update(selected);
                await _unitOfWork.SaveChangesAsync();
                await LoadObiectiveAsync();
                ValidationText.Text = "Proiectul a fost actualizat.";
            }
            catch (Exception ex)
            {
                ValidationText.Text = $"Eroare la actualizare proiect: {ex.Message}";
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_unitOfWork == null || ObiectiveGrid.SelectedItem is not Obiectiv selected)
                {
                    ValidationText.Text = "Selectati un obiectiv pentru stergere.";
                    return;
                }

                _unitOfWork.ObiectivRepository.Delete(selected);
                await _unitOfWork.SaveChangesAsync();
                await LoadObiectiveAsync();
                ValidationText.Text = "Proiectul a fost șters.";
            }
            catch (Exception ex)
            {
                ValidationText.Text = $"Eroare la ștergere proiect: {ex.Message}";
            }
        }

        private void ObiectiveGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ObiectiveGrid.SelectedItem is not Obiectiv selected)
                return;

            DenumireInput.Text = selected.Denumire;
            SuprafataInput.Text = selected.SuprafataM2.ToString("0.##");
            LocalitateInput.Text = selected.Localitate ?? string.Empty;
            ClientCombo.SelectedValue = selected.IdClient;
        }

        private static bool TryReadDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result)
                || decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }
    }
}
