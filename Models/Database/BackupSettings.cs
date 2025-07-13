using System;

namespace ErpMobile.Api.Models.Database
{
    /// <summary>
    /// Veritabanı yedekleme ayarları modeli
    /// </summary>
    public class BackupSettings
    {
        /// <summary>
        /// Yedekleme dizini
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// Otomatik yedekleme zamanı (saat:dakika:saniye formatında)
        /// </summary>
        public string ScheduleTime { get; set; }
        
        /// <summary>
        /// Yedeklerin saklanacağı gün sayısı
        /// </summary>
        public int RetentionDays { get; set; }
        
        /// <summary>
        /// Otomatik yedekleme aktif mi?
        /// </summary>
        public bool AutoBackupEnabled { get; set; }
    }
}
