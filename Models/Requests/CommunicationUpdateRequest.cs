using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    public class CommunicationUpdateRequest
    {
        [Required]
        public string CommAddress { get; set; }
        
        public bool IsDefault { get; set; }
        
        public string UpdatedBy { get; set; } = "SYSTEM";
        
        public bool IsBlocked { get; set; } = false;
    }
}
