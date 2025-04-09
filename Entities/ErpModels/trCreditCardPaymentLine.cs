using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCreditCardPaymentLine")]
    public partial class trCreditCardPaymentLine
    {
        public trCreditCardPaymentLine()
        {
            tpBulutTahsilatCreditCardPayments = new HashSet<tpBulutTahsilatCreditCardPayment>();
            tpCompanyCreditCardPaymentDueDates = new HashSet<tpCompanyCreditCardPaymentDueDate>();
            tpCreditCardBulutTahsilatVPOSReturns = new HashSet<tpCreditCardBulutTahsilatVPOSReturn>();
            tpCreditCardPaymentDueDates = new HashSet<tpCreditCardPaymentDueDate>();
            tpCreditCardPaymentFTAttributes = new HashSet<tpCreditCardPaymentFTAttribute>();
            tpPaynetCreditCardPayments = new HashSet<tpPaynetCreditCardPayment>();
            trCreditCardPaymentLineCurrencys = new HashSet<trCreditCardPaymentLineCurrency>();
            trOrderAdvancePaymentss = new HashSet<trOrderAdvancePayments>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AcquirerBankCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IssuerBankCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyCreditCardCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentProviderCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CreditCardNum { get; set; }

        [Required]
        public byte CreditCardInstallmentCount { get; set; }

        [Required]
        public DateTime InstallmentStartDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string POSProvisionID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CancelWorkPlaceID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CancelSortOrderNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CancelEndOfDayNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CancelBankCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CancelRefNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CardHoldersBankCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentCardBIN { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrAccCurrencyCode { get; set; }

        [Required]
        public double CurrAccExchangeRate { get; set; }

        [Required]
        public decimal CurrAccAmount { get; set; }

        [Required]
        public bool VPosReturnStatus { get; set; }

        [Required]
        public Guid CreditCardPaymentHeaderID { get; set; }

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

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CancelQRRefNumber { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object OriginalReferenceNo { get; set; }

        // Navigation Properties
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCompanyCreditCard cdCompanyCreditCard { get; set; }
        public virtual cdPaymentProvider cdPaymentProvider { get; set; }
        public virtual trCreditCardPaymentHeader trCreditCardPaymentHeader { get; set; }
        public virtual cdBank cdBank { get; set; }
        public virtual cdCreditCardType cdCreditCardType { get; set; }

        public virtual ICollection<tpBulutTahsilatCreditCardPayment> tpBulutTahsilatCreditCardPayments { get; set; }
        public virtual ICollection<tpCompanyCreditCardPaymentDueDate> tpCompanyCreditCardPaymentDueDates { get; set; }
        public virtual ICollection<tpCreditCardBulutTahsilatVPOSReturn> tpCreditCardBulutTahsilatVPOSReturns { get; set; }
        public virtual ICollection<tpCreditCardPaymentDueDate> tpCreditCardPaymentDueDates { get; set; }
        public virtual ICollection<tpCreditCardPaymentFTAttribute> tpCreditCardPaymentFTAttributes { get; set; }
        public virtual ICollection<tpPaynetCreditCardPayment> tpPaynetCreditCardPayments { get; set; }
        public virtual ICollection<trCreditCardPaymentLineCurrency> trCreditCardPaymentLineCurrencys { get; set; }
        public virtual ICollection<trOrderAdvancePayments> trOrderAdvancePaymentss { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
