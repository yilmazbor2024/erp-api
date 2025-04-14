namespace erp_api.Models.Requests
{
    public class AddressTypeUpdateRequest
    {
        public int Id { get; set; }
        public string AddressTypeCode { get; set; }
        public string AddressTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
} 