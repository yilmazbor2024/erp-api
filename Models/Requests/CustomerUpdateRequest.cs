using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Requests
{
    public class CustomerUpdateRequest
    {
        [Required]
        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }
        
        public bool IsVIP { get; set; }
        
        public string PromotionGroupCode { get; set; }
        
        public string CompanyCode { get; set; } = "1";
        
        public string OfficeCode { get; set; } = "M";
        
        public string IdentityNum { get; set; }
        
        public string TaxNumber { get; set; }
        
        public string TaxOfficeCode { get; set; }
        
        public bool IsSubjectToEInvoice { get; set; }
        
        public bool UseDBSIntegration { get; set; }
        
        public bool IsBlocked { get; set; }
        
        public string LastUpdatedUserName { get; set; } = "SYSTEM";
        
        public bool IsRealPerson { get; set; }
        
        public int CustomerTypeCode { get; set; }
        
        public string CurrencyCode { get; set; }

        public List<CustomerCommunicationCreateRequestNew> Communications { get; set; } = new();
        public List<CustomerContactCreateRequestNew> Contacts { get; set; } = new();
        public List<CustomerAddressCreateRequestNew> Addresses { get; set; } = new();
    }
}
