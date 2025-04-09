using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdGLType")]
    public partial class cdGLType
    {
        public cdGLType()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdGLAccs = new HashSet<cdGLAcc>();
            cdGLTypeDescs = new HashSet<cdGLTypeDesc>();
            prBankPOSAccountss = new HashSet<prBankPOSAccounts>();
            prBankPOSGLAccss = new HashSet<prBankPOSGLAccs>();
            prCurrAccOnlineBanks = new HashSet<prCurrAccOnlineBank>();
            prFixedAssetDepreciationInfos = new HashSet<prFixedAssetDepreciationInfo>();
            prGLAccOnlineBanks = new HashSet<prGLAccOnlineBank>();
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
            prOnlineBankWebServiceBankInternalParameters = new HashSet<prOnlineBankWebServiceBankInternalParameter>();
            prProcessInfos = new HashSet<prProcessInfo>();
            prStoreBankPOSAccountss = new HashSet<prStoreBankPOSAccounts>();
            prStoreBankPOSGLAccss = new HashSet<prStoreBankPOSGLAccs>();
            prSubCurrAccOnlineBanks = new HashSet<prSubCurrAccOnlineBank>();
            prWorkPlaceGLAccss = new HashSet<prWorkPlaceGLAccs>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
            trBankCreditLines = new HashSet<trBankCreditLine>();
            trBankHeaders = new HashSet<trBankHeader>();
            trBankLines = new HashSet<trBankLine>();
            trBankPaymentInstructionLines = new HashSet<trBankPaymentInstructionLine>();
            trBankPaymentListLines = new HashSet<trBankPaymentListLine>();
            trCashHeaders = new HashSet<trCashHeader>();
            trCashLines = new HashSet<trCashLine>();
            trChequeHeaders = new HashSet<trChequeHeader>();
            trContracts = new HashSet<trContract>();
            trCreditCardPaymentHeaders = new HashSet<trCreditCardPaymentHeader>();
            trDebitHeaders = new HashSet<trDebitHeader>();
            trExpenseAccrualLines = new HashSet<trExpenseAccrualLine>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trFixedAssetBookLines = new HashSet<trFixedAssetBookLine>();
            trGiftCardPaymentHeaders = new HashSet<trGiftCardPaymentHeader>();
            trInnerLines = new HashSet<trInnerLine>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trJournalLines = new HashSet<trJournalLine>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
            trPaymentHeaders = new HashSet<trPaymentHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trProposalLines = new HashSet<trProposalLine>();
            trVirementHeaders = new HashSet<trVirementHeader>();
            trVirementLines = new HashSet<trVirementLine>();
            zpOnlineBankCreditCardPaymentTransactions = new HashSet<zpOnlineBankCreditCardPaymentTransaction>();
            zpOnlineBankTransactions = new HashSet<zpOnlineBankTransaction>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

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

        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdGLAcc> cdGLAccs { get; set; }
        public virtual ICollection<cdGLTypeDesc> cdGLTypeDescs { get; set; }
        public virtual ICollection<prBankPOSAccounts> prBankPOSAccountss { get; set; }
        public virtual ICollection<prBankPOSGLAccs> prBankPOSGLAccss { get; set; }
        public virtual ICollection<prCurrAccOnlineBank> prCurrAccOnlineBanks { get; set; }
        public virtual ICollection<prFixedAssetDepreciationInfo> prFixedAssetDepreciationInfos { get; set; }
        public virtual ICollection<prGLAccOnlineBank> prGLAccOnlineBanks { get; set; }
        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
        public virtual ICollection<prOnlineBankWebServiceBankInternalParameter> prOnlineBankWebServiceBankInternalParameters { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
        public virtual ICollection<prStoreBankPOSAccounts> prStoreBankPOSAccountss { get; set; }
        public virtual ICollection<prStoreBankPOSGLAccs> prStoreBankPOSGLAccss { get; set; }
        public virtual ICollection<prSubCurrAccOnlineBank> prSubCurrAccOnlineBanks { get; set; }
        public virtual ICollection<prWorkPlaceGLAccs> prWorkPlaceGLAccss { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
        public virtual ICollection<trBankCreditLine> trBankCreditLines { get; set; }
        public virtual ICollection<trBankHeader> trBankHeaders { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trBankPaymentInstructionLine> trBankPaymentInstructionLines { get; set; }
        public virtual ICollection<trBankPaymentListLine> trBankPaymentListLines { get; set; }
        public virtual ICollection<trCashHeader> trCashHeaders { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
        public virtual ICollection<trChequeHeader> trChequeHeaders { get; set; }
        public virtual ICollection<trContract> trContracts { get; set; }
        public virtual ICollection<trCreditCardPaymentHeader> trCreditCardPaymentHeaders { get; set; }
        public virtual ICollection<trDebitHeader> trDebitHeaders { get; set; }
        public virtual ICollection<trExpenseAccrualLine> trExpenseAccrualLines { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trFixedAssetBookLine> trFixedAssetBookLines { get; set; }
        public virtual ICollection<trGiftCardPaymentHeader> trGiftCardPaymentHeaders { get; set; }
        public virtual ICollection<trInnerLine> trInnerLines { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
        public virtual ICollection<trPaymentHeader> trPaymentHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trVirementHeader> trVirementHeaders { get; set; }
        public virtual ICollection<trVirementLine> trVirementLines { get; set; }
        public virtual ICollection<zpOnlineBankCreditCardPaymentTransaction> zpOnlineBankCreditCardPaymentTransactions { get; set; }
        public virtual ICollection<zpOnlineBankTransaction> zpOnlineBankTransactions { get; set; }
    }
}
