using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prFixedAssetPurchases")]
    public partial class prFixedAssetPurchases
    {
        public prFixedAssetPurchases()
        {
        }

        [Key]
        [Required]
        public Guid FixedAssetPurchaseID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocCurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public double Qty { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        public Guid? InvoiceLineID { get; set; }

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
        public virtual cdItem cdItem { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }

    }
}
