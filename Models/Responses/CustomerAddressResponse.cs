using System;

namespace ErpMobile.Api.Models.Responses
{
    public class CustomerAddressResponse
    {
        public string CustomerCode { get; set; } = string.Empty;
        public Guid PostalAddressId { get; set; }
        public string AddressTypeCode { get; set; } = string.Empty;
        public string AddressTypeDescription { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string CountryDescription { get; set; } = string.Empty;
        public string StateCode { get; set; } = string.Empty;
        public string StateDescription { get; set; } = string.Empty;
        public string CityCode { get; set; } = string.Empty;
        public string CityDescription { get; set; } = string.Empty;
        public string DistrictCode { get; set; } = string.Empty;
        public string DistrictDescription { get; set; } = string.Empty;
        public string QuarterCode { get; set; } = string.Empty;
        public string QuarterName { get; set; } = string.Empty;
        public string StreetCode { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string SiteName { get; set; } = string.Empty;
        public string BuildingName { get; set; } = string.Empty;
        public string BuildingNum { get; set; } = string.Empty;
        public string FloorNum { get; set; } = string.Empty;
        public string DoorNum { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string TaxOfficeCode { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public bool IsBlocked { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CreatedUserName { get; set; } = string.Empty;
    }
} 