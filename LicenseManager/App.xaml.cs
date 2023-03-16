using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using LicenseManager.HostBuilder;
using LicenseManager.Services;
using Microsoft.Extensions.DependencyInjection;
using LicenseManager.ViewModels;

namespace LicenseManager
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                        .AddNavigationServices()
                        .AddViewModels()
                        .AddMainServices()
                        .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            INavigationService navigationService = _host.Services.GetRequiredService<NavigationService<ActivationLicenseViewModel>>();
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


        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMsg = "An application error occurred with the following information:";
            string eMessage = e.Exception.Message;
            StackTrace eStackTrace = new(e.Exception, true);
            StackFrame eFrame = eStackTrace.GetFrame(0);
            string eClass = eFrame.GetFileName();
            MethodBase eMethod = eFrame.GetMethod();
            int eLine = eFrame.GetFileLineNumber();

            if (eClass != null)
            {
                var eC = eClass.Split("\\");
                errorMsg = $"{errorMsg}\r\n{eMessage}\r\n\r\nClass: {eC[eC.Length - 1]}\r\nMethod: {eMethod.Name}\r\nLine: {eLine}";
            }
            else
            {
                errorMsg = $"{errorMsg}\r\n{eMessage}\r\n\r\nMethod: {eMethod.Name}";
            }

            switch (MessageBox.Show($"{errorMsg}\r\n\r\n{e.Exception.StackTrace}", "StripLaminator Error", MessageBoxButton.YesNoCancel))
            {
                case MessageBoxResult.OK:
                    e.Handled = true;
                    break;
                case MessageBoxResult.No:
                    e.Handled = false;
                    break;
                case MessageBoxResult.Cancel:
                    e.Handled = true;

                    ProcessStartInfo info = new()
                    {
                        Arguments = "/C choice /C Y /N /D Y /T 1 & START \"\" \"" + Assembly.GetEntryAssembly().Location + "\"",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        FileName = "cmd.exe"
                    };
                    _ = Process.Start(info);
                    Process.GetCurrentProcess().Kill();

                    break;
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.Yes:
                    break;
            }
        }

    }
}
