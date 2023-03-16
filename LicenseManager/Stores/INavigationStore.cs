using LicenseManager.ViewModels;

namespace LicenseManager.Stores
{
    public interface INavigationStore
    {
        ViewModelBase CurrentViewModel { set; }
    }
}