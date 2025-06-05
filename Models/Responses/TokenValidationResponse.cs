namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Token doğrulama yanıtı
    /// </summary>
    public class TokenValidationResponse
    {
        /// <summary>
        /// Token geçerli mi
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// Doğrulama mesajı
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Müşteri kodu (token geçerliyse)
        /// </summary>
        public string CustomerCode { get; set; }
    }
}
