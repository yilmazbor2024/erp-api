using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models.Notification
{
    /// <summary>
    /// Kullanıcıların bildirim tercihlerini saklar
    /// </summary>
    public class NotificationPreference
    {
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Kullanıcı kimliği (null ise genel ayar)
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Bildirim türü (CashTransaction, Invoice, Customer, etc.)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string NotificationType { get; set; }
        
        /// <summary>
        /// Bildirim alt türü (Create, Update, Delete, etc.)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ActionType { get; set; }
        
        /// <summary>
        /// Bildirim aktif mi
        /// </summary>
        public bool IsEnabled { get; set; }
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
