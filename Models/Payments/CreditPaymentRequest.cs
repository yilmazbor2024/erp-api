using System;
using System.Collections.Generic;

namespace ErpApi.Models.Payments
{
    /// <summary>
    /// Vadeli ödeme isteği modeli
    /// </summary>
    public class CreditPaymentRequest
    {
        /// <summary>
        /// Fatura başlık ID'si (string olarak)
        /// </summary>
        public string InvoiceHeaderID { get; set; }
        
        /// <summary>
        /// Fatura ID'si (Guid olarak)
        /// </summary>
        public Guid InvoiceId { get; set; }
        
        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }
        
        /// <summary>
        /// Cari hesap kodu
        /// </summary>
        public string CurrAccCode { get; set; }
        
        /// <summary>
        /// Cari hesap tipi kodu
        /// </summary>
        public byte CurrAccTypeCode { get; set; }
        
        /// <summary>
        /// İşlem kodu
        /// </summary>
        public string ProcessCode { get; set; }
        
        /// <summary>
        /// Belge para birimi kodu
        /// </summary>
        public string DocCurrencyCode { get; set; }
        
        /// <summary>
        /// Yerel para birimi kodu
        /// </summary>
        public string LocalCurrencyCode { get; set; }
        
        /// <summary>
        /// Döviz kuru
        /// </summary>
        public decimal ExchangeRate { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Ödeme satırları
        /// </summary>
        public List<PaymentRow> PaymentRows { get; set; }
        
        /// <summary>
        /// Öznitelikler
        /// </summary>
        public List<AttributeModel> Attributes { get; set; }
        
        /// <summary>
        /// Faturayı tamamlandı olarak işaretle
        /// </summary>
        public bool MarkAsCompleted { get; set; } = true;
    }
}
