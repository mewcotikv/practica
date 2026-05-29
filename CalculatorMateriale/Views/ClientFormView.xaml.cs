using System.Windows;
using System.Windows.Controls;

namespace CalculatorMateriale.Views
{
    /// <summary>
    /// Interaction logic for ClientFormView.xaml
    /// </summary>
    public partial class ClientFormView : UserControl
    {
        public ClientFormView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle Salvează (Save) button click
        /// </summary>
        private void SalveazaButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate form fields
            bool isValid = ValidateForm();

            if (isValid)
            {
                System.Diagnostics.Debug.WriteLine("Form saved successfully!");
                MessageBox.Show(
                    "Datele clientului au fost salvate cu succes.",
                    "Salvare",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                ClearForm();
            }
            else
            {
                MessageBox.Show(
                    "Vă rugăm să completați toate câmpurile obligatorii marcate cu *",
                    "Validare Formular",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Validate form fields
        /// </summary>
        private bool ValidateForm()
        {
            bool isValid = true;

            // Validate Nume (Name)
            if (string.IsNullOrWhiteSpace(NumeTextBox.Text))
            {
                NumeErrorLabel.Text = "Denumirea companiei este obligatorie";
                NumeErrorLabel.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                NumeErrorLabel.Visibility = Visibility.Collapsed;
            }

            // Validate CUI (Tax Code)
            if (string.IsNullOrWhiteSpace(CUITextBox.Text))
            {
                CUIErrorLabel.Text = "CUI-ul este obligatoriu";
                CUIErrorLabel.Visibility = Visibility.Visible;
                isValid = false;
            }
            else if (CUITextBox.Text.Length < 6)
            {
                CUIErrorLabel.Text = "CUI-ul trebuie să aibă cel puțin 6 caractere";
                CUIErrorLabel.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                CUIErrorLabel.Visibility = Visibility.Collapsed;
            }

            return isValid;
        }

        /// <summary>
        /// Clear all form fields
        /// </summary>
        private void ClearForm()
        {
            NumeTextBox.Clear();
            CUITextBox.Clear();
            AdresaTextBox.Clear();
            LocalitateTextBox.Clear();
            CodPostalTextBox.Clear();
            TelefonTextBox.Clear();
            EmailTextBox.Clear();

            // Clear error messages
            NumeErrorLabel.Visibility = Visibility.Collapsed;
            CUIErrorLabel.Visibility = Visibility.Collapsed;

            // Focus on first field
            NumeTextBox.Focus();
        }

        /// <summary>
        /// Get form data as dictionary
        /// </summary>
        public System.Collections.Generic.Dictionary<string, string> GetFormData()
        {
            return new System.Collections.Generic.Dictionary<string, string>
            {
                { "Nume", NumeTextBox.Text },
                { "CUI", CUITextBox.Text },
                { "Adresa", AdresaTextBox.Text },
                { "Localitate", LocalitateTextBox.Text },
                { "CodPostal", CodPostalTextBox.Text },
                { "Telefon", TelefonTextBox.Text },
                { "Email", EmailTextBox.Text }
            };
        }

        /// <summary>
        /// Populate form with data
        /// </summary>
        public void SetFormData(System.Collections.Generic.Dictionary<string, string> data)
        {
            if (data.ContainsKey("Nume")) NumeTextBox.Text = data["Nume"];
            if (data.ContainsKey("CUI")) CUITextBox.Text = data["CUI"];
            if (data.ContainsKey("Adresa")) AdresaTextBox.Text = data["Adresa"];
            if (data.ContainsKey("Localitate")) LocalitateTextBox.Text = data["Localitate"];
            if (data.ContainsKey("CodPostal")) CodPostalTextBox.Text = data["CodPostal"];
            if (data.ContainsKey("Telefon")) TelefonTextBox.Text = data["Telefon"];
            if (data.ContainsKey("Email")) EmailTextBox.Text = data["Email"];
        }
    }
}
