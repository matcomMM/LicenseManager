using LicenseManager.Component.Services;
using LicenseManager.Component.ViewModels;
using LicenseManager.WPF.HostBuilder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace LicenseManager.WPF
{
    public partial class App : Application
    {
        private static readonly IHost _host;

        static App()
        {
            _host = Host.CreateDefaultBuilder()
                        .AddNavigationServices()
                        .AddMainServices()
                        .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            NavigationService<MainWindowViewModel, ActivationLicenseViewModel> navigationService =
                _host.Services.GetRequiredService<NavigationService<MainWindowViewModel, ActivationLicenseViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }

        #region Unhandled Exception
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMsg = "An application error occurred with the following information:";
            string eMessage = e.Exception.Message;
            StackTrace eStackTrace = new(e.Exception, true);
            StackFrame? eFrame = eStackTrace.GetFrame(0);
            string eClass = eFrame?.GetFileName() ?? "";
            MethodBase? eMethod = eFrame?.GetMethod() ?? null;
            int eLine = eFrame?.GetFileLineNumber() ?? -1;

            if (eClass != null)
            {
                string[] eC = eClass.Split("\\");
                errorMsg = $"{errorMsg}\r\n{eMessage}\r\n\r\nClass: {eC[^1]}\r\nMethod: {eMethod?.Name}\r\nLine: {eLine}";
            }
            else
            {
                errorMsg = $"{errorMsg}\r\n{eMessage}\r\n\r\nMethod: {eMethod?.Name}";
            }

            switch (MessageBox.Show($"{errorMsg}\r\n\r\n{e.Exception.StackTrace}", "StripLaminator Error", MessageBoxButton.YesNoCancel))
            {
                case MessageBoxResult.Yes:
                    e.Handled = true;
                    break;
                case MessageBoxResult.OK:
                    e.Handled = true;
                    break;
                case MessageBoxResult.Cancel:
                    e.Handled = true;
                    string assemblyName = Assembly.GetEntryAssembly()?.GetName().Name ?? "";
                    string assemblyApplicationPath = Assembly.GetEntryAssembly()?.Location.ToString().Replace($"{assemblyName}.dll", $"{assemblyName}.exe") ?? "";

                    if (!string.IsNullOrEmpty(assemblyApplicationPath))
                    {
                        ProcessStartInfo info = new()
                        {
                            FileName = assemblyApplicationPath
                        };
                        _ = Process.Start(info);
                    }

                    Process.GetCurrentProcess().Kill();

                    break;
                case MessageBoxResult.No:
                    e.Handled = false;
                    break;
                case MessageBoxResult.None:
                    e.Handled = false;
                    break;
            }
        }
        #endregion
    }

}
