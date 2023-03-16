using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Extensions
{
    public static class StringExtensions
    {
        public static string GetNewName(this string baseName, IEnumerable<string> names)
        {
            int i = 1;
            string name = baseName;
            while (names.Contains(name))
            {
                name = baseName + i;
                i++;
            }
            return name;
        }

        public static string GetCopiedName(this string baseName, IEnumerable<string> names)
        {
            int i = 1;
            string name = baseName + " - Copy";
            while (names.Contains(name))
            {
                name = baseName + " - Copy" + i;
                i++;
            }
            return name;
        }

    }
}
