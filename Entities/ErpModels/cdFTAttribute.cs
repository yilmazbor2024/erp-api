using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdFTAttribute")]
    public partial class cdFTAttribute
    {
        public cdFTAttribute()
        {
            cdFTAttributeDescs = new HashSet<cdFTAttributeDesc>();
            dfTransactionDefFTAttributes = new HashSet<dfTransactionDefFTAttribute>();
            prProcessFTAttributes = new HashSet<prProcessFTAttribute>();
            prWorkPlaceFTAttributes = new HashSet<prWorkPlaceFTAttribute>();
            tpBankCreditFTAttributes = new HashSet<tpBankCreditFTAttribute>();
            tpBankFTAttributes = new HashSet<tpBankFTAttribute>();
            tpBankPaymentInstructionFTAttributes = new HashSet<tpBankPaymentInstructionFTAttribute>();
            tpBankPaymentListFTAttributes = new HashSet<tpBankPaymentListFTAttribute>();
            tpCashFTAttributes = new HashSet<tpCashFTAttribute>();
            tpChequeFTAttributes = new HashSet<tpChequeFTAttribute>();
            tpCreditCardPaymentFTAttributes = new HashSet<tpCreditCardPaymentFTAttribute>();
            tpCurrAccBookFTAttributes = new HashSet<tpCurrAccBookFTAttribute>();
            tpDebitFTAttributes = new HashSet<tpDebitFTAttribute>();
            tpExpenseAccrualFTAttributes = new HashSet<tpExpenseAccrualFTAttribute>();
            tpExpenseSlipFTAttributes = new HashSet<tpExpenseSlipFTAttribute>();
            tpGiftCardPaymentFTAttributes = new HashSet<tpGiftCardPaymentFTAttribute>();
            tpInvoiceFTAttributes = new HashSet<tpInvoiceFTAttribute>();
            tpJournalFTAttributes = new HashSet<tpJournalFTAttribute>();
            tpOrderFTAttributes = new HashSet<tpOrderFTAttribute>();
            tpOtherPaymentFTAttributes = new HashSet<tpOtherPaymentFTAttribute>();
            tpPaymentFTAttributes = new HashSet<tpPaymentFTAttribute>();
            tpProposalFTAttributes = new HashSet<tpProposalFTAttribute>();
            tpVirementFTAttributes = new HashSet<tpVirementFTAttribute>();
        }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual cdFTAttributeType cdFTAttributeType { get; set; }

        public virtual ICollection<cdFTAttributeDesc> cdFTAttributeDescs { get; set; }
        public virtual ICollection<dfTransactionDefFTAttribute> dfTransactionDefFTAttributes { get; set; }
        public virtual ICollection<prProcessFTAttribute> prProcessFTAttributes { get; set; }
        public virtual ICollection<prWorkPlaceFTAttribute> prWorkPlaceFTAttributes { get; set; }
        public virtual ICollection<tpBankCreditFTAttribute> tpBankCreditFTAttributes { get; set; }
        public virtual ICollection<tpBankFTAttribute> tpBankFTAttributes { get; set; }
        public virtual ICollection<tpBankPaymentInstructionFTAttribute> tpBankPaymentInstructionFTAttributes { get; set; }
        public virtual ICollection<tpBankPaymentListFTAttribute> tpBankPaymentListFTAttributes { get; set; }
        public virtual ICollection<tpCashFTAttribute> tpCashFTAttributes { get; set; }
        public virtual ICollection<tpChequeFTAttribute> tpChequeFTAttributes { get; set; }
        public virtual ICollection<tpCreditCardPaymentFTAttribute> tpCreditCardPaymentFTAttributes { get; set; }
        public virtual ICollection<tpCurrAccBookFTAttribute> tpCurrAccBookFTAttributes { get; set; }
        public virtual ICollection<tpDebitFTAttribute> tpDebitFTAttributes { get; set; }
        public virtual ICollection<tpExpenseAccrualFTAttribute> tpExpenseAccrualFTAttributes { get; set; }
        public virtual ICollection<tpExpenseSlipFTAttribute> tpExpenseSlipFTAttributes { get; set; }
        public virtual ICollection<tpGiftCardPaymentFTAttribute> tpGiftCardPaymentFTAttributes { get; set; }
        public virtual ICollection<tpInvoiceFTAttribute> tpInvoiceFTAttributes { get; set; }
        public virtual ICollection<tpJournalFTAttribute> tpJournalFTAttributes { get; set; }
        public virtual ICollection<tpOrderFTAttribute> tpOrderFTAttributes { get; set; }
        public virtual ICollection<tpOtherPaymentFTAttribute> tpOtherPaymentFTAttributes { get; set; }
        public virtual ICollection<tpPaymentFTAttribute> tpPaymentFTAttributes { get; set; }
        public virtual ICollection<tpProposalFTAttribute> tpProposalFTAttributes { get; set; }
        public virtual ICollection<tpVirementFTAttribute> tpVirementFTAttributes { get; set; }
    }
}
