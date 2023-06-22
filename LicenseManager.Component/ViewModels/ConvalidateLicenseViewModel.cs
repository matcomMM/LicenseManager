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

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string error = string.Empty;

        public bool HasError => !string.IsNullOrEmpty(Error);

        [RelayCommand]
        private void LoadLicense()
        {
            License? license = LicenseFunction.OpenLicenseFromFile(out _licensePath, out string _error);

            if (license != null)
            {
                LicenseVM = new(license);
            }
            else
            {
                Error = _error;
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
                bool saved = LicenseFunction.SaveLicenseFile(LicenseVM.License, out string _error);

                if (saved)
                {
                    MessageBox.Show("License saved, send back to customer the file", "License convalidated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (!string.IsNullOrEmpty(_error))
                    {
                        _ = MessageBox.Show($"License is NOT convalidate, retry\n\n{_error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
