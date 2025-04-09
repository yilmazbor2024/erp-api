using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDataLanguage")]
    public partial class cdDataLanguage
    {
        public cdDataLanguage()
        {
            bsAccountDetailDescs = new HashSet<bsAccountDetailDesc>();
            bsAdjustCostMethodDescs = new HashSet<bsAdjustCostMethodDesc>();
            bsAirportIATADescs = new HashSet<bsAirportIATADesc>();
            bsAllocationRuleDescs = new HashSet<bsAllocationRuleDesc>();
            bsAllocationSourceTypeDescs = new HashSet<bsAllocationSourceTypeDesc>();
            bsApplicationDescs = new HashSet<bsApplicationDesc>();
            bsBadDebtResultDescs = new HashSet<bsBadDebtResultDesc>();
            bsBadDebtTransTypeDescs = new HashSet<bsBadDebtTransTypeDesc>();
            bsBankAdditionalChargeTypeDescs = new HashSet<bsBankAdditionalChargeTypeDesc>();
            bsBankCardTypeDescs = new HashSet<bsBankCardTypeDesc>();
            bsBankCreditGuaranteeTypeDescs = new HashSet<bsBankCreditGuaranteeTypeDesc>();
            bsBankPOSImportTypeDescs = new HashSet<bsBankPOSImportTypeDesc>();
            bsBankTransTypeDescs = new HashSet<bsBankTransTypeDesc>();
            bsBasePriceDescs = new HashSet<bsBasePriceDesc>();
            bsBOMEntityLevelDescs = new HashSet<bsBOMEntityLevelDesc>();
            bsBrowseMethodTypeDescs = new HashSet<bsBrowseMethodTypeDesc>();
            bsBudgetDetailDescs = new HashSet<bsBudgetDetailDesc>();
            bsBulkMailServiceProviderDescs = new HashSet<bsBulkMailServiceProviderDesc>();
            bsCashTransTypeDescs = new HashSet<bsCashTransTypeDesc>();
            bsChannelTypeDescs = new HashSet<bsChannelTypeDesc>();
            bsChequeTransTypeDescs = new HashSet<bsChequeTransTypeDesc>();
            bsChequeTypeDescs = new HashSet<bsChequeTypeDesc>();
            bsCommunicationKindDescs = new HashSet<bsCommunicationKindDesc>();
            bsConfirmationRuleTypeDescs = new HashSet<bsConfirmationRuleTypeDesc>();
            bsConfirmationStatusDescs = new HashSet<bsConfirmationStatusDesc>();
            bsConfirmationTypeDescs = new HashSet<bsConfirmationTypeDesc>();
            bsContractTypeDescs = new HashSet<bsContractTypeDesc>();
            bsCostingLevelDescs = new HashSet<bsCostingLevelDesc>();
            bsCostingMethodDescs = new HashSet<bsCostingMethodDesc>();
            bsCostingVariantLevelDescs = new HashSet<bsCostingVariantLevelDesc>();
            bsCreditCardPaymentTypeDescs = new HashSet<bsCreditCardPaymentTypeDesc>();
            bsCreditTypeDescs = new HashSet<bsCreditTypeDesc>();
            bsCurrAccTypeDescs = new HashSet<bsCurrAccTypeDesc>();
            bsCustomerTypeDescs = new HashSet<bsCustomerTypeDesc>();
            bsCustomsProductGroupDescs = new HashSet<bsCustomsProductGroupDesc>();
            bsDayDescs = new HashSet<bsDayDesc>();
            bsDebitTypeDescs = new HashSet<bsDebitTypeDesc>();
            bsDebtStatusTypeDescs = new HashSet<bsDebtStatusTypeDesc>();
            bsDeclarationCapacityDescs = new HashSet<bsDeclarationCapacityDesc>();
            bsDeclarationTypeDescs = new HashSet<bsDeclarationTypeDesc>();
            bsDepreciationMethodDescs = new HashSet<bsDepreciationMethodDesc>();
            bsDeviceDescs = new HashSet<bsDeviceDesc>();
            bsDeviceTypeDescs = new HashSet<bsDeviceTypeDesc>();
            bsDiscountLevelOfUseDescs = new HashSet<bsDiscountLevelOfUseDesc>();
            bsDiscountOfferApplyDescs = new HashSet<bsDiscountOfferApplyDesc>();
            bsDiscountOfferMethodDescs = new HashSet<bsDiscountOfferMethodDesc>();
            bsDiscountOfferStageDescs = new HashSet<bsDiscountOfferStageDesc>();
            bsDiscountOfferTypeDescs = new HashSet<bsDiscountOfferTypeDesc>();
            bsDiscountVoucherBaseDescs = new HashSet<bsDiscountVoucherBaseDesc>();
            bsDispOrderTypeDescs = new HashSet<bsDispOrderTypeDesc>();
            bsDocumentTypeDescs = new HashSet<bsDocumentTypeDesc>();
            bsEasyStartupStepsDescs = new HashSet<bsEasyStartupStepsDesc>();
            bsEditMaskDescs = new HashSet<bsEditMaskDesc>();
            bsEInvoiceStatusDescs = new HashSet<bsEInvoiceStatusDesc>();
            bsEmailTypeDescs = new HashSet<bsEmailTypeDesc>();
            bsEmployeePayTypeDescs = new HashSet<bsEmployeePayTypeDesc>();
            bsEmployeeSpecialTypeDescs = new HashSet<bsEmployeeSpecialTypeDesc>();
            bsEShipmentStatusDescs = new HashSet<bsEShipmentStatusDesc>();
            bsExpenseSlipTypeDescs = new HashSet<bsExpenseSlipTypeDesc>();
            bsEyeGlassSutTypeDescs = new HashSet<bsEyeGlassSutTypeDesc>();
            bsFastDeliveryCompanyDescs = new HashSet<bsFastDeliveryCompanyDesc>();
            bsFileFormatTypeDescs = new HashSet<bsFileFormatTypeDesc>();
            bsFolderDescs = new HashSet<bsFolderDesc>();
            bsFormatTypeDescs = new HashSet<bsFormatTypeDesc>();
            bsGenderDescs = new HashSet<bsGenderDesc>();
            bsGiftCardPaymentTypeDescs = new HashSet<bsGiftCardPaymentTypeDesc>();
            bsIncompleteDownPaymentDistributionTypeDescs = new HashSet<bsIncompleteDownPaymentDistributionTypeDesc>();
            bsIncotermDescs = new HashSet<bsIncotermDesc>();
            bsInnerOrderTypeDescs = new HashSet<bsInnerOrderTypeDesc>();
            bsInnerProcessDescs = new HashSet<bsInnerProcessDesc>();
            bsInvoiceReturnTypeDescs = new HashSet<bsInvoiceReturnTypeDesc>();
            bsInvoiceTypeDescs = new HashSet<bsInvoiceTypeDesc>();
            bsItemDimTypeDescs = new HashSet<bsItemDimTypeDesc>();
            bsItemProcessPermitTypeDescs = new HashSet<bsItemProcessPermitTypeDesc>();
            bsItemTypeDescs = new HashSet<bsItemTypeDesc>();
            bsJournalTypeDescs = new HashSet<bsJournalTypeDesc>();
            bsLensTypeDescs = new HashSet<bsLensTypeDesc>();
            bsLetterOfGuaranteeTypeDescs = new HashSet<bsLetterOfGuaranteeTypeDesc>();
            bsLinkedProductTypeDescs = new HashSet<bsLinkedProductTypeDesc>();
            bsLoyaltyProgramProcessDescs = new HashSet<bsLoyaltyProgramProcessDesc>();
            bsMessageImportanceDescs = new HashSet<bsMessageImportanceDesc>();
            bsNebimV3ServicesDescs = new HashSet<bsNebimV3ServicesDesc>();
            bsNebimV3WindowsServicesDescs = new HashSet<bsNebimV3WindowsServicesDesc>();
            bsOrderDeliveryRecordTypeDescs = new HashSet<bsOrderDeliveryRecordTypeDesc>();
            bsOrderTypeDescs = new HashSet<bsOrderTypeDesc>();
            bsOtherPaymentTypeDescs = new HashSet<bsOtherPaymentTypeDesc>();
            bsPackagingTypeDescs = new HashSet<bsPackagingTypeDesc>();
            bsPaymentMeansDescs = new HashSet<bsPaymentMeansDesc>();
            bsPaymentTypeDescs = new HashSet<bsPaymentTypeDesc>();
            bsPayTypeDescs = new HashSet<bsPayTypeDesc>();
            bsPickingTypeDescs = new HashSet<bsPickingTypeDesc>();
            bsPointBaseDescs = new HashSet<bsPointBaseDesc>();
            bsPointRecordTypeDescs = new HashSet<bsPointRecordTypeDesc>();
            bsPolicyCustomerEditDescs = new HashSet<bsPolicyCustomerEditDesc>();
            bsPolicyCustomerPaymentDescs = new HashSet<bsPolicyCustomerPaymentDesc>();
            bsPolicyCustomerSharingDescs = new HashSet<bsPolicyCustomerSharingDesc>();
            bsPolicyVendorSharingDescs = new HashSet<bsPolicyVendorSharingDesc>();
            bsPOSModeDescs = new HashSet<bsPOSModeDesc>();
            bsPostAccTypeDescs = new HashSet<bsPostAccTypeDesc>();
            bsPresentCardActivationProcessDescs = new HashSet<bsPresentCardActivationProcessDesc>();
            bsPresentCardActivationStatusDescs = new HashSet<bsPresentCardActivationStatusDesc>();
            bsPresentCardActivationTypeDescs = new HashSet<bsPresentCardActivationTypeDesc>();
            bsProcessDescs = new HashSet<bsProcessDesc>();
            bsProcessFlowDescs = new HashSet<bsProcessFlowDesc>();
            bsProductTypeDescs = new HashSet<bsProductTypeDesc>();
            bsQuestionInputTypeDescs = new HashSet<bsQuestionInputTypeDesc>();
            bsReconciliationTypeDescs = new HashSet<bsReconciliationTypeDesc>();
            bsReserveTypeDescs = new HashSet<bsReserveTypeDesc>();
            bsSGKInsuaranceTypeDescs = new HashSet<bsSGKInsuaranceTypeDesc>();
            bsSGKMissionDescs = new HashSet<bsSGKMissionDesc>();
            bsSGKWorkPlaceSectorDescs = new HashSet<bsSGKWorkPlaceSectorDesc>();
            bsShipmentTypeDescs = new HashSet<bsShipmentTypeDesc>();
            bsSMSStatusDescs = new HashSet<bsSMSStatusDesc>();
            bsStandardBarcodeTypeDescs = new HashSet<bsStandardBarcodeTypeDesc>();
            bsSupportTypeDescs = new HashSet<bsSupportTypeDesc>();
            bsTaxExemptionDescs = new HashSet<bsTaxExemptionDesc>();
            bsTaxPaymentAccTypeDescs = new HashSet<bsTaxPaymentAccTypeDesc>();
            bsTaxPaymentTypeDescs = new HashSet<bsTaxPaymentTypeDesc>();
            bsTaxTypeDescs = new HashSet<bsTaxTypeDesc>();
            bsTextileCareSymbolGrDescs = new HashSet<bsTextileCareSymbolGrDesc>();
            bsTransferPlanRuleDescs = new HashSet<bsTransferPlanRuleDesc>();
            bsTransportModeDescs = new HashSet<bsTransportModeDesc>();
            bsTransTypeDescs = new HashSet<bsTransTypeDesc>();
            bsVendorTypeDescs = new HashSet<bsVendorTypeDesc>();
            bsWarehouseOwnerDescs = new HashSet<bsWarehouseOwnerDesc>();
            bsWorkDangerLevelDescs = new HashSet<bsWorkDangerLevelDesc>();
            bsWorkplaceKindDescs = new HashSet<bsWorkplaceKindDesc>();
            bsWorkplacePropertyStatusDescs = new HashSet<bsWorkplacePropertyStatusDesc>();
            cdAccountantDescs = new HashSet<cdAccountantDesc>();
            cdAddressShareCompanyWebServiceDescs = new HashSet<cdAddressShareCompanyWebServiceDesc>();
            cdAddressTypeDescs = new HashSet<cdAddressTypeDesc>();
            cdAllocationTemplateDescs = new HashSet<cdAllocationTemplateDesc>();
            cdAmountRuleDescs = new HashSet<cdAmountRuleDesc>();
            cdATAttributeDescs = new HashSet<cdATAttributeDesc>();
            cdATAttributeTypeDescs = new HashSet<cdATAttributeTypeDesc>();
            cdBadDebtLetterTypeDescs = new HashSet<cdBadDebtLetterTypeDesc>();
            cdBadDebtReasonDescs = new HashSet<cdBadDebtReasonDesc>();
            cdBankAccTypeDescs = new HashSet<cdBankAccTypeDesc>();
            cdBankCreditTypeDescs = new HashSet<cdBankCreditTypeDesc>();
            cdBankDescs = new HashSet<cdBankDesc>();
            cdBankOpTypeDescs = new HashSet<cdBankOpTypeDesc>();
            cdBarcodeTypeDescs = new HashSet<cdBarcodeTypeDesc>();
            cdBaseMaterialDescs = new HashSet<cdBaseMaterialDesc>();
            cdBatchDescs = new HashSet<cdBatchDesc>();
            cdBatchGroupDescs = new HashSet<cdBatchGroupDesc>();
            cdBloodTypeDescs = new HashSet<cdBloodTypeDesc>();
            cdBOMDescs = new HashSet<cdBOMDesc>();
            cdBOMEntityDescs = new HashSet<cdBOMEntityDesc>();
            cdBOMTemplateAttributeDescs = new HashSet<cdBOMTemplateAttributeDesc>();
            cdBOMTemplateAttributeTypeDescs = new HashSet<cdBOMTemplateAttributeTypeDesc>();
            cdBOMTemplateDescs = new HashSet<cdBOMTemplateDesc>();
            cdBrandDescs = new HashSet<cdBrandDesc>();
            cdBudgetTypeDescs = new HashSet<cdBudgetTypeDesc>();
            cdBusinessGroupDescs = new HashSet<cdBusinessGroupDesc>();
            cdCareWarningDescs = new HashSet<cdCareWarningDesc>();
            cdCareWarningTemplateDescs = new HashSet<cdCareWarningTemplateDesc>();
            cdChannelTemplateDescs = new HashSet<cdChannelTemplateDesc>();
            cdCheckOutReasonDescs = new HashSet<cdCheckOutReasonDesc>();
            cdChequeAttributeDescs = new HashSet<cdChequeAttributeDesc>();
            cdChequeAttributeTypeDescs = new HashSet<cdChequeAttributeTypeDesc>();
            cdChequeDenyReasonDescs = new HashSet<cdChequeDenyReasonDesc>();
            cdChequeDescs = new HashSet<cdChequeDesc>();
            cdCityDescs = new HashSet<cdCityDesc>();
            cdCoatingTypeDescs = new HashSet<cdCoatingTypeDesc>();
            cdCollectionDescs = new HashSet<cdCollectionDesc>();
            cdColorCatalogDescs = new HashSet<cdColorCatalogDesc>();
            cdColorDescs = new HashSet<cdColorDesc>();
            cdColorGroupDescs = new HashSet<cdColorGroupDesc>();
            cdColorThemeAttributeDescs = new HashSet<cdColorThemeAttributeDesc>();
            cdColorThemeAttributeTypeDescs = new HashSet<cdColorThemeAttributeTypeDesc>();
            cdColorThemeDescs = new HashSet<cdColorThemeDesc>();
            cdColorTypeDescs = new HashSet<cdColorTypeDesc>();
            cdCommercialRoleDescs = new HashSet<cdCommercialRoleDesc>();
            cdCommunicationTypeDescs = new HashSet<cdCommunicationTypeDesc>();
            cdCompanyBrandDescs = new HashSet<cdCompanyBrandDesc>();
            cdCompanyCreditCardDescs = new HashSet<cdCompanyCreditCardDesc>();
            cdConditionTypeDescs = new HashSet<cdConditionTypeDesc>();
            cdConfirmationFormStatusDescs = new HashSet<cdConfirmationFormStatusDesc>();
            cdConfirmationFormTypeDescs = new HashSet<cdConfirmationFormTypeDesc>();
            cdConfirmationReasonDescs = new HashSet<cdConfirmationReasonDesc>();
            cdContactTypeDescs = new HashSet<cdContactTypeDesc>();
            cdContainerTypeDescs = new HashSet<cdContainerTypeDesc>();
            cdContractContentDescs = new HashSet<cdContractContentDesc>();
            cdContractStatusDescs = new HashSet<cdContractStatusDesc>();
            cdCostCenterAttributeDescs = new HashSet<cdCostCenterAttributeDesc>();
            cdCostCenterAttributeTypeDescs = new HashSet<cdCostCenterAttributeTypeDesc>();
            cdCostCenterDescs = new HashSet<cdCostCenterDesc>();
            cdCostOfGoodsSoldPeriodDescs = new HashSet<cdCostOfGoodsSoldPeriodDesc>();
            cdCountryDescs = new HashSet<cdCountryDesc>();
            cdCreditCardTypeDescs = new HashSet<cdCreditCardTypeDesc>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdCurrAccAttributeDescs = new HashSet<cdCurrAccAttributeDesc>();
            cdCurrAccAttributeTypeDescs = new HashSet<cdCurrAccAttributeTypeDesc>();
            cdCurrAccDescs = new HashSet<cdCurrAccDesc>();
            cdCurrAccListDescs = new HashSet<cdCurrAccListDesc>();
            cdCurrAccLotGrDescs = new HashSet<cdCurrAccLotGrDesc>();
            cdCurrencyDescs = new HashSet<cdCurrencyDesc>();
            cdCustomerAlertColorDescs = new HashSet<cdCustomerAlertColorDesc>();
            cdCustomerCompanyBrandAttributeDescs = new HashSet<cdCustomerCompanyBrandAttributeDesc>();
            cdCustomerCompanyBrandAttributeTypeDescs = new HashSet<cdCustomerCompanyBrandAttributeTypeDesc>();
            cdCustomerConversationResultDescs = new HashSet<cdCustomerConversationResultDesc>();
            cdCustomerConversationSubjectDescs = new HashSet<cdCustomerConversationSubjectDesc>();
            cdCustomerConversationSubjectDetailDescs = new HashSet<cdCustomerConversationSubjectDetailDesc>();
            cdCustomerConversationSubtitleDescs = new HashSet<cdCustomerConversationSubtitleDesc>();
            cdCustomerCRMGroupDescs = new HashSet<cdCustomerCRMGroupDesc>();
            cdCustomerDiscountGrDescs = new HashSet<cdCustomerDiscountGrDesc>();
            cdCustomerMarkupGrDescs = new HashSet<cdCustomerMarkupGrDesc>();
            cdCustomerPaymentPlanGrDescs = new HashSet<cdCustomerPaymentPlanGrDesc>();
            cdCustomerShoppingHabitDescs = new HashSet<cdCustomerShoppingHabitDesc>();
            cdCustomerShoppingLevelDescs = new HashSet<cdCustomerShoppingLevelDesc>();
            cdCustomProcessGroupDescs = new HashSet<cdCustomProcessGroupDesc>();
            cdCustomsCompanyDescs = new HashSet<cdCustomsCompanyDesc>();
            cdCustomsOfficesDescs = new HashSet<cdCustomsOfficesDesc>();
            cdCustomsTariffNumberDescs = new HashSet<cdCustomsTariffNumberDesc>();
            cdDataLanguageDescs = new HashSet<cdDataLanguageDesc>();
            cdDataTransferCompanyDescs = new HashSet<cdDataTransferCompanyDesc>();
            cdDebitReasonDescs = new HashSet<cdDebitReasonDesc>();
            cdDeclarationDescs = new HashSet<cdDeclarationDesc>();
            cdDeductionDescs = new HashSet<cdDeductionDesc>();
            cdDeliveryCompanyDescs = new HashSet<cdDeliveryCompanyDesc>();
            cdDiagnosticDescs = new HashSet<cdDiagnosticDesc>();
            cdDigitalMarketingServiceDescs = new HashSet<cdDigitalMarketingServiceDesc>();
            cdDiscountOfferAttributeDescs = new HashSet<cdDiscountOfferAttributeDesc>();
            cdDiscountOfferAttributeTypeDescs = new HashSet<cdDiscountOfferAttributeTypeDesc>();
            cdDiscountOfferDescs = new HashSet<cdDiscountOfferDesc>();
            cdDiscountPointTypeDescs = new HashSet<cdDiscountPointTypeDesc>();
            cdDiscountReasonDescs = new HashSet<cdDiscountReasonDesc>();
            cdDiscountSubReasonDescs = new HashSet<cdDiscountSubReasonDesc>();
            cdDiscountTypeDescs = new HashSet<cdDiscountTypeDesc>();
            cdDiscountVoucherTypeDescs = new HashSet<cdDiscountVoucherTypeDesc>();
            cdDistrictDescs = new HashSet<cdDistrictDesc>();
            cdDOVDescs = new HashSet<cdDOVDesc>();
            cdDueDateFormulaDescs = new HashSet<cdDueDateFormulaDesc>();
            cdEArchiveWebServiceDescs = new HashSet<cdEArchiveWebServiceDesc>();
            cdEarningsDescs = new HashSet<cdEarningsDesc>();
            cdEducationStatusDescs = new HashSet<cdEducationStatusDesc>();
            cdEInvoiceWebServiceDescs = new HashSet<cdEInvoiceWebServiceDesc>();
            cdEMailServiceDescs = new HashSet<cdEMailServiceDesc>();
            cdEmployeeDocumentTypeDescs = new HashSet<cdEmployeeDocumentTypeDesc>();
            cdEmployeeRecordTypeDescs = new HashSet<cdEmployeeRecordTypeDesc>();
            cdEmployeeSocialInsuranceStatusDescs = new HashSet<cdEmployeeSocialInsuranceStatusDesc>();
            cdEmployeeTaxStatusDescs = new HashSet<cdEmployeeTaxStatusDesc>();
            cdEmploymentLawDescs = new HashSet<cdEmploymentLawDesc>();
            cdEShipmentWebServiceDescs = new HashSet<cdEShipmentWebServiceDesc>();
            cdExchangeTypeDescs = new HashSet<cdExchangeTypeDesc>();
            cdExecutionOfficeDescs = new HashSet<cdExecutionOfficeDesc>();
            cdExpensePeriodDescs = new HashSet<cdExpensePeriodDesc>();
            cdExpenseTypeDescs = new HashSet<cdExpenseTypeDesc>();
            cdExportFileAttributeDescs = new HashSet<cdExportFileAttributeDesc>();
            cdExportFileAttributeTypeDescs = new HashSet<cdExportFileAttributeTypeDesc>();
            cdExportFileDescs = new HashSet<cdExportFileDesc>();
            cdExportTypeDescs = new HashSet<cdExportTypeDesc>();
            cdFabricDescs = new HashSet<cdFabricDesc>();
            cdFinanceCompanyWebServiceDescs = new HashSet<cdFinanceCompanyWebServiceDesc>();
            cdFiscalPeriodDescs = new HashSet<cdFiscalPeriodDesc>();
            cdFixedAssetStatusDescs = new HashSet<cdFixedAssetStatusDesc>();
            cdFixedAssetTypeDescs = new HashSet<cdFixedAssetTypeDesc>();
            cdFocalTypeDescs = new HashSet<cdFocalTypeDesc>();
            cdForeignLanguageDescs = new HashSet<cdForeignLanguageDesc>();
            cdForeignTradeStatusDescs = new HashSet<cdForeignTradeStatusDesc>();
            cdFrameShapeTypeDescs = new HashSet<cdFrameShapeTypeDesc>();
            cdFrameTypeDescs = new HashSet<cdFrameTypeDesc>();
            cdFTAttributeDescs = new HashSet<cdFTAttributeDesc>();
            cdFTAttributeTypeDescs = new HashSet<cdFTAttributeTypeDesc>();
            cdGLAccAttributeDescs = new HashSet<cdGLAccAttributeDesc>();
            cdGLAccAttributeTypeDescs = new HashSet<cdGLAccAttributeTypeDesc>();
            cdGLAccClassDescs = new HashSet<cdGLAccClassDesc>();
            cdGLAccDescs = new HashSet<cdGLAccDesc>();
            cdGLAccGroupDescs = new HashSet<cdGLAccGroupDesc>();
            cdGLAccMainDescs = new HashSet<cdGLAccMainDesc>();
            cdGLAccSubDescs = new HashSet<cdGLAccSubDesc>();
            cdGLReflectionDescs = new HashSet<cdGLReflectionDesc>();
            cdGLTypeDescs = new HashSet<cdGLTypeDesc>();
            cdHandicapTypeDescs = new HashSet<cdHandicapTypeDesc>();
            cdImportFileAttributeDescs = new HashSet<cdImportFileAttributeDesc>();
            cdImportFileAttributeTypeDescs = new HashSet<cdImportFileAttributeTypeDesc>();
            cdImportFileDescs = new HashSet<cdImportFileDesc>();
            cdInactivationReasonDescs = new HashSet<cdInactivationReasonDesc>();
            cdIncentiveTypeDescs = new HashSet<cdIncentiveTypeDesc>();
            cdIndustryDescs = new HashSet<cdIndustryDesc>();
            cdInsuranceAgencyDescs = new HashSet<cdInsuranceAgencyDesc>();
            cdInsuranceTypeDescs = new HashSet<cdInsuranceTypeDesc>();
            cdInternationalUnitOfMeasureDescs = new HashSet<cdInternationalUnitOfMeasureDesc>();
            cdITAttributeDescs = new HashSet<cdITAttributeDesc>();
            cdITAttributeTypeDescs = new HashSet<cdITAttributeTypeDesc>();
            cdItemAccountGrDescs = new HashSet<cdItemAccountGrDesc>();
            cdItemAttributeDescs = new HashSet<cdItemAttributeDesc>();
            cdItemAttributeTypeDescs = new HashSet<cdItemAttributeTypeDesc>();
            cdItemDescs = new HashSet<cdItemDesc>();
            cdItemDim1Descs = new HashSet<cdItemDim1Desc>();
            cdItemDiscountGrDescs = new HashSet<cdItemDiscountGrDesc>();
            cdItemLikeTypeDescs = new HashSet<cdItemLikeTypeDesc>();
            cdItemListDescs = new HashSet<cdItemListDesc>();
            cdItemOTAttributeDescs = new HashSet<cdItemOTAttributeDesc>();
            cdItemOTAttributeTypeDescs = new HashSet<cdItemOTAttributeTypeDesc>();
            cdItemPaymentPlanGrDescs = new HashSet<cdItemPaymentPlanGrDesc>();
            cdItemTaxGrDescs = new HashSet<cdItemTaxGrDesc>();
            cdItemTestTypeDescs = new HashSet<cdItemTestTypeDesc>();
            cdItemTextileCareTemplateDescs = new HashSet<cdItemTextileCareTemplateDesc>();
            cdItemVendorGrDescs = new HashSet<cdItemVendorGrDesc>();
            cdJobDepartmentDescs = new HashSet<cdJobDepartmentDesc>();
            cdJobInterviewResultDescs = new HashSet<cdJobInterviewResultDesc>();
            cdJobPositionDescs = new HashSet<cdJobPositionDesc>();
            cdJobTitleDescs = new HashSet<cdJobTitleDesc>();
            cdJobTitleLevelDescs = new HashSet<cdJobTitleLevelDesc>();
            cdJobTrainingAttributeDescs = new HashSet<cdJobTrainingAttributeDesc>();
            cdJobTrainingAttributeTypeDescs = new HashSet<cdJobTrainingAttributeTypeDesc>();
            cdJobTrainingDescs = new HashSet<cdJobTrainingDesc>();
            cdJobTypeDescs = new HashSet<cdJobTypeDesc>();
            cdJournalTypeSubDescs = new HashSet<cdJournalTypeSubDesc>();
            cdKnowLevelDescs = new HashSet<cdKnowLevelDesc>();
            cdLabelTypes = new HashSet<cdLabelType>();
            cdLabelTypeDescs = new HashSet<cdLabelTypeDesc>();
            cdLeaveTypeDescs = new HashSet<cdLeaveTypeDesc>();
            cdLegalResignationDescs = new HashSet<cdLegalResignationDesc>();
            cdLegalResignationLocalDescs = new HashSet<cdLegalResignationLocalDesc>();
            cdLetterOfGuaranteeAttributeDescs = new HashSet<cdLetterOfGuaranteeAttributeDesc>();
            cdLetterOfGuaranteeAttributeTypeDescs = new HashSet<cdLetterOfGuaranteeAttributeTypeDesc>();
            cdLetterTypeDescs = new HashSet<cdLetterTypeDesc>();
            cdLotDescs = new HashSet<cdLotDesc>();
            cdLoyaltyProgramDescs = new HashSet<cdLoyaltyProgramDesc>();
            cdLoyaltyProgramLevelDescs = new HashSet<cdLoyaltyProgramLevelDesc>();
            cdLoyaltyProgramStatusDescs = new HashSet<cdLoyaltyProgramStatusDesc>();
            cdLoyaltyProgramStatusModifyReasonDescs = new HashSet<cdLoyaltyProgramStatusModifyReasonDesc>();
            cdMainJobTitleDescs = new HashSet<cdMainJobTitleDesc>();
            cdMaladyTypeDescs = new HashSet<cdMaladyTypeDesc>();
            cdManufacturerDescs = new HashSet<cdManufacturerDesc>();
            cdMessageReasonDescs = new HashSet<cdMessageReasonDesc>();
            cdMessageTypeDescs = new HashSet<cdMessageTypeDesc>();
            cdMilitaryServiceStatusDescs = new HashSet<cdMilitaryServiceStatusDesc>();
            cdMissingWorkReasonDescs = new HashSet<cdMissingWorkReasonDesc>();
            cdNationalityDescs = new HashSet<cdNationalityDesc>();
            cdOfficeCOGSGrDescs = new HashSet<cdOfficeCOGSGrDesc>();
            cdOfficeDescs = new HashSet<cdOfficeDesc>();
            cdOnlineBankWebServiceDescs = new HashSet<cdOnlineBankWebServiceDesc>();
            cdOnlineDBSWebServiceDescs = new HashSet<cdOnlineDBSWebServiceDesc>();
            cdOpticalGroupRangeDescs = new HashSet<cdOpticalGroupRangeDesc>();
            cdOpticalSutDescs = new HashSet<cdOpticalSutDesc>();
            cdOrderCancelReasonDescs = new HashSet<cdOrderCancelReasonDesc>();
            cdOrderStatusDescs = new HashSet<cdOrderStatusDesc>();
            cdOtherDocumentTypeDescs = new HashSet<cdOtherDocumentTypeDesc>();
            cdPackageBrandDescs = new HashSet<cdPackageBrandDesc>();
            cdPackageVolumeDescs = new HashSet<cdPackageVolumeDesc>();
            cdPantoneDescs = new HashSet<cdPantoneDesc>();
            cdPaymentMethodDescs = new HashSet<cdPaymentMethodDesc>();
            cdPaymentPlanDescs = new HashSet<cdPaymentPlanDesc>();
            cdPaymentProviderDescs = new HashSet<cdPaymentProviderDesc>();
            cdPayrollTypeDescs = new HashSet<cdPayrollTypeDesc>();
            cdPCTDescs = new HashSet<cdPCTDesc>();
            cdPerceptionOfFashionDescs = new HashSet<cdPerceptionOfFashionDesc>();
            cdPlasticBagTypeDescs = new HashSet<cdPlasticBagTypeDesc>();
            cdPointModifyReasonDescs = new HashSet<cdPointModifyReasonDesc>();
            cdPresentCardTypeDescs = new HashSet<cdPresentCardTypeDesc>();
            cdPrevJobTypeDescs = new HashSet<cdPrevJobTypeDesc>();
            cdPriceGroupDescs = new HashSet<cdPriceGroupDesc>();
            cdPriceListTypeDescs = new HashSet<cdPriceListTypeDesc>();
            cdPriorityDescs = new HashSet<cdPriorityDesc>();
            cdPrivateInsuranceDescs = new HashSet<cdPrivateInsuranceDesc>();
            cdProcessFlowDenyReasonDescs = new HashSet<cdProcessFlowDenyReasonDesc>();
            cdProductColorAttributeDescs = new HashSet<cdProductColorAttributeDesc>();
            cdProductColorAttributeTypeDescs = new HashSet<cdProductColorAttributeTypeDesc>();
            cdProductColorSetDescs = new HashSet<cdProductColorSetDesc>();
            cdProductDimSetDescs = new HashSet<cdProductDimSetDesc>();
            cdProductHierarchyLevelDescs = new HashSet<cdProductHierarchyLevelDesc>();
            cdProductPartDescs = new HashSet<cdProductPartDesc>();
            cdProductPointTypeDescs = new HashSet<cdProductPointTypeDesc>();
            cdProductStatusDescs = new HashSet<cdProductStatusDesc>();
            cdPromotionGroupDescs = new HashSet<cdPromotionGroupDesc>();
            cdPurchasePlanDescs = new HashSet<cdPurchasePlanDesc>();
            cdReasonForNotShoppingDescs = new HashSet<cdReasonForNotShoppingDesc>();
            cdRecidivistTypeDescs = new HashSet<cdRecidivistTypeDesc>();
            cdReconciliationDescs = new HashSet<cdReconciliationDesc>();
            cdRequisitionAttributeDescs = new HashSet<cdRequisitionAttributeDesc>();
            cdRequisitionAttributeTypeDescs = new HashSet<cdRequisitionAttributeTypeDesc>();
            cdRequisitionDescs = new HashSet<cdRequisitionDesc>();
            cdRequisitionTypeDescs = new HashSet<cdRequisitionTypeDesc>();
            cdResignationDescs = new HashSet<cdResignationDesc>();
            cdResponsibilityAreaDescs = new HashSet<cdResponsibilityAreaDesc>();
            cdReturnReasonDescs = new HashSet<cdReturnReasonDesc>();
            cdRoleDescs = new HashSet<cdRoleDesc>();
            cdRollNoteTypeDescs = new HashSet<cdRollNoteTypeDesc>();
            cdSalesChannelDescs = new HashSet<cdSalesChannelDesc>();
            cdSalespersonTeamDescs = new HashSet<cdSalespersonTeamDesc>();
            cdSalespersonTypeDescs = new HashSet<cdSalespersonTypeDesc>();
            cdSalesPlanTypeDescs = new HashSet<cdSalesPlanTypeDesc>();
            cdScrapReasonDescs = new HashSet<cdScrapReasonDesc>();
            cdSeasonDescs = new HashSet<cdSeasonDesc>();
            cdSectionTypeDescs = new HashSet<cdSectionTypeDesc>();
            cdSGKBorrowingTypeDescs = new HashSet<cdSGKBorrowingTypeDesc>();
            cdSGKProfessionDescs = new HashSet<cdSGKProfessionDesc>();
            cdShipmentMethodDescs = new HashSet<cdShipmentMethodDesc>();
            cdSMSJobTypeDescs = new HashSet<cdSMSJobTypeDesc>();
            cdSoftwareDescs = new HashSet<cdSoftwareDesc>();
            cdSoftwareTypeDescs = new HashSet<cdSoftwareTypeDesc>();
            cdSpecialDayTypeDescs = new HashSet<cdSpecialDayTypeDesc>();
            cdStateDescs = new HashSet<cdStateDesc>();
            cdStoreCapacityLevelDescs = new HashSet<cdStoreCapacityLevelDesc>();
            cdStoreClimateZoneDescs = new HashSet<cdStoreClimateZoneDesc>();
            cdStoreConceptDescs = new HashSet<cdStoreConceptDesc>();
            cdStoreCRMGroupDescs = new HashSet<cdStoreCRMGroupDesc>();
            cdStoreDistributionGroupDescs = new HashSet<cdStoreDistributionGroupDesc>();
            cdStoreHierarchyLevelDescs = new HashSet<cdStoreHierarchyLevelDesc>();
            cdStorePriceLevelDescs = new HashSet<cdStorePriceLevelDesc>();
            cdStoryBoardDescs = new HashSet<cdStoryBoardDesc>();
            cdSubCurrAccAttributeDescs = new HashSet<cdSubCurrAccAttributeDesc>();
            cdSubCurrAccAttributeTypeDescs = new HashSet<cdSubCurrAccAttributeTypeDesc>();
            cdSubJobDepartmentDescs = new HashSet<cdSubJobDepartmentDesc>();
            cdSubSeasonDescs = new HashSet<cdSubSeasonDesc>();
            cdSupportResolveTypeDescs = new HashSet<cdSupportResolveTypeDesc>();
            cdSupportStatusDescs = new HashSet<cdSupportStatusDesc>();
            cdSurveyDescs = new HashSet<cdSurveyDesc>();
            cdSurveyQuestionDescs = new HashSet<cdSurveyQuestionDesc>();
            cdSurveyQuestionOptionDescs = new HashSet<cdSurveyQuestionOptionDesc>();
            cdSurveySectionDescs = new HashSet<cdSurveySectionDesc>();
            cdTaxDistrictDescs = new HashSet<cdTaxDistrictDesc>();
            cdTaxOfficeDescs = new HashSet<cdTaxOfficeDesc>();
            cdTestDescs = new HashSet<cdTestDesc>();
            cdTestTypeDescs = new HashSet<cdTestTypeDesc>();
            cdTextileCareSymbolDescs = new HashSet<cdTextileCareSymbolDesc>();
            cdTimePeriodDescs = new HashSet<cdTimePeriodDesc>();
            cdTitleDescs = new HashSet<cdTitleDesc>();
            cdTransactionCancelReasonDescs = new HashSet<cdTransactionCancelReasonDesc>();
            cdTransferPlanTemplateDescs = new HashSet<cdTransferPlanTemplateDesc>();
            cdTurnoverTargetTypeDescs = new HashSet<cdTurnoverTargetTypeDesc>();
            cdUnDeliveryReasonDescs = new HashSet<cdUnDeliveryReasonDesc>();
            cdUnitOfMeasureDescs = new HashSet<cdUnitOfMeasureDesc>();
            cdUniversityDescs = new HashSet<cdUniversityDesc>();
            cdUniversityFacultyDepDescs = new HashSet<cdUniversityFacultyDepDesc>();
            cdUniversityFacultyDescs = new HashSet<cdUniversityFacultyDesc>();
            cdUniversityLevelDescs = new HashSet<cdUniversityLevelDesc>();
            cdUniversityTypeDescs = new HashSet<cdUniversityTypeDesc>();
            cdUserWarningDescs = new HashSet<cdUserWarningDesc>();
            cdVatDescs = new HashSet<cdVatDesc>();
            cdVehicleTypeDescs = new HashSet<cdVehicleTypeDesc>();
            cdVendorPaymentPlanGrDescs = new HashSet<cdVendorPaymentPlanGrDesc>();
            cdVisitFrequencyDescs = new HashSet<cdVisitFrequencyDesc>();
            cdWageGarnishmentTypeDescs = new HashSet<cdWageGarnishmentTypeDesc>();
            cdWagePlanTypeDescs = new HashSet<cdWagePlanTypeDesc>();
            cdWarehouseCategoryDescs = new HashSet<cdWarehouseCategoryDesc>();
            cdWarehouseChannelTemplateDescs = new HashSet<cdWarehouseChannelTemplateDesc>();
            cdWarehouseDescs = new HashSet<cdWarehouseDesc>();
            cdWarehouseTypeDescs = new HashSet<cdWarehouseTypeDesc>();
            cdWorkForceDescs = new HashSet<cdWorkForceDesc>();
            cdWorkPlaceDescs = new HashSet<cdWorkPlaceDesc>();
            cdWorkPlaceGroupDescs = new HashSet<cdWorkPlaceGroupDesc>();
            cdWorkPlaceTypeDescs = new HashSet<cdWorkPlaceTypeDesc>();
            cdZoneDescs = new HashSet<cdZoneDesc>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
            dfItemDimensionNamess = new HashSet<dfItemDimensionNames>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            dfPosUIDescs = new HashSet<dfPosUIDesc>();
            dfProductHierarchyLevelNamess = new HashSet<dfProductHierarchyLevelNames>();
            dfStoreHierarchyLevelNamess = new HashSet<dfStoreHierarchyLevelNames>();
            hrJobInterviews = new HashSet<hrJobInterview>();
            prCurrAccNotess = new HashSet<prCurrAccNotes>();
            prDataTransferTemplateQueryFilters = new HashSet<prDataTransferTemplateQueryFilter>();
            prDiscountOfferDescriptions = new HashSet<prDiscountOfferDescription>();
            prDiscountOfferNotess = new HashSet<prDiscountOfferNotes>();
            prDiscountPointTypeNotess = new HashSet<prDiscountPointTypeNotes>();
            prDiscountVoucherTypeNotess = new HashSet<prDiscountVoucherTypeNotes>();
            prEasyStartupCommentss = new HashSet<prEasyStartupComments>();
            prEasyStartupNotess = new HashSet<prEasyStartupNotes>();
            prGLAccNotess = new HashSet<prGLAccNotes>();
            prItemNotess = new HashSet<prItemNotes>();
            prJobTrainingNotess = new HashSet<prJobTrainingNotes>();
            prLoyaltyProgramNotess = new HashSet<prLoyaltyProgramNotes>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
            tpSupportRequestDecisionLetters = new HashSet<tpSupportRequestDecisionLetter>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DataLanguageCode { get; set; }

        [Required]
        public bool IsRequired { get; set; }

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

        public virtual ICollection<bsAccountDetailDesc> bsAccountDetailDescs { get; set; }
        public virtual ICollection<bsAdjustCostMethodDesc> bsAdjustCostMethodDescs { get; set; }
        public virtual ICollection<bsAirportIATADesc> bsAirportIATADescs { get; set; }
        public virtual ICollection<bsAllocationRuleDesc> bsAllocationRuleDescs { get; set; }
        public virtual ICollection<bsAllocationSourceTypeDesc> bsAllocationSourceTypeDescs { get; set; }
        public virtual ICollection<bsApplicationDesc> bsApplicationDescs { get; set; }
        public virtual ICollection<bsBadDebtResultDesc> bsBadDebtResultDescs { get; set; }
        public virtual ICollection<bsBadDebtTransTypeDesc> bsBadDebtTransTypeDescs { get; set; }
        public virtual ICollection<bsBankAdditionalChargeTypeDesc> bsBankAdditionalChargeTypeDescs { get; set; }
        public virtual ICollection<bsBankCardTypeDesc> bsBankCardTypeDescs { get; set; }
        public virtual ICollection<bsBankCreditGuaranteeTypeDesc> bsBankCreditGuaranteeTypeDescs { get; set; }
        public virtual ICollection<bsBankPOSImportTypeDesc> bsBankPOSImportTypeDescs { get; set; }
        public virtual ICollection<bsBankTransTypeDesc> bsBankTransTypeDescs { get; set; }
        public virtual ICollection<bsBasePriceDesc> bsBasePriceDescs { get; set; }
        public virtual ICollection<bsBOMEntityLevelDesc> bsBOMEntityLevelDescs { get; set; }
        public virtual ICollection<bsBrowseMethodTypeDesc> bsBrowseMethodTypeDescs { get; set; }
        public virtual ICollection<bsBudgetDetailDesc> bsBudgetDetailDescs { get; set; }
        public virtual ICollection<bsBulkMailServiceProviderDesc> bsBulkMailServiceProviderDescs { get; set; }
        public virtual ICollection<bsCashTransTypeDesc> bsCashTransTypeDescs { get; set; }
        public virtual ICollection<bsChannelTypeDesc> bsChannelTypeDescs { get; set; }
        public virtual ICollection<bsChequeTransTypeDesc> bsChequeTransTypeDescs { get; set; }
        public virtual ICollection<bsChequeTypeDesc> bsChequeTypeDescs { get; set; }
        public virtual ICollection<bsCommunicationKindDesc> bsCommunicationKindDescs { get; set; }
        public virtual ICollection<bsConfirmationRuleTypeDesc> bsConfirmationRuleTypeDescs { get; set; }
        public virtual ICollection<bsConfirmationStatusDesc> bsConfirmationStatusDescs { get; set; }
        public virtual ICollection<bsConfirmationTypeDesc> bsConfirmationTypeDescs { get; set; }
        public virtual ICollection<bsContractTypeDesc> bsContractTypeDescs { get; set; }
        public virtual ICollection<bsCostingLevelDesc> bsCostingLevelDescs { get; set; }
        public virtual ICollection<bsCostingMethodDesc> bsCostingMethodDescs { get; set; }
        public virtual ICollection<bsCostingVariantLevelDesc> bsCostingVariantLevelDescs { get; set; }
        public virtual ICollection<bsCreditCardPaymentTypeDesc> bsCreditCardPaymentTypeDescs { get; set; }
        public virtual ICollection<bsCreditTypeDesc> bsCreditTypeDescs { get; set; }
        public virtual ICollection<bsCurrAccTypeDesc> bsCurrAccTypeDescs { get; set; }
        public virtual ICollection<bsCustomerTypeDesc> bsCustomerTypeDescs { get; set; }
        public virtual ICollection<bsCustomsProductGroupDesc> bsCustomsProductGroupDescs { get; set; }
        public virtual ICollection<bsDayDesc> bsDayDescs { get; set; }
        public virtual ICollection<bsDebitTypeDesc> bsDebitTypeDescs { get; set; }
        public virtual ICollection<bsDebtStatusTypeDesc> bsDebtStatusTypeDescs { get; set; }
        public virtual ICollection<bsDeclarationCapacityDesc> bsDeclarationCapacityDescs { get; set; }
        public virtual ICollection<bsDeclarationTypeDesc> bsDeclarationTypeDescs { get; set; }
        public virtual ICollection<bsDepreciationMethodDesc> bsDepreciationMethodDescs { get; set; }
        public virtual ICollection<bsDeviceDesc> bsDeviceDescs { get; set; }
        public virtual ICollection<bsDeviceTypeDesc> bsDeviceTypeDescs { get; set; }
        public virtual ICollection<bsDiscountLevelOfUseDesc> bsDiscountLevelOfUseDescs { get; set; }
        public virtual ICollection<bsDiscountOfferApplyDesc> bsDiscountOfferApplyDescs { get; set; }
        public virtual ICollection<bsDiscountOfferMethodDesc> bsDiscountOfferMethodDescs { get; set; }
        public virtual ICollection<bsDiscountOfferStageDesc> bsDiscountOfferStageDescs { get; set; }
        public virtual ICollection<bsDiscountOfferTypeDesc> bsDiscountOfferTypeDescs { get; set; }
        public virtual ICollection<bsDiscountVoucherBaseDesc> bsDiscountVoucherBaseDescs { get; set; }
        public virtual ICollection<bsDispOrderTypeDesc> bsDispOrderTypeDescs { get; set; }
        public virtual ICollection<bsDocumentTypeDesc> bsDocumentTypeDescs { get; set; }
        public virtual ICollection<bsEasyStartupStepsDesc> bsEasyStartupStepsDescs { get; set; }
        public virtual ICollection<bsEditMaskDesc> bsEditMaskDescs { get; set; }
        public virtual ICollection<bsEInvoiceStatusDesc> bsEInvoiceStatusDescs { get; set; }
        public virtual ICollection<bsEmailTypeDesc> bsEmailTypeDescs { get; set; }
        public virtual ICollection<bsEmployeePayTypeDesc> bsEmployeePayTypeDescs { get; set; }
        public virtual ICollection<bsEmployeeSpecialTypeDesc> bsEmployeeSpecialTypeDescs { get; set; }
        public virtual ICollection<bsEShipmentStatusDesc> bsEShipmentStatusDescs { get; set; }
        public virtual ICollection<bsExpenseSlipTypeDesc> bsExpenseSlipTypeDescs { get; set; }
        public virtual ICollection<bsEyeGlassSutTypeDesc> bsEyeGlassSutTypeDescs { get; set; }
        public virtual ICollection<bsFastDeliveryCompanyDesc> bsFastDeliveryCompanyDescs { get; set; }
        public virtual ICollection<bsFileFormatTypeDesc> bsFileFormatTypeDescs { get; set; }
        public virtual ICollection<bsFolderDesc> bsFolderDescs { get; set; }
        public virtual ICollection<bsFormatTypeDesc> bsFormatTypeDescs { get; set; }
        public virtual ICollection<bsGenderDesc> bsGenderDescs { get; set; }
        public virtual ICollection<bsGiftCardPaymentTypeDesc> bsGiftCardPaymentTypeDescs { get; set; }
        public virtual ICollection<bsIncompleteDownPaymentDistributionTypeDesc> bsIncompleteDownPaymentDistributionTypeDescs { get; set; }
        public virtual ICollection<bsIncotermDesc> bsIncotermDescs { get; set; }
        public virtual ICollection<bsInnerOrderTypeDesc> bsInnerOrderTypeDescs { get; set; }
        public virtual ICollection<bsInnerProcessDesc> bsInnerProcessDescs { get; set; }
        public virtual ICollection<bsInvoiceReturnTypeDesc> bsInvoiceReturnTypeDescs { get; set; }
        public virtual ICollection<bsInvoiceTypeDesc> bsInvoiceTypeDescs { get; set; }
        public virtual ICollection<bsItemDimTypeDesc> bsItemDimTypeDescs { get; set; }
        public virtual ICollection<bsItemProcessPermitTypeDesc> bsItemProcessPermitTypeDescs { get; set; }
        public virtual ICollection<bsItemTypeDesc> bsItemTypeDescs { get; set; }
        public virtual ICollection<bsJournalTypeDesc> bsJournalTypeDescs { get; set; }
        public virtual ICollection<bsLensTypeDesc> bsLensTypeDescs { get; set; }
        public virtual ICollection<bsLetterOfGuaranteeTypeDesc> bsLetterOfGuaranteeTypeDescs { get; set; }
        public virtual ICollection<bsLinkedProductTypeDesc> bsLinkedProductTypeDescs { get; set; }
        public virtual ICollection<bsLoyaltyProgramProcessDesc> bsLoyaltyProgramProcessDescs { get; set; }
        public virtual ICollection<bsMessageImportanceDesc> bsMessageImportanceDescs { get; set; }
        public virtual ICollection<bsNebimV3ServicesDesc> bsNebimV3ServicesDescs { get; set; }
        public virtual ICollection<bsNebimV3WindowsServicesDesc> bsNebimV3WindowsServicesDescs { get; set; }
        public virtual ICollection<bsOrderDeliveryRecordTypeDesc> bsOrderDeliveryRecordTypeDescs { get; set; }
        public virtual ICollection<bsOrderTypeDesc> bsOrderTypeDescs { get; set; }
        public virtual ICollection<bsOtherPaymentTypeDesc> bsOtherPaymentTypeDescs { get; set; }
        public virtual ICollection<bsPackagingTypeDesc> bsPackagingTypeDescs { get; set; }
        public virtual ICollection<bsPaymentMeansDesc> bsPaymentMeansDescs { get; set; }
        public virtual ICollection<bsPaymentTypeDesc> bsPaymentTypeDescs { get; set; }
        public virtual ICollection<bsPayTypeDesc> bsPayTypeDescs { get; set; }
        public virtual ICollection<bsPickingTypeDesc> bsPickingTypeDescs { get; set; }
        public virtual ICollection<bsPointBaseDesc> bsPointBaseDescs { get; set; }
        public virtual ICollection<bsPointRecordTypeDesc> bsPointRecordTypeDescs { get; set; }
        public virtual ICollection<bsPolicyCustomerEditDesc> bsPolicyCustomerEditDescs { get; set; }
        public virtual ICollection<bsPolicyCustomerPaymentDesc> bsPolicyCustomerPaymentDescs { get; set; }
        public virtual ICollection<bsPolicyCustomerSharingDesc> bsPolicyCustomerSharingDescs { get; set; }
        public virtual ICollection<bsPolicyVendorSharingDesc> bsPolicyVendorSharingDescs { get; set; }
        public virtual ICollection<bsPOSModeDesc> bsPOSModeDescs { get; set; }
        public virtual ICollection<bsPostAccTypeDesc> bsPostAccTypeDescs { get; set; }
        public virtual ICollection<bsPresentCardActivationProcessDesc> bsPresentCardActivationProcessDescs { get; set; }
        public virtual ICollection<bsPresentCardActivationStatusDesc> bsPresentCardActivationStatusDescs { get; set; }
        public virtual ICollection<bsPresentCardActivationTypeDesc> bsPresentCardActivationTypeDescs { get; set; }
        public virtual ICollection<bsProcessDesc> bsProcessDescs { get; set; }
        public virtual ICollection<bsProcessFlowDesc> bsProcessFlowDescs { get; set; }
        public virtual ICollection<bsProductTypeDesc> bsProductTypeDescs { get; set; }
        public virtual ICollection<bsQuestionInputTypeDesc> bsQuestionInputTypeDescs { get; set; }
        public virtual ICollection<bsReconciliationTypeDesc> bsReconciliationTypeDescs { get; set; }
        public virtual ICollection<bsReserveTypeDesc> bsReserveTypeDescs { get; set; }
        public virtual ICollection<bsSGKInsuaranceTypeDesc> bsSGKInsuaranceTypeDescs { get; set; }
        public virtual ICollection<bsSGKMissionDesc> bsSGKMissionDescs { get; set; }
        public virtual ICollection<bsSGKWorkPlaceSectorDesc> bsSGKWorkPlaceSectorDescs { get; set; }
        public virtual ICollection<bsShipmentTypeDesc> bsShipmentTypeDescs { get; set; }
        public virtual ICollection<bsSMSStatusDesc> bsSMSStatusDescs { get; set; }
        public virtual ICollection<bsStandardBarcodeTypeDesc> bsStandardBarcodeTypeDescs { get; set; }
        public virtual ICollection<bsSupportTypeDesc> bsSupportTypeDescs { get; set; }
        public virtual ICollection<bsTaxExemptionDesc> bsTaxExemptionDescs { get; set; }
        public virtual ICollection<bsTaxPaymentAccTypeDesc> bsTaxPaymentAccTypeDescs { get; set; }
        public virtual ICollection<bsTaxPaymentTypeDesc> bsTaxPaymentTypeDescs { get; set; }
        public virtual ICollection<bsTaxTypeDesc> bsTaxTypeDescs { get; set; }
        public virtual ICollection<bsTextileCareSymbolGrDesc> bsTextileCareSymbolGrDescs { get; set; }
        public virtual ICollection<bsTransferPlanRuleDesc> bsTransferPlanRuleDescs { get; set; }
        public virtual ICollection<bsTransportModeDesc> bsTransportModeDescs { get; set; }
        public virtual ICollection<bsTransTypeDesc> bsTransTypeDescs { get; set; }
        public virtual ICollection<bsVendorTypeDesc> bsVendorTypeDescs { get; set; }
        public virtual ICollection<bsWarehouseOwnerDesc> bsWarehouseOwnerDescs { get; set; }
        public virtual ICollection<bsWorkDangerLevelDesc> bsWorkDangerLevelDescs { get; set; }
        public virtual ICollection<bsWorkplaceKindDesc> bsWorkplaceKindDescs { get; set; }
        public virtual ICollection<bsWorkplacePropertyStatusDesc> bsWorkplacePropertyStatusDescs { get; set; }
        public virtual ICollection<cdAccountantDesc> cdAccountantDescs { get; set; }
        public virtual ICollection<cdAddressShareCompanyWebServiceDesc> cdAddressShareCompanyWebServiceDescs { get; set; }
        public virtual ICollection<cdAddressTypeDesc> cdAddressTypeDescs { get; set; }
        public virtual ICollection<cdAllocationTemplateDesc> cdAllocationTemplateDescs { get; set; }
        public virtual ICollection<cdAmountRuleDesc> cdAmountRuleDescs { get; set; }
        public virtual ICollection<cdATAttributeDesc> cdATAttributeDescs { get; set; }
        public virtual ICollection<cdATAttributeTypeDesc> cdATAttributeTypeDescs { get; set; }
        public virtual ICollection<cdBadDebtLetterTypeDesc> cdBadDebtLetterTypeDescs { get; set; }
        public virtual ICollection<cdBadDebtReasonDesc> cdBadDebtReasonDescs { get; set; }
        public virtual ICollection<cdBankAccTypeDesc> cdBankAccTypeDescs { get; set; }
        public virtual ICollection<cdBankCreditTypeDesc> cdBankCreditTypeDescs { get; set; }
        public virtual ICollection<cdBankDesc> cdBankDescs { get; set; }
        public virtual ICollection<cdBankOpTypeDesc> cdBankOpTypeDescs { get; set; }
        public virtual ICollection<cdBarcodeTypeDesc> cdBarcodeTypeDescs { get; set; }
        public virtual ICollection<cdBaseMaterialDesc> cdBaseMaterialDescs { get; set; }
        public virtual ICollection<cdBatchDesc> cdBatchDescs { get; set; }
        public virtual ICollection<cdBatchGroupDesc> cdBatchGroupDescs { get; set; }
        public virtual ICollection<cdBloodTypeDesc> cdBloodTypeDescs { get; set; }
        public virtual ICollection<cdBOMDesc> cdBOMDescs { get; set; }
        public virtual ICollection<cdBOMEntityDesc> cdBOMEntityDescs { get; set; }
        public virtual ICollection<cdBOMTemplateAttributeDesc> cdBOMTemplateAttributeDescs { get; set; }
        public virtual ICollection<cdBOMTemplateAttributeTypeDesc> cdBOMTemplateAttributeTypeDescs { get; set; }
        public virtual ICollection<cdBOMTemplateDesc> cdBOMTemplateDescs { get; set; }
        public virtual ICollection<cdBrandDesc> cdBrandDescs { get; set; }
        public virtual ICollection<cdBudgetTypeDesc> cdBudgetTypeDescs { get; set; }
        public virtual ICollection<cdBusinessGroupDesc> cdBusinessGroupDescs { get; set; }
        public virtual ICollection<cdCareWarningDesc> cdCareWarningDescs { get; set; }
        public virtual ICollection<cdCareWarningTemplateDesc> cdCareWarningTemplateDescs { get; set; }
        public virtual ICollection<cdChannelTemplateDesc> cdChannelTemplateDescs { get; set; }
        public virtual ICollection<cdCheckOutReasonDesc> cdCheckOutReasonDescs { get; set; }
        public virtual ICollection<cdChequeAttributeDesc> cdChequeAttributeDescs { get; set; }
        public virtual ICollection<cdChequeAttributeTypeDesc> cdChequeAttributeTypeDescs { get; set; }
        public virtual ICollection<cdChequeDenyReasonDesc> cdChequeDenyReasonDescs { get; set; }
        public virtual ICollection<cdChequeDesc> cdChequeDescs { get; set; }
        public virtual ICollection<cdCityDesc> cdCityDescs { get; set; }
        public virtual ICollection<cdCoatingTypeDesc> cdCoatingTypeDescs { get; set; }
        public virtual ICollection<cdCollectionDesc> cdCollectionDescs { get; set; }
        public virtual ICollection<cdColorCatalogDesc> cdColorCatalogDescs { get; set; }
        public virtual ICollection<cdColorDesc> cdColorDescs { get; set; }
        public virtual ICollection<cdColorGroupDesc> cdColorGroupDescs { get; set; }
        public virtual ICollection<cdColorThemeAttributeDesc> cdColorThemeAttributeDescs { get; set; }
        public virtual ICollection<cdColorThemeAttributeTypeDesc> cdColorThemeAttributeTypeDescs { get; set; }
        public virtual ICollection<cdColorThemeDesc> cdColorThemeDescs { get; set; }
        public virtual ICollection<cdColorTypeDesc> cdColorTypeDescs { get; set; }
        public virtual ICollection<cdCommercialRoleDesc> cdCommercialRoleDescs { get; set; }
        public virtual ICollection<cdCommunicationTypeDesc> cdCommunicationTypeDescs { get; set; }
        public virtual ICollection<cdCompanyBrandDesc> cdCompanyBrandDescs { get; set; }
        public virtual ICollection<cdCompanyCreditCardDesc> cdCompanyCreditCardDescs { get; set; }
        public virtual ICollection<cdConditionTypeDesc> cdConditionTypeDescs { get; set; }
        public virtual ICollection<cdConfirmationFormStatusDesc> cdConfirmationFormStatusDescs { get; set; }
        public virtual ICollection<cdConfirmationFormTypeDesc> cdConfirmationFormTypeDescs { get; set; }
        public virtual ICollection<cdConfirmationReasonDesc> cdConfirmationReasonDescs { get; set; }
        public virtual ICollection<cdContactTypeDesc> cdContactTypeDescs { get; set; }
        public virtual ICollection<cdContainerTypeDesc> cdContainerTypeDescs { get; set; }
        public virtual ICollection<cdContractContentDesc> cdContractContentDescs { get; set; }
        public virtual ICollection<cdContractStatusDesc> cdContractStatusDescs { get; set; }
        public virtual ICollection<cdCostCenterAttributeDesc> cdCostCenterAttributeDescs { get; set; }
        public virtual ICollection<cdCostCenterAttributeTypeDesc> cdCostCenterAttributeTypeDescs { get; set; }
        public virtual ICollection<cdCostCenterDesc> cdCostCenterDescs { get; set; }
        public virtual ICollection<cdCostOfGoodsSoldPeriodDesc> cdCostOfGoodsSoldPeriodDescs { get; set; }
        public virtual ICollection<cdCountryDesc> cdCountryDescs { get; set; }
        public virtual ICollection<cdCreditCardTypeDesc> cdCreditCardTypeDescs { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdCurrAccAttributeDesc> cdCurrAccAttributeDescs { get; set; }
        public virtual ICollection<cdCurrAccAttributeTypeDesc> cdCurrAccAttributeTypeDescs { get; set; }
        public virtual ICollection<cdCurrAccDesc> cdCurrAccDescs { get; set; }
        public virtual ICollection<cdCurrAccListDesc> cdCurrAccListDescs { get; set; }
        public virtual ICollection<cdCurrAccLotGrDesc> cdCurrAccLotGrDescs { get; set; }
        public virtual ICollection<cdCurrencyDesc> cdCurrencyDescs { get; set; }
        public virtual ICollection<cdCustomerAlertColorDesc> cdCustomerAlertColorDescs { get; set; }
        public virtual ICollection<cdCustomerCompanyBrandAttributeDesc> cdCustomerCompanyBrandAttributeDescs { get; set; }
        public virtual ICollection<cdCustomerCompanyBrandAttributeTypeDesc> cdCustomerCompanyBrandAttributeTypeDescs { get; set; }
        public virtual ICollection<cdCustomerConversationResultDesc> cdCustomerConversationResultDescs { get; set; }
        public virtual ICollection<cdCustomerConversationSubjectDesc> cdCustomerConversationSubjectDescs { get; set; }
        public virtual ICollection<cdCustomerConversationSubjectDetailDesc> cdCustomerConversationSubjectDetailDescs { get; set; }
        public virtual ICollection<cdCustomerConversationSubtitleDesc> cdCustomerConversationSubtitleDescs { get; set; }
        public virtual ICollection<cdCustomerCRMGroupDesc> cdCustomerCRMGroupDescs { get; set; }
        public virtual ICollection<cdCustomerDiscountGrDesc> cdCustomerDiscountGrDescs { get; set; }
        public virtual ICollection<cdCustomerMarkupGrDesc> cdCustomerMarkupGrDescs { get; set; }
        public virtual ICollection<cdCustomerPaymentPlanGrDesc> cdCustomerPaymentPlanGrDescs { get; set; }
        public virtual ICollection<cdCustomerShoppingHabitDesc> cdCustomerShoppingHabitDescs { get; set; }
        public virtual ICollection<cdCustomerShoppingLevelDesc> cdCustomerShoppingLevelDescs { get; set; }
        public virtual ICollection<cdCustomProcessGroupDesc> cdCustomProcessGroupDescs { get; set; }
        public virtual ICollection<cdCustomsCompanyDesc> cdCustomsCompanyDescs { get; set; }
        public virtual ICollection<cdCustomsOfficesDesc> cdCustomsOfficesDescs { get; set; }
        public virtual ICollection<cdCustomsTariffNumberDesc> cdCustomsTariffNumberDescs { get; set; }
        public virtual ICollection<cdDataLanguageDesc> cdDataLanguageDescs { get; set; }
        public virtual ICollection<cdDataTransferCompanyDesc> cdDataTransferCompanyDescs { get; set; }
        public virtual ICollection<cdDebitReasonDesc> cdDebitReasonDescs { get; set; }
        public virtual ICollection<cdDeclarationDesc> cdDeclarationDescs { get; set; }
        public virtual ICollection<cdDeductionDesc> cdDeductionDescs { get; set; }
        public virtual ICollection<cdDeliveryCompanyDesc> cdDeliveryCompanyDescs { get; set; }
        public virtual ICollection<cdDiagnosticDesc> cdDiagnosticDescs { get; set; }
        public virtual ICollection<cdDigitalMarketingServiceDesc> cdDigitalMarketingServiceDescs { get; set; }
        public virtual ICollection<cdDiscountOfferAttributeDesc> cdDiscountOfferAttributeDescs { get; set; }
        public virtual ICollection<cdDiscountOfferAttributeTypeDesc> cdDiscountOfferAttributeTypeDescs { get; set; }
        public virtual ICollection<cdDiscountOfferDesc> cdDiscountOfferDescs { get; set; }
        public virtual ICollection<cdDiscountPointTypeDesc> cdDiscountPointTypeDescs { get; set; }
        public virtual ICollection<cdDiscountReasonDesc> cdDiscountReasonDescs { get; set; }
        public virtual ICollection<cdDiscountSubReasonDesc> cdDiscountSubReasonDescs { get; set; }
        public virtual ICollection<cdDiscountTypeDesc> cdDiscountTypeDescs { get; set; }
        public virtual ICollection<cdDiscountVoucherTypeDesc> cdDiscountVoucherTypeDescs { get; set; }
        public virtual ICollection<cdDistrictDesc> cdDistrictDescs { get; set; }
        public virtual ICollection<cdDOVDesc> cdDOVDescs { get; set; }
        public virtual ICollection<cdDueDateFormulaDesc> cdDueDateFormulaDescs { get; set; }
        public virtual ICollection<cdEArchiveWebServiceDesc> cdEArchiveWebServiceDescs { get; set; }
        public virtual ICollection<cdEarningsDesc> cdEarningsDescs { get; set; }
        public virtual ICollection<cdEducationStatusDesc> cdEducationStatusDescs { get; set; }
        public virtual ICollection<cdEInvoiceWebServiceDesc> cdEInvoiceWebServiceDescs { get; set; }
        public virtual ICollection<cdEMailServiceDesc> cdEMailServiceDescs { get; set; }
        public virtual ICollection<cdEmployeeDocumentTypeDesc> cdEmployeeDocumentTypeDescs { get; set; }
        public virtual ICollection<cdEmployeeRecordTypeDesc> cdEmployeeRecordTypeDescs { get; set; }
        public virtual ICollection<cdEmployeeSocialInsuranceStatusDesc> cdEmployeeSocialInsuranceStatusDescs { get; set; }
        public virtual ICollection<cdEmployeeTaxStatusDesc> cdEmployeeTaxStatusDescs { get; set; }
        public virtual ICollection<cdEmploymentLawDesc> cdEmploymentLawDescs { get; set; }
        public virtual ICollection<cdEShipmentWebServiceDesc> cdEShipmentWebServiceDescs { get; set; }
        public virtual ICollection<cdExchangeTypeDesc> cdExchangeTypeDescs { get; set; }
        public virtual ICollection<cdExecutionOfficeDesc> cdExecutionOfficeDescs { get; set; }
        public virtual ICollection<cdExpensePeriodDesc> cdExpensePeriodDescs { get; set; }
        public virtual ICollection<cdExpenseTypeDesc> cdExpenseTypeDescs { get; set; }
        public virtual ICollection<cdExportFileAttributeDesc> cdExportFileAttributeDescs { get; set; }
        public virtual ICollection<cdExportFileAttributeTypeDesc> cdExportFileAttributeTypeDescs { get; set; }
        public virtual ICollection<cdExportFileDesc> cdExportFileDescs { get; set; }
        public virtual ICollection<cdExportTypeDesc> cdExportTypeDescs { get; set; }
        public virtual ICollection<cdFabricDesc> cdFabricDescs { get; set; }
        public virtual ICollection<cdFinanceCompanyWebServiceDesc> cdFinanceCompanyWebServiceDescs { get; set; }
        public virtual ICollection<cdFiscalPeriodDesc> cdFiscalPeriodDescs { get; set; }
        public virtual ICollection<cdFixedAssetStatusDesc> cdFixedAssetStatusDescs { get; set; }
        public virtual ICollection<cdFixedAssetTypeDesc> cdFixedAssetTypeDescs { get; set; }
        public virtual ICollection<cdFocalTypeDesc> cdFocalTypeDescs { get; set; }
        public virtual ICollection<cdForeignLanguageDesc> cdForeignLanguageDescs { get; set; }
        public virtual ICollection<cdForeignTradeStatusDesc> cdForeignTradeStatusDescs { get; set; }
        public virtual ICollection<cdFrameShapeTypeDesc> cdFrameShapeTypeDescs { get; set; }
        public virtual ICollection<cdFrameTypeDesc> cdFrameTypeDescs { get; set; }
        public virtual ICollection<cdFTAttributeDesc> cdFTAttributeDescs { get; set; }
        public virtual ICollection<cdFTAttributeTypeDesc> cdFTAttributeTypeDescs { get; set; }
        public virtual ICollection<cdGLAccAttributeDesc> cdGLAccAttributeDescs { get; set; }
        public virtual ICollection<cdGLAccAttributeTypeDesc> cdGLAccAttributeTypeDescs { get; set; }
        public virtual ICollection<cdGLAccClassDesc> cdGLAccClassDescs { get; set; }
        public virtual ICollection<cdGLAccDesc> cdGLAccDescs { get; set; }
        public virtual ICollection<cdGLAccGroupDesc> cdGLAccGroupDescs { get; set; }
        public virtual ICollection<cdGLAccMainDesc> cdGLAccMainDescs { get; set; }
        public virtual ICollection<cdGLAccSubDesc> cdGLAccSubDescs { get; set; }
        public virtual ICollection<cdGLReflectionDesc> cdGLReflectionDescs { get; set; }
        public virtual ICollection<cdGLTypeDesc> cdGLTypeDescs { get; set; }
        public virtual ICollection<cdHandicapTypeDesc> cdHandicapTypeDescs { get; set; }
        public virtual ICollection<cdImportFileAttributeDesc> cdImportFileAttributeDescs { get; set; }
        public virtual ICollection<cdImportFileAttributeTypeDesc> cdImportFileAttributeTypeDescs { get; set; }
        public virtual ICollection<cdImportFileDesc> cdImportFileDescs { get; set; }
        public virtual ICollection<cdInactivationReasonDesc> cdInactivationReasonDescs { get; set; }
        public virtual ICollection<cdIncentiveTypeDesc> cdIncentiveTypeDescs { get; set; }
        public virtual ICollection<cdIndustryDesc> cdIndustryDescs { get; set; }
        public virtual ICollection<cdInsuranceAgencyDesc> cdInsuranceAgencyDescs { get; set; }
        public virtual ICollection<cdInsuranceTypeDesc> cdInsuranceTypeDescs { get; set; }
        public virtual ICollection<cdInternationalUnitOfMeasureDesc> cdInternationalUnitOfMeasureDescs { get; set; }
        public virtual ICollection<cdITAttributeDesc> cdITAttributeDescs { get; set; }
        public virtual ICollection<cdITAttributeTypeDesc> cdITAttributeTypeDescs { get; set; }
        public virtual ICollection<cdItemAccountGrDesc> cdItemAccountGrDescs { get; set; }
        public virtual ICollection<cdItemAttributeDesc> cdItemAttributeDescs { get; set; }
        public virtual ICollection<cdItemAttributeTypeDesc> cdItemAttributeTypeDescs { get; set; }
        public virtual ICollection<cdItemDesc> cdItemDescs { get; set; }
        public virtual ICollection<cdItemDim1Desc> cdItemDim1Descs { get; set; }
        public virtual ICollection<cdItemDiscountGrDesc> cdItemDiscountGrDescs { get; set; }
        public virtual ICollection<cdItemLikeTypeDesc> cdItemLikeTypeDescs { get; set; }
        public virtual ICollection<cdItemListDesc> cdItemListDescs { get; set; }
        public virtual ICollection<cdItemOTAttributeDesc> cdItemOTAttributeDescs { get; set; }
        public virtual ICollection<cdItemOTAttributeTypeDesc> cdItemOTAttributeTypeDescs { get; set; }
        public virtual ICollection<cdItemPaymentPlanGrDesc> cdItemPaymentPlanGrDescs { get; set; }
        public virtual ICollection<cdItemTaxGrDesc> cdItemTaxGrDescs { get; set; }
        public virtual ICollection<cdItemTestTypeDesc> cdItemTestTypeDescs { get; set; }
        public virtual ICollection<cdItemTextileCareTemplateDesc> cdItemTextileCareTemplateDescs { get; set; }
        public virtual ICollection<cdItemVendorGrDesc> cdItemVendorGrDescs { get; set; }
        public virtual ICollection<cdJobDepartmentDesc> cdJobDepartmentDescs { get; set; }
        public virtual ICollection<cdJobInterviewResultDesc> cdJobInterviewResultDescs { get; set; }
        public virtual ICollection<cdJobPositionDesc> cdJobPositionDescs { get; set; }
        public virtual ICollection<cdJobTitleDesc> cdJobTitleDescs { get; set; }
        public virtual ICollection<cdJobTitleLevelDesc> cdJobTitleLevelDescs { get; set; }
        public virtual ICollection<cdJobTrainingAttributeDesc> cdJobTrainingAttributeDescs { get; set; }
        public virtual ICollection<cdJobTrainingAttributeTypeDesc> cdJobTrainingAttributeTypeDescs { get; set; }
        public virtual ICollection<cdJobTrainingDesc> cdJobTrainingDescs { get; set; }
        public virtual ICollection<cdJobTypeDesc> cdJobTypeDescs { get; set; }
        public virtual ICollection<cdJournalTypeSubDesc> cdJournalTypeSubDescs { get; set; }
        public virtual ICollection<cdKnowLevelDesc> cdKnowLevelDescs { get; set; }
        public virtual ICollection<cdLabelType> cdLabelTypes { get; set; }
        public virtual ICollection<cdLabelTypeDesc> cdLabelTypeDescs { get; set; }
        public virtual ICollection<cdLeaveTypeDesc> cdLeaveTypeDescs { get; set; }
        public virtual ICollection<cdLegalResignationDesc> cdLegalResignationDescs { get; set; }
        public virtual ICollection<cdLegalResignationLocalDesc> cdLegalResignationLocalDescs { get; set; }
        public virtual ICollection<cdLetterOfGuaranteeAttributeDesc> cdLetterOfGuaranteeAttributeDescs { get; set; }
        public virtual ICollection<cdLetterOfGuaranteeAttributeTypeDesc> cdLetterOfGuaranteeAttributeTypeDescs { get; set; }
        public virtual ICollection<cdLetterTypeDesc> cdLetterTypeDescs { get; set; }
        public virtual ICollection<cdLotDesc> cdLotDescs { get; set; }
        public virtual ICollection<cdLoyaltyProgramDesc> cdLoyaltyProgramDescs { get; set; }
        public virtual ICollection<cdLoyaltyProgramLevelDesc> cdLoyaltyProgramLevelDescs { get; set; }
        public virtual ICollection<cdLoyaltyProgramStatusDesc> cdLoyaltyProgramStatusDescs { get; set; }
        public virtual ICollection<cdLoyaltyProgramStatusModifyReasonDesc> cdLoyaltyProgramStatusModifyReasonDescs { get; set; }
        public virtual ICollection<cdMainJobTitleDesc> cdMainJobTitleDescs { get; set; }
        public virtual ICollection<cdMaladyTypeDesc> cdMaladyTypeDescs { get; set; }
        public virtual ICollection<cdManufacturerDesc> cdManufacturerDescs { get; set; }
        public virtual ICollection<cdMessageReasonDesc> cdMessageReasonDescs { get; set; }
        public virtual ICollection<cdMessageTypeDesc> cdMessageTypeDescs { get; set; }
        public virtual ICollection<cdMilitaryServiceStatusDesc> cdMilitaryServiceStatusDescs { get; set; }
        public virtual ICollection<cdMissingWorkReasonDesc> cdMissingWorkReasonDescs { get; set; }
        public virtual ICollection<cdNationalityDesc> cdNationalityDescs { get; set; }
        public virtual ICollection<cdOfficeCOGSGrDesc> cdOfficeCOGSGrDescs { get; set; }
        public virtual ICollection<cdOfficeDesc> cdOfficeDescs { get; set; }
        public virtual ICollection<cdOnlineBankWebServiceDesc> cdOnlineBankWebServiceDescs { get; set; }
        public virtual ICollection<cdOnlineDBSWebServiceDesc> cdOnlineDBSWebServiceDescs { get; set; }
        public virtual ICollection<cdOpticalGroupRangeDesc> cdOpticalGroupRangeDescs { get; set; }
        public virtual ICollection<cdOpticalSutDesc> cdOpticalSutDescs { get; set; }
        public virtual ICollection<cdOrderCancelReasonDesc> cdOrderCancelReasonDescs { get; set; }
        public virtual ICollection<cdOrderStatusDesc> cdOrderStatusDescs { get; set; }
        public virtual ICollection<cdOtherDocumentTypeDesc> cdOtherDocumentTypeDescs { get; set; }
        public virtual ICollection<cdPackageBrandDesc> cdPackageBrandDescs { get; set; }
        public virtual ICollection<cdPackageVolumeDesc> cdPackageVolumeDescs { get; set; }
        public virtual ICollection<cdPantoneDesc> cdPantoneDescs { get; set; }
        public virtual ICollection<cdPaymentMethodDesc> cdPaymentMethodDescs { get; set; }
        public virtual ICollection<cdPaymentPlanDesc> cdPaymentPlanDescs { get; set; }
        public virtual ICollection<cdPaymentProviderDesc> cdPaymentProviderDescs { get; set; }
        public virtual ICollection<cdPayrollTypeDesc> cdPayrollTypeDescs { get; set; }
        public virtual ICollection<cdPCTDesc> cdPCTDescs { get; set; }
        public virtual ICollection<cdPerceptionOfFashionDesc> cdPerceptionOfFashionDescs { get; set; }
        public virtual ICollection<cdPlasticBagTypeDesc> cdPlasticBagTypeDescs { get; set; }
        public virtual ICollection<cdPointModifyReasonDesc> cdPointModifyReasonDescs { get; set; }
        public virtual ICollection<cdPresentCardTypeDesc> cdPresentCardTypeDescs { get; set; }
        public virtual ICollection<cdPrevJobTypeDesc> cdPrevJobTypeDescs { get; set; }
        public virtual ICollection<cdPriceGroupDesc> cdPriceGroupDescs { get; set; }
        public virtual ICollection<cdPriceListTypeDesc> cdPriceListTypeDescs { get; set; }
        public virtual ICollection<cdPriorityDesc> cdPriorityDescs { get; set; }
        public virtual ICollection<cdPrivateInsuranceDesc> cdPrivateInsuranceDescs { get; set; }
        public virtual ICollection<cdProcessFlowDenyReasonDesc> cdProcessFlowDenyReasonDescs { get; set; }
        public virtual ICollection<cdProductColorAttributeDesc> cdProductColorAttributeDescs { get; set; }
        public virtual ICollection<cdProductColorAttributeTypeDesc> cdProductColorAttributeTypeDescs { get; set; }
        public virtual ICollection<cdProductColorSetDesc> cdProductColorSetDescs { get; set; }
        public virtual ICollection<cdProductDimSetDesc> cdProductDimSetDescs { get; set; }
        public virtual ICollection<cdProductHierarchyLevelDesc> cdProductHierarchyLevelDescs { get; set; }
        public virtual ICollection<cdProductPartDesc> cdProductPartDescs { get; set; }
        public virtual ICollection<cdProductPointTypeDesc> cdProductPointTypeDescs { get; set; }
        public virtual ICollection<cdProductStatusDesc> cdProductStatusDescs { get; set; }
        public virtual ICollection<cdPromotionGroupDesc> cdPromotionGroupDescs { get; set; }
        public virtual ICollection<cdPurchasePlanDesc> cdPurchasePlanDescs { get; set; }
        public virtual ICollection<cdReasonForNotShoppingDesc> cdReasonForNotShoppingDescs { get; set; }
        public virtual ICollection<cdRecidivistTypeDesc> cdRecidivistTypeDescs { get; set; }
        public virtual ICollection<cdReconciliationDesc> cdReconciliationDescs { get; set; }
        public virtual ICollection<cdRequisitionAttributeDesc> cdRequisitionAttributeDescs { get; set; }
        public virtual ICollection<cdRequisitionAttributeTypeDesc> cdRequisitionAttributeTypeDescs { get; set; }
        public virtual ICollection<cdRequisitionDesc> cdRequisitionDescs { get; set; }
        public virtual ICollection<cdRequisitionTypeDesc> cdRequisitionTypeDescs { get; set; }
        public virtual ICollection<cdResignationDesc> cdResignationDescs { get; set; }
        public virtual ICollection<cdResponsibilityAreaDesc> cdResponsibilityAreaDescs { get; set; }
        public virtual ICollection<cdReturnReasonDesc> cdReturnReasonDescs { get; set; }
        public virtual ICollection<cdRoleDesc> cdRoleDescs { get; set; }
        public virtual ICollection<cdRollNoteTypeDesc> cdRollNoteTypeDescs { get; set; }
        public virtual ICollection<cdSalesChannelDesc> cdSalesChannelDescs { get; set; }
        public virtual ICollection<cdSalespersonTeamDesc> cdSalespersonTeamDescs { get; set; }
        public virtual ICollection<cdSalespersonTypeDesc> cdSalespersonTypeDescs { get; set; }
        public virtual ICollection<cdSalesPlanTypeDesc> cdSalesPlanTypeDescs { get; set; }
        public virtual ICollection<cdScrapReasonDesc> cdScrapReasonDescs { get; set; }
        public virtual ICollection<cdSeasonDesc> cdSeasonDescs { get; set; }
        public virtual ICollection<cdSectionTypeDesc> cdSectionTypeDescs { get; set; }
        public virtual ICollection<cdSGKBorrowingTypeDesc> cdSGKBorrowingTypeDescs { get; set; }
        public virtual ICollection<cdSGKProfessionDesc> cdSGKProfessionDescs { get; set; }
        public virtual ICollection<cdShipmentMethodDesc> cdShipmentMethodDescs { get; set; }
        public virtual ICollection<cdSMSJobTypeDesc> cdSMSJobTypeDescs { get; set; }
        public virtual ICollection<cdSoftwareDesc> cdSoftwareDescs { get; set; }
        public virtual ICollection<cdSoftwareTypeDesc> cdSoftwareTypeDescs { get; set; }
        public virtual ICollection<cdSpecialDayTypeDesc> cdSpecialDayTypeDescs { get; set; }
        public virtual ICollection<cdStateDesc> cdStateDescs { get; set; }
        public virtual ICollection<cdStoreCapacityLevelDesc> cdStoreCapacityLevelDescs { get; set; }
        public virtual ICollection<cdStoreClimateZoneDesc> cdStoreClimateZoneDescs { get; set; }
        public virtual ICollection<cdStoreConceptDesc> cdStoreConceptDescs { get; set; }
        public virtual ICollection<cdStoreCRMGroupDesc> cdStoreCRMGroupDescs { get; set; }
        public virtual ICollection<cdStoreDistributionGroupDesc> cdStoreDistributionGroupDescs { get; set; }
        public virtual ICollection<cdStoreHierarchyLevelDesc> cdStoreHierarchyLevelDescs { get; set; }
        public virtual ICollection<cdStorePriceLevelDesc> cdStorePriceLevelDescs { get; set; }
        public virtual ICollection<cdStoryBoardDesc> cdStoryBoardDescs { get; set; }
        public virtual ICollection<cdSubCurrAccAttributeDesc> cdSubCurrAccAttributeDescs { get; set; }
        public virtual ICollection<cdSubCurrAccAttributeTypeDesc> cdSubCurrAccAttributeTypeDescs { get; set; }
        public virtual ICollection<cdSubJobDepartmentDesc> cdSubJobDepartmentDescs { get; set; }
        public virtual ICollection<cdSubSeasonDesc> cdSubSeasonDescs { get; set; }
        public virtual ICollection<cdSupportResolveTypeDesc> cdSupportResolveTypeDescs { get; set; }
        public virtual ICollection<cdSupportStatusDesc> cdSupportStatusDescs { get; set; }
        public virtual ICollection<cdSurveyDesc> cdSurveyDescs { get; set; }
        public virtual ICollection<cdSurveyQuestionDesc> cdSurveyQuestionDescs { get; set; }
        public virtual ICollection<cdSurveyQuestionOptionDesc> cdSurveyQuestionOptionDescs { get; set; }
        public virtual ICollection<cdSurveySectionDesc> cdSurveySectionDescs { get; set; }
        public virtual ICollection<cdTaxDistrictDesc> cdTaxDistrictDescs { get; set; }
        public virtual ICollection<cdTaxOfficeDesc> cdTaxOfficeDescs { get; set; }
        public virtual ICollection<cdTestDesc> cdTestDescs { get; set; }
        public virtual ICollection<cdTestTypeDesc> cdTestTypeDescs { get; set; }
        public virtual ICollection<cdTextileCareSymbolDesc> cdTextileCareSymbolDescs { get; set; }
        public virtual ICollection<cdTimePeriodDesc> cdTimePeriodDescs { get; set; }
        public virtual ICollection<cdTitleDesc> cdTitleDescs { get; set; }
        public virtual ICollection<cdTransactionCancelReasonDesc> cdTransactionCancelReasonDescs { get; set; }
        public virtual ICollection<cdTransferPlanTemplateDesc> cdTransferPlanTemplateDescs { get; set; }
        public virtual ICollection<cdTurnoverTargetTypeDesc> cdTurnoverTargetTypeDescs { get; set; }
        public virtual ICollection<cdUnDeliveryReasonDesc> cdUnDeliveryReasonDescs { get; set; }
        public virtual ICollection<cdUnitOfMeasureDesc> cdUnitOfMeasureDescs { get; set; }
        public virtual ICollection<cdUniversityDesc> cdUniversityDescs { get; set; }
        public virtual ICollection<cdUniversityFacultyDepDesc> cdUniversityFacultyDepDescs { get; set; }
        public virtual ICollection<cdUniversityFacultyDesc> cdUniversityFacultyDescs { get; set; }
        public virtual ICollection<cdUniversityLevelDesc> cdUniversityLevelDescs { get; set; }
        public virtual ICollection<cdUniversityTypeDesc> cdUniversityTypeDescs { get; set; }
        public virtual ICollection<cdUserWarningDesc> cdUserWarningDescs { get; set; }
        public virtual ICollection<cdVatDesc> cdVatDescs { get; set; }
        public virtual ICollection<cdVehicleTypeDesc> cdVehicleTypeDescs { get; set; }
        public virtual ICollection<cdVendorPaymentPlanGrDesc> cdVendorPaymentPlanGrDescs { get; set; }
        public virtual ICollection<cdVisitFrequencyDesc> cdVisitFrequencyDescs { get; set; }
        public virtual ICollection<cdWageGarnishmentTypeDesc> cdWageGarnishmentTypeDescs { get; set; }
        public virtual ICollection<cdWagePlanTypeDesc> cdWagePlanTypeDescs { get; set; }
        public virtual ICollection<cdWarehouseCategoryDesc> cdWarehouseCategoryDescs { get; set; }
        public virtual ICollection<cdWarehouseChannelTemplateDesc> cdWarehouseChannelTemplateDescs { get; set; }
        public virtual ICollection<cdWarehouseDesc> cdWarehouseDescs { get; set; }
        public virtual ICollection<cdWarehouseTypeDesc> cdWarehouseTypeDescs { get; set; }
        public virtual ICollection<cdWorkForceDesc> cdWorkForceDescs { get; set; }
        public virtual ICollection<cdWorkPlaceDesc> cdWorkPlaceDescs { get; set; }
        public virtual ICollection<cdWorkPlaceGroupDesc> cdWorkPlaceGroupDescs { get; set; }
        public virtual ICollection<cdWorkPlaceTypeDesc> cdWorkPlaceTypeDescs { get; set; }
        public virtual ICollection<cdZoneDesc> cdZoneDescs { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
        public virtual ICollection<dfItemDimensionNames> dfItemDimensionNamess { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<dfPosUIDesc> dfPosUIDescs { get; set; }
        public virtual ICollection<dfProductHierarchyLevelNames> dfProductHierarchyLevelNamess { get; set; }
        public virtual ICollection<dfStoreHierarchyLevelNames> dfStoreHierarchyLevelNamess { get; set; }
        public virtual ICollection<hrJobInterview> hrJobInterviews { get; set; }
        public virtual ICollection<prCurrAccNotes> prCurrAccNotess { get; set; }
        public virtual ICollection<prDataTransferTemplateQueryFilter> prDataTransferTemplateQueryFilters { get; set; }
        public virtual ICollection<prDiscountOfferDescription> prDiscountOfferDescriptions { get; set; }
        public virtual ICollection<prDiscountOfferNotes> prDiscountOfferNotess { get; set; }
        public virtual ICollection<prDiscountPointTypeNotes> prDiscountPointTypeNotess { get; set; }
        public virtual ICollection<prDiscountVoucherTypeNotes> prDiscountVoucherTypeNotess { get; set; }
        public virtual ICollection<prEasyStartupComments> prEasyStartupCommentss { get; set; }
        public virtual ICollection<prEasyStartupNotes> prEasyStartupNotess { get; set; }
        public virtual ICollection<prGLAccNotes> prGLAccNotess { get; set; }
        public virtual ICollection<prItemNotes> prItemNotess { get; set; }
        public virtual ICollection<prJobTrainingNotes> prJobTrainingNotess { get; set; }
        public virtual ICollection<prLoyaltyProgramNotes> prLoyaltyProgramNotess { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
        public virtual ICollection<tpSupportRequestDecisionLetter> tpSupportRequestDecisionLetters { get; set; }
    }
}
