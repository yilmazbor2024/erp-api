using System;

namespace ErpMobile.Api.Models.Notification
{
    /// <summary>
    /// Düz yapıda bildirim tercihi DTO'su
    /// </summary>
    public class NotificationPreferenceFlatDto
    {
        /// <summary>
        /// Bildirim türü (CashTransaction, Invoice, Customer, etc.)
        /// </summary>
        public string NotificationType { get; set; }
        
        /// <summary>
        /// Bildirim türü için görünen ad
        /// </summary>
        public string NotificationTypeDisplayName { get; set; }
        
        /// <summary>
        /// Eylem türü (Create, Update, Delete, etc.)
        /// </summary>
        public string ActionType { get; set; }
        
        /// <summary>
        /// Eylem türü için görünen ad
        /// </summary>
        public string ActionTypeDisplayName { get; set; }
        
        /// <summary>
        /// Etkin mi?
        /// </summary>
        public bool IsEnabled { get; set; }
        
        /// <summary>
        /// Kullanıcı tercihi mi?
        /// </summary>
        public bool IsUserPreference { get; set; }
        
        /// <summary>
        /// Global tercih mi?
        /// </summary>
        public bool IsGlobalPreference { get; set; }
    }
}
