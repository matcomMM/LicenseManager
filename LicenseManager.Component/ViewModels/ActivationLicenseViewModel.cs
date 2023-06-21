﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LicenseManager.Domain;
using LicenseManager.Domain.Models;
using System;
using System.Windows;

namespace LicenseManager.Component.ViewModels
{
    public partial class ActivationLicenseViewModel : ObservableObject
    {
        public ActivationLicenseViewModel(License license)
        {
            licenseVM = new LicenseViewModel(license);
        }

        [ObservableProperty]
        private LicenseViewModel licenseVM;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string error = string.Empty;

        public bool HasError => !string.IsNullOrEmpty(Error);

        [RelayCommand]
        private void Import()
        {
            License? license = LicenseFunction.OpenLicenseFromFile(out _);
            if (license != null)
            {
                LicenseVM = new(license);

                if (CanActivate())
                {
                    ActivateCommand.NotifyCanExecuteChanged();
                }
                else
                {
                    Error = "Wrong Serial Key";
                }
            }
        }

        [RelayCommand]
        private void Export()
        {
            if (CanExport())
            {
                LicenseVM.Id = Guid.NewGuid();
                LicenseVM.SerialNumber = LicenseKey.SerialNumber;

                string error = string.Empty;

                if (LicenseFunction.SaveLicenseFile(LicenseVM.License, out error))
                {
                    _ = MessageBox.Show("License saved.\nPlease send this file at info@marangonimachinery.com", "License", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        _ = MessageBox.Show($"Export Error\n\n{error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private bool CanExport()
        {
            return LicenseVM.Validate();
        }

        [RelayCommand(CanExecute = nameof(CanActivate))]
        private void Activate()
        {
            string error = string.Empty;

            if (LicenseFunction.SaveLicenseFile(LicenseVM.License, out error, @$"{AppDomain.CurrentDomain.BaseDirectory}\License.lic"))
            {
                _ = MessageBox.Show("License Activated", "License", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (!string.IsNullOrEmpty(error))
                {
                    _ = MessageBox.Show($"License Error\n\n{error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CanActivate()
        {
            return LicenseKey.LocalSerialKey(LicenseVM.Product) == LicenseVM.SerialKey;
        }
    }
}
