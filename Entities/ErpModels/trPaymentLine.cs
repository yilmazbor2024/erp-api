using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPaymentLine")]
    public partial class trPaymentLine
    {
        public trPaymentLine()
        {
            tpPaymentFTAttributes = new HashSet<tpPaymentFTAttribute>();
            trPaymentLineCurrencys = new HashSet<trPaymentLineCurrency>();
        }

        [Key]
        [Required]
        public Guid PaymentLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte PaymentTypeCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        public Guid? CreditCardPaymentLineID { get; set; }

        public Guid? CashLineID { get; set; }

        public Guid? ChequeLineID { get; set; }

        public Guid? BankLineID { get; set; }

        public Guid? ReverseDebitLineID { get; set; }

        public Guid? GiftCardPaymentLineID { get; set; }

        public Guid? OtherPaymentLineID { get; set; }

        public Guid? EmployeeDebitID { get; set; }

        public Guid? DebitLineID { get; set; }

        public Guid? OrderPaymentPlanID { get; set; }

        [Required]
        public Guid PaymentHeaderID { get; set; }

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
        public virtual trOtherPaymentLine trOtherPaymentLine { get; set; }
        public virtual trDebitLine trDebitLine { get; set; }
        public virtual trBankLine trBankLine { get; set; }
        public virtual trEmployeeDebit trEmployeeDebit { get; set; }
        public virtual trChequeLine trChequeLine { get; set; }
        public virtual trCashLine trCashLine { get; set; }
        public virtual trGiftCardPaymentLine trGiftCardPaymentLine { get; set; }
        public virtual trCreditCardPaymentLine trCreditCardPaymentLine { get; set; }
        public virtual bsPaymentType bsPaymentType { get; set; }
        public virtual trPaymentHeader trPaymentHeader { get; set; }
        public virtual trOrderPaymentPlan trOrderPaymentPlan { get; set; }

        public virtual ICollection<tpPaymentFTAttribute> tpPaymentFTAttributes { get; set; }
        public virtual ICollection<trPaymentLineCurrency> trPaymentLineCurrencys { get; set; }
    }
}
