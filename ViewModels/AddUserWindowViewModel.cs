using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupabaseUserManager.Services;
using System.Windows;

namespace SupabaseUserManager.ViewModels
{
    partial class AddUserWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanAddUser))]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        private string username;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanAddUser))]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        private string password;

        public bool CanAddUser => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        public AddUserWindowViewModel()
        {

        }

        [RelayCommand(CanExecute = nameof(CanAddUser))]
        private async Task AddUserAsync()
        {
            try
            {
                string User = Username;
                User += "@gg.com";
                await SupabaseHandler.AddUserAsync(User, Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
