using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Borç senetleri listesi yanıt modeli
    /// </summary>
    public class DebitListResponse
    {
        /// <summary>
        /// Borç senetleri
        /// </summary>
        public List<InvoiceDebitResponse> Debits { get; set; } = new List<InvoiceDebitResponse>();
    }

    /// <summary>
    /// Borç senedi yanıt modeli
    /// </summary>
    public class InvoiceDebitResponse
    {
        /// <summary>
        /// Borç senedi ID
        /// </summary>
        public string DebitId { get; set; }

        /// <summary>
        /// Fatura ID
        /// </summary>
        public string InvoiceHeaderId { get; set; }

        /// <summary>
        /// Borç tutarı
        /// </summary>
        public decimal DebitAmount { get; set; }

        /// <summary>
        /// Borç tarihi
        /// </summary>
        public DateTime DebitDate { get; set; }

        /// <summary>
        /// İşlendi mi?
        /// </summary>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime? ProcessDate { get; set; }
    }
} 