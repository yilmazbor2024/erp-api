namespace ErpMobile.Api.Models.Requests
{
    public class AddressTypeUpdateRequest
    {
        public int Id { get; set; }
        public string AddressTypeCode { get; set; }
        public string AddressTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public string AddressTypeDescription { get; set; }
        public string LastUpdatedUserName { get; set; } = "SYSTEM";
    }
}