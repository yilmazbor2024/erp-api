using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderAdvancePayments")]
    public partial class trOrderAdvancePayments
    {
        public trOrderAdvancePayments()
        {
        }

        [Key]
        [Required]
        public Guid OrderAdvancePaymentsID { get; set; }

        [Required]
        public Guid OrderPaymentPlanID { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Guid? CreditCardPaymentLineID { get; set; }

        public Guid? CashLineID { get; set; }

        public Guid? ChequeLineID { get; set; }

        public Guid? BankLineID { get; set; }

        public Guid? GiftCardPaymentLineID { get; set; }

        public Guid? OtherPaymentLineID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual trChequeLine trChequeLine { get; set; }
        public virtual trGiftCardPaymentLine trGiftCardPaymentLine { get; set; }
        public virtual trCashLine trCashLine { get; set; }
        public virtual trOtherPaymentLine trOtherPaymentLine { get; set; }
        public virtual trBankLine trBankLine { get; set; }
        public virtual trCreditCardPaymentLine trCreditCardPaymentLine { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual trOrderPaymentPlan trOrderPaymentPlan { get; set; }

    }
}
