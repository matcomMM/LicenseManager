using CommunityToolkit.Mvvm.ComponentModel;
using LicenseManager.Domain.Models;

namespace LicenseManager.Component.ViewModels
{
    public class LicenseViewModel : ObservableObject
    {

        public LicenseViewModel(License license)
        {
            License = license;
        }

        public License License { get; }

        public string Company
        {
            get => License.Company;
            set
            {
                License.Company = value;
                OnPropertyChanged();
            }
        }

        public string Machine
        {
            get => License.Machine;
            set
            {
                License.Machine = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => License.Email;
            set
            {
                License.Email = value;
                OnPropertyChanged();
            }
        }

        public ProductType Product
        {
            get => License.Product;
            set
            {
                License.Product = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(ProductName));
            }
        }

        //public string ProductName => Product.GetDescription();

        public ExpirationType Expiration
        {
            get => License.Expiration;
            set
            {
                License.Expiration = value;
                OnPropertyChanged();
            }
        }

        public Feature? Feature
        {
            get => License.Feature;
            set
            {
                License.Feature = value;
                OnPropertyChanged();
            }
        }

        public string SerialKey
        {
            get => License.SerialKey;
            set
            {
                License.SerialKey = value;
                OnPropertyChanged();
            }
        }

        public string SerialNumber
        {
            get => License.SerialNumber;
            set
            {
                License.SerialNumber = value;
                OnPropertyChanged();
            }
        }

    }
}
