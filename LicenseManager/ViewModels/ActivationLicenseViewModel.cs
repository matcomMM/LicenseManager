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
    public class ActivationLicenseViewModel : ViewModelBase
    {
        //public ConvalidateLicenseViewModel(NavigationService<RuntimeViewModel> runtimeNavigationService)
        public ActivationLicenseViewModel(NavigationService<ConvalidateLicenseViewModel> convalidateLicenseViewModel)
        {
            ImportCommand = new RelayCommand<object>(Import);
            ExportCommand = new RelayCommand<object>(Export);

            ConvalidateLicenseNavigateCommand = new NavigateCommand(convalidateLicenseViewModel);
        }

        public LicenseViewModel LicenseViewModel { get; }

        private void Import(object obj)
        {
            throw new NotImplementedException();
        }

        private void Export(object obj)
        {
            throw new NotImplementedException();
        }

        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand ConvalidateLicenseNavigateCommand { get; }
    }
}
