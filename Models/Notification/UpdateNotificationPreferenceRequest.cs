using System;

namespace ErpMobile.Api.Models.Notification
{
    /// <summary>
    /// Bildirim tercihi güncelleme isteği
    /// </summary>
    public class UpdateNotificationPreferenceRequest
    {
        /// <summary>
        /// Bildirim türü (CashTransaction, Invoice, Customer, etc.)
        /// </summary>
        public string NotificationType { get; set; }
        
        /// <summary>
        /// Eylem türü (Create, Update, Delete, etc.)
        /// </summary>
        public string ActionType { get; set; }
        
        /// <summary>
        /// Etkin mi?
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
