using System.Collections.Generic;

namespace ErpMobile.Api.Models.Notification
{
    /// <summary>
    /// Bildirim tercihleri için DTO
    /// </summary>
    public class NotificationPreferenceDto
    {
        /// <summary>
        /// Kullanıcı kimliği (null ise genel ayar)
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Bildirim tercihleri listesi
        /// </summary>
        public List<NotificationTypePreference> Preferences { get; set; } = new List<NotificationTypePreference>();
    }
    
    /// <summary>
    /// Bildirim türü için tercihler
    /// </summary>
    public class NotificationTypePreference
    {
        /// <summary>
        /// Bildirim türü (CashTransaction, Invoice, Customer, etc.)
        /// </summary>
        public string NotificationType { get; set; }
        
        /// <summary>
        /// Bildirim türü için görünen ad
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// Bildirim alt türleri için tercihler
        /// </summary>
        public List<NotificationActionPreference> Actions { get; set; } = new List<NotificationActionPreference>();
    }
    
    /// <summary>
    /// Bildirim alt türü için tercihler
    /// </summary>
    public class NotificationActionPreference
    {
        /// <summary>
        /// Bildirim alt türü (Create, Update, Delete, etc.)
        /// </summary>
        public string ActionType { get; set; }
        
        /// <summary>
        /// Bildirim alt türü için görünen ad
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// Bildirim aktif mi
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
