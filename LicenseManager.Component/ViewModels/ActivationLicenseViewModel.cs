using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LicenseManager.Domain;
using LicenseManager.Domain.Models;
using LicenseManager.Domain.Services;

namespace LicenseManager.Component.ViewModels
{
    public partial class ActivationLicenseViewModel : ObservableObject
    {

        public ActivationLicenseViewModel()
        {
            licenseVM = new(new License());
        }

        [ObservableProperty]
        private LicenseViewModel licenseVM;

        [RelayCommand]
        private void Import()
        {
            LicenseFunction.OpenLicenseFromFile(out _);
        }   

        [RelayCommand]
        private void Export()
        {
            LicenseVM.SerialNumber = LicenseKey.SerialNumber;

            LicenseFunction.SaveLicenseFile(LicenseVM.License);
        }
    }
}
