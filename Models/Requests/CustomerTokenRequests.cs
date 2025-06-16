using System;
using System.Collections.Generic;
using ErpMobile.Api.Models.Customer;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Token ile müşteri adres bilgisi ekleme isteği
    /// </summary>
    public class CustomerAddressTokenRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string customerCode { get; set; }

        /// <summary>
        /// Adres bilgisi
        /// </summary>
        public CustomerAddressRequest address { get; set; }
    }

    /// <summary>
    /// Token ile müşteri iletişim bilgisi ekleme isteği
    /// </summary>
    public class CustomerCommunicationTokenRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string customerCode { get; set; }

        /// <summary>
        /// İletişim bilgisi
        /// </summary>
        public CustomerCommunicationRequest communication { get; set; }
    }

    /// <summary>
    /// Token ile müşteri contact bilgisi ekleme isteği
    /// </summary>
    public class CustomerContactTokenRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string customerCode { get; set; }

        /// <summary>
        /// Contact bilgisi
        /// </summary>
        public CustomerContactRequest contact { get; set; }
    }
}
