namespace erp_api.Models.Requests
{
    public class AddressUpdateRequest
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string AddressTypeCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
} 