using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCompany")]
    public partial class cdCompany
    {
        public cdCompany()
        {
            auBankPermits = new HashSet<auBankPermit>();
            auCardColumnPermits = new HashSet<auCardColumnPermit>();
            auCardElementPermits = new HashSet<auCardElementPermit>();
            auCardElementRequiredKeys = new HashSet<auCardElementRequiredKey>();
            auCardPermits = new HashSet<auCardPermit>();
            auCashPermits = new HashSet<auCashPermit>();
            auChequeDenys = new HashSet<auChequeDeny>();
            auChequePermits = new HashSet<auChequePermit>();
            auCreditCardPaymentPermits = new HashSet<auCreditCardPaymentPermit>();
            auDebitPermits = new HashSet<auDebitPermit>();
            auExpenseSlipPermits = new HashSet<auExpenseSlipPermit>();
            auInnerProcessPermits = new HashSet<auInnerProcessPermit>();
            auItemTests = new HashSet<auItemTest>();
            auJournalPermits = new HashSet<auJournalPermit>();
            auOptInOptOutTraces = new HashSet<auOptInOptOutTrace>();
            auPaymentPermits = new HashSet<auPaymentPermit>();
            auPriceListPermits = new HashSet<auPriceListPermit>();
            auProcessFlowDenys = new HashSet<auProcessFlowDeny>();
            auProcessPermits = new HashSet<auProcessPermit>();
            auProformaProcessPermits = new HashSet<auProformaProcessPermit>();
            auProgramPermits = new HashSet<auProgramPermit>();
            auPurchaseRequisitionPermits = new HashSet<auPurchaseRequisitionPermit>();
            auPurchaseRequisitionProposalPermits = new HashSet<auPurchaseRequisitionProposalPermit>();
            auReportQueryPermits = new HashSet<auReportQueryPermit>();
            auSupportRequests = new HashSet<auSupportRequest>();
            auSurveyPermits = new HashSet<auSurveyPermit>();
            auSurveySectionPermits = new HashSet<auSurveySectionPermit>();
            auVehicleLoadingPermits = new HashSet<auVehicleLoadingPermit>();
            auVehicleUnLoadingPermits = new HashSet<auVehicleUnLoadingPermit>();
            bsConfirmationRuleTypes = new HashSet<bsConfirmationRuleType>();
            cdAccountants = new HashSet<cdAccountant>();
            cdAllocationTemplates = new HashSet<cdAllocationTemplate>();
            cdBudgetTypes = new HashSet<cdBudgetType>();
            cdChannelTemplates = new HashSet<cdChannelTemplate>();
            cdCheques = new HashSet<cdCheque>();
            cdCompanyCreditCards = new HashSet<cdCompanyCreditCard>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdCustomsCompanys = new HashSet<cdCustomsCompany>();
            cdCustomsOfficess = new HashSet<cdCustomsOffices>();
            cdDeliveryCompanys = new HashSet<cdDeliveryCompany>();
            cdDistanceMatrixProviders = new HashSet<cdDistanceMatrixProvider>();
            cdEArchiveWebServices = new HashSet<cdEArchiveWebService>();
            cdEInvoiceWebServices = new HashSet<cdEInvoiceWebService>();
            cdEMailServices = new HashSet<cdEMailService>();
            cdEShipmentWebServices = new HashSet<cdEShipmentWebService>();
            cdExportFiles = new HashSet<cdExportFile>();
            cdFinanceCompanyWebServices = new HashSet<cdFinanceCompanyWebService>();
            cdGLAccs = new HashSet<cdGLAcc>();
            cdGLAccSubs = new HashSet<cdGLAccSub>();
            cdImportFiles = new HashSet<cdImportFile>();
            cdInteractiveSmsParameterss = new HashSet<cdInteractiveSmsParameters>();
            cdItems = new HashSet<cdItem>();
            cdJobPositions = new HashSet<cdJobPosition>();
            cdLawyers = new HashSet<cdLawyer>();
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            cdMMSBusinessPartnerServices = new HashSet<cdMMSBusinessPartnerService>();
            cdOffices = new HashSet<cdOffice>();
            cdOfficeCOGSGrs = new HashSet<cdOfficeCOGSGr>();
            cdOnlineBankWebServices = new HashSet<cdOnlineBankWebService>();
            cdOnlineDBSWebServices = new HashSet<cdOnlineDBSWebService>();
            cdPermissionMarketingServices = new HashSet<cdPermissionMarketingService>();
            cdProposalConfirmationLimits = new HashSet<cdProposalConfirmationLimit>();
            cdProposalConfirmationRules = new HashSet<cdProposalConfirmationRule>();
            cdRegisteredEMailServices = new HashSet<cdRegisteredEMailService>();
            cdRequisitionConfirmationLimits = new HashSet<cdRequisitionConfirmationLimit>();
            cdRequisitionConfirmationRules = new HashSet<cdRequisitionConfirmationRule>();
            cdRoles = new HashSet<cdRole>();
            cdSMSGatewayServices = new HashSet<cdSMSGatewayService>();
            cdTests = new HashSet<cdTest>();
            cdTransferPlanTemplates = new HashSet<cdTransferPlanTemplate>();
            cdTranslationProviders = new HashSet<cdTranslationProvider>();
            cdWarehouseChannelTemplates = new HashSet<cdWarehouseChannelTemplate>();
            cdWarehouseChannelTemplateDescs = new HashSet<cdWarehouseChannelTemplateDesc>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            dfBankDefATAttributes = new HashSet<dfBankDefATAttribute>();
            dfBankPOSReturnsRules = new HashSet<dfBankPOSReturnsRule>();
            dfBulkMailServiceProviderAccounts = new HashSet<dfBulkMailServiceProviderAccount>();
            dfBulutTahsilatVPosCompanys = new HashSet<dfBulutTahsilatVPosCompany>();
            dfBulutTahsilatVPosOffices = new HashSet<dfBulutTahsilatVPosOffice>();
            dfCarriageExpenseCodess = new HashSet<dfCarriageExpenseCodes>();
            dfCashDefATAttributes = new HashSet<dfCashDefATAttribute>();
            dfChequeDefATAttributes = new HashSet<dfChequeDefATAttribute>();
            dfCompanyClosedPeriods = new HashSet<dfCompanyClosedPeriod>();
            dfCompanyCostOfGoodsSolds = new HashSet<dfCompanyCostOfGoodsSold>();
            dfCompanyCurrAccSizes = new HashSet<dfCompanyCurrAccSize>();
            dfCompanyDeductionDefaults = new HashSet<dfCompanyDeductionDefault>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfCompanyDigitalMarketingServiceAdresss = new HashSet<dfCompanyDigitalMarketingServiceAdress>();
            dfCompanyEarningsDefaults = new HashSet<dfCompanyEarningsDefault>();
            dfCompanyEarningsMonthlys = new HashSet<dfCompanyEarningsMonthly>();
            dfCompanyEmailDefaults = new HashSet<dfCompanyEmailDefault>();
            dfCompanyFolders = new HashSet<dfCompanyFolder>();
            dfCompanyLockTransactions = new HashSet<dfCompanyLockTransaction>();
            dfCompanyMarkups = new HashSet<dfCompanyMarkup>();
            dfCompanyPriceGroups = new HashSet<dfCompanyPriceGroup>();
            dfCompanyProcessLockTransactions = new HashSet<dfCompanyProcessLockTransaction>();
            dfCreditCardPaymentDefATAttributes = new HashSet<dfCreditCardPaymentDefATAttribute>();
            dfCurrAccProductLotLevelss = new HashSet<dfCurrAccProductLotLevels>();
            dfCustomizedDiscountEngineCompanys = new HashSet<dfCustomizedDiscountEngineCompany>();
            dfCustomsCompanys = new HashSet<dfCustomsCompany>();
            dfDMSCompanys = new HashSet<dfDMSCompany>();
            dfDomesticPPIs = new HashSet<dfDomesticPPI>();
            dfEArchiveWebServiceParameterss = new HashSet<dfEArchiveWebServiceParameters>();
            dfEInvoiceWebServiceParameterss = new HashSet<dfEInvoiceWebServiceParameters>();
            dfEShipmentWebServiceParameterss = new HashSet<dfEShipmentWebServiceParameters>();
            dffastPayCompanys = new HashSet<dffastPayCompany>();
            dfGetirCarsiCompanys = new HashSet<dfGetirCarsiCompany>();
            dfGLClosedYears = new HashSet<dfGLClosedYear>();
            dfGlobalBlueCompanys = new HashSet<dfGlobalBlueCompany>();
            dfHopiCompanys = new HashSet<dfHopiCompany>();
            dfIGACompanys = new HashSet<dfIGACompany>();
            dfIncomeTaxReliefs = new HashSet<dfIncomeTaxRelief>();
            dfInstallmentCountRulesBrackets = new HashSet<dfInstallmentCountRulesBracket>();
            dfInsuaranceExpenseCodess = new HashSet<dfInsuaranceExpenseCodes>();
            dfIyzicoCompanys = new HashSet<dfIyzicoCompany>();
            dfJournalDefATAttributes = new HashSet<dfJournalDefATAttribute>();
            dfJoyRefundCompanys = new HashSet<dfJoyRefundCompany>();
            dfMacellanSuperappCompanys = new HashSet<dfMacellanSuperappCompany>();
            dfMarketPlaceParameterss = new HashSet<dfMarketPlaceParameters>();
            dfMobilDevCompanyBrandCollectorIDs = new HashSet<dfMobilDevCompanyBrandCollectorID>();
            dfMobilDevCompanyThirdPartyCollectorIDs = new HashSet<dfMobilDevCompanyThirdPartyCollectorID>();
            dfMobilDevStoreCompanyBrandCollectorIDs = new HashSet<dfMobilDevStoreCompanyBrandCollectorID>();
            dfMobilRevenueUsers = new HashSet<dfMobilRevenueUser>();
            dfMonthlyTurnoverTargets = new HashSet<dfMonthlyTurnoverTarget>();
            dfOnlineDistributors = new HashSet<dfOnlineDistributor>();
            dfOnlineInstallmentPaymentParameterss = new HashSet<dfOnlineInstallmentPaymentParameters>();
            dfOnlineSalesandPaymentBankAccss = new HashSet<dfOnlineSalesandPaymentBankAccs>();
            dfOnlineSalesAndPaymentParametersForConnections = new HashSet<dfOnlineSalesAndPaymentParametersForConnection>();
            dfOtpServiceCompanys = new HashSet<dfOtpServiceCompany>();
            dfPAROCompanys = new HashSet<dfPAROCompany>();
            dfPaxEftPosCompanys = new HashSet<dfPaxEftPosCompany>();
            dfPaynetCompanys = new HashSet<dfPaynetCompany>();
            dfPayrollDefaults = new HashSet<dfPayrollDefault>();
            dfPayrollForms = new HashSet<dfPayrollForm>();
            dfPDCCurrAccs = new HashSet<dfPDCCurrAcc>();
            dfPDCCurrAccAttributes = new HashSet<dfPDCCurrAccAttribute>();
            dfPDCCurrAccCommunications = new HashSet<dfPDCCurrAccCommunication>();
            dfPDCCurrAccContacts = new HashSet<dfPDCCurrAccContact>();
            dfPDCCurrAccPersonalInfos = new HashSet<dfPDCCurrAccPersonalInfo>();
            dfPDCCurrAccPostalAddresss = new HashSet<dfPDCCurrAccPostalAddress>();
            dfPDCCustomerCompanyBrandAttributes = new HashSet<dfPDCCustomerCompanyBrandAttribute>();
            dfPDCQuerys = new HashSet<dfPDCQuery>();
            dfPeriodicalAllocationRules = new HashSet<dfPeriodicalAllocationRule>();
            dfPeriodicalSMSRules = new HashSet<dfPeriodicalSMSRule>();
            dfPeriodicalTransferPlanRules = new HashSet<dfPeriodicalTransferPlanRule>();
            dfPlanetPaymentCompanys = new HashSet<dfPlanetPaymentCompany>();
            dfSocialInsuranceRatess = new HashSet<dfSocialInsuranceRates>();
            dfTaxFreePointCompanys = new HashSet<dfTaxFreePointCompany>();
            dfTaxFreeZoneCompanys = new HashSet<dfTaxFreeZoneCompany>();
            dfTransactionDefFTAttributes = new HashSet<dfTransactionDefFTAttribute>();
            dfUnifreeCompanys = new HashSet<dfUnifreeCompany>();
            dfUserAllowedOffices = new HashSet<dfUserAllowedOffice>();
            dfUserAllowedStores = new HashSet<dfUserAllowedStore>();
            dfUserPositions = new HashSet<dfUserPosition>();
            dfUTSWebServiceTokens = new HashSet<dfUTSWebServiceToken>();
            dfWeArePlanetTaxFreeCompanys = new HashSet<dfWeArePlanetTaxFreeCompany>();
            e_LastShipmentAskDates = new HashSet<e_LastShipmentAskDate>();
            hrEmployeeOrganizationCharts = new HashSet<hrEmployeeOrganizationChart>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
            hrJobInterviews = new HashSet<hrJobInterview>();
            hrJobInterviewResultss = new HashSet<hrJobInterviewResults>();
            hrJobTitleOrganizationCharts = new HashSet<hrJobTitleOrganizationChart>();
            hrSGKEmployeeJobEndDeclarations = new HashSet<hrSGKEmployeeJobEndDeclaration>();
            hrSGKEmployeeJobStartDeclarations = new HashSet<hrSGKEmployeeJobStartDeclaration>();
            prBankPOSAccountss = new HashSet<prBankPOSAccounts>();
            prCompanyCostCenters = new HashSet<prCompanyCostCenter>();
            prCompanyExpenses = new HashSet<prCompanyExpense>();
            prCompanyExpenseInvoiceConfirmationRules = new HashSet<prCompanyExpenseInvoiceConfirmationRule>();
            prCompanyHierarchys = new HashSet<prCompanyHierarchy>();
            prConfirmationRequiredProductGroupss = new HashSet<prConfirmationRequiredProductGroups>();
            prCostCenterHierarchys = new HashSet<prCostCenterHierarchy>();
            prCurrAccAvailableForeignCurrencyTranss = new HashSet<prCurrAccAvailableForeignCurrencyTrans>();
            prCurrAccEInvoiceAliass = new HashSet<prCurrAccEInvoiceAlias>();
            prCurrAccLotGrAtts = new HashSet<prCurrAccLotGrAtt>();
            prCurrAccPersonalDataConfirmations = new HashSet<prCurrAccPersonalDataConfirmation>();
            prCustomerConversations = new HashSet<prCustomerConversation>();
            prCustomerDBSAccounts = new HashSet<prCustomerDBSAccount>();
            prCustomerDiscountGrAtts = new HashSet<prCustomerDiscountGrAtt>();
            prCustomerMarkupGrAtts = new HashSet<prCustomerMarkupGrAtt>();
            prCustomerPaymentPlanGrAtts = new HashSet<prCustomerPaymentPlanGrAtt>();
            prCustomerVerificationPasswords = new HashSet<prCustomerVerificationPassword>();
            prEArchiveWebServiceCompanys = new HashSet<prEArchiveWebServiceCompany>();
            prEArchiveWebServiceOffices = new HashSet<prEArchiveWebServiceOffice>();
            prEInvoiceWebServiceCompanys = new HashSet<prEInvoiceWebServiceCompany>();
            prEInvoiceWebServiceOffices = new HashSet<prEInvoiceWebServiceOffice>();
            prExpenseInvoiceConfirmationRules = new HashSet<prExpenseInvoiceConfirmationRule>();
            prGLAccAvailableForeignCurrencyTranss = new HashSet<prGLAccAvailableForeignCurrencyTrans>();
            prImportFileExpenses = new HashSet<prImportFileExpense>();
            prInnerProcessInfos = new HashSet<prInnerProcessInfo>();
            prInnerProcessITAttributes = new HashSet<prInnerProcessITAttribute>();
            prInnerProcessItemTypes = new HashSet<prInnerProcessItemType>();
            prInteractiveSMSApplicationss = new HashSet<prInteractiveSMSApplications>();
            prITAttributeTypeRequiredProcessess = new HashSet<prITAttributeTypeRequiredProcesses>();
            prItemDiscountGrAtts = new HashSet<prItemDiscountGrAtt>();
            prItemPaymentPlanGrAtts = new HashSet<prItemPaymentPlanGrAtt>();
            prItemVendorGrAtts = new HashSet<prItemVendorGrAtt>();
            prMarketPlaceCreditCardMappingss = new HashSet<prMarketPlaceCreditCardMappings>();
            prMarketPlaceProducts = new HashSet<prMarketPlaceProduct>();
            prMarketPlaceProductInformations = new HashSet<prMarketPlaceProductInformation>();
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
            prPersonalDataConfirmationFormTypeForCurrAccTypess = new HashSet<prPersonalDataConfirmationFormTypeForCurrAccTypes>();
            prProcessATAttributes = new HashSet<prProcessATAttribute>();
            prProcessDefaultExpenseTypes = new HashSet<prProcessDefaultExpenseType>();
            prProcessDiscounts = new HashSet<prProcessDiscount>();
            prProcessFlowRuless = new HashSet<prProcessFlowRules>();
            prProcessFTAttributes = new HashSet<prProcessFTAttribute>();
            prProcessInfos = new HashSet<prProcessInfo>();
            prProcessITAttributes = new HashSet<prProcessITAttribute>();
            prProcessItemTypes = new HashSet<prProcessItemType>();
            prPurchasingAgentAvailableRequisitions = new HashSet<prPurchasingAgentAvailableRequisition>();
            prRequisitionLimits = new HashSet<prRequisitionLimit>();
            prTechnicalResponsibleAvailableRequisitions = new HashSet<prTechnicalResponsibleAvailableRequisition>();
            prVendorPaymentPlanGrAtts = new HashSet<prVendorPaymentPlanGrAtt>();
            rpOrderDeliveryAssignmentCollectedItemss = new HashSet<rpOrderDeliveryAssignmentCollectedItems>();
            rpRegisteredEmailForPayrollSendStatuss = new HashSet<rpRegisteredEmailForPayrollSendStatus>();
            srCodeNumberCheques = new HashSet<srCodeNumberCheque>();
            srCodeNumberCurrAccs = new HashSet<srCodeNumberCurrAcc>();
            srCodeNumberGiftCards = new HashSet<srCodeNumberGiftCard>();
            srCodeNumberItems = new HashSet<srCodeNumberItem>();
            srCodeNumberLetterOfGuarantees = new HashSet<srCodeNumberLetterOfGuarantee>();
            srCodeNumberWarehouses = new HashSet<srCodeNumberWarehouse>();
            srCustomerConversationFormNumbers = new HashSet<srCustomerConversationFormNumber>();
            srDistanceSaleBankPaymentNumbers = new HashSet<srDistanceSaleBankPaymentNumber>();
            srEArchiveSerialNumbers = new HashSet<srEArchiveSerialNumber>();
            srEInvoiceSerialNumbers = new HashSet<srEInvoiceSerialNumber>();
            srEShipmentSerialNumbers = new HashSet<srEShipmentSerialNumber>();
            srExpenseInvoiceDocumentNumbers = new HashSet<srExpenseInvoiceDocumentNumber>();
            srOnlineInstallmentBankPayments = new HashSet<srOnlineInstallmentBankPayment>();
            srOpticalProtocolNumbers = new HashSet<srOpticalProtocolNumber>();
            srPayrollDocumentNumbers = new HashSet<srPayrollDocumentNumber>();
            srRefNumberAdjustCosts = new HashSet<srRefNumberAdjustCost>();
            srRefNumberAgentReservations = new HashSet<srRefNumberAgentReservation>();
            srRefNumberAllocations = new HashSet<srRefNumberAllocation>();
            srRefNumberBadDebts = new HashSet<srRefNumberBadDebt>();
            srRefNumberBankCredits = new HashSet<srRefNumberBankCredit>();
            srRefNumberBankPaymentInstructions = new HashSet<srRefNumberBankPaymentInstruction>();
            srRefNumberBankPaymentLists = new HashSet<srRefNumberBankPaymentList>();
            srRefNumberBankTranss = new HashSet<srRefNumberBankTrans>();
            srRefNumberCashTranss = new HashSet<srRefNumberCashTrans>();
            srRefNumberChequeTranss = new HashSet<srRefNumberChequeTrans>();
            srRefNumberConfirmationForms = new HashSet<srRefNumberConfirmationForm>();
            srRefNumberContracts = new HashSet<srRefNumberContract>();
            srRefNumberCreditCardPayments = new HashSet<srRefNumberCreditCardPayment>();
            srRefNumberDebits = new HashSet<srRefNumberDebit>();
            srRefNumberDepartmentReceipts = new HashSet<srRefNumberDepartmentReceipt>();
            srRefNumberDeviceDocuments = new HashSet<srRefNumberDeviceDocument>();
            srRefNumberExpenseAccruals = new HashSet<srRefNumberExpenseAccrual>();
            srRefNumberExpenseSlips = new HashSet<srRefNumberExpenseSlip>();
            srRefNumberGiftCardPayments = new HashSet<srRefNumberGiftCardPayment>();
            srRefNumberIncentives = new HashSet<srRefNumberIncentive>();
            srRefNumberInnerOrders = new HashSet<srRefNumberInnerOrder>();
            srRefNumberInnerProcesss = new HashSet<srRefNumberInnerProcess>();
            srRefNumberItemTests = new HashSet<srRefNumberItemTest>();
            srRefNumberJournals = new HashSet<srRefNumberJournal>();
            srRefNumberOtherPayments = new HashSet<srRefNumberOtherPayment>();
            srRefNumberPayments = new HashSet<srRefNumberPayment>();
            srRefNumberPriceLists = new HashSet<srRefNumberPriceList>();
            srRefNumberProcessFlows = new HashSet<srRefNumberProcessFlow>();
            srRefNumberPurchaseRequisitions = new HashSet<srRefNumberPurchaseRequisition>();
            srRefNumberReportedSales = new HashSet<srRefNumberReportedSale>();
            srRefNumberSalesPlans = new HashSet<srRefNumberSalesPlan>();
            srRefNumberSupportRequests = new HashSet<srRefNumberSupportRequest>();
            srRefNumberTaxIncurreds = new HashSet<srRefNumberTaxIncurred>();
            srRefNumberTransferPlans = new HashSet<srRefNumberTransferPlan>();
            srRefNumberVehicleLoadings = new HashSet<srRefNumberVehicleLoading>();
            srRefNumberVehicleUnLoadings = new HashSet<srRefNumberVehicleUnLoading>();
            srRefNumberVendorPriceLists = new HashSet<srRefNumberVendorPriceList>();
            srRefNumberVirements = new HashSet<srRefNumberVirement>();
            tpJournalZNums = new HashSet<tpJournalZNum>();
            tpOrderCancelDetailHeaders = new HashSet<tpOrderCancelDetailHeader>();
            tpOrderDeliveryDetails = new HashSet<tpOrderDeliveryDetail>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            tpSupportResolves = new HashSet<tpSupportResolve>();
            trAdjustCostHeaders = new HashSet<trAdjustCostHeader>();
            trAgentContractHeaders = new HashSet<trAgentContractHeader>();
            trAgentPerformanceBonusHeaders = new HashSet<trAgentPerformanceBonusHeader>();
            trAgentReservationHeaders = new HashSet<trAgentReservationHeader>();
            trAllocations = new HashSet<trAllocation>();
            trBadDebtTransHeaders = new HashSet<trBadDebtTransHeader>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
            trBankHeaders = new HashSet<trBankHeader>();
            trBankPaymentInstructionHeaders = new HashSet<trBankPaymentInstructionHeader>();
            trBankPaymentListHeaders = new HashSet<trBankPaymentListHeader>();
            trCashHeaders = new HashSet<trCashHeader>();
            trChequeHeaders = new HashSet<trChequeHeader>();
            trContracts = new HashSet<trContract>();
            trCreditCardPaymentHeaders = new HashSet<trCreditCardPaymentHeader>();
            trDebitHeaders = new HashSet<trDebitHeader>();
            trDepartmentReceiptHeaders = new HashSet<trDepartmentReceiptHeader>();
            trDispOrderHeaders = new HashSet<trDispOrderHeader>();
            trGiftCardPaymentHeaders = new HashSet<trGiftCardPaymentHeader>();
            trIncentiveHeaders = new HashSet<trIncentiveHeader>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trItemTestHeaders = new HashSet<trItemTestHeader>();
            trJournalLedgerEntryNumbers = new HashSet<trJournalLedgerEntryNumber>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
            trPaymentHeaders = new HashSet<trPaymentHeader>();
            trPickingHeaders = new HashSet<trPickingHeader>();
            trPriceListHeaders = new HashSet<trPriceListHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trPurchaseRequisitionHeaders = new HashSet<trPurchaseRequisitionHeader>();
            trReportedSaleHeaders = new HashSet<trReportedSaleHeader>();
            trReserveHeaders = new HashSet<trReserveHeader>();
            trSalesPlans = new HashSet<trSalesPlan>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trSMSPoolHeaders = new HashSet<trSMSPoolHeader>();
            trStocks = new HashSet<trStock>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            trTaxIncurredHeaders = new HashSet<trTaxIncurredHeader>();
            trTransferPlans = new HashSet<trTransferPlan>();
            trVehicleLoadingHeaders = new HashSet<trVehicleLoadingHeader>();
            trVehicleUnLoadingHeaders = new HashSet<trVehicleUnLoadingHeader>();
            trVendorPriceListHeaders = new HashSet<trVendorPriceListHeader>();
            trVirementHeaders = new HashSet<trVirementHeader>();
            zpOnlineBankCreditCardPaymentTransactions = new HashSet<zpOnlineBankCreditCardPaymentTransaction>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ZoneCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CompanyName { get; set; }

        [Required]
        public bool IsZoneMainCompany { get; set; }

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
        public virtual cdZone cdZone { get; set; }

        public virtual ICollection<auBankPermit> auBankPermits { get; set; }
        public virtual ICollection<auCardColumnPermit> auCardColumnPermits { get; set; }
        public virtual ICollection<auCardElementPermit> auCardElementPermits { get; set; }
        public virtual ICollection<auCardElementRequiredKey> auCardElementRequiredKeys { get; set; }
        public virtual ICollection<auCardPermit> auCardPermits { get; set; }
        public virtual ICollection<auCashPermit> auCashPermits { get; set; }
        public virtual ICollection<auChequeDeny> auChequeDenys { get; set; }
        public virtual ICollection<auChequePermit> auChequePermits { get; set; }
        public virtual ICollection<auCreditCardPaymentPermit> auCreditCardPaymentPermits { get; set; }
        public virtual ICollection<auDebitPermit> auDebitPermits { get; set; }
        public virtual ICollection<auExpenseSlipPermit> auExpenseSlipPermits { get; set; }
        public virtual ICollection<auInnerProcessPermit> auInnerProcessPermits { get; set; }
        public virtual ICollection<auItemTest> auItemTests { get; set; }
        public virtual ICollection<auJournalPermit> auJournalPermits { get; set; }
        public virtual ICollection<auOptInOptOutTrace> auOptInOptOutTraces { get; set; }
        public virtual ICollection<auPaymentPermit> auPaymentPermits { get; set; }
        public virtual ICollection<auPriceListPermit> auPriceListPermits { get; set; }
        public virtual ICollection<auProcessFlowDeny> auProcessFlowDenys { get; set; }
        public virtual ICollection<auProcessPermit> auProcessPermits { get; set; }
        public virtual ICollection<auProformaProcessPermit> auProformaProcessPermits { get; set; }
        public virtual ICollection<auProgramPermit> auProgramPermits { get; set; }
        public virtual ICollection<auPurchaseRequisitionPermit> auPurchaseRequisitionPermits { get; set; }
        public virtual ICollection<auPurchaseRequisitionProposalPermit> auPurchaseRequisitionProposalPermits { get; set; }
        public virtual ICollection<auReportQueryPermit> auReportQueryPermits { get; set; }
        public virtual ICollection<auSupportRequest> auSupportRequests { get; set; }
        public virtual ICollection<auSurveyPermit> auSurveyPermits { get; set; }
        public virtual ICollection<auSurveySectionPermit> auSurveySectionPermits { get; set; }
        public virtual ICollection<auVehicleLoadingPermit> auVehicleLoadingPermits { get; set; }
        public virtual ICollection<auVehicleUnLoadingPermit> auVehicleUnLoadingPermits { get; set; }
        public virtual ICollection<bsConfirmationRuleType> bsConfirmationRuleTypes { get; set; }
        public virtual ICollection<cdAccountant> cdAccountants { get; set; }
        public virtual ICollection<cdAllocationTemplate> cdAllocationTemplates { get; set; }
        public virtual ICollection<cdBudgetType> cdBudgetTypes { get; set; }
        public virtual ICollection<cdChannelTemplate> cdChannelTemplates { get; set; }
        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdCompanyCreditCard> cdCompanyCreditCards { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdCustomsCompany> cdCustomsCompanys { get; set; }
        public virtual ICollection<cdCustomsOffices> cdCustomsOfficess { get; set; }
        public virtual ICollection<cdDeliveryCompany> cdDeliveryCompanys { get; set; }
        public virtual ICollection<cdDistanceMatrixProvider> cdDistanceMatrixProviders { get; set; }
        public virtual ICollection<cdEArchiveWebService> cdEArchiveWebServices { get; set; }
        public virtual ICollection<cdEInvoiceWebService> cdEInvoiceWebServices { get; set; }
        public virtual ICollection<cdEMailService> cdEMailServices { get; set; }
        public virtual ICollection<cdEShipmentWebService> cdEShipmentWebServices { get; set; }
        public virtual ICollection<cdExportFile> cdExportFiles { get; set; }
        public virtual ICollection<cdFinanceCompanyWebService> cdFinanceCompanyWebServices { get; set; }
        public virtual ICollection<cdGLAcc> cdGLAccs { get; set; }
        public virtual ICollection<cdGLAccSub> cdGLAccSubs { get; set; }
        public virtual ICollection<cdImportFile> cdImportFiles { get; set; }
        public virtual ICollection<cdInteractiveSmsParameters> cdInteractiveSmsParameterss { get; set; }
        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdJobPosition> cdJobPositions { get; set; }
        public virtual ICollection<cdLawyer> cdLawyers { get; set; }
        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<cdMMSBusinessPartnerService> cdMMSBusinessPartnerServices { get; set; }
        public virtual ICollection<cdOffice> cdOffices { get; set; }
        public virtual ICollection<cdOfficeCOGSGr> cdOfficeCOGSGrs { get; set; }
        public virtual ICollection<cdOnlineBankWebService> cdOnlineBankWebServices { get; set; }
        public virtual ICollection<cdOnlineDBSWebService> cdOnlineDBSWebServices { get; set; }
        public virtual ICollection<cdPermissionMarketingService> cdPermissionMarketingServices { get; set; }
        public virtual ICollection<cdProposalConfirmationLimit> cdProposalConfirmationLimits { get; set; }
        public virtual ICollection<cdProposalConfirmationRule> cdProposalConfirmationRules { get; set; }
        public virtual ICollection<cdRegisteredEMailService> cdRegisteredEMailServices { get; set; }
        public virtual ICollection<cdRequisitionConfirmationLimit> cdRequisitionConfirmationLimits { get; set; }
        public virtual ICollection<cdRequisitionConfirmationRule> cdRequisitionConfirmationRules { get; set; }
        public virtual ICollection<cdRole> cdRoles { get; set; }
        public virtual ICollection<cdSMSGatewayService> cdSMSGatewayServices { get; set; }
        public virtual ICollection<cdTest> cdTests { get; set; }
        public virtual ICollection<cdTransferPlanTemplate> cdTransferPlanTemplates { get; set; }
        public virtual ICollection<cdTranslationProvider> cdTranslationProviders { get; set; }
        public virtual ICollection<cdWarehouseChannelTemplate> cdWarehouseChannelTemplates { get; set; }
        public virtual ICollection<cdWarehouseChannelTemplateDesc> cdWarehouseChannelTemplateDescs { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<dfBankDefATAttribute> dfBankDefATAttributes { get; set; }
        public virtual ICollection<dfBankPOSReturnsRule> dfBankPOSReturnsRules { get; set; }
        public virtual ICollection<dfBulkMailServiceProviderAccount> dfBulkMailServiceProviderAccounts { get; set; }
        public virtual ICollection<dfBulutTahsilatVPosCompany> dfBulutTahsilatVPosCompanys { get; set; }
        public virtual ICollection<dfBulutTahsilatVPosOffice> dfBulutTahsilatVPosOffices { get; set; }
        public virtual ICollection<dfCarriageExpenseCodes> dfCarriageExpenseCodess { get; set; }
        public virtual ICollection<dfCashDefATAttribute> dfCashDefATAttributes { get; set; }
        public virtual ICollection<dfChequeDefATAttribute> dfChequeDefATAttributes { get; set; }
        public virtual ICollection<dfCompanyClosedPeriod> dfCompanyClosedPeriods { get; set; }
        public virtual ICollection<dfCompanyCostOfGoodsSold> dfCompanyCostOfGoodsSolds { get; set; }
        public virtual ICollection<dfCompanyCurrAccSize> dfCompanyCurrAccSizes { get; set; }
        public virtual ICollection<dfCompanyDeductionDefault> dfCompanyDeductionDefaults { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfCompanyDigitalMarketingServiceAdress> dfCompanyDigitalMarketingServiceAdresss { get; set; }
        public virtual ICollection<dfCompanyEarningsDefault> dfCompanyEarningsDefaults { get; set; }
        public virtual ICollection<dfCompanyEarningsMonthly> dfCompanyEarningsMonthlys { get; set; }
        public virtual ICollection<dfCompanyEmailDefault> dfCompanyEmailDefaults { get; set; }
        public virtual ICollection<dfCompanyFolder> dfCompanyFolders { get; set; }
        public virtual ICollection<dfCompanyLockTransaction> dfCompanyLockTransactions { get; set; }
        public virtual ICollection<dfCompanyMarkup> dfCompanyMarkups { get; set; }
        public virtual ICollection<dfCompanyPriceGroup> dfCompanyPriceGroups { get; set; }
        public virtual ICollection<dfCompanyProcessLockTransaction> dfCompanyProcessLockTransactions { get; set; }
        public virtual ICollection<dfCreditCardPaymentDefATAttribute> dfCreditCardPaymentDefATAttributes { get; set; }
        public virtual ICollection<dfCurrAccProductLotLevels> dfCurrAccProductLotLevelss { get; set; }
        public virtual ICollection<dfCustomizedDiscountEngineCompany> dfCustomizedDiscountEngineCompanys { get; set; }
        public virtual ICollection<dfCustomsCompany> dfCustomsCompanys { get; set; }
        public virtual ICollection<dfDMSCompany> dfDMSCompanys { get; set; }
        public virtual ICollection<dfDomesticPPI> dfDomesticPPIs { get; set; }
        public virtual ICollection<dfEArchiveWebServiceParameters> dfEArchiveWebServiceParameterss { get; set; }
        public virtual ICollection<dfEInvoiceWebServiceParameters> dfEInvoiceWebServiceParameterss { get; set; }
        public virtual ICollection<dfEShipmentWebServiceParameters> dfEShipmentWebServiceParameterss { get; set; }
        public virtual ICollection<dffastPayCompany> dffastPayCompanys { get; set; }
        public virtual ICollection<dfGetirCarsiCompany> dfGetirCarsiCompanys { get; set; }
        public virtual ICollection<dfGLClosedYear> dfGLClosedYears { get; set; }
        public virtual ICollection<dfGlobalBlueCompany> dfGlobalBlueCompanys { get; set; }
        public virtual ICollection<dfHopiCompany> dfHopiCompanys { get; set; }
        public virtual ICollection<dfIGACompany> dfIGACompanys { get; set; }
        public virtual ICollection<dfIncomeTaxRelief> dfIncomeTaxReliefs { get; set; }
        public virtual ICollection<dfInstallmentCountRulesBracket> dfInstallmentCountRulesBrackets { get; set; }
        public virtual ICollection<dfInsuaranceExpenseCodes> dfInsuaranceExpenseCodess { get; set; }
        public virtual ICollection<dfIyzicoCompany> dfIyzicoCompanys { get; set; }
        public virtual ICollection<dfJournalDefATAttribute> dfJournalDefATAttributes { get; set; }
        public virtual ICollection<dfJoyRefundCompany> dfJoyRefundCompanys { get; set; }
        public virtual ICollection<dfMacellanSuperappCompany> dfMacellanSuperappCompanys { get; set; }
        public virtual ICollection<dfMarketPlaceParameters> dfMarketPlaceParameterss { get; set; }
        public virtual ICollection<dfMobilDevCompanyBrandCollectorID> dfMobilDevCompanyBrandCollectorIDs { get; set; }
        public virtual ICollection<dfMobilDevCompanyThirdPartyCollectorID> dfMobilDevCompanyThirdPartyCollectorIDs { get; set; }
        public virtual ICollection<dfMobilDevStoreCompanyBrandCollectorID> dfMobilDevStoreCompanyBrandCollectorIDs { get; set; }
        public virtual ICollection<dfMobilRevenueUser> dfMobilRevenueUsers { get; set; }
        public virtual ICollection<dfMonthlyTurnoverTarget> dfMonthlyTurnoverTargets { get; set; }
        public virtual ICollection<dfOnlineDistributor> dfOnlineDistributors { get; set; }
        public virtual ICollection<dfOnlineInstallmentPaymentParameters> dfOnlineInstallmentPaymentParameterss { get; set; }
        public virtual ICollection<dfOnlineSalesandPaymentBankAccs> dfOnlineSalesandPaymentBankAccss { get; set; }
        public virtual ICollection<dfOnlineSalesAndPaymentParametersForConnection> dfOnlineSalesAndPaymentParametersForConnections { get; set; }
        public virtual ICollection<dfOtpServiceCompany> dfOtpServiceCompanys { get; set; }
        public virtual ICollection<dfPAROCompany> dfPAROCompanys { get; set; }
        public virtual ICollection<dfPaxEftPosCompany> dfPaxEftPosCompanys { get; set; }
        public virtual ICollection<dfPaynetCompany> dfPaynetCompanys { get; set; }
        public virtual ICollection<dfPayrollDefault> dfPayrollDefaults { get; set; }
        public virtual ICollection<dfPayrollForm> dfPayrollForms { get; set; }
        public virtual ICollection<dfPDCCurrAcc> dfPDCCurrAccs { get; set; }
        public virtual ICollection<dfPDCCurrAccAttribute> dfPDCCurrAccAttributes { get; set; }
        public virtual ICollection<dfPDCCurrAccCommunication> dfPDCCurrAccCommunications { get; set; }
        public virtual ICollection<dfPDCCurrAccContact> dfPDCCurrAccContacts { get; set; }
        public virtual ICollection<dfPDCCurrAccPersonalInfo> dfPDCCurrAccPersonalInfos { get; set; }
        public virtual ICollection<dfPDCCurrAccPostalAddress> dfPDCCurrAccPostalAddresss { get; set; }
        public virtual ICollection<dfPDCCustomerCompanyBrandAttribute> dfPDCCustomerCompanyBrandAttributes { get; set; }
        public virtual ICollection<dfPDCQuery> dfPDCQuerys { get; set; }
        public virtual ICollection<dfPeriodicalAllocationRule> dfPeriodicalAllocationRules { get; set; }
        public virtual ICollection<dfPeriodicalSMSRule> dfPeriodicalSMSRules { get; set; }
        public virtual ICollection<dfPeriodicalTransferPlanRule> dfPeriodicalTransferPlanRules { get; set; }
        public virtual ICollection<dfPlanetPaymentCompany> dfPlanetPaymentCompanys { get; set; }
        public virtual ICollection<dfSocialInsuranceRates> dfSocialInsuranceRatess { get; set; }
        public virtual ICollection<dfTaxFreePointCompany> dfTaxFreePointCompanys { get; set; }
        public virtual ICollection<dfTaxFreeZoneCompany> dfTaxFreeZoneCompanys { get; set; }
        public virtual ICollection<dfTransactionDefFTAttribute> dfTransactionDefFTAttributes { get; set; }
        public virtual ICollection<dfUnifreeCompany> dfUnifreeCompanys { get; set; }
        public virtual ICollection<dfUserAllowedOffice> dfUserAllowedOffices { get; set; }
        public virtual ICollection<dfUserAllowedStore> dfUserAllowedStores { get; set; }
        public virtual ICollection<dfUserPosition> dfUserPositions { get; set; }
        public virtual ICollection<dfUTSWebServiceToken> dfUTSWebServiceTokens { get; set; }
        public virtual ICollection<dfWeArePlanetTaxFreeCompany> dfWeArePlanetTaxFreeCompanys { get; set; }
        public virtual ICollection<e_LastShipmentAskDate> e_LastShipmentAskDates { get; set; }
        public virtual ICollection<hrEmployeeOrganizationChart> hrEmployeeOrganizationCharts { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
        public virtual ICollection<hrJobInterview> hrJobInterviews { get; set; }
        public virtual ICollection<hrJobInterviewResults> hrJobInterviewResultss { get; set; }
        public virtual ICollection<hrJobTitleOrganizationChart> hrJobTitleOrganizationCharts { get; set; }
        public virtual ICollection<hrSGKEmployeeJobEndDeclaration> hrSGKEmployeeJobEndDeclarations { get; set; }
        public virtual ICollection<hrSGKEmployeeJobStartDeclaration> hrSGKEmployeeJobStartDeclarations { get; set; }
        public virtual ICollection<prBankPOSAccounts> prBankPOSAccountss { get; set; }
        public virtual ICollection<prCompanyCostCenter> prCompanyCostCenters { get; set; }
        public virtual ICollection<prCompanyExpense> prCompanyExpenses { get; set; }
        public virtual ICollection<prCompanyExpenseInvoiceConfirmationRule> prCompanyExpenseInvoiceConfirmationRules { get; set; }
        public virtual ICollection<prCompanyHierarchy> prCompanyHierarchys { get; set; }
        public virtual ICollection<prConfirmationRequiredProductGroups> prConfirmationRequiredProductGroupss { get; set; }
        public virtual ICollection<prCostCenterHierarchy> prCostCenterHierarchys { get; set; }
        public virtual ICollection<prCurrAccAvailableForeignCurrencyTrans> prCurrAccAvailableForeignCurrencyTranss { get; set; }
        public virtual ICollection<prCurrAccEInvoiceAlias> prCurrAccEInvoiceAliass { get; set; }
        public virtual ICollection<prCurrAccLotGrAtt> prCurrAccLotGrAtts { get; set; }
        public virtual ICollection<prCurrAccPersonalDataConfirmation> prCurrAccPersonalDataConfirmations { get; set; }
        public virtual ICollection<prCustomerConversation> prCustomerConversations { get; set; }
        public virtual ICollection<prCustomerDBSAccount> prCustomerDBSAccounts { get; set; }
        public virtual ICollection<prCustomerDiscountGrAtt> prCustomerDiscountGrAtts { get; set; }
        public virtual ICollection<prCustomerMarkupGrAtt> prCustomerMarkupGrAtts { get; set; }
        public virtual ICollection<prCustomerPaymentPlanGrAtt> prCustomerPaymentPlanGrAtts { get; set; }
        public virtual ICollection<prCustomerVerificationPassword> prCustomerVerificationPasswords { get; set; }
        public virtual ICollection<prEArchiveWebServiceCompany> prEArchiveWebServiceCompanys { get; set; }
        public virtual ICollection<prEArchiveWebServiceOffice> prEArchiveWebServiceOffices { get; set; }
        public virtual ICollection<prEInvoiceWebServiceCompany> prEInvoiceWebServiceCompanys { get; set; }
        public virtual ICollection<prEInvoiceWebServiceOffice> prEInvoiceWebServiceOffices { get; set; }
        public virtual ICollection<prExpenseInvoiceConfirmationRule> prExpenseInvoiceConfirmationRules { get; set; }
        public virtual ICollection<prGLAccAvailableForeignCurrencyTrans> prGLAccAvailableForeignCurrencyTranss { get; set; }
        public virtual ICollection<prImportFileExpense> prImportFileExpenses { get; set; }
        public virtual ICollection<prInnerProcessInfo> prInnerProcessInfos { get; set; }
        public virtual ICollection<prInnerProcessITAttribute> prInnerProcessITAttributes { get; set; }
        public virtual ICollection<prInnerProcessItemType> prInnerProcessItemTypes { get; set; }
        public virtual ICollection<prInteractiveSMSApplications> prInteractiveSMSApplicationss { get; set; }
        public virtual ICollection<prITAttributeTypeRequiredProcesses> prITAttributeTypeRequiredProcessess { get; set; }
        public virtual ICollection<prItemDiscountGrAtt> prItemDiscountGrAtts { get; set; }
        public virtual ICollection<prItemPaymentPlanGrAtt> prItemPaymentPlanGrAtts { get; set; }
        public virtual ICollection<prItemVendorGrAtt> prItemVendorGrAtts { get; set; }
        public virtual ICollection<prMarketPlaceCreditCardMappings> prMarketPlaceCreditCardMappingss { get; set; }
        public virtual ICollection<prMarketPlaceProduct> prMarketPlaceProducts { get; set; }
        public virtual ICollection<prMarketPlaceProductInformation> prMarketPlaceProductInformations { get; set; }
        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
        public virtual ICollection<prPersonalDataConfirmationFormTypeForCurrAccTypes> prPersonalDataConfirmationFormTypeForCurrAccTypess { get; set; }
        public virtual ICollection<prProcessATAttribute> prProcessATAttributes { get; set; }
        public virtual ICollection<prProcessDefaultExpenseType> prProcessDefaultExpenseTypes { get; set; }
        public virtual ICollection<prProcessDiscount> prProcessDiscounts { get; set; }
        public virtual ICollection<prProcessFlowRules> prProcessFlowRuless { get; set; }
        public virtual ICollection<prProcessFTAttribute> prProcessFTAttributes { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
        public virtual ICollection<prProcessITAttribute> prProcessITAttributes { get; set; }
        public virtual ICollection<prProcessItemType> prProcessItemTypes { get; set; }
        public virtual ICollection<prPurchasingAgentAvailableRequisition> prPurchasingAgentAvailableRequisitions { get; set; }
        public virtual ICollection<prRequisitionLimit> prRequisitionLimits { get; set; }
        public virtual ICollection<prTechnicalResponsibleAvailableRequisition> prTechnicalResponsibleAvailableRequisitions { get; set; }
        public virtual ICollection<prVendorPaymentPlanGrAtt> prVendorPaymentPlanGrAtts { get; set; }
        public virtual ICollection<rpOrderDeliveryAssignmentCollectedItems> rpOrderDeliveryAssignmentCollectedItemss { get; set; }
        public virtual ICollection<rpRegisteredEmailForPayrollSendStatus> rpRegisteredEmailForPayrollSendStatuss { get; set; }
        public virtual ICollection<srCodeNumberCheque> srCodeNumberCheques { get; set; }
        public virtual ICollection<srCodeNumberCurrAcc> srCodeNumberCurrAccs { get; set; }
        public virtual ICollection<srCodeNumberGiftCard> srCodeNumberGiftCards { get; set; }
        public virtual ICollection<srCodeNumberItem> srCodeNumberItems { get; set; }
        public virtual ICollection<srCodeNumberLetterOfGuarantee> srCodeNumberLetterOfGuarantees { get; set; }
        public virtual ICollection<srCodeNumberWarehouse> srCodeNumberWarehouses { get; set; }
        public virtual ICollection<srCustomerConversationFormNumber> srCustomerConversationFormNumbers { get; set; }
        public virtual ICollection<srDistanceSaleBankPaymentNumber> srDistanceSaleBankPaymentNumbers { get; set; }
        public virtual ICollection<srEArchiveSerialNumber> srEArchiveSerialNumbers { get; set; }
        public virtual ICollection<srEInvoiceSerialNumber> srEInvoiceSerialNumbers { get; set; }
        public virtual ICollection<srEShipmentSerialNumber> srEShipmentSerialNumbers { get; set; }
        public virtual ICollection<srExpenseInvoiceDocumentNumber> srExpenseInvoiceDocumentNumbers { get; set; }
        public virtual ICollection<srOnlineInstallmentBankPayment> srOnlineInstallmentBankPayments { get; set; }
        public virtual ICollection<srOpticalProtocolNumber> srOpticalProtocolNumbers { get; set; }
        public virtual ICollection<srPayrollDocumentNumber> srPayrollDocumentNumbers { get; set; }
        public virtual ICollection<srRefNumberAdjustCost> srRefNumberAdjustCosts { get; set; }
        public virtual ICollection<srRefNumberAgentReservation> srRefNumberAgentReservations { get; set; }
        public virtual ICollection<srRefNumberAllocation> srRefNumberAllocations { get; set; }
        public virtual ICollection<srRefNumberBadDebt> srRefNumberBadDebts { get; set; }
        public virtual ICollection<srRefNumberBankCredit> srRefNumberBankCredits { get; set; }
        public virtual ICollection<srRefNumberBankPaymentInstruction> srRefNumberBankPaymentInstructions { get; set; }
        public virtual ICollection<srRefNumberBankPaymentList> srRefNumberBankPaymentLists { get; set; }
        public virtual ICollection<srRefNumberBankTrans> srRefNumberBankTranss { get; set; }
        public virtual ICollection<srRefNumberCashTrans> srRefNumberCashTranss { get; set; }
        public virtual ICollection<srRefNumberChequeTrans> srRefNumberChequeTranss { get; set; }
        public virtual ICollection<srRefNumberConfirmationForm> srRefNumberConfirmationForms { get; set; }
        public virtual ICollection<srRefNumberContract> srRefNumberContracts { get; set; }
        public virtual ICollection<srRefNumberCreditCardPayment> srRefNumberCreditCardPayments { get; set; }
        public virtual ICollection<srRefNumberDebit> srRefNumberDebits { get; set; }
        public virtual ICollection<srRefNumberDepartmentReceipt> srRefNumberDepartmentReceipts { get; set; }
        public virtual ICollection<srRefNumberDeviceDocument> srRefNumberDeviceDocuments { get; set; }
        public virtual ICollection<srRefNumberExpenseAccrual> srRefNumberExpenseAccruals { get; set; }
        public virtual ICollection<srRefNumberExpenseSlip> srRefNumberExpenseSlips { get; set; }
        public virtual ICollection<srRefNumberGiftCardPayment> srRefNumberGiftCardPayments { get; set; }
        public virtual ICollection<srRefNumberIncentive> srRefNumberIncentives { get; set; }
        public virtual ICollection<srRefNumberInnerOrder> srRefNumberInnerOrders { get; set; }
        public virtual ICollection<srRefNumberInnerProcess> srRefNumberInnerProcesss { get; set; }
        public virtual ICollection<srRefNumberItemTest> srRefNumberItemTests { get; set; }
        public virtual ICollection<srRefNumberJournal> srRefNumberJournals { get; set; }
        public virtual ICollection<srRefNumberOtherPayment> srRefNumberOtherPayments { get; set; }
        public virtual ICollection<srRefNumberPayment> srRefNumberPayments { get; set; }
        public virtual ICollection<srRefNumberPriceList> srRefNumberPriceLists { get; set; }
        public virtual ICollection<srRefNumberProcessFlow> srRefNumberProcessFlows { get; set; }
        public virtual ICollection<srRefNumberPurchaseRequisition> srRefNumberPurchaseRequisitions { get; set; }
        public virtual ICollection<srRefNumberReportedSale> srRefNumberReportedSales { get; set; }
        public virtual ICollection<srRefNumberSalesPlan> srRefNumberSalesPlans { get; set; }
        public virtual ICollection<srRefNumberSupportRequest> srRefNumberSupportRequests { get; set; }
        public virtual ICollection<srRefNumberTaxIncurred> srRefNumberTaxIncurreds { get; set; }
        public virtual ICollection<srRefNumberTransferPlan> srRefNumberTransferPlans { get; set; }
        public virtual ICollection<srRefNumberVehicleLoading> srRefNumberVehicleLoadings { get; set; }
        public virtual ICollection<srRefNumberVehicleUnLoading> srRefNumberVehicleUnLoadings { get; set; }
        public virtual ICollection<srRefNumberVendorPriceList> srRefNumberVendorPriceLists { get; set; }
        public virtual ICollection<srRefNumberVirement> srRefNumberVirements { get; set; }
        public virtual ICollection<tpJournalZNum> tpJournalZNums { get; set; }
        public virtual ICollection<tpOrderCancelDetailHeader> tpOrderCancelDetailHeaders { get; set; }
        public virtual ICollection<tpOrderDeliveryDetail> tpOrderDeliveryDetails { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<tpSupportResolve> tpSupportResolves { get; set; }
        public virtual ICollection<trAdjustCostHeader> trAdjustCostHeaders { get; set; }
        public virtual ICollection<trAgentContractHeader> trAgentContractHeaders { get; set; }
        public virtual ICollection<trAgentPerformanceBonusHeader> trAgentPerformanceBonusHeaders { get; set; }
        public virtual ICollection<trAgentReservationHeader> trAgentReservationHeaders { get; set; }
        public virtual ICollection<trAllocation> trAllocations { get; set; }
        public virtual ICollection<trBadDebtTransHeader> trBadDebtTransHeaders { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
        public virtual ICollection<trBankHeader> trBankHeaders { get; set; }
        public virtual ICollection<trBankPaymentInstructionHeader> trBankPaymentInstructionHeaders { get; set; }
        public virtual ICollection<trBankPaymentListHeader> trBankPaymentListHeaders { get; set; }
        public virtual ICollection<trCashHeader> trCashHeaders { get; set; }
        public virtual ICollection<trChequeHeader> trChequeHeaders { get; set; }
        public virtual ICollection<trContract> trContracts { get; set; }
        public virtual ICollection<trCreditCardPaymentHeader> trCreditCardPaymentHeaders { get; set; }
        public virtual ICollection<trDebitHeader> trDebitHeaders { get; set; }
        public virtual ICollection<trDepartmentReceiptHeader> trDepartmentReceiptHeaders { get; set; }
        public virtual ICollection<trDispOrderHeader> trDispOrderHeaders { get; set; }
        public virtual ICollection<trGiftCardPaymentHeader> trGiftCardPaymentHeaders { get; set; }
        public virtual ICollection<trIncentiveHeader> trIncentiveHeaders { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trItemTestHeader> trItemTestHeaders { get; set; }
        public virtual ICollection<trJournalLedgerEntryNumber> trJournalLedgerEntryNumbers { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
        public virtual ICollection<trPaymentHeader> trPaymentHeaders { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
        public virtual ICollection<trPriceListHeader> trPriceListHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trPurchaseRequisitionHeader> trPurchaseRequisitionHeaders { get; set; }
        public virtual ICollection<trReportedSaleHeader> trReportedSaleHeaders { get; set; }
        public virtual ICollection<trReserveHeader> trReserveHeaders { get; set; }
        public virtual ICollection<trSalesPlan> trSalesPlans { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trSMSPoolHeader> trSMSPoolHeaders { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<trTaxIncurredHeader> trTaxIncurredHeaders { get; set; }
        public virtual ICollection<trTransferPlan> trTransferPlans { get; set; }
        public virtual ICollection<trVehicleLoadingHeader> trVehicleLoadingHeaders { get; set; }
        public virtual ICollection<trVehicleUnLoadingHeader> trVehicleUnLoadingHeaders { get; set; }
        public virtual ICollection<trVendorPriceListHeader> trVendorPriceListHeaders { get; set; }
        public virtual ICollection<trVirementHeader> trVirementHeaders { get; set; }
        public virtual ICollection<zpOnlineBankCreditCardPaymentTransaction> zpOnlineBankCreditCardPaymentTransactions { get; set; }
    }
}
