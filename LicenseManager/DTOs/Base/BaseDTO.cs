using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.DTOs
{
    public class BaseDTO : IBaseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
