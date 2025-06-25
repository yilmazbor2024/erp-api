using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities
{
    [Table("UserDatabase")]
    public class UserDatabase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public Guid DatabaseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = null!; // "admin" veya "user"

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedAt { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        // Kullanıcı ile ilişki
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        // Veritabanı ile ilişki
        [ForeignKey("DatabaseId")]
        public virtual Database Database { get; set; } = null!;
    }
}
