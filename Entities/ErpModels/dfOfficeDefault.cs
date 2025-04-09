using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfOfficeDefault")]
    public partial class dfOfficeDefault
    {
        public dfOfficeDefault()
        {
            dfOfficeCreditCardTypes = new HashSet<dfOfficeCreditCardType>();
            dfOfficeEArchiveStartDates = new HashSet<dfOfficeEArchiveStartDate>();
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public decimal OfficeCodeNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public decimal MaxLimitOfCashPayment { get; set; }

        [Required]
        public decimal MaxLimitOfReceipt { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LocalDataLanguageCode { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceGroupCode { get; set; }

        [Required]
        public int PriceRoundingDigit { get; set; }

        [Required]
        public int AmountRoundingDigit { get; set; }

        [Required]
        public int ExchangeRateRoundingDigit { get; set; }

        [Required]
        public int PercentageRoundingDigit { get; set; }

        [Required]
        public float TimeLagFromServer { get; set; }

        [Required]
        public bool UseTransferApproving { get; set; }

        [Required]
        public bool DontCreateReturnReceiptOnApproving { get; set; }

        [Required]
        public bool DontCreateOrderOnApproving { get; set; }

        [Required]
        public bool RequestConfirmBeforeCreateTransaction { get; set; }

        [Required]
        public bool NotifyForOverStock { get; set; }

        [Required]
        public bool UseSetDefaultToAllLine { get; set; }

        [Required]
        public bool UseASyncEArchiveIntegration { get; set; }

        [Required]
        public bool UseSupportRequestSurvey { get; set; }

        [Required]
        public bool ForceSupportRequestSurvey { get; set; }

        [Required]
        public bool UseSupportResolveCustomerConfirmation { get; set; }

        [Required]
        public bool UseSupportRequestConfirmation { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SendAdvertConfirmationFormTypeCode { get; set; }

        [Required]
        public bool ForcePrintSendAdvertConfirm { get; set; }

        [Required]
        public bool ForcePrintSendAdvertUnConfirm { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxFreeRefundCompanyCode { get; set; }

        [Required]
        public bool UseTaxFreeIsEInvoice { get; set; }

        [Required]
        public bool UseAgentPerformance { get; set; }

        [Required]
        public bool UseReservationCard { get; set; }

        [Required]
        public bool AskOpDateOnTransferApproving { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SGKTaxPayerCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CommercialRegistrationNo { get; set; }

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

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string MersisNum { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string URNAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EShipmentUrnAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EShipmentUrnAddressPK { get; set; }

        [Required]
        public byte BulkMailServiceProviderCode { get; set; }

        [Required]
        public bool DoNotAskReservationCodeOnTransactionStart { get; set; }

        [Required]
        public bool DoNotFilterCompanyCodeOnReservationSearch { get; set; }

        [Required]
        public bool DoNotUsePersonalDataConfirmation { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormTypeCode { get; set; }

        [Required]
        public bool UseOfficeBasedSerialNumberTracking { get; set; }

        [Required]
        public byte DefaultSupportTypeCode { get; set; }

        [Required]
        public byte DefaultSupportPriorityCode { get; set; }

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
        public virtual cdState cdState { get; set; }
        public virtual cdCity cdCity { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual bsBulkMailServiceProvider bsBulkMailServiceProvider { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdExchangeType cdExchangeType { get; set; }
        public virtual cdConfirmationFormType cdConfirmationFormType { get; set; }
        public virtual cdStreet cdStreet { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdDistrict cdDistrict { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsTaxFreeRefundCompany bsTaxFreeRefundCompany { get; set; }
        public virtual cdPriceGroup cdPriceGroup { get; set; }

        public virtual ICollection<dfOfficeCreditCardType> dfOfficeCreditCardTypes { get; set; }
        public virtual ICollection<dfOfficeEArchiveStartDate> dfOfficeEArchiveStartDates { get; set; }
    }
}
