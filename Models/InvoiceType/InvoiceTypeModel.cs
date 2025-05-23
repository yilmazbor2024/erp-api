using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.InvoiceType
{
    public class InvoiceTypeModel
    {
        public bool IsBlocked { get; set; }
        
        [Required]
        public string LangCode { get; set; }
        
        public string InvoiceTypeDescription { get; set; }
    }
}
