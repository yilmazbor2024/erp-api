using System;

namespace erp_api.Models.Responses
{
    public class CustomerContactResponse
    {
        public int ContactID { get; set; }
        public string ContactTypeCode { get; set; } = string.Empty;
        public string ContactTypeDescription { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public bool IsBlocked { get; set; }
    }
} 