using System;
using System.Text.Json.Serialization;

namespace ErpMobile.Api.Models.Invoice
{
    /// <summary>
    /// Fatura başlığı uzantı bilgilerini içeren model
    /// tpInvoiceHeaderExtension tablosuna karşılık gelir
    /// </summary>
    public class InvoiceHeaderExtensionModel
    {
        /// <summary>
        /// Ödeme şekli kodu (CASH, CREDIT vb.)
        /// </summary>
        public string PaymentMeansCode { get; set; }
        
        /// <summary>
        /// Ödeme kanalı kodu (10=Peşin, 20=Vadeli vb.)
        /// </summary>
        public string PaymentChannelCode { get; set; }
        
        /// <summary>
        /// Bireysel müşteri mi?
        /// </summary>
        public bool IsIndividual { get; set; }
        
        /// <summary>
        /// Belge tarihi
        /// </summary>
        public DateTime? DocumentDate { get; set; }
    }
}
