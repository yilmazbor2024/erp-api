using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models
{
    public class BankAccountResponse
    {
        public string BankAccountCode { get; set; }
        public string BankAccountDescription { get; set; }
        public string BankAccTypeCode { get; set; }
        public string BankAccTypeDescription { get; set; }
        public string CompanyCode { get; set; }
        public string BankCode { get; set; }
        public string BankDescription { get; set; }
        public string BankBranchCode { get; set; }
        public string BankBranchDescription { get; set; }
        public string CurrencyCode { get; set; }
        public string BankAccNo { get; set; }
        public string IBAN { get; set; }
        public bool UseBankAccOnStore { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeDescription { get; set; }
        public bool IsBlocked { get; set; }
    }
}
