using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    public class CustomerDetailResponse : CustomerResponse
    {
        public new string CustomerCode { get; set; } = string.Empty;
        public new string CustomerName { get; set; } = string.Empty;
        public new string TaxNumber { get; set; } = string.Empty;
        public new string TaxOfficeCode { get; set; } = string.Empty;
        public string IdentityNum { get; set; } = string.Empty;
        public bool IsIndividual { get; set; }
        public decimal CreditLimit { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public string PaymentPlanCode { get; set; } = string.Empty;
        public bool IsVIP { get; set; }
        public string CompanyCode { get; set; } = string.Empty;
        public string OfficeCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CreatedUserName { get; set; } = string.Empty;
        public bool IsSubjectToEInvoice { get; set; }
        public bool IsSubjectToEArchive { get; set; }
        public bool IsSubjectToEWaybill { get; set; }
        public DateTime EInvoiceStartDate { get; set; }
        public DateTime EWaybillStartDate { get; set; }
        public new int CustomerTypeCode { get; set; }
        public new string CustomerTypeDescription { get; set; } = string.Empty;
        public new string DiscountGroupCode { get; set; } = string.Empty;
        public new string DiscountGroupDescription { get; set; } = string.Empty;
        public new string CustomerPaymentPlanGrCode { get; set; } = string.Empty;
        public new string PaymentPlanGroupDescription { get; set; } = string.Empty;
        public new string RegionCode { get; set; } = string.Empty;
        public new string RegionDescription { get; set; } = string.Empty;
        public new string CityCode { get; set; } = string.Empty;
        public new string CityDescription { get; set; } = string.Empty;
        public new string DistrictCode { get; set; } = string.Empty;
        public new string DistrictDescription { get; set; } = string.Empty;
        public new bool IsBlocked { get; set; }
        public int CurrAccType { get; set; }
        public int CurrAccTypeCode { get; set; }
        
        // Finansal bilgiler
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal Balance { get; set; }
        
        public new List<CustomerContactResponse> Contacts { get; set; } = new List<CustomerContactResponse>();
        public new List<CustomerAddressResponse> Addresses { get; set; } = new List<CustomerAddressResponse>();
        public new List<CustomerCommunicationResponse> Communications { get; set; } = new List<CustomerCommunicationResponse>();
    }
} 