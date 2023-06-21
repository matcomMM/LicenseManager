using CommunityToolkit.Mvvm.ComponentModel;
using LicenseManager.Component.Stores;
using LicenseManager.Domain.Services;
using System;

namespace LicenseManager.Component.Services
{
    public class NavigationService<TNavigationStore, TViewModel> : INavigationService<TNavigationStore, TViewModel> where TViewModel : ObservableObject
    {
        private readonly NavigationStore<TNavigationStore> _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore<TNavigationStore> navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
