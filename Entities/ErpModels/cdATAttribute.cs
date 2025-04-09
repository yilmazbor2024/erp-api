using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdATAttribute")]
    public partial class cdATAttribute
    {
        public cdATAttribute()
        {
            cdATAttributeDescs = new HashSet<cdATAttributeDesc>();
            dfBankDefATAttributes = new HashSet<dfBankDefATAttribute>();
            dfCashDefATAttributes = new HashSet<dfCashDefATAttribute>();
            dfChequeDefATAttributes = new HashSet<dfChequeDefATAttribute>();
            dfCreditCardPaymentDefATAttributes = new HashSet<dfCreditCardPaymentDefATAttribute>();
            dfJournalDefATAttributes = new HashSet<dfJournalDefATAttribute>();
            prPOSTerminalATAttributes = new HashSet<prPOSTerminalATAttribute>();
            prProcessATAttributes = new HashSet<prProcessATAttribute>();
            prWorkPlaceATAttributes = new HashSet<prWorkPlaceATAttribute>();
            tpAllocationATAttributes = new HashSet<tpAllocationATAttribute>();
            tpBankATAttributes = new HashSet<tpBankATAttribute>();
            tpBankCreditATAttributes = new HashSet<tpBankCreditATAttribute>();
            tpBankPaymentInstructionATAttributes = new HashSet<tpBankPaymentInstructionATAttribute>();
            tpBankPaymentListATAttributes = new HashSet<tpBankPaymentListATAttribute>();
            tpCashATAttributes = new HashSet<tpCashATAttribute>();
            tpChequeATAttributes = new HashSet<tpChequeATAttribute>();
            tpContractATAttributes = new HashSet<tpContractATAttribute>();
            tpCreditCardPaymentATAttributes = new HashSet<tpCreditCardPaymentATAttribute>();
            tpCurrAccBookATAttributes = new HashSet<tpCurrAccBookATAttribute>();
            tpDebitATAttributes = new HashSet<tpDebitATAttribute>();
            tpExpenseAccrualATAttributes = new HashSet<tpExpenseAccrualATAttribute>();
            tpExpenseSlipATAttributes = new HashSet<tpExpenseSlipATAttribute>();
            tpGiftCardPaymentATAttributes = new HashSet<tpGiftCardPaymentATAttribute>();
            tpInvoiceATAttributes = new HashSet<tpInvoiceATAttribute>();
            tpJournalATAttributes = new HashSet<tpJournalATAttribute>();
            tpOrderATAttributes = new HashSet<tpOrderATAttribute>();
            tpOtherPaymentATAttributes = new HashSet<tpOtherPaymentATAttribute>();
            tpPaymentATAttributes = new HashSet<tpPaymentATAttribute>();
            tpProposalATAttributes = new HashSet<tpProposalATAttribute>();
            tpPurchaseRequisitionATAttributes = new HashSet<tpPurchaseRequisitionATAttribute>();
            tpPurchaseRequisitionProposalATAttributes = new HashSet<tpPurchaseRequisitionProposalATAttribute>();
            tpTransferPlanATAttributes = new HashSet<tpTransferPlanATAttribute>();
            tpVirementATAttributes = new HashSet<tpVirementATAttribute>();
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
        public bool UseSecondForm { get; set; }

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
        public virtual cdATAttributeType cdATAttributeType { get; set; }

        public virtual ICollection<cdATAttributeDesc> cdATAttributeDescs { get; set; }
        public virtual ICollection<dfBankDefATAttribute> dfBankDefATAttributes { get; set; }
        public virtual ICollection<dfCashDefATAttribute> dfCashDefATAttributes { get; set; }
        public virtual ICollection<dfChequeDefATAttribute> dfChequeDefATAttributes { get; set; }
        public virtual ICollection<dfCreditCardPaymentDefATAttribute> dfCreditCardPaymentDefATAttributes { get; set; }
        public virtual ICollection<dfJournalDefATAttribute> dfJournalDefATAttributes { get; set; }
        public virtual ICollection<prPOSTerminalATAttribute> prPOSTerminalATAttributes { get; set; }
        public virtual ICollection<prProcessATAttribute> prProcessATAttributes { get; set; }
        public virtual ICollection<prWorkPlaceATAttribute> prWorkPlaceATAttributes { get; set; }
        public virtual ICollection<tpAllocationATAttribute> tpAllocationATAttributes { get; set; }
        public virtual ICollection<tpBankATAttribute> tpBankATAttributes { get; set; }
        public virtual ICollection<tpBankCreditATAttribute> tpBankCreditATAttributes { get; set; }
        public virtual ICollection<tpBankPaymentInstructionATAttribute> tpBankPaymentInstructionATAttributes { get; set; }
        public virtual ICollection<tpBankPaymentListATAttribute> tpBankPaymentListATAttributes { get; set; }
        public virtual ICollection<tpCashATAttribute> tpCashATAttributes { get; set; }
        public virtual ICollection<tpChequeATAttribute> tpChequeATAttributes { get; set; }
        public virtual ICollection<tpContractATAttribute> tpContractATAttributes { get; set; }
        public virtual ICollection<tpCreditCardPaymentATAttribute> tpCreditCardPaymentATAttributes { get; set; }
        public virtual ICollection<tpCurrAccBookATAttribute> tpCurrAccBookATAttributes { get; set; }
        public virtual ICollection<tpDebitATAttribute> tpDebitATAttributes { get; set; }
        public virtual ICollection<tpExpenseAccrualATAttribute> tpExpenseAccrualATAttributes { get; set; }
        public virtual ICollection<tpExpenseSlipATAttribute> tpExpenseSlipATAttributes { get; set; }
        public virtual ICollection<tpGiftCardPaymentATAttribute> tpGiftCardPaymentATAttributes { get; set; }
        public virtual ICollection<tpInvoiceATAttribute> tpInvoiceATAttributes { get; set; }
        public virtual ICollection<tpJournalATAttribute> tpJournalATAttributes { get; set; }
        public virtual ICollection<tpOrderATAttribute> tpOrderATAttributes { get; set; }
        public virtual ICollection<tpOtherPaymentATAttribute> tpOtherPaymentATAttributes { get; set; }
        public virtual ICollection<tpPaymentATAttribute> tpPaymentATAttributes { get; set; }
        public virtual ICollection<tpProposalATAttribute> tpProposalATAttributes { get; set; }
        public virtual ICollection<tpPurchaseRequisitionATAttribute> tpPurchaseRequisitionATAttributes { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposalATAttribute> tpPurchaseRequisitionProposalATAttributes { get; set; }
        public virtual ICollection<tpTransferPlanATAttribute> tpTransferPlanATAttributes { get; set; }
        public virtual ICollection<tpVirementATAttribute> tpVirementATAttributes { get; set; }
    }
}
