namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Geçici müşteri kayıt linki oluşturma yanıtı
    /// </summary>
    public class TempLinkResponse
    {
        /// <summary>
        /// İşlem başarılı mı
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Geçici kayıt linki
        /// </summary>
        public string TempLink { get; set; }
        
        
        /// <summary>
        /// Geçerlilik süresi (dakika cinsinden)
        /// </summary>
        public int ExpiryMinutes { get; set; }
        
        /// <summary>
        /// Hata mesajı (başarısız olursa)
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
