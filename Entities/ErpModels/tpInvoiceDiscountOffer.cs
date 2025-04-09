using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceDiscountOffer")]
    public partial class tpInvoiceDiscountOffer
    {
        public tpInvoiceDiscountOffer()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceDiscountOfferID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        public Guid? InvoiceLineID { get; set; }

        [Required]
        public bool IsEarned { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferCode { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

        [Required]
        public double DiscountRate { get; set; }

        [Required]
        public byte PaymentTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountVoucherTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [Required]
        public decimal EarnedAmount { get; set; }

        [Required]
        public decimal UsedAmount { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountPointTypeCode { get; set; }

        public Guid? CustomerDiscountPointID { get; set; }

        [Required]
        public decimal EarnedPoint { get; set; }

        [Required]
        public decimal UsedPoint { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Password { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DigitalMarketingServiceCode { get; set; }

        [Required]
        public bool IsBooster { get; set; }

        [Required]
        public bool UsedAsPayment { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        // Navigation Properties
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }

    }
}
