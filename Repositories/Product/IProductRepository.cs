using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Product;

namespace ErpMobile.Api.Repositories.Product
{
    public interface IProductRepository
    {
        Task<List<ProductVariantModel>> GetProductVariantsByBarcodeAsync(string barcode);
        
        /// <summary>
        /// Ürün kodu veya açıklaması ile ürün varyantlarını arar
        /// </summary>
        /// <param name="searchText">Ürün kodu veya açıklaması</param>
        /// <returns>Ürün varyant listesi</returns>
        Task<List<ProductVariantModel>> GetProductVariantsByProductCodeOrDescriptionAsync(string searchText);
        
        Task<List<ProductPriceListModel>> GetProductPriceListAsync(string productCode);
        
        /// <summary>
        /// Tüm ürün fiyat listesini getirir
        /// </summary>
        /// <param name="page">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="companyCode">Şirket kodu</param>
        /// <param name="itemCode">Ürün kodu (opsiyonel)</param>
        /// <returns>Ürün fiyat listesi detayları</returns>
        Task<List<ProductPriceListDetailModel>> GetAllProductPriceListAsync(
            int page = 1, 
            int pageSize = 50, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            int companyCode = 1,
            string itemCode = null);
            
        /// <summary>
        /// Ürün tiplerini getirir
        /// </summary>
        /// <returns>Ürün tipleri listesi</returns>
        Task<List<ProductTypeModel>> GetProductTypesAsync();
        
        /// <summary>
        /// Ölçü birimlerini getirir
        /// </summary>
        /// <returns>Ölçü birimleri listesi</returns>
        Task<List<UnitOfMeasureModel>> GetUnitsOfMeasureAsync();
    }
}
