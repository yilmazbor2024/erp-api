using System;

namespace ErpMobile.Api.Models.Inventory
{
    /// <summary>
    /// Depo bilgileri yanıt modeli
    /// </summary>
    public class WarehouseResponse
    {
        /// <summary>
        /// Depo kodu
        /// </summary>
        public string WarehouseCode { get; set; }
        
        /// <summary>
        /// Depo açıklaması
        /// </summary>
        public string WarehouseDescription { get; set; }
        
        /// <summary>
        /// Şirket kodu
        /// </summary>
        public string CompanyCode { get; set; }
        
        /// <summary>
        /// Ofis kodu
        /// </summary>
        public string OfficeCode { get; set; }
        
        /// <summary>
        /// Ofis açıklaması
        /// </summary>
        public string OfficeDescription { get; set; }
        
        /// <summary>
        /// Depo sahibi kodu
        /// </summary>
        public string WarehouseOwnerCode { get; set; }
        
        /// <summary>
        /// Depo sahibi açıklaması
        /// </summary>
        public string WarehouseOwnerDescription { get; set; }
        
        /// <summary>
        /// Depo tipi kodu
        /// </summary>
        public string WarehouseTypeCode { get; set; }
        
        /// <summary>
        /// Depo tipi açıklaması
        /// </summary>
        public string WarehouseTypeDescription { get; set; }
        
        /// <summary>
        /// Cari hesap tipi kodu
        /// </summary>
        public string CurrAccTypeCode { get; set; }
        
        /// <summary>
        /// Mağaza kodu
        /// </summary>
        public string StoreCode { get; set; }
        
        /// <summary>
        /// Mağaza açıklaması
        /// </summary>
        public string StoreDescription { get; set; }
        
        /// <summary>
        /// Şehir açıklaması
        /// </summary>
        public string CityDescription { get; set; }
        
        /// <summary>
        /// Adres
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Posta kodu
        /// </summary>
        public string ZipCode { get; set; }
        
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Müşteri açıklaması
        /// </summary>
        public string CustomerDescription { get; set; }
        
        /// <summary>
        /// Müşteri tipi kodu
        /// </summary>
        public string CustomerTypeCode { get; set; }
        
        /// <summary>
        /// Alt cari hesap ID
        /// </summary>
        public Guid? SubCurrAccID { get; set; }
        
        /// <summary>
        /// Alt cari hesap şirket adı
        /// </summary>
        public string SubCurrAccCompanyName { get; set; }
        
        /// <summary>
        /// Tedarikçi kodu
        /// </summary>
        public string VendorCode { get; set; }
        
        /// <summary>
        /// Tedarikçi açıklaması
        /// </summary>
        public string VendorDescription { get; set; }
        
        /// <summary>
        /// Toplam alan
        /// </summary>
        public decimal? TotalArea { get; set; }
        
        /// <summary>
        /// Negatif stok izni
        /// </summary>
        public bool PermitNegativeStock { get; set; }
        
        /// <summary>
        /// Stok seviyesi kontrolü
        /// </summary>
        public bool ControlStockLevel { get; set; }
        
        /// <summary>
        /// Perakende sonradan teslimat izni
        /// </summary>
        public bool PermitRetailSubsequentDelivery { get; set; }
        
        /// <summary>
        /// Varsayılan mı?
        /// </summary>
        public bool IsDefault { get; set; }
        
        /// <summary>
        /// Bölüm kullanımı
        /// </summary>
        public bool UseSection { get; set; }
        
        /// <summary>
        /// Bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}
