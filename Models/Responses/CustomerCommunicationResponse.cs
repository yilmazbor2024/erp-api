using System;

namespace erp_api.Models.Responses
{
    public class CustomerCommunicationResponse
    {
        public Guid CommunicationID { get; set; }
        public string CommunicationTypeCode { get; set; } = string.Empty;
        public string CommunicationTypeDescription { get; set; } = string.Empty;
        public string Communication { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public bool IsBlocked { get; set; }
    }
} 