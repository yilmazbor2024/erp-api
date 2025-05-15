using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Satış faturası yanıt modeli
    /// </summary>
    public class SalesInvoiceResponse
    {
        /// <summary>
        /// Fatura ID
        /// </summary>
        public Guid InvoiceId { get; set; }

        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Fatura tarihi
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Teslimat tarihi
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Vade tarihi
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Fatura durumu
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Toplam tutar
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// KDV hariç toplam tutar
        /// </summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal VatAmount { get; set; }

        /// <summary>
        /// İndirim tutarı
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Ödeme planı kodu
        /// </summary>
        public string PaymentPlanCode { get; set; }

        /// <summary>
        /// Ödeme planı adı
        /// </summary>
        public string PaymentPlanName { get; set; }

        /// <summary>
        /// Ofis kodu
        /// </summary>
        public string OfficeCode { get; set; }

        /// <summary>
        /// Ofis adı
        /// </summary>
        public string OfficeName { get; set; }

        /// <summary>
        /// Depo kodu
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Depo adı
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Referans no
        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Oluşturma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Fatura satır kalemleri
        /// </summary>
        public List<SalesInvoiceLineResponse> Lines { get; set; } = new List<SalesInvoiceLineResponse>();

        /// <summary>
        /// Fatura öznitelikleri
        /// </summary>
        public List<InvoiceAttributeResponse> Attributes { get; set; } = new List<InvoiceAttributeResponse>();
    }

    /// <summary>
    /// Satış faturası satır kalemi yanıt modeli
    /// </summary>
    public class SalesInvoiceLineResponse
    {
        /// <summary>
        /// Satır ID
        /// </summary>
        public Guid LineId { get; set; }

        /// <summary>
        /// Stok kodu
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Stok adı
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Miktar
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Birim fiyat
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Satır toplam tutar
        /// </summary>
        public decimal LineAmount { get; set; }

        /// <summary>
        /// KDV hariç satır tutar
        /// </summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// İndirim oranı
        /// </summary>
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// İndirim tutarı
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// KDV oranı
        /// </summary>
        public decimal VatRate { get; set; }

        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal VatAmount { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Birim kodu
        /// </summary>
        public string UnitCode { get; set; }

        /// <summary>
        /// Birim adı
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Stok boyut bilgileri
        /// </summary>
        public List<ItemDimensionResponse> Dimensions { get; set; } = new List<ItemDimensionResponse>();
    }

    /// <summary>
    /// Fatura özniteliği yanıt modeli
    /// </summary>
    public class InvoiceAttributeResponse
    {
        /// <summary>
        /// Öznitelik ID
        /// </summary>
        public Guid AttributeId { get; set; }

        /// <summary>
        /// Öznitelik tipi kodu
        /// </summary>
        public string AttributeTypeCode { get; set; }

        /// <summary>
        /// Öznitelik tipi adı
        /// </summary>
        public string AttributeTypeName { get; set; }

        /// <summary>
        /// Öznitelik değeri
        /// </summary>
        public string AttributeValue { get; set; }
    }

    /// <summary>
    /// Stok boyut bilgisi yanıt modeli
    /// </summary>
    public class ItemDimensionResponse
    {
        /// <summary>
        /// Boyut ID
        /// </summary>
        public Guid DimensionId { get; set; }

        /// <summary>
        /// Boyut tipi kodu
        /// </summary>
        public string DimensionTypeCode { get; set; }

        /// <summary>
        /// Boyut tipi adı
        /// </summary>
        public string DimensionTypeName { get; set; }

        /// <summary>
        /// Boyut değeri
        /// </summary>
        public string DimensionValue { get; set; }

        /// <summary>
        /// Boyut değeri açıklaması
        /// </summary>
        public string DimensionValueDescription { get; set; }
    }
} 