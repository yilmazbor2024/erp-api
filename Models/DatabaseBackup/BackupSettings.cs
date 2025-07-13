using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// Veritabanı yedekleme ayarları modeli
    /// </summary>
    public class BackupSettings
    {
        /// <summary>
        /// Yedekleme dizini
        /// </summary>
        public string BackupPath { get; set; }
        
        /// <summary>
        /// Zamanlanmış yedekleme saati (HH:mm:ss formatında)
        /// </summary>
        public string ScheduleTime { get; set; }
        
        /// <summary>
        /// Yedeklerin saklanacağı gün sayısı
        /// </summary>
        public int RetentionDays { get; set; }
        
        /// <summary>
        /// Otomatik yedekleme aktif mi
        /// </summary>
        public bool AutoBackupEnabled { get; set; }
        
        /// <summary>
        /// Veritabanı başına maksimum yedek sayısı
        /// </summary>
        public int MaxBackupsPerDatabase { get; set; }
        
        /// <summary>
        /// Minimum boş disk alanı (GB)
        /// </summary>
        public double MinimumFreeSpaceGB { get; set; }
        
        /// <summary>
        /// Yedekleme dizini mevcut mu
        /// </summary>
        public bool DirectoryExists { get; set; }
        
        /// <summary>
        /// Kullanılabilir boş alan (GB)
        /// </summary>
        public double AvailableFreeSpaceGB { get; set; }
        
        /// <summary>
        /// Toplam disk alanı (GB)
        /// </summary>
        public double TotalSizeGB { get; set; }
        
        /// <summary>
        /// Toplam yedek sayısı
        /// </summary>
        public int TotalBackupCount { get; set; }
    }
    
    /// <summary>
    /// Yedekleme dizini durum bilgisi
    /// </summary>
    public class BackupPathStatus
    {
        /// <summary>
        /// Dizin yolu
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// Dizin mevcut mu
        /// </summary>
        public bool Exists { get; set; }
        
        /// <summary>
        /// Dizine yazma izni var mı
        /// </summary>
        public bool IsWritable { get; set; }
        
        /// <summary>
        /// Kullanılabilir boş alan (GB)
        /// </summary>
        public double AvailableFreeSpaceGB { get; set; }
        
        /// <summary>
        /// Toplam disk alanı (GB)
        /// </summary>
        public double TotalSizeGB { get; set; }
    }
    
    /// <summary>
    /// Yedekleme istatistikleri
    /// </summary>
    public class BackupStatistics
    {
        /// <summary>
        /// Toplam yedek sayısı
        /// </summary>
        public int TotalBackupCount { get; set; }
        
        /// <summary>
        /// Son 24 saatte alınan yedek sayısı
        /// </summary>
        public int BackupsLast24Hours { get; set; }
        
        /// <summary>
        /// Son 7 günde alınan yedek sayısı
        /// </summary>
        public int BackupsLast7Days { get; set; }
        
        /// <summary>
        /// Son 30 günde alınan yedek sayısı
        /// </summary>
        public int BackupsLast30Days { get; set; }
        
        /// <summary>
        /// Toplam yedek boyutu (MB)
        /// </summary>
        public double TotalBackupSizeMB { get; set; }
        
        /// <summary>
        /// Veritabanı başına yedek sayısı
        /// </summary>
        public Dictionary<string, int> BackupCountPerDatabase { get; set; } = new Dictionary<string, int>();
        
        /// <summary>
        /// Son başarılı yedekleme zamanı
        /// </summary>
        public DateTime? LastSuccessfulBackup { get; set; }
    }
}
