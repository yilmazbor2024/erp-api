namespace ErpMobile.Api.Models.Requests
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
        public string ZipCode { get { return PostalCode; } set { PostalCode = value; } }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; } = false;
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        public string CityCode { get; set; }
        public string DistrictCode { get; set; }
    }
}