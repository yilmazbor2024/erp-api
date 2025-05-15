using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    public class CommunicationCreateRequest
    {
        [Required]
        public string CommunicationTypeCode { get; set; }
        
        [Required]
        public string CommAddress { get; set; }
        
        public bool IsDefault { get; set; }
        
        public string CreatedBy { get; set; } = "SYSTEM";
        
        public bool IsBlocked { get; set; } = false;
    }
}
