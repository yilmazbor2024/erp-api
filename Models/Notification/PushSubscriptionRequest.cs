using System;

namespace ErpMobile.Api.Models.Notification
{
    public class PushSubscriptionRequest
    {
        /// <summary>
        /// Push bildirimi için endpoint URL'i
        /// </summary>
        public string Endpoint { get; set; }
        
        /// <summary>
        /// P256DH anahtarı (VAPID şifreleme için)
        /// </summary>
        public string P256dh { get; set; }
        
        /// <summary>
        /// Auth anahtarı (VAPID şifreleme için)
        /// </summary>
        public string Auth { get; set; }
        
        /// <summary>
        /// Kullanıcı tarayıcı bilgisi
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// Kullanıcı ID (opsiyonel)
        /// </summary>
        public Guid? UserId { get; set; }
    }
}
