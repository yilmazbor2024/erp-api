using System;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Müşteri token isteği için model
    /// </summary>
    public class CustomerTokenRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }
    }
}
