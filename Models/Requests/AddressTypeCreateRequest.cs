using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    public class AddressTypeCreateRequest
    {
        [Required]
        public string? AddressTypeCode { get; set; }
        
        [Required]
        public string? AddressTypeName { get; set; }
        
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        
        // Property needed by the service
        public string? AddressTypeDesc { get; set; }
        public string? AddressTypeDescription { get; set; }
        
        public string CreatedUserName { get; set; } = "SYSTEM";
        public string LastUpdatedUserName { get; set; } = "SYSTEM";
    }
}