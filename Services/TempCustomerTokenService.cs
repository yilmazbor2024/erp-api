using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using ErpMobile.Api.Data;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Entities;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    public class TempCustomerTokenService : ITokenValidationService
    {
        private readonly NanoServiceDbContext _context;
        private readonly ILogger<TempCustomerTokenService> _logger;
        private readonly IConfiguration _configuration;

        public TempCustomerTokenService(
            NanoServiceDbContext context,
            ILogger<TempCustomerTokenService> logger,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Geçici müşteri kayıt linki oluşturur
        /// </summary>
        /// <param name="request">Geçici link oluşturma isteği</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        /// <param name="ipAddress">İstek yapılan IP adresi</param>
        /// <param name="userAgent">İstek yapılan tarayıcı bilgisi</param>
        /// <returns>Geçici link ve QR kod URL'si</returns>
        public async Task<TempLinkResponse> CreateTempLinkAsync(
            TempLinkRequest request, 
            string userId, 
            string userName, 
            string ipAddress, 
            string userAgent)
        {
            try
            {
                // Geçerlilik süresi (varsayılan 10 dakika)
                int expiryMinutes = request.ExpiryMinutes > 0 ? request.ExpiryMinutes : 10;
                
                // Benzersiz token oluştur
                string token = GenerateSecureToken();
                
                // Token veritabanına kaydet
                var tempToken = new TempCustomerToken
                {
                    Token = token,
                    CustomerCode = string.IsNullOrEmpty(request.CustomerCode) ? string.Empty : request.CustomerCode,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes),
                    IsUsed = false,
                    CreatedByUserId = userId,
                    CreatedByUserName = userName,
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    IsNewCustomer = string.IsNullOrEmpty(request.CustomerCode)
                };
                
                _context.Add(tempToken);
                await _context.SaveChangesAsync();
                
                // Frontend URL'sini yapılandırmadan al
                string frontendUrl = _configuration["AppSettings:FrontendUrl"] ?? "http://localhost:3000";
                
                // Geçici kayıt linki oluştur
                string tempLink = $"{frontendUrl}/customer-registration?token={token}";
                
                return new TempLinkResponse
                {
                    Success = true,
                    TempLink = tempLink,
                    ExpiryMinutes = expiryMinutes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Geçici link oluşturulurken hata oluştu: {ErrorMessage}", ex.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <returns>Token doğrulama sonucu</returns>
        public async Task<TokenValidationResponse> ValidateTokenAsync(string token)
        {
            try
            {
                // Token'ı veritabanında ara
                var tempToken = await _context.Set<TempCustomerToken>()
                    .FirstOrDefaultAsync(t => t.Token == token);
                
                if (tempToken == null)
                {
                    return new TokenValidationResponse
                    {
                        IsValid = false,
                        Message = "Geçersiz token."
                    };
                }
                
                // Token kullanılmış mı kontrol et
                if (tempToken.IsUsed)
                {
                    return new TokenValidationResponse
                    {
                        IsValid = false,
                        Message = "Bu token daha önce kullanılmış."
                    };
                }
                
                // Token süresi dolmuş mu kontrol et
                if (tempToken.ExpiresAt < DateTime.UtcNow)
                {
                    return new TokenValidationResponse
                    {
                        IsValid = false,
                        Message = "Token süresi dolmuş."
                    };
                }
                
                // Token geçerli, müşteri kodunu döndür
                return new TokenValidationResponse
                {
                    IsValid = true,
                    Message = "Token geçerli.",
                    CustomerCode = tempToken.CustomerCode
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token doğrulanırken hata oluştu: {ErrorMessage}", ex.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Token'ı kullanıldı olarak işaretler
        /// </summary>
        /// <param name="token">İşaretlenecek token</param>
        /// <returns>İşlem başarılı mı</returns>
        public async Task<bool> MarkTokenAsUsedAsync(string token)
        {
            try
            {
                var tempToken = await _context.Set<TempCustomerToken>()
                    .FirstOrDefaultAsync(t => t.Token == token);
                
                if (tempToken == null)
                {
                    return false;
                }
                
                tempToken.IsUsed = true;
                tempToken.UsedAt = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token kullanıldı olarak işaretlenirken hata oluştu: {ErrorMessage}", ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// Token'ı kullanıldı olarak işaretler ve müşteri kodunu kaydeder
        /// </summary>
        /// <param name="token">İşaretlenecek token</param>
        /// <param name="customerCode">Kaydedilecek müşteri kodu</param>
        /// <returns>İşlem başarılı mı</returns>
        public async Task<bool> MarkTokenAsUsedAsync(string token, string customerCode)
        {
            try
            {
                var tempToken = await _context.Set<TempCustomerToken>()
                    .FirstOrDefaultAsync(t => t.Token == token);
                
                if (tempToken == null)
                {
                    return false;
                }
                
                tempToken.IsUsed = true;
                tempToken.UsedAt = DateTime.UtcNow;
                tempToken.CustomerCode = customerCode;
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token kullanıldı olarak işaretlenirken hata oluştu: {ErrorMessage}", ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// Güvenli bir token oluşturur
        /// </summary>
        /// <returns>Benzersiz token</returns>
        private string GenerateSecureToken()
        {
            byte[] randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            
            return Convert.ToBase64String(randomBytes)
                .Replace("/", "_")
                .Replace("+", "-")
                .Replace("=", "")
                .Substring(0, 32);
        }
    }
}
