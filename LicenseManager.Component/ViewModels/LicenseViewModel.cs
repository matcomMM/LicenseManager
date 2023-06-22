using CommunityToolkit.Mvvm.ComponentModel;
using LicenseManager.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LicenseManager.Component.ViewModels
{
    public class LicenseViewModel : ObservableValidator
    {

        public LicenseViewModel(License license)
        {
            License = license;
        }

        public License License { get; }

        public Guid Id
        {
            get => License.Id;
            set
            {
                License.Id = value;
                OnPropertyChanged();
                ValidateProperty(Id, nameof(Id));
            }
        }

        [Required]
        [MinLength(3)]
        public string Company
        {
            get => License.Company;
            set
            {
                License.Company = value;
                OnPropertyChanged();
                ValidateProperty(Company, nameof(Company));
            }
        }

        [Required]
        [MinLength(3)]
        public string Machine
        {
            get => License.Machine;
            set
            {
                License.Machine = value;
                OnPropertyChanged();
                ValidateProperty(Machine, nameof(Machine));
            }
        }

        [Required]
        [EmailAddress]
        public string Email
        {
            get => License.Email;
            set
            {
                License.Email = value;
                OnPropertyChanged();
                ValidateProperty(Email, nameof(Email));
            }
        }

        public ProductType Product
        {
            get => License.Product;
            set
            {
                License.Product = value;
                OnPropertyChanged();
            }
        }

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

        public bool Validate()
        {
            ValidateAllProperties();

            return !HasErrors;
        }
    }
}
