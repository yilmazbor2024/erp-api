using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfStoreDefault")]
    public partial class dfStoreDefault
    {
        public dfStoreDefault()
        {
            dfPosNewCustomers = new HashSet<dfPosNewCustomer>();
            dfPosNewCustomerActionss = new HashSet<dfPosNewCustomerActions>();
            dfPosNewCustomerFields = new HashSet<dfPosNewCustomerField>();
            dfPosOrderOpticalProductFields = new HashSet<dfPosOrderOpticalProductField>();
            dfPosUIs = new HashSet<dfPosUI>();
            dfPosUISettings = new HashSet<dfPosUISetting>();
            dfStoreConsStores = new HashSet<dfStoreConsStore>();
            dfStoreDeliveryWarehouses = new HashSet<dfStoreDeliveryWarehouse>();
            dfStoreDigitalMarketingServices = new HashSet<dfStoreDigitalMarketingService>();
            dfStoreDistributionWarehouses = new HashSet<dfStoreDistributionWarehouse>();
            dfStoreFolders = new HashSet<dfStoreFolder>();
            dfStoreForeignCurrencys = new HashSet<dfStoreForeignCurrency>();
            dfStoreProductInformations = new HashSet<dfStoreProductInformation>();
            dfStoreSupportWarehouses = new HashSet<dfStoreSupportWarehouse>();
            dfStoreTotalDiscountAuthoritys = new HashSet<dfStoreTotalDiscountAuthority>();
            dfStoreTransferStores = new HashSet<dfStoreTransferStore>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCurrAccCode { get; set; }

        [Required]
        public decimal StoreCodeNumber { get; set; }

        [Required]
        public bool DeleteOff { get; set; }

        [Required]
        public bool UseTransferApproving { get; set; }

        [Required]
        public bool AskOpDateOnTransferApproving { get; set; }

        [Required]
        public bool FilterPosTerminalOnReprint { get; set; }

        [Required]
        public short ReprintsReceiptLastNDay { get; set; }

        [Required]
        public short AcceptReturnLastNDay { get; set; }

        [Required]
        public bool AcceptReturnWithoutSales { get; set; }

        [Required]
        public short DayRangeWarnCustomerBirthday { get; set; }

        [Required]
        public short DayRangeWarnCustomerMarriedDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DefRetailCustomerCode { get; set; }

        [Required]
        public bool UseAlwaysDefRetailCustomer { get; set; }

        [Required]
        public bool IsDefaultCurrentStoreCustomersForFind { get; set; }

        [Required]
        public bool DisplayCustomerPhotoOnPos { get; set; }

        [Required]
        public bool DisplayProductPhotoOnPos { get; set; }

        [Required]
        public bool UseBarcodeForInvoiceSerialNumberOnPos { get; set; }

        [Required]
        public bool IsTransDirectlyPostToGL { get; set; }

        [Required]
        public bool UseSeriesCode { get; set; }

        [Required]
        public bool CanSearchFromItemOnReturnProcess { get; set; }

        [Required]
        public bool RequiredSalesPerson { get; set; }

        [Required]
        public bool GetSalesPersonOnSalesForReturns { get; set; }

        [Required]
        public bool GetSalesPersonOnSalesForReplacement { get; set; }

        [Required]
        public bool AskSalesPersonForEachLine { get; set; }

        [Required]
        public float MinRetailDepositRate { get; set; }

        [Required]
        public bool DisplayEmployeePersonelInfoOnPos { get; set; }

        [Required]
        public bool UsePresendCardForCustomerVerification { get; set; }

        [Required]
        public bool ReadCustomerPresendCardWithSecure { get; set; }

        [Required]
        public bool UseSMSForCustomerVerification { get; set; }

        [Required]
        public bool IsDefaultCustomerPresendCardForFind { get; set; }

        [Required]
        public bool UseCustomerPresendCardActivation { get; set; }

        [Required]
        public byte PresentCardActivationTypeCode { get; set; }

        [Required]
        public bool ForceCustomerVerificationForInstallmentSale { get; set; }

        [Required]
        public bool IsValidDiscountWithCustomerVerification { get; set; }

        [Required]
        public int CreditVoucherAvailableDay { get; set; }

        [Required]
        public bool ForceDiscountReason { get; set; }

        [Required]
        public bool PermitSupportRequestWithoutSales { get; set; }

        [Required]
        public bool DontCreateReturnReceiptOnApproving { get; set; }

        [Required]
        public bool IgnoreTurkishCharactersForCustomerQuery { get; set; }

        [Required]
        public bool DisableDiscountOffersOnReplacement { get; set; }

        [Required]
        public bool IsWebStore { get; set; }

        [Required]
        public bool CommunicationConfirmationRequired { get; set; }

        [Required]
        public bool CanOnlyTakePaymentWithOwnBank { get; set; }

        [Required]
        public bool ShowDiscountPointAndVouchersOnPayment { get; set; }

        [Required]
        public bool ForceCustomerVerificationForReturnTransactions { get; set; }

        [Required]
        public bool ForceCustomerVerificationForSupportRequest { get; set; }

        [Required]
        public bool UsePasswordForCustomerVerification { get; set; }

        [Required]
        public bool StoreUserCanEditExchangeRate { get; set; }

        [Required]
        public bool UseLimitedTaxTypeOnStore { get; set; }

        [Required]
        public short AcceptChangeItemLastNDay { get; set; }

        [Required]
        public bool HideVIPCustomerCommInfo { get; set; }

        [Required]
        public short AcceptReturnsWithSupportRequestLastNDay { get; set; }

        [Required]
        public bool ShowCustomerDiscountOffersOnPos { get; set; }

        [Required]
        public bool CheckMaxCashReturnDailyLimit { get; set; }

        [Required]
        public decimal MaxCashReturnDailyLimit { get; set; }

        [Required]
        public bool UsePlasticBagSale { get; set; }

        [Required]
        public bool AddPlasticBagtoEveryTransaction { get; set; }

        [Required]
        public bool AddPlasticBagForEachItem { get; set; }

        [Required]
        public bool AddFixPiecePlasticBag { get; set; }

        [Required]
        public byte PlasticBagFixedPiece { get; set; }

        [Required]
        public bool UseCountryCallingCodes { get; set; }

        [Required]
        public bool DoNotFilterCustomerCodeOnOrderSearch { get; set; }

        [Required]
        public bool ConfirmCommAddressOnRepetitionOnly { get; set; }

        [Required]
        public bool FilterCompanyCodeOnSalesPersonSearch { get; set; }

        [Required]
        public bool CanSearchCreditVoucher { get; set; }

        [Required]
        public bool OpenCustomerContactCardOnSave { get; set; }

        [Required]
        public bool UseVerificationForCashReturnTransactions { get; set; }

        [Required]
        public bool UseVerificationForCreditCardReturnTransactions { get; set; }

        [Required]
        public bool CannotChangeExchangeRateOnBankDeposit { get; set; }

        [Required]
        public short DepartmentReceiptLastNDay { get; set; }

        [Required]
        public bool IgnoreSMSCustomerVerificationForReturnTransactions { get; set; }

        [Required]
        public bool GetSalesPersonOnSalesForReturnGiftProduct { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode1 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode2 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode3 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode4 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode5 { get; set; }

        [Required]
        public bool DontAllowDublicateCommAddress { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AddressTypeCode1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AddressTypeCode2 { get; set; }

        [Required]
        public byte AttributeTypeCode1 { get; set; }

        [Required]
        public byte AttributeTypeCode2 { get; set; }

        [Required]
        public byte AttributeTypeCode3 { get; set; }

        [Required]
        public byte AttributeTypeCode4 { get; set; }

        [Required]
        public byte AttributeTypeCode5 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        [Required]
        public object StoreCompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VehicleCode { get; set; }

        [Required]
        public bool UseCustomerPresendCardForFind { get; set; }

        [Required]
        public bool UseCustomerPhoneForFind { get; set; }

        [Required]
        public bool UseCustomerEmailForFind { get; set; }

        [Required]
        public bool AllowWebStoreReturnViaDeliveryService { get; set; }

        [Required]
        public bool UseVerificationForCashReturnSendSMSToStoreContact { get; set; }

        [Required]
        public bool UseVerificationForCashReturnIgnoreSMSVerificationSendSMSToStoreContact { get; set; }

        [Required]
        public bool UseVerificationForCreditCardReturnSendSMSToStoreContact { get; set; }

        [Required]
        public bool UseVerificationForCreditCardReturnIgnoreSMSVerificationSendSMSToStoreContact { get; set; }

        [Required]
        public bool UseLikeCashInstallmentsforCustomerPresentCard { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LikeCashInstallmentsforCustomerPresentCardType { get; set; }

        [Required]
        public short AdvancePaymentReturnLastNDay { get; set; }

        [Required]
        public bool ResolveSupportRequestServicemanSelectAllRecords { get; set; }

        [Required]
        public bool ResolveSupportRequestShowAllRecords { get; set; }

        [Required]
        public bool SupportStatusNotificationShowAllRecords { get; set; }

        [Required]
        public bool OpenCustomerCardIfPersonalDataConfirmationNeeded { get; set; }

        [Required]
        public bool SendSMSToStoreContactForBankPayment { get; set; }

        [Required]
        public bool IsWebStoreOrdersToWebStoreInvoice { get; set; }

        [Required]
        public bool CheckRetailCustomerAgeLimitOnSave { get; set; }

        [Required]
        public byte RetailCustomerAgeLimitOnSave { get; set; }

        [Required]
        public bool UseMultipleCurrencyForMicroExport { get; set; }

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

        [Required]
        public bool UseOtpServiceForCustomerVerification { get; set; }

        [Required]
        public bool UseRetailCustomerSurveyAfterInvoice { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RetailCustomerSurveyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCodeForLoyaltyProgram { get; set; }

        // Navigation Properties
        public virtual cdVehicle cdVehicle { get; set; }
        public virtual bsPresentCardActivationType bsPresentCardActivationType { get; set; }
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }

        public virtual ICollection<dfPosNewCustomer> dfPosNewCustomers { get; set; }
        public virtual ICollection<dfPosNewCustomerActions> dfPosNewCustomerActionss { get; set; }
        public virtual ICollection<dfPosNewCustomerField> dfPosNewCustomerFields { get; set; }
        public virtual ICollection<dfPosOrderOpticalProductField> dfPosOrderOpticalProductFields { get; set; }
        public virtual ICollection<dfPosUI> dfPosUIs { get; set; }
        public virtual ICollection<dfPosUISetting> dfPosUISettings { get; set; }
        public virtual ICollection<dfStoreConsStore> dfStoreConsStores { get; set; }
        public virtual ICollection<dfStoreDeliveryWarehouse> dfStoreDeliveryWarehouses { get; set; }
        public virtual ICollection<dfStoreDigitalMarketingService> dfStoreDigitalMarketingServices { get; set; }
        public virtual ICollection<dfStoreDistributionWarehouse> dfStoreDistributionWarehouses { get; set; }
        public virtual ICollection<dfStoreFolder> dfStoreFolders { get; set; }
        public virtual ICollection<dfStoreForeignCurrency> dfStoreForeignCurrencys { get; set; }
        public virtual ICollection<dfStoreProductInformation> dfStoreProductInformations { get; set; }
        public virtual ICollection<dfStoreSupportWarehouse> dfStoreSupportWarehouses { get; set; }
        public virtual ICollection<dfStoreTotalDiscountAuthority> dfStoreTotalDiscountAuthoritys { get; set; }
        public virtual ICollection<dfStoreTransferStore> dfStoreTransferStores { get; set; }
    }
}
