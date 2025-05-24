using System;

namespace ErpMobile.Api.Models.Product
{
    public class ProductPriceListModel
    {
        public string PriceListNumber { get; set; }
        public string PriceGroupCode { get; set; }
        public string PriceGroupDescription { get; set; }
        public string PriceListTypeCode { get; set; }
        public string PriceListTypeDescription { get; set; }
        public DateTime? PriceListDate { get; set; }
        public DateTime? ValidDate { get; set; }
        public TimeSpan? ValidTime { get; set; }
        public string CompanyCode { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsLocked { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationDescription { get; set; }
        public string CreatedUserName { get; set; }
        public string LastUpdatedUserName { get; set; }
        public int PriceListHeaderID { get; set; }
        public int ApplicationID { get; set; }
        
        // Fiyat bilgileri
        public decimal Price { get; set; }
        public decimal? VatRate { get; set; }
        public string ProductCode { get; set; }
    }
}
