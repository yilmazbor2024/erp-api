using System;

namespace erp_api.Models.Responses
{
    public class DailyCashTransactionResponse
    {
        public string CashRelCode { get; set; }
        public string CashFicheNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public TimeSpan DocumentTime { get; set; }
        public string CashAccountCode { get; set; }
        public string CashAccountDescription { get; set; }
        public string CurrencyCode { get; set; }
        public int CashTransTypeCode { get; set; }
        public string TransactionTypeName { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationName { get; set; }
        public string RelatedAccountCode { get; set; }
        public string RelatedAccountName { get; set; }
        public DateTime? DocDate { get; set; }
        public string DocNo { get; set; }
        public string Description { get; set; }
        public decimal LocalDebit { get; set; }
        public decimal LocalCredit { get; set; }
        public decimal DocumentDebit { get; set; }
        public decimal DocumentCredit { get; set; }
    }
}
