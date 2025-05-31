using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Inventory;

namespace ErpMobile.Api.Repositories.Inventory
{
    public interface IInventoryRepository
    {
        /// <summary>
        /// Çok amaçlı envanter/stok sorgulama metodu
        /// </summary>
        /// <param name="barcode">Barkod (opsiyonel)</param>
        /// <param name="productCode">Ürün kodu (opsiyonel)</param>
        /// <param name="productDescription">Ürün açıklaması (opsiyonel)</param>
        /// <param name="colorCode">Renk kodu (opsiyonel)</param>
        /// <param name="itemDim1Code">Beden kodu (opsiyonel)</param>
        /// <param name="warehouseCode">Depo kodu (opsiyonel)</param>
        /// <param name="showOnlyPositiveStock">Sadece stok miktarı sıfırdan büyük olanları göster (opsiyonel, varsayılan: false)</param>
        /// <returns>Envanter/stok bilgileri listesi</returns>
        Task<List<InventoryStockModel>> GetInventoryStockMultiPurposeAsync(
            string barcode = null,
            string productCode = null,
            string productDescription = null,
            string colorCode = null,
            string itemDim1Code = null,
            string warehouseCode = null,
            bool showOnlyPositiveStock = false);
    }
}
