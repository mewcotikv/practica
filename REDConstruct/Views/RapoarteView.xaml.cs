using System.Windows.Controls;
using REDConstruct.ViewModels;

namespace REDConstruct.Views
{
    public partial class RapoarteView : Page
    {
        public RapoarteView()
        {
            InitializeComponent();
            DataContext = new RapoarteViewModel();
        }
    }
}
