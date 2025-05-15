using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models
{
    public class CashTransactionResponse
    {
        // Header bilgileri
        public Guid CashHeaderID { get; set; }
        public int CashTransTypeCode { get; set; }
        public string CashTransNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public TimeSpan DocumentTime { get; set; }
        public string DocumentNumber { get; set; }
        public string RefNumber { get; set; }
        public string Description { get; set; }
        public int CashCurrAccTypeCode { get; set; }
        public string CashCurrAccCode { get; set; }
        public string OfficeCode { get; set; }
        public string StoreTypeCode { get; set; }
        public string StoreCode { get; set; }
        public Guid? POSTerminalID { get; set; }
        public decimal ExchangeRate { get; set; }
        public bool IsForeignCurrencyTransaction { get; set; }
        public bool IsDiffCurrencyEachLine { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsLocked { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsPostingJournal { get; set; }
        public DateTime? JournalDate { get; set; }
        public string ApplicationCode { get; set; }
        public Guid? ApplicationID { get; set; }
        public bool IsPostingApproved { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedUserName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        
        // Line bilgileri
        public Guid CashLineID { get; set; }
        public int SortOrder { get; set; }
        public int CurrAccTypeCode { get; set; }
        public string CurrAccCode { get; set; }
        public Guid? SubCurrAccID { get; set; }
        public Guid? ContactID { get; set; }
        public string CompanyCode { get; set; }
        public string GLAccCode { get; set; }
        public string CostCenterCode { get; set; }
        public string GLTypeCode { get; set; }
        public string ImportFileNumber { get; set; }
        public string ExportFileNumber { get; set; }
        public string LineDescription { get; set; }
        public string EmployeePayTypeCode { get; set; }
        public string DocCurrencyCode { get; set; }
        public string CurrAccCurrencyCode { get; set; }
        public decimal CurrAccExchangeRate { get; set; }
        public decimal CurrAccAmount { get; set; }
        
        // Belge para birimi
        public string Doc_CurrencyCode { get; set; }
        public decimal Doc_Debit { get; set; }
        public decimal Doc_Credit { get; set; }
        
        // Yerel para birimi
        public string Loc_CurrencyCode { get; set; }
        public decimal Loc_ExchangeRate { get; set; }
        public decimal Loc_Debit { get; set; }
        public decimal Loc_Credit { get; set; }
        
        // Şirket para birimi
        public string Com_CurrencyCode { get; set; }
        public decimal Com_ExchangeRate { get; set; }
        public decimal Com_Debit { get; set; }
        public decimal Com_Credit { get; set; }
    }
}
