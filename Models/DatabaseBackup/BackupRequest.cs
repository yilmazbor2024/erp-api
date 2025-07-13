using System;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// Yedekleme isteği modeli
    /// </summary>
    public class BackupRequest
    {
        /// <summary>
        /// Veritabanı ID
        /// </summary>
        public Guid DatabaseId { get; set; }
        
        /// <summary>
        /// Yedekleme açıklaması (opsiyonel)
        /// </summary>
        public string Description { get; set; }
    }
}
