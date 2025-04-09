using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prSubCurrAcc")]
    public partial class prSubCurrAcc
    {
        public prSubCurrAcc()
        {
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            cdWarehouses = new HashSet<cdWarehouse>();
            prCurrAccBankAccNos = new HashSet<prCurrAccBankAccNo>();
            prCurrAccCommunications = new HashSet<prCurrAccCommunication>();
            prCurrAccContacts = new HashSet<prCurrAccContact>();
            prCurrAccDefaults = new HashSet<prCurrAccDefault>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
            prCurrAccUTSInformations = new HashSet<prCurrAccUTSInformation>();
            prSubCurrAccAttributes = new HashSet<prSubCurrAccAttribute>();
            prSubCurrAccDefaults = new HashSet<prSubCurrAccDefault>();
            prSubCurrAccOnlineBanks = new HashSet<prSubCurrAccOnlineBank>();
            prSubCurrAccSalespersons = new HashSet<prSubCurrAccSalesperson>();
            trBankLines = new HashSet<trBankLine>();
            trBankPaymentInstructionLines = new HashSet<trBankPaymentInstructionLine>();
            trBankPaymentListLines = new HashSet<trBankPaymentListLine>();
            trCashLines = new HashSet<trCashLine>();
            trChequeHeaders = new HashSet<trChequeHeader>();
            trContracts = new HashSet<trContract>();
            trCreditCardPaymentHeaders = new HashSet<trCreditCardPaymentHeader>();
            trCurrAccBooks = new HashSet<trCurrAccBook>();
            trDebitHeaders = new HashSet<trDebitHeader>();
            trDispOrderHeaders = new HashSet<trDispOrderHeader>();
            trExpenseSlipHeaders = new HashSet<trExpenseSlipHeader>();
            trGiftCardPaymentHeaders = new HashSet<trGiftCardPaymentHeader>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
            trPaymentHeaders = new HashSet<trPaymentHeader>();
            trPickingHeaders = new HashSet<trPickingHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trReserveHeaders = new HashSet<trReserveHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trSMSPoolLines = new HashSet<trSMSPoolLine>();
            trStocks = new HashSet<trStock>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            zpOnlineBankCreditCardPaymentTransactions = new HashSet<zpOnlineBankCreditCardPaymentTransaction>();
        }

        [Key]
        [Required]
        public Guid SubCurrAccID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SubCurrAccCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TitleCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CompanyName { get; set; }

        [Required]
        public bool IsIndividualAcc { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [Required]
        public DateTime AgreementDate { get; set; }

        [Required]
        public short PaymentTerm { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerMarkupGrCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DataLanguageCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal CreditLimit { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WholesalePriceGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerDiscountGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerPaymentPlanGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PromotionGroupCode { get; set; }

        [Required]
        public bool IsVIP { get; set; }

        [Required]
        public bool IsSendAdvertSMS { get; set; }

        [Required]
        public bool IsSendAdvertMail { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VendorPaymentPlanGrCode { get; set; }

        // Navigation Properties
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCustomerMarkupGr cdCustomerMarkupGr { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual cdCustomerDiscountGr cdCustomerDiscountGr { get; set; }
        public virtual cdTaxOffice cdTaxOffice { get; set; }
        public virtual cdCustomerPaymentPlanGr cdCustomerPaymentPlanGr { get; set; }
        public virtual cdPromotionGroup cdPromotionGroup { get; set; }
        public virtual cdVendorPaymentPlanGr cdVendorPaymentPlanGr { get; set; }
        public virtual cdPriceGroup cdPriceGroup { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdTitle cdTitle { get; set; }

        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<cdWarehouse> cdWarehouses { get; set; }
        public virtual ICollection<prCurrAccBankAccNo> prCurrAccBankAccNos { get; set; }
        public virtual ICollection<prCurrAccCommunication> prCurrAccCommunications { get; set; }
        public virtual ICollection<prCurrAccContact> prCurrAccContacts { get; set; }
        public virtual ICollection<prCurrAccDefault> prCurrAccDefaults { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prCurrAccUTSInformation> prCurrAccUTSInformations { get; set; }
        public virtual ICollection<prSubCurrAccAttribute> prSubCurrAccAttributes { get; set; }
        public virtual ICollection<prSubCurrAccDefault> prSubCurrAccDefaults { get; set; }
        public virtual ICollection<prSubCurrAccOnlineBank> prSubCurrAccOnlineBanks { get; set; }
        public virtual ICollection<prSubCurrAccSalesperson> prSubCurrAccSalespersons { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trBankPaymentInstructionLine> trBankPaymentInstructionLines { get; set; }
        public virtual ICollection<trBankPaymentListLine> trBankPaymentListLines { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
        public virtual ICollection<trChequeHeader> trChequeHeaders { get; set; }
        public virtual ICollection<trContract> trContracts { get; set; }
        public virtual ICollection<trCreditCardPaymentHeader> trCreditCardPaymentHeaders { get; set; }
        public virtual ICollection<trCurrAccBook> trCurrAccBooks { get; set; }
        public virtual ICollection<trDebitHeader> trDebitHeaders { get; set; }
        public virtual ICollection<trDispOrderHeader> trDispOrderHeaders { get; set; }
        public virtual ICollection<trExpenseSlipHeader> trExpenseSlipHeaders { get; set; }
        public virtual ICollection<trGiftCardPaymentHeader> trGiftCardPaymentHeaders { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
        public virtual ICollection<trPaymentHeader> trPaymentHeaders { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trReserveHeader> trReserveHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trSMSPoolLine> trSMSPoolLines { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<zpOnlineBankCreditCardPaymentTransaction> zpOnlineBankCreditCardPaymentTransactions { get; set; }
    }
}
