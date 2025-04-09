using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDiscountOfferRules")]
    public partial class prDiscountOfferRules
    {
        public prDiscountOfferRules()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferCode { get; set; }

        [Key]
        [Required]
        public byte DiscountOfferStageCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TimePeriodCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AmountRuleCode { get; set; }

        [Required]
        public bool IsValidWithCustomerVerification { get; set; }

        [Required]
        public bool IsValidWithOtherInstantDiscounts { get; set; }

        [Required]
        public bool IsValidWithOtherPoints { get; set; }

        [Required]
        public bool IsValidWithOtherDiscountVouchers { get; set; }

        [Required]
        public bool IsValidWithOtherDiscountbyPMWP { get; set; }

        [Required]
        public bool IsValidCashPayments { get; set; }

        [Required]
        public bool IsValidCreditCardPayments { get; set; }

        [Required]
        public bool IsValidGiftCardPayments { get; set; }

        [Required]
        public bool IsValidCreditVoucherPayments { get; set; }

        [Required]
        public bool IsValidMobilePayments { get; set; }

        [Required]
        public bool IsValidHopiPayPayments { get; set; }

        [Required]
        public bool IsValidRemittanceAndEFTPayments { get; set; }

        [Required]
        public bool IsValidChippinPayments { get; set; }

        [Required]
        public bool IsValidAdvancePayments { get; set; }

        [Required]
        public bool IsValidNotesReceivablePayments { get; set; }

        [Required]
        public bool IsValidChequeReceivedPayments { get; set; }

        [Required]
        public bool IsValidWithPasswordVerification { get; set; }

        [Required]
        public bool CanPresentToOtherCustomer { get; set; }

        [Required]
        public bool CancelCustomerDiscount { get; set; }

        [Required]
        public bool OnlyBeUsedOnce { get; set; }

        [Required]
        public bool IsValidCustomerBirthDate { get; set; }

        [Required]
        public byte BeforeBirthDate { get; set; }

        [Required]
        public byte AfterBirthDate { get; set; }

        [Required]
        public bool IsValidCustomerMarriedDate { get; set; }

        [Required]
        public byte BeforeMarriedDate { get; set; }

        [Required]
        public byte AfterMarriedDate { get; set; }

        [Required]
        public byte MaxInstallmentCount { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string AvailableInstallments { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string AvailableInsuranceAgencies { get; set; }

        [Required]
        public bool IsValidWithDigitalMarketingServiceVerfication { get; set; }

        [Required]
        public bool IsNotValidWithDigitalMarketingServiceVerification { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DigitalMarketingServiceCode { get; set; }

        [Required]
        public bool UseCurrAccList { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrAccListCode { get; set; }

        [Required]
        public bool UseItemListForWinning { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemListCodeForWinning { get; set; }

        [Required]
        public bool UseItemListForUsing { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemListCodeForUsing { get; set; }

        [Required]
        public short DayCount { get; set; }

        [Required]
        public short WeekCount { get; set; }

        [Required]
        public short MonthCount { get; set; }

        [Required]
        public short YearCount { get; set; }

        [Required]
        public bool LastDayOption { get; set; }

        [Required]
        public DateTime LastValidDate { get; set; }

        [Required]
        public bool IsValidOtherPayments { get; set; }

        [Required]
        public bool DisableDiscountOfferOnReplacement { get; set; }

        [Required]
        public bool IsValidFastPayPayments { get; set; }

        [Required]
        public bool IsValidPaynetPayments { get; set; }

        [Required]
        public byte AgentPerformance { get; set; }

        public string PaymentProviderFilterString { get; set; }

        public string CustomerFilterString { get; set; }

        public string PositionFilterString { get; set; }

        public string ProductFilterString { get; set; }

        public string ProductS2FilterString { get; set; }

        public string CreditCardFilterString { get; set; }

        public string PaymentPlanFilterString { get; set; }

        public string PaymentProviderFilterStringForSQL { get; set; }

        public string CustomerFilterStringForSQL { get; set; }

        public string PositionFilterStringForSQL { get; set; }

        public string ProductFilterStringForSQL { get; set; }

        public string ProductS2FilterStringForSQL { get; set; }

        public string CreditCardFilterStringForSQL { get; set; }

        public string PaymentPlanFilterStringForSQL { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsValidWithCustomerSMSVerification { get; set; }

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
        public bool IsValidMacellanSuperAppPayments { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object LoyaltyProgramLevelCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object LoyaltyProgramStatusCode { get; set; }

        [Required]
        public short FirstDayCount { get; set; }

        // Navigation Properties
        public virtual cdTimePeriod cdTimePeriod { get; set; }
        public virtual cdAmountRule cdAmountRule { get; set; }
        public virtual cdCurrAccList cdCurrAccList { get; set; }
        public virtual bsDiscountOfferStage bsDiscountOfferStage { get; set; }
        public virtual cdLoyaltyProgram cdLoyaltyProgram { get; set; }
        public virtual cdDiscountOffer cdDiscountOffer { get; set; }
        public virtual cdItemList cdItemList { get; set; }
        public virtual cdDigitalMarketingService cdDigitalMarketingService { get; set; }

    }
}
