using System;

namespace ErpMobile.Api.Models.Responses
{
    public class CustomerCommunicationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public Guid CommunicationId { get; set; }
        public string CommunicationTypeCode { get; set; } = string.Empty;
        public string CommunicationTypeDescription { get; set; } = string.Empty;
        public string CommunicationValue { get; set; } = string.Empty;
        public string CommAddress { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public bool IsBlocked { get; set; }
        public bool CanSendAdvert { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserName { get; set; } = string.Empty;
    }
} 