using SupabaseUserManager.ViewModels;
using System.Windows.Controls;

namespace SupabaseUserManager.Views
{
    /// <summary>
    /// Interaction logic for UserListingView.xaml
    /// </summary>
    public partial class UserListingView : UserControl
    {
        public UserListingView()
        {
            InitializeComponent();
            DataContext = new UserListingViewModel();
        }
    }
}
