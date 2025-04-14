using System;
using System.Collections.Generic;

namespace erp_api.Models.Responses
{
    public class CustomerDetailResponse : CustomerResponse
    {
        public new string CustomerCode { get; set; }
        public new string CustomerName { get; set; }
        public new string TaxNumber { get; set; }
        public new string TaxOffice { get; set; }
        public new int CustomerTypeCode { get; set; }
        public new string CustomerTypeDescription { get; set; } = string.Empty;
        public new string DiscountGroupCode { get; set; }
        public new string DiscountGroupDescription { get; set; } = string.Empty;
        public new string PaymentPlanGroupCode { get; set; }
        public new string PaymentPlanGroupDescription { get; set; } = string.Empty;
        public new string RegionCode { get; set; }
        public new string RegionDescription { get; set; } = string.Empty;
        public new string CityCode { get; set; }
        public new string CityDescription { get; set; } = string.Empty;
        public new string DistrictCode { get; set; }
        public new string DistrictDescription { get; set; } = string.Empty;
        public new bool IsBlocked { get; set; }
        public new List<CustomerContactResponse> Contacts { get; set; } = new();
        public new List<CustomerAddressResponse> Addresses { get; set; } = new();
        public new List<CustomerCommunicationResponse> Communications { get; set; } = new();
    }
} 