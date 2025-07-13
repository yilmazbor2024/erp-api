using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Inventory
{
    /// <summary>
    /// Üretim fişi işlemi yanıt modeli
    /// </summary>
    public class ProductionOrderResponse
    {
        /// <summary>
        /// Üretim fiş numarası
        /// </summary>
        public string OrderNumber { get; set; }
        
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime OperationDate { get; set; }
        
        /// <summary>
        /// İşlem saati
        /// </summary>
        public string OperationTime { get; set; }
        
        /// <summary>
        /// Seri
        /// </summary>
        public string Series { get; set; }
        
        /// <summary>
        /// Seri numarası
        /// </summary>
        public string SeriesNumber { get; set; }
        
        /// <summary>
        /// İç işlem kodu
        /// </summary>
        public string InnerProcessCode { get; set; }
        
        /// <summary>
        /// Şirket kodu
        /// </summary>
        public string CompanyCode { get; set; }
        
        /// <summary>
        /// Ofis kodu
        /// </summary>
        public string OfficeCode { get; set; }
        
        /// <summary>
        /// Depo kodu
        /// </summary>
        public string WarehouseCode { get; set; }
        
        /// <summary>
        /// Depo adı
        /// </summary>
        public string WarehouseDescription { get; set; }
        
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string CustomerDescription { get; set; }
        
        /// <summary>
        /// Toplam miktar
        /// </summary>
        public decimal TotalQty { get; set; }
        
        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Durum kodu
        /// </summary>
        public int Status { get; set; }
        
        /// <summary>
        /// Durum açıklaması
        /// </summary>
        public string StatusDescription { get; set; }
        
        /// <summary>
        /// Kilitli mi?
        /// </summary>
        public bool IsLocked { get; set; }
        
        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public string CreatedBy { get; set; }
        
        /// <summary>
        /// Oluşturma tarihi
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        
        /// <summary>
        /// Değiştiren kullanıcı
        /// </summary>
        public string ModifiedBy { get; set; }
        
        /// <summary>
        /// Değiştirme tarihi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// Onaylayan kullanıcı
        /// </summary>
        public string ApprovedBy { get; set; }
        
        /// <summary>
        /// Onay tarihi
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        
        /// <summary>
        /// İptal eden kullanıcı
        /// </summary>
        public string CanceledBy { get; set; }
        
        /// <summary>
        /// İptal tarihi
        /// </summary>
        public DateTime? CanceledDate { get; set; }
    }

    /// <summary>
    /// Üretim fişi işlemi detaylı yanıt modeli
    /// </summary>
    public class ProductionOrderDetailResponse : ProductionOrderResponse
    {
        /// <summary>
        /// Üretim fişi kalemleri listesi
        /// </summary>
        public List<ProductionOrderItemResponse> Items { get; set; }
    }

    /// <summary>
    /// Üretim fişi kalem bilgileri
    /// </summary>
    public class ProductionOrderItemResponse
    {
        /// <summary>
        /// Sıralama
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// Ürün kodu
        /// </summary>
        public string ProductCode { get; set; }
        
        /// <summary>
        /// Ürün adı
        /// </summary>
        public string ProductDescription { get; set; }
        
        /// <summary>
        /// Öğe tipi kodu
        /// </summary>
        public int ItemTypeCode { get; set; }
        
        /// <summary>
        /// Öğe tipi açıklaması
        /// </summary>
        public string ItemTypeDescription { get; set; }
        
        /// <summary>
        /// Öğe kodu
        /// </summary>
        public string ItemCode { get; set; }
        
        /// <summary>
        /// Öğe açıklaması
        /// </summary>
        public string ItemDescription { get; set; }
        
        /// <summary>
        /// Birim kodu
        /// </summary>
        public string UnitCode { get; set; }
        
        /// <summary>
        /// Birim açıklaması
        /// </summary>
        public string UnitDescription { get; set; }
        
        /// <summary>
        /// Miktar
        /// </summary>
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
        /// Depo açıklaması
        /// </summary>
        public string WarehouseDescription { get; set; }
        
        /// <summary>
        /// Satır açıklaması
        /// </summary>
        public string LineDescription { get; set; }
        
        /// <summary>
        /// Satır durumu
        /// </summary>
        public int LineStatus { get; set; }
        
        /// <summary>
        /// Satır durum açıklaması
        /// </summary>
        public string LineStatusDescription { get; set; }
    }
}
