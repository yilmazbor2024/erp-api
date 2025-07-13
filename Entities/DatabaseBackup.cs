using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities
{
    /// <summary>
    /// Veritabanı yedekleme kaydı
    /// </summary>
    [Table("DatabaseBackups")]
    public class DatabaseBackup
    {
        /// <summary>
        /// Yedek ID
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Veritabanı ID
        /// </summary>
        public Guid DatabaseId { get; set; }

        /// <summary>
        /// Yedekleme tipi (Full, Differential)
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string BackupType { get; set; }

        /// <summary>
        /// Yedekleme dosyasının adı
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string BackupFileName { get; set; }

        /// <summary>
        /// Yedekleme dosyasının tam yolu
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string BackupPath { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        [MaxLength(100)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Yedek dosya boyutu (MB)
        /// </summary>
        public double SizeInMB { get; set; }

        /// <summary>
        /// İlişkili veritabanı
        /// </summary>
        [ForeignKey("DatabaseId")]
        public virtual Database Database { get; set; }
    }
}
