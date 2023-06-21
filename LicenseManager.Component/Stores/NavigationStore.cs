using CommunityToolkit.Mvvm.ComponentModel;

namespace LicenseManager.Component.Stores
{
    /// <summary>
    /// Create Navigation store where contain the current view model
    /// </summary>
    /// <typeparam name="TStore">Type of navigation store</typeparam>
    public partial class NavigationStore<TStore> : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject? _currentViewModel;
    }
}
