using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prProcessInfo")]
    public partial class prProcessInfo
    {
        public prProcessInfo()
        {
        }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [Required]
        public bool UseApproval { get; set; }

        [Required]
        public bool UseGLType { get; set; }

        [Required]
        public bool ForceGLType { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public bool ForceCostCenter { get; set; }

        [Required]
        public short TaxExemptionCode { get; set; }

        [Required]
        public bool UseTaxExemption { get; set; }

        [Required]
        public bool UsePaymentPlanByItem { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [Required]
        public bool ForcePaymentPlanBasePrice { get; set; }

        [Required]
        public bool UseCreditCartPaymentPlanOnPOS { get; set; }

        [Required]
        public bool UseReturnReasonProductHierarcyFilter { get; set; }

        [Required]
        public bool UseDueDateFormula { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DueDateFormulaCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DOVCode { get; set; }

        [Required]
        public byte RetailInstallmentSaleAgeLimit { get; set; }

        [Required]
        public byte CreditLimitControlProcessFlow { get; set; }

        [Required]
        public bool AllowCreditLimitExceeded { get; set; }

        [Required]
        public double CreditLimitExceedanceRate { get; set; }

        [Required]
        public bool AllowToSaleHaveArrears { get; set; }

        [Required]
        public bool AllowToSaleHaveArrearsForVip { get; set; }

        [Required]
        public bool ApplyToLateChargeForArrears { get; set; }

        [Required]
        public bool LateChargeMayNotBeAppliedIfAllArrearsSelected { get; set; }

        [Required]
        public bool NoApplyToLateChargeForArrearsVip { get; set; }

        [Required]
        public byte MinLatencyDay { get; set; }

        [Required]
        public decimal MinLatencyAmount { get; set; }

        [Required]
        public double MonthlyLateChargeRate { get; set; }

        [Required]
        public double MinLateChargeRate { get; set; }

        [Required]
        public bool ForceGuarantor { get; set; }

        [Required]
        public decimal DefaultLocalCreditLimit { get; set; }

        [Required]
        public bool UseCreditableConfirmationSurvey { get; set; }

        [Required]
        public bool ForceCreditableConfirmation { get; set; }

        [Required]
        public bool CanReceivePaymentUnconfirmedOrder { get; set; }

        [Required]
        public int InstallmentRoundingDigit { get; set; }

        [Required]
        public decimal AcceptableMissingPaymentAmount { get; set; }

        [Required]
        public bool ForcePaymentSortByInstallmentDate { get; set; }

        [Required]
        public bool AllowDuplicateSeriesNumber { get; set; }

        [Required]
        public bool IsInclutedVat { get; set; }

        [Required]
        public bool IsTransDirectlyPostToGL { get; set; }

        [Required]
        public bool IsOpenJournalAfterSave { get; set; }

        [Required]
        public byte DaysDiffInvoiceAndShipment { get; set; }

        [Required]
        public bool MustBeSameMonthInvoiceAndShipment { get; set; }

        [Required]
        public int OrderLeadTime { get; set; }

        [Required]
        public bool UseQty2 { get; set; }

        [Required]
        public bool UseToleranceQty { get; set; }

        [Required]
        public double SurplusOrderQtyToleranceRate { get; set; }

        [Required]
        public bool UseOrderDeliveryLocations { get; set; }

        [Required]
        public bool CanEditOtherStoreOrders { get; set; }

        [Required]
        public bool CommAddressRequiredOnProductReserveProcess { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequiredCommunicationTypeCode { get; set; }

        [Required]
        public bool ApplyDiscountOfferOrderProcess { get; set; }

        [Required]
        public bool EarnPointAndVoucherOrderProcess { get; set; }

        [Required]
        public bool NoApplyDiscountOfferOrderBaseInvoice { get; set; }

        [Required]
        public bool AskDiscountOfferOrderProcess { get; set; }

        [Required]
        public bool ApplyVoucherAndPointDiscountsAfterPayment { get; set; }

        [Required]
        public bool ForeignLocalCurrAccControl { get; set; }

        [Required]
        public bool AllowZeroAmountInvoice { get; set; }

        [Required]
        public bool GetBasePriceWhenPriceCantfound { get; set; }

        [Required]
        public bool WarnZeroPriceOnOrder { get; set; }

        [Required]
        public bool WarnZeroPriceOnInvoice { get; set; }

        [Required]
        public bool CanSellProductWithLessPrice { get; set; }

        [Required]
        public bool AllowDiffPriceOnChangeVariant { get; set; }

        [Required]
        public bool ForceWarehouseOnOrders { get; set; }

        [Required]
        public bool ForceDescriptionOnOrders { get; set; }

        [Required]
        public bool ForceLineDescriptionOnOrders { get; set; }

        [Required]
        public bool WarnRepeatedItemCodeOnOrders { get; set; }

        [Required]
        public bool CombineGLAccInJournal { get; set; }

        [Required]
        public bool SeparateDiscountInJournal { get; set; }

        [Required]
        public bool SeparateReturnDiscountInJournal { get; set; }

        [Required]
        public bool CombineDiscountGLAccInJournal { get; set; }

        [Required]
        public bool CombineVatGLAccInJournal { get; set; }

        [Required]
        public bool UseEarlyPaymentDiscount { get; set; }

        [Required]
        public double DefaultEarlyPaymentDiscount { get; set; }

        [Required]
        public bool ApplyEPDAllInstalmentsClosed { get; set; }

        [Required]
        public double MaxEarlyPaymentDiscount { get; set; }

        [Required]
        public byte MinEarlyPaymentDay { get; set; }

        [Required]
        public bool UseSalesDateForReplacement { get; set; }

        [Required]
        public bool ForceSamePaymentForReplacement { get; set; }

        [Required]
        public bool CanEditAmountForReplacement { get; set; }

        [Required]
        public bool CanSearchCreditVoucher { get; set; }

        [Required]
        public bool IgnorOverageOfCash { get; set; }

        [Required]
        public bool UseSeriesCode { get; set; }

        [Required]
        public bool UseSeriesCodeOnEInvoiceShipments { get; set; }

        [Required]
        public bool RequiredSalesPerson { get; set; }

        [Required]
        public bool EnablePriceControlSystem { get; set; }

        [Required]
        public bool CreateGiftCardSalesAsEInvoice { get; set; }

        [Required]
        public bool CreateGiftCardSalesAsEArchiveInvoice { get; set; }

        [Required]
        public bool CanEditInvoicedShipment { get; set; }

        [Required]
        public bool UseEArchiveInvoiceConfirmation { get; set; }

        [Required]
        public bool UseDebitVoucher { get; set; }

        [Required]
        public bool CreateSequantialDebitVoucher { get; set; }

        [Required]
        public bool DisplayAdvanceInfoOnPayment { get; set; }

        [Required]
        public bool DisplayCreditVoucherInfoOnPayment { get; set; }

        [Required]
        public bool CreateCreditVoucherAsBearer { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LogisticsDiffWarehouseCode { get; set; }

        [Required]
        public bool UseRetailCustomerConditionalRequiredFields { get; set; }

        [Required]
        public bool CanEditProposalBasedTransactions { get; set; }

        [Required]
        public bool UseProposalPriceForInvoiceAndOrder { get; set; }

        [Required]
        public bool CanReduceProposalPriceForInvoiceAndOrder { get; set; }

        [Required]
        public bool CanIncreaseProposalPriceForInvoiceAndOrder { get; set; }

        [Required]
        public bool UseOnlyCompanyBrandedProducts { get; set; }

        [Required]
        public bool UseSerialProNumberOnOpticalOrders { get; set; }

        [Required]
        public bool StoreCanOnlyReturnOwnInvoices { get; set; }

        [Required]
        public bool ShowCustomizedItemInformationOnPos { get; set; }

        [Required]
        public bool IncludeZeroPriceLinesInSupportInvoices { get; set; }

        [Required]
        public bool UseOrderPriceOnInvoiceOrderBase { get; set; }

        [Required]
        public bool UseOrderPriceOnShipmentOrderBase { get; set; }

        [Required]
        public bool UseInstallmentCountRules { get; set; }

        [Required]
        public bool StoreCanOnlyChangeVariantOwnInvoices { get; set; }

        [Required]
        public bool DoNotAllowDifferentTDiscountsOnOrderBasedInvoices { get; set; }

        [Required]
        public bool CheckBadDebtStatusOnGuarantorEntry { get; set; }

        [Required]
        public bool CanInputWithNewBatchInReturnTransactions { get; set; }

        [Required]
        public bool CannotEditSalesPerson { get; set; }

        [Required]
        public bool UseLastPriceOnReturnInvoices { get; set; }

        [Required]
        public bool CreateDisposibleCreditVouchers { get; set; }

        [Required]
        public bool IsReturnReasonRequired { get; set; }

        [Required]
        public bool WarnNegativeDiscountPoints { get; set; }

        [Required]
        public bool AskReturnTypeOnCompanyReturnInvoices { get; set; }

        [Required]
        public bool UseDefaultExpenseTypeForExpensePurchase { get; set; }

        [Required]
        public bool DiscountPointsWillBeAddedToNewCustomerOnReturnGiftProduct { get; set; }

        [Required]
        public bool WarnRepeatedItemLotOnOrders { get; set; }

        [Required]
        public bool UseSelectedSubCurrAccAddress { get; set; }

        [Required]
        public byte IncompleteDownPaymentDistributionTypeCode { get; set; }

        [Required]
        public bool FirstInstallmentChangedSetAllInstallmentsSameAmount { get; set; }

        [Required]
        public bool FixInstallmentCountOnEditPaymentPlan { get; set; }

        [Required]
        public bool OrderNumberIsRequiredForOrderBasedTransImportLines { get; set; }

        [Required]
        public bool UseConfirmationRequiredProductGroups { get; set; }

        [Required]
        public bool ReturnShipmentNotAllowedForInvoicedOrder { get; set; }

        [Required]
        public bool SendInvoiceByEmailIsVisible { get; set; }

        [Required]
        public bool SendInvoiceByEmailIsSelected { get; set; }

        [Required]
        public bool SendInvoiceByEmailIsRequired { get; set; }

        [Required]
        public bool SendInvoiceBySMSIsVisible { get; set; }

        [Required]
        public bool SendInvoiceBySMSIsSelected { get; set; }

        [Required]
        public bool SendInvoiceBySMSIsRequired { get; set; }

        [Required]
        public bool UseRetailCustomerSurvey { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RetailCustomerSurveyCode { get; set; }

        [Required]
        public short RetailCustomerSurveryExpirePeriod { get; set; }

        [Required]
        public bool UseAuditorSurvey { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AuditorSurveyCode { get; set; }

        [Required]
        public bool IsPasswordRequiredOnCancelTransaction { get; set; }

        [Required]
        public bool IsPasswordRequiredOnCancelItem { get; set; }

        [Required]
        public bool IsPasswordRequiredOnReturnTransaction { get; set; }

        [Required]
        public bool UseFixedCancelPassword { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CancelPassword { get; set; }

        [Required]
        public bool SendCancelPasswordSMSToStoreContact { get; set; }

        [Required]
        public bool UseBatchBarcodeInventoryForLineQty { get; set; }

        [Required]
        public bool ShowUserWarningsAsPopUpOnInstallmentPayment { get; set; }

        [Required]
        public bool MinLateChargeRateCanBeChangedWithPassword { get; set; }

        [Required]
        public bool UseFixedLateChargeRatePassword { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FixedLateChargeRatePassword { get; set; }

        [Required]
        public bool SendLateChargeRatePasswordSMSToStoreContact { get; set; }

        [Required]
        public bool SendLateChargeRatePasswordToDefinedContacts { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string DefinedContactsForLateChargeRatePassword { get; set; }

        [Required]
        public bool DispOrderConfirmationNotRequiredForVIPCustomers { get; set; }

        [Required]
        public bool PackageShippingConfirmationNotRequiredForVIPCustomers { get; set; }

        [Required]
        public bool OpenPostingEInvoicesAfterEInvoiceCreated { get; set; }

        [Required]
        public bool OpenPostingEShipmentsAfterEShipmentCreated { get; set; }

        [Required]
        public bool IsPasswordRequiredOnReplacementTransaction { get; set; }

        [Required]
        public bool NewInstallmentCountValidForGreaterInstallmentCounts { get; set; }

        [Required]
        public bool ForcePurchasePlanPrice { get; set; }

        [Required]
        public bool SetBatchCode { get; set; }

        [Required]
        public bool FilterCompanyCodeOnApproveStoreReturns { get; set; }

        [Required]
        public bool UseToWarehouseFilter { get; set; }

        [Required]
        public bool ApplyRoundingDiffLastToInstallment { get; set; }

        [Required]
        public bool ApplyEPDRemainingBalanceClosed { get; set; }

        [Required]
        public bool CanEditInvoiceWithPaymentRecord { get; set; }

        [Required]
        public bool OpenPostingEShipmentsAfterEShipmentCreatedOnStore { get; set; }

        [Required]
        public bool CanAcceptUnDeliveredEShipments { get; set; }

        [Required]
        public bool ApplyEPDAllInstalmentsClosedSkipMinLatencyDay { get; set; }

        [Required]
        public bool SeparateDiscountInJournalTaxIncluded { get; set; }

        [Required]
        public bool UseAttributeProductOnChangeVariant { get; set; }

        [Required]
        public byte AttributeProductOnChangeVariant { get; set; }

        [Required]
        public bool ReturnDiscountVoucherChangeVariantAndPartialLines { get; set; }

        [Required]
        public bool ApplyMaxOrderLeadTimeAllLines { get; set; }

        [Required]
        public bool DiscountPointsFirstValidDateEarnedPoint { get; set; }

        [Required]
        public short DiscountPointsLastValidDateNDay { get; set; }

        [Required]
        public bool LeadTimeOnlyDelay { get; set; }

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
        public virtual cdGLType cdGLType { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual bsItemType bsItemType { get; set; }
        public virtual cdDOV cdDOV { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsIncompleteDownPaymentDistributionType bsIncompleteDownPaymentDistributionType { get; set; }
        public virtual cdPaymentPlan cdPaymentPlan { get; set; }
        public virtual cdDueDateFormula cdDueDateFormula { get; set; }
        public virtual cdCommunicationType cdCommunicationType { get; set; }
        public virtual bsTaxExemption bsTaxExemption { get; set; }

    }
}
