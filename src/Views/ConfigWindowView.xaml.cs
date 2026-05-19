using System.Windows;

namespace SupabaseUserManager.Views
{
    public partial class ConfigWindowView : Window
    {
        public ConfigWindowView()
        {
            InitializeComponent();
            DataContext = new ViewModels.ConfigWindowViewModel();
        }
    }
}
