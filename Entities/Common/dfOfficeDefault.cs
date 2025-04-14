using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models{
    [Table("dfOfficeDefault")]
    public class dfOfficeDefault
    {
        [Key]
        [StringLength(10)]
        public string OfficeCode { get; set; }

        [Required]
        public decimal OfficeCodeNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string LocalCurrencyCode { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MaxLimitOfCashPayment { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MaxLimitOfReceipt { get; set; }

        [Required]
        [StringLength(5)]
        public string LocalDataLanguageCode { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [Required]
        [StringLength(10)]
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

        [Required]
        [StringLength(10)]
        public string SendAdvertConfirmationFormTypeCode { get; set; }

        [Required]
        public bool ForcePrintSendAdvertConfirm { get; set; }

        [Required]
        public bool ForcePrintSendAdvertUnConfirm { get; set; }

        [Required]
        [StringLength(10)]
        public string TaxFreeRefundCompanyCode { get; set; }

        [Required]
        public bool UseTaxFreeIsEInvoice { get; set; }

        [Required]
        public bool UseAgentPerformance { get; set; }

        [Required]
        public bool UseReservationCard { get; set; }

        [Required]
        public bool AskOpDateOnTransferApproving { get; set; }

        [Required]
        [StringLength(20)]
        public string SGKTaxPayerCode { get; set; }

        [Required]
        [StringLength(60)]
        public string CommercialRegistrationNo { get; set; }

        [Required]
        public long AddressID { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        public string SiteName { get; set; }

        [Required]
        [StringLength(20)]
        public string BuildingName { get; set; }

        [Required]
        [StringLength(10)]
        public string BuildingNum { get; set; }

        [Required]
        public short FloorNum { get; set; }

        [Required]
        public short DoorNum { get; set; }

        [Required]
        public int QuarterCode { get; set; }

        [Required]
        [StringLength(200)]
        public string QuarterName { get; set; }

        [Required]
        [StringLength(20)]
        public string Boulevard { get; set; }

        [Required]
        public int StreetCode { get; set; }

        [Required]
        [StringLength(20)]
        public string Street { get; set; }

        [Required]
        [StringLength(20)]
        public string Road { get; set; }

        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(10)]
        public string StateCode { get; set; }

        [Required]
        [StringLength(10)]
        public string CityCode { get; set; }

        [Required]
        [StringLength(30)]
        public string DistrictCode { get; set; }

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(100)]
        public string DrivingDirections { get; set; }

        [Required]
        [StringLength(20)]
        public string MersisNum { get; set; }

        [Required]
        [StringLength(100)]
        public string URNAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string EShipmentUrnAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string EShipmentUrnAddressPK { get; set; }

        [Required]
        public byte BulkMailServiceProviderCode { get; set; }

        [Required]
        public bool DoNotAskReservationCodeOnTransactionStart { get; set; }

        [Required]
        public bool DoNotFilterCompanyCodeOnReservationSearch { get; set; }

        [Required]
        public bool DoNotUsePersonalDataConfirmation { get; set; }

        [Required]
        [StringLength(10)]
        public string ConfirmationFormTypeCode { get; set; }

        [Required]
        public bool UseOfficeBasedSerialNumberTracking { get; set; }

        [Required]
        public byte DefaultSupportTypeCode { get; set; }

        [Required]
        public byte DefaultSupportPriorityCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(20)]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        [ForeignKey("OfficeCode")]
        public virtual cdOffice Office { get; set; }

        [ForeignKey("CountryCode")]
        public virtual cdCountry Country { get; set; }

        [ForeignKey("StateCode")]
        public virtual cdState State { get; set; }

        [ForeignKey("CityCode")]
        public virtual cdCity City { get; set; }

        [ForeignKey("DistrictCode")]
        public virtual cdDistrict District { get; set; }

        [ForeignKey("PriceGroupCode")]
        public virtual cdPriceGroup PriceGroup { get; set; }
    }
} 