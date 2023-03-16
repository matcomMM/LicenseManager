using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace LicenseManager
{
    public static class LicenseKey
    {
        public static string SecretKey => "38ef2819-5ded-446b-9e98-d0f9e277339f";

        public static string SerialNumber => GetHostSerialNumber();

        public static string SerialKey => GenerateLicenseKey(SerialNumber, SecretKey);

        public static string GetHostSerialNumber()
        {
            //cercare un modo migliore per l'unicità della macchina
            string computerName = Environment.MachineName;
            string macAddress = NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();

            string machineInfo = $"{computerName}_{macAddress}";

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(machineInfo));
                string hex = BitConverter.ToString(hash).Replace("-", string.Empty);
                return hex;
            }
        }

        public static string GenerateLicenseKey(string motherboardSerialNumber, string secretKey)
        {
            string licenseKey = string.Empty;

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(motherboardSerialNumber + secretKey);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                licenseKey = sb.ToString();
            }

            return licenseKey;
        }

    }
}
