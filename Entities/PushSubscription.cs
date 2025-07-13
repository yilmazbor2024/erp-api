using System;

namespace ErpMobile.Api.Entities
{
    public class PushSubscription
    {
        /// <summary>
        /// Abonelik ID
        /// </summary>
        public Guid Id { get; set; }
        
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
        
        /// <summary>
        /// Abonelik aktif mi
        /// </summary>
        public bool IsActive { get; set; }
        
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
