using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LicenseManager.Domain;
using LicenseManager.Domain.Models;
using System.Windows;

namespace LicenseManager.Component.ViewModels
{
    public partial class ConvalidateLicenseViewModel : ObservableObject
    {
        private string _licensePath = string.Empty;

        public ConvalidateLicenseViewModel() { }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConvalidateCommand))]
        [NotifyCanExecuteChangedFor(nameof(SaveLicenseCommand))]
        private LicenseViewModel? licenseVM;

        [RelayCommand]
        private void LoadLicense()
        {
            License? license = LicenseFunction.OpenLicenseFromFile(out _licensePath);
            if (license != null)
            {
                LicenseVM = new(license);
            }
        }

        [RelayCommand(CanExecute = nameof(CanConvalidate))]
        private void Convalidate()
        {
            if (LicenseVM != null)
            {
                LicenseVM.SerialKey = LicenseKey.GenerateSerialKey(LicenseVM.SerialNumber, LicenseVM.Product);
            }
        }

        private bool CanConvalidate()
        {
            return LicenseVM != null;
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private void SaveLicense()
        {
            if (LicenseVM != null)
            {
                string error = string.Empty;

                bool saved = LicenseFunction.SaveLicenseFile(LicenseVM.License, out error);

                if (saved)
                {
                    MessageBox.Show("License saved, send back to customer the file", "License convalidated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        _ = MessageBox.Show($"License is NOT convalidate, retry\n\n{error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private bool CanSave()
        {
            return LicenseVM != null;
        }
    }
}
