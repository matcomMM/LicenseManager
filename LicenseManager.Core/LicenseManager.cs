using LicenseManager.Domain.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace LicenseManager.Core
{
    public static class LicenseManager
    {
        private static bool CheckKey(string licensePath)
        {
            if (File.Exists(licensePath))
            {
                License license = JsonConvert.DeserializeObject<License>(File.ReadAllText(licensePath));
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
                _ = MessageBox.Show("No license file present.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                return JsonConvert.DeserializeObject<License>(File.ReadAllText(ofd.FileName));
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
                    SaveFileDialog ofd = new()
                    {
                        Filter = "License Files(.lic)| *.lic",
                        Title = "Select License file",
                        FileName = "License",
                        RestoreDirectory = true
                    };
                    if (ofd.ShowDialog() == false)
                    {
                        return false;
                    }
                    licensePath = ofd.FileName;
                }

                if (File.Exists(licensePath))
                {
                    File.Delete(licensePath);
                }

                File.WriteAllText(licensePath, JsonConvert.SerializeObject(license));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
