using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Müşteri borç/alacak hareketlerini içeren yanıt modeli
    /// </summary>
    public class CustomerTransactionResponse
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// İşlem ID
        /// </summary>
        public long TransactionId { get; set; }

        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Belge tarihi
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// İşlem tipi kodu
        /// </summary>
        public int TransactionTypeCode { get; set; }

        /// <summary>
        /// İşlem tipi adı
        /// </summary>
        public string TransactionTypeName { get; set; }

        /// <summary>
        /// Belge numarası
        /// </summary>
        public string DocumentNo { get; set; }

        /// <summary>
        /// Belge numarası (alternatif)
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Referans numarası
        /// </summary>
        public string RefNumber { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Satır açıklaması
        /// </summary>
        public string LineDescription { get; set; }

        /// <summary>
        /// Belge para birimi kodu
        /// </summary>
        public string DocCurrencyCode { get; set; }

        /// <summary>
        /// Borç tutarı
        /// </summary>
        public decimal DebitAmount { get; set; }

        /// <summary>
        /// Borç (alternatif)
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// Alacak tutarı
        /// </summary>
        public decimal CreditAmount { get; set; }

        /// <summary>
        /// Alacak (alternatif)
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// Bakiye
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Döviz kuru
        /// </summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı adı
        /// </summary>
        public string CreatedUserName { get; set; }
    }
}
