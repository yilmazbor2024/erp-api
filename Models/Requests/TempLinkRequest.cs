using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Geçici müşteri kayıt linki oluşturma isteği
    /// </summary>
    public class TempLinkRequest
    {
        /// <summary>
        /// Müşteri kodu (opsiyonel, boş ise yeni müşteri kaydı için link oluşturulur)
        /// </summary>
        [StringLength(50, ErrorMessage = "Müşteri kodu en fazla 50 karakter olabilir.")]
        public string CustomerCode { get; set; } = string.Empty;
        
        /// <summary>
        /// Linkin geçerlilik süresi (dakika cinsinden)
        /// </summary>
        public int ExpiryMinutes { get; set; } = 10;
    }
}
