using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Notification
{
    public class PushNotification
    {
        /// <summary>
        /// Bildirim başlığı
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Bildirim içeriği
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// Bildirim ikonu (URL)
        /// </summary>
        public string Icon { get; set; }
        
        /// <summary>
        /// Bildirim rozeti (URL)
        /// </summary>
        public string Badge { get; set; }
        
        /// <summary>
        /// Bildirime tıklandığında açılacak URL
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// Bildirimle ilgili ek veriler
        /// </summary>
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    }
}
