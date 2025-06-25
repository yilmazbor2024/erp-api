using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities
{
    [Table("Database")]
    public class Database
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DatabaseName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = null!;

        [StringLength(200)]
        public string? CompanyAddress { get; set; }

        [StringLength(20)]
        public string? CompanyPhone { get; set; }

        [StringLength(100)]
        public string? CompanyEmail { get; set; }

        [StringLength(20)]
        public string? CompanyTaxNumber { get; set; }

        [StringLength(100)]
        public string? CompanyTaxOffice { get; set; }

        [Required]
        [StringLength(500)]
        public string ConnectionString { get; set; } = null!;

        [StringLength(100)]
        public string? ServerName { get; set; }

        [StringLength(50)]
        public string? ServerPort { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedAt { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        // Kullanıcı veritabanı ilişkileri
        public virtual ICollection<UserDatabase> UserDatabases { get; set; } = new List<UserDatabase>();
    }
}
