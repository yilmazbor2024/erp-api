using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Invoice
{
    public class CreateInvoiceRequest
    {
        // Fatura başlık bilgileri
        [Required]
        public string InvoiceNumber { get; set; }
        
        public bool IsReturn { get; set; }
        
        public bool IsEInvoice { get; set; }
        
        
        
        [Required]
        public DateTime InvoiceDate { get; set; }
        
        public string InvoiceTime { get; set; }
        
        [Required]
        public int CurrAccTypeCode { get; set; }
        
        public string VendorCode { get; set; }
        
        public string CustomerCode { get; set; }
        
        public string RetailCustomerCode { get; set; }
        
        public string StoreCurrAccCode { get; set; }
        
        public string EmployeeCode { get; set; }
        
        public string SubCurrAccCode { get; set; }
        
        public int? SubCurrAccID { get; set; }
        
        public bool IsCreditSale { get; set; }
        
        public string ProcessCode { get; set; }
        
        public int? TransTypeCode { get; set; }
        
        /// <summary>
        /// Teslimat adresi ID'si
        /// </summary>
        [Required(ErrorMessage = "Teslimat adresi zorunludur")]
        public Guid ShippingPostalAddressID { get; set; }
        
        /// <summary>
        /// Fatura adresi ID'si
        /// </summary>
        [Required(ErrorMessage = "Fatura adresi zorunludur")]
        public Guid BillingPostalAddressID { get; set; }
        
        /// <summary>
        /// Sevkiyat yöntemi kodu, zorunlu değil
        /// </summary>
        public string ShipmentMethodCode { get; set; }
        
        [Required]
        public string DocCurrencyCode { get; set; }
        
        [Required]
        public string LocalCurrencyCode { get; set; }
        
        [Required]
        public decimal ExchangeRate { get; set; }
        
        public string Series { get; set; }
        
        public string SeriesNumber { get; set; }
        
        public string EInvoiceNumber { get; set; }
        
        [Required]
        public string CompanyCode { get; set; }
        
        public string OfficeCode { get; set; }
        
        public string StoreCode { get; set; }
        
        [Required]
        public string WarehouseCode { get; set; }
        
        public string ImportFileNumber { get; set; }
        
        public string ExportFileNumber { get; set; }
        
        public string ExportTypeCode { get; set; }
        
        public string PosTerminalID { get; set; }
        
        public byte TaxTypeCode { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public bool IsSuspended { get; set; }
        
        public string ApplicationCode { get; set; }
        
        public int? ApplicationID { get; set; }
        
        /// <summary>
        /// Belge tipi (Invoice, Order, Shipment vb.)
        /// Bu alan ApplicationCode ve FormType alanlarının otomatik belirlenmesi için kullanılır
        /// </summary>
        public string DocumentType { get; set; }
        
        public int? FormType { get; set; }
        
        public int? DocumentTypeCode { get; set; }
        
        /// <summary>
        /// Ödeme gün sayısı (vadeli ödeme için)
        /// </summary>
        public int PaymentTerm { get; set; }
        
        public string Notes { get; set; }
        
        /// <summary>
        /// Fatura toplam tutarı (KDV dahil)
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// Fatura net tutarı (KDV hariç)
        /// </summary>
        public decimal NetAmount { get; set; }
        
        /// <summary>
        /// Fatura ara toplam tutarı (indirimler öncesi)
        /// </summary>
        public decimal SubtotalAmount { get; set; }
        
        // Fatura detay bilgileri
        [Required]
        public List<CreateInvoiceDetailRequest> Details { get; set; }
        
        /// <summary>
        /// Fatura başlığı uzantı bilgileri (tpInvoiceHeaderExtension tablosu için)
        /// </summary>
        public InvoiceHeaderExtensionModel InvoiceHeaderExtension { get; set; }
    }

    public class CreateInvoiceDetailRequest
    {
        /// <summary>
        /// Satır numarası (SortOrder)
        /// </summary>
        public int LineNumber { get; set; }
        
        /// <summary>
        /// Ürün kodu
        /// </summary>
        [Required]
        public string ItemCode { get; set; }
        
        /// <summary>
        /// Ürün tipi kodu (1: Normal Ürün, 2: Hizmet, 3: Sabit Kıymet, 4: Masraf)
        /// </summary>
        public byte? ItemTypeCode { get; set; }
        
        /// <summary>
        /// Renk kodu
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// Ürün boyut 1 kodu
        /// </summary>
        public string ItemDim1Code { get; set; }
        
        /// <summary>
        /// Ürün boyut 2 kodu
        /// </summary>
        public string ItemDim2Code { get; set; }
        
        /// <summary>
        /// Ürün boyut 3 kodu
        /// </summary>
        public string ItemDim3Code { get; set; }
        
        /// <summary>
        /// Ölçü birimi kodu
        /// </summary>
        [Required]
        public string UnitOfMeasureCode { get; set; }
        
        /// <summary>
        /// Miktar
        /// </summary>
        [Required]
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Birim fiyat
        /// </summary>
        [Required]
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// İndirim oranı
        /// </summary>
        public decimal DiscountRate { get; set; }
        
        /// <summary>
        /// KDV oranı
        /// </summary>
        public float VatRate { get; set; }
        
        /// <summary>
        /// KDV kodu
        /// </summary>
        public string VatCode { get; set; }
        
        /// <summary>
        /// Tutar
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// İndirim tutarı
        /// </summary>
        public decimal DiscountAmount { get; set; }
        
        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal VatAmount { get; set; }
        
        /// <summary>
        /// Net tutar (KDV hariç)
        /// </summary>
        public decimal NetAmount { get; set; }

         /// <summary>
        /// Net tutar (KDV hariç)
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// Toplam tutar (KDV dahil)
        /// </summary>
        public decimal LineTotalAmount { get; set; }
        
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Fiyat para birimi kodu
        /// </summary>
        public string PriceCurrencyCode { get; set; }
        
        /// <summary>
        /// Döviz kuru
        /// </summary>
        public decimal? ExchangeRate { get; set; }
        
        /// <summary>
        /// Depo kodu
        /// </summary>
        public string WarehouseCode { get; set; }
        
        /// <summary>
        /// Hediye mi?
        /// </summary>
        public bool IsGift { get; set; }
        
        /// <summary>
        /// Promosyon mu?
        /// </summary>
        public bool IsPromotional { get; set; }
        
        /// <summary>
        /// Satış temsilcisi kodu
        /// </summary>
        public string SalesPersonCode { get; set; }
        
        /// <summary>
        /// Ürün tipi kodu
        /// </summary>
        public string ProductTypeCode { get; set; }
        
        /// <summary>
        /// Promosyon kodu
        /// </summary>
        public string PromotionCode { get; set; }
        
        /// <summary>
        /// Parti kodu
        /// </summary>
        public string BatchCode { get; set; }
        
        /// <summary>
        /// Bölüm kodu
        /// </summary>
        public string SectionCode { get; set; }
        
        /// <summary>
        /// Satır açıklaması
        /// </summary>
        public string Description { get; set; }
        
        // Veritabanı eşleşmesi için gerekli özellikler
        public decimal Qty => Quantity;
        
        public string UnitCode => UnitOfMeasureCode;
        
        public string ProductCode => ItemCode;
    }
}
