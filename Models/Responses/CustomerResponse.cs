using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    public class CustomerResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Id { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string TaxOfficeCode { get; set; } = string.Empty;
        public int CustomerTypeCode { get; set; }
        public string CustomerTypeDescription { get; set; } = string.Empty;
        public string DiscountGroupCode { get; set; } = string.Empty;
        public string DiscountGroupDescription { get; set; } = string.Empty;
        public string CustomerPaymentPlanGrCode { get; set; } = string.Empty;
        public string PaymentPlanGroupDescription { get; set; } = string.Empty;
        public string RegionCode { get; set; } = string.Empty;
        public string RegionDescription { get; set; } = string.Empty;
        public string CityCode { get; set; } = string.Empty;
        public string CityDescription { get; set; } = string.Empty;
        public string DistrictCode { get; set; } = string.Empty;
        public string DistrictDescription { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<CustomerContactResponse> Contacts { get; set; } = new List<CustomerContactResponse>();
        public List<CustomerAddressResponse> Addresses { get; set; } = new List<CustomerAddressResponse>();
        public List<CustomerCommunicationResponse> Communications { get; set; } = new List<CustomerCommunicationResponse>();
    }
} 