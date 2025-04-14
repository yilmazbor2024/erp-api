using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    public class AddressTypeCreateRequest
    {
        [Required]
        public string? AddressTypeCode { get; set; }
        
        [Required]
        public string? AddressTypeName { get; set; }
        
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        
        // Property needed by the service
        public string? AddressTypeDesc { get; set; }
    }
} 