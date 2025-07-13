using System;

namespace ErpMobile.Api.Models.Responses
{
    public class CashSummaryResponse
    {
        public string CashAccountCode { get; set; }
        public string CashAccountDescription { get; set; }
        public string CurrencyCode { get; set; }
        
        // Açılış(Devir) bakiyeleri
        public decimal LocalOpeningBalance { get; set; }     // Yerel para açılış bakiyesi
        public decimal DocumentOpeningBalance { get; set; }  // Belge para açılış bakiyesi
        
        // Yerel ve belge toplamları
        public decimal LocalTotalDebit { get; set; }
        public decimal LocalTotalCredit { get; set; }
        public decimal DocumentTotalDebit { get; set; }
        public decimal DocumentTotalCredit { get; set; }
        
        // Fiş toplamları
        public decimal TotalReceipt { get; set; }      // Tahsilat
        public decimal TotalPayment { get; set; }      // Tediye
        public decimal TotalTransferIn { get; set; }   // Virman Giriş
        public decimal TotalTransferOut { get; set; }  // Virman Çıkış
        
        // Kapanış bakiyeleri
        public decimal LocalClosingBalance { get; set; }     // Yerel para kapanış bakiyesi
        public decimal DocumentClosingBalance { get; set; }  // Belge para kapanış bakiyesi
    }
}
