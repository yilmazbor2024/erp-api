using Microsoft.EntityFrameworkCore;
using ErpMobile.Api.Entities.ErpModels;

namespace ErpMobile.Api.Data.Context
{
    public class ErpDbContext : DbContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options) { }

        // Audit Tables
        public DbSet<auAirConnTransactionInfo> auAirConnTransactionInfo { get; set; }
        public DbSet<auAirConnZInfo> auAirConnZInfo { get; set; }
        public DbSet<auAllocationTrace> auAllocationTrace { get; set; }
        public DbSet<auAngolaSVCIntegrationInfo> auAngolaSVCIntegrationInfo { get; set; }
        public DbSet<auBankPermit> auBankPermit { get; set; }
        public DbSet<auBasefyServiceLog> auBasefyServiceLog { get; set; }
        public DbSet<auBasePricePermit> auBasePricePermit { get; set; }
        public DbSet<auCancelRetailTransactions> auCancelRetailTransactions { get; set; }
        public DbSet<auCardColumnPermit> auCardColumnPermit { get; set; }
        public DbSet<auCardElementPermit> auCardElementPermit { get; set; }
        public DbSet<auCardElementRequiredKey> auCardElementRequiredKey { get; set; }
        public DbSet<auCardPermit> auCardPermit { get; set; }
        public DbSet<auCashPermit> auCashPermit { get; set; }
        public DbSet<auChequeDeny> auChequeDeny { get; set; }
        public DbSet<auChequeDenyTrace> auChequeDenyTrace { get; set; }
        public DbSet<auChequePermit> auChequePermit { get; set; }
        public DbSet<auCreditCardPaymentPermit> auCreditCardPaymentPermit { get; set; }
        public DbSet<auCurrAccBadDebtStatusTrace> auCurrAccBadDebtStatusTrace { get; set; }
        public DbSet<auCustomerCompanyBrandAttributeTrace> auCustomerCompanyBrandAttributeTrace { get; set; }
        public DbSet<auCustomizedDiscountEngineServiceGetReturnableItemsLog> auCustomizedDiscountEngineServiceGetReturnableItemsLog { get; set; }
        public DbSet<auCustomizedDiscountEngineServiceLog> auCustomizedDiscountEngineServiceLog { get; set; }
        public DbSet<auCustomQ3XFFPTransactionInfo> auCustomQ3XFFPTransactionInfo { get; set; }
        public DbSet<auCustomsServiceLog> auCustomsServiceLog { get; set; }
        public DbSet<auCustomTablePermit> auCustomTablePermit { get; set; }
        public DbSet<auDataTransferLog> auDataTransferLog { get; set; }
        public DbSet<auDataTransferMissingRecordsTrace> auDataTransferMissingRecordsTrace { get; set; }
        public DbSet<auDataTransferTrace> auDataTransferTrace { get; set; }
        public DbSet<auDebitPermit> auDebitPermit { get; set; }
        public DbSet<auDunadPFRTransactionInfo> auDunadPFRTransactionInfo { get; set; }
        public DbSet<auETTNGuuduuTransactionInfo> auETTNGuuduuTransactionInfo { get; set; }
        public DbSet<auEuromsgSentAccountTrace> auEuromsgSentAccountTrace { get; set; }
        public DbSet<auExpenseSlipPermit> auExpenseSlipPermit { get; set; }
        public DbSet<auFastPayServiceLog> auFastPayServiceLog { get; set; }
        public DbSet<auFiscalPrinterFaultTrace> auFiscalPrinterFaultTrace { get; set; }
        public DbSet<auFiscalPrinterPaymentInfo> auFiscalPrinterPaymentInfo { get; set; }
        public DbSet<auFixEInvoiceStatusCodes> auFixEInvoiceStatusCodes { get; set; }
        public DbSet<auFixPaymentTrace> auFixPaymentTrace { get; set; }
        public DbSet<auGettingDataTransferTraceHeader> auGettingDataTransferTraceHeader { get; set; }
        public DbSet<auGettingDataTransferTraceLine> auGettingDataTransferTraceLine { get; set; }
        public DbSet<auGlobalDefaultTrace> auGlobalDefaultTrace { get; set; }
        public DbSet<auInnerProcessPermit> auInnerProcessPermit { get; set; }
        public DbSet<auInnerTrace> auInnerTrace { get; set; }
        public DbSet<auInteractiveSMSTrace> auInteractiveSMSTrace { get; set; }
        public DbSet<auInvoiceDufrySendStatus> auInvoiceDufrySendStatus { get; set; }
        public DbSet<auInvoiceReprintTrace> auInvoiceReprintTrace { get; set; }
        public DbSet<auInvoiceTrace> auInvoiceTrace { get; set; }
        public DbSet<auInvoiceTsmPaymentInfo> auInvoiceTsmPaymentInfo { get; set; }
        public DbSet<auInvoiceTsmTransactionInfo> auInvoiceTsmTransactionInfo { get; set; }
        public DbSet<auInvoiceUnifreeSendStatus> auInvoiceUnifreeSendStatus { get; set; }
        public DbSet<auItemCopyTrace> auItemCopyTrace { get; set; }
        public DbSet<auItemTest> auItemTest { get; set; }
        public DbSet<auIyzicoServiceLog> auIyzicoServiceLog { get; set; }
        public DbSet<auJournalPermit> auJournalPermit { get; set; }
        public DbSet<auMacellanSuperappServiceLog> auMacellanSuperappServiceLog { get; set; }
        public DbSet<auMarketPlaceServiceTrace> auMarketPlaceServiceTrace { get; set; }
        public DbSet<auMergeRetailCustomerTrace> auMergeRetailCustomerTrace { get; set; }
        public DbSet<auMernisQueryTrace> auMernisQueryTrace { get; set; }
        public DbSet<auMobilDevServiceLog> auMobilDevServiceLog { get; set; }
        public DbSet<auMobileStorePaymentLog> auMobileStorePaymentLog { get; set; }
        public DbSet<auMobileStorePermit> auMobileStorePermit { get; set; }
        public DbSet<auMobilRevenueReportPermit> auMobilRevenueReportPermit { get; set; }
        public DbSet<auN2AnimaServiceLog> auN2AnimaServiceLog { get; set; }
        public DbSet<auOpenDrawerWithNoSaleTrace> auOpenDrawerWithNoSaleTrace { get; set; }
        public DbSet<auOptInOptOutTrace> auOptInOptOutTrace { get; set; }
        public DbSet<auPaymentPermit> auPaymentPermit { get; set; }
        public DbSet<auPaymentReprintTrace> auPaymentReprintTrace { get; set; }
        public DbSet<auPaynetServiceLog> auPaynetServiceLog { get; set; }
        public DbSet<auPlanetPaymentServiceLog> auPlanetPaymentServiceLog { get; set; }
        public DbSet<auPosTerminalLoginTrace> auPosTerminalLoginTrace { get; set; }
        public DbSet<auPriceListPermit> auPriceListPermit { get; set; }
        public DbSet<auProcessFlowDeny> auProcessFlowDeny { get; set; }
        public DbSet<auProcessFlowDenyTrace> auProcessFlowDenyTrace { get; set; }
        public DbSet<auProcessPermit> auProcessPermit { get; set; }
        public DbSet<auProformaProcessPermit> auProformaProcessPermit { get; set; }
        public DbSet<auProgramPermit> auProgramPermit { get; set; }
        public DbSet<auProgramUseTrace> auProgramUseTrace { get; set; }
        public DbSet<auProposalLineRevisionTrace> auProposalLineRevisionTrace { get; set; }
        public DbSet<auPurchaseRequisitionLineRevisionTrace> auPurchaseRequisitionLineRevisionTrace { get; set; }
        public DbSet<auPurchaseRequisitionPermit> auPurchaseRequisitionPermit { get; set; }
        public DbSet<auPurchaseRequisitionProposalPermit> auPurchaseRequisitionProposalPermit { get; set; }
        public DbSet<auPurchaseRequisitionProposalRevisionTrace> auPurchaseRequisitionProposalRevisionTrace { get; set; }
        public DbSet<auReconciliationReportNotificationTrace> auReconciliationReportNotificationTrace { get; set; }
        public DbSet<auReportFilterMinMaxDateValue> auReportFilterMinMaxDateValue { get; set; }
        public DbSet<auReportQueryPermit> auReportQueryPermit { get; set; }
        public DbSet<auRetailCustomerIdentitySharingSystemTrace> auRetailCustomerIdentitySharingSystemTrace { get; set; }
        public DbSet<auRetailInvoiceLineChangeTrace> auRetailInvoiceLineChangeTrace { get; set; }
        public DbSet<auSeaBoxServiceLog> auSeaBoxServiceLog { get; set; }
        public DbSet<auSendingDataTransferTrace> auSendingDataTransferTrace { get; set; }
        public DbSet<auShipmentReprintTrace> auShipmentReprintTrace { get; set; }
        public DbSet<auShipmentTrace> auShipmentTrace { get; set; }
        public DbSet<auSoftekESIRLinkTransactionInfo> auSoftekESIRLinkTransactionInfo { get; set; }
        public DbSet<auSupportRequest> auSupportRequest { get; set; }
        public DbSet<auSurveyPermit> auSurveyPermit { get; set; }
        public DbSet<auSurveySectionPermit> auSurveySectionPermit { get; set; }
        public DbSet<auTransactionCheckInOutTrace> auTransactionCheckInOutTrace { get; set; }
        public DbSet<auTransferPlanTrace> auTransferPlanTrace { get; set; }
        public DbSet<auTRAVFDTransactionInfo> auTRAVFDTransactionInfo { get; set; }
        public DbSet<auTRINGT202TransactionInfo> auTRINGT202TransactionInfo { get; set; }
        public DbSet<auTsmIntegratorPaymentInfo> auTsmIntegratorPaymentInfo { get; set; }
        public DbSet<auTsmPosPaymentInfo> auTsmPosPaymentInfo { get; set; }
        public DbSet<auTsmTransactionInfo> auTsmTransactionInfo { get; set; }
        public DbSet<auTuratelServiceLog> auTuratelServiceLog { get; set; }
        public DbSet<auUmicoServiceLog> auUmicoServiceLog { get; set; }
        public DbSet<auUnifreeServiceLog> auUnifreeServiceLog { get; set; }
        public DbSet<auUpdateItemCodeTrace> auUpdateItemCodeTrace { get; set; }
        public DbSet<auVehicleLoadingPermit> auVehicleLoadingPermit { get; set; }
        public DbSet<auVehicleUnLoadingPermit> auVehicleUnLoadingPermit { get; set; }
        public DbSet<auVerifoneProcessTrace> auVerifoneProcessTrace { get; set; }
        public DbSet<auWebTelZATCATransactionInfo> auWebTelZATCATransactionInfo { get; set; }

        // Base Tables
        public DbSet<bsAccountDetail> bsAccountDetail { get; set; }
        public DbSet<bsAccountDetailDesc> bsAccountDetailDesc { get; set; }
        public DbSet<bsAdjustCostMethod> bsAdjustCostMethod { get; set; }
        public DbSet<bsAdjustCostMethodDesc> bsAdjustCostMethodDesc { get; set; }
        public DbSet<bsAirportIATA> bsAirportIATA { get; set; }
        public DbSet<bsAirportIATADesc> bsAirportIATADesc { get; set; }
        public DbSet<bsAllocationRule> bsAllocationRule { get; set; }
        public DbSet<bsAllocationRuleDesc> bsAllocationRuleDesc { get; set; }
        public DbSet<bsAllocationSourceType> bsAllocationSourceType { get; set; }
        public DbSet<bsAllocationSourceTypeDesc> bsAllocationSourceTypeDesc { get; set; }
        public DbSet<bsApplication> bsApplication { get; set; }
        public DbSet<bsApplicationDesc> bsApplicationDesc { get; set; }
        public DbSet<bsBadDebtResult> bsBadDebtResult { get; set; }
        public DbSet<bsBadDebtResultDesc> bsBadDebtResultDesc { get; set; }
        public DbSet<bsBadDebtTransType> bsBadDebtTransType { get; set; }
        public DbSet<bsBadDebtTransTypeDesc> bsBadDebtTransTypeDesc { get; set; }
        public DbSet<bsBankAdditionalChargeType> bsBankAdditionalChargeType { get; set; }
        public DbSet<bsBankAdditionalChargeTypeDesc> bsBankAdditionalChargeTypeDesc { get; set; }
        public DbSet<bsBankCardType> bsBankCardType { get; set; }
        public DbSet<bsBankCardTypeDesc> bsBankCardTypeDesc { get; set; }
        public DbSet<bsBankCreditGuaranteeType> bsBankCreditGuaranteeType { get; set; }
        public DbSet<bsBankCreditGuaranteeTypeDesc> bsBankCreditGuaranteeTypeDesc { get; set; }
        public DbSet<bsBankPOSImportType> bsBankPOSImportType { get; set; }
        public DbSet<bsBankPOSImportTypeDesc> bsBankPOSImportTypeDesc { get; set; }
        public DbSet<bsBankPOSProvider> bsBankPOSProvider { get; set; }
        public DbSet<bsBankTransType> bsBankTransType { get; set; }
        public DbSet<bsBankTransTypeDesc> bsBankTransTypeDesc { get; set; }
        public DbSet<bsBasePrice> bsBasePrice { get; set; }
        public DbSet<bsBasePriceDesc> bsBasePriceDesc { get; set; }
        public DbSet<bsBOMEntityLevel> bsBOMEntityLevel { get; set; }
        public DbSet<bsBOMEntityLevelDesc> bsBOMEntityLevelDesc { get; set; }
        public DbSet<bsBrowseMethodType> bsBrowseMethodType { get; set; }
        public DbSet<bsBrowseMethodTypeDesc> bsBrowseMethodTypeDesc { get; set; }
        public DbSet<bsBudgetDetail> bsBudgetDetail { get; set; }
        public DbSet<bsBudgetDetailDesc> bsBudgetDetailDesc { get; set; }
        public DbSet<bsBulkMailServiceProvider> bsBulkMailServiceProvider { get; set; }
        public DbSet<bsBulkMailServiceProviderDesc> bsBulkMailServiceProviderDesc { get; set; }
        public DbSet<bsCashTransType> bsCashTransType { get; set; }
        public DbSet<bsCashTransTypeDesc> bsCashTransTypeDesc { get; set; }
        public DbSet<bsChannelType> bsChannelType { get; set; }
        public DbSet<bsChannelTypeDesc> bsChannelTypeDesc { get; set; }
        public DbSet<bsChequeTransType> bsChequeTransType { get; set; }
        public DbSet<bsChequeTransTypeDesc> bsChequeTransTypeDesc { get; set; }
        public DbSet<bsChequeType> bsChequeType { get; set; }
        public DbSet<bsChequeTypeDesc> bsChequeTypeDesc { get; set; }
        public DbSet<bsCommunicationKind> bsCommunicationKind { get; set; }
        public DbSet<bsCommunicationKindDesc> bsCommunicationKindDesc { get; set; }
        public DbSet<bsConfirmationRuleType> bsConfirmationRuleType { get; set; }
        public DbSet<bsConfirmationRuleTypeDesc> bsConfirmationRuleTypeDesc { get; set; }
        public DbSet<bsConfirmationStatus> bsConfirmationStatus { get; set; }
        public DbSet<bsConfirmationStatusDesc> bsConfirmationStatusDesc { get; set; }
        public DbSet<bsConfirmationType> bsConfirmationType { get; set; }
        public DbSet<bsConfirmationTypeDesc> bsConfirmationTypeDesc { get; set; }
        public DbSet<bsConsentSource> bsConsentSource { get; set; }
        public DbSet<bsContractType> bsContractType { get; set; }
        public DbSet<bsContractTypeDesc> bsContractTypeDesc { get; set; }
        public DbSet<bsCostingLevel> bsCostingLevel { get; set; }
        public DbSet<bsCostingLevelDesc> bsCostingLevelDesc { get; set; }
        public DbSet<bsCostingMethod> bsCostingMethod { get; set; }
        public DbSet<bsCostingMethodDesc> bsCostingMethodDesc { get; set; }
        public DbSet<bsCostingVariantLevel> bsCostingVariantLevel { get; set; }
        public DbSet<bsCostingVariantLevelDesc> bsCostingVariantLevelDesc { get; set; }
        public DbSet<bsCountryISO> bsCountryISO { get; set; }
        public DbSet<bsCreditCardPaymentType> bsCreditCardPaymentType { get; set; }
        public DbSet<bsCreditCardPaymentTypeDesc> bsCreditCardPaymentTypeDesc { get; set; }
        public DbSet<bsCreditType> bsCreditType { get; set; }
        public DbSet<bsCreditTypeDesc> bsCreditTypeDesc { get; set; }
        public DbSet<bsCurrAccType> bsCurrAccType { get; set; }
        public DbSet<bsCurrAccTypeDesc> bsCurrAccTypeDesc { get; set; }
        public DbSet<bsCustomerType> bsCustomerType { get; set; }
        public DbSet<bsCustomerTypeDesc> bsCustomerTypeDesc { get; set; }
        public DbSet<bsCustomizedQuery> bsCustomizedQuery { get; set; }
        public DbSet<bsCustomizedSQLObject> bsCustomizedSQLObject { get; set; }
        public DbSet<bsCustomsProductGroup> bsCustomsProductGroup { get; set; }
        public DbSet<bsCustomsProductGroupDesc> bsCustomsProductGroupDesc { get; set; }
        public DbSet<bsDataTransferConvertType> bsDataTransferConvertType { get; set; }
        public DbSet<bsDataTransferTableList> bsDataTransferTableList { get; set; }
        public DbSet<bsDataTransferValidateColumnList> bsDataTransferValidateColumnList { get; set; }
        public DbSet<bsDay> bsDay { get; set; }
        public DbSet<bsDayDesc> bsDayDesc { get; set; }
        public DbSet<bsDebitType> bsDebitType { get; set; }
        public DbSet<bsDebitTypeDesc> bsDebitTypeDesc { get; set; }
        public DbSet<bsDebtStatusType> bsDebtStatusType { get; set; }
        public DbSet<bsDebtStatusTypeDesc> bsDebtStatusTypeDesc { get; set; }
        public DbSet<bsDeclarationCapacity> bsDeclarationCapacity { get; set; }
        public DbSet<bsDeclarationCapacityDesc> bsDeclarationCapacityDesc { get; set; }
        public DbSet<bsDeclarationPostType> bsDeclarationPostType { get; set; }
        public DbSet<bsDeclarationType> bsDeclarationType { get; set; }
        public DbSet<bsDeclarationTypeDesc> bsDeclarationTypeDesc { get; set; }
        public DbSet<bsDepreciationMethod> bsDepreciationMethod { get; set; }
        public DbSet<bsDepreciationMethodDesc> bsDepreciationMethodDesc { get; set; }
        public DbSet<bsDevice> bsDevice { get; set; }
        public DbSet<bsDeviceDesc> bsDeviceDesc { get; set; }
        public DbSet<bsDeviceType> bsDeviceType { get; set; }
        public DbSet<bsDeviceTypeDesc> bsDeviceTypeDesc { get; set; }
        public DbSet<bsDiscountLevelOfUse> bsDiscountLevelOfUse { get; set; }
        public DbSet<bsDiscountLevelOfUseDesc> bsDiscountLevelOfUseDesc { get; set; }
        public DbSet<bsDiscountOfferApply> bsDiscountOfferApply { get; set; }
        public DbSet<bsDiscountOfferApplyDesc> bsDiscountOfferApplyDesc { get; set; }
        public DbSet<bsDiscountOfferMethod> bsDiscountOfferMethod { get; set; }
        public DbSet<bsDiscountOfferMethodDesc> bsDiscountOfferMethodDesc { get; set; }
        public DbSet<bsDiscountOfferStage> bsDiscountOfferStage { get; set; }
        public DbSet<bsDiscountOfferStageDesc> bsDiscountOfferStageDesc { get; set; }
        public DbSet<bsDiscountOfferType> bsDiscountOfferType { get; set; }
        public DbSet<bsDiscountOfferTypeDesc> bsDiscountOfferTypeDesc { get; set; }
        public DbSet<bsDiscountVoucherBase> bsDiscountVoucherBase { get; set; }
        public DbSet<bsDiscountVoucherBaseDesc> bsDiscountVoucherBaseDesc { get; set; }
        public DbSet<bsDispOrderType> bsDispOrderType { get; set; }
        public DbSet<bsDispOrderTypeDesc> bsDispOrderTypeDesc { get; set; }
        public DbSet<bsDocumentType> bsDocumentType { get; set; }
        public DbSet<bsDocumentTypeDesc> bsDocumentTypeDesc { get; set; }
        public DbSet<bsEasyStartupSteps> bsEasyStartupSteps { get; set; }
        public DbSet<bsEasyStartupStepsDesc> bsEasyStartupStepsDesc { get; set; }
        public DbSet<bsEditMask> bsEditMask { get; set; }
        public DbSet<bsEditMaskDesc> bsEditMaskDesc { get; set; }
        public DbSet<bsEInvoiceStatus> bsEInvoiceStatus { get; set; }
        public DbSet<bsEInvoiceStatusDesc> bsEInvoiceStatusDesc { get; set; }
        public DbSet<bsEmailType> bsEmailType { get; set; }
        public DbSet<bsEmailTypeDesc> bsEmailTypeDesc { get; set; }
        public DbSet<bsEmployeePayType> bsEmployeePayType { get; set; }
        public DbSet<bsEmployeePayTypeDesc> bsEmployeePayTypeDesc { get; set; }
        public DbSet<bsEmployeeSpecialType> bsEmployeeSpecialType { get; set; }
        public DbSet<bsEmployeeSpecialTypeDesc> bsEmployeeSpecialTypeDesc { get; set; }
        public DbSet<bsEShipmentStatus> bsEShipmentStatus { get; set; }
        public DbSet<bsEShipmentStatusDesc> bsEShipmentStatusDesc { get; set; }
        public DbSet<bsExpenseSlipType> bsExpenseSlipType { get; set; }
        public DbSet<bsExpenseSlipTypeDesc> bsExpenseSlipTypeDesc { get; set; }
        public DbSet<bsEyeGlassSutType> bsEyeGlassSutType { get; set; }
        public DbSet<bsEyeGlassSutTypeDesc> bsEyeGlassSutTypeDesc { get; set; }
        public DbSet<bsFastDeliveryCompany> bsFastDeliveryCompany { get; set; }
        public DbSet<bsFastDeliveryCompanyDesc> bsFastDeliveryCompanyDesc { get; set; }
        public DbSet<bsFileFormatType> bsFileFormatType { get; set; }
        public DbSet<bsFileFormatTypeDesc> bsFileFormatTypeDesc { get; set; }
        public DbSet<bsFolder> bsFolder { get; set; }
        public DbSet<bsFolderDesc> bsFolderDesc { get; set; }
        public DbSet<bsFormatType> bsFormatType { get; set; }
        public DbSet<bsFormatTypeDesc> bsFormatTypeDesc { get; set; }
        public DbSet<bsGatewayServiceProvider> bsGatewayServiceProvider { get; set; }
        public DbSet<bsGender> bsGender { get; set; }
        public DbSet<bsGenderDesc> bsGenderDesc { get; set; }
        public DbSet<bsGettingData> bsGettingData { get; set; }
        public DbSet<bsGiftCardPaymentType> bsGiftCardPaymentType { get; set; }
        public DbSet<bsGiftCardPaymentTypeDesc> bsGiftCardPaymentTypeDesc { get; set; }
        public DbSet<bsGlassIndex> bsGlassIndex { get; set; }
        public DbSet<bsIncompleteDownPaymentDistributionType> bsIncompleteDownPaymentDistributionType { get; set; }
        public DbSet<bsIncompleteDownPaymentDistributionTypeDesc> bsIncompleteDownPaymentDistributionTypeDesc { get; set; }
        public DbSet<bsIncoterm> bsIncoterm { get; set; }
        public DbSet<bsIncotermDesc> bsIncotermDesc { get; set; }
        public DbSet<bsInnerOrderType> bsInnerOrderType { get; set; }
        public DbSet<bsInnerOrderTypeDesc> bsInnerOrderTypeDesc { get; set; }
        public DbSet<bsInnerProcess> bsInnerProcess { get; set; }
        public DbSet<bsInnerProcessDesc> bsInnerProcessDesc { get; set; }
        public DbSet<bsInternetPaymentType> bsInternetPaymentType { get; set; }
        public DbSet<bsInvoiceReturnType> bsInvoiceReturnType { get; set; }
        public DbSet<bsInvoiceReturnTypeDesc> bsInvoiceReturnTypeDesc { get; set; }
        public DbSet<bsInvoiceType> bsInvoiceType { get; set; }
        public DbSet<bsInvoiceTypeDesc> bsInvoiceTypeDesc { get; set; }
        public DbSet<bsItemDimType> bsItemDimType { get; set; }
        public DbSet<bsItemDimTypeDesc> bsItemDimTypeDesc { get; set; }
        public DbSet<bsItemProcessPermitType> bsItemProcessPermitType { get; set; }
        public DbSet<bsItemProcessPermitTypeDesc> bsItemProcessPermitTypeDesc { get; set; }
        public DbSet<bsItemType> bsItemType { get; set; }
        public DbSet<bsItemTypeDesc> bsItemTypeDesc { get; set; }
        public DbSet<bsJournalType> bsJournalType { get; set; }
        public DbSet<bsJournalTypeDesc> bsJournalTypeDesc { get; set; }
        public DbSet<bsLensType> bsLensType { get; set; }
        public DbSet<bsLensTypeDesc> bsLensTypeDesc { get; set; }
        public DbSet<bsLetterOfGuaranteeType> bsLetterOfGuaranteeType { get; set; }
        public DbSet<bsLetterOfGuaranteeTypeDesc> bsLetterOfGuaranteeTypeDesc { get; set; }
        public DbSet<bsLinkedProductType> bsLinkedProductType { get; set; }
        public DbSet<bsLinkedProductTypeDesc> bsLinkedProductTypeDesc { get; set; }
        public DbSet<bsLoyaltyProgramProcess> bsLoyaltyProgramProcess { get; set; }
        public DbSet<bsLoyaltyProgramProcessDesc> bsLoyaltyProgramProcessDesc { get; set; }
        public DbSet<bsMarketPlace> bsMarketPlace { get; set; }
        public DbSet<bsMessageImportance> bsMessageImportance { get; set; }
        public DbSet<bsMessageImportanceDesc> bsMessageImportanceDesc { get; set; }
        public DbSet<bsMMSBusinessPartner> bsMMSBusinessPartner { get; set; }
        public DbSet<bsMT940Process> bsMT940Process { get; set; }
        public DbSet<bsNebimV3HotfixVersion> bsNebimV3HotfixVersion { get; set; }
        public DbSet<bsNebimV3Services> bsNebimV3Services { get; set; }
        public DbSet<bsNebimV3ServicesDesc> bsNebimV3ServicesDesc { get; set; }
        public DbSet<bsNebimV3Version> bsNebimV3Version { get; set; }
        public DbSet<bsNebimV3WindowsServices> bsNebimV3WindowsServices { get; set; }
        public DbSet<bsNebimV3WindowsServicesDesc> bsNebimV3WindowsServicesDesc { get; set; }
        public DbSet<bsOfficialTaxType> bsOfficialTaxType { get; set; }
        public DbSet<bsOrderDeliveryRecordType> bsOrderDeliveryRecordType { get; set; }
        public DbSet<bsOrderDeliveryRecordTypeDesc> bsOrderDeliveryRecordTypeDesc { get; set; }
        public DbSet<bsOrderType> bsOrderType { get; set; }
        public DbSet<bsOrderTypeDesc> bsOrderTypeDesc { get; set; }
        public DbSet<bsOtherPaymentType> bsOtherPaymentType { get; set; }
        public DbSet<bsOtherPaymentTypeDesc> bsOtherPaymentTypeDesc { get; set; }
        public DbSet<bsPackagingType> bsPackagingType { get; set; }
        public DbSet<bsPackagingTypeDesc> bsPackagingTypeDesc { get; set; }
        public DbSet<bsPaymentMeans> bsPaymentMeans { get; set; }
        public DbSet<bsPaymentMeansDesc> bsPaymentMeansDesc { get; set; }
        public DbSet<bsPaymentType> bsPaymentType { get; set; }
        public DbSet<bsPaymentTypeDesc> bsPaymentTypeDesc { get; set; }
        public DbSet<bsPayType> bsPayType { get; set; }
        public DbSet<bsPayTypeDesc> bsPayTypeDesc { get; set; }
        public DbSet<bsPDCQueryList> bsPDCQueryList { get; set; }
        public DbSet<bsPickingType> bsPickingType { get; set; }
        public DbSet<bsPickingTypeDesc> bsPickingTypeDesc { get; set; }
        public DbSet<bsPointBase> bsPointBase { get; set; }
        public DbSet<bsPointBaseDesc> bsPointBaseDesc { get; set; }
        public DbSet<bsPointRecordType> bsPointRecordType { get; set; }
        public DbSet<bsPointRecordTypeDesc> bsPointRecordTypeDesc { get; set; }
        public DbSet<bsPolicyCustomerEdit> bsPolicyCustomerEdit { get; set; }
        public DbSet<bsPolicyCustomerEditDesc> bsPolicyCustomerEditDesc { get; set; }
        public DbSet<bsPolicyCustomerPayment> bsPolicyCustomerPayment { get; set; }
        public DbSet<bsPolicyCustomerPaymentDesc> bsPolicyCustomerPaymentDesc { get; set; }
        public DbSet<bsPolicyCustomerSharing> bsPolicyCustomerSharing { get; set; }
        public DbSet<bsPolicyCustomerSharingDesc> bsPolicyCustomerSharingDesc { get; set; }
        public DbSet<bsPolicyVendorSharing> bsPolicyVendorSharing { get; set; }
        public DbSet<bsPolicyVendorSharingDesc> bsPolicyVendorSharingDesc { get; set; }
        public DbSet<bsPOSMode> bsPOSMode { get; set; }
        public DbSet<bsPOSModeDesc> bsPOSModeDesc { get; set; }
        public DbSet<bsPostAccType> bsPostAccType { get; set; }
        public DbSet<bsPostAccTypeDesc> bsPostAccTypeDesc { get; set; }
        public DbSet<bsPresentCardActivationProcess> bsPresentCardActivationProcess { get; set; }
        public DbSet<bsPresentCardActivationProcessDesc> bsPresentCardActivationProcessDesc { get; set; }
        public DbSet<bsPresentCardActivationStatus> bsPresentCardActivationStatus { get; set; }
        public DbSet<bsPresentCardActivationStatusDesc> bsPresentCardActivationStatusDesc { get; set; }
        public DbSet<bsPresentCardActivationType> bsPresentCardActivationType { get; set; }
        public DbSet<bsPresentCardActivationTypeDesc> bsPresentCardActivationTypeDesc { get; set; }
        public DbSet<bsProcess> bsProcess { get; set; }
        public DbSet<bsProcessDesc> bsProcessDesc { get; set; }
        public DbSet<bsProcessFlow> bsProcessFlow { get; set; }
        public DbSet<bsProcessFlowDesc> bsProcessFlowDesc { get; set; }
        public DbSet<bsProductType> bsProductType { get; set; }
        public DbSet<bsProductTypeDesc> bsProductTypeDesc { get; set; }
        public DbSet<bsQuery> bsQuery { get; set; }
        public DbSet<bsQueryCustom> bsQueryCustom { get; set; }
        public DbSet<bsQuestionInputType> bsQuestionInputType { get; set; }
        public DbSet<bsQuestionInputTypeDesc> bsQuestionInputTypeDesc { get; set; }
        public DbSet<bsReconciliationType> bsReconciliationType { get; set; }
        public DbSet<bsReconciliationTypeDesc> bsReconciliationTypeDesc { get; set; }
        public DbSet<bsReserveType> bsReserveType { get; set; }
        public DbSet<bsReserveTypeDesc> bsReserveTypeDesc { get; set; }
        public DbSet<bsRetailCustomerConditionalRequiredFields> bsRetailCustomerConditionalRequiredFields { get; set; }
        public DbSet<bsSendingData> bsSendingData { get; set; }
        public DbSet<bsSGKInsuaranceType> bsSGKInsuaranceType { get; set; }
        public DbSet<bsSGKInsuaranceTypeDesc> bsSGKInsuaranceTypeDesc { get; set; }
        public DbSet<bsSGKMission> bsSGKMission { get; set; }
        public DbSet<bsSGKMissionDesc> bsSGKMissionDesc { get; set; }
        public DbSet<bsSGKWorkPlaceSector> bsSGKWorkPlaceSector { get; set; }
        public DbSet<bsSGKWorkPlaceSectorDesc> bsSGKWorkPlaceSectorDesc { get; set; }
        public DbSet<bsShipmentType> bsShipmentType { get; set; }
        public DbSet<bsShipmentTypeDesc> bsShipmentTypeDesc { get; set; }
        public DbSet<bsSMSGatewayServiceCompany> bsSMSGatewayServiceCompany { get; set; }
        public DbSet<bsSMSStatus> bsSMSStatus { get; set; }
        public DbSet<bsSMSStatusDesc> bsSMSStatusDesc { get; set; }
        public DbSet<bsStandardBarcodeType> bsStandardBarcodeType { get; set; }
        public DbSet<bsStandardBarcodeTypeDesc> bsStandardBarcodeTypeDesc { get; set; }
        public DbSet<bsSupportType> bsSupportType { get; set; }
        public DbSet<bsSupportTypeDesc> bsSupportTypeDesc { get; set; }
        public DbSet<bsTaxExemption> bsTaxExemption { get; set; }
        public DbSet<bsTaxExemptionDesc> bsTaxExemptionDesc { get; set; }
        public DbSet<bsTaxFreeRefundCompany> bsTaxFreeRefundCompany { get; set; }
        public DbSet<bsTaxPaymentAccType> bsTaxPaymentAccType { get; set; }
        public DbSet<bsTaxPaymentAccTypeDesc> bsTaxPaymentAccTypeDesc { get; set; }
        public DbSet<bsTaxPaymentType> bsTaxPaymentType { get; set; }
        public DbSet<bsTaxPaymentTypeDesc> bsTaxPaymentTypeDesc { get; set; }
        public DbSet<bsTaxType> bsTaxType { get; set; }
        public DbSet<bsTaxTypeDesc> bsTaxTypeDesc { get; set; }
        public DbSet<bsTextileCareSymbolGr> bsTextileCareSymbolGr { get; set; }
        public DbSet<bsTextileCareSymbolGrDesc> bsTextileCareSymbolGrDesc { get; set; }
        public DbSet<bsTransferPlanRule> bsTransferPlanRule { get; set; }
        public DbSet<bsTransferPlanRuleDesc> bsTransferPlanRuleDesc { get; set; }
        public DbSet<bsTransportMode> bsTransportMode { get; set; }
        public DbSet<bsTransportModeDesc> bsTransportModeDesc { get; set; }
        public DbSet<bsTransType> bsTransType { get; set; }
        public DbSet<bsTransTypeDesc> bsTransTypeDesc { get; set; }
        public DbSet<bsUBLExtensions> bsUBLExtensions { get; set; }
        public DbSet<bsUTSDeclarationField> bsUTSDeclarationField { get; set; }
        public DbSet<bsVendorType> bsVendorType { get; set; }
        public DbSet<bsVendorTypeDesc> bsVendorTypeDesc { get; set; }
        public DbSet<bsWarehouseOwner> bsWarehouseOwner { get; set; }
        public DbSet<bsWarehouseOwnerDesc> bsWarehouseOwnerDesc { get; set; }
        public DbSet<bsWithHoldingTaxType> bsWithHoldingTaxType { get; set; }
        public DbSet<bsWorkDangerLevel> bsWorkDangerLevel { get; set; }
        public DbSet<bsWorkDangerLevelDesc> bsWorkDangerLevelDesc { get; set; }
        public DbSet<bsWorkplaceKind> bsWorkplaceKind { get; set; }
        public DbSet<bsWorkplaceKindDesc> bsWorkplaceKindDesc { get; set; }
        public DbSet<bsWorkplacePropertyStatus> bsWorkplacePropertyStatus { get; set; }
        public DbSet<bsWorkplacePropertyStatusDesc> bsWorkplacePropertyStatusDesc { get; set; }

        // Code/Reference Tables
        public DbSet<cdAccountant> cdAccountant { get; set; }
        public DbSet<cdAccountantDesc> cdAccountantDesc { get; set; }
        public DbSet<cdAddressShareCompanyWebService> cdAddressShareCompanyWebService { get; set; }
        public DbSet<cdAddressShareCompanyWebServiceDesc> cdAddressShareCompanyWebServiceDesc { get; set; }
        public DbSet<cdAddressType> cdAddressType { get; set; }
        public DbSet<cdAddressTypeDesc> cdAddressTypeDesc { get; set; }
        public DbSet<cdAllocationTemplate> cdAllocationTemplate { get; set; }
        public DbSet<cdAllocationTemplateDesc> cdAllocationTemplateDesc { get; set; }
        public DbSet<cdAmountRule> cdAmountRule { get; set; }
        public DbSet<cdAmountRuleDesc> cdAmountRuleDesc { get; set; }
        public DbSet<cdATAttribute> cdATAttribute { get; set; }
        public DbSet<cdATAttributeDesc> cdATAttributeDesc { get; set; }
        public DbSet<cdATAttributeType> cdATAttributeType { get; set; }
        public DbSet<cdATAttributeTypeDesc> cdATAttributeTypeDesc { get; set; }
        public DbSet<cdBadDebtLetterType> cdBadDebtLetterType { get; set; }
        public DbSet<cdBadDebtLetterTypeDesc> cdBadDebtLetterTypeDesc { get; set; }
        public DbSet<cdBadDebtReason> cdBadDebtReason { get; set; }
        public DbSet<cdBadDebtReasonDesc> cdBadDebtReasonDesc { get; set; }
        public DbSet<cdBank> cdBank { get; set; }
        public DbSet<cdBankAccType> cdBankAccType { get; set; }
        public DbSet<cdBankAccTypeDesc> cdBankAccTypeDesc { get; set; }
        public DbSet<cdBankCreditType> cdBankCreditType { get; set; }
        public DbSet<cdBankCreditTypeDesc> cdBankCreditTypeDesc { get; set; }
        public DbSet<cdBankDesc> cdBankDesc { get; set; }
        public DbSet<cdBankOpType> cdBankOpType { get; set; }
        public DbSet<cdBankOpTypeDesc> cdBankOpTypeDesc { get; set; }
        public DbSet<cdBarcodeCompany> cdBarcodeCompany { get; set; }
        public DbSet<cdBarcodeType> cdBarcodeType { get; set; }
        public DbSet<cdBarcodeTypeDesc> cdBarcodeTypeDesc { get; set; }
        public DbSet<cdBaseMaterial> cdBaseMaterial { get; set; }
        public DbSet<cdBaseMaterialDesc> cdBaseMaterialDesc { get; set; }
        public DbSet<cdBatch> cdBatch { get; set; }
        public DbSet<cdBatchDesc> cdBatchDesc { get; set; }
        public DbSet<cdBatchGroup> cdBatchGroup { get; set; }
        public DbSet<cdBatchGroupDesc> cdBatchGroupDesc { get; set; }
        public DbSet<cdBloodType> cdBloodType { get; set; }
        public DbSet<cdBloodTypeDesc> cdBloodTypeDesc { get; set; }
        public DbSet<cdBOM> cdBOM { get; set; }
        public DbSet<cdBOMDesc> cdBOMDesc { get; set; }
        public DbSet<cdBOMEntity> cdBOMEntity { get; set; }
        public DbSet<cdBOMEntityDesc> cdBOMEntityDesc { get; set; }
        public DbSet<cdBOMTemplate> cdBOMTemplate { get; set; }
        public DbSet<cdBOMTemplateAttribute> cdBOMTemplateAttribute { get; set; }
        public DbSet<cdBOMTemplateAttributeDesc> cdBOMTemplateAttributeDesc { get; set; }
        public DbSet<cdBOMTemplateAttributeType> cdBOMTemplateAttributeType { get; set; }
        public DbSet<cdBOMTemplateAttributeTypeDesc> cdBOMTemplateAttributeTypeDesc { get; set; }
        public DbSet<cdBOMTemplateDesc> cdBOMTemplateDesc { get; set; }
        public DbSet<cdBrand> cdBrand { get; set; }
        public DbSet<cdBrandDesc> cdBrandDesc { get; set; }
        public DbSet<cdBudgetType> cdBudgetType { get; set; }
        public DbSet<cdBudgetTypeDesc> cdBudgetTypeDesc { get; set; }
        public DbSet<cdBusinessGroup> cdBusinessGroup { get; set; }
        public DbSet<cdBusinessGroupDesc> cdBusinessGroupDesc { get; set; }
        public DbSet<cdCareWarning> cdCareWarning { get; set; }
        public DbSet<cdCareWarningDesc> cdCareWarningDesc { get; set; }
        public DbSet<cdCareWarningTemplate> cdCareWarningTemplate { get; set; }
        public DbSet<cdCareWarningTemplateDesc> cdCareWarningTemplateDesc { get; set; }
        public DbSet<cdChannelTemplate> cdChannelTemplate { get; set; }
        public DbSet<cdChannelTemplateDesc> cdChannelTemplateDesc { get; set; }
        public DbSet<cdCheckOutReason> cdCheckOutReason { get; set; }
        public DbSet<cdCheckOutReasonDesc> cdCheckOutReasonDesc { get; set; }
        public DbSet<cdCheque> cdCheque { get; set; }
        public DbSet<cdChequeAttribute> cdChequeAttribute { get; set; }
        public DbSet<cdChequeAttributeDesc> cdChequeAttributeDesc { get; set; }
        public DbSet<cdChequeAttributeType> cdChequeAttributeType { get; set; }
        public DbSet<cdChequeAttributeTypeDesc> cdChequeAttributeTypeDesc { get; set; }
        public DbSet<cdChequeDenyReason> cdChequeDenyReason { get; set; }
        public DbSet<cdChequeDenyReasonDesc> cdChequeDenyReasonDesc { get; set; }
        public DbSet<cdChequeDesc> cdChequeDesc { get; set; }
        public DbSet<cdCity> cdCity { get; set; }
        public DbSet<cdCityDesc> cdCityDesc { get; set; }
        public DbSet<cdCoatingType> cdCoatingType { get; set; }
        public DbSet<cdCoatingTypeDesc> cdCoatingTypeDesc { get; set; }
        public DbSet<cdCollection> cdCollection { get; set; }
        public DbSet<cdCollectionDesc> cdCollectionDesc { get; set; }
        public DbSet<cdColor> cdColor { get; set; }
        public DbSet<cdColorCatalog> cdColorCatalog { get; set; }
        public DbSet<cdColorCatalogDesc> cdColorCatalogDesc { get; set; }
        public DbSet<cdColorDesc> cdColorDesc { get; set; }
        public DbSet<cdColorGroup> cdColorGroup { get; set; }
        public DbSet<cdColorGroupDesc> cdColorGroupDesc { get; set; }
        public DbSet<cdColorTheme> cdColorTheme { get; set; }
        public DbSet<cdColorThemeAttribute> cdColorThemeAttribute { get; set; }
        public DbSet<cdColorThemeAttributeDesc> cdColorThemeAttributeDesc { get; set; }
        public DbSet<cdColorThemeAttributeType> cdColorThemeAttributeType { get; set; }
        public DbSet<cdColorThemeAttributeTypeDesc> cdColorThemeAttributeTypeDesc { get; set; }
        public DbSet<cdColorThemeDesc> cdColorThemeDesc { get; set; }
        public DbSet<cdColorType> cdColorType { get; set; }
        public DbSet<cdColorTypeDesc> cdColorTypeDesc { get; set; }
        public DbSet<cdCommercialRole> cdCommercialRole { get; set; }
        public DbSet<cdCommercialRoleDesc> cdCommercialRoleDesc { get; set; }
        public DbSet<cdCommunicationType> cdCommunicationType { get; set; }
        public DbSet<cdCommunicationTypeDesc> cdCommunicationTypeDesc { get; set; }
        public DbSet<cdCompany> cdCompany { get; set; }
        public DbSet<cdCompanyBrand> cdCompanyBrand { get; set; }
        public DbSet<cdCompanyBrandDesc> cdCompanyBrandDesc { get; set; }
        public DbSet<cdCompanyCreditCard> cdCompanyCreditCard { get; set; }
        public DbSet<cdCompanyCreditCardDesc> cdCompanyCreditCardDesc { get; set; }
        public DbSet<cdConditionType> cdConditionType { get; set; }
        public DbSet<cdConditionTypeDesc> cdConditionTypeDesc { get; set; }
        public DbSet<cdConfirmationFormStatus> cdConfirmationFormStatus { get; set; }
        public DbSet<cdConfirmationFormStatusDesc> cdConfirmationFormStatusDesc { get; set; }
        public DbSet<cdConfirmationFormType> cdConfirmationFormType { get; set; }
        public DbSet<cdConfirmationFormTypeDesc> cdConfirmationFormTypeDesc { get; set; }
        public DbSet<cdConfirmationReason> cdConfirmationReason { get; set; }
        public DbSet<cdConfirmationReasonDesc> cdConfirmationReasonDesc { get; set; }
        public DbSet<cdConfirmationRule> cdConfirmationRule { get; set; }
        public DbSet<cdContactType> cdContactType { get; set; }
        public DbSet<cdContactTypeDesc> cdContactTypeDesc { get; set; }
        public DbSet<cdContainerType> cdContainerType { get; set; }
        public DbSet<cdContainerTypeDesc> cdContainerTypeDesc { get; set; }
        public DbSet<cdContractContent> cdContractContent { get; set; }
        public DbSet<cdContractContentDesc> cdContractContentDesc { get; set; }
        public DbSet<cdContractStatus> cdContractStatus { get; set; }
        public DbSet<cdContractStatusDesc> cdContractStatusDesc { get; set; }
        public DbSet<cdCostCenter> cdCostCenter { get; set; }
        public DbSet<cdCostCenterAttribute> cdCostCenterAttribute { get; set; }
        public DbSet<cdCostCenterAttributeDesc> cdCostCenterAttributeDesc { get; set; }
        public DbSet<cdCostCenterAttributeType> cdCostCenterAttributeType { get; set; }
        public DbSet<cdCostCenterAttributeTypeDesc> cdCostCenterAttributeTypeDesc { get; set; }
        public DbSet<cdCostCenterDesc> cdCostCenterDesc { get; set; }
        public DbSet<cdCostOfGoodsSoldPeriod> cdCostOfGoodsSoldPeriod { get; set; }
        public DbSet<cdCostOfGoodsSoldPeriodDesc> cdCostOfGoodsSoldPeriodDesc { get; set; }
        public DbSet<cdCountry> cdCountry { get; set; }
        public DbSet<cdCountryDesc> cdCountryDesc { get; set; }
        public DbSet<cdCreditCardType> cdCreditCardType { get; set; }
        public DbSet<cdCreditCardTypeDesc> cdCreditCardTypeDesc { get; set; }
        public DbSet<cdCreditSurveyor> cdCreditSurveyor { get; set; }
        public DbSet<cdCurrAcc> cdCurrAcc { get; set; }
        public DbSet<cdCurrAccAttribute> cdCurrAccAttribute { get; set; }
        public DbSet<cdCurrAccAttributeDesc> cdCurrAccAttributeDesc { get; set; }
        public DbSet<cdCurrAccAttributeType> cdCurrAccAttributeType { get; set; }
        public DbSet<cdCurrAccAttributeTypeDesc> cdCurrAccAttributeTypeDesc { get; set; }
        public DbSet<cdCurrAccDesc> cdCurrAccDesc { get; set; }
        public DbSet<cdCurrAccList> cdCurrAccList { get; set; }
        public DbSet<cdCurrAccListDesc> cdCurrAccListDesc { get; set; }
        public DbSet<cdCurrAccLotGr> cdCurrAccLotGr { get; set; }
        public DbSet<cdCurrAccLotGrDesc> cdCurrAccLotGrDesc { get; set; }
        public DbSet<cdCurrency> cdCurrency { get; set; }
        public DbSet<cdCurrencyDesc> cdCurrencyDesc { get; set; }
        public DbSet<cdCustomerAlertColor> cdCustomerAlertColor { get; set; }
        public DbSet<cdCustomerAlertColorDesc> cdCustomerAlertColorDesc { get; set; }
        public DbSet<cdCustomerCompanyBrandAttribute> cdCustomerCompanyBrandAttribute { get; set; }
        public DbSet<cdCustomerCompanyBrandAttributeDesc> cdCustomerCompanyBrandAttributeDesc { get; set; }
        public DbSet<cdCustomerCompanyBrandAttributeType> cdCustomerCompanyBrandAttributeType { get; set; }
        public DbSet<cdCustomerCompanyBrandAttributeTypeDesc> cdCustomerCompanyBrandAttributeTypeDesc { get; set; }
        public DbSet<cdCustomerConversationResult> cdCustomerConversationResult { get; set; }
        public DbSet<cdCustomerConversationResultDesc> cdCustomerConversationResultDesc { get; set; }
        public DbSet<cdCustomerConversationSubject> cdCustomerConversationSubject { get; set; }
        public DbSet<cdCustomerConversationSubjectDesc> cdCustomerConversationSubjectDesc { get; set; }
        public DbSet<cdCustomerConversationSubjectDetail> cdCustomerConversationSubjectDetail { get; set; }
        public DbSet<cdCustomerConversationSubjectDetailDesc> cdCustomerConversationSubjectDetailDesc { get; set; }
        public DbSet<cdCustomerConversationSubtitle> cdCustomerConversationSubtitle { get; set; }
        public DbSet<cdCustomerConversationSubtitleDesc> cdCustomerConversationSubtitleDesc { get; set; }
        public DbSet<cdCustomerCRMGroup> cdCustomerCRMGroup { get; set; }
        public DbSet<cdCustomerCRMGroupDesc> cdCustomerCRMGroupDesc { get; set; }
        public DbSet<cdCustomerDiscountGr> cdCustomerDiscountGr { get; set; }
        public DbSet<cdCustomerDiscountGrDesc> cdCustomerDiscountGrDesc { get; set; }
        public DbSet<cdCustomerMarkupGr> cdCustomerMarkupGr { get; set; }
        public DbSet<cdCustomerMarkupGrDesc> cdCustomerMarkupGrDesc { get; set; }
        public DbSet<cdCustomerPaymentPlanGr> cdCustomerPaymentPlanGr { get; set; }
        public DbSet<cdCustomerPaymentPlanGrDesc> cdCustomerPaymentPlanGrDesc { get; set; }
        public DbSet<cdCustomerShoppingHabit> cdCustomerShoppingHabit { get; set; }
        public DbSet<cdCustomerShoppingHabitDesc> cdCustomerShoppingHabitDesc { get; set; }
        public DbSet<cdCustomerShoppingLevel> cdCustomerShoppingLevel { get; set; }
        public DbSet<cdCustomerShoppingLevelDesc> cdCustomerShoppingLevelDesc { get; set; }
        public DbSet<cdCustomProcessGroup> cdCustomProcessGroup { get; set; }
        public DbSet<cdCustomProcessGroupDesc> cdCustomProcessGroupDesc { get; set; }
        public DbSet<cdCustomsCompany> cdCustomsCompany { get; set; }
        public DbSet<cdCustomsCompanyDesc> cdCustomsCompanyDesc { get; set; }
        public DbSet<cdCustomsOffices> cdCustomsOffices { get; set; }
        public DbSet<cdCustomsOfficesDesc> cdCustomsOfficesDesc { get; set; }
        public DbSet<cdCustomsTariffNumber> cdCustomsTariffNumber { get; set; }
        public DbSet<cdCustomsTariffNumberDesc> cdCustomsTariffNumberDesc { get; set; }
        public DbSet<cdDataLanguage> cdDataLanguage { get; set; }
        public DbSet<cdDataLanguageDesc> cdDataLanguageDesc { get; set; }
        public DbSet<cdDataTransferCompany> cdDataTransferCompany { get; set; }
        public DbSet<cdDataTransferCompanyDesc> cdDataTransferCompanyDesc { get; set; }
        public DbSet<cdDataTransferConvert> cdDataTransferConvert { get; set; }
        public DbSet<cdDataTransferConvertForAttribute> cdDataTransferConvertForAttribute { get; set; }
        public DbSet<cdDataTransferJob> cdDataTransferJob { get; set; }
        public DbSet<cdDataTransferSchedule> cdDataTransferSchedule { get; set; }
        public DbSet<cdDataTransferTemplate> cdDataTransferTemplate { get; set; }
        public DbSet<cdDebitReason> cdDebitReason { get; set; }
        public DbSet<cdDebitReasonDesc> cdDebitReasonDesc { get; set; }
        public DbSet<cdDeclaration> cdDeclaration { get; set; }
        public DbSet<cdDeclarationDesc> cdDeclarationDesc { get; set; }
        public DbSet<cdDeduction> cdDeduction { get; set; }
        public DbSet<cdDeductionDesc> cdDeductionDesc { get; set; }
        public DbSet<cdDeliveryCompany> cdDeliveryCompany { get; set; }
        public DbSet<cdDeliveryCompanyDesc> cdDeliveryCompanyDesc { get; set; }
        public DbSet<cdDiagnostic> cdDiagnostic { get; set; }
        public DbSet<cdDiagnosticDesc> cdDiagnosticDesc { get; set; }
        public DbSet<cdDigitalMarketingService> cdDigitalMarketingService { get; set; }
        public DbSet<cdDigitalMarketingServiceDesc> cdDigitalMarketingServiceDesc { get; set; }
        public DbSet<cdDiscountOffer> cdDiscountOffer { get; set; }
        public DbSet<cdDiscountOfferAttribute> cdDiscountOfferAttribute { get; set; }
        public DbSet<cdDiscountOfferAttributeDesc> cdDiscountOfferAttributeDesc { get; set; }
        public DbSet<cdDiscountOfferAttributeType> cdDiscountOfferAttributeType { get; set; }
        public DbSet<cdDiscountOfferAttributeTypeDesc> cdDiscountOfferAttributeTypeDesc { get; set; }
        public DbSet<cdDiscountOfferDesc> cdDiscountOfferDesc { get; set; }
        public DbSet<cdDiscountPointType> cdDiscountPointType { get; set; }
        public DbSet<cdDiscountPointTypeDesc> cdDiscountPointTypeDesc { get; set; }
        public DbSet<cdDiscountReason> cdDiscountReason { get; set; }
        public DbSet<cdDiscountReasonDesc> cdDiscountReasonDesc { get; set; }
        public DbSet<cdDiscountSubReason> cdDiscountSubReason { get; set; }
        public DbSet<cdDiscountSubReasonDesc> cdDiscountSubReasonDesc { get; set; }
        public DbSet<cdDiscountType> cdDiscountType { get; set; }
        public DbSet<cdDiscountTypeDesc> cdDiscountTypeDesc { get; set; }
        public DbSet<cdDiscountVoucher> cdDiscountVoucher { get; set; }
        public DbSet<cdDiscountVoucherType> cdDiscountVoucherType { get; set; }
        public DbSet<cdDiscountVoucherTypeDesc> cdDiscountVoucherTypeDesc { get; set; }
        public DbSet<cdDistanceMatrixProvider> cdDistanceMatrixProvider { get; set; }
        public DbSet<cdDistrict> cdDistrict { get; set; }
        public DbSet<cdDistrictDesc> cdDistrictDesc { get; set; }
        public DbSet<cdDOV> cdDOV { get; set; }
        public DbSet<cdDOVDesc> cdDOVDesc { get; set; }
        public DbSet<cdDriver> cdDriver { get; set; }
        public DbSet<cdDueDateFormula> cdDueDateFormula { get; set; }
        public DbSet<cdDueDateFormulaDesc> cdDueDateFormulaDesc { get; set; }
        public DbSet<cdEArchiveWebService> cdEArchiveWebService { get; set; }
        public DbSet<cdEArchiveWebServiceDesc> cdEArchiveWebServiceDesc { get; set; }
        public DbSet<cdEarnings> cdEarnings { get; set; }
        public DbSet<cdEarningsDesc> cdEarningsDesc { get; set; }
        public DbSet<cdEducationStatus> cdEducationStatus { get; set; }
        public DbSet<cdEducationStatusDesc> cdEducationStatusDesc { get; set; }
        public DbSet<cdEInvoiceWebService> cdEInvoiceWebService { get; set; }
        public DbSet<cdEInvoiceWebServiceDesc> cdEInvoiceWebServiceDesc { get; set; }
        public DbSet<cdEMailService> cdEMailService { get; set; }
        public DbSet<cdEMailServiceDesc> cdEMailServiceDesc { get; set; }
        public DbSet<cdEmployeeDocumentType> cdEmployeeDocumentType { get; set; }
        public DbSet<cdEmployeeDocumentTypeDesc> cdEmployeeDocumentTypeDesc { get; set; }
        public DbSet<cdEmployeeRecordType> cdEmployeeRecordType { get; set; }
        public DbSet<cdEmployeeRecordTypeDesc> cdEmployeeRecordTypeDesc { get; set; }
        public DbSet<cdEmployeeSocialInsuranceStatus> cdEmployeeSocialInsuranceStatus { get; set; }
        public DbSet<cdEmployeeSocialInsuranceStatusDesc> cdEmployeeSocialInsuranceStatusDesc { get; set; }
        public DbSet<cdEmployeeTaxStatus> cdEmployeeTaxStatus { get; set; }
        public DbSet<cdEmployeeTaxStatusDesc> cdEmployeeTaxStatusDesc { get; set; }
        public DbSet<cdEmploymentLaw> cdEmploymentLaw { get; set; }
        public DbSet<cdEmploymentLawDesc> cdEmploymentLawDesc { get; set; }
        public DbSet<cdEShipmentWebService> cdEShipmentWebService { get; set; }
        public DbSet<cdEShipmentWebServiceDesc> cdEShipmentWebServiceDesc { get; set; }
        public DbSet<cdExchangeType> cdExchangeType { get; set; }
        public DbSet<cdExchangeTypeDesc> cdExchangeTypeDesc { get; set; }
        public DbSet<cdExecutionOffice> cdExecutionOffice { get; set; }
        public DbSet<cdExecutionOfficeDesc> cdExecutionOfficeDesc { get; set; }
        public DbSet<cdExpensePeriod> cdExpensePeriod { get; set; }
        public DbSet<cdExpensePeriodDesc> cdExpensePeriodDesc { get; set; }
        public DbSet<cdExpenseType> cdExpenseType { get; set; }
        public DbSet<cdExpenseTypeDesc> cdExpenseTypeDesc { get; set; }
        public DbSet<cdExportFile> cdExportFile { get; set; }
        public DbSet<cdExportFileAttribute> cdExportFileAttribute { get; set; }
        public DbSet<cdExportFileAttributeDesc> cdExportFileAttributeDesc { get; set; }
        public DbSet<cdExportFileAttributeType> cdExportFileAttributeType { get; set; }
        public DbSet<cdExportFileAttributeTypeDesc> cdExportFileAttributeTypeDesc { get; set; }
        public DbSet<cdExportFileDesc> cdExportFileDesc { get; set; }
        public DbSet<cdExportType> cdExportType { get; set; }
        public DbSet<cdExportTypeDesc> cdExportTypeDesc { get; set; }
        public DbSet<cdFabric> cdFabric { get; set; }
        public DbSet<cdFabricDesc> cdFabricDesc { get; set; }
        public DbSet<cdFinanceCompanyWebService> cdFinanceCompanyWebService { get; set; }
        public DbSet<cdFinanceCompanyWebServiceDesc> cdFinanceCompanyWebServiceDesc { get; set; }
        public DbSet<cdFiscalPeriod> cdFiscalPeriod { get; set; }
        public DbSet<cdFiscalPeriodDesc> cdFiscalPeriodDesc { get; set; }
        public DbSet<cdFixedAssetStatus> cdFixedAssetStatus { get; set; }
        public DbSet<cdFixedAssetStatusDesc> cdFixedAssetStatusDesc { get; set; }
        public DbSet<cdFixedAssetType> cdFixedAssetType { get; set; }
        public DbSet<cdFixedAssetTypeDesc> cdFixedAssetTypeDesc { get; set; }
        public DbSet<cdFocalType> cdFocalType { get; set; }
        public DbSet<cdFocalTypeDesc> cdFocalTypeDesc { get; set; }
        public DbSet<cdForeignLanguage> cdForeignLanguage { get; set; }
        public DbSet<cdForeignLanguageDesc> cdForeignLanguageDesc { get; set; }
        public DbSet<cdForeignTradeStatus> cdForeignTradeStatus { get; set; }
        public DbSet<cdForeignTradeStatusDesc> cdForeignTradeStatusDesc { get; set; }
        public DbSet<cdFrameShapeType> cdFrameShapeType { get; set; }
        public DbSet<cdFrameShapeTypeDesc> cdFrameShapeTypeDesc { get; set; }
        public DbSet<cdFrameType> cdFrameType { get; set; }
        public DbSet<cdFrameTypeDesc> cdFrameTypeDesc { get; set; }
        public DbSet<cdFTAttribute> cdFTAttribute { get; set; }
        public DbSet<cdFTAttributeDesc> cdFTAttributeDesc { get; set; }
        public DbSet<cdFTAttributeType> cdFTAttributeType { get; set; }
        public DbSet<cdFTAttributeTypeDesc> cdFTAttributeTypeDesc { get; set; }
        public DbSet<cdGiftCard> cdGiftCard { get; set; }
        public DbSet<cdGLAcc> cdGLAcc { get; set; }
        public DbSet<cdGLAccAttribute> cdGLAccAttribute { get; set; }
        public DbSet<cdGLAccAttributeDesc> cdGLAccAttributeDesc { get; set; }
        public DbSet<cdGLAccAttributeType> cdGLAccAttributeType { get; set; }
        public DbSet<cdGLAccAttributeTypeDesc> cdGLAccAttributeTypeDesc { get; set; }
        public DbSet<cdGLAccClass> cdGLAccClass { get; set; }
        public DbSet<cdGLAccClassDesc> cdGLAccClassDesc { get; set; }
        public DbSet<cdGLAccDesc> cdGLAccDesc { get; set; }
        public DbSet<cdGLAccGroup> cdGLAccGroup { get; set; }
        public DbSet<cdGLAccGroupDesc> cdGLAccGroupDesc { get; set; }
        public DbSet<cdGLAccMain> cdGLAccMain { get; set; }
        public DbSet<cdGLAccMainDesc> cdGLAccMainDesc { get; set; }
        public DbSet<cdGLAccSub> cdGLAccSub { get; set; }
        public DbSet<cdGLAccSubDesc> cdGLAccSubDesc { get; set; }
        public DbSet<cdGLReflection> cdGLReflection { get; set; }
        public DbSet<cdGLReflectionDesc> cdGLReflectionDesc { get; set; }
        public DbSet<cdGLType> cdGLType { get; set; }
        public DbSet<cdGLTypeDesc> cdGLTypeDesc { get; set; }
        public DbSet<cdGrandLedger> cdGrandLedger { get; set; }
        public DbSet<cdHandicapType> cdHandicapType { get; set; }
        public DbSet<cdHandicapTypeDesc> cdHandicapTypeDesc { get; set; }
        public DbSet<cdImportFile> cdImportFile { get; set; }
        public DbSet<cdImportFileAttribute> cdImportFileAttribute { get; set; }
        public DbSet<cdImportFileAttributeDesc> cdImportFileAttributeDesc { get; set; }
        public DbSet<cdImportFileAttributeType> cdImportFileAttributeType { get; set; }
        public DbSet<cdImportFileAttributeTypeDesc> cdImportFileAttributeTypeDesc { get; set; }
        public DbSet<cdImportFileDesc> cdImportFileDesc { get; set; }
        public DbSet<cdInactivationReason> cdInactivationReason { get; set; }
        public DbSet<cdInactivationReasonDesc> cdInactivationReasonDesc { get; set; }
        public DbSet<cdIncentiveType> cdIncentiveType { get; set; }
        public DbSet<cdIncentiveTypeDesc> cdIncentiveTypeDesc { get; set; }
        public DbSet<cdIndustry> cdIndustry { get; set; }
        public DbSet<cdIndustryDesc> cdIndustryDesc { get; set; }
        public DbSet<cdInsuranceAgency> cdInsuranceAgency { get; set; }
        public DbSet<cdInsuranceAgencyDesc> cdInsuranceAgencyDesc { get; set; }
        public DbSet<cdInsuranceType> cdInsuranceType { get; set; }
        public DbSet<cdInsuranceTypeDesc> cdInsuranceTypeDesc { get; set; }
        public DbSet<cdInteractiveSmsParameters> cdInteractiveSmsParameters { get; set; }
        public DbSet<cdInternationalUnitOfMeasure> cdInternationalUnitOfMeasure { get; set; }
        public DbSet<cdInternationalUnitOfMeasureDesc> cdInternationalUnitOfMeasureDesc { get; set; }
        public DbSet<cdITAttribute> cdITAttribute { get; set; }
        public DbSet<cdITAttributeDesc> cdITAttributeDesc { get; set; }
        public DbSet<cdITAttributeType> cdITAttributeType { get; set; }
        public DbSet<cdITAttributeTypeDesc> cdITAttributeTypeDesc { get; set; }
        public DbSet<cdItem> cdItem { get; set; }
        public DbSet<cdItemAccountGr> cdItemAccountGr { get; set; }
        public DbSet<cdItemAccountGrDesc> cdItemAccountGrDesc { get; set; }
        public DbSet<cdItemAttribute> cdItemAttribute { get; set; }
        public DbSet<cdItemAttributeDesc> cdItemAttributeDesc { get; set; }
        public DbSet<cdItemAttributeType> cdItemAttributeType { get; set; }
        public DbSet<cdItemAttributeTypeDesc> cdItemAttributeTypeDesc { get; set; }
        public DbSet<cdItemDesc> cdItemDesc { get; set; }
        public DbSet<cdItemDim1> cdItemDim1 { get; set; }
        public DbSet<cdItemDim1Desc> cdItemDim1Desc { get; set; }
        public DbSet<cdItemDim2> cdItemDim2 { get; set; }
        public DbSet<cdItemDim3> cdItemDim3 { get; set; }
        public DbSet<cdItemDiscountGr> cdItemDiscountGr { get; set; }
        public DbSet<cdItemDiscountGrDesc> cdItemDiscountGrDesc { get; set; }
        public DbSet<cdItemLikeType> cdItemLikeType { get; set; }
        public DbSet<cdItemLikeTypeDesc> cdItemLikeTypeDesc { get; set; }
        public DbSet<cdItemList> cdItemList { get; set; }
        public DbSet<cdItemListDesc> cdItemListDesc { get; set; }
        public DbSet<cdItemOTAttribute> cdItemOTAttribute { get; set; }
        public DbSet<cdItemOTAttributeDesc> cdItemOTAttributeDesc { get; set; }
        public DbSet<cdItemOTAttributeType> cdItemOTAttributeType { get; set; }
        public DbSet<cdItemOTAttributeTypeDesc> cdItemOTAttributeTypeDesc { get; set; }
        public DbSet<cdItemPaymentPlanGr> cdItemPaymentPlanGr { get; set; }
        public DbSet<cdItemPaymentPlanGrDesc> cdItemPaymentPlanGrDesc { get; set; }
        public DbSet<cdItemTaxGr> cdItemTaxGr { get; set; }
        public DbSet<cdItemTaxGrDesc> cdItemTaxGrDesc { get; set; }
        public DbSet<cdItemTestType> cdItemTestType { get; set; }
        public DbSet<cdItemTestTypeDesc> cdItemTestTypeDesc { get; set; }
        public DbSet<cdItemTextileCareTemplate> cdItemTextileCareTemplate { get; set; }
        public DbSet<cdItemTextileCareTemplateDesc> cdItemTextileCareTemplateDesc { get; set; }
        public DbSet<cdItemVendorGr> cdItemVendorGr { get; set; }
        public DbSet<cdItemVendorGrDesc> cdItemVendorGrDesc { get; set; }
        public DbSet<cdJobDepartment> cdJobDepartment { get; set; }
        public DbSet<cdJobDepartmentDesc> cdJobDepartmentDesc { get; set; }
        public DbSet<cdJobInterviewResult> cdJobInterviewResult { get; set; }
        public DbSet<cdJobInterviewResultDesc> cdJobInterviewResultDesc { get; set; }
        public DbSet<cdJobPosition> cdJobPosition { get; set; }
        public DbSet<cdJobPositionDesc> cdJobPositionDesc { get; set; }
        public DbSet<cdJobTitle> cdJobTitle { get; set; }
        public DbSet<cdJobTitleDesc> cdJobTitleDesc { get; set; }
        public DbSet<cdJobTitleLevel> cdJobTitleLevel { get; set; }
        public DbSet<cdJobTitleLevelDesc> cdJobTitleLevelDesc { get; set; }
        public DbSet<cdJobTraining> cdJobTraining { get; set; }
        public DbSet<cdJobTrainingAttribute> cdJobTrainingAttribute { get; set; }
        public DbSet<cdJobTrainingAttributeDesc> cdJobTrainingAttributeDesc { get; set; }
        public DbSet<cdJobTrainingAttributeType> cdJobTrainingAttributeType { get; set; }
        public DbSet<cdJobTrainingAttributeTypeDesc> cdJobTrainingAttributeTypeDesc { get; set; }
        public DbSet<cdJobTrainingDesc> cdJobTrainingDesc { get; set; }
        public DbSet<cdJobType> cdJobType { get; set; }
        public DbSet<cdJobTypeDesc> cdJobTypeDesc { get; set; }
        public DbSet<cdJournalLedger> cdJournalLedger { get; set; }
        public DbSet<cdJournalTypeSub> cdJournalTypeSub { get; set; }
        public DbSet<cdJournalTypeSubDesc> cdJournalTypeSubDesc { get; set; }
        public DbSet<cdKnowLevel> cdKnowLevel { get; set; }
        public DbSet<cdKnowLevelDesc> cdKnowLevelDesc { get; set; }
        public DbSet<cdLabelType> cdLabelType { get; set; }
        public DbSet<cdLabelTypeDesc> cdLabelTypeDesc { get; set; }
        public DbSet<cdLawyer> cdLawyer { get; set; }
        public DbSet<cdLeaveType> cdLeaveType { get; set; }
        public DbSet<cdLeaveTypeDesc> cdLeaveTypeDesc { get; set; }
        public DbSet<cdLegalResignation> cdLegalResignation { get; set; }
        public DbSet<cdLegalResignationDesc> cdLegalResignationDesc { get; set; }
        public DbSet<cdLegalResignationLocal> cdLegalResignationLocal { get; set; }
        public DbSet<cdLegalResignationLocalDesc> cdLegalResignationLocalDesc { get; set; }
        public DbSet<cdLetterOfGuarantee> cdLetterOfGuarantee { get; set; }
        public DbSet<cdLetterOfGuaranteeAttribute> cdLetterOfGuaranteeAttribute { get; set; }
        public DbSet<cdLetterOfGuaranteeAttributeDesc> cdLetterOfGuaranteeAttributeDesc { get; set; }
        public DbSet<cdLetterOfGuaranteeAttributeType> cdLetterOfGuaranteeAttributeType { get; set; }
        public DbSet<cdLetterOfGuaranteeAttributeTypeDesc> cdLetterOfGuaranteeAttributeTypeDesc { get; set; }
        public DbSet<cdLetterType> cdLetterType { get; set; }
        public DbSet<cdLetterTypeDesc> cdLetterTypeDesc { get; set; }
        public DbSet<cdLot> cdLot { get; set; }
        public DbSet<cdLotDesc> cdLotDesc { get; set; }
        public DbSet<cdLoyaltyProgram> cdLoyaltyProgram { get; set; }
        public DbSet<cdLoyaltyProgramDesc> cdLoyaltyProgramDesc { get; set; }
        public DbSet<cdLoyaltyProgramLevel> cdLoyaltyProgramLevel { get; set; }
        public DbSet<cdLoyaltyProgramLevelDesc> cdLoyaltyProgramLevelDesc { get; set; }
        public DbSet<cdLoyaltyProgramStatus> cdLoyaltyProgramStatus { get; set; }
        public DbSet<cdLoyaltyProgramStatusDesc> cdLoyaltyProgramStatusDesc { get; set; }
        public DbSet<cdLoyaltyProgramStatusModifyReason> cdLoyaltyProgramStatusModifyReason { get; set; }
        public DbSet<cdLoyaltyProgramStatusModifyReasonDesc> cdLoyaltyProgramStatusModifyReasonDesc { get; set; }
        public DbSet<cdMainJobTitle> cdMainJobTitle { get; set; }
        public DbSet<cdMainJobTitleDesc> cdMainJobTitleDesc { get; set; }
        public DbSet<cdMaladyType> cdMaladyType { get; set; }
        public DbSet<cdMaladyTypeDesc> cdMaladyTypeDesc { get; set; }
        public DbSet<cdManufacturer> cdManufacturer { get; set; }
        public DbSet<cdManufacturerDesc> cdManufacturerDesc { get; set; }
        public DbSet<cdMessageReason> cdMessageReason { get; set; }
        public DbSet<cdMessageReasonDesc> cdMessageReasonDesc { get; set; }
        public DbSet<cdMessageType> cdMessageType { get; set; }
        public DbSet<cdMessageTypeDesc> cdMessageTypeDesc { get; set; }
        public DbSet<cdMilitaryServiceStatus> cdMilitaryServiceStatus { get; set; }
        public DbSet<cdMilitaryServiceStatusDesc> cdMilitaryServiceStatusDesc { get; set; }
        public DbSet<cdMissingWorkReason> cdMissingWorkReason { get; set; }
        public DbSet<cdMissingWorkReasonDesc> cdMissingWorkReasonDesc { get; set; }
        public DbSet<cdMMSBusinessPartnerService> cdMMSBusinessPartnerService { get; set; }
        public DbSet<cdNationality> cdNationality { get; set; }
        public DbSet<cdNationalityDesc> cdNationalityDesc { get; set; }
        public DbSet<cdOffice> cdOffice { get; set; }
        public DbSet<cdOfficeCOGSGr> cdOfficeCOGSGr { get; set; }
        public DbSet<cdOfficeCOGSGrDesc> cdOfficeCOGSGrDesc { get; set; }
        public DbSet<cdOfficeDesc> cdOfficeDesc { get; set; }
        public DbSet<cdOnlineBankWebService> cdOnlineBankWebService { get; set; }
        public DbSet<cdOnlineBankWebServiceDesc> cdOnlineBankWebServiceDesc { get; set; }
        public DbSet<cdOnlineDBSWebService> cdOnlineDBSWebService { get; set; }
        public DbSet<cdOnlineDBSWebServiceDesc> cdOnlineDBSWebServiceDesc { get; set; }
        public DbSet<cdOpticalGroupRange> cdOpticalGroupRange { get; set; }
        public DbSet<cdOpticalGroupRangeDesc> cdOpticalGroupRangeDesc { get; set; }
        public DbSet<cdOpticalSut> cdOpticalSut { get; set; }
        public DbSet<cdOpticalSutDesc> cdOpticalSutDesc { get; set; }
        public DbSet<cdOrderCancelReason> cdOrderCancelReason { get; set; }
        public DbSet<cdOrderCancelReasonDesc> cdOrderCancelReasonDesc { get; set; }
        public DbSet<cdOrderStatus> cdOrderStatus { get; set; }
        public DbSet<cdOrderStatusDesc> cdOrderStatusDesc { get; set; }
        public DbSet<cdOtherDocumentType> cdOtherDocumentType { get; set; }
        public DbSet<cdOtherDocumentTypeDesc> cdOtherDocumentTypeDesc { get; set; }
        public DbSet<cdPackageBrand> cdPackageBrand { get; set; }
        public DbSet<cdPackageBrandDesc> cdPackageBrandDesc { get; set; }
        public DbSet<cdPackageVolume> cdPackageVolume { get; set; }
        public DbSet<cdPackageVolumeDesc> cdPackageVolumeDesc { get; set; }
        public DbSet<cdPantone> cdPantone { get; set; }
        public DbSet<cdPantoneDesc> cdPantoneDesc { get; set; }
        public DbSet<cdPaymentMethod> cdPaymentMethod { get; set; }
        public DbSet<cdPaymentMethodDesc> cdPaymentMethodDesc { get; set; }
        public DbSet<cdPaymentPlan> cdPaymentPlan { get; set; }
        public DbSet<cdPaymentPlanDesc> cdPaymentPlanDesc { get; set; }
        public DbSet<cdPaymentProvider> cdPaymentProvider { get; set; }
        public DbSet<cdPaymentProviderDesc> cdPaymentProviderDesc { get; set; }
        public DbSet<cdPayrollType> cdPayrollType { get; set; }
        public DbSet<cdPayrollTypeDesc> cdPayrollTypeDesc { get; set; }
        public DbSet<cdPCT> cdPCT { get; set; }
        public DbSet<cdPCTDesc> cdPCTDesc { get; set; }
        public DbSet<cdPerceptionOfFashion> cdPerceptionOfFashion { get; set; }
        public DbSet<cdPerceptionOfFashionDesc> cdPerceptionOfFashionDesc { get; set; }
        public DbSet<cdPermissionMarketingService> cdPermissionMarketingService { get; set; }
        public DbSet<cdPlasticBagType> cdPlasticBagType { get; set; }
        public DbSet<cdPlasticBagTypeDesc> cdPlasticBagTypeDesc { get; set; }
        public DbSet<cdPointModifyReason> cdPointModifyReason { get; set; }
        public DbSet<cdPointModifyReasonDesc> cdPointModifyReasonDesc { get; set; }
        public DbSet<cdPort> cdPort { get; set; }
        public DbSet<cdPortDesc> cdPortDesc { get; set; }
        public DbSet<cdPOSTerminal> cdPOSTerminal { get; set; }
        public DbSet<cdPresentCardType> cdPresentCardType { get; set; }
        public DbSet<cdPresentCardTypeDesc> cdPresentCardTypeDesc { get; set; }
        public DbSet<cdPrevJobType> cdPrevJobType { get; set; }
        public DbSet<cdPrevJobTypeDesc> cdPrevJobTypeDesc { get; set; }
        public DbSet<cdPriceGroup> cdPriceGroup { get; set; }
        public DbSet<cdPriceGroupDesc> cdPriceGroupDesc { get; set; }
        public DbSet<cdPriceListType> cdPriceListType { get; set; }
        public DbSet<cdPriceListTypeDesc> cdPriceListTypeDesc { get; set; }
        public DbSet<cdPriority> cdPriority { get; set; }
        public DbSet<cdPriorityDesc> cdPriorityDesc { get; set; }
        public DbSet<cdPrivateInsurance> cdPrivateInsurance { get; set; }
        public DbSet<cdPrivateInsuranceDesc> cdPrivateInsuranceDesc { get; set; }
        public DbSet<cdProcessFlowDenyReason> cdProcessFlowDenyReason { get; set; }
        public DbSet<cdProcessFlowDenyReasonDesc> cdProcessFlowDenyReasonDesc { get; set; }
        public DbSet<cdProductCollectionGr> cdProductCollectionGr { get; set; }
        public DbSet<cdProductColorAttribute> cdProductColorAttribute { get; set; }
        public DbSet<cdProductColorAttributeDesc> cdProductColorAttributeDesc { get; set; }
        public DbSet<cdProductColorAttributeType> cdProductColorAttributeType { get; set; }
        public DbSet<cdProductColorAttributeTypeDesc> cdProductColorAttributeTypeDesc { get; set; }
        public DbSet<cdProductColorSet> cdProductColorSet { get; set; }
        public DbSet<cdProductColorSetDesc> cdProductColorSetDesc { get; set; }
        public DbSet<cdProductDimSet> cdProductDimSet { get; set; }
        public DbSet<cdProductDimSetDesc> cdProductDimSetDesc { get; set; }
        public DbSet<cdProductHierarchyLevel> cdProductHierarchyLevel { get; set; }
        public DbSet<cdProductHierarchyLevelDesc> cdProductHierarchyLevelDesc { get; set; }
        public DbSet<cdProductPart> cdProductPart { get; set; }
        public DbSet<cdProductPartDesc> cdProductPartDesc { get; set; }
        public DbSet<cdProductPointType> cdProductPointType { get; set; }
        public DbSet<cdProductPointTypeDesc> cdProductPointTypeDesc { get; set; }
        public DbSet<cdProductStatus> cdProductStatus { get; set; }
        public DbSet<cdProductStatusDesc> cdProductStatusDesc { get; set; }
        public DbSet<cdPromotionGroup> cdPromotionGroup { get; set; }
        public DbSet<cdPromotionGroupDesc> cdPromotionGroupDesc { get; set; }
        public DbSet<cdProposalConfirmationLimit> cdProposalConfirmationLimit { get; set; }
        public DbSet<cdProposalConfirmationRule> cdProposalConfirmationRule { get; set; }
        public DbSet<cdPurchasePlan> cdPurchasePlan { get; set; }
        public DbSet<cdPurchasePlanDesc> cdPurchasePlanDesc { get; set; }
        public DbSet<cdPurchasingAgent> cdPurchasingAgent { get; set; }
        public DbSet<cdQuarter> cdQuarter { get; set; }
        public DbSet<cdReasonForNotShopping> cdReasonForNotShopping { get; set; }
        public DbSet<cdReasonForNotShoppingDesc> cdReasonForNotShoppingDesc { get; set; }
        public DbSet<cdRecidivistType> cdRecidivistType { get; set; }
        public DbSet<cdRecidivistTypeDesc> cdRecidivistTypeDesc { get; set; }
        public DbSet<cdReconciliation> cdReconciliation { get; set; }
        public DbSet<cdReconciliationDesc> cdReconciliationDesc { get; set; }
        public DbSet<cdRegisteredEMailService> cdRegisteredEMailService { get; set; }
        public DbSet<cdRequisition> cdRequisition { get; set; }
        public DbSet<cdRequisitionAttribute> cdRequisitionAttribute { get; set; }
        public DbSet<cdRequisitionAttributeDesc> cdRequisitionAttributeDesc { get; set; }
        public DbSet<cdRequisitionAttributeType> cdRequisitionAttributeType { get; set; }
        public DbSet<cdRequisitionAttributeTypeDesc> cdRequisitionAttributeTypeDesc { get; set; }
        public DbSet<cdRequisitionConfirmationLimit> cdRequisitionConfirmationLimit { get; set; }
        public DbSet<cdRequisitionConfirmationRule> cdRequisitionConfirmationRule { get; set; }
        public DbSet<cdRequisitionDesc> cdRequisitionDesc { get; set; }
        public DbSet<cdRequisitionType> cdRequisitionType { get; set; }
        public DbSet<cdRequisitionTypeDesc> cdRequisitionTypeDesc { get; set; }
        public DbSet<cdResignation> cdResignation { get; set; }
        public DbSet<cdResignationDesc> cdResignationDesc { get; set; }
        public DbSet<cdResponsibilityArea> cdResponsibilityArea { get; set; }
        public DbSet<cdResponsibilityAreaDesc> cdResponsibilityAreaDesc { get; set; }
        public DbSet<cdReturnReason> cdReturnReason { get; set; }
        public DbSet<cdReturnReasonDesc> cdReturnReasonDesc { get; set; }
        public DbSet<cdRole> cdRole { get; set; }
        public DbSet<cdRoleDesc> cdRoleDesc { get; set; }
        public DbSet<cdRoll> cdRoll { get; set; }
        public DbSet<cdRollNoteType> cdRollNoteType { get; set; }
        public DbSet<cdRollNoteTypeDesc> cdRollNoteTypeDesc { get; set; }
        public DbSet<cdRoundsman> cdRoundsman { get; set; }
        public DbSet<cdSalesChannel> cdSalesChannel { get; set; }
        public DbSet<cdSalesChannelDesc> cdSalesChannelDesc { get; set; }
        public DbSet<cdSalesperson> cdSalesperson { get; set; }
        public DbSet<cdSalespersonTeam> cdSalespersonTeam { get; set; }
        public DbSet<cdSalespersonTeamDesc> cdSalespersonTeamDesc { get; set; }
        public DbSet<cdSalespersonType> cdSalespersonType { get; set; }
        public DbSet<cdSalespersonTypeDesc> cdSalespersonTypeDesc { get; set; }
        public DbSet<cdSalesPlanType> cdSalesPlanType { get; set; }
        public DbSet<cdSalesPlanTypeDesc> cdSalesPlanTypeDesc { get; set; }
        public DbSet<cdScheduleReSendSMSForCustomerRelationship> cdScheduleReSendSMSForCustomerRelationship { get; set; }
        public DbSet<cdScheduleSMSForCustomerRelationship> cdScheduleSMSForCustomerRelationship { get; set; }
        public DbSet<cdScrapReason> cdScrapReason { get; set; }
        public DbSet<cdScrapReasonDesc> cdScrapReasonDesc { get; set; }
        public DbSet<cdSeason> cdSeason { get; set; }
        public DbSet<cdSeasonDesc> cdSeasonDesc { get; set; }
        public DbSet<cdSectionType> cdSectionType { get; set; }
        public DbSet<cdSectionTypeDesc> cdSectionTypeDesc { get; set; }
        public DbSet<cdServiceman> cdServiceman { get; set; }
        public DbSet<cdSGKBorrowingType> cdSGKBorrowingType { get; set; }
        public DbSet<cdSGKBorrowingTypeDesc> cdSGKBorrowingTypeDesc { get; set; }
        public DbSet<cdSGKProfession> cdSGKProfession { get; set; }
        public DbSet<cdSGKProfessionDesc> cdSGKProfessionDesc { get; set; }
        public DbSet<cdShipmentMethod> cdShipmentMethod { get; set; }
        public DbSet<cdShipmentMethodDesc> cdShipmentMethodDesc { get; set; }
        public DbSet<cdSMSGatewayService> cdSMSGatewayService { get; set; }
        public DbSet<cdSMSJobType> cdSMSJobType { get; set; }
        public DbSet<cdSMSJobTypeDesc> cdSMSJobTypeDesc { get; set; }
        public DbSet<cdSoftware> cdSoftware { get; set; }
        public DbSet<cdSoftwareDesc> cdSoftwareDesc { get; set; }
        public DbSet<cdSoftwareType> cdSoftwareType { get; set; }
        public DbSet<cdSoftwareTypeDesc> cdSoftwareTypeDesc { get; set; }
        public DbSet<cdSpecialDayType> cdSpecialDayType { get; set; }
        public DbSet<cdSpecialDayTypeDesc> cdSpecialDayTypeDesc { get; set; }
        public DbSet<cdState> cdState { get; set; }
        public DbSet<cdStateDesc> cdStateDesc { get; set; }
        public DbSet<cdStoreCapacityLevel> cdStoreCapacityLevel { get; set; }
        public DbSet<cdStoreCapacityLevelDesc> cdStoreCapacityLevelDesc { get; set; }
        public DbSet<cdStoreClimateZone> cdStoreClimateZone { get; set; }
        public DbSet<cdStoreClimateZoneDesc> cdStoreClimateZoneDesc { get; set; }
        public DbSet<cdStoreConcept> cdStoreConcept { get; set; }
        public DbSet<cdStoreConceptDesc> cdStoreConceptDesc { get; set; }
        public DbSet<cdStoreCRMGroup> cdStoreCRMGroup { get; set; }
        public DbSet<cdStoreCRMGroupDesc> cdStoreCRMGroupDesc { get; set; }
        public DbSet<cdStoreDistributionGroup> cdStoreDistributionGroup { get; set; }
        public DbSet<cdStoreDistributionGroupDesc> cdStoreDistributionGroupDesc { get; set; }
        public DbSet<cdStoreHierarchyLevel> cdStoreHierarchyLevel { get; set; }
        public DbSet<cdStoreHierarchyLevelDesc> cdStoreHierarchyLevelDesc { get; set; }
        public DbSet<cdStorePriceLevel> cdStorePriceLevel { get; set; }
        public DbSet<cdStorePriceLevelDesc> cdStorePriceLevelDesc { get; set; }
        public DbSet<cdStoryBoard> cdStoryBoard { get; set; }
        public DbSet<cdStoryBoardDesc> cdStoryBoardDesc { get; set; }
        public DbSet<cdStreet> cdStreet { get; set; }
        public DbSet<cdSubCurrAccAttribute> cdSubCurrAccAttribute { get; set; }
        public DbSet<cdSubCurrAccAttributeDesc> cdSubCurrAccAttributeDesc { get; set; }
        public DbSet<cdSubCurrAccAttributeType> cdSubCurrAccAttributeType { get; set; }
        public DbSet<cdSubCurrAccAttributeTypeDesc> cdSubCurrAccAttributeTypeDesc { get; set; }
        public DbSet<cdSubJobDepartment> cdSubJobDepartment { get; set; }
        public DbSet<cdSubJobDepartmentDesc> cdSubJobDepartmentDesc { get; set; }
        public DbSet<cdSubSeason> cdSubSeason { get; set; }
        public DbSet<cdSubSeasonDesc> cdSubSeasonDesc { get; set; }
        public DbSet<cdSupportResolveType> cdSupportResolveType { get; set; }
        public DbSet<cdSupportResolveTypeDesc> cdSupportResolveTypeDesc { get; set; }
        public DbSet<cdSupportStatus> cdSupportStatus { get; set; }
        public DbSet<cdSupportStatusDesc> cdSupportStatusDesc { get; set; }
        public DbSet<cdSurvey> cdSurvey { get; set; }
        public DbSet<cdSurveyDesc> cdSurveyDesc { get; set; }
        public DbSet<cdSurveyQuestion> cdSurveyQuestion { get; set; }
        public DbSet<cdSurveyQuestionDesc> cdSurveyQuestionDesc { get; set; }
        public DbSet<cdSurveyQuestionOption> cdSurveyQuestionOption { get; set; }
        public DbSet<cdSurveyQuestionOptionDesc> cdSurveyQuestionOptionDesc { get; set; }
        public DbSet<cdSurveySection> cdSurveySection { get; set; }
        public DbSet<cdSurveySectionDesc> cdSurveySectionDesc { get; set; }
        public DbSet<cdTaxDistrict> cdTaxDistrict { get; set; }
        public DbSet<cdTaxDistrictDesc> cdTaxDistrictDesc { get; set; }
        public DbSet<cdTaxOffice> cdTaxOffice { get; set; }
        public DbSet<cdTaxOfficeDesc> cdTaxOfficeDesc { get; set; }
        public DbSet<cdTechnicalResponsible> cdTechnicalResponsible { get; set; }
        public DbSet<cdTest> cdTest { get; set; }
        public DbSet<cdTestDesc> cdTestDesc { get; set; }
        public DbSet<cdTestType> cdTestType { get; set; }
        public DbSet<cdTestTypeDesc> cdTestTypeDesc { get; set; }
        public DbSet<cdTextileCareSymbol> cdTextileCareSymbol { get; set; }
        public DbSet<cdTextileCareSymbolDesc> cdTextileCareSymbolDesc { get; set; }
        public DbSet<cdTimePeriod> cdTimePeriod { get; set; }
        public DbSet<cdTimePeriodDesc> cdTimePeriodDesc { get; set; }
        public DbSet<cdTitle> cdTitle { get; set; }
        public DbSet<cdTitleDesc> cdTitleDesc { get; set; }
        public DbSet<cdTradeRegistryOffice> cdTradeRegistryOffice { get; set; }
        public DbSet<cdTransactionCancelReason> cdTransactionCancelReason { get; set; }
        public DbSet<cdTransactionCancelReasonDesc> cdTransactionCancelReasonDesc { get; set; }
        public DbSet<cdTransferPlanTemplate> cdTransferPlanTemplate { get; set; }
        public DbSet<cdTransferPlanTemplateDesc> cdTransferPlanTemplateDesc { get; set; }
        public DbSet<cdTranslationProvider> cdTranslationProvider { get; set; }
        public DbSet<cdTurnoverTargetType> cdTurnoverTargetType { get; set; }
        public DbSet<cdTurnoverTargetTypeDesc> cdTurnoverTargetTypeDesc { get; set; }
        public DbSet<cdUnDeliveryReason> cdUnDeliveryReason { get; set; }
        public DbSet<cdUnDeliveryReasonDesc> cdUnDeliveryReasonDesc { get; set; }
        public DbSet<cdUniFreeTenderType> cdUniFreeTenderType { get; set; }
        public DbSet<cdUnitOfMeasure> cdUnitOfMeasure { get; set; }
        public DbSet<cdUnitOfMeasureDesc> cdUnitOfMeasureDesc { get; set; }
        public DbSet<cdUniversity> cdUniversity { get; set; }
        public DbSet<cdUniversityDesc> cdUniversityDesc { get; set; }
        public DbSet<cdUniversityFaculty> cdUniversityFaculty { get; set; }
        public DbSet<cdUniversityFacultyDep> cdUniversityFacultyDep { get; set; }
        public DbSet<cdUniversityFacultyDepDesc> cdUniversityFacultyDepDesc { get; set; }
        public DbSet<cdUniversityFacultyDesc> cdUniversityFacultyDesc { get; set; }
        public DbSet<cdUniversityLevel> cdUniversityLevel { get; set; }
        public DbSet<cdUniversityLevelDesc> cdUniversityLevelDesc { get; set; }
        public DbSet<cdUniversityType> cdUniversityType { get; set; }
        public DbSet<cdUniversityTypeDesc> cdUniversityTypeDesc { get; set; }
        public DbSet<cdUserWarning> cdUserWarning { get; set; }
        public DbSet<cdUserWarningDesc> cdUserWarningDesc { get; set; }
        public DbSet<cdUTSAttribute> cdUTSAttribute { get; set; }
        public DbSet<cdUTSMRGInfo> cdUTSMRGInfo { get; set; }
        public DbSet<cdVat> cdVat { get; set; }
        public DbSet<cdVatDesc> cdVatDesc { get; set; }
        public DbSet<cdVehicle> cdVehicle { get; set; }
        public DbSet<cdVehicleType> cdVehicleType { get; set; }
        public DbSet<cdVehicleTypeDesc> cdVehicleTypeDesc { get; set; }
        public DbSet<cdVendorPaymentPlanGr> cdVendorPaymentPlanGr { get; set; }
        public DbSet<cdVendorPaymentPlanGrDesc> cdVendorPaymentPlanGrDesc { get; set; }
        public DbSet<cdVisitFrequency> cdVisitFrequency { get; set; }
        public DbSet<cdVisitFrequencyDesc> cdVisitFrequencyDesc { get; set; }
        public DbSet<cdWageGarnishmentType> cdWageGarnishmentType { get; set; }
        public DbSet<cdWageGarnishmentTypeDesc> cdWageGarnishmentTypeDesc { get; set; }
        public DbSet<cdWagePlanType> cdWagePlanType { get; set; }
        public DbSet<cdWagePlanTypeDesc> cdWagePlanTypeDesc { get; set; }
        public DbSet<cdWarehouse> cdWarehouse { get; set; }
        public DbSet<cdWarehouseCategory> cdWarehouseCategory { get; set; }
        public DbSet<cdWarehouseCategoryDesc> cdWarehouseCategoryDesc { get; set; }
        public DbSet<cdWarehouseChannelTemplate> cdWarehouseChannelTemplate { get; set; }
        public DbSet<cdWarehouseChannelTemplateDesc> cdWarehouseChannelTemplateDesc { get; set; }
        public DbSet<cdWarehouseDesc> cdWarehouseDesc { get; set; }
        public DbSet<cdWarehouseType> cdWarehouseType { get; set; }
        public DbSet<cdWarehouseTypeDesc> cdWarehouseTypeDesc { get; set; }
        public DbSet<cdWorkForce> cdWorkForce { get; set; }
        public DbSet<cdWorkForceDesc> cdWorkForceDesc { get; set; }
        public DbSet<cdWorkPlace> cdWorkPlace { get; set; }
        public DbSet<cdWorkPlaceDesc> cdWorkPlaceDesc { get; set; }
        public DbSet<cdWorkPlaceGroup> cdWorkPlaceGroup { get; set; }
        public DbSet<cdWorkPlaceGroupDesc> cdWorkPlaceGroupDesc { get; set; }
        public DbSet<cdWorkPlaceType> cdWorkPlaceType { get; set; }
        public DbSet<cdWorkPlaceTypeDesc> cdWorkPlaceTypeDesc { get; set; }
        public DbSet<cdZone> cdZone { get; set; }
        public DbSet<cdZoneDesc> cdZoneDesc { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships for auBankPermit
            modelBuilder.Entity<auBankPermit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auBankPermit_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auBankPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auBankPermit_cdRole_FK");

                entity.HasOne(d => d.bsBankTransType)
                    .WithMany()
                    .HasForeignKey(d => d.BankTransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auBankPermit_bsBankTransType_FK");

            });

            // Configure relationships for auBasePricePermit
            modelBuilder.Entity<auBasePricePermit>(entity =>
            {
                entity.HasOne(d => d.bsBasePrice)
                    .WithMany()
                    .HasForeignKey(d => d.BasePriceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auBasePricePermit_bsBasePrice_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auBasePricePermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auBasePricePermit_cdRole_FK");

                entity.HasOne(d => d.bsItemType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auBasePricePermit_bsItemType_FK");

            });

            // Configure relationships for auCardColumnPermit
            modelBuilder.Entity<auCardColumnPermit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardColumnPermit_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardColumnPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardColumnPermit_cdRole_FK");

            });

            // Configure relationships for auCardElementPermit
            modelBuilder.Entity<auCardElementPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardElementPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardElementPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardElementPermit_cdCompany_FK");

            });

            // Configure relationships for auCardElementRequiredKey
            modelBuilder.Entity<auCardElementRequiredKey>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardElementRequiredKey_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardElementRequiredKey_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardElementRequiredKey_cdRole_FK");

            });

            // Configure relationships for auCardPermit
            modelBuilder.Entity<auCardPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCardPermit_cdCompany_FK");

            });

            // Configure relationships for auCashPermit
            modelBuilder.Entity<auCashPermit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCashPermit_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCashPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCashPermit_cdRole_FK");

                entity.HasOne(d => d.bsCashTransType)
                    .WithMany()
                    .HasForeignKey(d => d.CashTransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCashPermit_bsCashTransType_FK");

            });

            // Configure relationships for auChequeDeny
            modelBuilder.Entity<auChequeDeny>(entity =>
            {
                entity.HasOne(d => d.bsChequeType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequeDeny_bsChequeType_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequeDeny_cdCompany_FK");

                entity.HasOne(d => d.cdChequeDenyReason)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeDenyReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequeDeny_cdChequeDenyReason_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequeDeny_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequeDeny_cdCurrAcc_FK");

            });

            // Configure relationships for auChequePermit
            modelBuilder.Entity<auChequePermit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequePermit_cdCompany_FK");

                entity.HasOne(d => d.bsChequeType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequePermit_bsChequeType_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequePermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequePermit_cdRole_FK");

                entity.HasOne(d => d.bsChequeTransType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auChequePermit_bsChequeTransType_FK");

            });

            // Configure relationships for auCreditCardPaymentPermit
            modelBuilder.Entity<auCreditCardPaymentPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCreditCardPaymentPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCreditCardPaymentPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCreditCardPaymentPermit_cdCompany_FK");

            });

            // Configure relationships for auCustomTablePermit
            modelBuilder.Entity<auCustomTablePermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCustomTablePermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auCustomTablePermit_cdRole_FK");

            });

            // Configure relationships for auDebitPermit
            modelBuilder.Entity<auDebitPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auDebitPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auDebitPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auDebitPermit_cdCompany_FK");

            });

            // Configure relationships for auExpenseSlipPermit
            modelBuilder.Entity<auExpenseSlipPermit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auExpenseSlipPermit_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auExpenseSlipPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auExpenseSlipPermit_cdRole_FK");

                entity.HasOne(d => d.bsExpenseSlipType)
                    .WithMany()
                    .HasForeignKey(d => d.ExpenseSlipTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auExpenseSlipPermit_bsExpenseSlipType_FK");

            });

            // Configure relationships for auGettingDataTransferTraceLine
            modelBuilder.Entity<auGettingDataTransferTraceLine>(entity =>
            {
                entity.HasOne(d => d.auGettingDataTransferTraceHeader)
                    .WithMany()
                    .HasForeignKey(d => d.TraceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auGettingDataTransferTraceLine_auGettingDataTransferTraceHeader_FK");

            });

            // Configure relationships for auInnerProcessPermit
            modelBuilder.Entity<auInnerProcessPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auInnerProcessPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auInnerProcessPermit_cdRole_FK");

                entity.HasOne(d => d.bsInnerProcess)
                    .WithMany()
                    .HasForeignKey(d => d.InnerProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auInnerProcessPermit_bsInnerProcess_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auInnerProcessPermit_cdCompany_FK");

            });

            // Configure relationships for auItemTest
            modelBuilder.Entity<auItemTest>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auItemTest_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auItemTest_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auItemTest_cdRole_FK");

            });

            // Configure relationships for auJournalPermit
            modelBuilder.Entity<auJournalPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auJournalPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auJournalPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auJournalPermit_cdCompany_FK");

                entity.HasOne(d => d.cdJournalTypeSub)
                    .WithMany()
                    .HasForeignKey(d => d.JournalTypeSubCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auJournalPermit_cdJournalTypeSub_FK");

            });

            // Configure relationships for auMobileStorePermit
            modelBuilder.Entity<auMobileStorePermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auMobileStorePermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auMobileStorePermit_cdRole_FK");

            });

            // Configure relationships for auMobilRevenueReportPermit
            modelBuilder.Entity<auMobilRevenueReportPermit>(entity =>
            {
                entity.HasOne(d => d.dfMobilRevenueUser)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auMobilRevenueReportPermit_dfMobilRevenueUser_FK");

                entity.HasOne(d => d.dfMobilRevenueUser)
                    .WithMany()
                    .HasForeignKey(d => d.UserGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auMobilRevenueReportPermit_dfMobilRevenueUser_FK");

                entity.HasOne(d => d.dfMobilRevenueUser)
                    .WithMany()
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auMobilRevenueReportPermit_dfMobilRevenueUser_FK");

            });

            // Configure relationships for auOptInOptOutTrace
            modelBuilder.Entity<auOptInOptOutTrace>(entity =>
            {
                entity.HasOne(d => d.cdPermissionMarketingService)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdPermissionMarketingService_FK");

                entity.HasOne(d => d.cdPermissionMarketingService)
                    .WithMany()
                    .HasForeignKey(d => d.PermissionMarketingServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdPermissionMarketingService_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdCompany_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdOffice_FK");

                entity.HasOne(d => d.cdConfirmationFormType)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationFormTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdConfirmationFormType_FK");

                entity.HasOne(d => d.cdCompanyBrand)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyBrandCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdCompanyBrand_FK");

                entity.HasOne(d => d.cdCommunicationType)
                    .WithMany()
                    .HasForeignKey(d => d.CommunicationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdCommunicationType_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auOptInOptOutTrace_cdCurrAcc_FK");

            });

            // Configure relationships for auPaymentPermit
            modelBuilder.Entity<auPaymentPermit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPaymentPermit_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPaymentPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPaymentPermit_cdRole_FK");

            });

            // Configure relationships for auPriceListPermit
            modelBuilder.Entity<auPriceListPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPriceListPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPriceListPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPriceListPermit_cdCompany_FK");

                entity.HasOne(d => d.cdPriceGroup)
                    .WithMany()
                    .HasForeignKey(d => d.PriceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPriceListPermit_cdPriceGroup_FK");

            });

            // Configure relationships for auProcessFlowDeny
            modelBuilder.Entity<auProcessFlowDeny>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessFlowDeny_cdCompany_FK");

                entity.HasOne(d => d.bsProcessFlow)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessFlowCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessFlowDeny_bsProcessFlow_FK");

                entity.HasOne(d => d.bsProcess)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessFlowDeny_bsProcess_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessFlowDeny_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessFlowDeny_cdCurrAcc_FK");

            });

            // Configure relationships for auProcessPermit
            modelBuilder.Entity<auProcessPermit>(entity =>
            {
                entity.HasOne(d => d.bsProcess)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessPermit_bsProcess_FK");

                entity.HasOne(d => d.bsProcessFlow)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessFlowCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessPermit_bsProcessFlow_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessPermit_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProcessPermit_cdRole_FK");

            });

            // Configure relationships for auProformaProcessPermit
            modelBuilder.Entity<auProformaProcessPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProformaProcessPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProformaProcessPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProformaProcessPermit_cdCompany_FK");

                entity.HasOne(d => d.bsProcessFlow)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessFlowCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProformaProcessPermit_bsProcessFlow_FK");

                entity.HasOne(d => d.bsProcess)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProformaProcessPermit_bsProcess_FK");

            });

            // Configure relationships for auProgramPermit
            modelBuilder.Entity<auProgramPermit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProgramPermit_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProgramPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auProgramPermit_cdRole_FK");

            });

            // Configure relationships for auPurchaseRequisitionPermit
            modelBuilder.Entity<auPurchaseRequisitionPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPurchaseRequisitionPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPurchaseRequisitionPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPurchaseRequisitionPermit_cdCompany_FK");

            });

            // Configure relationships for auPurchaseRequisitionProposalPermit
            modelBuilder.Entity<auPurchaseRequisitionProposalPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPurchaseRequisitionProposalPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPurchaseRequisitionProposalPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auPurchaseRequisitionProposalPermit_cdCompany_FK");

            });

            // Configure relationships for auReportFilterMinMaxDateValue
            modelBuilder.Entity<auReportFilterMinMaxDateValue>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auReportFilterMinMaxDateValue_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auReportFilterMinMaxDateValue_cdRole_FK");

            });

            // Configure relationships for auReportQueryPermit
            modelBuilder.Entity<auReportQueryPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auReportQueryPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auReportQueryPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auReportQueryPermit_cdCompany_FK");

            });

            // Configure relationships for auSupportRequest
            modelBuilder.Entity<auSupportRequest>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSupportRequest_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSupportRequest_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSupportRequest_cdRole_FK");

            });

            // Configure relationships for auSurveyPermit
            modelBuilder.Entity<auSurveyPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSurveyPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSurveyPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSurveyPermit_cdCompany_FK");

            });

            // Configure relationships for auSurveySectionPermit
            modelBuilder.Entity<auSurveySectionPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSurveySectionPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSurveySectionPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSurveySectionPermit_cdCompany_FK");

                entity.HasOne(d => d.cdSurveySection)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSurveySectionPermit_cdSurveySection_FK");

                entity.HasOne(d => d.cdSurveySection)
                    .WithMany()
                    .HasForeignKey(d => d.SurveySectionNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auSurveySectionPermit_cdSurveySection_FK");

            });

            // Configure relationships for auTransactionCheckInOutTrace
            modelBuilder.Entity<auTransactionCheckInOutTrace>(entity =>
            {
                entity.HasOne(d => d.cdCheckOutReason)
                    .WithMany()
                    .HasForeignKey(d => d.CheckOutReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auTransactionCheckInOutTrace_cdCheckOutReason_FK");

            });

            // Configure relationships for auVehicleLoadingPermit
            modelBuilder.Entity<auVehicleLoadingPermit>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auVehicleLoadingPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auVehicleLoadingPermit_cdRole_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auVehicleLoadingPermit_cdCompany_FK");

            });

            // Configure relationships for auVehicleUnLoadingPermit
            modelBuilder.Entity<auVehicleUnLoadingPermit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auVehicleUnLoadingPermit_cdCompany_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auVehicleUnLoadingPermit_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auVehicleUnLoadingPermit_cdRole_FK");

            });

            // Configure relationships for bsAccountDetailDesc
            modelBuilder.Entity<bsAccountDetailDesc>(entity =>
            {
                entity.HasOne(d => d.bsAccountDetail)
                    .WithMany()
                    .HasForeignKey(d => d.AccountDetail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAccountDetailDesc_bsAccountDetail_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAccountDetailDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsAdjustCostMethodDesc
            modelBuilder.Entity<bsAdjustCostMethodDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAdjustCostMethodDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsAdjustCostMethod)
                    .WithMany()
                    .HasForeignKey(d => d.AdjustCostMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAdjustCostMethodDesc_bsAdjustCostMethod_FK");

            });

            // Configure relationships for bsAirportIATADesc
            modelBuilder.Entity<bsAirportIATADesc>(entity =>
            {
                entity.HasOne(d => d.bsAirportIATA)
                    .WithMany()
                    .HasForeignKey(d => d.AirportIATACode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAirportIATADesc_bsAirportIATA_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAirportIATADesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsAllocationRuleDesc
            modelBuilder.Entity<bsAllocationRuleDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAllocationRuleDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsAllocationRule)
                    .WithMany()
                    .HasForeignKey(d => d.AllocationRuleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAllocationRuleDesc_bsAllocationRule_FK");

            });

            // Configure relationships for bsAllocationSourceTypeDesc
            modelBuilder.Entity<bsAllocationSourceTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAllocationSourceTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsAllocationSourceType)
                    .WithMany()
                    .HasForeignKey(d => d.AllocationSourceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsAllocationSourceTypeDesc_bsAllocationSourceType_FK");

            });

            // Configure relationships for bsApplicationDesc
            modelBuilder.Entity<bsApplicationDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsApplicationDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsApplication)
                    .WithMany()
                    .HasForeignKey(d => d.ApplicationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsApplicationDesc_bsApplication_FK");

            });

            // Configure relationships for bsBadDebtResultDesc
            modelBuilder.Entity<bsBadDebtResultDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBadDebtResultDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBadDebtResult)
                    .WithMany()
                    .HasForeignKey(d => d.BadDebtResultCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBadDebtResultDesc_bsBadDebtResult_FK");

            });

            // Configure relationships for bsBadDebtTransTypeDesc
            modelBuilder.Entity<bsBadDebtTransTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsBadDebtTransType)
                    .WithMany()
                    .HasForeignKey(d => d.BadDebtTransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBadDebtTransTypeDesc_bsBadDebtTransType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBadDebtTransTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsBankAdditionalChargeTypeDesc
            modelBuilder.Entity<bsBankAdditionalChargeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankAdditionalChargeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBankAdditionalChargeType)
                    .WithMany()
                    .HasForeignKey(d => d.BankAdditionalChargeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankAdditionalChargeTypeDesc_bsBankAdditionalChargeType_FK");

            });

            // Configure relationships for bsBankCardTypeDesc
            modelBuilder.Entity<bsBankCardTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankCardTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBankCardType)
                    .WithMany()
                    .HasForeignKey(d => d.BankCardTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankCardTypeDesc_bsBankCardType_FK");

            });

            // Configure relationships for bsBankCreditGuaranteeTypeDesc
            modelBuilder.Entity<bsBankCreditGuaranteeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankCreditGuaranteeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBankCreditGuaranteeType)
                    .WithMany()
                    .HasForeignKey(d => d.BankCreditGuaranteeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankCreditGuaranteeTypeDesc_bsBankCreditGuaranteeType_FK");

            });

            // Configure relationships for bsBankPOSImportTypeDesc
            modelBuilder.Entity<bsBankPOSImportTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankPOSImportTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBankPOSImportType)
                    .WithMany()
                    .HasForeignKey(d => d.BankPOSImportTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankPOSImportTypeDesc_bsBankPOSImportType_FK");

            });

            // Configure relationships for bsBankTransTypeDesc
            modelBuilder.Entity<bsBankTransTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsBankTransType)
                    .WithMany()
                    .HasForeignKey(d => d.BankTransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankTransTypeDesc_bsBankTransType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBankTransTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsBasePriceDesc
            modelBuilder.Entity<bsBasePriceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBasePriceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBasePrice)
                    .WithMany()
                    .HasForeignKey(d => d.BasePriceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBasePriceDesc_bsBasePrice_FK");

            });

            // Configure relationships for bsBOMEntityLevelDesc
            modelBuilder.Entity<bsBOMEntityLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBOMEntityLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBOMEntityLevel)
                    .WithMany()
                    .HasForeignKey(d => d.BOMEntityLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBOMEntityLevelDesc_bsBOMEntityLevel_FK");

            });

            // Configure relationships for bsBrowseMethodTypeDesc
            modelBuilder.Entity<bsBrowseMethodTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBrowseMethodTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBrowseMethodType)
                    .WithMany()
                    .HasForeignKey(d => d.BrowseMethodTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBrowseMethodTypeDesc_bsBrowseMethodType_FK");

            });

            // Configure relationships for bsBudgetDetailDesc
            modelBuilder.Entity<bsBudgetDetailDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBudgetDetailDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBudgetDetail)
                    .WithMany()
                    .HasForeignKey(d => d.BudgetDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBudgetDetailDesc_bsBudgetDetail_FK");

            });

            // Configure relationships for bsBulkMailServiceProviderDesc
            modelBuilder.Entity<bsBulkMailServiceProviderDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBulkMailServiceProviderDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBulkMailServiceProvider)
                    .WithMany()
                    .HasForeignKey(d => d.BulkMailServiceProviderCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsBulkMailServiceProviderDesc_bsBulkMailServiceProvider_FK");

            });

            // Configure relationships for bsCashTransTypeDesc
            modelBuilder.Entity<bsCashTransTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCashTransTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCashTransType)
                    .WithMany()
                    .HasForeignKey(d => d.CashTransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCashTransTypeDesc_bsCashTransType_FK");

            });

            // Configure relationships for bsChannelTypeDesc
            modelBuilder.Entity<bsChannelTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsChannelTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsChannelType)
                    .WithMany()
                    .HasForeignKey(d => d.ChannelTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsChannelTypeDesc_bsChannelType_FK");

            });

            // Configure relationships for bsChequeTransType
            modelBuilder.Entity<bsChequeTransType>(entity =>
            {
                entity.HasOne(d => d.bsChequeType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsChequeTransType_bsChequeType_FK");

            });

            // Configure relationships for bsChequeTransTypeDesc
            modelBuilder.Entity<bsChequeTransTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsChequeTransTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsChequeTransType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsChequeTransTypeDesc_bsChequeTransType_FK");

            });

            // Configure relationships for bsChequeTypeDesc
            modelBuilder.Entity<bsChequeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsChequeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsChequeType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsChequeTypeDesc_bsChequeType_FK");

            });

            // Configure relationships for bsCommunicationKindDesc
            modelBuilder.Entity<bsCommunicationKindDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCommunicationKindDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCommunicationKind)
                    .WithMany()
                    .HasForeignKey(d => d.CommunicationKindCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCommunicationKindDesc_bsCommunicationKind_FK");

            });

            // Configure relationships for bsConfirmationRuleType
            modelBuilder.Entity<bsConfirmationRuleType>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsConfirmationRuleType_cdCompany_FK");

            });

            // Configure relationships for bsConfirmationRuleTypeDesc
            modelBuilder.Entity<bsConfirmationRuleTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsConfirmationRuleType)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsConfirmationRuleTypeDesc_bsConfirmationRuleType_FK");

                entity.HasOne(d => d.bsConfirmationRuleType)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationRuleTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsConfirmationRuleTypeDesc_bsConfirmationRuleType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsConfirmationRuleTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsConfirmationStatusDesc
            modelBuilder.Entity<bsConfirmationStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsConfirmationStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsConfirmationStatus)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsConfirmationStatusDesc_bsConfirmationStatus_FK");

            });

            // Configure relationships for bsConfirmationTypeDesc
            modelBuilder.Entity<bsConfirmationTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsConfirmationType)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsConfirmationTypeDesc_bsConfirmationType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsConfirmationTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsContractTypeDesc
            modelBuilder.Entity<bsContractTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsContractTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsContractType)
                    .WithMany()
                    .HasForeignKey(d => d.ContractTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsContractTypeDesc_bsContractType_FK");

            });

            // Configure relationships for bsCostingLevelDesc
            modelBuilder.Entity<bsCostingLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCostingLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCostingLevel)
                    .WithMany()
                    .HasForeignKey(d => d.CostingLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCostingLevelDesc_bsCostingLevel_FK");

            });

            // Configure relationships for bsCostingMethodDesc
            modelBuilder.Entity<bsCostingMethodDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCostingMethodDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCostingMethod)
                    .WithMany()
                    .HasForeignKey(d => d.CostingMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCostingMethodDesc_bsCostingMethod_FK");

            });

            // Configure relationships for bsCostingVariantLevelDesc
            modelBuilder.Entity<bsCostingVariantLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCostingVariantLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCostingVariantLevel)
                    .WithMany()
                    .HasForeignKey(d => d.CostingVariantLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCostingVariantLevelDesc_bsCostingVariantLevel_FK");

            });

            // Configure relationships for bsCreditCardPaymentTypeDesc
            modelBuilder.Entity<bsCreditCardPaymentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCreditCardPaymentTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCreditCardPaymentType)
                    .WithMany()
                    .HasForeignKey(d => d.CreditCardPaymentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCreditCardPaymentTypeDesc_bsCreditCardPaymentType_FK");

            });

            // Configure relationships for bsCreditTypeDesc
            modelBuilder.Entity<bsCreditTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCreditTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCreditType)
                    .WithMany()
                    .HasForeignKey(d => d.CreditTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCreditTypeDesc_bsCreditType_FK");

            });

            // Configure relationships for bsCurrAccTypeDesc
            modelBuilder.Entity<bsCurrAccTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCurrAccTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCurrAccType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCurrAccTypeDesc_bsCurrAccType_FK");

            });

            // Configure relationships for bsCustomerTypeDesc
            modelBuilder.Entity<bsCustomerTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCustomerTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCustomerType)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCustomerTypeDesc_bsCustomerType_FK");

            });

            // Configure relationships for bsCustomsProductGroup
            modelBuilder.Entity<bsCustomsProductGroup>(entity =>
            {
                entity.HasOne(d => d.cdUnitOfMeasure)
                    .WithMany()
                    .HasForeignKey(d => d.UnitOfMeasureCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCustomsProductGroup_cdUnitOfMeasure_FK");

            });

            // Configure relationships for bsCustomsProductGroupDesc
            modelBuilder.Entity<bsCustomsProductGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCustomsProductGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsCustomsProductGroup)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsProductGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsCustomsProductGroupDesc_bsCustomsProductGroup_FK");

            });

            // Configure relationships for bsDayDesc
            modelBuilder.Entity<bsDayDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDayDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDay)
                    .WithMany()
                    .HasForeignKey(d => d.DayCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDayDesc_bsDay_FK");

            });

            // Configure relationships for bsDebitTypeDesc
            modelBuilder.Entity<bsDebitTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDebitTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDebitType)
                    .WithMany()
                    .HasForeignKey(d => d.DebitTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDebitTypeDesc_bsDebitType_FK");

            });

            // Configure relationships for bsDebtStatusTypeDesc
            modelBuilder.Entity<bsDebtStatusTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDebtStatusTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDebtStatusType)
                    .WithMany()
                    .HasForeignKey(d => d.DebtStatusTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDebtStatusTypeDesc_bsDebtStatusType_FK");

            });

            // Configure relationships for bsDeclarationCapacityDesc
            modelBuilder.Entity<bsDeclarationCapacityDesc>(entity =>
            {
                entity.HasOne(d => d.bsDeclarationCapacity)
                    .WithMany()
                    .HasForeignKey(d => d.DeclarationCapacityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDeclarationCapacityDesc_bsDeclarationCapacity_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDeclarationCapacityDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsDeclarationTypeDesc
            modelBuilder.Entity<bsDeclarationTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDeclarationTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDeclarationType)
                    .WithMany()
                    .HasForeignKey(d => d.DeclarationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDeclarationTypeDesc_bsDeclarationType_FK");

            });

            // Configure relationships for bsDepreciationMethodDesc
            modelBuilder.Entity<bsDepreciationMethodDesc>(entity =>
            {
                entity.HasOne(d => d.bsDepreciationMethod)
                    .WithMany()
                    .HasForeignKey(d => d.DepreciationMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDepreciationMethodDesc_bsDepreciationMethod_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDepreciationMethodDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsDeviceDesc
            modelBuilder.Entity<bsDeviceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDeviceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDevice)
                    .WithMany()
                    .HasForeignKey(d => d.DeviceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDeviceDesc_bsDevice_FK");

            });

            // Configure relationships for bsDeviceTypeDesc
            modelBuilder.Entity<bsDeviceTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsDeviceType)
                    .WithMany()
                    .HasForeignKey(d => d.DeviceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDeviceTypeDesc_bsDeviceType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDeviceTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsDiscountLevelOfUseDesc
            modelBuilder.Entity<bsDiscountLevelOfUseDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountLevelOfUseDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDiscountLevelOfUse)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountLevelOfUseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountLevelOfUseDesc_bsDiscountLevelOfUse_FK");

            });

            // Configure relationships for bsDiscountOfferApplyDesc
            modelBuilder.Entity<bsDiscountOfferApplyDesc>(entity =>
            {
                entity.HasOne(d => d.bsDiscountOfferApply)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferApplyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferApplyDesc_bsDiscountOfferApply_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferApplyDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsDiscountOfferMethod
            modelBuilder.Entity<bsDiscountOfferMethod>(entity =>
            {
                entity.HasOne(d => d.bsDiscountOfferType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferMethod_bsDiscountOfferType_FK");

            });

            // Configure relationships for bsDiscountOfferMethodDesc
            modelBuilder.Entity<bsDiscountOfferMethodDesc>(entity =>
            {
                entity.HasOne(d => d.bsDiscountOfferMethod)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferMethodDesc_bsDiscountOfferMethod_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferMethodDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsDiscountOfferStageDesc
            modelBuilder.Entity<bsDiscountOfferStageDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferStageDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDiscountOfferStage)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferStageCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferStageDesc_bsDiscountOfferStage_FK");

            });

            // Configure relationships for bsDiscountOfferTypeDesc
            modelBuilder.Entity<bsDiscountOfferTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsDiscountOfferType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferTypeDesc_bsDiscountOfferType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountOfferTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsDiscountVoucherBaseDesc
            modelBuilder.Entity<bsDiscountVoucherBaseDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountVoucherBaseDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDiscountVoucherBase)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountVoucherBaseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDiscountVoucherBaseDesc_bsDiscountVoucherBase_FK");

            });

            // Configure relationships for bsDispOrderTypeDesc
            modelBuilder.Entity<bsDispOrderTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsDispOrderType)
                    .WithMany()
                    .HasForeignKey(d => d.DispOrderTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDispOrderTypeDesc_bsDispOrderType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDispOrderTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsDocumentTypeDesc
            modelBuilder.Entity<bsDocumentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDocumentTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsDocumentType)
                    .WithMany()
                    .HasForeignKey(d => d.DocumentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsDocumentTypeDesc_bsDocumentType_FK");

            });

            // Configure relationships for bsEasyStartupStepsDesc
            modelBuilder.Entity<bsEasyStartupStepsDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEasyStartupStepsDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsEasyStartupSteps)
                    .WithMany()
                    .HasForeignKey(d => d.EasyStartupStepCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEasyStartupStepsDesc_bsEasyStartupSteps_FK");

            });

            // Configure relationships for bsEditMask
            modelBuilder.Entity<bsEditMask>(entity =>
            {
                entity.HasOne(d => d.bsCommunicationKind)
                    .WithMany()
                    .HasForeignKey(d => d.CommunicationKindCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEditMask_bsCommunicationKind_FK");

            });

            // Configure relationships for bsEditMaskDesc
            modelBuilder.Entity<bsEditMaskDesc>(entity =>
            {
                entity.HasOne(d => d.bsEditMask)
                    .WithMany()
                    .HasForeignKey(d => d.EditMaskCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEditMaskDesc_bsEditMask_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEditMaskDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsEInvoiceStatus
            modelBuilder.Entity<bsEInvoiceStatus>(entity =>
            {
                entity.HasOne(d => d.bsInvoiceType)
                    .WithMany()
                    .HasForeignKey(d => d.InvoiceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEInvoiceStatus_bsInvoiceType_FK");

            });

            // Configure relationships for bsEInvoiceStatusDesc
            modelBuilder.Entity<bsEInvoiceStatusDesc>(entity =>
            {
                entity.HasOne(d => d.bsEInvoiceStatus)
                    .WithMany()
                    .HasForeignKey(d => d.EInvoiceStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEInvoiceStatusDesc_bsEInvoiceStatus_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEInvoiceStatusDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsEmailTypeDesc
            modelBuilder.Entity<bsEmailTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEmailTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsEmailType)
                    .WithMany()
                    .HasForeignKey(d => d.EmailTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEmailTypeDesc_bsEmailType_FK");

            });

            // Configure relationships for bsEmployeePayTypeDesc
            modelBuilder.Entity<bsEmployeePayTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEmployeePayTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsEmployeePayType)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeePayTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEmployeePayTypeDesc_bsEmployeePayType_FK");

            });

            // Configure relationships for bsEmployeeSpecialTypeDesc
            modelBuilder.Entity<bsEmployeeSpecialTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsEmployeeSpecialType)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeSpecialTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEmployeeSpecialTypeDesc_bsEmployeeSpecialType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEmployeeSpecialTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsEShipmentStatusDesc
            modelBuilder.Entity<bsEShipmentStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEShipmentStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsEShipmentStatus)
                    .WithMany()
                    .HasForeignKey(d => d.EShipmentStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEShipmentStatusDesc_bsEShipmentStatus_FK");

            });

            // Configure relationships for bsExpenseSlipTypeDesc
            modelBuilder.Entity<bsExpenseSlipTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsExpenseSlipTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsExpenseSlipType)
                    .WithMany()
                    .HasForeignKey(d => d.ExpenseSlipTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsExpenseSlipTypeDesc_bsExpenseSlipType_FK");

            });

            // Configure relationships for bsEyeGlassSutTypeDesc
            modelBuilder.Entity<bsEyeGlassSutTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEyeGlassSutTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsEyeGlassSutType)
                    .WithMany()
                    .HasForeignKey(d => d.EyeGlassSutTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsEyeGlassSutTypeDesc_bsEyeGlassSutType_FK");

            });

            // Configure relationships for bsFastDeliveryCompanyDesc
            modelBuilder.Entity<bsFastDeliveryCompanyDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsFastDeliveryCompanyDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsFastDeliveryCompany)
                    .WithMany()
                    .HasForeignKey(d => d.FastDeliveryCompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsFastDeliveryCompanyDesc_bsFastDeliveryCompany_FK");

            });

            // Configure relationships for bsFileFormatTypeDesc
            modelBuilder.Entity<bsFileFormatTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsFileFormatTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsFileFormatType)
                    .WithMany()
                    .HasForeignKey(d => d.FileFormatTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsFileFormatTypeDesc_bsFileFormatType_FK");

            });

            // Configure relationships for bsFolderDesc
            modelBuilder.Entity<bsFolderDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsFolderDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsFolder)
                    .WithMany()
                    .HasForeignKey(d => d.FolderCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsFolderDesc_bsFolder_FK");

            });

            // Configure relationships for bsFormatTypeDesc
            modelBuilder.Entity<bsFormatTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsFormatTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsFormatType)
                    .WithMany()
                    .HasForeignKey(d => d.FormatTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsFormatTypeDesc_bsFormatType_FK");

            });

            // Configure relationships for bsGenderDesc
            modelBuilder.Entity<bsGenderDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsGenderDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsGender)
                    .WithMany()
                    .HasForeignKey(d => d.GenderCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsGenderDesc_bsGender_FK");

            });

            // Configure relationships for bsGiftCardPaymentTypeDesc
            modelBuilder.Entity<bsGiftCardPaymentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsGiftCardPaymentTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsGiftCardPaymentType)
                    .WithMany()
                    .HasForeignKey(d => d.GiftCardPaymentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsGiftCardPaymentTypeDesc_bsGiftCardPaymentType_FK");

            });

            // Configure relationships for bsIncompleteDownPaymentDistributionTypeDesc
            modelBuilder.Entity<bsIncompleteDownPaymentDistributionTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsIncompleteDownPaymentDistributionTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsIncompleteDownPaymentDistributionType)
                    .WithMany()
                    .HasForeignKey(d => d.IncompleteDownPaymentDistributionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsIncompleteDownPaymentDistributionTypeDesc_bsIncompleteDownPaymentDistributionType_FK");

            });

            // Configure relationships for bsIncotermDesc
            modelBuilder.Entity<bsIncotermDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsIncotermDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsIncoterm)
                    .WithMany()
                    .HasForeignKey(d => d.IncotermCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsIncotermDesc_bsIncoterm_FK");

            });

            // Configure relationships for bsInnerOrderTypeDesc
            modelBuilder.Entity<bsInnerOrderTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsInnerOrderType)
                    .WithMany()
                    .HasForeignKey(d => d.InnerOrderTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInnerOrderTypeDesc_bsInnerOrderType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInnerOrderTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsInnerProcess
            modelBuilder.Entity<bsInnerProcess>(entity =>
            {
                entity.HasOne(d => d.bsTransType)
                    .WithMany()
                    .HasForeignKey(d => d.TransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInnerProcess_bsTransType_FK");

            });

            // Configure relationships for bsInnerProcessDesc
            modelBuilder.Entity<bsInnerProcessDesc>(entity =>
            {
                entity.HasOne(d => d.bsInnerProcess)
                    .WithMany()
                    .HasForeignKey(d => d.InnerProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInnerProcessDesc_bsInnerProcess_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInnerProcessDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsInvoiceReturnTypeDesc
            modelBuilder.Entity<bsInvoiceReturnTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInvoiceReturnTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsInvoiceReturnType)
                    .WithMany()
                    .HasForeignKey(d => d.InvoiceReturnTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInvoiceReturnTypeDesc_bsInvoiceReturnType_FK");

            });

            // Configure relationships for bsInvoiceTypeDesc
            modelBuilder.Entity<bsInvoiceTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInvoiceTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsInvoiceType)
                    .WithMany()
                    .HasForeignKey(d => d.InvoiceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsInvoiceTypeDesc_bsInvoiceType_FK");

            });

            // Configure relationships for bsItemDimTypeDesc
            modelBuilder.Entity<bsItemDimTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsItemDimType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDimTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsItemDimTypeDesc_bsItemDimType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsItemDimTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsItemProcessPermitTypeDesc
            modelBuilder.Entity<bsItemProcessPermitTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsItemProcessPermitTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsItemProcessPermitType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemProcessPermitTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsItemProcessPermitTypeDesc_bsItemProcessPermitType_FK");

            });

            // Configure relationships for bsItemTypeDesc
            modelBuilder.Entity<bsItemTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsItemType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsItemTypeDesc_bsItemType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsItemTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsJournalTypeDesc
            modelBuilder.Entity<bsJournalTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsJournalTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsJournalType)
                    .WithMany()
                    .HasForeignKey(d => d.JournalTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsJournalTypeDesc_bsJournalType_FK");

            });

            // Configure relationships for bsLensTypeDesc
            modelBuilder.Entity<bsLensTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsLensTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsLensType)
                    .WithMany()
                    .HasForeignKey(d => d.LensTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsLensTypeDesc_bsLensType_FK");

            });

            // Configure relationships for bsLetterOfGuaranteeTypeDesc
            modelBuilder.Entity<bsLetterOfGuaranteeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsLetterOfGuaranteeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsLetterOfGuaranteeType)
                    .WithMany()
                    .HasForeignKey(d => d.LetterOfGuaranteeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsLetterOfGuaranteeTypeDesc_bsLetterOfGuaranteeType_FK");

            });

            // Configure relationships for bsLinkedProductTypeDesc
            modelBuilder.Entity<bsLinkedProductTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsLinkedProductType)
                    .WithMany()
                    .HasForeignKey(d => d.LinkedProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsLinkedProductTypeDesc_bsLinkedProductType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsLinkedProductTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsLoyaltyProgramProcessDesc
            modelBuilder.Entity<bsLoyaltyProgramProcessDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsLoyaltyProgramProcessDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsLoyaltyProgramProcess)
                    .WithMany()
                    .HasForeignKey(d => d.LoyaltyProgramProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsLoyaltyProgramProcessDesc_bsLoyaltyProgramProcess_FK");

            });

            // Configure relationships for bsMessageImportanceDesc
            modelBuilder.Entity<bsMessageImportanceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsMessageImportanceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsMessageImportance)
                    .WithMany()
                    .HasForeignKey(d => d.MessageImportanceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsMessageImportanceDesc_bsMessageImportance_FK");

            });

            // Configure relationships for bsNebimV3ServicesDesc
            modelBuilder.Entity<bsNebimV3ServicesDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsNebimV3ServicesDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsNebimV3Services)
                    .WithMany()
                    .HasForeignKey(d => d.NebimV3ServicesCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsNebimV3ServicesDesc_bsNebimV3Services_FK");

            });

            // Configure relationships for bsNebimV3WindowsServicesDesc
            modelBuilder.Entity<bsNebimV3WindowsServicesDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsNebimV3WindowsServicesDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsNebimV3WindowsServices)
                    .WithMany()
                    .HasForeignKey(d => d.NebimV3WindowsServicesCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsNebimV3WindowsServicesDesc_bsNebimV3WindowsServices_FK");

            });

            // Configure relationships for bsOrderDeliveryRecordTypeDesc
            modelBuilder.Entity<bsOrderDeliveryRecordTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsOrderDeliveryRecordTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsOrderDeliveryRecordType)
                    .WithMany()
                    .HasForeignKey(d => d.OrderDeliveryRecordTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsOrderDeliveryRecordTypeDesc_bsOrderDeliveryRecordType_FK");

            });

            // Configure relationships for bsOrderTypeDesc
            modelBuilder.Entity<bsOrderTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsOrderTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsOrderType)
                    .WithMany()
                    .HasForeignKey(d => d.OrderTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsOrderTypeDesc_bsOrderType_FK");

            });

            // Configure relationships for bsOtherPaymentTypeDesc
            modelBuilder.Entity<bsOtherPaymentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsOtherPaymentType)
                    .WithMany()
                    .HasForeignKey(d => d.OtherPaymentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsOtherPaymentTypeDesc_bsOtherPaymentType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsOtherPaymentTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsPackagingTypeDesc
            modelBuilder.Entity<bsPackagingTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPackagingTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPackagingType)
                    .WithMany()
                    .HasForeignKey(d => d.PackagingTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPackagingTypeDesc_bsPackagingType_FK");

            });

            // Configure relationships for bsPaymentMeansDesc
            modelBuilder.Entity<bsPaymentMeansDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPaymentMeansDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPaymentMeans)
                    .WithMany()
                    .HasForeignKey(d => d.PaymentMeansCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPaymentMeansDesc_bsPaymentMeans_FK");

            });

            // Configure relationships for bsPaymentTypeDesc
            modelBuilder.Entity<bsPaymentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPaymentTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPaymentType)
                    .WithMany()
                    .HasForeignKey(d => d.PaymentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPaymentTypeDesc_bsPaymentType_FK");

            });

            // Configure relationships for bsPayTypeDesc
            modelBuilder.Entity<bsPayTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsPayType)
                    .WithMany()
                    .HasForeignKey(d => d.PayTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPayTypeDesc_bsPayType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPayTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsPickingTypeDesc
            modelBuilder.Entity<bsPickingTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPickingTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPickingType)
                    .WithMany()
                    .HasForeignKey(d => d.PickingTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPickingTypeDesc_bsPickingType_FK");

            });

            // Configure relationships for bsPointBaseDesc
            modelBuilder.Entity<bsPointBaseDesc>(entity =>
            {
                entity.HasOne(d => d.bsPointBase)
                    .WithMany()
                    .HasForeignKey(d => d.PointBaseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPointBaseDesc_bsPointBase_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPointBaseDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsPointRecordTypeDesc
            modelBuilder.Entity<bsPointRecordTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPointRecordTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPointRecordType)
                    .WithMany()
                    .HasForeignKey(d => d.PointRecordTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPointRecordTypeDesc_bsPointRecordType_FK");

            });

            // Configure relationships for bsPolicyCustomerEditDesc
            modelBuilder.Entity<bsPolicyCustomerEditDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPolicyCustomerEditDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPolicyCustomerEdit)
                    .WithMany()
                    .HasForeignKey(d => d.PolicyCustomerEdit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPolicyCustomerEditDesc_bsPolicyCustomerEdit_FK");

            });

            // Configure relationships for bsPolicyCustomerPaymentDesc
            modelBuilder.Entity<bsPolicyCustomerPaymentDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPolicyCustomerPaymentDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPolicyCustomerPayment)
                    .WithMany()
                    .HasForeignKey(d => d.PolicyCustomerPayment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPolicyCustomerPaymentDesc_bsPolicyCustomerPayment_FK");

            });

            // Configure relationships for bsPolicyCustomerSharingDesc
            modelBuilder.Entity<bsPolicyCustomerSharingDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPolicyCustomerSharingDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPolicyCustomerSharing)
                    .WithMany()
                    .HasForeignKey(d => d.PolicyCustomerSharing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPolicyCustomerSharingDesc_bsPolicyCustomerSharing_FK");

            });

            // Configure relationships for bsPolicyVendorSharingDesc
            modelBuilder.Entity<bsPolicyVendorSharingDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPolicyVendorSharingDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPolicyVendorSharing)
                    .WithMany()
                    .HasForeignKey(d => d.PolicyVendorSharing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPolicyVendorSharingDesc_bsPolicyVendorSharing_FK");

            });

            // Configure relationships for bsPOSModeDesc
            modelBuilder.Entity<bsPOSModeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPOSModeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPOSMode)
                    .WithMany()
                    .HasForeignKey(d => d.POSModeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPOSModeDesc_bsPOSMode_FK");

            });

            // Configure relationships for bsPostAccTypeDesc
            modelBuilder.Entity<bsPostAccTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPostAccTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPostAccType)
                    .WithMany()
                    .HasForeignKey(d => d.PostAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPostAccTypeDesc_bsPostAccType_FK");

            });

            // Configure relationships for bsPresentCardActivationProcessDesc
            modelBuilder.Entity<bsPresentCardActivationProcessDesc>(entity =>
            {
                entity.HasOne(d => d.bsPresentCardActivationProcess)
                    .WithMany()
                    .HasForeignKey(d => d.ActivationProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPresentCardActivationProcessDesc_bsPresentCardActivationProcess_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPresentCardActivationProcessDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsPresentCardActivationStatusDesc
            modelBuilder.Entity<bsPresentCardActivationStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPresentCardActivationStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsPresentCardActivationStatus)
                    .WithMany()
                    .HasForeignKey(d => d.ActivationStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPresentCardActivationStatusDesc_bsPresentCardActivationStatus_FK");

            });

            // Configure relationships for bsPresentCardActivationTypeDesc
            modelBuilder.Entity<bsPresentCardActivationTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsPresentCardActivationType)
                    .WithMany()
                    .HasForeignKey(d => d.PresentCardActivationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPresentCardActivationTypeDesc_bsPresentCardActivationType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsPresentCardActivationTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsProcess
            modelBuilder.Entity<bsProcess>(entity =>
            {
                entity.HasOne(d => d.bsTransType)
                    .WithMany()
                    .HasForeignKey(d => d.TransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsProcess_bsTransType_FK");

            });

            // Configure relationships for bsProcessDesc
            modelBuilder.Entity<bsProcessDesc>(entity =>
            {
                entity.HasOne(d => d.bsProcess)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsProcessDesc_bsProcess_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsProcessDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsProcessFlowDesc
            modelBuilder.Entity<bsProcessFlowDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsProcessFlowDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsProcessFlow)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessFlowCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsProcessFlowDesc_bsProcessFlow_FK");

            });

            // Configure relationships for bsProductTypeDesc
            modelBuilder.Entity<bsProductTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsProductType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsProductTypeDesc_bsProductType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsProductTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsQuestionInputTypeDesc
            modelBuilder.Entity<bsQuestionInputTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsQuestionInputTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsQuestionInputType)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionInputTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsQuestionInputTypeDesc_bsQuestionInputType_FK");

            });

            // Configure relationships for bsReconciliationType
            modelBuilder.Entity<bsReconciliationType>(entity =>
            {
                entity.HasOne(d => d.bsCurrAccType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsReconciliationType_bsCurrAccType_FK");

            });

            // Configure relationships for bsReconciliationTypeDesc
            modelBuilder.Entity<bsReconciliationTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsReconciliationTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsReconciliationType)
                    .WithMany()
                    .HasForeignKey(d => d.ReconciliationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsReconciliationTypeDesc_bsReconciliationType_FK");

            });

            // Configure relationships for bsReserveTypeDesc
            modelBuilder.Entity<bsReserveTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsReserveType)
                    .WithMany()
                    .HasForeignKey(d => d.ReserveTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsReserveTypeDesc_bsReserveType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsReserveTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsSGKInsuaranceTypeDesc
            modelBuilder.Entity<bsSGKInsuaranceTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSGKInsuaranceTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsSGKInsuaranceType)
                    .WithMany()
                    .HasForeignKey(d => d.SGKInsuaranceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSGKInsuaranceTypeDesc_bsSGKInsuaranceType_FK");

            });

            // Configure relationships for bsSGKMissionDesc
            modelBuilder.Entity<bsSGKMissionDesc>(entity =>
            {
                entity.HasOne(d => d.bsSGKMission)
                    .WithMany()
                    .HasForeignKey(d => d.SGKMissionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSGKMissionDesc_bsSGKMission_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSGKMissionDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsSGKWorkPlaceSectorDesc
            modelBuilder.Entity<bsSGKWorkPlaceSectorDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSGKWorkPlaceSectorDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsSGKWorkPlaceSector)
                    .WithMany()
                    .HasForeignKey(d => d.SGKWorkPlaceSectorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSGKWorkPlaceSectorDesc_bsSGKWorkPlaceSector_FK");

            });

            // Configure relationships for bsShipmentTypeDesc
            modelBuilder.Entity<bsShipmentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsShipmentTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsShipmentType)
                    .WithMany()
                    .HasForeignKey(d => d.ShipmentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsShipmentTypeDesc_bsShipmentType_FK");

            });

            // Configure relationships for bsSMSStatusDesc
            modelBuilder.Entity<bsSMSStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSMSStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsSMSStatus)
                    .WithMany()
                    .HasForeignKey(d => d.SMSStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSMSStatusDesc_bsSMSStatus_FK");

            });

            // Configure relationships for bsStandardBarcodeTypeDesc
            modelBuilder.Entity<bsStandardBarcodeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsStandardBarcodeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsStandardBarcodeType)
                    .WithMany()
                    .HasForeignKey(d => d.StandardBarcodeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsStandardBarcodeTypeDesc_bsStandardBarcodeType_FK");

            });

            // Configure relationships for bsSupportTypeDesc
            modelBuilder.Entity<bsSupportTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSupportTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsSupportType)
                    .WithMany()
                    .HasForeignKey(d => d.SupportTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsSupportTypeDesc_bsSupportType_FK");

            });

            // Configure relationships for bsTaxExemptionDesc
            modelBuilder.Entity<bsTaxExemptionDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxExemptionDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsTaxExemption)
                    .WithMany()
                    .HasForeignKey(d => d.TaxExemptionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxExemptionDesc_bsTaxExemption_FK");

            });

            // Configure relationships for bsTaxFreeRefundCompany
            modelBuilder.Entity<bsTaxFreeRefundCompany>(entity =>
            {
                entity.HasOne(d => d.cdDistrict)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxFreeRefundCompany_cdDistrict_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.StreetCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxFreeRefundCompany_cdStreet_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxFreeRefundCompany_cdStreet_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.QuarterCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxFreeRefundCompany_cdStreet_FK");

                entity.HasOne(d => d.cdTaxOffice)
                    .WithMany()
                    .HasForeignKey(d => d.TaxOfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxFreeRefundCompany_cdTaxOffice_FK");

                entity.HasOne(d => d.cdCountry)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxFreeRefundCompany_cdCountry_FK");

                entity.HasOne(d => d.cdCity)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxFreeRefundCompany_cdCity_FK");

                entity.HasOne(d => d.cdState)
                    .WithMany()
                    .HasForeignKey(d => d.StateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxFreeRefundCompany_cdState_FK");

            });

            // Configure relationships for bsTaxPaymentAccTypeDesc
            modelBuilder.Entity<bsTaxPaymentAccTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsTaxPaymentAccType)
                    .WithMany()
                    .HasForeignKey(d => d.TaxPaymentAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxPaymentAccTypeDesc_bsTaxPaymentAccType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxPaymentAccTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsTaxPaymentTypeDesc
            modelBuilder.Entity<bsTaxPaymentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxPaymentTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsTaxPaymentType)
                    .WithMany()
                    .HasForeignKey(d => d.TaxPaymentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxPaymentTypeDesc_bsTaxPaymentType_FK");

            });

            // Configure relationships for bsTaxTypeDesc
            modelBuilder.Entity<bsTaxTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsTaxType)
                    .WithMany()
                    .HasForeignKey(d => d.TaxTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTaxTypeDesc_bsTaxType_FK");

            });

            // Configure relationships for bsTextileCareSymbolGrDesc
            modelBuilder.Entity<bsTextileCareSymbolGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTextileCareSymbolGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsTextileCareSymbolGr)
                    .WithMany()
                    .HasForeignKey(d => d.TextileCareSymbolGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTextileCareSymbolGrDesc_bsTextileCareSymbolGr_FK");

            });

            // Configure relationships for bsTransferPlanRuleDesc
            modelBuilder.Entity<bsTransferPlanRuleDesc>(entity =>
            {
                entity.HasOne(d => d.bsTransferPlanRule)
                    .WithMany()
                    .HasForeignKey(d => d.TransferPlanRuleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTransferPlanRuleDesc_bsTransferPlanRule_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTransferPlanRuleDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsTransportModeDesc
            modelBuilder.Entity<bsTransportModeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTransportModeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsTransportMode)
                    .WithMany()
                    .HasForeignKey(d => d.TransportModeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTransportModeDesc_bsTransportMode_FK");

            });

            // Configure relationships for bsTransTypeDesc
            modelBuilder.Entity<bsTransTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTransTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsTransType)
                    .WithMany()
                    .HasForeignKey(d => d.TransTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsTransTypeDesc_bsTransType_FK");

            });

            // Configure relationships for bsVendorTypeDesc
            modelBuilder.Entity<bsVendorTypeDesc>(entity =>
            {
                entity.HasOne(d => d.bsVendorType)
                    .WithMany()
                    .HasForeignKey(d => d.VendorTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsVendorTypeDesc_bsVendorType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsVendorTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for bsWarehouseOwnerDesc
            modelBuilder.Entity<bsWarehouseOwnerDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsWarehouseOwnerDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsWarehouseOwner)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseOwnerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsWarehouseOwnerDesc_bsWarehouseOwner_FK");

            });

            // Configure relationships for bsWorkDangerLevelDesc
            modelBuilder.Entity<bsWorkDangerLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsWorkDangerLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsWorkDangerLevel)
                    .WithMany()
                    .HasForeignKey(d => d.WorkDangerLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsWorkDangerLevelDesc_bsWorkDangerLevel_FK");

            });

            // Configure relationships for bsWorkplaceKindDesc
            modelBuilder.Entity<bsWorkplaceKindDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsWorkplaceKindDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.bsWorkplaceKind)
                    .WithMany()
                    .HasForeignKey(d => d.WorkplaceKindCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsWorkplaceKindDesc_bsWorkplaceKind_FK");

            });

            // Configure relationships for bsWorkplacePropertyStatusDesc
            modelBuilder.Entity<bsWorkplacePropertyStatusDesc>(entity =>
            {
                entity.HasOne(d => d.bsWorkplacePropertyStatus)
                    .WithMany()
                    .HasForeignKey(d => d.WorkplacePropertyStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsWorkplacePropertyStatusDesc_bsWorkplacePropertyStatus_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bsWorkplacePropertyStatusDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdAccountant
            modelBuilder.Entity<cdAccountant>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAccountant_cdCompany_FK");

            });

            // Configure relationships for cdAccountantDesc
            modelBuilder.Entity<cdAccountantDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAccountantDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAccountantDesc_cdAccountant_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.AccountantCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAccountantDesc_cdAccountant_FK");

            });

            // Configure relationships for cdAddressShareCompanyWebServiceDesc
            modelBuilder.Entity<cdAddressShareCompanyWebServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAddressShareCompanyWebServiceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdAddressShareCompanyWebService)
                    .WithMany()
                    .HasForeignKey(d => d.AddressShareCompanyWebServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAddressShareCompanyWebServiceDesc_cdAddressShareCompanyWebService_FK");

            });

            // Configure relationships for cdAddressTypeDesc
            modelBuilder.Entity<cdAddressTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAddressTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdAddressType)
                    .WithMany()
                    .HasForeignKey(d => d.AddressTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAddressTypeDesc_cdAddressType_FK");

            });

            // Configure relationships for cdAllocationTemplate
            modelBuilder.Entity<cdAllocationTemplate>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAllocationTemplate_cdCompany_FK");

                entity.HasOne(d => d.cdWarehouse)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAllocationTemplate_cdWarehouse_FK");

                entity.HasOne(d => d.bsAllocationRule)
                    .WithMany()
                    .HasForeignKey(d => d.AllocationRuleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAllocationTemplate_bsAllocationRule_FK");

                entity.HasOne(d => d.bsAllocationSourceType)
                    .WithMany()
                    .HasForeignKey(d => d.AllocationSourceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAllocationTemplate_bsAllocationSourceType_FK");

            });

            // Configure relationships for cdAllocationTemplateDesc
            modelBuilder.Entity<cdAllocationTemplateDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAllocationTemplateDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdAllocationTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAllocationTemplateDesc_cdAllocationTemplate_FK");

                entity.HasOne(d => d.cdAllocationTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.AllocationTemplateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAllocationTemplateDesc_cdAllocationTemplate_FK");

            });

            // Configure relationships for cdAmountRule
            modelBuilder.Entity<cdAmountRule>(entity =>
            {
                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAmountRule_cdCurrency_FK");

            });

            // Configure relationships for cdAmountRuleDesc
            modelBuilder.Entity<cdAmountRuleDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAmountRuleDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdAmountRule)
                    .WithMany()
                    .HasForeignKey(d => d.AmountRuleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdAmountRuleDesc_cdAmountRule_FK");

            });

            // Configure relationships for cdATAttribute
            modelBuilder.Entity<cdATAttribute>(entity =>
            {
                entity.HasOne(d => d.cdATAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdATAttribute_cdATAttributeType_FK");

            });

            // Configure relationships for cdATAttributeDesc
            modelBuilder.Entity<cdATAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdATAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdATAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdATAttributeDesc_cdATAttribute_FK");

                entity.HasOne(d => d.cdATAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdATAttributeDesc_cdATAttribute_FK");

            });

            // Configure relationships for cdATAttributeTypeDesc
            modelBuilder.Entity<cdATAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdATAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdATAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdATAttributeTypeDesc_cdATAttributeType_FK");

            });

            // Configure relationships for cdBadDebtLetterTypeDesc
            modelBuilder.Entity<cdBadDebtLetterTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBadDebtLetterTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBadDebtLetterType)
                    .WithMany()
                    .HasForeignKey(d => d.BadDebtLetterTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBadDebtLetterTypeDesc_cdBadDebtLetterType_FK");

            });

            // Configure relationships for cdBadDebtReasonDesc
            modelBuilder.Entity<cdBadDebtReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBadDebtReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBadDebtReason)
                    .WithMany()
                    .HasForeignKey(d => d.BadDebtReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBadDebtReasonDesc_cdBadDebtReason_FK");

            });

            // Configure relationships for cdBankAccTypeDesc
            modelBuilder.Entity<cdBankAccTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdBankAccType)
                    .WithMany()
                    .HasForeignKey(d => d.BankAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankAccTypeDesc_cdBankAccType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankAccTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdBankCreditType
            modelBuilder.Entity<cdBankCreditType>(entity =>
            {
                entity.HasOne(d => d.bsCreditType)
                    .WithMany()
                    .HasForeignKey(d => d.CreditTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankCreditType_bsCreditType_FK");

            });

            // Configure relationships for cdBankCreditTypeDesc
            modelBuilder.Entity<cdBankCreditTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankCreditTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBankCreditType)
                    .WithMany()
                    .HasForeignKey(d => d.BankCreditTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankCreditTypeDesc_cdBankCreditType_FK");

            });

            // Configure relationships for cdBankDesc
            modelBuilder.Entity<cdBankDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBank)
                    .WithMany()
                    .HasForeignKey(d => d.BankCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankDesc_cdBank_FK");

            });

            // Configure relationships for cdBankOpTypeDesc
            modelBuilder.Entity<cdBankOpTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdBankOpType)
                    .WithMany()
                    .HasForeignKey(d => d.BankOpTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankOpTypeDesc_cdBankOpType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBankOpTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdBarcodeType
            modelBuilder.Entity<cdBarcodeType>(entity =>
            {
                entity.HasOne(d => d.bsStandardBarcodeType)
                    .WithMany()
                    .HasForeignKey(d => d.StandardBarcodeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBarcodeType_bsStandardBarcodeType_FK");

            });

            // Configure relationships for cdBarcodeTypeDesc
            modelBuilder.Entity<cdBarcodeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBarcodeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBarcodeType)
                    .WithMany()
                    .HasForeignKey(d => d.BarcodeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBarcodeTypeDesc_cdBarcodeType_FK");

            });

            // Configure relationships for cdBaseMaterial
            modelBuilder.Entity<cdBaseMaterial>(entity =>
            {
                entity.HasOne(d => d.bsProductType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBaseMaterial_bsProductType_FK");

            });

            // Configure relationships for cdBaseMaterialDesc
            modelBuilder.Entity<cdBaseMaterialDesc>(entity =>
            {
                entity.HasOne(d => d.cdBaseMaterial)
                    .WithMany()
                    .HasForeignKey(d => d.BaseMaterialCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBaseMaterialDesc_cdBaseMaterial_FK");

                entity.HasOne(d => d.cdBaseMaterial)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBaseMaterialDesc_cdBaseMaterial_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBaseMaterialDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdBatchDesc
            modelBuilder.Entity<cdBatchDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBatchDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBatch)
                    .WithMany()
                    .HasForeignKey(d => d.BatchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBatchDesc_cdBatch_FK");

            });

            // Configure relationships for cdBatchGroupDesc
            modelBuilder.Entity<cdBatchGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdBatchGroup)
                    .WithMany()
                    .HasForeignKey(d => d.BatchGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBatchGroupDesc_cdBatchGroup_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBatchGroupDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdBloodTypeDesc
            modelBuilder.Entity<cdBloodTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBloodTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBloodType)
                    .WithMany()
                    .HasForeignKey(d => d.BloodTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBloodTypeDesc_cdBloodType_FK");

            });

            // Configure relationships for cdBOM
            modelBuilder.Entity<cdBOM>(entity =>
            {
                entity.HasOne(d => d.cdBOMTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.BOMTemplateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOM_cdBOMTemplate_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOM_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOM_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ColorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOM_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDim1Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOM_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDim2Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOM_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDim3Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOM_prItemVariant_FK");

            });

            // Configure relationships for cdBOMDesc
            modelBuilder.Entity<cdBOMDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBOM)
                    .WithMany()
                    .HasForeignKey(d => d.BOMCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMDesc_cdBOM_FK");

            });

            // Configure relationships for cdBOMEntityDesc
            modelBuilder.Entity<cdBOMEntityDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMEntityDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBOMEntity)
                    .WithMany()
                    .HasForeignKey(d => d.BOMEntityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMEntityDesc_cdBOMEntity_FK");

            });

            // Configure relationships for cdBOMTemplate
            modelBuilder.Entity<cdBOMTemplate>(entity =>
            {
                entity.HasOne(d => d.cdBOMEntity)
                    .WithMany()
                    .HasForeignKey(d => d.BOMEntityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplate_cdBOMEntity_FK");

            });

            // Configure relationships for cdBOMTemplateAttribute
            modelBuilder.Entity<cdBOMTemplateAttribute>(entity =>
            {
                entity.HasOne(d => d.cdItemAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.ItemAttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttribute_cdItemAttribute_FK");

                entity.HasOne(d => d.cdItemAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttribute_cdItemAttribute_FK");

                entity.HasOne(d => d.cdItemAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttribute_cdItemAttribute_FK");

                entity.HasOne(d => d.cdBOMTemplateAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttribute_cdBOMTemplateAttributeType_FK");

            });

            // Configure relationships for cdBOMTemplateAttributeDesc
            modelBuilder.Entity<cdBOMTemplateAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBOMTemplateAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttributeDesc_cdBOMTemplateAttribute_FK");

                entity.HasOne(d => d.cdBOMTemplateAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttributeDesc_cdBOMTemplateAttribute_FK");

            });

            // Configure relationships for cdBOMTemplateAttributeTypeDesc
            modelBuilder.Entity<cdBOMTemplateAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBOMTemplateAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateAttributeTypeDesc_cdBOMTemplateAttributeType_FK");

            });

            // Configure relationships for cdBOMTemplateDesc
            modelBuilder.Entity<cdBOMTemplateDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBOMTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.BOMTemplateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBOMTemplateDesc_cdBOMTemplate_FK");

            });

            // Configure relationships for cdBrand
            modelBuilder.Entity<cdBrand>(entity =>
            {
                entity.HasOne(d => d.bsProductType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBrand_bsProductType_FK");

            });

            // Configure relationships for cdBrandDesc
            modelBuilder.Entity<cdBrandDesc>(entity =>
            {
                entity.HasOne(d => d.cdBrand)
                    .WithMany()
                    .HasForeignKey(d => d.BrandCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBrandDesc_cdBrand_FK");

                entity.HasOne(d => d.cdBrand)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBrandDesc_cdBrand_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBrandDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdBudgetType
            modelBuilder.Entity<cdBudgetType>(entity =>
            {
                entity.HasOne(d => d.bsBudgetDetail)
                    .WithMany()
                    .HasForeignKey(d => d.BudgetDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBudgetType_bsBudgetDetail_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBudgetType_cdCurrency_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBudgetType_cdCompany_FK");

            });

            // Configure relationships for cdBudgetTypeDesc
            modelBuilder.Entity<cdBudgetTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdBudgetType)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBudgetTypeDesc_cdBudgetType_FK");

                entity.HasOne(d => d.cdBudgetType)
                    .WithMany()
                    .HasForeignKey(d => d.BudgetTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBudgetTypeDesc_cdBudgetType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBudgetTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdBusinessGroupDesc
            modelBuilder.Entity<cdBusinessGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBusinessGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdBusinessGroup)
                    .WithMany()
                    .HasForeignKey(d => d.BusinessGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdBusinessGroupDesc_cdBusinessGroup_FK");

            });

            // Configure relationships for cdCareWarningDesc
            modelBuilder.Entity<cdCareWarningDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCareWarningDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCareWarning)
                    .WithMany()
                    .HasForeignKey(d => d.CareWarningCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCareWarningDesc_cdCareWarning_FK");

            });

            // Configure relationships for cdCareWarningTemplateDesc
            modelBuilder.Entity<cdCareWarningTemplateDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCareWarningTemplateDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCareWarningTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.CareWarningTemplateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCareWarningTemplateDesc_cdCareWarningTemplate_FK");

            });

            // Configure relationships for cdChannelTemplate
            modelBuilder.Entity<cdChannelTemplate>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChannelTemplate_cdCompany_FK");

            });

            // Configure relationships for cdChannelTemplateDesc
            modelBuilder.Entity<cdChannelTemplateDesc>(entity =>
            {
                entity.HasOne(d => d.cdChannelTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChannelTemplateDesc_cdChannelTemplate_FK");

                entity.HasOne(d => d.cdChannelTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.ChannelTemplateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChannelTemplateDesc_cdChannelTemplate_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChannelTemplateDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCheckOutReasonDesc
            modelBuilder.Entity<cdCheckOutReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheckOutReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCheckOutReason)
                    .WithMany()
                    .HasForeignKey(d => d.CheckOutReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheckOutReasonDesc_cdCheckOutReason_FK");

            });

            // Configure relationships for cdCheque
            modelBuilder.Entity<cdCheque>(entity =>
            {
                entity.HasOne(d => d.bsChequeType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_bsChequeType_FK");

                entity.HasOne(d => d.cdCity)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdCity_FK");

                entity.HasOne(d => d.cdState)
                    .WithMany()
                    .HasForeignKey(d => d.StateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdState_FK");

                entity.HasOne(d => d.prBankBranch)
                    .WithMany()
                    .HasForeignKey(d => d.BankCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_prBankBranch_FK");

                entity.HasOne(d => d.prBankBranch)
                    .WithMany()
                    .HasForeignKey(d => d.BankBranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_prBankBranch_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdCompany_FK");

                entity.HasOne(d => d.cdCountry)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdCountry_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdCurrency_FK");

                entity.HasOne(d => d.cdDistrict)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdDistrict_FK");

                entity.HasOne(d => d.cdTaxOffice)
                    .WithMany()
                    .HasForeignKey(d => d.TaxOfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdTaxOffice_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.BankCurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.BankCurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCheque_cdCurrAcc_FK");

            });

            // Configure relationships for cdChequeAttribute
            modelBuilder.Entity<cdChequeAttribute>(entity =>
            {
                entity.HasOne(d => d.cdChequeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttribute_cdChequeAttributeType_FK");

                entity.HasOne(d => d.cdChequeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttribute_cdChequeAttributeType_FK");

            });

            // Configure relationships for cdChequeAttributeDesc
            modelBuilder.Entity<cdChequeAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdChequeAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttributeDesc_cdChequeAttribute_FK");

                entity.HasOne(d => d.cdChequeAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttributeDesc_cdChequeAttribute_FK");

                entity.HasOne(d => d.cdChequeAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttributeDesc_cdChequeAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdChequeAttributeType
            modelBuilder.Entity<cdChequeAttributeType>(entity =>
            {
                entity.HasOne(d => d.bsChequeType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttributeType_bsChequeType_FK");

            });

            // Configure relationships for cdChequeAttributeTypeDesc
            modelBuilder.Entity<cdChequeAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdChequeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttributeTypeDesc_cdChequeAttributeType_FK");

                entity.HasOne(d => d.cdChequeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttributeTypeDesc_cdChequeAttributeType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeAttributeTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdChequeDenyReasonDesc
            modelBuilder.Entity<cdChequeDenyReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeDenyReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdChequeDenyReason)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeDenyReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeDenyReasonDesc_cdChequeDenyReason_FK");

            });

            // Configure relationships for cdChequeDesc
            modelBuilder.Entity<cdChequeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCheque)
                    .WithMany()
                    .HasForeignKey(d => d.BankBranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeDesc_cdCheque_FK");

                entity.HasOne(d => d.cdCheque)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeDesc_cdCheque_FK");

                entity.HasOne(d => d.cdCheque)
                    .WithMany()
                    .HasForeignKey(d => d.ChequeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeDesc_cdCheque_FK");

                entity.HasOne(d => d.cdCheque)
                    .WithMany()
                    .HasForeignKey(d => d.BankCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdChequeDesc_cdCheque_FK");

            });

            // Configure relationships for cdCity
            modelBuilder.Entity<cdCity>(entity =>
            {
                entity.HasOne(d => d.cdState)
                    .WithMany()
                    .HasForeignKey(d => d.StateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCity_cdState_FK");

            });

            // Configure relationships for cdCityDesc
            modelBuilder.Entity<cdCityDesc>(entity =>
            {
                entity.HasOne(d => d.cdCity)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCityDesc_cdCity_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCityDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCoatingTypeDesc
            modelBuilder.Entity<cdCoatingTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCoatingTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCoatingType)
                    .WithMany()
                    .HasForeignKey(d => d.CoatingTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCoatingTypeDesc_cdCoatingType_FK");

            });

            // Configure relationships for cdCollectionDesc
            modelBuilder.Entity<cdCollectionDesc>(entity =>
            {
                entity.HasOne(d => d.cdCollection)
                    .WithMany()
                    .HasForeignKey(d => d.CollectionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCollectionDesc_cdCollection_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCollectionDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdColor
            modelBuilder.Entity<cdColor>(entity =>
            {
                entity.HasOne(d => d.cdColorCatalog)
                    .WithMany()
                    .HasForeignKey(d => d.ColorCatalogCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColor_cdColorCatalog_FK");

                entity.HasOne(d => d.cdColorCatalog)
                    .WithMany()
                    .HasForeignKey(d => d.ColorCatalogCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColor_cdColorCatalog_FK");

                entity.HasOne(d => d.cdColorCatalog)
                    .WithMany()
                    .HasForeignKey(d => d.ColorCatalogCode3)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColor_cdColorCatalog_FK");

            });

            // Configure relationships for cdColorCatalogDesc
            modelBuilder.Entity<cdColorCatalogDesc>(entity =>
            {
                entity.HasOne(d => d.cdColorCatalog)
                    .WithMany()
                    .HasForeignKey(d => d.ColorCatalogCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorCatalogDesc_cdColorCatalog_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorCatalogDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdColorDesc
            modelBuilder.Entity<cdColorDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdColor)
                    .WithMany()
                    .HasForeignKey(d => d.ColorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorDesc_cdColor_FK");

            });

            // Configure relationships for cdColorGroupDesc
            modelBuilder.Entity<cdColorGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdColorGroup)
                    .WithMany()
                    .HasForeignKey(d => d.ColorGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorGroupDesc_cdColorGroup_FK");

            });

            // Configure relationships for cdColorThemeAttribute
            modelBuilder.Entity<cdColorThemeAttribute>(entity =>
            {
                entity.HasOne(d => d.cdColorThemeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorThemeAttribute_cdColorThemeAttributeType_FK");

            });

            // Configure relationships for cdColorThemeAttributeDesc
            modelBuilder.Entity<cdColorThemeAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdColorThemeAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorThemeAttributeDesc_cdColorThemeAttribute_FK");

                entity.HasOne(d => d.cdColorThemeAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorThemeAttributeDesc_cdColorThemeAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorThemeAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdColorThemeAttributeTypeDesc
            modelBuilder.Entity<cdColorThemeAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorThemeAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdColorThemeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorThemeAttributeTypeDesc_cdColorThemeAttributeType_FK");

            });

            // Configure relationships for cdColorThemeDesc
            modelBuilder.Entity<cdColorThemeDesc>(entity =>
            {
                entity.HasOne(d => d.cdColorTheme)
                    .WithMany()
                    .HasForeignKey(d => d.ColorThemeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorThemeDesc_cdColorTheme_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorThemeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdColorTypeDesc
            modelBuilder.Entity<cdColorTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdColorType)
                    .WithMany()
                    .HasForeignKey(d => d.ColorTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdColorTypeDesc_cdColorType_FK");

            });

            // Configure relationships for cdCommercialRoleDesc
            modelBuilder.Entity<cdCommercialRoleDesc>(entity =>
            {
                entity.HasOne(d => d.cdCommercialRole)
                    .WithMany()
                    .HasForeignKey(d => d.CommercialRoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCommercialRoleDesc_cdCommercialRole_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCommercialRoleDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCommunicationType
            modelBuilder.Entity<cdCommunicationType>(entity =>
            {
                entity.HasOne(d => d.bsEditMask)
                    .WithMany()
                    .HasForeignKey(d => d.EditMaskCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCommunicationType_bsEditMask_FK");

                entity.HasOne(d => d.bsCommunicationKind)
                    .WithMany()
                    .HasForeignKey(d => d.CommunicationKindCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCommunicationType_bsCommunicationKind_FK");

            });

            // Configure relationships for cdCommunicationTypeDesc
            modelBuilder.Entity<cdCommunicationTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCommunicationTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCommunicationType)
                    .WithMany()
                    .HasForeignKey(d => d.CommunicationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCommunicationTypeDesc_cdCommunicationType_FK");

            });

            // Configure relationships for cdCompany
            modelBuilder.Entity<cdCompany>(entity =>
            {
                entity.HasOne(d => d.cdZone)
                    .WithMany()
                    .HasForeignKey(d => d.ZoneCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompany_cdZone_FK");

            });

            // Configure relationships for cdCompanyBrandDesc
            modelBuilder.Entity<cdCompanyBrandDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyBrandDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCompanyBrand)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyBrandCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyBrandDesc_cdCompanyBrand_FK");

            });

            // Configure relationships for cdCompanyCreditCard
            modelBuilder.Entity<cdCompanyCreditCard>(entity =>
            {
                entity.HasOne(d => d.cdGLAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCard_cdGLAcc_FK");

                entity.HasOne(d => d.cdGLAcc)
                    .WithMany()
                    .HasForeignKey(d => d.PointGLAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCard_cdGLAcc_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCard_cdCompany_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCard_cdOffice_FK");

                entity.HasOne(d => d.cdCreditCardType)
                    .WithMany()
                    .HasForeignKey(d => d.CreditCardTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCard_cdCreditCardType_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.BankCurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCard_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.BankCurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCard_cdCurrAcc_FK");

            });

            // Configure relationships for cdCompanyCreditCardDesc
            modelBuilder.Entity<cdCompanyCreditCardDesc>(entity =>
            {
                entity.HasOne(d => d.cdCompanyCreditCard)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCreditCardCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCardDesc_cdCompanyCreditCard_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCompanyCreditCardDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdConditionTypeDesc
            modelBuilder.Entity<cdConditionTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConditionTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdConditionType)
                    .WithMany()
                    .HasForeignKey(d => d.ConditionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConditionTypeDesc_cdConditionType_FK");

            });

            // Configure relationships for cdConfirmationFormStatusDesc
            modelBuilder.Entity<cdConfirmationFormStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationFormStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdConfirmationFormStatus)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationFormStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationFormStatusDesc_cdConfirmationFormStatus_FK");

            });

            // Configure relationships for cdConfirmationFormType
            modelBuilder.Entity<cdConfirmationFormType>(entity =>
            {
                entity.HasOne(d => d.bsConsentSource)
                    .WithMany()
                    .HasForeignKey(d => d.ConsentSource)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationFormType_bsConsentSource_FK");

                entity.HasOne(d => d.cdConfirmationFormStatus)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationFormStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationFormType_cdConfirmationFormStatus_FK");

            });

            // Configure relationships for cdConfirmationFormTypeDesc
            modelBuilder.Entity<cdConfirmationFormTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdConfirmationFormType)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationFormTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationFormTypeDesc_cdConfirmationFormType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationFormTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdConfirmationReasonDesc
            modelBuilder.Entity<cdConfirmationReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdConfirmationReason)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationReasonDesc_cdConfirmationReason_FK");

            });

            // Configure relationships for cdConfirmationRule
            modelBuilder.Entity<cdConfirmationRule>(entity =>
            {
                entity.HasOne(d => d.bsConfirmationRuleType)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationRule_bsConfirmationRuleType_FK");

                entity.HasOne(d => d.bsConfirmationRuleType)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationRuleTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdConfirmationRule_bsConfirmationRuleType_FK");

            });

            // Configure relationships for cdContactTypeDesc
            modelBuilder.Entity<cdContactTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContactTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdContactType)
                    .WithMany()
                    .HasForeignKey(d => d.ContactTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContactTypeDesc_cdContactType_FK");

            });

            // Configure relationships for cdContainerTypeDesc
            modelBuilder.Entity<cdContainerTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContainerTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdContainerType)
                    .WithMany()
                    .HasForeignKey(d => d.ContainerTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContainerTypeDesc_cdContainerType_FK");

            });

            // Configure relationships for cdContractContentDesc
            modelBuilder.Entity<cdContractContentDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContractContentDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdContractContent)
                    .WithMany()
                    .HasForeignKey(d => d.ContractTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContractContentDesc_cdContractContent_FK");

                entity.HasOne(d => d.cdContractContent)
                    .WithMany()
                    .HasForeignKey(d => d.ContractContentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContractContentDesc_cdContractContent_FK");

            });

            // Configure relationships for cdContractStatusDesc
            modelBuilder.Entity<cdContractStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContractStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdContractStatus)
                    .WithMany()
                    .HasForeignKey(d => d.ContractStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdContractStatusDesc_cdContractStatus_FK");

            });

            // Configure relationships for cdCostCenterAttribute
            modelBuilder.Entity<cdCostCenterAttribute>(entity =>
            {
                entity.HasOne(d => d.cdCostCenterAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostCenterAttribute_cdCostCenterAttributeType_FK");

            });

            // Configure relationships for cdCostCenterAttributeDesc
            modelBuilder.Entity<cdCostCenterAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdCostCenterAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostCenterAttributeDesc_cdCostCenterAttribute_FK");

                entity.HasOne(d => d.cdCostCenterAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostCenterAttributeDesc_cdCostCenterAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostCenterAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCostCenterAttributeTypeDesc
            modelBuilder.Entity<cdCostCenterAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostCenterAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCostCenterAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostCenterAttributeTypeDesc_cdCostCenterAttributeType_FK");

            });

            // Configure relationships for cdCostCenterDesc
            modelBuilder.Entity<cdCostCenterDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostCenterDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCostCenter)
                    .WithMany()
                    .HasForeignKey(d => d.CostCenterCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostCenterDesc_cdCostCenter_FK");

            });

            // Configure relationships for cdCostOfGoodsSoldPeriodDesc
            modelBuilder.Entity<cdCostOfGoodsSoldPeriodDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostOfGoodsSoldPeriodDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCostOfGoodsSoldPeriod)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostOfGoodsSoldPeriodDesc_cdCostOfGoodsSoldPeriod_FK");

                entity.HasOne(d => d.cdCostOfGoodsSoldPeriod)
                    .WithMany()
                    .HasForeignKey(d => d.CostOfGoodsSoldPeriodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCostOfGoodsSoldPeriodDesc_cdCostOfGoodsSoldPeriod_FK");

            });

            // Configure relationships for cdCountry
            modelBuilder.Entity<cdCountry>(entity =>
            {
                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCountry_cdCurrency_FK");

                entity.HasOne(d => d.bsCountryISO)
                    .WithMany()
                    .HasForeignKey(d => d.CountryISOCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCountry_bsCountryISO_FK");

            });

            // Configure relationships for cdCountryDesc
            modelBuilder.Entity<cdCountryDesc>(entity =>
            {
                entity.HasOne(d => d.cdCountry)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCountryDesc_cdCountry_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCountryDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCreditCardType
            modelBuilder.Entity<cdCreditCardType>(entity =>
            {
                entity.HasOne(d => d.bsEditMask)
                    .WithMany()
                    .HasForeignKey(d => d.EditMaskCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCreditCardType_bsEditMask_FK");

                entity.HasOne(d => d.bsBankCardType)
                    .WithMany()
                    .HasForeignKey(d => d.BankCardTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCreditCardType_bsBankCardType_FK");

                entity.HasOne(d => d.cdBank)
                    .WithMany()
                    .HasForeignKey(d => d.BankCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCreditCardType_cdBank_FK");

            });

            // Configure relationships for cdCreditCardTypeDesc
            modelBuilder.Entity<cdCreditCardTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdCreditCardType)
                    .WithMany()
                    .HasForeignKey(d => d.CreditCardTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCreditCardTypeDesc_cdCreditCardType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCreditCardTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCreditSurveyor
            modelBuilder.Entity<cdCreditSurveyor>(entity =>
            {
                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCreditSurveyor_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCreditSurveyor_cdCurrAcc_FK");

            });

            // Configure relationships for cdCurrAcc
            modelBuilder.Entity<cdCurrAcc>(entity =>
            {
                 entity.HasKey(e => e.CurrAccCode);
                entity.HasOne(d => d.cdCustomerMarkupGr)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerMarkupGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdCustomerMarkupGr_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdCurrency_FK");

                entity.HasOne(d => d.cdCostCenter)
                    .WithMany()
                    .HasForeignKey(d => d.CostCenterCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdCostCenter_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.DataLanguageCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerPaymentPlanGr)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerPaymentPlanGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdCustomerPaymentPlanGr_FK");

                entity.HasOne(d => d.cdCurrAccLotGr)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccLotGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdCurrAccLotGr_FK");

                entity.HasOne(d => d.cdTaxOffice)
                    .WithMany()
                    .HasForeignKey(d => d.TaxOfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdTaxOffice_FK");

                entity.HasOne(d => d.dfStoreHierarchy)
                    .WithMany()
                    .HasForeignKey(d => d.StoreHierarchyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_dfStoreHierarchy_FK");

                entity.HasOne(d => d.cdCustomerDiscountGr)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerDiscountGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdCustomerDiscountGr_FK");

                entity.HasOne(d => d.cdBarcodeType)
                    .WithMany()
                    .HasForeignKey(d => d.BarcodeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdBarcodeType_FK");

                entity.HasOne(d => d.cdGLType)
                    .WithMany()
                    .HasForeignKey(d => d.GLTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdGLType_FK");

                entity.HasOne(d => d.prBankBranch)
                    .WithMany()
                    .HasForeignKey(d => d.BankCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_prBankBranch_FK");

                entity.HasOne(d => d.prBankBranch)
                    .WithMany()
                    .HasForeignKey(d => d.BankBranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_prBankBranch_FK");

                entity.HasOne(d => d.cdPromotionGroup)
                    .WithMany()  // WithMany() içine parametre verme
                    .HasForeignKey(d => d.PromotionGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdPromotionGroup_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdOffice_FK");

                entity.HasOne(d => d.cdBankAccType)
                    .WithMany()
                    .HasForeignKey(d => d.BankAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdBankAccType_FK");

                entity.HasOne(d => d.cdSalesChannel)
                    .WithMany()
                    .HasForeignKey(d => d.SalesChannelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdSalesChannel_FK");

                entity.HasOne(d => d.cdExchangeType)
                    .WithMany()
                    .HasForeignKey(d => d.ExchangeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdExchangeType_FK");

                entity.HasOne(d => d.bsVendorType)
                    .WithMany()
                    .HasForeignKey(d => d.VendorTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_bsVendorType_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdCompany_FK");

                entity.HasOne(d => d.cdVendorPaymentPlanGr)
                    .WithMany()
                    .HasForeignKey(d => d.VendorPaymentPlanGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdVendorPaymentPlanGr_FK");

                entity.HasOne(d => d.cdConfirmationRule)
                    .WithMany()
                    .HasForeignKey(d => d.EInvoiceConfirmationRuleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdConfirmationRule_FK");

                entity.HasOne(d => d.bsCurrAccType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_bsCurrAccType_FK");

                entity.HasOne(d => d.cdDueDateFormula)
                    .WithMany()
                    .HasForeignKey(d => d.DueDateFormulaCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdDueDateFormula_FK");

                entity.HasOne(d => d.cdPriceGroup)
                    .WithMany()
                    .HasForeignKey(d => d.RetailSalePriceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdPriceGroup_FK");

                entity.HasOne(d => d.cdPriceGroup)
                    .WithMany()
                    .HasForeignKey(d => d.WholesalePriceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdPriceGroup_FK");

                entity.HasOne(d => d.cdTitle)
                    .WithMany()
                    .HasForeignKey(d => d.TitleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_cdTitle_FK");

                entity.HasOne(d => d.bsCustomerType)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAcc_bsCustomerType_FK");

            });

            // Configure relationships for cdCurrAccAttribute
            modelBuilder.Entity<cdCurrAccAttribute>(entity =>
            {
                entity.HasOne(d => d.cdCurrAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttribute_cdCurrAccAttributeType_FK");

                entity.HasOne(d => d.cdCurrAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttribute_cdCurrAccAttributeType_FK");

            });

            // Configure relationships for cdCurrAccAttributeDesc
            modelBuilder.Entity<cdCurrAccAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCurrAccAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttributeDesc_cdCurrAccAttribute_FK");

                entity.HasOne(d => d.cdCurrAccAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttributeDesc_cdCurrAccAttribute_FK");

                entity.HasOne(d => d.cdCurrAccAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttributeDesc_cdCurrAccAttribute_FK");

            });

            // Configure relationships for cdCurrAccAttributeType
            modelBuilder.Entity<cdCurrAccAttributeType>(entity =>
            {
                entity.HasOne(d => d.bsCurrAccType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttributeType_bsCurrAccType_FK");

            });

            // Configure relationships for cdCurrAccAttributeTypeDesc
            modelBuilder.Entity<cdCurrAccAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCurrAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttributeTypeDesc_cdCurrAccAttributeType_FK");

                entity.HasOne(d => d.cdCurrAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccAttributeTypeDesc_cdCurrAccAttributeType_FK");

            });

            // Configure relationships for cdCurrAccDesc
            modelBuilder.Entity<cdCurrAccDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccDesc_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccDesc_cdCurrAcc_FK");

            });

            // Configure relationships for cdCurrAccList
            modelBuilder.Entity<cdCurrAccList>(entity =>
            {
                entity.HasOne(d => d.cdCompanyBrand)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyBrandCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccList_cdCompanyBrand_FK");

            });

            // Configure relationships for cdCurrAccListDesc
            modelBuilder.Entity<cdCurrAccListDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccListDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCurrAccList)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccListCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccListDesc_cdCurrAccList_FK");

            });

            // Configure relationships for cdCurrAccLotGrDesc
            modelBuilder.Entity<cdCurrAccLotGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccLotGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCurrAccLotGr)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccLotGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrAccLotGrDesc_cdCurrAccLotGr_FK");

            });

            // Configure relationships for cdCurrencyDesc
            modelBuilder.Entity<cdCurrencyDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrencyDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCurrencyDesc_cdCurrency_FK");

            });

            // Configure relationships for cdCustomerAlertColorDesc
            modelBuilder.Entity<cdCustomerAlertColorDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerAlertColorDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerAlertColor)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerAlertColorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerAlertColorDesc_cdCustomerAlertColor_FK");

            });

            // Configure relationships for cdCustomerCompanyBrandAttribute
            modelBuilder.Entity<cdCustomerCompanyBrandAttribute>(entity =>
            {
                entity.HasOne(d => d.cdCustomerCompanyBrandAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCompanyBrandAttribute_cdCustomerCompanyBrandAttributeType_FK");

            });

            // Configure relationships for cdCustomerCompanyBrandAttributeDesc
            modelBuilder.Entity<cdCustomerCompanyBrandAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCompanyBrandAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerCompanyBrandAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCompanyBrandAttributeDesc_cdCustomerCompanyBrandAttribute_FK");

                entity.HasOne(d => d.cdCustomerCompanyBrandAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCompanyBrandAttributeDesc_cdCustomerCompanyBrandAttribute_FK");

            });

            // Configure relationships for cdCustomerCompanyBrandAttributeTypeDesc
            modelBuilder.Entity<cdCustomerCompanyBrandAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCompanyBrandAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerCompanyBrandAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCompanyBrandAttributeTypeDesc_cdCustomerCompanyBrandAttributeType_FK");

            });

            // Configure relationships for cdCustomerConversationResultDesc
            modelBuilder.Entity<cdCustomerConversationResultDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerConversationResultDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerConversationResult)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerConversationResultCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerConversationResultDesc_cdCustomerConversationResult_FK");

            });

            // Configure relationships for cdCustomerConversationSubjectDesc
            modelBuilder.Entity<cdCustomerConversationSubjectDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerConversationSubjectDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerConversationSubject)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerConversationSubjectCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerConversationSubjectDesc_cdCustomerConversationSubject_FK");

            });

            // Configure relationships for cdCustomerConversationSubjectDetailDesc
            modelBuilder.Entity<cdCustomerConversationSubjectDetailDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerConversationSubjectDetailDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerConversationSubjectDetail)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerConversationSubjectDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerConversationSubjectDetailDesc_cdCustomerConversationSubjectDetail_FK");

            });

            // Configure relationships for cdCustomerConversationSubtitleDesc
            modelBuilder.Entity<cdCustomerConversationSubtitleDesc>(entity =>
            {
                entity.HasOne(d => d.cdCustomerConversationSubtitle)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerConversationSubtitleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerConversationSubtitleDesc_cdCustomerConversationSubtitle_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerConversationSubtitleDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCustomerCRMGroup
            modelBuilder.Entity<cdCustomerCRMGroup>(entity =>
            {
                entity.HasOne(d => d.cdCustomerAlertColor)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerAlertColorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCRMGroup_cdCustomerAlertColor_FK");

                entity.HasOne(d => d.cdPresentCardType)
                    .WithMany()
                    .HasForeignKey(d => d.PresentCardTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCRMGroup_cdPresentCardType_FK");

                entity.HasOne(d => d.cdCustomerShoppingHabit)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerShoppingHabitCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCRMGroup_cdCustomerShoppingHabit_FK");

                entity.HasOne(d => d.cdCustomerShoppingLevel)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerShoppingLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCRMGroup_cdCustomerShoppingLevel_FK");

            });

            // Configure relationships for cdCustomerCRMGroupDesc
            modelBuilder.Entity<cdCustomerCRMGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdCustomerCRMGroup)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerCRMGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCRMGroupDesc_cdCustomerCRMGroup_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerCRMGroupDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCustomerDiscountGrDesc
            modelBuilder.Entity<cdCustomerDiscountGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerDiscountGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerDiscountGr)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerDiscountGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerDiscountGrDesc_cdCustomerDiscountGr_FK");

            });

            // Configure relationships for cdCustomerMarkupGrDesc
            modelBuilder.Entity<cdCustomerMarkupGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerMarkupGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerMarkupGr)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerMarkupGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerMarkupGrDesc_cdCustomerMarkupGr_FK");

            });

            // Configure relationships for cdCustomerPaymentPlanGrDesc
            modelBuilder.Entity<cdCustomerPaymentPlanGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerPaymentPlanGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerPaymentPlanGr)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerPaymentPlanGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerPaymentPlanGrDesc_cdCustomerPaymentPlanGr_FK");

            });

            // Configure relationships for cdCustomerShoppingHabitDesc
            modelBuilder.Entity<cdCustomerShoppingHabitDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerShoppingHabitDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerShoppingHabit)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerShoppingHabitCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerShoppingHabitDesc_cdCustomerShoppingHabit_FK");

            });

            // Configure relationships for cdCustomerShoppingLevelDesc
            modelBuilder.Entity<cdCustomerShoppingLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerShoppingLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomerShoppingLevel)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerShoppingLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomerShoppingLevelDesc_cdCustomerShoppingLevel_FK");

            });

            // Configure relationships for cdCustomProcessGroupDesc
            modelBuilder.Entity<cdCustomProcessGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomProcessGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomProcessGroup)
                    .WithMany()
                    .HasForeignKey(d => d.CustomProcessGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomProcessGroupDesc_cdCustomProcessGroup_FK");

            });

            // Configure relationships for cdCustomsCompany
            modelBuilder.Entity<cdCustomsCompany>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsCompany_cdCompany_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsCompany_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsCompany_cdCurrAcc_FK");

            });

            // Configure relationships for cdCustomsCompanyDesc
            modelBuilder.Entity<cdCustomsCompanyDesc>(entity =>
            {
                entity.HasOne(d => d.cdCustomsCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsCompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsCompanyDesc_cdCustomsCompany_FK");

                entity.HasOne(d => d.cdCustomsCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsCompanyDesc_cdCustomsCompany_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsCompanyDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCustomsOffices
            modelBuilder.Entity<cdCustomsOffices>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsOffices_cdCompany_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsOffices_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsOffices_cdCurrAcc_FK");

            });

            // Configure relationships for cdCustomsOfficesDesc
            modelBuilder.Entity<cdCustomsOfficesDesc>(entity =>
            {
                entity.HasOne(d => d.cdCustomsOffices)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsOfficesDesc_cdCustomsOffices_FK");

                entity.HasOne(d => d.cdCustomsOffices)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsOfficesCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsOfficesDesc_cdCustomsOffices_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsOfficesDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdCustomsTariffNumberDesc
            modelBuilder.Entity<cdCustomsTariffNumberDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsTariffNumberDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdCustomsTariffNumber)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsTariffNumberCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdCustomsTariffNumberDesc_cdCustomsTariffNumber_FK");

            });

            // Configure relationships for cdDataLanguageDesc
            modelBuilder.Entity<cdDataLanguageDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.DataLanguageCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataLanguageDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataLanguageDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdDataTransferCompany
            modelBuilder.Entity<cdDataTransferCompany>(entity =>
            {
                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataTransferCompany_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataTransferCompany_cdCurrAcc_FK");

            });

            // Configure relationships for cdDataTransferCompanyDesc
            modelBuilder.Entity<cdDataTransferCompanyDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataTransferCompanyDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDataTransferCompany)
                    .WithMany()
                    .HasForeignKey(d => d.DataTransferCompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataTransferCompanyDesc_cdDataTransferCompany_FK");

            });

            // Configure relationships for cdDataTransferConvert
            modelBuilder.Entity<cdDataTransferConvert>(entity =>
            {
                entity.HasOne(d => d.bsDataTransferConvertType)
                    .WithMany()
                    .HasForeignKey(d => d.ConvertTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataTransferConvert_bsDataTransferConvertType_FK");

            });

            // Configure relationships for cdDataTransferConvertForAttribute
            modelBuilder.Entity<cdDataTransferConvertForAttribute>(entity =>
            {
                entity.HasOne(d => d.bsDataTransferConvertType)
                    .WithMany()
                    .HasForeignKey(d => d.ConvertTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataTransferConvertForAttribute_bsDataTransferConvertType_FK");

            });

            // Configure relationships for cdDataTransferJob
            modelBuilder.Entity<cdDataTransferJob>(entity =>
            {
                entity.HasOne(d => d.cdDataTransferTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.DataTransferTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDataTransferJob_cdDataTransferTemplate_FK");

            });

            // Configure relationships for cdDebitReasonDesc
            modelBuilder.Entity<cdDebitReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDebitReason)
                    .WithMany()
                    .HasForeignKey(d => d.DebitReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDebitReasonDesc_cdDebitReason_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDebitReasonDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdDeclaration
            modelBuilder.Entity<cdDeclaration>(entity =>
            {
                entity.HasOne(d => d.bsDeclarationCapacity)
                    .WithMany()
                    .HasForeignKey(d => d.DeclarationCapacityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_bsDeclarationCapacity_FK");

                entity.HasOne(d => d.bsDeclarationType)
                    .WithMany()
                    .HasForeignKey(d => d.DeclarationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_bsDeclarationType_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.AccountantCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_cdAccountant_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.AccountantCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_cdAccountant_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.AccountantCode3)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_cdAccountant_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.AccountantCode4)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_cdAccountant_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_cdAccountant_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_cdAccountant_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_cdAccountant_FK");

                entity.HasOne(d => d.cdAccountant)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclaration_cdAccountant_FK");

            });

            // Configure relationships for cdDeclarationDesc
            modelBuilder.Entity<cdDeclarationDesc>(entity =>
            {
                entity.HasOne(d => d.cdDeclaration)
                    .WithMany()
                    .HasForeignKey(d => d.DeclarationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclarationDesc_cdDeclaration_FK");

                entity.HasOne(d => d.cdDeclaration)
                    .WithMany()
                    .HasForeignKey(d => d.DeclarationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclarationDesc_cdDeclaration_FK");

                entity.HasOne(d => d.cdDeclaration)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclarationDesc_cdDeclaration_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeclarationDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdDeductionDesc
            modelBuilder.Entity<cdDeductionDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeductionDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDeduction)
                    .WithMany()
                    .HasForeignKey(d => d.DeductionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeductionDesc_cdDeduction_FK");

            });

            // Configure relationships for cdDeliveryCompany
            modelBuilder.Entity<cdDeliveryCompany>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeliveryCompany_cdCompany_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeliveryCompany_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeliveryCompany_cdCurrAcc_FK");

            });

            // Configure relationships for cdDeliveryCompanyDesc
            modelBuilder.Entity<cdDeliveryCompanyDesc>(entity =>
            {
                entity.HasOne(d => d.cdDeliveryCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeliveryCompanyDesc_cdDeliveryCompany_FK");

                entity.HasOne(d => d.cdDeliveryCompany)
                    .WithMany()
                    .HasForeignKey(d => d.DeliveryCompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeliveryCompanyDesc_cdDeliveryCompany_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDeliveryCompanyDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdDiagnosticDesc
            modelBuilder.Entity<cdDiagnosticDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiagnosticDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDiagnostic)
                    .WithMany()
                    .HasForeignKey(d => d.DiagnosticCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiagnosticDesc_cdDiagnostic_FK");

            });

            // Configure relationships for cdDigitalMarketingServiceDesc
            modelBuilder.Entity<cdDigitalMarketingServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDigitalMarketingServiceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDigitalMarketingService)
                    .WithMany()
                    .HasForeignKey(d => d.DigitalMarketingServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDigitalMarketingServiceDesc_cdDigitalMarketingService_FK");

            });

            // Configure relationships for cdDiscountOffer
            modelBuilder.Entity<cdDiscountOffer>(entity =>
            {
                entity.HasOne(d => d.bsDiscountOfferApply)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferApplyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOffer_bsDiscountOfferApply_FK");

                entity.HasOne(d => d.cdDiscountPointType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountPointTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOffer_cdDiscountPointType_FK");

                entity.HasOne(d => d.bsDiscountOfferType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOffer_bsDiscountOfferType_FK");

                entity.HasOne(d => d.bsProcess)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOffer_bsProcess_FK");

                entity.HasOne(d => d.bsDiscountOfferMethod)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOffer_bsDiscountOfferMethod_FK");

                entity.HasOne(d => d.bsCurrAccType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOffer_bsCurrAccType_FK");

                entity.HasOne(d => d.cdDiscountVoucherType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountVoucherTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOffer_cdDiscountVoucherType_FK");

            });

            // Configure relationships for cdDiscountOfferAttribute
            modelBuilder.Entity<cdDiscountOfferAttribute>(entity =>
            {
                entity.HasOne(d => d.cdDiscountOfferAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOfferAttribute_cdDiscountOfferAttributeType_FK");

            });

            // Configure relationships for cdDiscountOfferAttributeDesc
            modelBuilder.Entity<cdDiscountOfferAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOfferAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDiscountOfferAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOfferAttributeDesc_cdDiscountOfferAttribute_FK");

                entity.HasOne(d => d.cdDiscountOfferAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOfferAttributeDesc_cdDiscountOfferAttribute_FK");

            });

            // Configure relationships for cdDiscountOfferAttributeTypeDesc
            modelBuilder.Entity<cdDiscountOfferAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOfferAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDiscountOfferAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOfferAttributeTypeDesc_cdDiscountOfferAttributeType_FK");

            });

            // Configure relationships for cdDiscountOfferDesc
            modelBuilder.Entity<cdDiscountOfferDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOfferDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDiscountOffer)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountOfferCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountOfferDesc_cdDiscountOffer_FK");

            });

            // Configure relationships for cdDiscountPointType
            modelBuilder.Entity<cdDiscountPointType>(entity =>
            {
                entity.HasOne(d => d.bsDiscountLevelOfUse)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountLevelOfUseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountPointType_bsDiscountLevelOfUse_FK");

                entity.HasOne(d => d.cdProductPointType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductPointTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountPointType_cdProductPointType_FK");

                entity.HasOne(d => d.bsPointBase)
                    .WithMany()
                    .HasForeignKey(d => d.PointBaseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountPointType_bsPointBase_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountPointType_cdCurrency_FK");

            });

            // Configure relationships for cdDiscountPointTypeDesc
            modelBuilder.Entity<cdDiscountPointTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountPointTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDiscountPointType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountPointTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountPointTypeDesc_cdDiscountPointType_FK");

            });

            // Configure relationships for cdDiscountReasonDesc
            modelBuilder.Entity<cdDiscountReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDiscountReason)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountReasonDesc_cdDiscountReason_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountReasonDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdDiscountSubReasonDesc
            modelBuilder.Entity<cdDiscountSubReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDiscountSubReason)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountSubReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountSubReasonDesc_cdDiscountSubReason_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountSubReasonDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdDiscountTypeDesc
            modelBuilder.Entity<cdDiscountTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDiscountType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountTypeDesc_cdDiscountType_FK");

            });

            // Configure relationships for cdDiscountVoucher
            modelBuilder.Entity<cdDiscountVoucher>(entity =>
            {
                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucher_cdCurrency_FK");

                entity.HasOne(d => d.cdDiscountVoucherType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountVoucherTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucher_cdDiscountVoucherType_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucher_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucher_cdCurrAcc_FK");

            });

            // Configure relationships for cdDiscountVoucherType
            modelBuilder.Entity<cdDiscountVoucherType>(entity =>
            {
                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucherType_cdCurrency_FK");

                entity.HasOne(d => d.bsDiscountLevelOfUse)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountLevelOfUseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucherType_bsDiscountLevelOfUse_FK");

                entity.HasOne(d => d.cdBarcodeType)
                    .WithMany()
                    .HasForeignKey(d => d.BarcodeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucherType_cdBarcodeType_FK");

                entity.HasOne(d => d.bsDiscountVoucherBase)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountVoucherBaseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucherType_bsDiscountVoucherBase_FK");

            });

            // Configure relationships for cdDiscountVoucherTypeDesc
            modelBuilder.Entity<cdDiscountVoucherTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucherTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDiscountVoucherType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountVoucherTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDiscountVoucherTypeDesc_cdDiscountVoucherType_FK");

            });

            // Configure relationships for cdDistanceMatrixProvider
            modelBuilder.Entity<cdDistanceMatrixProvider>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDistanceMatrixProvider_cdCompany_FK");

            });

            // Configure relationships for cdDistrict
            modelBuilder.Entity<cdDistrict>(entity =>
            {
                entity.HasOne(d => d.cdCity)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDistrict_cdCity_FK");

            });

            // Configure relationships for cdDistrictDesc
            modelBuilder.Entity<cdDistrictDesc>(entity =>
            {
                entity.HasOne(d => d.cdDistrict)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDistrictDesc_cdDistrict_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDistrictDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdDOVDesc
            modelBuilder.Entity<cdDOVDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDOVDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDOV)
                    .WithMany()
                    .HasForeignKey(d => d.DOVCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDOVDesc_cdDOV_FK");

            });

            // Configure relationships for cdDriver
            modelBuilder.Entity<cdDriver>(entity =>
            {
                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDriver_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDriver_cdCurrAcc_FK");

            });

            // Configure relationships for cdDueDateFormulaDesc
            modelBuilder.Entity<cdDueDateFormulaDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDueDateFormulaDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdDueDateFormula)
                    .WithMany()
                    .HasForeignKey(d => d.DueDateFormulaCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdDueDateFormulaDesc_cdDueDateFormula_FK");

            });

            // Configure relationships for cdEArchiveWebService
            modelBuilder.Entity<cdEArchiveWebService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEArchiveWebService_cdCompany_FK");

            });

            // Configure relationships for cdEArchiveWebServiceDesc
            modelBuilder.Entity<cdEArchiveWebServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEArchiveWebServiceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdEArchiveWebService)
                    .WithMany()
                    .HasForeignKey(d => d.EArchiveWebServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEArchiveWebServiceDesc_cdEArchiveWebService_FK");

                entity.HasOne(d => d.cdEArchiveWebService)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEArchiveWebServiceDesc_cdEArchiveWebService_FK");

            });

            // Configure relationships for cdEarningsDesc
            modelBuilder.Entity<cdEarningsDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEarningsDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdEarnings)
                    .WithMany()
                    .HasForeignKey(d => d.EarningsCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEarningsDesc_cdEarnings_FK");

            });

            // Configure relationships for cdEducationStatusDesc
            modelBuilder.Entity<cdEducationStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdEducationStatus)
                    .WithMany()
                    .HasForeignKey(d => d.EducationStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEducationStatusDesc_cdEducationStatus_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEducationStatusDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdEInvoiceWebService
            modelBuilder.Entity<cdEInvoiceWebService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEInvoiceWebService_cdCompany_FK");

            });

            // Configure relationships for cdEInvoiceWebServiceDesc
            modelBuilder.Entity<cdEInvoiceWebServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdEInvoiceWebService)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEInvoiceWebServiceDesc_cdEInvoiceWebService_FK");

                entity.HasOne(d => d.cdEInvoiceWebService)
                    .WithMany()
                    .HasForeignKey(d => d.EInvoiceWebServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEInvoiceWebServiceDesc_cdEInvoiceWebService_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEInvoiceWebServiceDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdEMailService
            modelBuilder.Entity<cdEMailService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEMailService_cdCompany_FK");

            });

            // Configure relationships for cdEMailServiceDesc
            modelBuilder.Entity<cdEMailServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEMailServiceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdEMailService)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEMailServiceDesc_cdEMailService_FK");

                entity.HasOne(d => d.cdEMailService)
                    .WithMany()
                    .HasForeignKey(d => d.EMailServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEMailServiceDesc_cdEMailService_FK");

            });

            // Configure relationships for cdEmployeeDocumentTypeDesc
            modelBuilder.Entity<cdEmployeeDocumentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmployeeDocumentTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdEmployeeDocumentType)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeDocumentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmployeeDocumentTypeDesc_cdEmployeeDocumentType_FK");

            });

            // Configure relationships for cdEmployeeRecordTypeDesc
            modelBuilder.Entity<cdEmployeeRecordTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdEmployeeRecordType)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeRecordTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmployeeRecordTypeDesc_cdEmployeeRecordType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmployeeRecordTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdEmployeeSocialInsuranceStatusDesc
            modelBuilder.Entity<cdEmployeeSocialInsuranceStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmployeeSocialInsuranceStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdEmployeeSocialInsuranceStatus)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeSocialInsuranceStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmployeeSocialInsuranceStatusDesc_cdEmployeeSocialInsuranceStatus_FK");

            });

            // Configure relationships for cdEmployeeTaxStatusDesc
            modelBuilder.Entity<cdEmployeeTaxStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdEmployeeTaxStatus)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTaxStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmployeeTaxStatusDesc_cdEmployeeTaxStatus_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmployeeTaxStatusDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdEmploymentLawDesc
            modelBuilder.Entity<cdEmploymentLawDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmploymentLawDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdEmploymentLaw)
                    .WithMany()
                    .HasForeignKey(d => d.EmploymentLawCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEmploymentLawDesc_cdEmploymentLaw_FK");

            });

            // Configure relationships for cdEShipmentWebService
            modelBuilder.Entity<cdEShipmentWebService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEShipmentWebService_cdCompany_FK");

            });

            // Configure relationships for cdEShipmentWebServiceDesc
            modelBuilder.Entity<cdEShipmentWebServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEShipmentWebServiceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdEShipmentWebService)
                    .WithMany()
                    .HasForeignKey(d => d.EShipmentWebServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEShipmentWebServiceDesc_cdEShipmentWebService_FK");

                entity.HasOne(d => d.cdEShipmentWebService)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdEShipmentWebServiceDesc_cdEShipmentWebService_FK");

            });

            // Configure relationships for cdExchangeTypeDesc
            modelBuilder.Entity<cdExchangeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExchangeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdExchangeType)
                    .WithMany()
                    .HasForeignKey(d => d.ExchangeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExchangeTypeDesc_cdExchangeType_FK");

            });

            // Configure relationships for cdExecutionOffice
            modelBuilder.Entity<cdExecutionOffice>(entity =>
            {
                entity.HasOne(d => d.cdCity)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExecutionOffice_cdCity_FK");

            });

            // Configure relationships for cdExecutionOfficeDesc
            modelBuilder.Entity<cdExecutionOfficeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExecutionOfficeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdExecutionOffice)
                    .WithMany()
                    .HasForeignKey(d => d.ExecutionOfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExecutionOfficeDesc_cdExecutionOffice_FK");

            });

            // Configure relationships for cdExpensePeriodDesc
            modelBuilder.Entity<cdExpensePeriodDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExpensePeriodDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdExpensePeriod)
                    .WithMany()
                    .HasForeignKey(d => d.ExpensePeriodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExpensePeriodDesc_cdExpensePeriod_FK");

            });

            // Configure relationships for cdExpenseType
            modelBuilder.Entity<cdExpenseType>(entity =>
            {
                entity.HasOne(d => d.bsDocumentType)
                    .WithMany()
                    .HasForeignKey(d => d.DocumentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExpenseType_bsDocumentType_FK");

            });

            // Configure relationships for cdExpenseTypeDesc
            modelBuilder.Entity<cdExpenseTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExpenseTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdExpenseType)
                    .WithMany()
                    .HasForeignKey(d => d.ExpenseTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExpenseTypeDesc_cdExpenseType_FK");

            });

            // Configure relationships for cdExportFile
            modelBuilder.Entity<cdExportFile>(entity =>
            {
                entity.HasOne(d => d.cdCustomsCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsCompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdCustomsCompany_FK");

                entity.HasOne(d => d.cdCustomsCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdCustomsCompany_FK");

                entity.HasOne(d => d.cdCustomsOffices)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdCustomsOffices_FK");

                entity.HasOne(d => d.cdCustomsOffices)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsOfficesCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdCustomsOffices_FK");

                entity.HasOne(d => d.bsIncoterm)
                    .WithMany()
                    .HasForeignKey(d => d.IncotermCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_bsIncoterm_FK");

                entity.HasOne(d => d.bsIncoterm)
                    .WithMany()
                    .HasForeignKey(d => d.IncotermCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_bsIncoterm_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdCompany_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdOffice_FK");

                entity.HasOne(d => d.bsPaymentMeans)
                    .WithMany()
                    .HasForeignKey(d => d.PaymentMeansCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_bsPaymentMeans_FK");

                entity.HasOne(d => d.cdPaymentMethod)
                    .WithMany()
                    .HasForeignKey(d => d.PaymentMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdPaymentMethod_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFile_cdCurrAcc_FK");

            });

            // Configure relationships for cdExportFileAttribute
            modelBuilder.Entity<cdExportFileAttribute>(entity =>
            {
                entity.HasOne(d => d.cdExportFileAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFileAttribute_cdExportFileAttributeType_FK");

            });

            // Configure relationships for cdExportFileAttributeDesc
            modelBuilder.Entity<cdExportFileAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdExportFileAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFileAttributeDesc_cdExportFileAttribute_FK");

                entity.HasOne(d => d.cdExportFileAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFileAttributeDesc_cdExportFileAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFileAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdExportFileAttributeTypeDesc
            modelBuilder.Entity<cdExportFileAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFileAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdExportFileAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFileAttributeTypeDesc_cdExportFileAttributeType_FK");

            });

            // Configure relationships for cdExportFileDesc
            modelBuilder.Entity<cdExportFileDesc>(entity =>
            {
                entity.HasOne(d => d.cdExportFile)
                    .WithMany()
                    .HasForeignKey(d => d.ExportFileNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFileDesc_cdExportFile_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportFileDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdExportTypeDesc
            modelBuilder.Entity<cdExportTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdExportType)
                    .WithMany()
                    .HasForeignKey(d => d.ExportTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdExportTypeDesc_cdExportType_FK");

            });

            // Configure relationships for cdFabricDesc
            modelBuilder.Entity<cdFabricDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFabricDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdFabric)
                    .WithMany()
                    .HasForeignKey(d => d.FabricCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFabricDesc_cdFabric_FK");

            });

            // Configure relationships for cdFinanceCompanyWebService
            modelBuilder.Entity<cdFinanceCompanyWebService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFinanceCompanyWebService_cdCompany_FK");

            });

            // Configure relationships for cdFinanceCompanyWebServiceDesc
            modelBuilder.Entity<cdFinanceCompanyWebServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdFinanceCompanyWebService)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFinanceCompanyWebServiceDesc_cdFinanceCompanyWebService_FK");

                entity.HasOne(d => d.cdFinanceCompanyWebService)
                    .WithMany()
                    .HasForeignKey(d => d.FinanceCompanyWebServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFinanceCompanyWebServiceDesc_cdFinanceCompanyWebService_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFinanceCompanyWebServiceDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdFiscalPeriodDesc
            modelBuilder.Entity<cdFiscalPeriodDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFiscalPeriodDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdFiscalPeriod)
                    .WithMany()
                    .HasForeignKey(d => d.FiscalPeriodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFiscalPeriodDesc_cdFiscalPeriod_FK");

            });

            // Configure relationships for cdFixedAssetStatusDesc
            modelBuilder.Entity<cdFixedAssetStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdFixedAssetStatus)
                    .WithMany()
                    .HasForeignKey(d => d.FixedAssetStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFixedAssetStatusDesc_cdFixedAssetStatus_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFixedAssetStatusDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdFixedAssetTypeDesc
            modelBuilder.Entity<cdFixedAssetTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFixedAssetTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdFixedAssetType)
                    .WithMany()
                    .HasForeignKey(d => d.FixedAssetTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFixedAssetTypeDesc_cdFixedAssetType_FK");

            });

            // Configure relationships for cdFocalTypeDesc
            modelBuilder.Entity<cdFocalTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdFocalType)
                    .WithMany()
                    .HasForeignKey(d => d.FocalTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFocalTypeDesc_cdFocalType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFocalTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdForeignLanguageDesc
            modelBuilder.Entity<cdForeignLanguageDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdForeignLanguageDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdForeignLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.ForeignLanguageCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdForeignLanguageDesc_cdForeignLanguage_FK");

            });

            // Configure relationships for cdForeignTradeStatusDesc
            modelBuilder.Entity<cdForeignTradeStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdForeignTradeStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdForeignTradeStatus)
                    .WithMany()
                    .HasForeignKey(d => d.ForeignTradeStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdForeignTradeStatusDesc_cdForeignTradeStatus_FK");

            });

            // Configure relationships for cdFrameShapeTypeDesc
            modelBuilder.Entity<cdFrameShapeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdFrameShapeType)
                    .WithMany()
                    .HasForeignKey(d => d.FrameShapeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFrameShapeTypeDesc_cdFrameShapeType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFrameShapeTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdFrameTypeDesc
            modelBuilder.Entity<cdFrameTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFrameTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdFrameType)
                    .WithMany()
                    .HasForeignKey(d => d.FrameTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFrameTypeDesc_cdFrameType_FK");

            });

            // Configure relationships for cdFTAttribute
            modelBuilder.Entity<cdFTAttribute>(entity =>
            {
                entity.HasOne(d => d.cdFTAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFTAttribute_cdFTAttributeType_FK");

            });

            // Configure relationships for cdFTAttributeDesc
            modelBuilder.Entity<cdFTAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFTAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdFTAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFTAttributeDesc_cdFTAttribute_FK");

                entity.HasOne(d => d.cdFTAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFTAttributeDesc_cdFTAttribute_FK");

            });

            // Configure relationships for cdFTAttributeTypeDesc
            modelBuilder.Entity<cdFTAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFTAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdFTAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdFTAttributeTypeDesc_cdFTAttributeType_FK");

            });

            // Configure relationships for cdGiftCard
            modelBuilder.Entity<cdGiftCard>(entity =>
            {
                entity.HasOne(d => d.cdItem)
                    .WithMany()
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGiftCard_cdItem_FK");

                entity.HasOne(d => d.cdItem)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGiftCard_cdItem_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGiftCard_cdCurrency_FK");

            });

            // Configure relationships for cdGLAcc
            modelBuilder.Entity<cdGLAcc>(entity =>
            {
                entity.HasOne(d => d.cdTaxOffice)
                    .WithMany()
                    .HasForeignKey(d => d.TaxOfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdTaxOffice_FK");

                entity.HasOne(d => d.cdGLAccMain)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccMainCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdGLAccMain_FK");

                entity.HasOne(d => d.cdGLType)
                    .WithMany()
                    .HasForeignKey(d => d.GLTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdGLType_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdCompany_FK");

                entity.HasOne(d => d.bsTaxPaymentAccType)
                    .WithMany()
                    .HasForeignKey(d => d.TaxPaymentAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_bsTaxPaymentAccType_FK");

                entity.HasOne(d => d.bsTaxPaymentType)
                    .WithMany()
                    .HasForeignKey(d => d.TaxPaymentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_bsTaxPaymentType_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdGLAccSub_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccMainCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdGLAccSub_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccSubCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdGLAccSub_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccSubCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdGLAccSub_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccSubCode3)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAcc_cdGLAccSub_FK");

            });

            // Configure relationships for cdGLAccAttribute
            modelBuilder.Entity<cdGLAccAttribute>(entity =>
            {
                entity.HasOne(d => d.cdGLAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccAttribute_cdGLAccAttributeType_FK");

            });

            // Configure relationships for cdGLAccAttributeDesc
            modelBuilder.Entity<cdGLAccAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdGLAccAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccAttributeDesc_cdGLAccAttribute_FK");

                entity.HasOne(d => d.cdGLAccAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccAttributeDesc_cdGLAccAttribute_FK");

            });

            // Configure relationships for cdGLAccAttributeTypeDesc
            modelBuilder.Entity<cdGLAccAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdGLAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccAttributeTypeDesc_cdGLAccAttributeType_FK");

            });

            // Configure relationships for cdGLAccClassDesc
            modelBuilder.Entity<cdGLAccClassDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccClassDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdGLAccClass)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccClassCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccClassDesc_cdGLAccClass_FK");

            });

            // Configure relationships for cdGLAccDesc
            modelBuilder.Entity<cdGLAccDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdGLAcc)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccDesc_cdGLAcc_FK");

                entity.HasOne(d => d.cdGLAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccDesc_cdGLAcc_FK");

            });

            // Configure relationships for cdGLAccGroup
            modelBuilder.Entity<cdGLAccGroup>(entity =>
            {
                entity.HasOne(d => d.cdGLAccClass)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccClassCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccGroup_cdGLAccClass_FK");

            });

            // Configure relationships for cdGLAccGroupDesc
            modelBuilder.Entity<cdGLAccGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdGLAccGroup)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccGroupDesc_cdGLAccGroup_FK");

            });

            // Configure relationships for cdGLAccMain
            modelBuilder.Entity<cdGLAccMain>(entity =>
            {
                entity.HasOne(d => d.cdGLAccGroup)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccMain_cdGLAccGroup_FK");

            });

            // Configure relationships for cdGLAccMainDesc
            modelBuilder.Entity<cdGLAccMainDesc>(entity =>
            {
                entity.HasOne(d => d.cdGLAccMain)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccMainCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccMainDesc_cdGLAccMain_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccMainDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdGLAccSub
            modelBuilder.Entity<cdGLAccSub>(entity =>
            {
                entity.HasOne(d => d.cdGLAccMain)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccMainCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccSub_cdGLAccMain_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccSub_cdCompany_FK");

            });

            // Configure relationships for cdGLAccSubDesc
            modelBuilder.Entity<cdGLAccSubDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccSubDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccSubCode3)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccSubDesc_cdGLAccSub_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccSubCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccSubDesc_cdGLAccSub_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccSubCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccSubDesc_cdGLAccSub_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.GLAccMainCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccSubDesc_cdGLAccSub_FK");

                entity.HasOne(d => d.cdGLAccSub)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLAccSubDesc_cdGLAccSub_FK");

            });

            // Configure relationships for cdGLReflectionDesc
            modelBuilder.Entity<cdGLReflectionDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLReflectionDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdGLReflection)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLReflectionDesc_cdGLReflection_FK");

                entity.HasOne(d => d.cdGLReflection)
                    .WithMany()
                    .HasForeignKey(d => d.GLReflectionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLReflectionDesc_cdGLReflection_FK");

            });

            // Configure relationships for cdGLTypeDesc
            modelBuilder.Entity<cdGLTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdGLType)
                    .WithMany()
                    .HasForeignKey(d => d.GLTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdGLTypeDesc_cdGLType_FK");

            });

            // Configure relationships for cdHandicapTypeDesc
            modelBuilder.Entity<cdHandicapTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdHandicapType)
                    .WithMany()
                    .HasForeignKey(d => d.HandicapTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdHandicapTypeDesc_cdHandicapType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdHandicapTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdImportFile
            modelBuilder.Entity<cdImportFile>(entity =>
            {
                entity.HasOne(d => d.cdPaymentMethod)
                    .WithMany()
                    .HasForeignKey(d => d.PaymentMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdPaymentMethod_FK");

                entity.HasOne(d => d.bsIncoterm)
                    .WithMany()
                    .HasForeignKey(d => d.IncotermCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_bsIncoterm_FK");

                entity.HasOne(d => d.bsIncoterm)
                    .WithMany()
                    .HasForeignKey(d => d.IncotermCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_bsIncoterm_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdOffice_FK");

                entity.HasOne(d => d.cdCustomsCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsCompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdCustomsCompany_FK");

                entity.HasOne(d => d.cdCustomsCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdCustomsCompany_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdCompany_FK");

                entity.HasOne(d => d.cdCustomsOffices)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsOfficesCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdCustomsOffices_FK");

                entity.HasOne(d => d.cdCustomsOffices)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdCustomsOffices_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFile_cdCurrAcc_FK");

            });

            // Configure relationships for cdImportFileAttribute
            modelBuilder.Entity<cdImportFileAttribute>(entity =>
            {
                entity.HasOne(d => d.cdImportFileAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFileAttribute_cdImportFileAttributeType_FK");

            });

            // Configure relationships for cdImportFileAttributeDesc
            modelBuilder.Entity<cdImportFileAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdImportFileAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFileAttributeDesc_cdImportFileAttribute_FK");

                entity.HasOne(d => d.cdImportFileAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFileAttributeDesc_cdImportFileAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFileAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdImportFileAttributeTypeDesc
            modelBuilder.Entity<cdImportFileAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFileAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdImportFileAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFileAttributeTypeDesc_cdImportFileAttributeType_FK");

            });

            // Configure relationships for cdImportFileDesc
            modelBuilder.Entity<cdImportFileDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFileDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdImportFile)
                    .WithMany()
                    .HasForeignKey(d => d.ImportFileNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdImportFileDesc_cdImportFile_FK");

            });

            // Configure relationships for cdInactivationReasonDesc
            modelBuilder.Entity<cdInactivationReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdInactivationReason)
                    .WithMany()
                    .HasForeignKey(d => d.InactivationReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInactivationReasonDesc_cdInactivationReason_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInactivationReasonDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdIncentiveTypeDesc
            modelBuilder.Entity<cdIncentiveTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdIncentiveTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdIncentiveType)
                    .WithMany()
                    .HasForeignKey(d => d.IncentiveTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdIncentiveTypeDesc_cdIncentiveType_FK");

            });

            // Configure relationships for cdIndustryDesc
            modelBuilder.Entity<cdIndustryDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdIndustryDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdIndustry)
                    .WithMany()
                    .HasForeignKey(d => d.IndustryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdIndustryDesc_cdIndustry_FK");

            });

            // Configure relationships for cdInsuranceAgencyDesc
            modelBuilder.Entity<cdInsuranceAgencyDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInsuranceAgencyDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdInsuranceAgency)
                    .WithMany()
                    .HasForeignKey(d => d.InsuranceAgencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInsuranceAgencyDesc_cdInsuranceAgency_FK");

            });

            // Configure relationships for cdInsuranceTypeDesc
            modelBuilder.Entity<cdInsuranceTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInsuranceTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdInsuranceType)
                    .WithMany()
                    .HasForeignKey(d => d.InsuranceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInsuranceTypeDesc_cdInsuranceType_FK");

            });

            // Configure relationships for cdInteractiveSmsParameters
            modelBuilder.Entity<cdInteractiveSmsParameters>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInteractiveSmsParameters_cdCompany_FK");

            });

            // Configure relationships for cdInternationalUnitOfMeasureDesc
            modelBuilder.Entity<cdInternationalUnitOfMeasureDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInternationalUnitOfMeasureDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdInternationalUnitOfMeasure)
                    .WithMany()
                    .HasForeignKey(d => d.InternationalUnitOfMeasureCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdInternationalUnitOfMeasureDesc_cdInternationalUnitOfMeasure_FK");

            });

            // Configure relationships for cdITAttribute
            modelBuilder.Entity<cdITAttribute>(entity =>
            {
                entity.HasOne(d => d.cdITAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdITAttribute_cdITAttributeType_FK");

            });

            // Configure relationships for cdITAttributeDesc
            modelBuilder.Entity<cdITAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdITAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdITAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdITAttributeDesc_cdITAttribute_FK");

                entity.HasOne(d => d.cdITAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdITAttributeDesc_cdITAttribute_FK");

            });

            // Configure relationships for cdITAttributeTypeDesc
            modelBuilder.Entity<cdITAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdITAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdITAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdITAttributeTypeDesc_cdITAttributeType_FK");

            });

            // Configure relationships for cdItem
            modelBuilder.Entity<cdItem>(entity =>
            {
                entity.HasOne(d => d.cdBOMEntity)
                    .WithMany()
                    .HasForeignKey(d => d.BOMEntityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdBOMEntity_FK");

                entity.HasOne(d => d.cdCommercialRole)
                    .WithMany()
                    .HasForeignKey(d => d.CommercialRoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdCommercialRole_FK");

                entity.HasOne(d => d.cdItemDiscountGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDiscountGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdItemDiscountGr_FK");

                entity.HasOne(d => d.cdItemPaymentPlanGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemPaymentPlanGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdItemPaymentPlanGr_FK");

                entity.HasOne(d => d.bsCustomsProductGroup)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsProductGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_bsCustomsProductGroup_FK");

                entity.HasOne(d => d.cdCustomsTariffNumber)
                    .WithMany()
                    .HasForeignKey(d => d.CustomsTariffNumberCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdCustomsTariffNumber_FK");

                entity.HasOne(d => d.cdUnitOfMeasure)
                    .WithMany()
                    .HasForeignKey(d => d.UnitOfMeasureCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdUnitOfMeasure_FK");

                entity.HasOne(d => d.cdUnitOfMeasure)
                    .WithMany()
                    .HasForeignKey(d => d.UnitOfMeasureCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdUnitOfMeasure_FK");

                entity.HasOne(d => d.bsItemDimType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDimTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_bsItemDimType_FK");

                entity.HasOne(d => d.cdStorePriceLevel)
                    .WithMany()
                    .HasForeignKey(d => d.StorePriceLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdStorePriceLevel_FK");

                entity.HasOne(d => d.cdItemTaxGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTaxGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdItemTaxGr_FK");

                entity.HasOne(d => d.cdPerceptionOfFashion)
                    .WithMany()
                    .HasForeignKey(d => d.PerceptionOfFashionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdPerceptionOfFashion_FK");

                entity.HasOne(d => d.bsProductType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_bsProductType_FK");

                entity.HasOne(d => d.bsItemType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_bsItemType_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdCompany_FK");

                entity.HasOne(d => d.cdPromotionGroup)
                    .WithMany()
                    .HasForeignKey(d => d.PromotionGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdPromotionGroup_FK");

                entity.HasOne(d => d.cdPromotionGroup)
                    .WithMany()
                    .HasForeignKey(d => d.PromotionGroupCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdPromotionGroup_FK");

                entity.HasOne(d => d.cdItemVendorGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemVendorGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdItemVendorGr_FK");

                entity.HasOne(d => d.dfProductHierarchy)
                    .WithMany()
                    .HasForeignKey(d => d.ProductHierarchyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_dfProductHierarchy_FK");

                entity.HasOne(d => d.cdProductCollectionGr)
                    .WithMany()
                    .HasForeignKey(d => d.ProductCollectionGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdProductCollectionGr_FK");

                entity.HasOne(d => d.cdItemAccountGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemAccountGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdItemAccountGr_FK");

                entity.HasOne(d => d.cdStoreCapacityLevel)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCapacityLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItem_cdStoreCapacityLevel_FK");

            });

            // Configure relationships for cdItemAccountGr
            modelBuilder.Entity<cdItemAccountGr>(entity =>
            {
                entity.HasOne(d => d.bsItemType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAccountGr_bsItemType_FK");

            });

            // Configure relationships for cdItemAccountGrDesc
            modelBuilder.Entity<cdItemAccountGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdItemAccountGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemAccountGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAccountGrDesc_cdItemAccountGr_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAccountGrDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdItemAttribute
            modelBuilder.Entity<cdItemAttribute>(entity =>
            {
                entity.HasOne(d => d.cdItemAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttribute_cdItemAttributeType_FK");

                entity.HasOne(d => d.cdItemAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttribute_cdItemAttributeType_FK");

            });

            // Configure relationships for cdItemAttributeDesc
            modelBuilder.Entity<cdItemAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdItemAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttributeDesc_cdItemAttribute_FK");

                entity.HasOne(d => d.cdItemAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttributeDesc_cdItemAttribute_FK");

                entity.HasOne(d => d.cdItemAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttributeDesc_cdItemAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdItemAttributeType
            modelBuilder.Entity<cdItemAttributeType>(entity =>
            {
                entity.HasOne(d => d.bsItemType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttributeType_bsItemType_FK");

            });

            // Configure relationships for cdItemAttributeTypeDesc
            modelBuilder.Entity<cdItemAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdItemAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttributeTypeDesc_cdItemAttributeType_FK");

                entity.HasOne(d => d.cdItemAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttributeTypeDesc_cdItemAttributeType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemAttributeTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdItemDesc
            modelBuilder.Entity<cdItemDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItem)
                    .WithMany()
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemDesc_cdItem_FK");

                entity.HasOne(d => d.cdItem)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemDesc_cdItem_FK");

            });

            // Configure relationships for cdItemDim1Desc
            modelBuilder.Entity<cdItemDim1Desc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemDim1Desc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItemDim1)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDim1Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemDim1Desc_cdItemDim1_FK");

            });

            // Configure relationships for cdItemDiscountGrDesc
            modelBuilder.Entity<cdItemDiscountGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemDiscountGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItemDiscountGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDiscountGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemDiscountGrDesc_cdItemDiscountGr_FK");

            });

            // Configure relationships for cdItemLikeTypeDesc
            modelBuilder.Entity<cdItemLikeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemLikeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItemLikeType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemLikeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemLikeTypeDesc_cdItemLikeType_FK");

            });

            // Configure relationships for cdItemListDesc
            modelBuilder.Entity<cdItemListDesc>(entity =>
            {
                entity.HasOne(d => d.cdItemList)
                    .WithMany()
                    .HasForeignKey(d => d.ItemListCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemListDesc_cdItemList_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemListDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdItemOTAttribute
            modelBuilder.Entity<cdItemOTAttribute>(entity =>
            {
                entity.HasOne(d => d.cdItemOTAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttribute_cdItemOTAttributeType_FK");

                entity.HasOne(d => d.cdItemOTAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttribute_cdItemOTAttributeType_FK");

            });

            // Configure relationships for cdItemOTAttributeDesc
            modelBuilder.Entity<cdItemOTAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdItemOTAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttributeDesc_cdItemOTAttribute_FK");

                entity.HasOne(d => d.cdItemOTAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttributeDesc_cdItemOTAttribute_FK");

                entity.HasOne(d => d.cdItemOTAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttributeDesc_cdItemOTAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdItemOTAttributeType
            modelBuilder.Entity<cdItemOTAttributeType>(entity =>
            {
                entity.HasOne(d => d.bsItemType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttributeType_bsItemType_FK");

            });

            // Configure relationships for cdItemOTAttributeTypeDesc
            modelBuilder.Entity<cdItemOTAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItemOTAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttributeTypeDesc_cdItemOTAttributeType_FK");

                entity.HasOne(d => d.cdItemOTAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemOTAttributeTypeDesc_cdItemOTAttributeType_FK");

            });

            // Configure relationships for cdItemPaymentPlanGrDesc
            modelBuilder.Entity<cdItemPaymentPlanGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemPaymentPlanGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItemPaymentPlanGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemPaymentPlanGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemPaymentPlanGrDesc_cdItemPaymentPlanGr_FK");

            });

            // Configure relationships for cdItemTaxGrDesc
            modelBuilder.Entity<cdItemTaxGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemTaxGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItemTaxGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTaxGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemTaxGrDesc_cdItemTaxGr_FK");

            });

            // Configure relationships for cdItemTestTypeDesc
            modelBuilder.Entity<cdItemTestTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemTestTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItemTestType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTestTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemTestTypeDesc_cdItemTestType_FK");

            });

            // Configure relationships for cdItemTextileCareTemplateDesc
            modelBuilder.Entity<cdItemTextileCareTemplateDesc>(entity =>
            {
                entity.HasOne(d => d.cdItemTextileCareTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTextileCareTemplateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemTextileCareTemplateDesc_cdItemTextileCareTemplate_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemTextileCareTemplateDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdItemVendorGrDesc
            modelBuilder.Entity<cdItemVendorGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemVendorGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdItemVendorGr)
                    .WithMany()
                    .HasForeignKey(d => d.ItemVendorGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdItemVendorGrDesc_cdItemVendorGr_FK");

            });

            // Configure relationships for cdJobDepartmentDesc
            modelBuilder.Entity<cdJobDepartmentDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobDepartmentDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.JobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobDepartmentDesc_cdJobDepartment_FK");

            });

            // Configure relationships for cdJobInterviewResultDesc
            modelBuilder.Entity<cdJobInterviewResultDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobInterviewResultDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdJobInterviewResult)
                    .WithMany()
                    .HasForeignKey(d => d.JobInterviewResultCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobInterviewResultDesc_cdJobInterviewResult_FK");

            });

            // Configure relationships for cdJobPosition
            modelBuilder.Entity<cdJobPosition>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdCompany_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdOffice_FK");

                entity.HasOne(d => d.cdJobType)
                    .WithMany()
                    .HasForeignKey(d => d.JobTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdJobType_FK");

                entity.HasOne(d => d.cdJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.JobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdJobDepartment_FK");

                entity.HasOne(d => d.cdJobTitle)
                    .WithMany()
                    .HasForeignKey(d => d.JobTitleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdJobTitle_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.SampleJobCandidateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.SampleJobCandidateTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPosition_cdCurrAcc_FK");

            });

            // Configure relationships for cdJobPositionDesc
            modelBuilder.Entity<cdJobPositionDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPositionDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdJobPosition)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPositionDesc_cdJobPosition_FK");

                entity.HasOne(d => d.cdJobPosition)
                    .WithMany()
                    .HasForeignKey(d => d.JobPositionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobPositionDesc_cdJobPosition_FK");

            });

            // Configure relationships for cdJobTitle
            modelBuilder.Entity<cdJobTitle>(entity =>
            {
                entity.HasOne(d => d.cdJobTitleLevel)
                    .WithMany()
                    .HasForeignKey(d => d.JobTitleLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTitle_cdJobTitleLevel_FK");

            });

            // Configure relationships for cdJobTitleDesc
            modelBuilder.Entity<cdJobTitleDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTitleDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdJobTitle)
                    .WithMany()
                    .HasForeignKey(d => d.JobTitleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTitleDesc_cdJobTitle_FK");

            });

            // Configure relationships for cdJobTitleLevelDesc
            modelBuilder.Entity<cdJobTitleLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTitleLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdJobTitleLevel)
                    .WithMany()
                    .HasForeignKey(d => d.JobTitleLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTitleLevelDesc_cdJobTitleLevel_FK");

            });

            // Configure relationships for cdJobTraining
            modelBuilder.Entity<cdJobTraining>(entity =>
            {
                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTraining_cdCurrency_FK");

            });

            // Configure relationships for cdJobTrainingAttribute
            modelBuilder.Entity<cdJobTrainingAttribute>(entity =>
            {
                entity.HasOne(d => d.cdJobTrainingAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingAttribute_cdJobTrainingAttributeType_FK");

            });

            // Configure relationships for cdJobTrainingAttributeDesc
            modelBuilder.Entity<cdJobTrainingAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdJobTrainingAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingAttributeDesc_cdJobTrainingAttribute_FK");

                entity.HasOne(d => d.cdJobTrainingAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingAttributeDesc_cdJobTrainingAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdJobTrainingAttributeTypeDesc
            modelBuilder.Entity<cdJobTrainingAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdJobTrainingAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingAttributeTypeDesc_cdJobTrainingAttributeType_FK");

            });

            // Configure relationships for cdJobTrainingDesc
            modelBuilder.Entity<cdJobTrainingDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdJobTraining)
                    .WithMany()
                    .HasForeignKey(d => d.JobTrainingCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingDesc_cdJobTraining_FK");

                entity.HasOne(d => d.cdJobTraining)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTrainingDesc_cdJobTraining_FK");

            });

            // Configure relationships for cdJobTypeDesc
            modelBuilder.Entity<cdJobTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdJobType)
                    .WithMany()
                    .HasForeignKey(d => d.JobTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTypeDesc_cdJobType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJobTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdJournalTypeSub
            modelBuilder.Entity<cdJournalTypeSub>(entity =>
            {
                entity.HasOne(d => d.bsJournalType)
                    .WithMany()
                    .HasForeignKey(d => d.JournalTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJournalTypeSub_bsJournalType_FK");

            });

            // Configure relationships for cdJournalTypeSubDesc
            modelBuilder.Entity<cdJournalTypeSubDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJournalTypeSubDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdJournalTypeSub)
                    .WithMany()
                    .HasForeignKey(d => d.JournalTypeSubCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdJournalTypeSubDesc_cdJournalTypeSub_FK");

            });

            // Configure relationships for cdKnowLevelDesc
            modelBuilder.Entity<cdKnowLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdKnowLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdKnowLevel)
                    .WithMany()
                    .HasForeignKey(d => d.KnowLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdKnowLevelDesc_cdKnowLevel_FK");

            });

            // Configure relationships for cdLabelType
            modelBuilder.Entity<cdLabelType>(entity =>
            {
                entity.HasOne(d => d.cdBarcodeType)
                    .WithMany()
                    .HasForeignKey(d => d.BarcodeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLabelType_cdBarcodeType_FK");

                entity.HasOne(d => d.cdCountry)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLabelType_cdCountry_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LabelLangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLabelType_cdDataLanguage_FK");

                entity.HasOne(d => d.bsBasePrice)
                    .WithMany()
                    .HasForeignKey(d => d.FirstSalePriceBasePriceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLabelType_bsBasePrice_FK");

                entity.HasOne(d => d.cdPriceGroup)
                    .WithMany()
                    .HasForeignKey(d => d.FirstSalePriceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLabelType_cdPriceGroup_FK");

            });

            // Configure relationships for cdLabelTypeDesc
            modelBuilder.Entity<cdLabelTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLabelTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdLabelType)
                    .WithMany()
                    .HasForeignKey(d => d.LabelTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLabelTypeDesc_cdLabelType_FK");

            });

            // Configure relationships for cdLawyer
            modelBuilder.Entity<cdLawyer>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLawyer_cdCompany_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLawyer_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLawyer_cdCurrAcc_FK");

            });

            // Configure relationships for cdLeaveTypeDesc
            modelBuilder.Entity<cdLeaveTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdLeaveType)
                    .WithMany()
                    .HasForeignKey(d => d.LeaveTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLeaveTypeDesc_cdLeaveType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLeaveTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdLegalResignationDesc
            modelBuilder.Entity<cdLegalResignationDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLegalResignationDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdLegalResignation)
                    .WithMany()
                    .HasForeignKey(d => d.LegalResignationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLegalResignationDesc_cdLegalResignation_FK");

            });

            // Configure relationships for cdLegalResignationLocalDesc
            modelBuilder.Entity<cdLegalResignationLocalDesc>(entity =>
            {
                entity.HasOne(d => d.cdLegalResignationLocal)
                    .WithMany()
                    .HasForeignKey(d => d.LegalResignationLocalCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLegalResignationLocalDesc_cdLegalResignationLocal_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLegalResignationLocalDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdLetterOfGuarantee
            modelBuilder.Entity<cdLetterOfGuarantee>(entity =>
            {
                entity.HasOne(d => d.cdExportFile)
                    .WithMany()
                    .HasForeignKey(d => d.ExportFileNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdExportFile_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdCurrency_FK");

                entity.HasOne(d => d.cdGLAcc)
                    .WithMany()
                    .HasForeignKey(d => d.DebitGLAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdGLAcc_FK");

                entity.HasOne(d => d.cdGLAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CreditGLAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdGLAcc_FK");

                entity.HasOne(d => d.cdLetterType)
                    .WithMany()
                    .HasForeignKey(d => d.LetterTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdLetterType_FK");

                entity.HasOne(d => d.cdCostCenter)
                    .WithMany()
                    .HasForeignKey(d => d.CostCenterCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdCostCenter_FK");

                entity.HasOne(d => d.cdGLAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdGLAcc_FK");

                entity.HasOne(d => d.cdGLAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdGLAcc_FK");

                entity.HasOne(d => d.cdImportFile)
                    .WithMany()
                    .HasForeignKey(d => d.ImportFileNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdImportFile_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdOffice_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdCompany_FK");

                entity.HasOne(d => d.bsLetterOfGuaranteeType)
                    .WithMany()
                    .HasForeignKey(d => d.LetterOfGuaranteeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_bsLetterOfGuaranteeType_FK");

                entity.HasOne(d => d.prBankBranch)
                    .WithMany()
                    .HasForeignKey(d => d.BankBranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_prBankBranch_FK");

                entity.HasOne(d => d.prBankBranch)
                    .WithMany()
                    .HasForeignKey(d => d.BankCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_prBankBranch_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_cdCurrAcc_FK");

                entity.HasOne(d => d.prSubCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.SubCurrAccID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuarantee_prSubCurrAcc_FK");

            });

            // Configure relationships for cdLetterOfGuaranteeAttribute
            modelBuilder.Entity<cdLetterOfGuaranteeAttribute>(entity =>
            {
                entity.HasOne(d => d.cdLetterOfGuaranteeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.LetterOfGuaranteeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttribute_cdLetterOfGuaranteeAttributeType_FK");

                entity.HasOne(d => d.cdLetterOfGuaranteeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttribute_cdLetterOfGuaranteeAttributeType_FK");

            });

            // Configure relationships for cdLetterOfGuaranteeAttributeDesc
            modelBuilder.Entity<cdLetterOfGuaranteeAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdLetterOfGuaranteeAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.LetterOfGuaranteeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttributeDesc_cdLetterOfGuaranteeAttribute_FK");

                entity.HasOne(d => d.cdLetterOfGuaranteeAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttributeDesc_cdLetterOfGuaranteeAttribute_FK");

                entity.HasOne(d => d.cdLetterOfGuaranteeAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttributeDesc_cdLetterOfGuaranteeAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdLetterOfGuaranteeAttributeType
            modelBuilder.Entity<cdLetterOfGuaranteeAttributeType>(entity =>
            {
                entity.HasOne(d => d.bsLetterOfGuaranteeType)
                    .WithMany()
                    .HasForeignKey(d => d.LetterOfGuaranteeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttributeType_bsLetterOfGuaranteeType_FK");

            });

            // Configure relationships for cdLetterOfGuaranteeAttributeTypeDesc
            modelBuilder.Entity<cdLetterOfGuaranteeAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdLetterOfGuaranteeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttributeTypeDesc_cdLetterOfGuaranteeAttributeType_FK");

                entity.HasOne(d => d.cdLetterOfGuaranteeAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.LetterOfGuaranteeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttributeTypeDesc_cdLetterOfGuaranteeAttributeType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterOfGuaranteeAttributeTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdLetterTypeDesc
            modelBuilder.Entity<cdLetterTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdLetterType)
                    .WithMany()
                    .HasForeignKey(d => d.LetterTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLetterTypeDesc_cdLetterType_FK");

            });

            // Configure relationships for cdLot
            modelBuilder.Entity<cdLot>(entity =>
            {
                entity.HasOne(d => d.bsItemDimType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDimTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLot_bsItemDimType_FK");

            });

            // Configure relationships for cdLotDesc
            modelBuilder.Entity<cdLotDesc>(entity =>
            {
                entity.HasOne(d => d.cdLot)
                    .WithMany()
                    .HasForeignKey(d => d.LotCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLotDesc_cdLot_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLotDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdLoyaltyProgramDesc
            modelBuilder.Entity<cdLoyaltyProgramDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLoyaltyProgramDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdLoyaltyProgram)
                    .WithMany()
                    .HasForeignKey(d => d.LoyaltyProgramCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLoyaltyProgramDesc_cdLoyaltyProgram_FK");

            });

            // Configure relationships for cdLoyaltyProgramLevelDesc
            modelBuilder.Entity<cdLoyaltyProgramLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdLoyaltyProgramLevel)
                    .WithMany()
                    .HasForeignKey(d => d.LoyaltyProgramLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLoyaltyProgramLevelDesc_cdLoyaltyProgramLevel_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLoyaltyProgramLevelDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdLoyaltyProgramStatusDesc
            modelBuilder.Entity<cdLoyaltyProgramStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLoyaltyProgramStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdLoyaltyProgramStatus)
                    .WithMany()
                    .HasForeignKey(d => d.LoyaltyProgramStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLoyaltyProgramStatusDesc_cdLoyaltyProgramStatus_FK");

            });

            // Configure relationships for cdLoyaltyProgramStatusModifyReasonDesc
            modelBuilder.Entity<cdLoyaltyProgramStatusModifyReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdLoyaltyProgramStatusModifyReason)
                    .WithMany()
                    .HasForeignKey(d => d.LoyaltyProgramStatusModifyReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLoyaltyProgramStatusModifyReasonDesc_cdLoyaltyProgramStatusModifyReason_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdLoyaltyProgramStatusModifyReasonDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdMainJobTitleDesc
            modelBuilder.Entity<cdMainJobTitleDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMainJobTitleDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdMainJobTitle)
                    .WithMany()
                    .HasForeignKey(d => d.MainJobTitleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMainJobTitleDesc_cdMainJobTitle_FK");

            });

            // Configure relationships for cdMaladyTypeDesc
            modelBuilder.Entity<cdMaladyTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdMaladyType)
                    .WithMany()
                    .HasForeignKey(d => d.MaladyTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMaladyTypeDesc_cdMaladyType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMaladyTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdManufacturer
            modelBuilder.Entity<cdManufacturer>(entity =>
            {
                entity.HasOne(d => d.bsProductType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdManufacturer_bsProductType_FK");

            });

            // Configure relationships for cdManufacturerDesc
            modelBuilder.Entity<cdManufacturerDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdManufacturerDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdManufacturer)
                    .WithMany()
                    .HasForeignKey(d => d.ManufacturerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdManufacturerDesc_cdManufacturer_FK");

                entity.HasOne(d => d.cdManufacturer)
                    .WithMany()
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdManufacturerDesc_cdManufacturer_FK");

            });

            // Configure relationships for cdMessageReasonDesc
            modelBuilder.Entity<cdMessageReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMessageReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdMessageReason)
                    .WithMany()
                    .HasForeignKey(d => d.MessageReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMessageReasonDesc_cdMessageReason_FK");

            });

            // Configure relationships for cdMessageTypeDesc
            modelBuilder.Entity<cdMessageTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMessageTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdMessageType)
                    .WithMany()
                    .HasForeignKey(d => d.MessageTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMessageTypeDesc_cdMessageType_FK");

            });

            // Configure relationships for cdMilitaryServiceStatusDesc
            modelBuilder.Entity<cdMilitaryServiceStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMilitaryServiceStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdMilitaryServiceStatus)
                    .WithMany()
                    .HasForeignKey(d => d.MilitaryServiceStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMilitaryServiceStatusDesc_cdMilitaryServiceStatus_FK");

            });

            // Configure relationships for cdMissingWorkReasonDesc
            modelBuilder.Entity<cdMissingWorkReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdMissingWorkReason)
                    .WithMany()
                    .HasForeignKey(d => d.MissingWorkReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMissingWorkReasonDesc_cdMissingWorkReason_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMissingWorkReasonDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdMMSBusinessPartnerService
            modelBuilder.Entity<cdMMSBusinessPartnerService>(entity =>
            {
                entity.HasOne(d => d.bsMMSBusinessPartner)
                    .WithMany()
                    .HasForeignKey(d => d.MMSBusinessPartnerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMMSBusinessPartnerService_bsMMSBusinessPartner_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdMMSBusinessPartnerService_cdCompany_FK");

            });

            // Configure relationships for cdNationalityDesc
            modelBuilder.Entity<cdNationalityDesc>(entity =>
            {
                entity.HasOne(d => d.cdNationality)
                    .WithMany()
                    .HasForeignKey(d => d.NationalityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdNationalityDesc_cdNationality_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdNationalityDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdOffice
            modelBuilder.Entity<cdOffice>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOffice_cdCompany_FK");

            });

            // Configure relationships for cdOfficeCOGSGr
            modelBuilder.Entity<cdOfficeCOGSGr>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOfficeCOGSGr_cdCompany_FK");

            });

            // Configure relationships for cdOfficeCOGSGrDesc
            modelBuilder.Entity<cdOfficeCOGSGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOfficeCOGSGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdOfficeCOGSGr)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCOGSGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOfficeCOGSGrDesc_cdOfficeCOGSGr_FK");

            });

            // Configure relationships for cdOfficeDesc
            modelBuilder.Entity<cdOfficeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOfficeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOfficeDesc_cdOffice_FK");

            });

            // Configure relationships for cdOnlineBankWebService
            modelBuilder.Entity<cdOnlineBankWebService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOnlineBankWebService_cdCompany_FK");

            });

            // Configure relationships for cdOnlineBankWebServiceDesc
            modelBuilder.Entity<cdOnlineBankWebServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOnlineBankWebServiceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdOnlineBankWebService)
                    .WithMany()
                    .HasForeignKey(d => d.OnlineBankWebServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOnlineBankWebServiceDesc_cdOnlineBankWebService_FK");

                entity.HasOne(d => d.cdOnlineBankWebService)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOnlineBankWebServiceDesc_cdOnlineBankWebService_FK");

            });

            // Configure relationships for cdOnlineDBSWebService
            modelBuilder.Entity<cdOnlineDBSWebService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOnlineDBSWebService_cdCompany_FK");

            });

            // Configure relationships for cdOnlineDBSWebServiceDesc
            modelBuilder.Entity<cdOnlineDBSWebServiceDesc>(entity =>
            {
                entity.HasOne(d => d.cdOnlineDBSWebService)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOnlineDBSWebServiceDesc_cdOnlineDBSWebService_FK");

                entity.HasOne(d => d.cdOnlineDBSWebService)
                    .WithMany()
                    .HasForeignKey(d => d.OnlineDBSWebServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOnlineDBSWebServiceDesc_cdOnlineDBSWebService_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOnlineDBSWebServiceDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdOpticalGroupRangeDesc
            modelBuilder.Entity<cdOpticalGroupRangeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOpticalGroupRangeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdOpticalGroupRange)
                    .WithMany()
                    .HasForeignKey(d => d.OpticalGroupRangeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOpticalGroupRangeDesc_cdOpticalGroupRange_FK");

            });

            // Configure relationships for cdOpticalSut
            modelBuilder.Entity<cdOpticalSut>(entity =>
            {
                entity.HasOne(d => d.bsEyeGlassSutType)
                    .WithMany()
                    .HasForeignKey(d => d.EyeGlassSutTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOpticalSut_bsEyeGlassSutType_FK");

            });

            // Configure relationships for cdOpticalSutDesc
            modelBuilder.Entity<cdOpticalSutDesc>(entity =>
            {
                entity.HasOne(d => d.cdOpticalSut)
                    .WithMany()
                    .HasForeignKey(d => d.OpticalSutCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOpticalSutDesc_cdOpticalSut_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOpticalSutDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdOrderCancelReasonDesc
            modelBuilder.Entity<cdOrderCancelReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOrderCancelReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdOrderCancelReason)
                    .WithMany()
                    .HasForeignKey(d => d.OrderCancelReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOrderCancelReasonDesc_cdOrderCancelReason_FK");

            });

            // Configure relationships for cdOrderStatusDesc
            modelBuilder.Entity<cdOrderStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOrderStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdOrderStatus)
                    .WithMany()
                    .HasForeignKey(d => d.OrderStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOrderStatusDesc_cdOrderStatus_FK");

            });

            // Configure relationships for cdOtherDocumentTypeDesc
            modelBuilder.Entity<cdOtherDocumentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdOtherDocumentType)
                    .WithMany()
                    .HasForeignKey(d => d.OtherDocumentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOtherDocumentTypeDesc_cdOtherDocumentType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdOtherDocumentTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdPackageBrandDesc
            modelBuilder.Entity<cdPackageBrandDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPackageBrandDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPackageBrand)
                    .WithMany()
                    .HasForeignKey(d => d.PackageBrandCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPackageBrandDesc_cdPackageBrand_FK");

            });

            // Configure relationships for cdPackageVolume
            modelBuilder.Entity<cdPackageVolume>(entity =>
            {
                entity.HasOne(d => d.cdUnitOfMeasure)
                    .WithMany()
                    .HasForeignKey(d => d.SizeUnitOfMeasureCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPackageVolume_cdUnitOfMeasure_FK");

            });

            // Configure relationships for cdPackageVolumeDesc
            modelBuilder.Entity<cdPackageVolumeDesc>(entity =>
            {
                entity.HasOne(d => d.cdPackageVolume)
                    .WithMany()
                    .HasForeignKey(d => d.PackageVolumeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPackageVolumeDesc_cdPackageVolume_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPackageVolumeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdPantone
            modelBuilder.Entity<cdPantone>(entity =>
            {
                entity.HasOne(d => d.cdColor)
                    .WithMany()
                    .HasForeignKey(d => d.ColorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPantone_cdColor_FK");

            });

            // Configure relationships for cdPantoneDesc
            modelBuilder.Entity<cdPantoneDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPantoneDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPantone)
                    .WithMany()
                    .HasForeignKey(d => d.PantoneCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPantoneDesc_cdPantone_FK");

            });

            // Configure relationships for cdPaymentMethodDesc
            modelBuilder.Entity<cdPaymentMethodDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPaymentMethodDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPaymentMethod)
                    .WithMany()
                    .HasForeignKey(d => d.PaymentMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPaymentMethodDesc_cdPaymentMethod_FK");

            });

            // Configure relationships for cdPaymentPlan
            modelBuilder.Entity<cdPaymentPlan>(entity =>
            {
                entity.HasOne(d => d.cdDueDateFormula)
                    .WithMany()
                    .HasForeignKey(d => d.DueDateFormulaCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPaymentPlan_cdDueDateFormula_FK");

            });

            // Configure relationships for cdPaymentPlanDesc
            modelBuilder.Entity<cdPaymentPlanDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPaymentPlanDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPaymentPlan)
                    .WithMany()
                    .HasForeignKey(d => d.PaymentPlanCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPaymentPlanDesc_cdPaymentPlan_FK");

            });

            // Configure relationships for cdPaymentProvider
            modelBuilder.Entity<cdPaymentProvider>(entity =>
            {
                entity.HasOne(d => d.cdDiscountPointType)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountPointTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPaymentProvider_cdDiscountPointType_FK");

            });

            // Configure relationships for cdPaymentProviderDesc
            modelBuilder.Entity<cdPaymentProviderDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPaymentProviderDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPaymentProvider)
                    .WithMany()
                    .HasForeignKey(d => d.PaymentProviderCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPaymentProviderDesc_cdPaymentProvider_FK");

            });

            // Configure relationships for cdPayrollTypeDesc
            modelBuilder.Entity<cdPayrollTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPayrollTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPayrollType)
                    .WithMany()
                    .HasForeignKey(d => d.PayrollTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPayrollTypeDesc_cdPayrollType_FK");

            });

            // Configure relationships for cdPCT
            modelBuilder.Entity<cdPCT>(entity =>
            {
                entity.HasOne(d => d.bsOfficialTaxType)
                    .WithMany()
                    .HasForeignKey(d => d.OfficialTaxTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPCT_bsOfficialTaxType_FK");

                entity.HasOne(d => d.cdUnitOfMeasure)
                    .WithMany()
                    .HasForeignKey(d => d.UnitOfMeasureCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPCT_cdUnitOfMeasure_FK");

            });

            // Configure relationships for cdPCTDesc
            modelBuilder.Entity<cdPCTDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPCTDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPCT)
                    .WithMany()
                    .HasForeignKey(d => d.PCTCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPCTDesc_cdPCT_FK");

            });

            // Configure relationships for cdPerceptionOfFashionDesc
            modelBuilder.Entity<cdPerceptionOfFashionDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPerceptionOfFashionDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPerceptionOfFashion)
                    .WithMany()
                    .HasForeignKey(d => d.PerceptionOfFashionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPerceptionOfFashionDesc_cdPerceptionOfFashion_FK");

            });

            // Configure relationships for cdPermissionMarketingService
            modelBuilder.Entity<cdPermissionMarketingService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPermissionMarketingService_cdCompany_FK");

            });

            // Configure relationships for cdPlasticBagType
            modelBuilder.Entity<cdPlasticBagType>(entity =>
            {
                entity.HasOne(d => d.cdItem)
                    .WithMany()
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPlasticBagType_cdItem_FK");

                entity.HasOne(d => d.cdItem)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPlasticBagType_cdItem_FK");

            });

            // Configure relationships for cdPlasticBagTypeDesc
            modelBuilder.Entity<cdPlasticBagTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPlasticBagTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPlasticBagType)
                    .WithMany()
                    .HasForeignKey(d => d.PlasticBagTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPlasticBagTypeDesc_cdPlasticBagType_FK");

            });

            // Configure relationships for cdPointModifyReasonDesc
            modelBuilder.Entity<cdPointModifyReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPointModifyReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPointModifyReason)
                    .WithMany()
                    .HasForeignKey(d => d.PointModifyReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPointModifyReasonDesc_cdPointModifyReason_FK");

            });

            // Configure relationships for cdPort
            modelBuilder.Entity<cdPort>(entity =>
            {
                entity.HasOne(d => d.cdCity)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPort_cdCity_FK");

                entity.HasOne(d => d.cdState)
                    .WithMany()
                    .HasForeignKey(d => d.StateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPort_cdState_FK");

                entity.HasOne(d => d.cdCountry)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPort_cdCountry_FK");

                entity.HasOne(d => d.cdDistrict)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPort_cdDistrict_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.StreetCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPort_cdStreet_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPort_cdStreet_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.QuarterCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPort_cdStreet_FK");

            });

            // Configure relationships for cdPortDesc
            modelBuilder.Entity<cdPortDesc>(entity =>
            {
                entity.HasOne(d => d.cdPort)
                    .WithMany()
                    .HasForeignKey(d => d.PortCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPortDesc_cdPort_FK");

            });

            // Configure relationships for cdPOSTerminal
            modelBuilder.Entity<cdPOSTerminal>(entity =>
            {
                entity.HasOne(d => d.bsDocumentType)
                    .WithMany()
                    .HasForeignKey(d => d.DefaultDocumentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPOSTerminal_bsDocumentType_FK");

                entity.HasOne(d => d.cdWarehouse)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPOSTerminal_cdWarehouse_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPOSTerminal_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CashCurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPOSTerminal_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPOSTerminal_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CashCurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPOSTerminal_cdCurrAcc_FK");

            });

            // Configure relationships for cdPresentCardType
            modelBuilder.Entity<cdPresentCardType>(entity =>
            {
                entity.HasOne(d => d.cdCompanyBrand)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyBrandCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPresentCardType_cdCompanyBrand_FK");

                entity.HasOne(d => d.cdStoreCRMGroup)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCRMGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPresentCardType_cdStoreCRMGroup_FK");

            });

            // Configure relationships for cdPresentCardTypeDesc
            modelBuilder.Entity<cdPresentCardTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdPresentCardType)
                    .WithMany()
                    .HasForeignKey(d => d.PresentCardTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPresentCardTypeDesc_cdPresentCardType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPresentCardTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdPrevJobTypeDesc
            modelBuilder.Entity<cdPrevJobTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPrevJobTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPrevJobType)
                    .WithMany()
                    .HasForeignKey(d => d.PrevJobTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPrevJobTypeDesc_cdPrevJobType_FK");

            });

            // Configure relationships for cdPriceGroup
            modelBuilder.Entity<cdPriceGroup>(entity =>
            {
                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.DefaultCurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPriceGroup_cdCurrency_FK");

            });

            // Configure relationships for cdPriceGroupDesc
            modelBuilder.Entity<cdPriceGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPriceGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPriceGroup)
                    .WithMany()
                    .HasForeignKey(d => d.PriceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPriceGroupDesc_cdPriceGroup_FK");

            });

            // Configure relationships for cdPriceListTypeDesc
            modelBuilder.Entity<cdPriceListTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPriceListTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPriceListType)
                    .WithMany()
                    .HasForeignKey(d => d.PriceListTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPriceListTypeDesc_cdPriceListType_FK");

            });

            // Configure relationships for cdPriorityDesc
            modelBuilder.Entity<cdPriorityDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPriorityDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPriority)
                    .WithMany()
                    .HasForeignKey(d => d.PriorityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPriorityDesc_cdPriority_FK");

            });

            // Configure relationships for cdPrivateInsuranceDesc
            modelBuilder.Entity<cdPrivateInsuranceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPrivateInsuranceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPrivateInsurance)
                    .WithMany()
                    .HasForeignKey(d => d.PrivateInsuranceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPrivateInsuranceDesc_cdPrivateInsurance_FK");

            });

            // Configure relationships for cdProcessFlowDenyReasonDesc
            modelBuilder.Entity<cdProcessFlowDenyReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProcessFlowDenyReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdProcessFlowDenyReason)
                    .WithMany()
                    .HasForeignKey(d => d.ProcessFlowDenyReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProcessFlowDenyReasonDesc_cdProcessFlowDenyReason_FK");

            });

            // Configure relationships for cdProductCollectionGr
            modelBuilder.Entity<cdProductCollectionGr>(entity =>
            {
                entity.HasOne(d => d.cdSeason)
                    .WithMany()
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductCollectionGr_cdSeason_FK");

                entity.HasOne(d => d.cdStoryBoard)
                    .WithMany()
                    .HasForeignKey(d => d.StoryBoardCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductCollectionGr_cdStoryBoard_FK");

                entity.HasOne(d => d.cdCollection)
                    .WithMany()
                    .HasForeignKey(d => d.CollectionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductCollectionGr_cdCollection_FK");

            });

            // Configure relationships for cdProductColorAttribute
            modelBuilder.Entity<cdProductColorAttribute>(entity =>
            {
                entity.HasOne(d => d.cdProductColorAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductColorAttribute_cdProductColorAttributeType_FK");

            });

            // Configure relationships for cdProductColorAttributeDesc
            modelBuilder.Entity<cdProductColorAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdProductColorAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductColorAttributeDesc_cdProductColorAttribute_FK");

                entity.HasOne(d => d.cdProductColorAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductColorAttributeDesc_cdProductColorAttribute_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductColorAttributeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdProductColorAttributeTypeDesc
            modelBuilder.Entity<cdProductColorAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductColorAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdProductColorAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductColorAttributeTypeDesc_cdProductColorAttributeType_FK");

            });

            // Configure relationships for cdProductColorSetDesc
            modelBuilder.Entity<cdProductColorSetDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductColorSetDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdProductColorSet)
                    .WithMany()
                    .HasForeignKey(d => d.ProductColorSetCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductColorSetDesc_cdProductColorSet_FK");

            });

            // Configure relationships for cdProductDimSet
            modelBuilder.Entity<cdProductDimSet>(entity =>
            {
                entity.HasOne(d => d.bsItemDimType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDimTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductDimSet_bsItemDimType_FK");

            });

            // Configure relationships for cdProductDimSetDesc
            modelBuilder.Entity<cdProductDimSetDesc>(entity =>
            {
                entity.HasOne(d => d.cdProductDimSet)
                    .WithMany()
                    .HasForeignKey(d => d.ProductDimSetCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductDimSetDesc_cdProductDimSet_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductDimSetDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdProductHierarchyLevelDesc
            modelBuilder.Entity<cdProductHierarchyLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductHierarchyLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdProductHierarchyLevel)
                    .WithMany()
                    .HasForeignKey(d => d.ProductHierarchyLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductHierarchyLevelDesc_cdProductHierarchyLevel_FK");

            });

            // Configure relationships for cdProductPartDesc
            modelBuilder.Entity<cdProductPartDesc>(entity =>
            {
                entity.HasOne(d => d.cdProductPart)
                    .WithMany()
                    .HasForeignKey(d => d.ProductPartCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductPartDesc_cdProductPart_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductPartDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdProductPointTypeDesc
            modelBuilder.Entity<cdProductPointTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductPointTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdProductPointType)
                    .WithMany()
                    .HasForeignKey(d => d.ProductPointTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductPointTypeDesc_cdProductPointType_FK");

            });

            // Configure relationships for cdProductStatusDesc
            modelBuilder.Entity<cdProductStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdProductStatus)
                    .WithMany()
                    .HasForeignKey(d => d.ProductStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductStatusDesc_cdProductStatus_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProductStatusDesc_cdDataLanguage_FK");

            });

           // Configure relationships for cdPromotionGroupDesc
            modelBuilder.Entity<cdPromotionGroupDesc>(entity =>
            {
                entity.HasKey(e => new { e.PromotionGroupCode, e.LangCode });

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany(p => p.cdPromotionGroupDescs)
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPromotionGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdPromotionGroup)
                    .WithMany(p => p.cdPromotionGroupDescs)
                    .HasForeignKey(d => d.PromotionGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPromotionGroupDesc_cdPromotionGroup_FK");
            });

            // Configure relationships for cdProposalConfirmationLimit
            modelBuilder.Entity<cdProposalConfirmationLimit>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationLimit_cdCompany_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationLimit_cdCurrency_FK");

                entity.HasOne(d => d.cdRequisition)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationLimit_cdRequisition_FK");

                entity.HasOne(d => d.cdRequisitionType)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationLimit_cdRequisitionType_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationLimit_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationLimit_cdCurrAcc_FK");

                entity.HasOne(d => d.cdJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.JobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationLimit_cdJobDepartment_FK");

                entity.HasOne(d => d.cdJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationJobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationLimit_cdJobDepartment_FK");

            });

            // Configure relationships for cdProposalConfirmationRule
            modelBuilder.Entity<cdProposalConfirmationRule>(entity =>
            {
                entity.HasOne(d => d.cdRequisitionType)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationRule_cdRequisitionType_FK");

                entity.HasOne(d => d.cdRequisition)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationRule_cdRequisition_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationRule_cdCurrency_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationRule_cdCompany_FK");

                entity.HasOne(d => d.cdJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.JobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationRule_cdJobDepartment_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationRule_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdProposalConfirmationRule_cdCurrAcc_FK");

            });

            // Configure relationships for cdPurchasePlanDesc
            modelBuilder.Entity<cdPurchasePlanDesc>(entity =>
            {
                entity.HasOne(d => d.cdPurchasePlan)
                    .WithMany()
                    .HasForeignKey(d => d.PurchasePlanCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPurchasePlanDesc_cdPurchasePlan_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdPurchasePlanDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdQuarter
            modelBuilder.Entity<cdQuarter>(entity =>
            {
                entity.HasOne(d => d.cdDistrict)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdQuarter_cdDistrict_FK");

            });

            // Configure relationships for cdReasonForNotShoppingDesc
            modelBuilder.Entity<cdReasonForNotShoppingDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdReasonForNotShoppingDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdReasonForNotShopping)
                    .WithMany()
                    .HasForeignKey(d => d.ReasonForNotShoppingCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdReasonForNotShoppingDesc_cdReasonForNotShopping_FK");

            });

            // Configure relationships for cdRecidivistTypeDesc
            modelBuilder.Entity<cdRecidivistTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdRecidivistType)
                    .WithMany()
                    .HasForeignKey(d => d.RecidivistTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRecidivistTypeDesc_cdRecidivistType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRecidivistTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdReconciliation
            modelBuilder.Entity<cdReconciliation>(entity =>
            {
                entity.HasOne(d => d.bsReconciliationType)
                    .WithMany()
                    .HasForeignKey(d => d.ReconciliationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdReconciliation_bsReconciliationType_FK");

            });

            // Configure relationships for cdReconciliationDesc
            modelBuilder.Entity<cdReconciliationDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdReconciliationDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdReconciliation)
                    .WithMany()
                    .HasForeignKey(d => d.ReconciliationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdReconciliationDesc_cdReconciliation_FK");

            });

            // Configure relationships for cdRegisteredEMailService
            modelBuilder.Entity<cdRegisteredEMailService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRegisteredEMailService_cdCompany_FK");

                entity.HasOne(d => d.cdCommunicationType)
                    .WithMany()
                    .HasForeignKey(d => d.CommunicationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRegisteredEMailService_cdCommunicationType_FK");

            });

            // Configure relationships for cdRequisition
            modelBuilder.Entity<cdRequisition>(entity =>
            {
                entity.HasOne(d => d.bsItemType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisition_bsItemType_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.AveragePriceCurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisition_cdCurrency_FK");

                entity.HasOne(d => d.cdRequisitionType)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisition_cdRequisitionType_FK");

            });

            // Configure relationships for cdRequisitionAttribute
            modelBuilder.Entity<cdRequisitionAttribute>(entity =>
            {
                entity.HasOne(d => d.cdRequisitionAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionAttribute_cdRequisitionAttributeType_FK");

            });

            // Configure relationships for cdRequisitionAttributeDesc
            modelBuilder.Entity<cdRequisitionAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdRequisitionAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionAttributeDesc_cdRequisitionAttribute_FK");

                entity.HasOne(d => d.cdRequisitionAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionAttributeDesc_cdRequisitionAttribute_FK");

            });

            // Configure relationships for cdRequisitionAttributeTypeDesc
            modelBuilder.Entity<cdRequisitionAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdRequisitionAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionAttributeTypeDesc_cdRequisitionAttributeType_FK");

            });

            // Configure relationships for cdRequisitionConfirmationLimit
            modelBuilder.Entity<cdRequisitionConfirmationLimit>(entity =>
            {
                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationLimit_cdCurrency_FK");

                entity.HasOne(d => d.cdRequisitionType)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationLimit_cdRequisitionType_FK");

                entity.HasOne(d => d.cdRequisition)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationLimit_cdRequisition_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationLimit_cdCompany_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationLimit_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationLimit_cdCurrAcc_FK");

                entity.HasOne(d => d.cdJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.ConfirmationJobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationLimit_cdJobDepartment_FK");

                entity.HasOne(d => d.cdJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.JobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationLimit_cdJobDepartment_FK");

            });

            // Configure relationships for cdRequisitionConfirmationRule
            modelBuilder.Entity<cdRequisitionConfirmationRule>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationRule_cdCompany_FK");

                entity.HasOne(d => d.cdRequisition)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationRule_cdRequisition_FK");

                entity.HasOne(d => d.cdRequisitionType)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationRule_cdRequisitionType_FK");

                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationRule_cdCurrency_FK");

                entity.HasOne(d => d.cdJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.JobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationRule_cdJobDepartment_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationRule_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionConfirmationRule_cdCurrAcc_FK");

            });

            // Configure relationships for cdRequisitionDesc
            modelBuilder.Entity<cdRequisitionDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdRequisition)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionDesc_cdRequisition_FK");

            });

            // Configure relationships for cdRequisitionType
            modelBuilder.Entity<cdRequisitionType>(entity =>
            {
                entity.HasOne(d => d.bsItemType)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionType_bsItemType_FK");

            });

            // Configure relationships for cdRequisitionTypeDesc
            modelBuilder.Entity<cdRequisitionTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdRequisitionType)
                    .WithMany()
                    .HasForeignKey(d => d.RequisitionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionTypeDesc_cdRequisitionType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRequisitionTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdResignationDesc
            modelBuilder.Entity<cdResignationDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdResignationDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdResignation)
                    .WithMany()
                    .HasForeignKey(d => d.ResignationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdResignationDesc_cdResignation_FK");

            });

            // Configure relationships for cdResponsibilityAreaDesc
            modelBuilder.Entity<cdResponsibilityAreaDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdResponsibilityAreaDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdResponsibilityArea)
                    .WithMany()
                    .HasForeignKey(d => d.ResponsibilityAreaCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdResponsibilityAreaDesc_cdResponsibilityArea_FK");

            });

            // Configure relationships for cdReturnReasonDesc
            modelBuilder.Entity<cdReturnReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdReturnReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdReturnReason)
                    .WithMany()
                    .HasForeignKey(d => d.ReturnReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdReturnReasonDesc_cdReturnReason_FK");

            });

            // Configure relationships for cdRole
            modelBuilder.Entity<cdRole>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRole_cdCompany_FK");

            });

            // Configure relationships for cdRoleDesc
            modelBuilder.Entity<cdRoleDesc>(entity =>
            {
                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoleDesc_cdRole_FK");

                entity.HasOne(d => d.cdRole)
                    .WithMany()
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoleDesc_cdRole_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoleDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdRoll
            modelBuilder.Entity<cdRoll>(entity =>
            {
                entity.HasOne(d => d.cdBatchGroup)
                    .WithMany()
                    .HasForeignKey(d => d.BatchGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoll_cdBatchGroup_FK");

                entity.HasOne(d => d.cdBatch)
                    .WithMany()
                    .HasForeignKey(d => d.BatchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoll_cdBatch_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoll_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDim3Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoll_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDim2Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoll_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemDim1Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoll_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ColorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoll_prItemVariant_FK");

                entity.HasOne(d => d.prItemVariant)
                    .WithMany()
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoll_prItemVariant_FK");

            });

            // Configure relationships for cdRollNoteTypeDesc
            modelBuilder.Entity<cdRollNoteTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRollNoteTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdRollNoteType)
                    .WithMany()
                    .HasForeignKey(d => d.RollNoteTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRollNoteTypeDesc_cdRollNoteType_FK");

            });

            // Configure relationships for cdRoundsman
            modelBuilder.Entity<cdRoundsman>(entity =>
            {
                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoundsman_cdOffice_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoundsman_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoundsman_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoundsman_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdRoundsman_cdCurrAcc_FK");

            });

            // Configure relationships for cdSalesChannelDesc
            modelBuilder.Entity<cdSalesChannelDesc>(entity =>
            {
                entity.HasOne(d => d.cdSalesChannel)
                    .WithMany()
                    .HasForeignKey(d => d.SalesChannelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesChannelDesc_cdSalesChannel_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesChannelDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdSalesperson
            modelBuilder.Entity<cdSalesperson>(entity =>
            {
                entity.HasOne(d => d.cdSalespersonType)
                    .WithMany()
                    .HasForeignKey(d => d.SalespersonTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesperson_cdSalespersonType_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesperson_cdOffice_FK");

                entity.HasOne(d => d.cdSalespersonTeam)
                    .WithMany()
                    .HasForeignKey(d => d.SalespersonTeamCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesperson_cdSalespersonTeam_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesperson_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesperson_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesperson_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesperson_cdCurrAcc_FK");

            });

            // Configure relationships for cdSalespersonTeamDesc
            modelBuilder.Entity<cdSalespersonTeamDesc>(entity =>
            {
                entity.HasOne(d => d.cdSalespersonTeam)
                    .WithMany()
                    .HasForeignKey(d => d.SalespersonTeamCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalespersonTeamDesc_cdSalespersonTeam_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalespersonTeamDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdSalespersonTypeDesc
            modelBuilder.Entity<cdSalespersonTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalespersonTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSalespersonType)
                    .WithMany()
                    .HasForeignKey(d => d.SalespersonTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalespersonTypeDesc_cdSalespersonType_FK");

            });

            // Configure relationships for cdSalesPlanTypeDesc
            modelBuilder.Entity<cdSalesPlanTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdSalesPlanType)
                    .WithMany()
                    .HasForeignKey(d => d.SalesPlanTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesPlanTypeDesc_cdSalesPlanType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSalesPlanTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdScrapReasonDesc
            modelBuilder.Entity<cdScrapReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdScrapReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdScrapReason)
                    .WithMany()
                    .HasForeignKey(d => d.ScrapReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdScrapReasonDesc_cdScrapReason_FK");

            });

            // Configure relationships for cdSeasonDesc
            modelBuilder.Entity<cdSeasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdSeason)
                    .WithMany()
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSeasonDesc_cdSeason_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSeasonDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdSectionTypeDesc
            modelBuilder.Entity<cdSectionTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSectionTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSectionType)
                    .WithMany()
                    .HasForeignKey(d => d.SectionTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSectionTypeDesc_cdSectionType_FK");

            });

            // Configure relationships for cdServiceman
            modelBuilder.Entity<cdServiceman>(entity =>
            {
                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdServiceman_cdOffice_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdServiceman_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdServiceman_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdServiceman_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdServiceman_cdCurrAcc_FK");

            });

            // Configure relationships for cdSGKBorrowingTypeDesc
            modelBuilder.Entity<cdSGKBorrowingTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdSGKBorrowingType)
                    .WithMany()
                    .HasForeignKey(d => d.SGKBorrowingTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSGKBorrowingTypeDesc_cdSGKBorrowingType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSGKBorrowingTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdSGKProfessionDesc
            modelBuilder.Entity<cdSGKProfessionDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSGKProfessionDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSGKProfession)
                    .WithMany()
                    .HasForeignKey(d => d.SGKProfessionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSGKProfessionDesc_cdSGKProfession_FK");

            });

            // Configure relationships for cdShipmentMethod
            modelBuilder.Entity<cdShipmentMethod>(entity =>
            {
                entity.HasOne(d => d.bsTransportMode)
                    .WithMany()
                    .HasForeignKey(d => d.TransportModeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdShipmentMethod_bsTransportMode_FK");

            });

            // Configure relationships for cdShipmentMethodDesc
            modelBuilder.Entity<cdShipmentMethodDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdShipmentMethodDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdShipmentMethod)
                    .WithMany()
                    .HasForeignKey(d => d.ShipmentMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdShipmentMethodDesc_cdShipmentMethod_FK");

            });

            // Configure relationships for cdSMSGatewayService
            modelBuilder.Entity<cdSMSGatewayService>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSMSGatewayService_cdCompany_FK");

                entity.HasOne(d => d.bsSMSGatewayServiceCompany)
                    .WithMany()
                    .HasForeignKey(d => d.SMSGatewayServiceCompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSMSGatewayService_bsSMSGatewayServiceCompany_FK");

                entity.HasOne(d => d.bsGatewayServiceProvider)
                    .WithMany()
                    .HasForeignKey(d => d.ServiceProviderCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSMSGatewayService_bsGatewayServiceProvider_FK");

            });

            // Configure relationships for cdSMSJobTypeDesc
            modelBuilder.Entity<cdSMSJobTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSMSJobTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSMSJobType)
                    .WithMany()
                    .HasForeignKey(d => d.SMSJobTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSMSJobTypeDesc_cdSMSJobType_FK");

            });

            // Configure relationships for cdSoftware
            modelBuilder.Entity<cdSoftware>(entity =>
            {
                entity.HasOne(d => d.cdSoftwareType)
                    .WithMany()
                    .HasForeignKey(d => d.SoftWareTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSoftware_cdSoftwareType_FK");

            });

            // Configure relationships for cdSoftwareDesc
            modelBuilder.Entity<cdSoftwareDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSoftwareDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSoftware)
                    .WithMany()
                    .HasForeignKey(d => d.SoftWareCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSoftwareDesc_cdSoftware_FK");

            });

            // Configure relationships for cdSoftwareTypeDesc
            modelBuilder.Entity<cdSoftwareTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSoftwareTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSoftwareType)
                    .WithMany()
                    .HasForeignKey(d => d.SoftwareTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSoftwareTypeDesc_cdSoftwareType_FK");

            });

            // Configure relationships for cdSpecialDayTypeDesc
            modelBuilder.Entity<cdSpecialDayTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSpecialDayTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSpecialDayType)
                    .WithMany()
                    .HasForeignKey(d => d.SpecialDayTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSpecialDayTypeDesc_cdSpecialDayType_FK");

            });

            // Configure relationships for cdState
            modelBuilder.Entity<cdState>(entity =>
            {
                entity.HasOne(d => d.cdCountry)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdState_cdCountry_FK");

            });

            // Configure relationships for cdStateDesc
            modelBuilder.Entity<cdStateDesc>(entity =>
            {
                entity.HasOne(d => d.cdState)
                    .WithMany()
                    .HasForeignKey(d => d.StateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStateDesc_cdState_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStateDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdStoreCapacityLevelDesc
            modelBuilder.Entity<cdStoreCapacityLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreCapacityLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdStoreCapacityLevel)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCapacityLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreCapacityLevelDesc_cdStoreCapacityLevel_FK");

            });

            // Configure relationships for cdStoreClimateZoneDesc
            modelBuilder.Entity<cdStoreClimateZoneDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreClimateZoneDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdStoreClimateZone)
                    .WithMany()
                    .HasForeignKey(d => d.StoreClimateZoneCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreClimateZoneDesc_cdStoreClimateZone_FK");

            });

            // Configure relationships for cdStoreConceptDesc
            modelBuilder.Entity<cdStoreConceptDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreConceptDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdStoreConcept)
                    .WithMany()
                    .HasForeignKey(d => d.StoreConceptCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreConceptDesc_cdStoreConcept_FK");

            });

            // Configure relationships for cdStoreCRMGroupDesc
            modelBuilder.Entity<cdStoreCRMGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreCRMGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdStoreCRMGroup)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCRMGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreCRMGroupDesc_cdStoreCRMGroup_FK");

            });

            // Configure relationships for cdStoreDistributionGroupDesc
            modelBuilder.Entity<cdStoreDistributionGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreDistributionGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdStoreDistributionGroup)
                    .WithMany()
                    .HasForeignKey(d => d.StoreDistributionGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreDistributionGroupDesc_cdStoreDistributionGroup_FK");

            });

            // Configure relationships for cdStoreHierarchyLevelDesc
            modelBuilder.Entity<cdStoreHierarchyLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreHierarchyLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdStoreHierarchyLevel)
                    .WithMany()
                    .HasForeignKey(d => d.StoreHierarchyLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoreHierarchyLevelDesc_cdStoreHierarchyLevel_FK");

            });

            // Configure relationships for cdStorePriceLevelDesc
            modelBuilder.Entity<cdStorePriceLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStorePriceLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdStorePriceLevel)
                    .WithMany()
                    .HasForeignKey(d => d.StorePriceLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStorePriceLevelDesc_cdStorePriceLevel_FK");

            });

            // Configure relationships for cdStoryBoardDesc
            modelBuilder.Entity<cdStoryBoardDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoryBoardDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdStoryBoard)
                    .WithMany()
                    .HasForeignKey(d => d.StoryBoardCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStoryBoardDesc_cdStoryBoard_FK");

            });

            // Configure relationships for cdStreet
            modelBuilder.Entity<cdStreet>(entity =>
            {
                entity.HasOne(d => d.cdQuarter)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStreet_cdQuarter_FK");

                entity.HasOne(d => d.cdQuarter)
                    .WithMany()
                    .HasForeignKey(d => d.QuarterCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdStreet_cdQuarter_FK");

            });

            // Configure relationships for cdSubCurrAccAttribute
            modelBuilder.Entity<cdSubCurrAccAttribute>(entity =>
            {
                entity.HasOne(d => d.cdSubCurrAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttribute_cdSubCurrAccAttributeType_FK");

                entity.HasOne(d => d.cdSubCurrAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttribute_cdSubCurrAccAttributeType_FK");

            });

            // Configure relationships for cdSubCurrAccAttributeDesc
            modelBuilder.Entity<cdSubCurrAccAttributeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttributeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSubCurrAccAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttributeDesc_cdSubCurrAccAttribute_FK");

                entity.HasOne(d => d.cdSubCurrAccAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttributeDesc_cdSubCurrAccAttribute_FK");

                entity.HasOne(d => d.cdSubCurrAccAttribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttributeDesc_cdSubCurrAccAttribute_FK");

            });

            // Configure relationships for cdSubCurrAccAttributeType
            modelBuilder.Entity<cdSubCurrAccAttributeType>(entity =>
            {
                entity.HasOne(d => d.bsCurrAccType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttributeType_bsCurrAccType_FK");

            });

            // Configure relationships for cdSubCurrAccAttributeTypeDesc
            modelBuilder.Entity<cdSubCurrAccAttributeTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttributeTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSubCurrAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttributeTypeDesc_cdSubCurrAccAttributeType_FK");

                entity.HasOne(d => d.cdSubCurrAccAttributeType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubCurrAccAttributeTypeDesc_cdSubCurrAccAttributeType_FK");

            });

            // Configure relationships for cdSubJobDepartmentDesc
            modelBuilder.Entity<cdSubJobDepartmentDesc>(entity =>
            {
                entity.HasOne(d => d.cdSubJobDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.SubJobDepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubJobDepartmentDesc_cdSubJobDepartment_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubJobDepartmentDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdSubSeason
            modelBuilder.Entity<cdSubSeason>(entity =>
            {
                entity.HasOne(d => d.cdSeason)
                    .WithMany()
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubSeason_cdSeason_FK");

            });

            // Configure relationships for cdSubSeasonDesc
            modelBuilder.Entity<cdSubSeasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdSubSeason)
                    .WithMany()
                    .HasForeignKey(d => d.SeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubSeasonDesc_cdSubSeason_FK");

                entity.HasOne(d => d.cdSubSeason)
                    .WithMany()
                    .HasForeignKey(d => d.SubSeasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubSeasonDesc_cdSubSeason_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSubSeasonDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdSupportResolveTypeDesc
            modelBuilder.Entity<cdSupportResolveTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSupportResolveTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSupportResolveType)
                    .WithMany()
                    .HasForeignKey(d => d.SupportResolveTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSupportResolveTypeDesc_cdSupportResolveType_FK");

            });

            // Configure relationships for cdSupportStatusDesc
            modelBuilder.Entity<cdSupportStatusDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSupportStatusDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSupportStatus)
                    .WithMany()
                    .HasForeignKey(d => d.SupportStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSupportStatusDesc_cdSupportStatus_FK");

            });

            // Configure relationships for cdSurvey
            modelBuilder.Entity<cdSurvey>(entity =>
            {
                entity.HasOne(d => d.bsCurrAccType)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurvey_bsCurrAccType_FK");

            });

            // Configure relationships for cdSurveyDesc
            modelBuilder.Entity<cdSurveyDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSurvey)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyDesc_cdSurvey_FK");

            });

            // Configure relationships for cdSurveyQuestion
            modelBuilder.Entity<cdSurveyQuestion>(entity =>
            {
                entity.HasOne(d => d.cdSurveySection)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestion_cdSurveySection_FK");

                entity.HasOne(d => d.cdSurveySection)
                    .WithMany()
                    .HasForeignKey(d => d.SurveySectionNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestion_cdSurveySection_FK");

                entity.HasOne(d => d.bsQuestionInputType)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionInputTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestion_bsQuestionInputType_FK");

            });

            // Configure relationships for cdSurveyQuestionDesc
            modelBuilder.Entity<cdSurveyQuestionDesc>(entity =>
            {
                entity.HasOne(d => d.cdSurveyQuestion)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionDesc_cdSurveyQuestion_FK");

                entity.HasOne(d => d.cdSurveyQuestion)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionDesc_cdSurveyQuestion_FK");

                entity.HasOne(d => d.cdSurveyQuestion)
                    .WithMany()
                    .HasForeignKey(d => d.SurveySectionNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionDesc_cdSurveyQuestion_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdSurveyQuestionOption
            modelBuilder.Entity<cdSurveyQuestionOption>(entity =>
            {
                entity.HasOne(d => d.cdSurveyQuestion)
                    .WithMany()
                    .HasForeignKey(d => d.SurveySectionNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionOption_cdSurveyQuestion_FK");

                entity.HasOne(d => d.cdSurveyQuestion)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionOption_cdSurveyQuestion_FK");

                entity.HasOne(d => d.cdSurveyQuestion)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionOption_cdSurveyQuestion_FK");

            });

            // Configure relationships for cdSurveyQuestionOptionDesc
            modelBuilder.Entity<cdSurveyQuestionOptionDesc>(entity =>
            {
                entity.HasOne(d => d.cdSurveyQuestionOption)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionOptionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionOptionDesc_cdSurveyQuestionOption_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveyQuestionOptionDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdSurveySection
            modelBuilder.Entity<cdSurveySection>(entity =>
            {
                entity.HasOne(d => d.cdSurvey)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveySection_cdSurvey_FK");

            });

            // Configure relationships for cdSurveySectionDesc
            modelBuilder.Entity<cdSurveySectionDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveySectionDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdSurveySection)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveySectionDesc_cdSurveySection_FK");

                entity.HasOne(d => d.cdSurveySection)
                    .WithMany()
                    .HasForeignKey(d => d.SurveySectionNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdSurveySectionDesc_cdSurveySection_FK");

            });

            // Configure relationships for cdTaxDistrictDesc
            modelBuilder.Entity<cdTaxDistrictDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTaxDistrictDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdTaxDistrict)
                    .WithMany()
                    .HasForeignKey(d => d.TaxDistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTaxDistrictDesc_cdTaxDistrict_FK");

            });

            // Configure relationships for cdTaxOffice
            modelBuilder.Entity<cdTaxOffice>(entity =>
            {
                entity.HasOne(d => d.cdCity)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTaxOffice_cdCity_FK");

            });

            // Configure relationships for cdTaxOfficeDesc
            modelBuilder.Entity<cdTaxOfficeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTaxOfficeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdTaxOffice)
                    .WithMany()
                    .HasForeignKey(d => d.TaxOfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTaxOfficeDesc_cdTaxOffice_FK");

            });

            // Configure relationships for cdTest
            modelBuilder.Entity<cdTest>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTest_cdCompany_FK");

            });

            // Configure relationships for cdTestDesc
            modelBuilder.Entity<cdTestDesc>(entity =>
            {
                entity.HasOne(d => d.cdTest)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTestDesc_cdTest_FK");

                entity.HasOne(d => d.cdTest)
                    .WithMany()
                    .HasForeignKey(d => d.TestCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTestDesc_cdTest_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTestDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdTestTypeDesc
            modelBuilder.Entity<cdTestTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTestTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdTestType)
                    .WithMany()
                    .HasForeignKey(d => d.TestTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTestTypeDesc_cdTestType_FK");

            });

            // Configure relationships for cdTextileCareSymbol
            modelBuilder.Entity<cdTextileCareSymbol>(entity =>
            {
                entity.HasOne(d => d.bsTextileCareSymbolGr)
                    .WithMany()
                    .HasForeignKey(d => d.TextileCareSymbolGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTextileCareSymbol_bsTextileCareSymbolGr_FK");

            });

            // Configure relationships for cdTextileCareSymbolDesc
            modelBuilder.Entity<cdTextileCareSymbolDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTextileCareSymbolDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdTextileCareSymbol)
                    .WithMany()
                    .HasForeignKey(d => d.TextileCareSymbolGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTextileCareSymbolDesc_cdTextileCareSymbol_FK");

                entity.HasOne(d => d.cdTextileCareSymbol)
                    .WithMany()
                    .HasForeignKey(d => d.TextileCareSymbolCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTextileCareSymbolDesc_cdTextileCareSymbol_FK");

            });

            // Configure relationships for cdTimePeriodDesc
            modelBuilder.Entity<cdTimePeriodDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTimePeriodDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdTimePeriod)
                    .WithMany()
                    .HasForeignKey(d => d.TimePeriodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTimePeriodDesc_cdTimePeriod_FK");

            });

            // Configure relationships for cdTitleDesc
            modelBuilder.Entity<cdTitleDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTitleDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdTitle)
                    .WithMany()
                    .HasForeignKey(d => d.TitleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTitleDesc_cdTitle_FK");

            });

            // Configure relationships for cdTransactionCancelReasonDesc
            modelBuilder.Entity<cdTransactionCancelReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTransactionCancelReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdTransactionCancelReason)
                    .WithMany()
                    .HasForeignKey(d => d.TransactionCancelReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTransactionCancelReasonDesc_cdTransactionCancelReason_FK");

            });

            // Configure relationships for cdTransferPlanTemplate
            modelBuilder.Entity<cdTransferPlanTemplate>(entity =>
            {
                entity.HasOne(d => d.bsTransferPlanRule)
                    .WithMany()
                    .HasForeignKey(d => d.TransferPlanRuleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTransferPlanTemplate_bsTransferPlanRule_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTransferPlanTemplate_cdCompany_FK");

            });

            // Configure relationships for cdTransferPlanTemplateDesc
            modelBuilder.Entity<cdTransferPlanTemplateDesc>(entity =>
            {
                entity.HasOne(d => d.cdTransferPlanTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTransferPlanTemplateDesc_cdTransferPlanTemplate_FK");

                entity.HasOne(d => d.cdTransferPlanTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.TransferPlanTemplateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTransferPlanTemplateDesc_cdTransferPlanTemplate_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTransferPlanTemplateDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdTranslationProvider
            modelBuilder.Entity<cdTranslationProvider>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTranslationProvider_cdCompany_FK");

            });

            // Configure relationships for cdTurnoverTargetTypeDesc
            modelBuilder.Entity<cdTurnoverTargetTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdTurnoverTargetType)
                    .WithMany()
                    .HasForeignKey(d => d.TurnoverTargetTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTurnoverTargetTypeDesc_cdTurnoverTargetType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdTurnoverTargetTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdUnDeliveryReasonDesc
            modelBuilder.Entity<cdUnDeliveryReasonDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUnDeliveryReasonDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdUnDeliveryReason)
                    .WithMany()
                    .HasForeignKey(d => d.UnDeliveryReasonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUnDeliveryReasonDesc_cdUnDeliveryReason_FK");

            });

            // Configure relationships for cdUniFreeTenderType
            modelBuilder.Entity<cdUniFreeTenderType>(entity =>
            {
                entity.HasOne(d => d.cdCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniFreeTenderType_cdCurrency_FK");

                entity.HasOne(d => d.cdExchangeType)
                    .WithMany()
                    .HasForeignKey(d => d.ExchangeTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniFreeTenderType_cdExchangeType_FK");

            });

            // Configure relationships for cdUnitOfMeasure
            modelBuilder.Entity<cdUnitOfMeasure>(entity =>
            {
                entity.HasOne(d => d.cdInternationalUnitOfMeasure)
                    .WithMany()
                    .HasForeignKey(d => d.InternationalUnitOfMeasureCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUnitOfMeasure_cdInternationalUnitOfMeasure_FK");

            });

            // Configure relationships for cdUnitOfMeasureDesc
            modelBuilder.Entity<cdUnitOfMeasureDesc>(entity =>
            {
                entity.HasOne(d => d.cdUnitOfMeasure)
                    .WithMany()
                    .HasForeignKey(d => d.UnitOfMeasureCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUnitOfMeasureDesc_cdUnitOfMeasure_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUnitOfMeasureDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdUniversity
            modelBuilder.Entity<cdUniversity>(entity =>
            {
                entity.HasOne(d => d.cdCountry)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversity_cdCountry_FK");

                entity.HasOne(d => d.cdUniversityType)
                    .WithMany()
                    .HasForeignKey(d => d.UniversityTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversity_cdUniversityType_FK");

            });

            // Configure relationships for cdUniversityDesc
            modelBuilder.Entity<cdUniversityDesc>(entity =>
            {
                entity.HasOne(d => d.cdUniversity)
                    .WithMany()
                    .HasForeignKey(d => d.UniversityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityDesc_cdUniversity_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdUniversityFacultyDepDesc
            modelBuilder.Entity<cdUniversityFacultyDepDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityFacultyDepDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdUniversityFacultyDep)
                    .WithMany()
                    .HasForeignKey(d => d.UniversityFacultyDepCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityFacultyDepDesc_cdUniversityFacultyDep_FK");

            });

            // Configure relationships for cdUniversityFacultyDesc
            modelBuilder.Entity<cdUniversityFacultyDesc>(entity =>
            {
                entity.HasOne(d => d.cdUniversityFaculty)
                    .WithMany()
                    .HasForeignKey(d => d.UniversityFacultyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityFacultyDesc_cdUniversityFaculty_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityFacultyDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdUniversityLevelDesc
            modelBuilder.Entity<cdUniversityLevelDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityLevelDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdUniversityLevel)
                    .WithMany()
                    .HasForeignKey(d => d.UniversityLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityLevelDesc_cdUniversityLevel_FK");

            });

            // Configure relationships for cdUniversityTypeDesc
            modelBuilder.Entity<cdUniversityTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdUniversityType)
                    .WithMany()
                    .HasForeignKey(d => d.UniversityTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityTypeDesc_cdUniversityType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUniversityTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdUserWarningDesc
            modelBuilder.Entity<cdUserWarningDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUserWarningDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdUserWarning)
                    .WithMany()
                    .HasForeignKey(d => d.UserWarningCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdUserWarningDesc_cdUserWarning_FK");

            });

            // Configure relationships for cdVat
            modelBuilder.Entity<cdVat>(entity =>
            {
                entity.HasOne(d => d.bsOfficialTaxType)
                    .WithMany()
                    .HasForeignKey(d => d.OfficialTaxTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVat_bsOfficialTaxType_FK");

            });

            // Configure relationships for cdVatDesc
            modelBuilder.Entity<cdVatDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVatDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdVat)
                    .WithMany()
                    .HasForeignKey(d => d.VatCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVatDesc_cdVat_FK");

            });

            // Configure relationships for cdVehicle
            modelBuilder.Entity<cdVehicle>(entity =>
            {
                entity.HasOne(d => d.cdVehicleType)
                    .WithMany()
                    .HasForeignKey(d => d.VehicleTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVehicle_cdVehicleType_FK");

            });

            // Configure relationships for cdVehicleTypeDesc
            modelBuilder.Entity<cdVehicleTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdVehicleType)
                    .WithMany()
                    .HasForeignKey(d => d.VehicleTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVehicleTypeDesc_cdVehicleType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVehicleTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdVendorPaymentPlanGrDesc
            modelBuilder.Entity<cdVendorPaymentPlanGrDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVendorPaymentPlanGrDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdVendorPaymentPlanGr)
                    .WithMany()
                    .HasForeignKey(d => d.VendorPaymentPlanGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVendorPaymentPlanGrDesc_cdVendorPaymentPlanGr_FK");

            });

            // Configure relationships for cdVisitFrequencyDesc
            modelBuilder.Entity<cdVisitFrequencyDesc>(entity =>
            {
                entity.HasOne(d => d.cdVisitFrequency)
                    .WithMany()
                    .HasForeignKey(d => d.VisitFrequencyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVisitFrequencyDesc_cdVisitFrequency_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdVisitFrequencyDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdWageGarnishmentTypeDesc
            modelBuilder.Entity<cdWageGarnishmentTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWageGarnishmentTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdWageGarnishmentType)
                    .WithMany()
                    .HasForeignKey(d => d.WageGarnishmentTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWageGarnishmentTypeDesc_cdWageGarnishmentType_FK");

            });

            // Configure relationships for cdWagePlanTypeDesc
            modelBuilder.Entity<cdWagePlanTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWagePlanTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdWagePlanType)
                    .WithMany()
                    .HasForeignKey(d => d.WagePlanTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWagePlanTypeDesc_cdWagePlanType_FK");

            });

            // Configure relationships for cdWarehouse
            modelBuilder.Entity<cdWarehouse>(entity =>
            {
                entity.HasOne(d => d.bsWarehouseOwner)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseOwnerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouse_bsWarehouseOwner_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouse_cdOffice_FK");

                entity.HasOne(d => d.cdWarehouseType)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouse_cdWarehouseType_FK");

                entity.HasOne(d => d.prSubCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.SubCurrAccID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouse_prSubCurrAcc_FK");

                entity.HasOne(d => d.cdWarehouseCategory)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseCategoryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouse_cdWarehouseCategory_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouse_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.CurrAccTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouse_cdCurrAcc_FK");

            });

            // Configure relationships for cdWarehouseCategoryDesc
            modelBuilder.Entity<cdWarehouseCategoryDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseCategoryDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdWarehouseCategory)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseCategoryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseCategoryDesc_cdWarehouseCategory_FK");

            });

            // Configure relationships for cdWarehouseChannelTemplate
            modelBuilder.Entity<cdWarehouseChannelTemplate>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseChannelTemplate_cdCompany_FK");

            });

            // Configure relationships for cdWarehouseChannelTemplateDesc
            modelBuilder.Entity<cdWarehouseChannelTemplateDesc>(entity =>
            {
                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseChannelTemplateDesc_cdCompany_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseChannelTemplateDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdWarehouseChannelTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseChannelTemplateDesc_cdWarehouseChannelTemplate_FK");

                entity.HasOne(d => d.cdWarehouseChannelTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseChannelTemplateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseChannelTemplateDesc_cdWarehouseChannelTemplate_FK");

            });

            // Configure relationships for cdWarehouseDesc
            modelBuilder.Entity<cdWarehouseDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdWarehouse)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseDesc_cdWarehouse_FK");

            });

            // Configure relationships for cdWarehouseTypeDesc
            modelBuilder.Entity<cdWarehouseTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseTypeDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdWarehouseType)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWarehouseTypeDesc_cdWarehouseType_FK");

            });

            // Configure relationships for cdWorkForceDesc
            modelBuilder.Entity<cdWorkForceDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkForceDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdWorkForce)
                    .WithMany()
                    .HasForeignKey(d => d.WorkForceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkForceDesc_cdWorkForce_FK");

            });

            // Configure relationships for cdWorkPlace
            modelBuilder.Entity<cdWorkPlace>(entity =>
            {
                entity.HasOne(d => d.bsWorkplacePropertyStatus)
                    .WithMany()
                    .HasForeignKey(d => d.WorkplacePropertyStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_bsWorkplacePropertyStatus_FK");

                entity.HasOne(d => d.bsWorkplaceKind)
                    .WithMany()
                    .HasForeignKey(d => d.WorkplaceKindCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_bsWorkplaceKind_FK");

                entity.HasOne(d => d.cdOffice)
                    .WithMany()
                    .HasForeignKey(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdOffice_FK");

                entity.HasOne(d => d.cdState)
                    .WithMany()
                    .HasForeignKey(d => d.StateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdState_FK");

                entity.HasOne(d => d.cdCity)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdCity_FK");

                entity.HasOne(d => d.cdTradeRegistryOffice)
                    .WithMany()
                    .HasForeignKey(d => d.TradeRegistryOfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdTradeRegistryOffice_FK");

                entity.HasOne(d => d.bsSGKWorkPlaceSector)
                    .WithMany()
                    .HasForeignKey(d => d.SGKWorkPlaceSectorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_bsSGKWorkPlaceSector_FK");

                entity.HasOne(d => d.cdCountry)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdCountry_FK");

                entity.HasOne(d => d.cdCompany)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdCompany_FK");

                entity.HasOne(d => d.cdDistrict)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdDistrict_FK");

                entity.HasOne(d => d.cdWorkPlaceType)
                    .WithMany()
                    .HasForeignKey(d => d.WorkPlaceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdWorkPlaceType_FK");

                entity.HasOne(d => d.cdWarehouse)
                    .WithMany()
                    .HasForeignKey(d => d.WarehouseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdWarehouse_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdStreet_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.QuarterCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdStreet_FK");

                entity.HasOne(d => d.cdWorkPlaceGroup)
                    .WithMany()
                    .HasForeignKey(d => d.WorkPlaceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdWorkPlaceGroup_FK");

                entity.HasOne(d => d.cdStreet)
                    .WithMany()
                    .HasForeignKey(d => d.StreetCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdStreet_FK");

                entity.HasOne(d => d.bsWorkDangerLevel)
                    .WithMany()
                    .HasForeignKey(d => d.WorkDangerLevelCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_bsWorkDangerLevel_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdCurrAcc_FK");

                entity.HasOne(d => d.cdCurrAcc)
                    .WithMany()
                    .HasForeignKey(d => d.StoreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlace_cdCurrAcc_FK");

            });

            // Configure relationships for cdWorkPlaceDesc
            modelBuilder.Entity<cdWorkPlaceDesc>(entity =>
            {
                entity.HasOne(d => d.cdWorkPlace)
                    .WithMany()
                    .HasForeignKey(d => d.WorkPlaceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlaceDesc_cdWorkPlace_FK");

                entity.HasOne(d => d.cdWorkPlace)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlaceDesc_cdWorkPlace_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlaceDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdWorkPlaceGroupDesc
            modelBuilder.Entity<cdWorkPlaceGroupDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlaceGroupDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdWorkPlaceGroup)
                    .WithMany()
                    .HasForeignKey(d => d.WorkPlaceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlaceGroupDesc_cdWorkPlaceGroup_FK");

            });

            // Configure relationships for cdWorkPlaceTypeDesc
            modelBuilder.Entity<cdWorkPlaceTypeDesc>(entity =>
            {
                entity.HasOne(d => d.cdWorkPlaceType)
                    .WithMany()
                    .HasForeignKey(d => d.WorkPlaceTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlaceTypeDesc_cdWorkPlaceType_FK");

                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdWorkPlaceTypeDesc_cdDataLanguage_FK");

            });

            // Configure relationships for cdZoneDesc
            modelBuilder.Entity<cdZoneDesc>(entity =>
            {
                entity.HasOne(d => d.cdDataLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.LangCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdZoneDesc_cdDataLanguage_FK");

                entity.HasOne(d => d.cdZone)
                    .WithMany()
                    .HasForeignKey(d => d.ZoneCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cdZoneDesc_cdZone_FK");

            });

           

        }
    }
}
