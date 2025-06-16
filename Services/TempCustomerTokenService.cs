using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        
        // Rate limiting için IP bazlı istek sayacı
        private readonly Dictionary<string, (int Count, DateTime FirstRequest)> _ipRequests = new Dictionary<string, (int, DateTime)>();
        private readonly object _lockObject = new object();

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
                    IsNewCustomer = string.IsNullOrEmpty(request.CustomerCode),
                    Scope = TokenScope.CustomerRegistration // Varsayılan olarak CustomerRegistration atama
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
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder (senkron)
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <returns>Token geçerli mi</returns>
        public bool ValidateToken(string token)
        {
            try
            {
                // Rate limiting kontrolü
                string ipAddress = GetClientIpAddress();
                if (!CheckRateLimit(ipAddress))
                {
                    _logger.LogWarning("Rate limit aşıldı. IP: {IpAddress}", ipAddress);
                    return false;
                }
                
                // Asenkron metodu senkron olarak çağır
                var result = ValidateTokenAsync(token).GetAwaiter().GetResult();
                return result.IsValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token doğrulanırken hata oluştu: {ErrorMessage}", ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder (senkron)
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <param name="requiredScope">Gereken token kapsamı</param>
        /// <returns>Token geçerli mi</returns>
        public bool ValidateToken(string token, TokenScope requiredScope)
        {
            try
            {
                // Asenkron metodu senkron olarak çağır
                var result = ValidateTokenAsync(token, requiredScope).GetAwaiter().GetResult();
                return result.IsValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token doğrulanırken hata oluştu: {ErrorMessage}", ex.Message);
                return false;
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
        /// Token'ı doğrular ve geçerli olup olmadığını kontrol eder
        /// </summary>
        /// <param name="token">Doğrulanacak token</param>
        /// <param name="requiredScope">Gereken token kapsamı</param>
        /// <returns>Token doğrulama sonucu</returns>
        public async Task<TokenValidationResponse> ValidateTokenAsync(string token, TokenScope requiredScope)
        {
            // Önce normal doğrulama yap
            var result = await ValidateTokenAsync(token);
            
            if (!result.IsValid)
                return result;
                
            // Token'ı veritabanından al
            var tempToken = await _context.Set<TempCustomerToken>()
                .FirstOrDefaultAsync(t => t.Token == token);
                
            // Scope kontrolü yap
            // TempCustomerToken.Scope alanını kullan
            TokenScope tokenScope = tempToken.Scope;
            
            if (tokenScope != requiredScope)
            {
                return new TokenValidationResponse
                {
                    IsValid = false,
                    Message = "Bu token bu işlem için geçerli değil."
                };
            }
            
            return result;
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
        
        /// <summary>
        /// Rate limiting kontrolü yapar
        /// </summary>
        /// <param name="ipAddress">Kontrol edilecek IP adresi</param>
        /// <returns>Rate limit aşılmadıysa true</returns>
        private bool CheckRateLimit(string ipAddress)
        {
            lock (_lockObject)
            {
                // IP adresini temizle
                if (string.IsNullOrEmpty(ipAddress))
                    return true;
                    
                // Mevcut zaman
                var now = DateTime.UtcNow;
                
                // IP adresi için istek sayısını kontrol et
                if (_ipRequests.TryGetValue(ipAddress, out var requestInfo))
                {
                    // Son 1 dakika içindeki istekleri kontrol et
                    if ((now - requestInfo.FirstRequest).TotalMinutes < 1)
                    {
                        // 1 dakikada en fazla 10 istek
                        if (requestInfo.Count >= 10)
                        {
                            _logger.LogWarning("Rate limit aşıldı. IP: {IpAddress}", ipAddress);
                            return false;
                        }
                        
                        // İstek sayısını artır
                        _ipRequests[ipAddress] = (requestInfo.Count + 1, requestInfo.FirstRequest);
                    }
                    else
                    {
                        // Süre dolmuş, yeni istek sayacı başlat
                        _ipRequests[ipAddress] = (1, now);
                    }
                }
                else
                {
                    // İlk istek
                    _ipRequests[ipAddress] = (1, now);
                }
                
                return true;
            }
        }
        
        /// <summary>
        /// İstemcinin IP adresini alır
        /// </summary>
        /// <returns>IP adresi</returns>
        private string GetClientIpAddress()
        {
            // HttpContext'e erişim olmadığı için şu an boş dönüyör
            // Gerçek implementasyonda HttpContext.Connection.RemoteIpAddress kullanılmalı
            // veya IHttpContextAccessor enjekte edilmeli
            return "127.0.0.1";
        }
        
        /// <summary>
        /// Müşteri kodu için yeni bir token oluşturur
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Oluşturulan token</returns>
        public async Task<string> GenerateTokenAsync(string customerCode)
        {
            try
            {
                _logger.LogInformation($"[TempCustomerTokenService.GenerateTokenAsync] - Müşteri için token oluşturuluyor: {customerCode}");
                
                if (string.IsNullOrEmpty(customerCode))
                {
                    throw new ArgumentException("Müşteri kodu boş olamaz", nameof(customerCode));
                }
                
                // Benzersiz token oluştur
                string token = GenerateSecureToken();
                
                // Geçerlilik süresi (varsayılan 60 dakika)
                int expiryMinutes = 60;
                
                // Token veritabanına kaydet
                var tempToken = new TempCustomerToken
                {
                    Token = token,
                    CustomerCode = customerCode,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes),
                    IsUsed = false,
                    CreatedByUserId = "SYSTEM",
                    CreatedByUserName = "SYSTEM",
                    IpAddress = GetClientIpAddress(),
                    UserAgent = "API",
                    IsNewCustomer = false,
                    Scope = TokenScope.CustomerRegistration
                };
                
                _context.Add(tempToken);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation($"[TempCustomerTokenService.GenerateTokenAsync] - Müşteri için token oluşturuldu: {customerCode}");
                
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[TempCustomerTokenService.GenerateTokenAsync] - Müşteri için token oluşturulurken hata: {CustomerCode}", customerCode);
                throw;
            }
        }
    }
}
