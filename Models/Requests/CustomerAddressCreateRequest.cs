using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    public class CustomerAddressCreateRequest
    {
        [Required]
        public string? CustomerCode { get; set; }
        
        public int AddressTypeCode { get; set; }
        public string? AddressTitle { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? PostalCode { get; set; }
        public int? CityCode { get; set; }
        public int? DistrictCode { get; set; }
        public int? RegionCode { get; set; }
        public bool IsDefault { get; set; }
        
        // Properties needed by the service
        public string? Address { get; set; }
        public string? CountryCode { get; set; }
        public string? StateCode { get; set; }
        public bool IsBlocked { get; set; }
    }
} 