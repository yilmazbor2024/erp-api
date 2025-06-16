using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Services.Interfaces
{
    /// <summary>
    /// Token kullanım kapsamı
    /// </summary>
    public enum TokenScope
    {
        /// <summary>
        /// Müşteri kayıt formu
        /// </summary>
        CustomerRegistration,
        
        /// <summary>
        /// Şifre sıfırlama
        /// </summary>
        PasswordReset,
        
        /// <summary>
        /// E-posta doğrulama
        /// </summary>
        EmailVerification
    }
    
    /// <summary>
    /// Token doğrulama servisi arayüzü
    /// </summary>
    public interface ITokenValidationService
    {
        /// <summary>
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder (senkron)
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <returns>Token geçerli mi</returns>
        bool ValidateToken(string token);
        
        /// <summary>
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder (senkron)
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <param name="requiredScope">Gereken token kapsamı</param>
        /// <returns>Token geçerli mi</returns>
        bool ValidateToken(string token, TokenScope requiredScope);
        
        /// <summary>
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <returns>Token doğrulama sonucu</returns>
        Task<TokenValidationResponse> ValidateTokenAsync(string token);
        
        /// <summary>
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <param name="requiredScope">Gereken token kapsamı</param>
        /// <returns>Token doğrulama sonucu</returns>
        Task<TokenValidationResponse> ValidateTokenAsync(string token, TokenScope requiredScope);
        
        /// <summary>
        /// Token'ı kullanıldı olarak işaretler
        /// </summary>
        /// <param name="token">İşaretlenecek token</param>
        /// <returns>İşlem başarılı mı</returns>
        Task<bool> MarkTokenAsUsedAsync(string token);
        
        /// <summary>
        /// Token'ı kullanıldı olarak işaretler ve müşteri kodunu kaydeder
        /// </summary>
        /// <param name="token">İşaretlenecek token</param>
        /// <param name="customerCode">Kaydedilecek müşteri kodu</param>
        /// <returns>İşlem başarılı mı</returns>
        Task<bool> MarkTokenAsUsedAsync(string token, string customerCode);
        
        /// <summary>
        /// Müşteri kodu için yeni bir token oluşturur
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Oluşturulan token</returns>
        Task<string> GenerateTokenAsync(string customerCode);
    }
}
