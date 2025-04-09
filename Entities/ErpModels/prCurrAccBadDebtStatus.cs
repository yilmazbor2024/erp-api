using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccBadDebtStatus")]
    public partial class prCurrAccBadDebtStatus
    {
        public prCurrAccBadDebtStatus()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public byte DebtStatusTypeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BadDebtReasonCode { get; set; }

        [Required]
        public bool DenyRetailSale { get; set; }

        [Required]
        public bool DenyInstalmentSale { get; set; }

        [Required]
        public bool DenyInstalmentPayment { get; set; }

        [Required]
        public bool DenyGuarantor { get; set; }

        [Required]
        public bool DenyReturnRetailSale { get; set; }

        [Required]
        public bool DenyReturnInstalmentSale { get; set; }

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
        public virtual cdBadDebtReason cdBadDebtReason { get; set; }
        public virtual bsDebtStatusType bsDebtStatusType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
