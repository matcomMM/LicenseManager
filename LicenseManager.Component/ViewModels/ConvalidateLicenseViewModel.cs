using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LicenseManager.Domain;
using LicenseManager.Domain.Models;
using LicenseManager.Domain.Services;
using System.Windows;

namespace LicenseManager.Component.ViewModels
{
    public partial class ConvalidateLicenseViewModel : ObservableObject
    {
        private string _licensePath = string.Empty;

        public ConvalidateLicenseViewModel()
        {
            licenseVM = new(new License());
        }

        [ObservableProperty]
        private LicenseViewModel licenseVM;

        [RelayCommand]
        private void LoadLicense()
        {
            License? license = LicenseFunction.OpenLicenseFromFile(out _licensePath);
            if (license != null)
            {
                LicenseVM = new(license);
            }
        }

        [RelayCommand]
        private void Convalidate()
        {
            LicenseVM.SerialKey = LicenseKey.GenerateSerialKey(LicenseVM.SerialNumber);
            OnPropertyChanged(nameof(LicenseVM));
        }

        [RelayCommand]
        private void SaveLicense()
        {
            bool saved = LicenseFunction.SaveLicenseFile(LicenseVM.License, _licensePath);

            if (saved)
            {
                MessageBox.Show("License saved, send back to customer the file", "License convalidated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("License is NOT convalidate, retry", "License NOT convalidated", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
