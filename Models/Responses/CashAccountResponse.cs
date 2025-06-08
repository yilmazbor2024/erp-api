using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    public class CashAccountResponse
    {
        public string CashAccountCode { get; set; }
        public string CashAccountDescription { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyDescription { get; set; }
        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeDescription { get; set; }
        public string StoreCode { get; set; }
        public bool IsBlocked { get; set; }
    }
}
