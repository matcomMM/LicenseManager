using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseManager;
using LicenseManager.Services;
using LicenseManager.Stores;
using LicenseManager.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LicenseManager.HostBuilder
{
    public static class AddHostBuilderExtensions
    {
        public static IHostBuilder AddMainServices(this IHostBuilder hostBuilder)
        {
            _ = hostBuilder.ConfigureServices(services =>
              {
                  _ = services.AddSingleton<MainViewModel>();
                  _ = services.AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });
              });

            return hostBuilder;
        }

        public static IHostBuilder AddNavigationServices(this IHostBuilder hostBuilder)
        {
            _ = hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<ModalNavigationStore>();

                _ = services.AddSingleton((s) => new NavigationService<ConvalidateLicenseViewModel>(
                        s.GetRequiredService<NavigationStore>(),
                        () => new ConvalidateLicenseViewModel(s.GetRequiredService<NavigationService<ActivationLicenseViewModel>>())));

                _ = services.AddSingleton((s) => new NavigationService<ActivationLicenseViewModel>(
                        s.GetRequiredService<NavigationStore>(),
                        () => new ActivationLicenseViewModel(s.GetRequiredService<NavigationService<ConvalidateLicenseViewModel>>())));

            });

            return hostBuilder;
        }

        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            _ = hostBuilder.ConfigureServices(services =>
            {
                //_ = services.AddSingleton<SessionsMainViewModel>();
                //_ = services.AddSingleton<VariablesMainViewModel>();

                //_ = services.AddTransient<LoggerServiceViewModel>();
            });

            return hostBuilder;
        }
    }
}
