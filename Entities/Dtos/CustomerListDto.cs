using System;
using Microsoft.EntityFrameworkCore;

namespace Api.Entities.Dtos
{
    [Keyless]
    public class CustomerListDto
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int CustomerTypeCode { get; set; }
        public string CustomerTypeDescription { get; set; }
        public string CustomerDiscountGrCode { get; set; }
        public string CustomerDiscountGrDescription { get; set; }
        public string CustomerPaymentPlanGrCode { get; set; }
        public string CustomerPaymentPlanGrDescription { get; set; }
        public string PriceGroupCode { get; set; }
        public string PriceGroupDescription { get; set; }
        public string PromotionGroupCode { get; set; }
        public string PromotionGroupDescription { get; set; }
        public bool IsBlocked { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOfficeCode { get; set; }
    }
} 