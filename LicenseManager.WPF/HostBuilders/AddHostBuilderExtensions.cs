using LicenseManager.Component.Services;
using LicenseManager.Component.Stores;
using LicenseManager.Component.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LicenseManager.WPF.HostBuilder
{
    public static class AddHostBuilderExtensions
    {
        public static IHostBuilder AddMainServices(this IHostBuilder hostBuilder)
        {
            _ = hostBuilder.ConfigureServices(services =>
              {
                  _ = services.AddSingleton<MainWindowViewModel>();
                  _ = services.AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainWindowViewModel>() });
              });

            return hostBuilder;
        }

        public static IHostBuilder AddNavigationServices(this IHostBuilder hostBuilder)
        {
            _ = hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationStore<MainWindowViewModel>>();

                _ = services.AddSingleton((s) => new NavigationService<MainWindowViewModel, ConvalidateLicenseViewModel>(
                        s.GetRequiredService<NavigationStore<MainWindowViewModel>>(), () => new ConvalidateLicenseViewModel()));

                _ = services.AddSingleton((s) => new NavigationService<MainWindowViewModel, ActivationLicenseViewModel>(
                        s.GetRequiredService<NavigationStore<MainWindowViewModel>>(), () => new ActivationLicenseViewModel()));
            });

            return hostBuilder;
        }
    }
}
