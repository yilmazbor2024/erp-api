namespace erp_api.Models.Requests
{
    public class CustomerCommunicationCreateRequest
    {
        public string? CommunicationTypeCode { get; set; }
        public string? Communication { get; set; }
        public bool IsDefault { get; set; }
    }
} 