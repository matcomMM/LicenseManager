using LicenseManager.Commands;
using LicenseManager.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LicenseManager.ViewModels
{
    public class ConvalidateLicenseViewModel : ViewModelBase
    {
        public ConvalidateLicenseViewModel(NavigationService<ActivationLicenseViewModel> activationLicenseNavigationService)
        {
            LoadLicenseCommand = new RelayCommand<object>(LoadLicense);
            ConvalidateCommand = new RelayCommand<object>(Convalidate);

            ActivationLicenseNavigateCommand = new NavigateCommand(activationLicenseNavigationService);
        }

        public LicenseViewModel LicenseViewModel { get; }

        private void LoadLicense(object obj)
        {
            throw new NotImplementedException();
        }

        private void Convalidate(object obj)
        {
            throw new NotImplementedException();
        }

        public ICommand LoadLicenseCommand { get; }
        public ICommand ConvalidateCommand { get; }
        public ICommand ActivationLicenseNavigateCommand { get; }
    }
}
