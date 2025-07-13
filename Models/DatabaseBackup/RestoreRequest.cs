using System;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// Yedek geri yükleme isteği modeli
    /// </summary>
    public class RestoreRequest
    {
        /// <summary>
        /// Veritabanı ID
        /// </summary>
        public Guid DatabaseId { get; set; }
        
        /// <summary>
        /// Yedek ID
        /// </summary>
        public Guid BackupId { get; set; }
    }
}
