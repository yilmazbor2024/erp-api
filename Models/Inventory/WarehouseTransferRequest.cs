using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Inventory
{
    /// <summary>
    /// Depolar arası sevk işlemi için istek modeli
    /// </summary>
    public class WarehouseTransferRequest
    {
        /// <summary>
        /// Kaynak depo kodu
        /// </summary>
        [Required(ErrorMessage = "Kaynak depo kodu zorunludur")]
        public string SourceWarehouseCode { get; set; }
        
        /// <summary>
        /// Hedef depo kodu
        /// </summary>
        [Required(ErrorMessage = "Hedef depo kodu zorunludur")]
        public string TargetWarehouseCode { get; set; }
        
        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// İşlem tarihi (varsayılan: bugün)
        /// </summary>
        public DateTime? OperationDate { get; set; }
        
        /// <summary>
        /// Sevkiyat yöntemi kodu
        /// </summary>
        public string ShipmentMethodCode { get; set; }
        
        /// <summary>
        /// Sevk edilecek ürünler listesi
        /// </summary>
        [Required(ErrorMessage = "En az bir ürün eklenmelidir")]
        public List<WarehouseTransferItemRequest> Items { get; set; }
    }

    /// <summary>
    /// Depolar arası sevk edilecek ürün bilgileri
    /// </summary>
    public class WarehouseTransferItemRequest
    {
        /// <summary>
        /// Ürün kodu
        /// </summary>
        [Required(ErrorMessage = "Ürün kodu zorunludur")]
        public string ItemCode { get; set; }
        
        /// <summary>
        /// Renk kodu
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// Beden kodu
        /// </summary>
        public string ItemDim1Code { get; set; }
        
        /// <summary>
        /// Sevk edilecek miktar
        /// </summary>
        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar 0'dan büyük olmalıdır")]
        public double Quantity { get; set; }
        
        /// <summary>
        /// Satır açıklaması
        /// </summary>
        public string LineDescription { get; set; }
        
        /// <summary>
        /// Barkod (opsiyonel)
        /// </summary>
        public string Barcode { get; set; }
    }
}
