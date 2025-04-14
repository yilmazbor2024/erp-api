namespace erp_api.Models.Requests
{
    public class AddressTypeListRequest
    {
        public string SearchText { get; set; }
        public bool? IsActive { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string LangCode { get; set; } = "TR";
    }
} 