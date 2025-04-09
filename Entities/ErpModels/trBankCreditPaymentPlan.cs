using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankCreditPaymentPlan")]
    public partial class trBankCreditPaymentPlan
    {
        public trBankCreditPaymentPlan()
        {
        }

        [Key]
        [Required]
        public Guid BankCreditPaymentPlanID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public Guid BankCreditHeaderID { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trBankCreditHeader trBankCreditHeader { get; set; }

    }
}
