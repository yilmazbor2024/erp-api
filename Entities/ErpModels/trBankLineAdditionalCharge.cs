using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankLineAdditionalCharge")]
    public partial class trBankLineAdditionalCharge
    {
        public trBankLineAdditionalCharge()
        {
        }

        [Key]
        [Required]
        public Guid BankLineID { get; set; }

        [Key]
        [Required]
        public byte BankAdditionalChargeTypeCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RelationCurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool IsTransferChargeIncluded { get; set; }

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
        public virtual trBankLine trBankLine { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsBankAdditionalChargeType bsBankAdditionalChargeType { get; set; }

    }
}
