using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LicenseManager.Component.Services;
using LicenseManager.Component.Stores;
using LicenseManager.Component.ViewModels;
using LicenseManager.Domain.Services;
using System.Windows;

namespace LicenseManager.WPF
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly NavigationService<MainWindowViewModel, ConvalidateLicenseViewModel> _convalidateLicenseNavServ;
        private readonly NavigationService<MainWindowViewModel, ActivationLicenseViewModel> _activationLicenseNavServ;

        public MainWindowViewModel(NavigationService<MainWindowViewModel, ConvalidateLicenseViewModel> convalidateLicenseNavServ,
                                   NavigationService<MainWindowViewModel, ActivationLicenseViewModel> activationLicenseNavServ, 
                                   NavigationStore<MainWindowViewModel> navigationStore)
        {
            _convalidateLicenseNavServ = convalidateLicenseNavServ;
            _activationLicenseNavServ = activationLicenseNavServ;

            this.navigationStore = navigationStore;
        }

        [ObservableProperty]
        private NavigationStore<MainWindowViewModel> navigationStore;

        public bool EnableConvalidate { get; set; } = true;

        public Style ConvalidateStyle => SelectStyle<ConvalidateLicenseViewModel>();

        [RelayCommand]
        private void GoToConvalidate()
        {
            _convalidateLicenseNavServ.Navigate();

            OnPropertyChanged(nameof(ConvalidateStyle));
            OnPropertyChanged(nameof(ActivationStyle));
        }

        public bool EnableActivation { get; set; } = false;

        public Style ActivationStyle => SelectStyle<ActivationLicenseViewModel>();

        [RelayCommand]
        private void GoToActivation()
        {
            _activationLicenseNavServ.Navigate();

            OnPropertyChanged(nameof(ConvalidateStyle));
            OnPropertyChanged(nameof(ActivationStyle));
        }

        private Style SelectStyle<T>()
        {
            return NavigationStore.CurrentViewModel is T
                   ? App.GetStyle("NavigationButtonSelected")
                   : App.GetStyle("NavigationButton");
        }
    }
}
