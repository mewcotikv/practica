using System.Windows;

namespace CalculatorMateriale
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int percentage, string message)
        {
            LoadingBar.Value = percentage;
            StatusText.Text = message;
        }

        public void Show(string message)
        {
            StatusText.Text = message;
            this.Show();
        }
    }
}
