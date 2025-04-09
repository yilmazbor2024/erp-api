using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCompanyDefault")]
    public partial class dfCompanyDefault
    {
        public dfCompanyDefault()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

       
 

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CompanyName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ShortName { get; set; }

        [Required]
        public bool UseGLPostingApproving { get; set; }

        [Required]
        public bool IsPermissionNegative { get; set; }

        [Required]
        public bool IsPermissionNegativeUserRoles { get; set; }

        [Required]
        public bool DeleteOff { get; set; }

        [Required]
        public bool KeepStoreVisitorDataHourly { get; set; }

        [Required]
        public bool AutoGenDocNumberForELedger { get; set; }

        [Required]
        public bool AutoGenWarehouseCode { get; set; }

        [Required]
        public bool AutoGenSectionCode { get; set; }

        [Required]
        public bool AutoGenChequeReceived { get; set; }

        [Required]
        public bool AutoGenNotesReceivable { get; set; }

        [Required]
        public bool AutoGenChequesGiven { get; set; }

        [Required]
        public bool AutoGenNotesPayable { get; set; }

        [Required]
        public bool AutoGenReceivedLOG { get; set; }

        [Required]
        public bool AutoGenGivenLOG { get; set; }

        [Required]
        public bool DisallowExitTransWithoutCompleted { get; set; }

        [Required]
        public byte GLAccCodeSize { get; set; }

        [Required]
        public byte GLAccBreakCount { get; set; }

        [Required]
        public byte GLAccBreakSize1 { get; set; }

        [Required]
        public byte GLAccBreakSize2 { get; set; }

        [Required]
        public byte GLAccBreakSize3 { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string GLAccBreakChar { get; set; }

        [Required]
        public bool UseQtyOnGL { get; set; }

        [Required]
        public bool UseGLPosting { get; set; }

        [Required]
        public bool MergeBankGlAccFromBankTrans { get; set; }

        [Required]
        public bool IsBankTransDirectlyPostToGL { get; set; }

        [Required]
        public bool IsCashTransDirectlyPostToGL { get; set; }

        [Required]
        public bool IsChequeTransDirectlyPostToGL { get; set; }

        [Required]
        public bool IsCreditDebitClosureDirectlyPostToGL { get; set; }

        [Required]
        public bool IsCreditCardPaymentDirectlyPostToGL { get; set; }

        [Required]
        public bool IsExpenseSlipDirectlyPostToGL { get; set; }

        [Required]
        public bool IsDebitNoteDirectlyPostToGL { get; set; }

        [Required]
        public bool IsCreditNoteDirectlyPostToGL { get; set; }

        [Required]
        public bool IsLetterOfGuaranteeDirectlyPostToGL { get; set; }

        [Required]
        public bool PostToGLWithTransactionDescription { get; set; }

        [Required]
        public bool IsOpenJournalAfterSave { get; set; }

        [Required]
        public bool ForceGLTypeBankTrans { get; set; }

        [Required]
        public bool ForceGLTypeCashTrans { get; set; }

        [Required]
        public bool ForceGLTypeCreditCardPaymentTrans { get; set; }

        [Required]
        public bool ForceGLTypeChequeTrans { get; set; }

        [Required]
        public bool ForceGLTypeDebitTrans { get; set; }

        [Required]
        public bool ForceGLTypeExpenseSlipTrans { get; set; }

        [Required]
        public bool ForceGLTypeExpenseAccrualTrans { get; set; }

        [Required]
        public bool IsIndividualAcc { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CommercialName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string MersisNum { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CommercialRegistrationNo { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMail { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Tel { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string MobileTel { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Fax { get; set; }

        [Required]
        public long AddressID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Address { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SiteName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BuildingName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BuildingNum { get; set; }

        [Required]
        public short FloorNum { get; set; }

        [Required]
        public short DoorNum { get; set; }

        [Required]
        public int QuarterCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string QuarterName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Boulevard { get; set; }

        [Required]
        public int StreetCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Street { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Road { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StateCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DistrictCode { get; set; }
 

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ZipCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DrivingDirections { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ScopeOfBusiness { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string JuralStructure { get; set; }

        [Required]
        public byte[] ReportLogoImage { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WebAddress { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SMSGatewayServiceCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EMailServiceCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string InstantSMSGatewayServiceCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ForeignInstantSMSGatewayServiceCode { get; set; }

        [Required]
        public bool UseForeignInstantSMS { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string OnlineReconciliationPortalAddress { get; set; }

        [Required]
        public bool OnlineReconciliationPortalWithoutSignIn { get; set; }

        [Required]
        public byte OnlineReconciliationLinkExpireDayCount { get; set; }

        [Required]
        public bool HaveLogisticsIntegration { get; set; }

        [Required]
        public bool HaveManufacturingIntegration { get; set; }

        [Required]
        public bool MessageSystemActive { get; set; }

        [Required]
        public bool ForceCheckOutReason { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string EInvoicePrefixCode { get; set; }

        [Required]
        public bool IsCompanySubjectToEInvoice { get; set; }

        [Required]
        public bool EInvoiceNumberGivenByIntegrator { get; set; }

        [Required]
        public bool IsCompanySubjectToELedger { get; set; }

        [Required]
        public DateTime ELedgerStartDate { get; set; }

        [Required]
        public bool UseELedgerIntegrator { get; set; }

        [Required]
        public bool IsCompanySubjectToEArchive { get; set; }

        [Required]
        public DateTime EArchiveStartDate { get; set; }

        [Required]
        public bool EArchiveNumberGivenByIntegrator { get; set; }

        [Required]
        public bool SendSMSByIntegrator { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EArchiveWebServiceCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EInvoiceWebServiceCode { get; set; }

        [Required]
        public bool IsCompanySubjectToEShipment { get; set; }

        [Required]
        public DateTime EShipmentStartDate { get; set; }

        [Required]
        public bool EShipmentNumberGivenByIntegrator { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EShipmentWebServiceCode { get; set; }

        [Required]
        public bool HasCustomJournalPeriod { get; set; }

        [Required]
        public byte CustomJournalPeriodDay { get; set; }

        [Required]
        public byte CustomJournalPeriodMonth { get; set; }

        [Required]
        public bool UseCustomerDataGatheringSurvey { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SurveyURL { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SurveyParameter { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemProcessPermitLevel { get; set; }

        [Required]
        public bool UsePersonalDataConfirmation { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormTypeCode { get; set; }

        [Required]
        public short LoggingLevel { get; set; }

        [Required]
        public bool PostRetailReceiptToGLGroupingByZNumber { get; set; }

        [Required]
        public byte BulkMailServiceProviderCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FinanceCompanyWebServiceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InactivationReasonCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TranslationProviderCode { get; set; }

        [Required]
        public DateTime ExportSalesEInvoiceStartDate { get; set; }

        [Required]
        public DateTime TaxFreeEInvoiceStartDate { get; set; }

        [Required]
        public bool IsCompanySubjectToUTS { get; set; }

        [Required]
        public DateTime UTSStartDate { get; set; }

        [Required]
        public long UTSKurumKodu { get; set; }

        [Required]
        public DateTime UTSTransitionDate { get; set; }

        [Required]
        public bool TransferChargesProcessSeparatelyOnBankGLAcc { get; set; }

        [Required]
        public decimal RoundingAmountTolerance { get; set; }

        [Required]
        public bool UseEInvoiceConfirmation { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PurchaseRequisitionConfirmationPortalAddress { get; set; }

        [Required]
        public byte PurchaseRequisitionConfirmationLinkExpireDayCount { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PurchaseRequisitionProposalConfirmationPortalAddress { get; set; }

        [Required]
        public byte PurchaseRequisitionProposalConfirmationLinkExpireDayCount { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string HRDepartmentCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OnlineBankWebServiceCode { get; set; }

        [Required]
        public bool UsePermissionMarketingIntegration { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PermissionMarketingServiceCode { get; set; }

        [Required]
        public bool UseMMSIntegration { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MMSBusinessPartnerCode { get; set; }

        [Required]
        public DateTime IYSIntegrationStartDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string AddressShareCompanyWebServiceCode { get; set; }

        [Required]
        public bool PermitEmployeeDataForWithHoldingAndSGK { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DistanceMatrixProviderCode { get; set; }

        [Required]
        public short EventLoggingLevelForNebimV3Services { get; set; }

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
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdEInvoiceWebService cdEInvoiceWebService { get; set; }
        public virtual cdConfirmationFormType cdConfirmationFormType { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdInactivationReason cdInactivationReason { get; set; }
        public virtual bsBulkMailServiceProvider bsBulkMailServiceProvider { get; set; }
        public virtual cdFinanceCompanyWebService cdFinanceCompanyWebService { get; set; }
        public virtual cdCity cdCity { get; set; }
        public virtual bsMMSBusinessPartner bsMMSBusinessPartner { get; set; }
        public virtual cdPermissionMarketingService cdPermissionMarketingService { get; set; }
        public virtual cdState cdState { get; set; }
        public virtual cdEShipmentWebService cdEShipmentWebService { get; set; }
        public virtual cdDistrict cdDistrict { get; set; }
        public virtual cdStreet cdStreet { get; set; }
        public virtual cdTaxOffice cdTaxOffice { get; set; }
        public virtual cdEArchiveWebService cdEArchiveWebService { get; set; }
        public virtual cdAddressShareCompanyWebService cdAddressShareCompanyWebService { get; set; }
        public virtual cdEMailService cdEMailService { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

    }
}
