using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Enumerables
{
    public enum ExpirationType
    {
        [Description("Unlimited")]
        Unlimited,
        [Description("One Week")]
        OneWeek,
        [Description("One Month")]
        OneMonth,
        [Description("One Year")]
        OneYear,
    }
}
