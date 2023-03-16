using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Enumerables
{
    public enum LicenseType
    {
        [Description("Full")]
        Full,
        [Description("Restricted")]
        Restricted,
    }
}
