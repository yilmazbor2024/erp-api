using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Müşteri finansal bilgilerini güncelleme isteği modeli
    /// </summary>
    public class CustomerFinancialUpdateRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [Required(ErrorMessage = "Müşteri kodu zorunludur")]
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        [StringLength(5, ErrorMessage = "Para birimi kodu en fazla 5 karakter olabilir")]
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Kredi limiti
        /// </summary>
        public decimal? CreditLimit { get; set; }
        
        /// <summary>
        /// Risk limiti
        /// </summary>
        public decimal? RiskLimit { get; set; }
        
        /// <summary>
        /// Minimum bakiye
        /// </summary>
        public decimal? MinBalance { get; set; }
        
        /// <summary>
        /// Ödeme vadesi (gün)
        /// </summary>
        public int? PaymentTerm { get; set; }
        
        /// <summary>
        /// Vade tarihi formülü kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Vade tarihi formülü kodu en fazla 10 karakter olabilir")]
        public string DueDateFormulaCode { get; set; }
        
        /// <summary>
        /// Ödeme planı kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Ödeme planı kodu en fazla 10 karakter olabilir")]
        public string PaymentPlanCode { get; set; }
        
        /// <summary>
        /// Güncelleyen kullanıcı adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Güncelleyen kullanıcı adı en fazla 50 karakter olabilir")]
        public string ModifiedUserName { get; set; }
        
        /// <summary>
        /// Banka kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Banka kodu en fazla 10 karakter olabilir")]
        public string BankCode { get; set; }
        
        /// <summary>
        /// Banka şube kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Banka şube kodu en fazla 10 karakter olabilir")]
        public string BankBranchCode { get; set; }
        
        /// <summary>
        /// Banka hesap tipi kodu
        /// </summary>
        public byte? BankAccTypeCode { get; set; }
        
        /// <summary>
        /// IBAN
        /// </summary>
        [StringLength(50, ErrorMessage = "IBAN en fazla 50 karakter olabilir")]
        public string IBAN { get; set; }
        
        /// <summary>
        /// SWIFT kodu
        /// </summary>
        [StringLength(20, ErrorMessage = "SWIFT kodu en fazla 20 karakter olabilir")]
        public string SWIFTCode { get; set; }
        
        /// <summary>
        /// Banka hesap numarası
        /// </summary>
        [StringLength(50, ErrorMessage = "Banka hesap numarası en fazla 50 karakter olabilir")]
        public string BankAccNo { get; set; }
        
        /// <summary>
        /// Mağazada banka hesabı kullanılsın mı?
        /// </summary>
        public bool? UseBankAccOnStore { get; set; }
        
        /// <summary>
        /// Müşteri indirim grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Müşteri indirim grubu kodu en fazla 10 karakter olabilir")]
        public string CustomerDiscountGrCode { get; set; }
        
        /// <summary>
        /// Müşteri fiyat artış grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Müşteri fiyat artış grubu kodu en fazla 10 karakter olabilir")]
        public string CustomerMarkupGrCode { get; set; }
        
        /// <summary>
        /// Müşteri ödeme planı grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Müşteri ödeme planı grubu kodu en fazla 10 karakter olabilir")]
        public string CustomerPaymentPlanGrCode { get; set; }
        
        /// <summary>
        /// Tedarikçi ödeme planı grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Tedarikçi ödeme planı grubu kodu en fazla 10 karakter olabilir")]
        public string VendorPaymentPlanGrCode { get; set; }
        
        /// <summary>
        /// Perakende satış fiyat grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Perakende satış fiyat grubu kodu en fazla 10 karakter olabilir")]
        public string RetailSalePriceGroupCode { get; set; }
        
        /// <summary>
        /// Toptan satış fiyat grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Toptan satış fiyat grubu kodu en fazla 10 karakter olabilir")]
        public string WholesalePriceGroupCode { get; set; }
        
        /// <summary>
        /// Promosyon grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Promosyon grubu kodu en fazla 10 karakter olabilir")]
        public string PromotionGroupCode { get; set; }
        
        /// <summary>
        /// Satış kanalı kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Satış kanalı kodu en fazla 10 karakter olabilir")]
        public string SalesChannelCode { get; set; }
        
        /// <summary>
        /// Hesap açılış tarihi
        /// </summary>
        public DateTime? AccountOpeningDate { get; set; }
        
        /// <summary>
        /// Hesap kapanış tarihi
        /// </summary>
        public DateTime? AccountClosingDate { get; set; }
        
        /// <summary>
        /// E-Fatura başlangıç tarihi
        /// </summary>
        public DateTime? EInvoiceStartDate { get; set; }
        
        /// <summary>
        /// E-İrsaliye başlangıç tarihi
        /// </summary>
        public DateTime? EShipmentStartDate { get; set; }
        
        /// <summary>
        /// Kredi bakiyesine izin ver
        /// </summary>
        public bool? PermitCreditBalance { get; set; }
        
        /// <summary>
        /// E-Faturaya tabi mi?
        /// </summary>
        public bool? IsSubjectToEInvoice { get; set; }
        
        /// <summary>
        /// E-İrsaliyeye tabi mi?
        /// </summary>
        public bool? IsSubjectToEShipment { get; set; }
        
        /// <summary>
        /// Ticari fatura düzenle
        /// </summary>
        public bool? IsArrangeCommercialInvoice { get; set; }
        
        /// <summary>
        /// Satın alma talebi gerekli mi?
        /// </summary>
        public bool? PurchaseRequisitionRequired { get; set; }
        
        /// <summary>
        /// Güncelleyen kullanıcı adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Güncelleyen kullanıcı adı en fazla 50 karakter olabilir")]
        public string LastUpdatedUserName { get; set; } = "SYSTEM";
    }
}
