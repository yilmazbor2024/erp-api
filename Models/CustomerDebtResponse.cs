using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models
{
    public class CustomerDebtResponse
    {
        public int CurrAccTypeCode { get; set; }
        public string CurrAccCode { get; set; }
        public Guid? SubCurrAccID { get; set; }
        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string StoreTypeCode { get; set; }
        public string StoreCode { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DueDate { get; set; }
        public string LineDescription { get; set; }
        public string RefNumber { get; set; }
        
        // Belge para birimi
        public string Doc_CurrencyCode { get; set; }
        public decimal Doc_Debit { get; set; }
        
        // Yerel para birimi
        public string Loc_CurrencyCode { get; set; }
        public decimal Loc_ExchangeRate { get; set; }
        public decimal Loc_Debit { get; set; }
        public decimal Loc_Balance { get; set; }
        
        public int DebitTypeCode { get; set; }
        public string DebitReasonCode { get; set; }
        public Guid DebitLineID { get; set; }
        public Guid DebitHeaderID { get; set; }
        public string ApplicationCode { get; set; }
        public Guid? ApplicationID { get; set; }
        
        // Özel alanlar
        public string ATAtt01 { get; set; }
        public string ATAtt02 { get; set; }
        public string ATAtt03 { get; set; }
        public string ATAtt04 { get; set; }
        public string ATAtt05 { get; set; }
    }
    
    // Müşteri borç özeti için model
    public class CustomerDebtSummaryResponse
    {
        public string CurrAccCode { get; set; }
        public string CurrAccDescription { get; set; }
        public string CurrencyCode { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal TotalBalance { get; set; }
        public int DebtCount { get; set; }
        public DateTime? OldestDebtDate { get; set; }
        public DateTime? LatestDebtDate { get; set; }
    }
}
