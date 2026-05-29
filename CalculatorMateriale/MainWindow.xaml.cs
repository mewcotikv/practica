using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.Logging;

namespace CalculatorMateriale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILogger<MainWindow>? _logger;
        private string _currentPage = "Dashboard";

        public MainWindow()
        {
            InitializeComponent();
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
            var content = new TextBlock
            {
                Text = "📋 Pagina Clienți\n\nFuncționalitate: Gestionare clienți (CRUD)\n- Adăugare clienți\n- Editare date\n- Ștergere\n- Căutare după CUI sau nume",
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 14,
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap
            };
            ContentFrame.Content = content;
            LogMessage("Client page displayed");
        }

        /// <summary>
        /// Show material management page
        /// </summary>
        private void ShowMaterialPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var content = new TextBlock
            {
                Text = "📦 Pagina Materiale\n\nFuncționalitate: Gestionare materiale termoizolante\n- Adăugare materiale\n- Configurare proprietăți izolante\n- Gestionare prețuri\n- Monitorizare stoc",
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 14,
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap
            };
            ContentFrame.Content = content;
            LogMessage("Material page displayed");
        }

        /// <summary>
        /// Show order management page
        /// </summary>
        private void ShowOrderPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var content = new TextBlock
            {
                Text = "📋 Pagina Comenzi\n\nFuncționalitate: Gestionare comenzi\n- Creare comenzi noi\n- Editare comenzi\n- Urmărire stare\n- Calcul valoare\n- Generare facturi",
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 14,
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap
            };
            ContentFrame.Content = content;
            LogMessage("Order page displayed");
        }

        /// <summary>
        /// Show calculations page
        /// </summary>
        private void ShowCalculationsPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var content = new TextBlock
            {
                Text = "🧮 Pagina Calcule\n\nFuncționalitate: Calcule consum materiale\n- Calculator consum pe bază de suprafață\n- Calcul densitate/greutate\n- Estimare costuri\n- Tabele cu coeficienți izolație",
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 14,
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap
            };
            ContentFrame.Content = content;
            LogMessage("Calculations page displayed");
        }

        /// <summary>
        /// Show projects page
        /// </summary>
        private void ShowProjectsPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var content = new TextBlock
            {
                Text = "🏗️ Pagina Proiecte\n\nFuncționalitate: Gestionare proiecte/obiective\n- Creare proiecte\n- Asociere la clienți\n- Urmărire progres\n- Linkare cu comenzi",
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 14,
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap
            };
            ContentFrame.Content = content;
            LogMessage("Projects page displayed");
        }

        /// <summary>
        /// Show reports page
        /// </summary>
        private void ShowReportsPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var content = new TextBlock
            {
                Text = "📈 Pagina Rapoarte Vânzări\n\nFuncționalitate: Analiza și rapoarte\n- Rapoarte pe perioadă\n- Top materiale vândute\n- Analiză revenue\n- Grafice și statistici",
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 14,
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap
            };
            ContentFrame.Content = content;
            LogMessage("Reports page displayed");
        }

        /// <summary>
        /// Show inventory page
        /// </summary>
        private void ShowInventoryPage()
        {
            DashboardPlaceholder.Visibility = Visibility.Collapsed;
            var content = new TextBlock
            {
                Text = "📉 Pagina Stocuri\n\nFuncționalitate: Monitorizare stocuri\n- Nivel stoc curent\n- Alerte stoc minim\n- Istoric mișcări stoc\n- Recomandări reaprovizionare",
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 14,
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap
            };
            ContentFrame.Content = content;
            LogMessage("Inventory page displayed");
        }

        /// <summary>
        /// Export data to Excel format
        /// </summary>
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            LogMessage("Export to Excel initiated");
            MessageBox.Show(
                "Funcția Export Excel va fi implementată.\n\nVa folosi NPOI pentru a genera fișiere .xlsx din datele curente.",
                "Export Excel",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Export data to PDF format
        /// </summary>
        private void ExportToPDF(object sender, RoutedEventArgs e)
        {
            LogMessage("Export to PDF initiated");
            MessageBox.Show(
                "Funcția Export PDF va fi implementată.\n\nVa folosi QuestPDF pentru a genera documente PDF profesionale.",
                "Export PDF",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Update menu button visual states
        /// </summary>
        private void UpdateMenuButtonStates(Button selectedButton)
        {
            // Find all buttons in the sidebar
            var buttons = FindLogicalChildren<Button>(this);
            foreach (var btn in buttons)
            {
                if (btn.Style?.TargetType.Name == "Button" && 
                    (string?)btn.Tag is string tag && 
                    tag != "Rapoarte" && tag != "Stocuri") // Skip export buttons
                {
                    // Could add selection highlight here if needed
                }
            }
        }

        /// <summary>
        /// Helper method to find all logical children of a specific type
        /// </summary>
        private IEnumerable<T> FindLogicalChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
                yield break;

            var children = LogicalTreeHelper.GetChildren(parent);
            foreach (var child in children)
            {
                if (child is T typedChild)
                    yield return typedChild;

                // Only recurse if child is a DependencyObject to avoid infinite recursion
                if (child is DependencyObject depObj)
                {
                    foreach (var grandChild in FindLogicalChildren<T>(depObj))
                        yield return grandChild;
                }
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
    }
}
