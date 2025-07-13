using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Inventory;

namespace ErpMobile.Api.Repositories.Inventory
{
    /// <summary>
    /// Üretim fişi işlemleri için repository arayüzü
    /// </summary>
    public interface IProductionOrderRepository
    {
        /// <summary>
        /// Üretim fişi listesini getirir
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi (opsiyonel)</param>
        /// <param name="endDate">Bitiş tarihi (opsiyonel)</param>
        /// <param name="warehouseCode">Depo kodu (opsiyonel)</param>
        /// <returns>Üretim fişi listesi</returns>
        Task<IEnumerable<ProductionOrderResponse>> GetProductionOrdersAsync(
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string warehouseCode = null);

        /// <summary>
        /// Belirli bir üretim fişini getirir
        /// </summary>
        /// <param name="orderNumber">Üretim fiş numarası</param>
        /// <returns>Üretim fişi detayları</returns>
        Task<ProductionOrderResponse> GetProductionOrderByNumberAsync(string orderNumber);

        /// <summary>
        /// Belirli bir üretim fişinin detaylarını ve kalemlerini getirir
        /// </summary>
        /// <param name="orderNumber">Üretim fiş numarası</param>
        /// <returns>Üretim fişi detayları ve kalemleri</returns>
        Task<ProductionOrderDetailResponse> GetProductionOrderDetailAsync(string orderNumber);

        /// <summary>
        /// Yeni bir üretim fişi oluşturur
        /// </summary>
        /// <param name="request">Üretim fişi bilgileri</param>
        /// <returns>Oluşturulan üretim fişi</returns>
        Task<ProductionOrderResponse> CreateProductionOrderAsync(ProductionOrderRequest request);

        /// <summary>
        /// Yeni üretim fiş numarası oluşturur
        /// </summary>
        /// <returns>Oluşturulan fiş numarası</returns>
        Task<string> GetNextProductionOrderNumberAsync();
        
        /// <summary>
        /// Belirli bir üretim fişinin satır detaylarını getirir
        /// </summary>
        /// <param name="orderNumber">Üretim fiş numarası</param>
        /// <returns>Üretim fişi satır detayları listesi</returns>
        Task<List<ProductionOrderItemResponse>> GetProductionOrderItemsAsync(string orderNumber);

        /// <summary>
        /// Üretim fişi işlemlerinde kullanılacak depo listesini getirir
        /// </summary>
        /// <returns>Depo listesi</returns>
        Task<IEnumerable<WarehouseResponse>> GetWarehousesAsync();
    }
}
