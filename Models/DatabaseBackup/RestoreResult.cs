using System;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// Yedek geri yükleme sonucunu içeren model
    /// </summary>
    public class RestoreResult
    {
        /// <summary>
        /// İşlem başarılı mı
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// İşlem mesajı
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Geri yüklenen veritabanı adı
        /// </summary>
        public string DatabaseName { get; set; }
        
        /// <summary>
        /// İşlem tamamlanma tarihi
        /// </summary>
        public DateTime CompletedAt { get; set; }
    }
}
