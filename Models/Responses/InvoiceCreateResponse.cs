using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Fatura oluşturma yanıt modeli
    /// </summary>
    public class InvoiceCreateResponse
    {
        /// <summary>
        /// Fatura ID
        /// </summary>
        public Guid InvoiceId { get; set; }

        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Fatura tarihi
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Fatura toplamı
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Mesaj
        /// </summary>
        public string Message { get; set; }
    }
} 