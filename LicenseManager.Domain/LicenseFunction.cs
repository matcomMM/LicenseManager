using LicenseManager.Domain.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text.Json;

namespace LicenseManager.Domain
{
    public static class LicenseFunction
    {
        private static readonly string applicationLicensePath = @$"{AppDomain.CurrentDomain.BaseDirectory}\License.lic";

        public static License? GetApplicationLicense()
        {
            if (File.Exists(applicationLicensePath))
            {
                try
                {
                    return JsonSerializer.Deserialize<License>(File.ReadAllText(applicationLicensePath));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool CheckKey(License license, out string error)
        {
            error = "";

            if (LicenseKey.LocalSerialKey(license.Product) == license.SerialKey)
            {
                return true;
            }
            else
            {
                error = "Wrong SerialKey";
                return false;
            }
        }

        public static License? OpenLicenseFromFile(out string licensePath, out string error)
        {
            licensePath = "";
            error = "";

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
                try
                {
                    return JsonSerializer.Deserialize<License>(File.ReadAllText(ofd.FileName));
                }
                catch (Exception)
                {
                    error = "Wrong license format";
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool SaveLicenseFile(License license, out string error, string licensePath = "")
        {
            error = "";

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
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
