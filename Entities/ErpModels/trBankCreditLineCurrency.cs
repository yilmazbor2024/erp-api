using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankCreditLineCurrency")]
    public partial class trBankCreditLineCurrency
    {
        public trBankCreditLineCurrency()
        {
        }

        [Key]
        [Required]
        public Guid BankCreditLineID { get; set; }

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
        public decimal CapitalAmount { get; set; }

        [Required]
        public decimal InterestAmount { get; set; }

        [Required]
        public decimal KKDFAmount { get; set; }

        [Required]
        public decimal BSMVAmount { get; set; }

        [Required]
        public decimal DeductionAmount1 { get; set; }

        [Required]
        public decimal DeductionAmount2 { get; set; }

        [Required]
        public decimal DeductionAmount3 { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
        public virtual trBankCreditLine trBankCreditLine { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
