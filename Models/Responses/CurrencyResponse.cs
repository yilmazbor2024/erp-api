using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Para birimi yanıt modeli
    /// </summary>
    public class CurrencyResponse
    {
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Para birimi açıklaması
        /// </summary>
        public string CurrencyDescription { get; set; }

        /// <summary>
        /// Dil kodu
        /// </summary>
        public string LangCode { get; set; }

        /// <summary>
        /// Bloke durumu
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 