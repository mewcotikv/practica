using System.Windows;
using REDConstruct.Views;

namespace REDConstruct
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentFrame.Navigate(new RapoarteView());
        }
    }
}
