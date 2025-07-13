using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Database
{
    /// <summary>
    /// Veritabanı yedekleme sonucu
    /// </summary>
    public class BackupResult
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// İşlem mesajı
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Yedekleme dosyasının adı
        /// </summary>
        public string BackupFileName { get; set; }

        /// <summary>
        /// Yedekleme dosyasının tam yolu
        /// </summary>
        public string BackupPath { get; set; }

        /// <summary>
        /// Yedekleme tipi (Full, Differential)
        /// </summary>
        public string BackupType { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Veritabanı yedeği DTO
    /// </summary>
    public class DatabaseBackupDto
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
        /// Yedekleme tipi (Full, Differential)
        /// </summary>
        public string BackupType { get; set; }

        /// <summary>
        /// Yedekleme dosyasının adı
        /// </summary>
        public string BackupFileName { get; set; }

        /// <summary>
        /// Yedekleme dosyasının tam yolu
        /// </summary>
        public string BackupPath { get; set; }

        /// <summary>
        /// Dosya boyutu (MB)
        /// </summary>
        public double FileSizeInMB { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public string CreatedBy { get; set; }
    }

    /// <summary>
    /// Yedekleme isteği
    /// </summary>
    public class BackupRequest
    {
        /// <summary>
        /// Veritabanı ID
        /// </summary>
        public Guid DatabaseId { get; set; }

        /// <summary>
        /// Yedekleme tipi (Full, Differential)
        /// </summary>
        public string BackupType { get; set; }
    }

    /// <summary>
    /// Geri yükleme isteği
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

    /// <summary>
    /// SQL sorgu isteği
    /// </summary>
    public class QueryRequest
    {
        /// <summary>
        /// Veritabanı ID
        /// </summary>
        public Guid DatabaseId { get; set; }

        /// <summary>
        /// SQL sorgusu
        /// </summary>
        public string SqlQuery { get; set; }
    }

    /// <summary>
    /// SQL sorgu sonucu
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// İşlem mesajı
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Sorgu sonuçları
        /// </summary>
        public List<Dictionary<string, object>> Data { get; set; }

        /// <summary>
        /// Etkilenen satır sayısı
        /// </summary>
        public int RowsAffected { get; set; }

        /// <summary>
        /// Sorgu çalışma süresi (ms)
        /// </summary>
        public double ExecutionTime { get; set; }
    }

    /// <summary>
    /// Veritabanı tablosu DTO
    /// </summary>
    public class DatabaseTableDto
    {
        /// <summary>
        /// Şema adı
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Tablo adı
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Kolon sayısı
        /// </summary>
        public int ColumnCount { get; set; }

        /// <summary>
        /// Satır sayısı
        /// </summary>
        public long RowCount { get; set; }
    }
}
