using System.Windows;
using System.Windows.Controls;

namespace CalculatorMateriale.Views
{
    /// <summary>
    /// Interaction logic for ClientiView.xaml
    /// </summary>
    public partial class ClientiView : UserControl
    {
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
            // TODO: Bind data from ViewModel when available
        }

        /// <summary>
        /// Handle DataGrid selection changes
        /// </summary>
        private void ClientiDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientiDataGrid.SelectedItem != null)
            {
                // TODO: Update UI to show selected client details
                SelectedClientLabel.Text = "1 selectat";
            }
            else
            {
                SelectedClientLabel.Text = "Niciun";
            }
        }
    }
}
