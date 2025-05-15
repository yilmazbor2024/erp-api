using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models
{
    public class CustomerCreditResponse
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
        public string RefNumber { get; set; }
        public DateTime? DueDate { get; set; }
        public string LineDescription { get; set; }
        
        // Belge para birimi
        public string Doc_CurrencyCode { get; set; }
        public decimal Doc_Amount { get; set; }
        
        // Yerel para birimi
        public string Loc_CurrencyCode { get; set; }
        public decimal Loc_ExchangeRate { get; set; }
        public decimal Loc_Amount { get; set; }
        public decimal Loc_Balance { get; set; }
        
        public string ApplicationCode { get; set; }
        public Guid? ApplicationLineID { get; set; }
        public Guid CurrAccBookID { get; set; }
        public int PaymentTypeCode { get; set; }
        
        // Özel alanlar
        public string ATAtt01 { get; set; }
        public string ATAtt02 { get; set; }
        public string ATAtt03 { get; set; }
        public string ATAtt04 { get; set; }
        public string ATAtt05 { get; set; }
    }
    
    // Müşteri alacak özeti için model
    public class CustomerCreditSummaryResponse
    {
        public string CurrAccCode { get; set; }
        public string CurrAccDescription { get; set; }
        public string CurrencyCode { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalBalance { get; set; }
        public int CreditCount { get; set; }
        public DateTime? OldestCreditDate { get; set; }
        public DateTime? LatestCreditDate { get; set; }
        public int PaymentTypeCode { get; set; }
        public string PaymentTypeDescription { get; set; }
    }
}
