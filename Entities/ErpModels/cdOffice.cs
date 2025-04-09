using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdOffice")]
    public partial class cdOffice
    {
        public cdOffice()
        {
            auOptInOptOutTraces = new HashSet<auOptInOptOutTrace>();
            cdCompanyCreditCards = new HashSet<cdCompanyCreditCard>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdExportFiles = new HashSet<cdExportFile>();
            cdImportFiles = new HashSet<cdImportFile>();
            cdJobPositions = new HashSet<cdJobPosition>();
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            cdOfficeDescs = new HashSet<cdOfficeDesc>();
            cdRoundsmans = new HashSet<cdRoundsman>();
            cdSalespersons = new HashSet<cdSalesperson>();
            cdServicemans = new HashSet<cdServiceman>();
            cdWarehouses = new HashSet<cdWarehouse>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            dfBankCreditOfficialForms = new HashSet<dfBankCreditOfficialForm>();
            dfBankOfficialForms = new HashSet<dfBankOfficialForm>();
            dfBankPaymentInstructionOfficialForms = new HashSet<dfBankPaymentInstructionOfficialForm>();
            dfBankPaymentListOfficialForms = new HashSet<dfBankPaymentListOfficialForm>();
            dfBulutTahsilatVPosOffices = new HashSet<dfBulutTahsilatVPosOffice>();
            dfCashOfficialForms = new HashSet<dfCashOfficialForm>();
            dfChequeOfficialForms = new HashSet<dfChequeOfficialForm>();
            dfCommunicationForms = new HashSet<dfCommunicationForm>();
            dfCreditCardPaymentOfficialForms = new HashSet<dfCreditCardPaymentOfficialForm>();
            dfDebitOfficialForms = new HashSet<dfDebitOfficialForm>();
            dfDepartmentReceiptOfficialForms = new HashSet<dfDepartmentReceiptOfficialForm>();
            dfEArchiveOfficialForms = new HashSet<dfEArchiveOfficialForm>();
            dfEInvoiceOfficialForms = new HashSet<dfEInvoiceOfficialForm>();
            dfEShipmentOfficialForms = new HashSet<dfEShipmentOfficialForm>();
            dfExpenseSlipForms = new HashSet<dfExpenseSlipForm>();
            dfGetirCarsiStores = new HashSet<dfGetirCarsiStore>();
            dfInnerOrderProcessOfficialForms = new HashSet<dfInnerOrderProcessOfficialForm>();
            dfInnerProcessOfficialForms = new HashSet<dfInnerProcessOfficialForm>();
            dfItemTestOfficialForms = new HashSet<dfItemTestOfficialForm>();
            dfJournalOfficialForms = new HashSet<dfJournalOfficialForm>();
            dfMobilRevenueUserSalesPoints = new HashSet<dfMobilRevenueUserSalesPoint>();
            dfMonthlyTurnoverTargets = new HashSet<dfMonthlyTurnoverTarget>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            dfOfficeNotAvailableProcesss = new HashSet<dfOfficeNotAvailableProcess>();
            dfOnlineSalesAndPaymentParametersForConnections = new HashSet<dfOnlineSalesAndPaymentParametersForConnection>();
            dfOtherPaymentOfficialForms = new HashSet<dfOtherPaymentOfficialForm>();
            dfPaymentOfficialForms = new HashSet<dfPaymentOfficialForm>();
            dfPriceListForms = new HashSet<dfPriceListForm>();
            dfProcessOfficialForms = new HashSet<dfProcessOfficialForm>();
            dfProductsForOfficeBasedSerialNumberTrackings = new HashSet<dfProductsForOfficeBasedSerialNumberTracking>();
            dfPurchaseRequisitionOfficialForms = new HashSet<dfPurchaseRequisitionOfficialForm>();
            dfPurchaseRequisitionProposalOfficialForms = new HashSet<dfPurchaseRequisitionProposalOfficialForm>();
            dfRomaniaGoosfrabaeInvoiceOffices = new HashSet<dfRomaniaGoosfrabaeInvoiceOffice>();
            dfRomaniaGoosfrabaeShipmentOffices = new HashSet<dfRomaniaGoosfrabaeShipmentOffice>();
            dfSupportRequestOfficialForms = new HashSet<dfSupportRequestOfficialForm>();
            dfUserAllowedOffices = new HashSet<dfUserAllowedOffice>();
            dfUserPositions = new HashSet<dfUserPosition>();
            dfUTSWebServiceTokens = new HashSet<dfUTSWebServiceToken>();
            dfVehicleLoadingOfficialForms = new HashSet<dfVehicleLoadingOfficialForm>();
            dfVehicleUnLoadingOfficialForms = new HashSet<dfVehicleUnLoadingOfficialForm>();
            dfVendorPriceListForms = new HashSet<dfVendorPriceListForm>();
            dfVirementOfficialForms = new HashSet<dfVirementOfficialForm>();
            prBankAdditionalChargeTypeGLAccss = new HashSet<prBankAdditionalChargeTypeGLAccs>();
            prChequeGLAccss = new HashSet<prChequeGLAccs>();
            prCreditCardTypeGLAccss = new HashSet<prCreditCardTypeGLAccs>();
            prCurrAccPersonalDataConfirmations = new HashSet<prCurrAccPersonalDataConfirmation>();
            prCustomerConversations = new HashSet<prCustomerConversation>();
            prCustomerVerificationPasswords = new HashSet<prCustomerVerificationPassword>();
            prDiscountTypeGLAccss = new HashSet<prDiscountTypeGLAccs>();
            prDOVGLAccss = new HashSet<prDOVGLAccs>();
            prEArchiveWebServiceOffices = new HashSet<prEArchiveWebServiceOffice>();
            prEInvoiceWebServiceOffices = new HashSet<prEInvoiceWebServiceOffice>();
            prExpenseInvoiceConfirmationRules = new HashSet<prExpenseInvoiceConfirmationRule>();
            prItemAccountGrGLAccss = new HashSet<prItemAccountGrGLAccs>();
            prNotesGLAccss = new HashSet<prNotesGLAccs>();
            prOfficeCOGSGrAtts = new HashSet<prOfficeCOGSGrAtt>();
            prOfficeGLAccss = new HashSet<prOfficeGLAccs>();
            prOfficeMapLocations = new HashSet<prOfficeMapLocation>();
            prPaymentProviderGLAccss = new HashSet<prPaymentProviderGLAccs>();
            prPCTGLAccss = new HashSet<prPCTGLAccs>();
            prVatGLAccss = new HashSet<prVatGLAccs>();
            rpOrderDeliveryAssignmentCollectedItemss = new HashSet<rpOrderDeliveryAssignmentCollectedItems>();
            srCashSerialNumbers = new HashSet<srCashSerialNumber>();
            srChequesSerialNumbers = new HashSet<srChequesSerialNumber>();
            srEArchiveSerialNumbers = new HashSet<srEArchiveSerialNumber>();
            srRefNumberDiscountVouchers = new HashSet<srRefNumberDiscountVoucher>();
            srSerialNumbers = new HashSet<srSerialNumber>();
            tpJournalZNums = new HashSet<tpJournalZNum>();
            tpOrderCancelDetailHeaders = new HashSet<tpOrderCancelDetailHeader>();
            tpOrderDeliveryDetails = new HashSet<tpOrderDeliveryDetail>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            tpSupportResolves = new HashSet<tpSupportResolve>();
            trAdjustCostHeaders = new HashSet<trAdjustCostHeader>();
            trAgentReservationHeaders = new HashSet<trAgentReservationHeader>();
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
            trExpenseAccrualHeaders = new HashSet<trExpenseAccrualHeader>();
            trExpenseSlipHeaders = new HashSet<trExpenseSlipHeader>();
            trGiftCardPaymentHeaders = new HashSet<trGiftCardPaymentHeader>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trItemTestHeaders = new HashSet<trItemTestHeader>();
            trJournalHeaders = new HashSet<trJournalHeader>();
            trJournalInflationAdjustmentHeaders = new HashSet<trJournalInflationAdjustmentHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
            trPaymentHeaders = new HashSet<trPaymentHeader>();
            trPickingHeaders = new HashSet<trPickingHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trPurchaseRequisitionHeaders = new HashSet<trPurchaseRequisitionHeader>();
            trPurchaseRequisitionLines = new HashSet<trPurchaseRequisitionLine>();
            trReserveHeaders = new HashSet<trReserveHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trSMSPoolHeaders = new HashSet<trSMSPoolHeader>();
            trStocks = new HashSet<trStock>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            trTaxIncurredHeaders = new HashSet<trTaxIncurredHeader>();
            trVehicleLoadingHeaders = new HashSet<trVehicleLoadingHeader>();
            trVehicleUnLoadingHeaders = new HashSet<trVehicleUnLoadingHeader>();
            trVirementHeaders = new HashSet<trVirementHeader>();
            zpOnlineBankCreditCardPaymentTransactions = new HashSet<zpOnlineBankCreditCardPaymentTransaction>();
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public bool IsExecutiveOffice { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<auOptInOptOutTrace> auOptInOptOutTraces { get; set; }
        public virtual ICollection<cdCompanyCreditCard> cdCompanyCreditCards { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdExportFile> cdExportFiles { get; set; }
        public virtual ICollection<cdImportFile> cdImportFiles { get; set; }
        public virtual ICollection<cdJobPosition> cdJobPositions { get; set; }
        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<cdOfficeDesc> cdOfficeDescs { get; set; }
        public virtual ICollection<cdRoundsman> cdRoundsmans { get; set; }
        public virtual ICollection<cdSalesperson> cdSalespersons { get; set; }
        public virtual ICollection<cdServiceman> cdServicemans { get; set; }
        public virtual ICollection<cdWarehouse> cdWarehouses { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<dfBankCreditOfficialForm> dfBankCreditOfficialForms { get; set; }
        public virtual ICollection<dfBankOfficialForm> dfBankOfficialForms { get; set; }
        public virtual ICollection<dfBankPaymentInstructionOfficialForm> dfBankPaymentInstructionOfficialForms { get; set; }
        public virtual ICollection<dfBankPaymentListOfficialForm> dfBankPaymentListOfficialForms { get; set; }
        public virtual ICollection<dfBulutTahsilatVPosOffice> dfBulutTahsilatVPosOffices { get; set; }
        public virtual ICollection<dfCashOfficialForm> dfCashOfficialForms { get; set; }
        public virtual ICollection<dfChequeOfficialForm> dfChequeOfficialForms { get; set; }
        public virtual ICollection<dfCommunicationForm> dfCommunicationForms { get; set; }
        public virtual ICollection<dfCreditCardPaymentOfficialForm> dfCreditCardPaymentOfficialForms { get; set; }
        public virtual ICollection<dfDebitOfficialForm> dfDebitOfficialForms { get; set; }
        public virtual ICollection<dfDepartmentReceiptOfficialForm> dfDepartmentReceiptOfficialForms { get; set; }
        public virtual ICollection<dfEArchiveOfficialForm> dfEArchiveOfficialForms { get; set; }
        public virtual ICollection<dfEInvoiceOfficialForm> dfEInvoiceOfficialForms { get; set; }
        public virtual ICollection<dfEShipmentOfficialForm> dfEShipmentOfficialForms { get; set; }
        public virtual ICollection<dfExpenseSlipForm> dfExpenseSlipForms { get; set; }
        public virtual ICollection<dfGetirCarsiStore> dfGetirCarsiStores { get; set; }
        public virtual ICollection<dfInnerOrderProcessOfficialForm> dfInnerOrderProcessOfficialForms { get; set; }
        public virtual ICollection<dfInnerProcessOfficialForm> dfInnerProcessOfficialForms { get; set; }
        public virtual ICollection<dfItemTestOfficialForm> dfItemTestOfficialForms { get; set; }
        public virtual ICollection<dfJournalOfficialForm> dfJournalOfficialForms { get; set; }
        public virtual ICollection<dfMobilRevenueUserSalesPoint> dfMobilRevenueUserSalesPoints { get; set; }
        public virtual ICollection<dfMonthlyTurnoverTarget> dfMonthlyTurnoverTargets { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<dfOfficeNotAvailableProcess> dfOfficeNotAvailableProcesss { get; set; }
        public virtual ICollection<dfOnlineSalesAndPaymentParametersForConnection> dfOnlineSalesAndPaymentParametersForConnections { get; set; }
        public virtual ICollection<dfOtherPaymentOfficialForm> dfOtherPaymentOfficialForms { get; set; }
        public virtual ICollection<dfPaymentOfficialForm> dfPaymentOfficialForms { get; set; }
        public virtual ICollection<dfPriceListForm> dfPriceListForms { get; set; }
        public virtual ICollection<dfProcessOfficialForm> dfProcessOfficialForms { get; set; }
        public virtual ICollection<dfProductsForOfficeBasedSerialNumberTracking> dfProductsForOfficeBasedSerialNumberTrackings { get; set; }
        public virtual ICollection<dfPurchaseRequisitionOfficialForm> dfPurchaseRequisitionOfficialForms { get; set; }
        public virtual ICollection<dfPurchaseRequisitionProposalOfficialForm> dfPurchaseRequisitionProposalOfficialForms { get; set; }
        public virtual ICollection<dfRomaniaGoosfrabaeInvoiceOffice> dfRomaniaGoosfrabaeInvoiceOffices { get; set; }
        public virtual ICollection<dfRomaniaGoosfrabaeShipmentOffice> dfRomaniaGoosfrabaeShipmentOffices { get; set; }
        public virtual ICollection<dfSupportRequestOfficialForm> dfSupportRequestOfficialForms { get; set; }
        public virtual ICollection<dfUserAllowedOffice> dfUserAllowedOffices { get; set; }
        public virtual ICollection<dfUserPosition> dfUserPositions { get; set; }
        public virtual ICollection<dfUTSWebServiceToken> dfUTSWebServiceTokens { get; set; }
        public virtual ICollection<dfVehicleLoadingOfficialForm> dfVehicleLoadingOfficialForms { get; set; }
        public virtual ICollection<dfVehicleUnLoadingOfficialForm> dfVehicleUnLoadingOfficialForms { get; set; }
        public virtual ICollection<dfVendorPriceListForm> dfVendorPriceListForms { get; set; }
        public virtual ICollection<dfVirementOfficialForm> dfVirementOfficialForms { get; set; }
        public virtual ICollection<prBankAdditionalChargeTypeGLAccs> prBankAdditionalChargeTypeGLAccss { get; set; }
        public virtual ICollection<prChequeGLAccs> prChequeGLAccss { get; set; }
        public virtual ICollection<prCreditCardTypeGLAccs> prCreditCardTypeGLAccss { get; set; }
        public virtual ICollection<prCurrAccPersonalDataConfirmation> prCurrAccPersonalDataConfirmations { get; set; }
        public virtual ICollection<prCustomerConversation> prCustomerConversations { get; set; }
        public virtual ICollection<prCustomerVerificationPassword> prCustomerVerificationPasswords { get; set; }
        public virtual ICollection<prDiscountTypeGLAccs> prDiscountTypeGLAccss { get; set; }
        public virtual ICollection<prDOVGLAccs> prDOVGLAccss { get; set; }
        public virtual ICollection<prEArchiveWebServiceOffice> prEArchiveWebServiceOffices { get; set; }
        public virtual ICollection<prEInvoiceWebServiceOffice> prEInvoiceWebServiceOffices { get; set; }
        public virtual ICollection<prExpenseInvoiceConfirmationRule> prExpenseInvoiceConfirmationRules { get; set; }
        public virtual ICollection<prItemAccountGrGLAccs> prItemAccountGrGLAccss { get; set; }
        public virtual ICollection<prNotesGLAccs> prNotesGLAccss { get; set; }
        public virtual ICollection<prOfficeCOGSGrAtt> prOfficeCOGSGrAtts { get; set; }
        public virtual ICollection<prOfficeGLAccs> prOfficeGLAccss { get; set; }
        public virtual ICollection<prOfficeMapLocation> prOfficeMapLocations { get; set; }
        public virtual ICollection<prPaymentProviderGLAccs> prPaymentProviderGLAccss { get; set; }
        public virtual ICollection<prPCTGLAccs> prPCTGLAccss { get; set; }
        public virtual ICollection<prVatGLAccs> prVatGLAccss { get; set; }
        public virtual ICollection<rpOrderDeliveryAssignmentCollectedItems> rpOrderDeliveryAssignmentCollectedItemss { get; set; }
        public virtual ICollection<srCashSerialNumber> srCashSerialNumbers { get; set; }
        public virtual ICollection<srChequesSerialNumber> srChequesSerialNumbers { get; set; }
        public virtual ICollection<srEArchiveSerialNumber> srEArchiveSerialNumbers { get; set; }
        public virtual ICollection<srRefNumberDiscountVoucher> srRefNumberDiscountVouchers { get; set; }
        public virtual ICollection<srSerialNumber> srSerialNumbers { get; set; }
        public virtual ICollection<tpJournalZNum> tpJournalZNums { get; set; }
        public virtual ICollection<tpOrderCancelDetailHeader> tpOrderCancelDetailHeaders { get; set; }
        public virtual ICollection<tpOrderDeliveryDetail> tpOrderDeliveryDetails { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<tpSupportResolve> tpSupportResolves { get; set; }
        public virtual ICollection<trAdjustCostHeader> trAdjustCostHeaders { get; set; }
        public virtual ICollection<trAgentReservationHeader> trAgentReservationHeaders { get; set; }
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
        public virtual ICollection<trExpenseAccrualHeader> trExpenseAccrualHeaders { get; set; }
        public virtual ICollection<trExpenseSlipHeader> trExpenseSlipHeaders { get; set; }
        public virtual ICollection<trGiftCardPaymentHeader> trGiftCardPaymentHeaders { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trItemTestHeader> trItemTestHeaders { get; set; }
        public virtual ICollection<trJournalHeader> trJournalHeaders { get; set; }
        public virtual ICollection<trJournalInflationAdjustmentHeader> trJournalInflationAdjustmentHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
        public virtual ICollection<trPaymentHeader> trPaymentHeaders { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trPurchaseRequisitionHeader> trPurchaseRequisitionHeaders { get; set; }
        public virtual ICollection<trPurchaseRequisitionLine> trPurchaseRequisitionLines { get; set; }
        public virtual ICollection<trReserveHeader> trReserveHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trSMSPoolHeader> trSMSPoolHeaders { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<trTaxIncurredHeader> trTaxIncurredHeaders { get; set; }
        public virtual ICollection<trVehicleLoadingHeader> trVehicleLoadingHeaders { get; set; }
        public virtual ICollection<trVehicleUnLoadingHeader> trVehicleUnLoadingHeaders { get; set; }
        public virtual ICollection<trVirementHeader> trVirementHeaders { get; set; }
        public virtual ICollection<zpOnlineBankCreditCardPaymentTransaction> zpOnlineBankCreditCardPaymentTransactions { get; set; }
    }
}
