namespace LicenseManager.Domain.Services
{
    public interface INavigationService<TNavigationStore, TViewModel>
    {
        void Navigate();
    }
}