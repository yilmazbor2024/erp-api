using System;

namespace erp_api.Models.Responses
{
    public class CustomerAddressResponse
    {
        public string PostalAddressID { get; set; }
        public string AddressTypeCode { get; set; } = string.Empty;
        public string AddressTypeDescription { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string CountryDescription { get; set; }
        public string StateCode { get; set; } = string.Empty;
        public string StateDescription { get; set; }
        public string CityCode { get; set; } = string.Empty;
        public string CityDescription { get; set; }
        public string DistrictCode { get; set; } = string.Empty;
        public string DistrictDescription { get; set; }
        public string QuarterCode { get; set; }
        public string QuarterName { get; set; }
        public string StreetCode { get; set; }
        public string Street { get; set; }
        public string SiteName { get; set; }
        public string BuildingName { get; set; }
        public string BuildingNum { get; set; }
        public string FloorNum { get; set; }
        public string DoorNum { get; set; }
        public string ZipCode { get; set; }
        public string TaxOfficeCode { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public bool IsBlocked { get; set; }
        public string PostalCode { get; set; } = string.Empty;
    }
} 