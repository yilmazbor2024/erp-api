using System;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// Yedekleme işlemi sonucunu içeren model
    /// </summary>
    public class BackupResult
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
        /// Yedek dosyası adı
        /// </summary>
        public string BackupFileName { get; set; }
        
        /// <summary>
        /// Yedek dosyası tam yolu
        /// </summary>
        public string BackupPath { get; set; }
        
        /// <summary>
        /// Yedek tipi (Full, Differential)
        /// </summary>
        public string BackupType { get; set; }
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Yedek dosyası boyutu (MB)
        /// </summary>
        public double SizeInMB { get; set; }
    }
}
