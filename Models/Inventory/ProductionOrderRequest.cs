using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Inventory
{
    /// <summary>
    /// Üretim fişi işlemi için istek modeli
    /// </summary>
    public class ProductionOrderRequest
    {
        /// <summary>
        /// Depo kodu
        /// </summary>
        [Required(ErrorMessage = "Depo kodu zorunludur")]
        public string WarehouseCode { get; set; }
        
        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// İşlem tarihi (varsayılan: bugün)
        /// </summary>
        public DateTime? OperationDate { get; set; }
        
        /// <summary>
        /// Sipariş tarihi (varsayılan: bugün)
        /// </summary>
        public DateTime? OrderDate { get; set; }
        
        /// <summary>
        /// Müşteri kodu (opsiyonel)
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Üretilecek ürünler listesi
        /// </summary>
        [Required(ErrorMessage = "En az bir ürün eklenmelidir")]
        public List<ProductionOrderItemRequest> Items { get; set; }
    }

    /// <summary>
    /// Üretim fişi kalem bilgileri
    /// </summary>
    public class ProductionOrderItemRequest
    {
        /// <summary>
        /// Sıralama
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// Öğe tipi kodu
        /// </summary>
        [Required(ErrorMessage = "Öğe tipi kodu zorunludur")]
        public int ItemTypeCode { get; set; }
        
        /// <summary>
        /// Öğe kodu
        /// </summary>
        [Required(ErrorMessage = "Öğe kodu zorunludur")]
        public string ItemCode { get; set; }
        
        /// <summary>
        /// Birim kodu
        /// </summary>
        [Required(ErrorMessage = "Birim kodu zorunludur")]
        public string UnitCode { get; set; }
        
        /// <summary>
        /// Miktar
        /// </summary>
        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar 0'dan büyük olmalıdır")]
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Fiyat
        /// </summary>
        public decimal Price { get; set; }
        
        /// <summary>
        /// Depo kodu
        /// </summary>
        public string WarehouseCode { get; set; }
        
        /// <summary>
        /// Satır açıklaması
        /// </summary>
        public string LineDescription { get; set; }
        
        /// <summary>
        /// Renk kodu
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// Boyut 1 kodu (Beden)
        /// </summary>
        public string ItemDim1Code { get; set; }
        
        /// <summary>
        /// Boyut 2 kodu
        /// </summary>
        public string ItemDim2Code { get; set; }
        
        /// <summary>
        /// Boyut 3 kodu
        /// </summary>
        public string ItemDim3Code { get; set; }
        
        /// <summary>
        /// Para birimi kodu (default: TRY)
        /// </summary>
        public string CurrencyCode { get; set; } = "TRY";
        
        /// <summary>
        /// Birim maliyet
        /// </summary>
        public decimal CostPrice { get; set; }
        
        /// <summary>
        /// Toplam maliyet tutarı
        /// </summary>
        public decimal CostAmount { get; set; }
    }
}
