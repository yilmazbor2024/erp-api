namespace ErpMobile.Api.Models.Requests
{
    public class AddressTypeRequest
    {
        public int Id { get; set; }
        public string AddressTypeCode { get; set; }
        public string AddressTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
} 