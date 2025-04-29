using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    /// <summary>
    /// Satış faturası oluşturma isteği modeli
    /// </summary>
    public class SalesInvoiceCreateRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [Required]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Fatura tarihi
        /// </summary>
        [Required]
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
        /// Ödeme planı kodu
        /// </summary>
        public string PaymentPlanCode { get; set; }

        /// <summary>
        /// Ofis kodu
        /// </summary>
        [Required]
        public string OfficeCode { get; set; }

        /// <summary>
        /// Depo kodu
        /// </summary>
        [Required]
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Referans no
        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// Satır kalemleri
        /// </summary>
        [Required]
        public List<SalesInvoiceLineCreateRequest> Lines { get; set; } = new List<SalesInvoiceLineCreateRequest>();

        /// <summary>
        /// Fatura öznitelikleri
        /// </summary>
        public List<InvoiceAttributeCreateRequest> Attributes { get; set; } = new List<InvoiceAttributeCreateRequest>();
    }

    /// <summary>
    /// Satış faturası satır kalemi oluşturma isteği modeli
    /// </summary>
    public class SalesInvoiceLineCreateRequest
    {
        /// <summary>
        /// Stok kodu
        /// </summary>
        [Required]
        public string ItemCode { get; set; }

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
        public decimal VatRate { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Birim kodu
        /// </summary>
        [Required]
        public string UnitCode { get; set; }

        /// <summary>
        /// Boyut bilgileri
        /// </summary>
        public List<ItemDimensionCreateRequest> Dimensions { get; set; } = new List<ItemDimensionCreateRequest>();
    }

    /// <summary>
    /// Fatura özniteliği oluşturma isteği modeli
    /// </summary>
    public class InvoiceAttributeCreateRequest
    {
        /// <summary>
        /// Öznitelik tipi kodu
        /// </summary>
        [Required]
        public string AttributeTypeCode { get; set; }

        /// <summary>
        /// Öznitelik değeri
        /// </summary>
        [Required]
        public string AttributeValue { get; set; }
    }

    /// <summary>
    /// Stok boyut bilgisi oluşturma isteği modeli
    /// </summary>
    public class ItemDimensionCreateRequest
    {
        /// <summary>
        /// Boyut tipi kodu
        /// </summary>
        [Required]
        public string DimensionTypeCode { get; set; }

        /// <summary>
        /// Boyut değeri
        /// </summary>
        [Required]
        public string DimensionValue { get; set; }
    }
} 