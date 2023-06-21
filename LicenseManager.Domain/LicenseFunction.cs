using LicenseManager.Domain.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace LicenseManager.Domain
{
    public static class LicenseFunction
    {
        public static bool CheckKey(License license)
        {
            if (license != null)
            {
                if (LicenseKey.LocalSerialKey == license.SerialKey)
                {
                    return true;
                }
                else
                {
                    _ = MessageBox.Show("Wrong SerialKey", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                _ = MessageBox.Show("License Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static License? OpenLicenseFromFile(out string licensePath)
        {
            licensePath = "";

            OpenFileDialog ofd = new()
            {
                Filter = "License Files(.lic)| *.lic",
                Title = "Select License file",
                FileName = "License",
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == true)
            {
                licensePath = ofd.FileName;
                return JsonSerializer.Deserialize<License>(File.ReadAllText(ofd.FileName));
            }
            else
            {
                return null;
            }
        }

        public static bool SaveLicenseFile(License license, string licensePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(licensePath))
                {
                    SaveFileDialog sfd = new()
                    {
                        Filter = "License Files(.lic)| *.lic",
                        Title = "Select License file",
                        FileName = "License",
                        RestoreDirectory = true
                    };
                    if (sfd.ShowDialog() == false)
                    {
                        return false;
                    }
                    licensePath = sfd.FileName;
                }

                if (File.Exists(licensePath))
                {
                    File.Delete(licensePath);
                }

                File.WriteAllText(licensePath, JsonSerializer.Serialize(license));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
