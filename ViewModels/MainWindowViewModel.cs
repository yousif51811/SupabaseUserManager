using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupabaseUserManager.ViewModels
{
    partial class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            
        }

        [RelayCommand]
        public void OpenConfig()
        {
            var configWindow = new Views.ConfigWindowView();
            configWindow.ShowDialog();
        }
    }
}
