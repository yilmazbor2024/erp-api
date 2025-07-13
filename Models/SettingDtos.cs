using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models
{
    /// <summary>
    /// Kullanıcı ayarı DTO
    /// </summary>
    public class UserSettingDto
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = null!;
        
        [Required]
        [StringLength(50)]
        public string SettingKey { get; set; } = null!;
        
        [Required]
        public string SettingValue { get; set; } = null!;
        
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
    
    /// <summary>
    /// Kullanıcı ayarı oluşturma/güncelleme DTO
    /// </summary>
    public class UserSettingCreateUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string SettingKey { get; set; } = null!;
        
        [Required]
        public string SettingValue { get; set; } = null!;
        
        public string? Description { get; set; }
    }
    
    /// <summary>
    /// Genel ayar DTO
    /// </summary>
    public class GlobalSettingDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string SettingKey { get; set; } = null!;
        
        [Required]
        public string SettingValue { get; set; } = null!;
        
        public string? Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
    
    /// <summary>
    /// Genel ayar oluşturma/güncelleme DTO
    /// </summary>
    public class GlobalSettingCreateUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string SettingKey { get; set; } = null!;
        
        [Required]
        public string SettingValue { get; set; } = null!;
        
        public string? Description { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
    
    /// <summary>
    /// Barkod ayarları için özel DTO
    /// </summary>
    public class BarcodeSettingsDto
    {
        [Required]
        public string Settings { get; set; } = null!;
        
        public string? Description { get; set; }
    }
}
