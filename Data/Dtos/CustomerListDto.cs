using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ErpMobile.Api.Data.Dtos
{
    [Keyless]
    public class CustomerListDto
    {
        [Column("CustomerCode")]
        public string CustomerCode { get; set; }

        [Column("CustomerName")]
        public string CustomerName { get; set; }

        [Column("CustomerTypeCode")]
        public string CustomerTypeCode { get; set; }

        [Column("CustomerTypeDescription")]
        public string CustomerTypeDescription { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("CreatedUsername")]
        public string CreatedUsername { get; set; }

        [Column("CurrencyCode")]
        public string CurrencyCode { get; set; }

        [Column("IsVIP")]
        public bool IsVIP { get; set; }

        [Column("PromotionGroupDescription")]
        public string PromotionGroupDescription { get; set; }

        [Column("CompanyCode")]
        public string CompanyCode { get; set; }

        [Column("OfficeCode")]
        public string OfficeCode { get; set; }

        [Column("OfficeDescription")]
        public string OfficeDescription { get; set; }

        [Column("OfficeCountryCode")]
        public string OfficeCountryCode { get; set; }

        [Column("CityDescription")]
        public string CityDescription { get; set; }

        [Column("DistrictDescription")]
        public string DistrictDescription { get; set; }

        [Column("IdentityNum")]
        public string IdentityNum { get; set; }

        [Column("TaxNumber")]
        public string TaxNumber { get; set; }

        [Column("VendorCode")]
        public string VendorCode { get; set; }

        [Column("IsSubjectToEInvoice")]
        public bool IsSubjectToEInvoice { get; set; }

        [Column("UseDBSIntegration")]
        public bool UseDBSIntegration { get; set; }

        [Column("IsBlocked")]
        public bool IsBlocked { get; set; }
    }
} 