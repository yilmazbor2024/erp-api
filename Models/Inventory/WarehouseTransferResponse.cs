using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Inventory
{
    /// <summary>
    /// Depolar arası sevk işlemi yanıt modeli
    /// </summary>
    public class WarehouseTransferResponse
    {
        /// <summary>
        /// Sevk fiş numarası
        /// </summary>
        public string TransferNumber { get; set; }
        
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime OperationDate { get; set; }
        
        /// <summary>
        /// İşlem saati
        /// </summary>
        public TimeSpan OperationTime { get; set; }
        
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
        /// İade mi?
        /// </summary>
        public bool IsReturn { get; set; }
        
        /// <summary>
        /// Şirket kodu
        /// </summary>
        public string CompanyCode { get; set; }
        
        /// <summary>
        /// Ofis kodu
        /// </summary>
        public string OfficeCode { get; set; }
        
        /// <summary>
        /// Hedef ofis kodu
        /// </summary>
        public string ToOfficeCode { get; set; }
        
        /// <summary>
        /// Mağaza kodu
        /// </summary>
        public string StoreCode { get; set; }
        
        /// <summary>
        /// Kaynak depo kodu
        /// </summary>
        public string SourceWarehouseCode { get; set; }
        
        /// <summary>
        /// Kaynak depo adı
        /// </summary>
        public string SourceWarehouseName { get; set; }
        
        /// <summary>
        /// Hedef depo kodu
        /// </summary>
        public string TargetWarehouseCode { get; set; }
        
        /// <summary>
        /// Hedef depo adı
        /// </summary>
        public string TargetWarehouseName { get; set; }
        
        /// <summary>
        /// Hedef mağaza kodu
        /// </summary>
        public string ToStoreCode { get; set; }
        
        /// <summary>
        /// Cari hesap tipi kodu
        /// </summary>
        public string CurrAccTypeCode { get; set; }
        
        /// <summary>
        /// Tedarikçi kodu
        /// </summary>
        public string VendorCode { get; set; }
        
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Perakende müşteri kodu
        /// </summary>
        public string RetailCustomerCode { get; set; }
        
        /// <summary>
        /// Personel kodu
        /// </summary>
        public string EmployeeCode { get; set; }
        
        /// <summary>
        /// Toplam miktar
        /// </summary>
        public double TotalQty { get; set; }
        
        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// İthalat dosya numarası
        /// </summary>
        public string ImportFileNumber { get; set; }
        
        /// <summary>
        /// Tamamlandı mı?
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Kilitli mi?
        /// </summary>
        public bool IsLocked { get; set; }
        
        /// <summary>
        /// Sevk onaylandı mı?
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// İç sipariş bazlı mı?
        /// </summary>
        public bool IsInnerOrderBase { get; set; }
        
        /// <summary>
        /// Bölüm transferi mi?
        /// </summary>
        public bool IsSectionTransfer { get; set; }
        
        /// <summary>
        /// Uygulama kodu
        /// </summary>
        public string ApplicationCode { get; set; }
        
        /// <summary>
        /// Uygulama açıklaması
        /// </summary>
        public string ApplicationDescription { get; set; }
        
        /// <summary>
        /// Uygulama ID
        /// </summary>
        public Guid? ApplicationID { get; set; }
        
        /// <summary>
        /// İç başlık ID
        /// </summary>
        public Guid InnerHeaderID { get; set; }
        
        /// <summary>
        /// Sevkiyat yöntemi kodu
        /// </summary>
        public string ShipmentMethodCode { get; set; }
        
        /// <summary>
        /// Sevkiyat yöntemi adı
        /// </summary>
        public string ShipmentMethodName { get; set; }
        
        /// <summary>
        /// Onay tarihi
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public string CreatedUserName { get; set; }
        
        /// <summary>
        /// Oluşturma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// Depolar arası sevk işlemi detaylı yanıt modeli
    /// </summary>
    public class WarehouseTransferDetailResponse : WarehouseTransferResponse
    {
        /// <summary>
        /// Sevk edilen ürünler listesi
        /// </summary>
        public List<WarehouseTransferItemResponse> Items { get; set; }
    }

    /// <summary>
    /// Depolar arası sevk edilen ürün bilgileri
    /// </summary>
    public class WarehouseTransferItemResponse
    {
        /// <summary>
        /// Ürün kodu
        /// </summary>
        public string ItemCode { get; set; }
        
        /// <summary>
        /// Ürün adı
        /// </summary>
        public string ItemName { get; set; }
        
        /// <summary>
        /// Renk kodu
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// Renk adı
        /// </summary>
        public string ColorName { get; set; }
        
        /// <summary>
        /// Beden kodu
        /// </summary>
        public string ItemDim1Code { get; set; }
        
        /// <summary>
        /// Beden adı
        /// </summary>
        public string ItemDim1Name { get; set; }
        
        /// <summary>
        /// Sevk edilen miktar
        /// </summary>
        public double Quantity { get; set; }
        
        /// <summary>
        /// Satır açıklaması
        /// </summary>
        public string LineDescription { get; set; }
        
        /// <summary>
        /// Barkod
        /// </summary>
        public string Barcode { get; set; }
    }
}
