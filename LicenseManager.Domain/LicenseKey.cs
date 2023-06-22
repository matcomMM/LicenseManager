using LicenseManager.Domain.Models;
using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Text;

namespace LicenseManager.Domain
{
    public static class LicenseKey
    {
        private const string _secretKey = "38ef2819-5ded-446b-9e98-d0f9e277339f";

        public static License? ApplicationLicense => LicenseFunction.GetApplicationLicense();

        public static string SerialNumber => GetHostSerialNumber();

        public static string LocalSerialKey(ProductType productType)
        { 
            return GenerateSerialKey(SerialNumber, productType); 
        }

        public static string GenerateSerialKey(string serialNumber, ProductType productType)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(serialNumber + productType + _secretKey);
            byte[] hashBytes = MD5.HashData(inputBytes);

            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            string licenseKey = sb.ToString();

            return licenseKey;
        }

        private static string GetHostSerialNumber()
        {
            string devId = GetHostDeviceId();
            string prodId = GetHostProductId();
            string machineId = $"{devId}-{prodId}";

            byte[] hash = SHA1.HashData(Encoding.UTF8.GetBytes(machineId));
            string hex = BitConverter.ToString(hash).Replace("-", string.Empty);
            return hex;
        }

        private static string GetHostProductId()
        {
            string productId;
            try
            {
                RegistryKey? regProduct = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", false);
                productId = regProduct?.GetValue("ProductId")?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                productId = string.Format($"ProductID error: {ex.Message}");
            }

            return productId;
        }

        private static string GetHostDeviceId()
        {
            string deviceId;

            try
            {
                RegistryKey? regDevice = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\SQMClient", false);
                deviceId = regDevice?.GetValue("MachineId")?.ToString()?.Replace("{", "").Replace("}", "") ?? "";
            }
            catch (Exception ex)
            {
                deviceId = string.Format($"DeviceID error: {ex.Message}");
            }

            return deviceId;
        }
    }
}
