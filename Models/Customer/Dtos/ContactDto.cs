namespace ErpMobile.Api.Models.Customer
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
} 