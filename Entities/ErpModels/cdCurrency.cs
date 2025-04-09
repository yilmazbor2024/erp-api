using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCurrency")]
    public partial class cdCurrency
    {
        public cdCurrency()
        {
            cdAmountRules = new HashSet<cdAmountRule>();
            cdBudgetTypes = new HashSet<cdBudgetType>();
            cdCheques = new HashSet<cdCheque>();
            cdCountrys = new HashSet<cdCountry>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdCurrencyDescs = new HashSet<cdCurrencyDesc>();
            cdDiscountPointTypes = new HashSet<cdDiscountPointType>();
            cdDiscountVouchers = new HashSet<cdDiscountVoucher>();
            cdDiscountVoucherTypes = new HashSet<cdDiscountVoucherType>();
            cdGiftCards = new HashSet<cdGiftCard>();
            cdJobTrainings = new HashSet<cdJobTraining>();
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            cdPriceGroups = new HashSet<cdPriceGroup>();
            cdProposalConfirmationLimits = new HashSet<cdProposalConfirmationLimit>();
            cdProposalConfirmationRules = new HashSet<cdProposalConfirmationRule>();
            cdRequisitions = new HashSet<cdRequisition>();
            cdRequisitionConfirmationLimits = new HashSet<cdRequisitionConfirmationLimit>();
            cdRequisitionConfirmationRules = new HashSet<cdRequisitionConfirmationRule>();
            cdUniFreeTenderTypes = new HashSet<cdUniFreeTenderType>();
            dfAirportExchangeRateWidgetParameterss = new HashSet<dfAirportExchangeRateWidgetParameters>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
            dfMonthlyTurnoverTargets = new HashSet<dfMonthlyTurnoverTarget>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            dfStoreForeignCurrencys = new HashSet<dfStoreForeignCurrency>();
            dfTaxFreeRefundRates = new HashSet<dfTaxFreeRefundRate>();
            hrEmployeeWages = new HashSet<hrEmployeeWage>();
            hrWageGarnishments = new HashSet<hrWageGarnishment>();
            prChequeGLAccss = new HashSet<prChequeGLAccs>();
            prCompanyCreditCardExpenses = new HashSet<prCompanyCreditCardExpense>();
            prCompanyCreditCardUsageFees = new HashSet<prCompanyCreditCardUsageFee>();
            prCurrAccAvailableForeignCurrencyTranss = new HashSet<prCurrAccAvailableForeignCurrencyTrans>();
            prCurrAccBankAccNos = new HashSet<prCurrAccBankAccNo>();
            prCurrAccPersonalInfos = new HashSet<prCurrAccPersonalInfo>();
            prCustomerCreditLimits = new HashSet<prCustomerCreditLimit>();
            prEmployeeShoppingLimits = new HashSet<prEmployeeShoppingLimit>();
            prExportFileIndirectExpenses = new HashSet<prExportFileIndirectExpense>();
            prExportFileInsurances = new HashSet<prExportFileInsurance>();
            prFixedAssetDepreciationInfos = new HashSet<prFixedAssetDepreciationInfo>();
            prFixedAssetExpenses = new HashSet<prFixedAssetExpense>();
            prFixedAssetInsurances = new HashSet<prFixedAssetInsurance>();
            prFixedAssetPurchasess = new HashSet<prFixedAssetPurchases>();
            prFixedAssetSaless = new HashSet<prFixedAssetSales>();
            prGLAccAvailableForeignCurrencyTranss = new HashSet<prGLAccAvailableForeignCurrencyTrans>();
            prImportFileIndirectExpenses = new HashSet<prImportFileIndirectExpense>();
            prImportFileInsurances = new HashSet<prImportFileInsurance>();
            prInsuranceAgencyContributions = new HashSet<prInsuranceAgencyContribution>();
            prItemBasePrices = new HashSet<prItemBasePrice>();
            prNotesGLAccss = new HashSet<prNotesGLAccs>();
            prRequisitionLimits = new HashSet<prRequisitionLimit>();
            prStoreStatuss = new HashSet<prStoreStatus>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
            prUniFreeTenderTypeMappings = new HashSet<prUniFreeTenderTypeMapping>();
            srCashSerialNumbers = new HashSet<srCashSerialNumber>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            tpSupportResolveMaterials = new HashSet<tpSupportResolveMaterial>();
            trAdjustCostHeaders = new HashSet<trAdjustCostHeader>();
            trAgentContractDeservedLines = new HashSet<trAgentContractDeservedLine>();
            trAgentContractPeriodicalLines = new HashSet<trAgentContractPeriodicalLine>();
            trAgentContractVehicles = new HashSet<trAgentContractVehicle>();
            trAgentContractVisitFrequencyLines = new HashSet<trAgentContractVisitFrequencyLine>();
            trAgentPerformanceBonusLines = new HashSet<trAgentPerformanceBonusLine>();
            trBadDebtTransLineAddExpenses = new HashSet<trBadDebtTransLineAddExpense>();
            trBadDebtTransLineInstalments = new HashSet<trBadDebtTransLineInstalment>();
            trBadDebtTransLineResults = new HashSet<trBadDebtTransLineResult>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
            trBankCreditLines = new HashSet<trBankCreditLine>();
            trBankCreditLineCurrencys = new HashSet<trBankCreditLineCurrency>();
            trBankCreditPaymentPlans = new HashSet<trBankCreditPaymentPlan>();
            trBankHeaders = new HashSet<trBankHeader>();
            trBankLines = new HashSet<trBankLine>();
            trBankLineAdditionalCharges = new HashSet<trBankLineAdditionalCharge>();
            trBankLineCurrencys = new HashSet<trBankLineCurrency>();
            trBankPaymentInstructionHeaders = new HashSet<trBankPaymentInstructionHeader>();
            trBankPaymentInstructionLines = new HashSet<trBankPaymentInstructionLine>();
            trBankPaymentListLines = new HashSet<trBankPaymentListLine>();
            trCashHeaders = new HashSet<trCashHeader>();
            trCashLines = new HashSet<trCashLine>();
            trCashLineCurrencys = new HashSet<trCashLineCurrency>();
            trChequeHeaders = new HashSet<trChequeHeader>();
            trChequeLines = new HashSet<trChequeLine>();
            trChequeLineCurrencys = new HashSet<trChequeLineCurrency>();
            trContracts = new HashSet<trContract>();
            trCostCenterDistributionss = new HashSet<trCostCenterDistributions>();
            trCostOfGoodsSoldLines = new HashSet<trCostOfGoodsSoldLine>();
            trCreditCardPaymentHeaders = new HashSet<trCreditCardPaymentHeader>();
            trCreditCardPaymentLines = new HashSet<trCreditCardPaymentLine>();
            trCreditCardPaymentLineCurrencys = new HashSet<trCreditCardPaymentLineCurrency>();
            trCurrAccBooks = new HashSet<trCurrAccBook>();
            trCurrAccBookCurrencys = new HashSet<trCurrAccBookCurrency>();
            trCurrAccReconciliationReports = new HashSet<trCurrAccReconciliationReport>();
            trDebitHeaders = new HashSet<trDebitHeader>();
            trDebitLines = new HashSet<trDebitLine>();
            trDebitLineCurrencys = new HashSet<trDebitLineCurrency>();
            trEmployeeDebits = new HashSet<trEmployeeDebit>();
            trEmployeeDebitCurrencys = new HashSet<trEmployeeDebitCurrency>();
            trEndOfPeriodInventorys = new HashSet<trEndOfPeriodInventory>();
            trExchangeRateHeaders = new HashSet<trExchangeRateHeader>();
            trExchangeRateLines = new HashSet<trExchangeRateLine>();
            trExpenseAccrualHeaders = new HashSet<trExpenseAccrualHeader>();
            trExpenseAccrualLines = new HashSet<trExpenseAccrualLine>();
            trExpenseAccrualLineCurrencys = new HashSet<trExpenseAccrualLineCurrency>();
            trExpenseSlipHeaders = new HashSet<trExpenseSlipHeader>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trExpenseSlipLineCurrencys = new HashSet<trExpenseSlipLineCurrency>();
            trFixedAssetBookHeaders = new HashSet<trFixedAssetBookHeader>();
            trGiftCardPaymentHeaders = new HashSet<trGiftCardPaymentHeader>();
            trGiftCardPaymentLines = new HashSet<trGiftCardPaymentLine>();
            trGiftCardPaymentLineCurrencys = new HashSet<trGiftCardPaymentLineCurrency>();
            trIncentiveHeaders = new HashSet<trIncentiveHeader>();
            trIncentiveLines = new HashSet<trIncentiveLine>();
            trInnerLines = new HashSet<trInnerLine>();
            trInnerOrderLines = new HashSet<trInnerOrderLine>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trInvoiceLineCurrencys = new HashSet<trInvoiceLineCurrency>();
            trItemTestLines = new HashSet<trItemTestLine>();
            trJournalHeaders = new HashSet<trJournalHeader>();
            trJournalLedgerEntryNumbers = new HashSet<trJournalLedgerEntryNumber>();
            trJournalLines = new HashSet<trJournalLine>();
            trJournalLineCurrencys = new HashSet<trJournalLineCurrency>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOrderLines = new HashSet<trOrderLine>();
            trOrderLineCurrencys = new HashSet<trOrderLineCurrency>();
            trOrderPaymentPlans = new HashSet<trOrderPaymentPlan>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
            trOtherPaymentLines = new HashSet<trOtherPaymentLine>();
            trOtherPaymentLineCurrencys = new HashSet<trOtherPaymentLineCurrency>();
            trPaymentHeaders = new HashSet<trPaymentHeader>();
            trPaymentLineCurrencys = new HashSet<trPaymentLineCurrency>();
            trPayrollHeaders = new HashSet<trPayrollHeader>();
            trPayrollLineDeductions = new HashSet<trPayrollLineDeduction>();
            trPayrollLineTallys = new HashSet<trPayrollLineTally>();
            trPriceListHeaders = new HashSet<trPriceListHeader>();
            trPriceListLines = new HashSet<trPriceListLine>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trProposalLines = new HashSet<trProposalLine>();
            trProposalLineCurrencys = new HashSet<trProposalLineCurrency>();
            trPurchaseRequisitionLines = new HashSet<trPurchaseRequisitionLine>();
            trStocks = new HashSet<trStock>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            trSupportRequestLines = new HashSet<trSupportRequestLine>();
            trVendorPriceListHeaders = new HashSet<trVendorPriceListHeader>();
            trVendorPriceListLines = new HashSet<trVendorPriceListLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

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

        public virtual ICollection<cdAmountRule> cdAmountRules { get; set; }
        public virtual ICollection<cdBudgetType> cdBudgetTypes { get; set; }
        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdCountry> cdCountrys { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdCurrencyDesc> cdCurrencyDescs { get; set; }
        public virtual ICollection<cdDiscountPointType> cdDiscountPointTypes { get; set; }
        public virtual ICollection<cdDiscountVoucher> cdDiscountVouchers { get; set; }
        public virtual ICollection<cdDiscountVoucherType> cdDiscountVoucherTypes { get; set; }
        public virtual ICollection<cdGiftCard> cdGiftCards { get; set; }
        public virtual ICollection<cdJobTraining> cdJobTrainings { get; set; }
        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<cdPriceGroup> cdPriceGroups { get; set; }
        public virtual ICollection<cdProposalConfirmationLimit> cdProposalConfirmationLimits { get; set; }
        public virtual ICollection<cdProposalConfirmationRule> cdProposalConfirmationRules { get; set; }
        public virtual ICollection<cdRequisition> cdRequisitions { get; set; }
        public virtual ICollection<cdRequisitionConfirmationLimit> cdRequisitionConfirmationLimits { get; set; }
        public virtual ICollection<cdRequisitionConfirmationRule> cdRequisitionConfirmationRules { get; set; }
        public virtual ICollection<cdUniFreeTenderType> cdUniFreeTenderTypes { get; set; }
        public virtual ICollection<dfAirportExchangeRateWidgetParameters> dfAirportExchangeRateWidgetParameterss { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
        public virtual ICollection<dfMonthlyTurnoverTarget> dfMonthlyTurnoverTargets { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<dfStoreForeignCurrency> dfStoreForeignCurrencys { get; set; }
        public virtual ICollection<dfTaxFreeRefundRate> dfTaxFreeRefundRates { get; set; }
        public virtual ICollection<hrEmployeeWage> hrEmployeeWages { get; set; }
        public virtual ICollection<hrWageGarnishment> hrWageGarnishments { get; set; }
        public virtual ICollection<prChequeGLAccs> prChequeGLAccss { get; set; }
        public virtual ICollection<prCompanyCreditCardExpense> prCompanyCreditCardExpenses { get; set; }
        public virtual ICollection<prCompanyCreditCardUsageFee> prCompanyCreditCardUsageFees { get; set; }
        public virtual ICollection<prCurrAccAvailableForeignCurrencyTrans> prCurrAccAvailableForeignCurrencyTranss { get; set; }
        public virtual ICollection<prCurrAccBankAccNo> prCurrAccBankAccNos { get; set; }
        public virtual ICollection<prCurrAccPersonalInfo> prCurrAccPersonalInfos { get; set; }
        public virtual ICollection<prCustomerCreditLimit> prCustomerCreditLimits { get; set; }
        public virtual ICollection<prEmployeeShoppingLimit> prEmployeeShoppingLimits { get; set; }
        public virtual ICollection<prExportFileIndirectExpense> prExportFileIndirectExpenses { get; set; }
        public virtual ICollection<prExportFileInsurance> prExportFileInsurances { get; set; }
        public virtual ICollection<prFixedAssetDepreciationInfo> prFixedAssetDepreciationInfos { get; set; }
        public virtual ICollection<prFixedAssetExpense> prFixedAssetExpenses { get; set; }
        public virtual ICollection<prFixedAssetInsurance> prFixedAssetInsurances { get; set; }
        public virtual ICollection<prFixedAssetPurchases> prFixedAssetPurchasess { get; set; }
        public virtual ICollection<prFixedAssetSales> prFixedAssetSaless { get; set; }
        public virtual ICollection<prGLAccAvailableForeignCurrencyTrans> prGLAccAvailableForeignCurrencyTranss { get; set; }
        public virtual ICollection<prImportFileIndirectExpense> prImportFileIndirectExpenses { get; set; }
        public virtual ICollection<prImportFileInsurance> prImportFileInsurances { get; set; }
        public virtual ICollection<prInsuranceAgencyContribution> prInsuranceAgencyContributions { get; set; }
        public virtual ICollection<prItemBasePrice> prItemBasePrices { get; set; }
        public virtual ICollection<prNotesGLAccs> prNotesGLAccss { get; set; }
        public virtual ICollection<prRequisitionLimit> prRequisitionLimits { get; set; }
        public virtual ICollection<prStoreStatus> prStoreStatuss { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
        public virtual ICollection<prUniFreeTenderTypeMapping> prUniFreeTenderTypeMappings { get; set; }
        public virtual ICollection<srCashSerialNumber> srCashSerialNumbers { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<tpSupportResolveMaterial> tpSupportResolveMaterials { get; set; }
        public virtual ICollection<trAdjustCostHeader> trAdjustCostHeaders { get; set; }
        public virtual ICollection<trAgentContractDeservedLine> trAgentContractDeservedLines { get; set; }
        public virtual ICollection<trAgentContractPeriodicalLine> trAgentContractPeriodicalLines { get; set; }
        public virtual ICollection<trAgentContractVehicle> trAgentContractVehicles { get; set; }
        public virtual ICollection<trAgentContractVisitFrequencyLine> trAgentContractVisitFrequencyLines { get; set; }
        public virtual ICollection<trAgentPerformanceBonusLine> trAgentPerformanceBonusLines { get; set; }
        public virtual ICollection<trBadDebtTransLineAddExpense> trBadDebtTransLineAddExpenses { get; set; }
        public virtual ICollection<trBadDebtTransLineInstalment> trBadDebtTransLineInstalments { get; set; }
        public virtual ICollection<trBadDebtTransLineResult> trBadDebtTransLineResults { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
        public virtual ICollection<trBankCreditLine> trBankCreditLines { get; set; }
        public virtual ICollection<trBankCreditLineCurrency> trBankCreditLineCurrencys { get; set; }
        public virtual ICollection<trBankCreditPaymentPlan> trBankCreditPaymentPlans { get; set; }
        public virtual ICollection<trBankHeader> trBankHeaders { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trBankLineAdditionalCharge> trBankLineAdditionalCharges { get; set; }
        public virtual ICollection<trBankLineCurrency> trBankLineCurrencys { get; set; }
        public virtual ICollection<trBankPaymentInstructionHeader> trBankPaymentInstructionHeaders { get; set; }
        public virtual ICollection<trBankPaymentInstructionLine> trBankPaymentInstructionLines { get; set; }
        public virtual ICollection<trBankPaymentListLine> trBankPaymentListLines { get; set; }
        public virtual ICollection<trCashHeader> trCashHeaders { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
        public virtual ICollection<trCashLineCurrency> trCashLineCurrencys { get; set; }
        public virtual ICollection<trChequeHeader> trChequeHeaders { get; set; }
        public virtual ICollection<trChequeLine> trChequeLines { get; set; }
        public virtual ICollection<trChequeLineCurrency> trChequeLineCurrencys { get; set; }
        public virtual ICollection<trContract> trContracts { get; set; }
        public virtual ICollection<trCostCenterDistributions> trCostCenterDistributionss { get; set; }
        public virtual ICollection<trCostOfGoodsSoldLine> trCostOfGoodsSoldLines { get; set; }
        public virtual ICollection<trCreditCardPaymentHeader> trCreditCardPaymentHeaders { get; set; }
        public virtual ICollection<trCreditCardPaymentLine> trCreditCardPaymentLines { get; set; }
        public virtual ICollection<trCreditCardPaymentLineCurrency> trCreditCardPaymentLineCurrencys { get; set; }
        public virtual ICollection<trCurrAccBook> trCurrAccBooks { get; set; }
        public virtual ICollection<trCurrAccBookCurrency> trCurrAccBookCurrencys { get; set; }
        public virtual ICollection<trCurrAccReconciliationReport> trCurrAccReconciliationReports { get; set; }
        public virtual ICollection<trDebitHeader> trDebitHeaders { get; set; }
        public virtual ICollection<trDebitLine> trDebitLines { get; set; }
        public virtual ICollection<trDebitLineCurrency> trDebitLineCurrencys { get; set; }
        public virtual ICollection<trEmployeeDebit> trEmployeeDebits { get; set; }
        public virtual ICollection<trEmployeeDebitCurrency> trEmployeeDebitCurrencys { get; set; }
        public virtual ICollection<trEndOfPeriodInventory> trEndOfPeriodInventorys { get; set; }
        public virtual ICollection<trExchangeRateHeader> trExchangeRateHeaders { get; set; }
        public virtual ICollection<trExchangeRateLine> trExchangeRateLines { get; set; }
        public virtual ICollection<trExpenseAccrualHeader> trExpenseAccrualHeaders { get; set; }
        public virtual ICollection<trExpenseAccrualLine> trExpenseAccrualLines { get; set; }
        public virtual ICollection<trExpenseAccrualLineCurrency> trExpenseAccrualLineCurrencys { get; set; }
        public virtual ICollection<trExpenseSlipHeader> trExpenseSlipHeaders { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trExpenseSlipLineCurrency> trExpenseSlipLineCurrencys { get; set; }
        public virtual ICollection<trFixedAssetBookHeader> trFixedAssetBookHeaders { get; set; }
        public virtual ICollection<trGiftCardPaymentHeader> trGiftCardPaymentHeaders { get; set; }
        public virtual ICollection<trGiftCardPaymentLine> trGiftCardPaymentLines { get; set; }
        public virtual ICollection<trGiftCardPaymentLineCurrency> trGiftCardPaymentLineCurrencys { get; set; }
        public virtual ICollection<trIncentiveHeader> trIncentiveHeaders { get; set; }
        public virtual ICollection<trIncentiveLine> trIncentiveLines { get; set; }
        public virtual ICollection<trInnerLine> trInnerLines { get; set; }
        public virtual ICollection<trInnerOrderLine> trInnerOrderLines { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trInvoiceLineCurrency> trInvoiceLineCurrencys { get; set; }
        public virtual ICollection<trItemTestLine> trItemTestLines { get; set; }
        public virtual ICollection<trJournalHeader> trJournalHeaders { get; set; }
        public virtual ICollection<trJournalLedgerEntryNumber> trJournalLedgerEntryNumbers { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
        public virtual ICollection<trJournalLineCurrency> trJournalLineCurrencys { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trOrderLineCurrency> trOrderLineCurrencys { get; set; }
        public virtual ICollection<trOrderPaymentPlan> trOrderPaymentPlans { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
        public virtual ICollection<trOtherPaymentLine> trOtherPaymentLines { get; set; }
        public virtual ICollection<trOtherPaymentLineCurrency> trOtherPaymentLineCurrencys { get; set; }
        public virtual ICollection<trPaymentHeader> trPaymentHeaders { get; set; }
        public virtual ICollection<trPaymentLineCurrency> trPaymentLineCurrencys { get; set; }
        public virtual ICollection<trPayrollHeader> trPayrollHeaders { get; set; }
        public virtual ICollection<trPayrollLineDeduction> trPayrollLineDeductions { get; set; }
        public virtual ICollection<trPayrollLineTally> trPayrollLineTallys { get; set; }
        public virtual ICollection<trPriceListHeader> trPriceListHeaders { get; set; }
        public virtual ICollection<trPriceListLine> trPriceListLines { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trProposalLineCurrency> trProposalLineCurrencys { get; set; }
        public virtual ICollection<trPurchaseRequisitionLine> trPurchaseRequisitionLines { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<trSupportRequestLine> trSupportRequestLines { get; set; }
        public virtual ICollection<trVendorPriceListHeader> trVendorPriceListHeaders { get; set; }
        public virtual ICollection<trVendorPriceListLine> trVendorPriceListLines { get; set; }
    }
}
