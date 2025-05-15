using System;

namespace ErpMobile.Api.Models.Customer
{
    public class CustomerCreditInfoResponse
    {
        public string CustomerCode { get; set; }
        public string CustomerDescription { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public decimal OpenRisk { get; set; }
        public decimal BalanceAndRisk { get; set; }
        public decimal RemainingCreditLimit { get; set; }
    }
}
