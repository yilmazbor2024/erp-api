using System;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// Veritabanı bilgilerini içeren model
    /// </summary>
    public class DatabaseInfo
    {
        /// <summary>
        /// Veritabanı ID
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Veritabanı adı
        /// </summary>
        public string DatabaseName { get; set; }
        
        /// <summary>
        /// Veritabanı boyutu (MB)
        /// </summary>
        public double SizeMB { get; set; }
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Son değiştirilme tarihi
        /// </summary>
        public DateTime LastModified { get; set; }
        
        /// <summary>
        /// Veritabanı uyumluluğu
        /// </summary>
        public string Compatibility { get; set; }
        
        /// <summary>
        /// Veritabanı durumu
        /// </summary>
        public string Status { get; set; }
    }
}
