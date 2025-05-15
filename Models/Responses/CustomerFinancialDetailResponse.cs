using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    public class CustomerFinancialDetailResponse
    {
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal Balance { get; set; }
        public List<CustomerDebitItem> DebitItems { get; set; } = new List<CustomerDebitItem>();
        public List<CustomerCreditItem> CreditItems { get; set; } = new List<CustomerCreditItem>();
    }

    public class CustomerDebitItem
    {
        public int CurrAccTypeCode { get; set; }
        public string CurrAccCode { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime DocumentDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string LineDescription { get; set; } = string.Empty;
        public string RefNumber { get; set; } = string.Empty;
        public string DocCurrencyCode { get; set; } = string.Empty;
        public decimal DocDebit { get; set; }
        public string LocCurrencyCode { get; set; } = string.Empty;
        public decimal LocExchangeRate { get; set; }
        public decimal LocDebit { get; set; }
        public decimal LocBalance { get; set; }
        public int DebitTypeCode { get; set; }
        public string DebitReasonCode { get; set; } = string.Empty;
        public Guid DebitLineID { get; set; }
        public Guid DebitHeaderID { get; set; }
        public string ApplicationCode { get; set; } = string.Empty;
        public Guid? ApplicationID { get; set; }
    }

    public class CustomerCreditItem
    {
        public int CurrAccTypeCode { get; set; }
        public string CurrAccCode { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime DocumentDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string LineDescription { get; set; } = string.Empty;
        public string RefNumber { get; set; } = string.Empty;
        public string DocCurrencyCode { get; set; } = string.Empty;
        public decimal DocAmount { get; set; }
        public string LocCurrencyCode { get; set; } = string.Empty;
        public decimal LocExchangeRate { get; set; }
        public decimal LocAmount { get; set; }
        public decimal LocBalance { get; set; }
        public string ApplicationCode { get; set; } = string.Empty;
        public Guid? ApplicationLineID { get; set; }
        public Guid CurrAccBookID { get; set; }
        public int PaymentTypeCode { get; set; }
    }
}
