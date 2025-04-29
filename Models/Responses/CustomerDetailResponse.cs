using System;
using System.Collections.Generic;

namespace erp_api.Models.Responses
{
    public class CustomerDetailResponse : CustomerResponse
    {
        public new string CustomerCode { get; set; } = string.Empty;
        public new string CustomerName { get; set; } = string.Empty;
        public new string TaxNumber { get; set; } = string.Empty;
        public new string TaxOfficeCode { get; set; } = string.Empty;
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
        public new List<CustomerContactResponse> Contacts { get; set; } = new List<CustomerContactResponse>();
        public new List<CustomerAddressResponse> Addresses { get; set; } = new List<CustomerAddressResponse>();
        public new List<CustomerCommunicationResponse> Communications { get; set; } = new List<CustomerCommunicationResponse>();
    }
} 