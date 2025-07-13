using System;

namespace ErpMobile.Api.Models.Responses
{
    public class CashTransactionResponse
    {
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; }
        public string CashTransTypeCode { get; set; }
        public string CashTransTypeDescription { get; set; }
        public string CashTransNumber { get; set; }
        public string RefNumber { get; set; }
        public string GLTypeCode { get; set; }
        public string Description { get; set; }
        public string SumDescription { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationDescription { get; set; }
        public string CurrAccTypeCode { get; set; }
        public string CurrAccTypeDescription { get; set; }
        public string CurrAccCode { get; set; }
        public string CurrAccDescription { get; set; }
        public string GLAccCode { get; set; }
        public string GLAccDescription { get; set; }
        public string LineDescription { get; set; }
        public string ChequeTransTypeDescription { get; set; }
        public string Doc_CurrencyCode { get; set; }
        public decimal Doc_Debit { get; set; }
        public decimal Doc_Credit { get; set; }
        public decimal Doc_Balance { get; set; }
        public string Loc_CurrencyCode { get; set; }
        public string Loc_ExchangeRate { get; set; }
        public decimal Loc_Debit { get; set; }
        public decimal Loc_Credit { get; set; }
        public decimal Loc_Balance { get; set; }
        public string ATAtt01 { get; set; }
        public string ATAtt02 { get; set; }
        public string ATAtt03 { get; set; }
        public string ATAtt04 { get; set; }
        public string ATAtt05 { get; set; }
        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string StoreCode { get; set; }
        public bool IsTurnover { get; set; }
        public bool IsPostingJournal { get; set; }
        public DateTime? JournalDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string JournalNumber { get; set; }
        public string ImportFileNumber { get; set; }
        public string ExportFileNumber { get; set; }
        public string CashAccountCode { get; set; }
        public string LastUpdatedUserName { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LinkedApplicationCode { get; set; }
        public Guid? LinkedApplicationID { get; set; }
    }
}
