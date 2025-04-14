using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    public class AddressCreateRequest
    {
        [Required]
        public string? CustomerCode { get; set; }
        
        [Required]
        public string? AddressTypeCode { get; set; }
        
        [Required]
        public string? Address { get; set; }
        
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        
        // Properties needed by the service
        public string? CountryCode { get; set; }
        public string? StateCode { get; set; }
        public string? CityCode { get; set; }
        public string? DistrictCode { get; set; }
        public bool IsBlocked { get; set; }
    }
} 