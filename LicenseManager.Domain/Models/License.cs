using System.ComponentModel;
using System.Text.Json.Serialization;

namespace LicenseManager.Domain.Models
{
    public enum ExpirationType
    {
        [Description("Expired")]
        Expired,
        [Description("Unlimited")]
        Unlimited,
        [Description("One Week")]
        OneWeek,
        [Description("One Month")]
        OneMonth,
        [Description("One Year")]
        OneYear,
    }

    public enum ProductType
    {
        [Description("None")]
        None,
        [Description("Station 2.0")]
        Station,
        [Description("StripLaminator 2.0")]
        StripLaminator,
    }

    public class License : DomainObject
    {
        public string Company { get; set; } = string.Empty;
        public string Machine { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType Product { get; set; } = ProductType.None;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpirationType Expiration { get; set; } = ExpirationType.Expired;
        public Feature? Feature { get; set; }
        public string SerialKey { get; set; } = string.Empty; //identifies the license number for a specify serial number
        public string SerialNumber { get; set; } = string.Empty; //identifies the PC to activate
    }
}
