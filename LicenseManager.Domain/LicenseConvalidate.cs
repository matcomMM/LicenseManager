using LicenseManager.Domain.Models;

namespace LicenseManager.Domain
{
    public static class LicenseConvalidate
    {
        public static License? LoadLicense(out string licensePath)
        {
            return LicenseFunction.OpenLicenseFromFile(out licensePath);
        }

        public static void Convalidate(License license)
        {
            license.SerialKey = LicenseKey.GenerateSerialKey(license.SerialNumber);
        }

        public static bool SaveLicense(License license, string licensePath = "") //open file dialog if license path empty
        {
            return LicenseFunction.SaveLicenseFile(license, licensePath);
        }
    }
}
