using LicenseManager.DTOs;
using LicenseManager.Enumerables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LicenseManager.ViewModels
{
    public class LicenseViewModel : ViewModelBaseDTO<LicenseDTO>
    {
        public LicenseViewModel(LicenseDTO dto) : base(dto) { }

        #region Dispose
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose();
        }
        private bool isDisposed;
        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    isDisposed = true;
                }
            }
        }
        ~LicenseViewModel()
        {
            Dispose(false);
        }
        #endregion

        public string Name
        {
            get => Item.Name;
            set => Item.Name = value;
        }

        public string Contact
        {
            get => Item.Contact;
            set => Item.Contact = value;
        }

        public string Company
        {
            get => Item.Company;
            set => Item.Company = value;
        }

        public string CompanyId
        {
            get => Item.CompanyId;
            set => Item.CompanyId = value;
        }

        public ProductType Product
        {
            get => Item.Product;
            set => Item.Product = value;
        }

        public ExpirationType Expiration
        {
            get => Item.Expiration;
            set => Item.Expiration = value;
        }

        public LicenseType Type
        {
            get => Item.Type;
            set => Item.Type = value;
        }

        public string SerialNumber
        {
            get => Item.SerialNumber;
            set => Item.SerialNumber = value;
        }

        public string SerialKey
        {
            get => Item.SerialKey;
            set => Item.SerialKey = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
