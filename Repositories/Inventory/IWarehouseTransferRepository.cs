using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Inventory;

namespace ErpMobile.Api.Repositories.Inventory
{
    /// <summary>
    /// Depolar arası sevk işlemleri için repository interface
    /// </summary>
    public interface IWarehouseTransferRepository
    {
        /// <summary>
        /// Depolar arası sevk listesini getirir
        /// </summary>
        /// <param name="sourceWarehouseCode">Kaynak depo kodu (opsiyonel)</param>
        /// <param name="targetWarehouseCode">Hedef depo kodu (opsiyonel)</param>
        /// <param name="startDate">Başlangıç tarihi (opsiyonel)</param>
        /// <param name="endDate">Bitiş tarihi (opsiyonel)</param>
        /// <returns>Depolar arası sevk listesi</returns>
        Task<List<WarehouseTransferResponse>> GetWarehouseTransfersAsync(
            string sourceWarehouseCode = null, 
            string targetWarehouseCode = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null);

        /// <summary>
        /// Belirli bir sevk kaydını getirir
        /// </summary>
        /// <param name="transferNumber">Sevk fiş numarası</param>
        /// <returns>Sevk kaydı detayları</returns>
        Task<WarehouseTransferDetailResponse> GetWarehouseTransferByNumberAsync(string transferNumber);

        /// <summary>
        /// Yeni bir depolar arası sevk kaydı oluşturur
        /// </summary>
        /// <param name="request">Sevk bilgileri</param>
        /// <param name="userName">İşlemi yapan kullanıcı</param>
        /// <returns>Oluşturulan sevk fiş numarası</returns>
        Task<string> CreateWarehouseTransferAsync(WarehouseTransferRequest request, string userName);

        /// <summary>
        /// Sevk işlemini onaylar
        /// </summary>
        /// <param name="transferNumber">Sevk fiş numarası</param>
        /// <param name="userName">İşlemi yapan kullanıcı</param>
        /// <returns>İşlem başarılı mı?</returns>
        Task<bool> ApproveWarehouseTransferAsync(string transferNumber, string userName);

        /// <summary>
        /// Sevk işlemini iptal eder
        /// </summary>
        /// <param name="transferNumber">Sevk fiş numarası</param>
        /// <param name="userName">İşlemi yapan kullanıcı</param>
        /// <returns>İşlem başarılı mı?</returns>
        Task<bool> CancelWarehouseTransferAsync(string transferNumber, string userName);
        
        /// <summary>
        /// Yeni sevk fiş numarası oluşturur
        /// </summary>
        /// <returns>Oluşturulan fiş numarası</returns>
        Task<string> GenerateTransferNumberAsync();
        
        /// <summary>
        /// Depolar arası sevk işlemlerinde kullanılacak depo listesini getirir
        /// </summary>
        /// <returns>Depo listesi</returns>
        Task<List<WarehouseResponse>> GetWarehousesAsync();
        
        /// <summary>
        /// Sevk kaydını düzenleme için kilitler
        /// </summary>
        /// <param name="transferNumber">Sevk fiş numarası</param>
        /// <param name="userName">İşlemi yapan kullanıcı</param>
        /// <param name="comment">Kilitleme nedeni açıklaması (opsiyonel)</param>
        /// <returns>Kilitleme başarılı mı?</returns>
        Task<bool> LockWarehouseTransferAsync(string transferNumber, string userName, string comment = null);
        
        /// <summary>
        /// Sevk kaydının kilidini kaldırır
        /// </summary>
        /// <param name="transferNumber">Sevk fiş numarası</param>
        /// <param name="userName">İşlemi yapan kullanıcı</param>
        /// <returns>Kilit kaldırma başarılı mı?</returns>
        Task<bool> UnlockWarehouseTransferAsync(string transferNumber, string userName);
        
        /// <summary>
        /// Sevk kaydının kilit durumunu kontrol eder
        /// </summary>
        /// <param name="transferNumber">Sevk fiş numarası</param>
        /// <returns>Kilit durumu bilgisi</returns>
        Task<(bool IsLocked, string LockedByUser, DateTime? LockDate)> CheckWarehouseTransferLockStatusAsync(string transferNumber);
    }
}
