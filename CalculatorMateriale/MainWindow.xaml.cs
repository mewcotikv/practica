using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CalculatorMateriale.Data;
using CalculatorMateriale.Views;

namespace CalculatorMateriale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILogger<MainWindow>? _logger;
        private string _currentPage = "Dashboard";
        private IServiceProvider? _serviceProvider;
        private IUnitOfWork? _unitOfWork;

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _unitOfWork = serviceProvider?.GetService<IUnitOfWork>();
            
            // Store UnitOfWork in Application properties for child views
            Application.Current.Properties["UnitOfWork"] = _unitOfWork;
            
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Try to get logger from DependencyInjection if available
            try
            {
                _logger = Application.Current.Properties["Logger"] as ILogger<MainWindow>;
            }
            catch
            {
                // Logger not available, continue without it
            }

            LogMessage("MainWindow loaded successfully");
            ShowDashboard();
            WireTopBarButtons();
        }

        /// <summary>
        /// Navigate to different pages based on menu selection
        /// </summary>
        private void NavigateToPage(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string pageTag)
            {
                _currentPage = pageTag;
                LogMessage($"Navigating to page: {pageTag}");

                // Clear the dashboard placeholder
                DashboardPlaceholder.Visibility = Visibility.Collapsed;

                switch (pageTag)
                {
                    case "Dashboard":
                        ShowDashboard();
                        break;
                    case "Clienti":
                        ShowClientPage();
                        break;
                    case "Materiale":
                        ShowMaterialPage();
                        break;
                    case "Comenzi":
                        ShowOrderPage();
                        break;
                    case "Calcule":
                        ShowCalculationsPage();
                        break;
                    case "Proiecte":
                        ShowProjectsPage();
                        break;
                    case "DevizExport":
                        ShowExportPage();
                        break;
                    case "Rapoarte":
                        ShowReportsPage();
                        break;
                    case "Stocuri":
                        ShowInventoryPage();
                        break;
                    default:
                        LogMessage($"Unknown page: {pageTag}");
                        break;
                }

                // Update button states (highlight selected button)
                UpdateMenuButtonStates(button);
            }
        }

        /// <summary>
        /// Show the dashboard
        /// </summary>
        private void ShowDashboard()
        {
            DashboardPlaceholder.Visibility = Visibility.Visible;
            ContentFrame.Content = null;
            LogMessage("Dashboard displayed");
        }

        /// <summary>
        /// Show client management page
        /// </summary>
        private void ShowClientPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var clientiView = new ClientiView();
            ContentFrame.Content = clientiView;
            LogMessage("Client page displayed");
        }

        /// <summary>
        /// Show material management page
        /// </summary>
        private void ShowMaterialPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            ContentFrame.Content = new MaterialeView();
            LogMessage("Material page displayed");
        }

        /// <summary>
        /// Show order management page
        /// </summary>
        private void ShowOrderPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var comenziView = new ComenziView();
            ContentFrame.Content = comenziView;
            LogMessage("Order page displayed");
        }

        /// <summary>
        /// Show calculations page
        /// </summary>
        private void ShowCalculationsPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var calculatorView = new CalculatorView();
            ContentFrame.Content = calculatorView;
            LogMessage("Calculations page displayed");
        }

        /// <summary>
        /// Show projects page
        /// </summary>
        private void ShowProjectsPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            ContentFrame.Content = new ObiectiveView();
            LogMessage("Projects page displayed");
        }

        private void ShowExportPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            ContentFrame.Content = new DevizView();
            LogMessage("Quote and export page displayed");
        }

        /// <summary>
        /// Show reports page
        /// </summary>
        private void ShowReportsPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            ContentFrame.Content = new RapoarteView();
            LogMessage("Reports page displayed");
        }

        /// <summary>
        /// Show inventory page
        /// </summary>
        private void ShowInventoryPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            ContentFrame.Content = new StocuriView();
            LogMessage("Inventory page displayed");
        }

        /// <summary>
        /// Export data to Excel format
        /// </summary>
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            LogMessage("Export to Excel initiated");
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            ContentFrame.Content = new RapoarteView();
        }

        /// <summary>
        /// Export data to PDF format
        /// </summary>
        private void ExportToPDF(object sender, RoutedEventArgs e)
        {
            LogMessage("Export to PDF initiated");
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            ContentFrame.Content = new DevizView();
        }

        /// <summary>
        /// Update menu button visual states
        /// </summary>
        private void UpdateMenuButtonStates(Button selectedButton)
        {
            var buttons = new[]
            {
                BtnDashboard,
                BtnClienti,
                BtnMateriale,
                BtnComenzi,
                BtnCalcule,
                BtnProiecte,
                BtnDevizExport
            };

            foreach (var btn in buttons)
            {
                var isSelected = btn == selectedButton;
                btn.Background = isSelected
                    ? new SolidColorBrush(Color.FromRgb(178, 107, 0))
                    : Brushes.Transparent;
                btn.Foreground = isSelected
                    ? Brushes.White
                    : Brushes.White;
            }
        }

        /// <summary>
        /// Log message to console and logger if available
        /// </summary>
        private void LogMessage(string message)
        {
            System.Diagnostics.Debug.WriteLine($"[MainWindow] {message}");
            _logger?.LogInformation($"{message}");
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "RED Construct Calculator\nAplicatie pentru materiale, stocuri, comenzi, calcule si rapoarte.",
                "Despre", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Setarile sunt pregatite pentru extindere. Baza activa: SQLite local.", "Setari",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WireTopBarButtons()
        {
            foreach (var button in FindVisualChildren<Button>(this))
            {
                var content = button.Content?.ToString() ?? string.Empty;
                if (content.Contains("Set"))
                    button.Click += SettingsButton_Click;
                else if (content.Contains("Ie") || content.Contains("Ies"))
                    button.Click += ExitButton_Click;
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
                yield break;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    yield return typedChild;

                foreach (var nestedChild in FindVisualChildren<T>(child))
                    yield return nestedChild;
            }
        }
    }
}

