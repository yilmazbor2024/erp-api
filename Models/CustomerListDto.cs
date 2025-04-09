using System;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    [Keyless]
    public class CustomerListDto
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public byte CustomerTypeCode { get; set; }
        public string CustomerTypeDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUsername { get; set; }
        public string CurrencyCode { get; set; }
        public bool IsVIP { get; set; }
        public string PromotionGroupDescription { get; set; }
        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeDescription { get; set; }
        public string OfficeCountryCode { get; set; }
        public string CityDescription { get; set; }
        public string DistrictDescription { get; set; }
        public string IdentityNum { get; set; }
        public string TaxNumber { get; set; }
        public string VendorCode { get; set; }
        public bool IsSubjectToEInvoice { get; set; }
        public bool UseDBSIntegration { get; set; }
        public bool IsBlocked { get; set; }
    }
} 