using Supabase;
using Supabase.Gotrue;
using System.Threading.Tasks;
using System.Windows;

namespace SupabaseUserManager.Services
{
    internal static class SupabaseHandler
    {
        private static string SupabaseUrl = Properties.Settings.Default.URL;
        private static string SupabaseKey = Properties.Settings.Default.SecretKey;

        // Declare the supabase client.
        private static Supabase.Client Client { get; set; }

        static SupabaseHandler() => InitializeAsync().Wait();

        public static async Task InitializeAsync()
        {
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = false
            };

            Client = new Supabase.Client(SupabaseUrl, SupabaseKey, options);

            await Client.InitializeAsync().ConfigureAwait(false);
        }


        #region User Management
        public static async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var response = await Client.AdminAuth(SupabaseKey).ListUsers().ConfigureAwait(false);
                return response.Users;
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Check your configuration!\n {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public static async Task<bool> DeleteUserAsync(string id)
        {
            var response = await Client.AdminAuth(SupabaseKey).DeleteUser(id).ConfigureAwait(false);
            return response;
        }
        public static async Task AddUserAsync(string email, string password)
        {
            try
            {
                AdminUserAttributes userAttributes = new AdminUserAttributes
                {
                    Email = email,
                    Password = password,
                    EmailConfirm = true,
                };
                await Client.AdminAuth(SupabaseKey).CreateUser(userAttributes).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user!\n {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion


        /// <summary>
        /// Method to Reload settings.
        /// </summary>
        public static void ReloadSettings()
        {
            SupabaseUrl = Properties.Settings.Default.URL;
            SupabaseKey = Properties.Settings.Default.SecretKey;
            InitializeAsync().Wait();
        }
    }
}