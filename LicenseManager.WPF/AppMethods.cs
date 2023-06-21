using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LicenseManager.WPF
{
    public partial class App
    {
        public static T GetRequiredService<T>() => _host.Services.GetRequiredService<T>();

        public static IServiceScope GetServiceScope() => _host.Services.CreateScope();

        public static Window GetMainWindow()
        {
            return _host.Services.GetRequiredService<MainWindow>();
        }

        public static Style GetStyle(string key)
        {
            return (Style)Current.FindResource(key);
        }
    }
}
