using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupabaseUserManager.Models;
using SupabaseUserManager.Services;
using SupabaseUserManager.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;

namespace SupabaseUserManager.ViewModels
{
    partial class UserListingViewModel : ObservableObject
    {
        // An observable collection of all the users retrieved from the Supabase database.
        // This collection is bound to the UI to display the list of users.
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSelected))]
        public partial User SelectedUser { get; set; }
        public UserListingViewModel()
        {
            _ = LoadUsersAsync();
        }

        public bool IsSelected => SelectedUser != null;

        /// <summary>
        /// Load all users from the Supabase database and populate the Users collection.
        /// This method is called when the ViewModel is initialized to ensure that the user list is up-to-date when the UI is displayed.
        /// </summary>
        [RelayCommand]
        private async Task LoadUsersAsync()
        {
            try
            {


                Users.Clear();
                var users = await SupabaseHandler.GetAllUsersAsync();

                foreach (var user in users)
                {
                    Users.Add(new User
                    {
                        UID = user.Id,
                        Display_Name = user.Email.Split('@')[0],
                        Email = user.Email,
                        CreatedAt = user.CreatedAt.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            /// <summary>
            /// Deletes the currently selected user after confirming the action with the user.
            /// </summary>
            [RelayCommand]
            async Task DeleteUserAsync()
            {
                if (SelectedUser == null)
                    return;
                var result = MessageBox.Show($"Are you sure you want to delete user '{SelectedUser.Email}'?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    bool success = await SupabaseHandler.DeleteUserAsync(SelectedUser.UID);
                    if (success)
                    {
                        Users.Remove(SelectedUser);
                        SelectedUser = null;
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the user. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            /// <summary>
            /// Launch the adduserwindow view
            /// </summary>
            [RelayCommand]
            async Task LaunchAddUser()
            {
                var addUserWindow = new AddUserWindowView();
                addUserWindow.ShowDialog();
                await LoadUsersAsync();
            }

        }
    }
}
