using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models    
{
    [Table("cdCurrAcc")]
    public class cdCurrAcc
    {
        [Key]
        [Column(Order = 1)]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string CurrAccCode { get; set; }

        [StringLength(60)]
        public string FirstLastName { get; set; }

        public byte CustomerTypeCode { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedUserName { get; set; }

        [StringLength(10)]
        public string CurrencyCode { get; set; }

        public bool IsVIP { get; set; }

        [Required]
        [StringLength(30)]
        [ForeignKey("PromotionGroupCode")]
        public string PromotionGroupCode { get; set; }

        [StringLength(10)]
        public string CompanyCode { get; set; }

        [StringLength(10)]
        public string OfficeCode { get; set; }

        [StringLength(20)]
        public string IdentityNum { get; set; }

        [StringLength(20)]
        public string TaxNumber { get; set; }

        public bool IsSubjectToEInvoice { get; set; }

        public bool UseDBSIntegration { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsIndividualAcc { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        [Required]
        [StringLength(20)]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        // Navigation Properties
        [ForeignKey("PromotionGroupCode")]
        public virtual cdPromotionGroup PromotionGroup { get; set; }
        public virtual cdCurrAccDesc CurrAccDesc { get; set; }
        public virtual prCustomerVendorAccount CustomerVendorAccount { get; set; }
        public virtual prCurrAccDefault CurrAccDefault { get; set; }
    }
} 