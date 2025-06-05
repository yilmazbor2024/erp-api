using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Services.Interfaces
{
    /// <summary>
    /// Token doğrulama servisi arayüzü
    /// </summary>
    public interface ITokenValidationService
    {
        /// <summary>
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <returns>Token doğrulama sonucu</returns>
        Task<TokenValidationResponse> ValidateTokenAsync(string token);
        
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
    }
}
