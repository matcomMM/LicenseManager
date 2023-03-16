using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Enumerables
{
    public enum ProductType
    {
        [Description("Station 2.0")]
        Station,
        [Description("StripLaminator 2.0")]
        StripLaminator,
    }
}
