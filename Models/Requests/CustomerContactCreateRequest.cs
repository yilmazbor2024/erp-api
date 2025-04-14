namespace erp_api.Models.Requests
{
    public class CustomerContactCreateRequest
    {
        public string? ContactTypeCode { get; set; }
        public string? Contact { get; set; }
        public bool IsDefault { get; set; }
    }
} 