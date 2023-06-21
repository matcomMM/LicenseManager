using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Text;

namespace LicenseManager.Core
{
    public static class LicenseKey
    {
        public static string SerialNumber => GetHostSerialNumber();

        public static string LocalSerialKey => GenerateSerialKey(SerialNumber);

        public static string GenerateSerialKey(string serialNumber)
        {
            string licenseKey = string.Empty;
            string secretKey = "38ef2819-5ded-446b-9e98-d0f9e277339f";

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(serialNumber + secretKey);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                licenseKey = sb.ToString();
            }

            return licenseKey;
        }

        private static string GetHostSerialNumber()
        {
            string devId = GetHostDeviceId();
            string prodId = GetHostProductId();
            string machineId = $"{devId}-{prodId}";

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(machineId));
                string hex = BitConverter.ToString(hash).Replace("-", string.Empty);
                return hex;
            }
        }

        private static string GetHostProductId()
        {
            string productId;
            try
            {
                RegistryKey regProduct = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", false);
                productId = regProduct.GetValue("ProductId").ToString();
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
                RegistryKey regDevice = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\SQMClient", false);
                deviceId = regDevice.GetValue("MachineId").ToString().Replace("{", "").Replace("}", "");
            }
            catch (Exception ex)
            {
                deviceId = string.Format($"DeviceID error: {ex.Message}");
            }

            return deviceId;
        }

        #region OLD
        //private static Dictionary<string, object> GetSystemInformation()
        //{
        //    Dictionary<string, object> infos = new();
        //    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        //    GetValuesWin32(infos, docPath, "Win32_OperatingSystem");
        //    GetValuesWin32(infos, docPath, "Win32_NetworkAdapterConfiguration");
        //    GetValuesWin32(infos, docPath, "Win32_Processor");
        //    GetValuesWin32(infos, docPath, "Win32_ComputerSystem");
        //    GetValuesWin32(infos, docPath, "Win32_PnPSignedDriver");
        //    GetValuesWin32(infos, docPath, "Win32_Service");
        //    GetValuesWin32(infos, docPath, "Win32_DiskDrive");

        //    return infos;
        //}
        //private static void GetValuesWin32(Dictionary<string, object> directory, string docPath, string dll)
        //{
        //    using (StreamWriter outputFile = new(Path.Combine(docPath, $"{dll}.txt")))
        //    {
        //        ManagementObjectSearcher searcher = new($"SELECT * FROM {dll}");
        //        ManagementObjectCollection collection = searcher.Get();
        //        foreach (ManagementObject obj in collection)
        //        {
        //            foreach (PropertyData prop in obj.Properties)
        //            {
        //                string _key = $"{dll} | {prop.Name}".GetNewName(directory.Keys);
        //                object _value = prop.Value;

        //                directory.Add(_key, _value);
        //                outputFile.WriteLine($"{_key} -> {_value}");
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}
