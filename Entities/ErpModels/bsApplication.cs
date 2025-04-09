using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsApplication")]
    public partial class bsApplication
    {
        public bsApplication()
        {
            bsApplicationDescs = new HashSet<bsApplicationDesc>();
            dfCompanyLockTransactions = new HashSet<dfCompanyLockTransaction>();
            dfCompanyProcessLockTransactions = new HashSet<dfCompanyProcessLockTransaction>();
            dfTransactionDefFTAttributes = new HashSet<dfTransactionDefFTAttribute>();
            lgV3OfflinePOSSendStatusLogs = new HashSet<lgV3OfflinePOSSendStatusLog>();
            prCustomerConversations = new HashSet<prCustomerConversation>();
            tpJournalIntegrationStatuss = new HashSet<tpJournalIntegrationStatus>();
            tpOrderDeliveryDetails = new HashSet<tpOrderDeliveryDetail>();
            tpPaymentRegisterInfos = new HashSet<tpPaymentRegisterInfo>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            trAllocations = new HashSet<trAllocation>();
            trBadDebtTransAddExpenseDebitss = new HashSet<trBadDebtTransAddExpenseDebits>();
            trBadDebtTransHeaders = new HashSet<trBadDebtTransHeader>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
            trBankHeaders = new HashSet<trBankHeader>();
            trBankPaymentInstructionHeaders = new HashSet<trBankPaymentInstructionHeader>();
            trBankPaymentListHeaders = new HashSet<trBankPaymentListHeader>();
            trCashHeaders = new HashSet<trCashHeader>();
            trChequeHeaders = new HashSet<trChequeHeader>();
            trContracts = new HashSet<trContract>();
            trCreditCardPaymentHeaders = new HashSet<trCreditCardPaymentHeader>();
            trCurrAccBooks = new HashSet<trCurrAccBook>();
            trDebitHeaders = new HashSet<trDebitHeader>();
            trDepartmentReceiptHeaders = new HashSet<trDepartmentReceiptHeader>();
            trDispOrderHeaders = new HashSet<trDispOrderHeader>();
            trEmployeeDebits = new HashSet<trEmployeeDebit>();
            trExpenseAccrualHeaders = new HashSet<trExpenseAccrualHeader>();
            trExpenseSlipHeaders = new HashSet<trExpenseSlipHeader>();
            trGiftCardPaymentHeaders = new HashSet<trGiftCardPaymentHeader>();
            trIncentiveHeaders = new HashSet<trIncentiveHeader>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trItemTestHeaders = new HashSet<trItemTestHeader>();
            trJournalHeaders = new HashSet<trJournalHeader>();
            trOrderAdvancePaymentss = new HashSet<trOrderAdvancePayments>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
            trPaymentHeaders = new HashSet<trPaymentHeader>();
            trPayrollHeaders = new HashSet<trPayrollHeader>();
            trPickingHeaders = new HashSet<trPickingHeader>();
            trPriceListHeaders = new HashSet<trPriceListHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trPurchaseRequisitionHeaders = new HashSet<trPurchaseRequisitionHeader>();
            trReportedSaleHeaders = new HashSet<trReportedSaleHeader>();
            trReserveHeaders = new HashSet<trReserveHeader>();
            trSalesPlans = new HashSet<trSalesPlan>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trSMSPoolLines = new HashSet<trSMSPoolLine>();
            trStocks = new HashSet<trStock>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            trSurveyAnswerHeaders = new HashSet<trSurveyAnswerHeader>();
            trTransferPlans = new HashSet<trTransferPlan>();
            trVehicleLoadingHeaders = new HashSet<trVehicleLoadingHeader>();
            trVehicleUnLoadingHeaders = new HashSet<trVehicleUnLoadingHeader>();
            trVendorPriceListHeaders = new HashSet<trVendorPriceListHeader>();
            trVirementHeaders = new HashSet<trVirementHeader>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Required]
        public bool LockTransaction { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsApplicationDesc> bsApplicationDescs { get; set; }
        public virtual ICollection<dfCompanyLockTransaction> dfCompanyLockTransactions { get; set; }
        public virtual ICollection<dfCompanyProcessLockTransaction> dfCompanyProcessLockTransactions { get; set; }
        public virtual ICollection<dfTransactionDefFTAttribute> dfTransactionDefFTAttributes { get; set; }
        public virtual ICollection<lgV3OfflinePOSSendStatusLog> lgV3OfflinePOSSendStatusLogs { get; set; }
        public virtual ICollection<prCustomerConversation> prCustomerConversations { get; set; }
        public virtual ICollection<tpJournalIntegrationStatus> tpJournalIntegrationStatuss { get; set; }
        public virtual ICollection<tpOrderDeliveryDetail> tpOrderDeliveryDetails { get; set; }
        public virtual ICollection<tpPaymentRegisterInfo> tpPaymentRegisterInfos { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<trAllocation> trAllocations { get; set; }
        public virtual ICollection<trBadDebtTransAddExpenseDebits> trBadDebtTransAddExpenseDebitss { get; set; }
        public virtual ICollection<trBadDebtTransHeader> trBadDebtTransHeaders { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
        public virtual ICollection<trBankHeader> trBankHeaders { get; set; }
        public virtual ICollection<trBankPaymentInstructionHeader> trBankPaymentInstructionHeaders { get; set; }
        public virtual ICollection<trBankPaymentListHeader> trBankPaymentListHeaders { get; set; }
        public virtual ICollection<trCashHeader> trCashHeaders { get; set; }
        public virtual ICollection<trChequeHeader> trChequeHeaders { get; set; }
        public virtual ICollection<trContract> trContracts { get; set; }
        public virtual ICollection<trCreditCardPaymentHeader> trCreditCardPaymentHeaders { get; set; }
        public virtual ICollection<trCurrAccBook> trCurrAccBooks { get; set; }
        public virtual ICollection<trDebitHeader> trDebitHeaders { get; set; }
        public virtual ICollection<trDepartmentReceiptHeader> trDepartmentReceiptHeaders { get; set; }
        public virtual ICollection<trDispOrderHeader> trDispOrderHeaders { get; set; }
        public virtual ICollection<trEmployeeDebit> trEmployeeDebits { get; set; }
        public virtual ICollection<trExpenseAccrualHeader> trExpenseAccrualHeaders { get; set; }
        public virtual ICollection<trExpenseSlipHeader> trExpenseSlipHeaders { get; set; }
        public virtual ICollection<trGiftCardPaymentHeader> trGiftCardPaymentHeaders { get; set; }
        public virtual ICollection<trIncentiveHeader> trIncentiveHeaders { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trItemTestHeader> trItemTestHeaders { get; set; }
        public virtual ICollection<trJournalHeader> trJournalHeaders { get; set; }
        public virtual ICollection<trOrderAdvancePayments> trOrderAdvancePaymentss { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
        public virtual ICollection<trPaymentHeader> trPaymentHeaders { get; set; }
        public virtual ICollection<trPayrollHeader> trPayrollHeaders { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
        public virtual ICollection<trPriceListHeader> trPriceListHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trPurchaseRequisitionHeader> trPurchaseRequisitionHeaders { get; set; }
        public virtual ICollection<trReportedSaleHeader> trReportedSaleHeaders { get; set; }
        public virtual ICollection<trReserveHeader> trReserveHeaders { get; set; }
        public virtual ICollection<trSalesPlan> trSalesPlans { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trSMSPoolLine> trSMSPoolLines { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<trSurveyAnswerHeader> trSurveyAnswerHeaders { get; set; }
        public virtual ICollection<trTransferPlan> trTransferPlans { get; set; }
        public virtual ICollection<trVehicleLoadingHeader> trVehicleLoadingHeaders { get; set; }
        public virtual ICollection<trVehicleUnLoadingHeader> trVehicleUnLoadingHeaders { get; set; }
        public virtual ICollection<trVendorPriceListHeader> trVendorPriceListHeaders { get; set; }
        public virtual ICollection<trVirementHeader> trVirementHeaders { get; set; }
    }
}
