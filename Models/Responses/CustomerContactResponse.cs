using System;

namespace ErpMobile.Api.Models.Responses
{
    public class CustomerContactResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public Guid ContactId { get; set; }
        public string ContactTypeCode { get; set; } = string.Empty;
        public string ContactTypeDescription { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactSurname { get; set; } = string.Empty;
        public string ContactTitle { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsAuthorized { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserName { get; set; } = string.Empty;
    }
} 