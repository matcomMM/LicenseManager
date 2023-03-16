using LicenseManager.DTOs;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LicenseManager
{
    public static class License
    {
        public static LicenseDTO OpenLicenseFromFile()
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "License Files(.license)| *.license";
            ofd.Title = "Select License file";
            ofd.FileName = "Marangoni License";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                return JsonConvert.DeserializeObject<LicenseDTO>(ofd.FileName);
            }
            else
            {
                return null;
            }
        }

        public static bool CheckLocalKey(string licensePath)
        {
            return CheckKey(licensePath, LicenseKey.SerialKey);
        }

        private static bool CheckKey(string licensePath, string serialKey)
        {
            if (File.Exists(licensePath))
            {
                LicenseDTO license = JsonConvert.DeserializeObject<LicenseDTO>(File.ReadAllText(licensePath));
                if (serialKey == license.SerialKey)
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
    }
}
