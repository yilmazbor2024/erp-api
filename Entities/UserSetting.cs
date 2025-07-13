using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities
{
    /// <summary>
    /// Kullanıcıya özel ayarlar için entity sınıfı
    /// </summary>
    public class UserSetting
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(450)]
        public string UserId { get; set; } = null!;
        
        [Required]
        [StringLength(50)]
        public string SettingKey { get; set; } = null!;
        
        [Required]
        [Column(TypeName = "ntext")]
        public string SettingValue { get; set; } = null!;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        [StringLength(50)]
        public string CreatedBy { get; set; } = null!;
        
        [StringLength(50)]
        public string? UpdatedBy { get; set; }
        
        // İlişkiler
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}
