using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsProcess")]
    public partial class bsProcess
    {
        public bsProcess()
        {
            auProcessFlowDenys = new HashSet<auProcessFlowDeny>();
            auProcessPermits = new HashSet<auProcessPermit>();
            auProformaProcessPermits = new HashSet<auProformaProcessPermit>();
            bsProcessDescs = new HashSet<bsProcessDesc>();
            cdDiscountOffers = new HashSet<cdDiscountOffer>();
            dfCompanyProcessLockTransactions = new HashSet<dfCompanyProcessLockTransaction>();
            dfEArchiveOfficialForms = new HashSet<dfEArchiveOfficialForm>();
            dfEInvoiceOfficialForms = new HashSet<dfEInvoiceOfficialForm>();
            dfEShipmentOfficialForms = new HashSet<dfEShipmentOfficialForm>();
            dfOfficeNotAvailableProcesss = new HashSet<dfOfficeNotAvailableProcess>();
            dfOnlineDistributors = new HashSet<dfOnlineDistributor>();
            dfProcessOfficialForms = new HashSet<dfProcessOfficialForm>();
            prCreditCardTypeGLAccss = new HashSet<prCreditCardTypeGLAccs>();
            prCustomerDiscountGrAtts = new HashSet<prCustomerDiscountGrAtt>();
            prCustomerPaymentPlanGrAtts = new HashSet<prCustomerPaymentPlanGrAtt>();
            prDiscountTypeGLAccss = new HashSet<prDiscountTypeGLAccs>();
            prDOVGLAccss = new HashSet<prDOVGLAccs>();
            prImportFileGLAccss = new HashSet<prImportFileGLAccs>();
            prITAttributeTypeRequiredProcessess = new HashSet<prITAttributeTypeRequiredProcesses>();
            prItemAccountGrGLAccss = new HashSet<prItemAccountGrGLAccs>();
            prItemDiscountGrAtts = new HashSet<prItemDiscountGrAtt>();
            prItemPaymentPlanGrAtts = new HashSet<prItemPaymentPlanGrAtt>();
            prItemTaxGrAtts = new HashSet<prItemTaxGrAtt>();
            prPaymentProviderGLAccss = new HashSet<prPaymentProviderGLAccs>();
            prPCTGLAccss = new HashSet<prPCTGLAccs>();
            prPOSTerminalATAttributes = new HashSet<prPOSTerminalATAttribute>();
            prProcessATAttributes = new HashSet<prProcessATAttribute>();
            prProcessDefaultExpenseTypes = new HashSet<prProcessDefaultExpenseType>();
            prProcessDiscounts = new HashSet<prProcessDiscount>();
            prProcessFlowRuless = new HashSet<prProcessFlowRules>();
            prProcessFTAttributes = new HashSet<prProcessFTAttribute>();
            prProcessInfos = new HashSet<prProcessInfo>();
            prProcessITAttributes = new HashSet<prProcessITAttribute>();
            prProcessItemTypes = new HashSet<prProcessItemType>();
            prReturnReasonAvailableProcesss = new HashSet<prReturnReasonAvailableProcess>();
            prVatGLAccss = new HashSet<prVatGLAccs>();
            prVendorPaymentPlanGrAtts = new HashSet<prVendorPaymentPlanGrAtt>();
            prWarehouseProcessFlowRuless = new HashSet<prWarehouseProcessFlowRules>();
            rpOrderDeliveryAssignmentCollectedItemss = new HashSet<rpOrderDeliveryAssignmentCollectedItems>();
            srRefNumberProcessFlows = new HashSet<srRefNumberProcessFlow>();
            srRefNumberReportedSales = new HashSet<srRefNumberReportedSale>();
            trAdjustCostHeaders = new HashSet<trAdjustCostHeader>();
            trDispOrderHeaders = new HashSet<trDispOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trPickingHeaders = new HashSet<trPickingHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trReportedSaleHeaders = new HashSet<trReportedSaleHeader>();
            trReserveHeaders = new HashSet<trReserveHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trStocks = new HashSet<trStock>();
        }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public bool IsOutOfCosting { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsTransType bsTransType { get; set; }

        public virtual ICollection<auProcessFlowDeny> auProcessFlowDenys { get; set; }
        public virtual ICollection<auProcessPermit> auProcessPermits { get; set; }
        public virtual ICollection<auProformaProcessPermit> auProformaProcessPermits { get; set; }
        public virtual ICollection<bsProcessDesc> bsProcessDescs { get; set; }
        public virtual ICollection<cdDiscountOffer> cdDiscountOffers { get; set; }
        public virtual ICollection<dfCompanyProcessLockTransaction> dfCompanyProcessLockTransactions { get; set; }
        public virtual ICollection<dfEArchiveOfficialForm> dfEArchiveOfficialForms { get; set; }
        public virtual ICollection<dfEInvoiceOfficialForm> dfEInvoiceOfficialForms { get; set; }
        public virtual ICollection<dfEShipmentOfficialForm> dfEShipmentOfficialForms { get; set; }
        public virtual ICollection<dfOfficeNotAvailableProcess> dfOfficeNotAvailableProcesss { get; set; }
        public virtual ICollection<dfOnlineDistributor> dfOnlineDistributors { get; set; }
        public virtual ICollection<dfProcessOfficialForm> dfProcessOfficialForms { get; set; }
        public virtual ICollection<prCreditCardTypeGLAccs> prCreditCardTypeGLAccss { get; set; }
        public virtual ICollection<prCustomerDiscountGrAtt> prCustomerDiscountGrAtts { get; set; }
        public virtual ICollection<prCustomerPaymentPlanGrAtt> prCustomerPaymentPlanGrAtts { get; set; }
        public virtual ICollection<prDiscountTypeGLAccs> prDiscountTypeGLAccss { get; set; }
        public virtual ICollection<prDOVGLAccs> prDOVGLAccss { get; set; }
        public virtual ICollection<prImportFileGLAccs> prImportFileGLAccss { get; set; }
        public virtual ICollection<prITAttributeTypeRequiredProcesses> prITAttributeTypeRequiredProcessess { get; set; }
        public virtual ICollection<prItemAccountGrGLAccs> prItemAccountGrGLAccss { get; set; }
        public virtual ICollection<prItemDiscountGrAtt> prItemDiscountGrAtts { get; set; }
        public virtual ICollection<prItemPaymentPlanGrAtt> prItemPaymentPlanGrAtts { get; set; }
        public virtual ICollection<prItemTaxGrAtt> prItemTaxGrAtts { get; set; }
        public virtual ICollection<prPaymentProviderGLAccs> prPaymentProviderGLAccss { get; set; }
        public virtual ICollection<prPCTGLAccs> prPCTGLAccss { get; set; }
        public virtual ICollection<prPOSTerminalATAttribute> prPOSTerminalATAttributes { get; set; }
        public virtual ICollection<prProcessATAttribute> prProcessATAttributes { get; set; }
        public virtual ICollection<prProcessDefaultExpenseType> prProcessDefaultExpenseTypes { get; set; }
        public virtual ICollection<prProcessDiscount> prProcessDiscounts { get; set; }
        public virtual ICollection<prProcessFlowRules> prProcessFlowRuless { get; set; }
        public virtual ICollection<prProcessFTAttribute> prProcessFTAttributes { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
        public virtual ICollection<prProcessITAttribute> prProcessITAttributes { get; set; }
        public virtual ICollection<prProcessItemType> prProcessItemTypes { get; set; }
        public virtual ICollection<prReturnReasonAvailableProcess> prReturnReasonAvailableProcesss { get; set; }
        public virtual ICollection<prVatGLAccs> prVatGLAccss { get; set; }
        public virtual ICollection<prVendorPaymentPlanGrAtt> prVendorPaymentPlanGrAtts { get; set; }
        public virtual ICollection<prWarehouseProcessFlowRules> prWarehouseProcessFlowRuless { get; set; }
        public virtual ICollection<rpOrderDeliveryAssignmentCollectedItems> rpOrderDeliveryAssignmentCollectedItemss { get; set; }
        public virtual ICollection<srRefNumberProcessFlow> srRefNumberProcessFlows { get; set; }
        public virtual ICollection<srRefNumberReportedSale> srRefNumberReportedSales { get; set; }
        public virtual ICollection<trAdjustCostHeader> trAdjustCostHeaders { get; set; }
        public virtual ICollection<trDispOrderHeader> trDispOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trReportedSaleHeader> trReportedSaleHeaders { get; set; }
        public virtual ICollection<trReserveHeader> trReserveHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
    }
}
