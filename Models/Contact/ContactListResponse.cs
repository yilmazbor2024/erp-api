namespace ErpMobile.Api.Models.Contact
{
    public class ContactListResponse
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
} 