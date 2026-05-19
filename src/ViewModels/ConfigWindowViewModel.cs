using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupabaseUserManager.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SupabaseUserManager.ViewModels
{
    partial class ConfigWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveConfigCommand))]
        private string url = Properties.Settings.Default.URL;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveConfigCommand))]
        private string key = Properties.Settings.Default.SecretKey;

        [RelayCommand(CanExecute = nameof(ValidateConfig))]
        private void SaveConfig()
        {
            Properties.Settings.Default.SecretKey = key;
            Properties.Settings.Default.URL = url;
            SupabaseHandler.ReloadSettings();
            Properties.Settings.Default.Save();
            MessageBox.Show("Configuration saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool ValidateConfig()
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            return true;
        }
    }
}
