using System;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// Yedek bilgilerini içeren model
    /// </summary>
    public class BackupInfo
    {
        /// <summary>
        /// Yedek ID
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Veritabanı ID
        /// </summary>
        public Guid DatabaseId { get; set; }
        
        /// <summary>
        /// Yedek adı
        /// </summary>
        public string BackupName { get; set; }
        
        /// <summary>
        /// Yedek tipi (Full, Differential)
        /// </summary>
        public string BackupType { get; set; }
        
        /// <summary>
        /// Yedek dosyası yolu
        /// </summary>
        public string BackupPath { get; set; }
        
        /// <summary>
        /// Yedek boyutu (MB)
        /// </summary>
        public double SizeInMB { get; set; }
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
