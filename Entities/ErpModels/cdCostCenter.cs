using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCostCenter")]
    public partial class cdCostCenter
    {
        public cdCostCenter()
        {
            cdCostCenterDescs = new HashSet<cdCostCenterDesc>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            prBankPOSGLAccss = new HashSet<prBankPOSGLAccs>();
            prCompanyCostCenters = new HashSet<prCompanyCostCenter>();
            prCostCenterAttributes = new HashSet<prCostCenterAttribute>();
            prCostCenterHierarchys = new HashSet<prCostCenterHierarchy>();
            prCurrAccOnlineBanks = new HashSet<prCurrAccOnlineBank>();
            prFixedAssetDepreciationInfos = new HashSet<prFixedAssetDepreciationInfo>();
            prGLAccOnlineBanks = new HashSet<prGLAccOnlineBank>();
            prItemCostCenters = new HashSet<prItemCostCenter>();
            prItemCostCenterRatess = new HashSet<prItemCostCenterRates>();
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
            prOnlineBankWebServiceBankInternalParameters = new HashSet<prOnlineBankWebServiceBankInternalParameter>();
            prStoreBankPOSGLAccss = new HashSet<prStoreBankPOSGLAccs>();
            prSubCurrAccOnlineBanks = new HashSet<prSubCurrAccOnlineBank>();
            prWorkPlaceGLAccss = new HashSet<prWorkPlaceGLAccs>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
            trBankCreditLines = new HashSet<trBankCreditLine>();
            trBankLines = new HashSet<trBankLine>();
            trCashLines = new HashSet<trCashLine>();
            trCostCenterDistributionss = new HashSet<trCostCenterDistributions>();
            trExpenseAccrualLines = new HashSet<trExpenseAccrualLine>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trFixedAssetBookLines = new HashSet<trFixedAssetBookLine>();
            trInnerLines = new HashSet<trInnerLine>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trJournalLines = new HashSet<trJournalLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalLines = new HashSet<trProposalLine>();
            trPurchaseRequisitionLines = new HashSet<trPurchaseRequisitionLine>();
            zpOnlineBankTransactions = new HashSet<zpOnlineBankTransaction>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

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

        public virtual ICollection<cdCostCenterDesc> cdCostCenterDescs { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<prBankPOSGLAccs> prBankPOSGLAccss { get; set; }
        public virtual ICollection<prCompanyCostCenter> prCompanyCostCenters { get; set; }
        public virtual ICollection<prCostCenterAttribute> prCostCenterAttributes { get; set; }
        public virtual ICollection<prCostCenterHierarchy> prCostCenterHierarchys { get; set; }
        public virtual ICollection<prCurrAccOnlineBank> prCurrAccOnlineBanks { get; set; }
        public virtual ICollection<prFixedAssetDepreciationInfo> prFixedAssetDepreciationInfos { get; set; }
        public virtual ICollection<prGLAccOnlineBank> prGLAccOnlineBanks { get; set; }
        public virtual ICollection<prItemCostCenter> prItemCostCenters { get; set; }
        public virtual ICollection<prItemCostCenterRates> prItemCostCenterRatess { get; set; }
        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
        public virtual ICollection<prOnlineBankWebServiceBankInternalParameter> prOnlineBankWebServiceBankInternalParameters { get; set; }
        public virtual ICollection<prStoreBankPOSGLAccs> prStoreBankPOSGLAccss { get; set; }
        public virtual ICollection<prSubCurrAccOnlineBank> prSubCurrAccOnlineBanks { get; set; }
        public virtual ICollection<prWorkPlaceGLAccs> prWorkPlaceGLAccss { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
        public virtual ICollection<trBankCreditLine> trBankCreditLines { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
        public virtual ICollection<trCostCenterDistributions> trCostCenterDistributionss { get; set; }
        public virtual ICollection<trExpenseAccrualLine> trExpenseAccrualLines { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trFixedAssetBookLine> trFixedAssetBookLines { get; set; }
        public virtual ICollection<trInnerLine> trInnerLines { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trPurchaseRequisitionLine> trPurchaseRequisitionLines { get; set; }
        public virtual ICollection<zpOnlineBankTransaction> zpOnlineBankTransactions { get; set; }
    }
}
