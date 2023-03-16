using LicenseManager.Enumerables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.DTOs
{
    public class LicenseDTO : BaseDTO
    {
        public string Contact { get; set; }
        public string Company { get; set; }
        public string CompanyId { get; set; }
        public ProductType Product { get; set; } = ProductType.StripLaminator;
        public ExpirationType Expiration { get; set; } = ExpirationType.Unlimited;
        public LicenseType Type { get; set; } = LicenseType.Full;
        public string SerialNumber { get; set; }
        public string SerialKey { get; set; }

    }
}
