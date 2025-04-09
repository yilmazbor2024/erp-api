using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDiscountVoucher")]
    public partial class cdDiscountVoucher
    {
        public cdDiscountVoucher()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountVoucherTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [Required]
        public byte CustomerTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        [Required]
        public DateTime FirstValidDate { get; set; }

        [Required]
        public DateTime LastValidDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal UsedAmount { get; set; }

        [Required]
        public decimal MinAmount { get; set; }

        [Required]
        public float DiscountRate { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

        public Guid? OrderHeaderID { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsCanceled { get; set; }

        [Required]
        public bool IsReturnVoucher { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CancelDescription { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdDiscountVoucherType cdDiscountVoucherType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
