using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBadDebtTransLineResult")]
    public partial class trBadDebtTransLineResult
    {
        public trBadDebtTransLineResult()
        {
        }

        [Key]
        [Required]
        public Guid BadDebtTransLineID { get; set; }

        [Required]
        public byte BadDebtResultCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public DateTime ResultDate { get; set; }

        [Required]
        public decimal TotalCredit { get; set; }

        [Required]
        public byte DebtStatusTypeCode { get; set; }

        [Required]
        public bool DenyRetailSale { get; set; }

        [Required]
        public bool DenyInstalmentSale { get; set; }

        [Required]
        public bool DenyReturnRetailSale { get; set; }

        [Required]
        public bool DenyReturnInstalmentSale { get; set; }

        [Required]
        public bool DenyInstalmentPayment { get; set; }

        [Required]
        public bool DenyGuarantor { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsClosedWithMissingAmount { get; set; }

        public Guid? PaymentHeaderID { get; set; }

        [Required]
        public byte LastDebtStatusTypeCode { get; set; }

        [Required]
        public bool LastDenyRetailSale { get; set; }

        [Required]
        public bool LastDenyInstalmentSale { get; set; }

        [Required]
        public bool LastDenyInstalmentPayment { get; set; }

        [Required]
        public bool LastDenyGuarantor { get; set; }

        [Required]
        public bool LastDenyReturnRetailSale { get; set; }

        [Required]
        public bool LastDenyReturnInstalmentSale { get; set; }

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
        public virtual bsDebtStatusType bsDebtStatusType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trBadDebtTransLine trBadDebtTransLine { get; set; }
        public virtual trPaymentHeader trPaymentHeader { get; set; }

    }
}
