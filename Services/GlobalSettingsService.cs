using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    public interface IGlobalSettingsService
    {
        Task<T> GetSettingAsync<T>(string key, T defaultValue);
        Task<bool> SaveSettingAsync<T>(string key, T value, string username);
    }

    public class GlobalSettingsService : IGlobalSettingsService
    {
        private readonly NanoServiceDbContext _context;
        private readonly ILogger<GlobalSettingsService> _logger;

        public GlobalSettingsService(
            NanoServiceDbContext context,
            ILogger<GlobalSettingsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Belirtilen anahtara sahip ayarı alır
        /// </summary>
        /// <typeparam name="T">Ayarın dönüştürüleceği tip</typeparam>
        /// <param name="key">Ayar anahtarı</param>
        /// <param name="defaultValue">Ayar bulunamazsa kullanılacak varsayılan değer</param>
        /// <returns>Ayar değeri veya varsayılan değer</returns>
        public async Task<T> GetSettingAsync<T>(string key, T defaultValue)
        {
            try
            {
                var setting = await _context.GlobalSettings
                    .Where(s => s.SettingKey == key && s.IsActive)
                    .FirstOrDefaultAsync();

                if (setting == null)
                {
                    _logger.LogInformation("Ayar bulunamadı: {Key}, varsayılan değer kullanılıyor.", key);
                    return defaultValue;
                }

                try
                {
                    // Basit tipler için doğrudan dönüştürme deneyin
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)setting.SettingValue;
                    }
                    else if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
                    {
                        if (int.TryParse(setting.SettingValue, out int intValue))
                        {
                            return (T)(object)intValue;
                        }
                    }
                    else if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
                    {
                        if (bool.TryParse(setting.SettingValue, out bool boolValue))
                        {
                            return (T)(object)boolValue;
                        }
                    }
                    else if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
                    {
                        if (DateTime.TryParse(setting.SettingValue, out DateTime dateValue))
                        {
                            return (T)(object)dateValue;
                        }
                    }
                    
                    // Karmaşık tipler için JSON deserialize kullanın
                    return JsonSerializer.Deserialize<T>(setting.SettingValue);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ayar değeri dönüştürülürken hata oluştu: {Key}", key);
                    return defaultValue;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ayar alınırken hata oluştu: {Key}", key);
                return defaultValue;
            }
        }

        /// <summary>
        /// Belirtilen anahtara sahip ayarı kaydeder
        /// </summary>
        /// <typeparam name="T">Ayarın tipi</typeparam>
        /// <param name="key">Ayar anahtarı</param>
        /// <param name="value">Ayar değeri</param>
        /// <param name="username">İşlemi yapan kullanıcı</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        public async Task<bool> SaveSettingAsync<T>(string key, T value, string username)
        {
            try
            {
                string stringValue;
                
                // Basit tipler için doğrudan string'e dönüştürme
                if (value == null)
                {
                    stringValue = string.Empty;
                }
                else if (value is string || value is int || value is bool || value is DateTime)
                {
                    stringValue = value.ToString();
                }
                else
                {
                    // Karmaşık tipler için JSON serialize kullanın
                    stringValue = JsonSerializer.Serialize(value);
                }

                var setting = await _context.GlobalSettings
                    .Where(s => s.SettingKey == key)
                    .FirstOrDefaultAsync();

                if (setting == null)
                {
                    // Yeni ayar oluştur
                    setting = new GlobalSetting
                    {
                        SettingKey = key,
                        SettingValue = stringValue,
                        Description = $"Otomatik oluşturuldu: {key}",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = username
                    };
                    
                    _context.GlobalSettings.Add(setting);
                }
                else
                {
                    // Mevcut ayarı güncelle
                    setting.SettingValue = stringValue;
                    setting.UpdatedAt = DateTime.Now;
                    setting.UpdatedBy = username;
                    setting.IsActive = true;
                    
                    _context.GlobalSettings.Update(setting);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Ayar başarıyla kaydedildi: {Key}", key);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ayar kaydedilirken hata oluştu: {Key}", key);
                return false;
            }
        }
    }
}
