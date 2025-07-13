using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Common;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ExchangeRateSettingsController : ControllerBase
    {
        private readonly NanoServiceDbContext _dbContext;
        private readonly ILogger<ExchangeRateSettingsController> _logger;

        public ExchangeRateSettingsController(
            NanoServiceDbContext dbContext,
            ILogger<ExchangeRateSettingsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Döviz kuru senkronizasyon ayarlarını getirir
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<Dictionary<string, string>>>> GetExchangeRateSettings()
        {
            try
            {
                var settings = await _dbContext.GlobalSettings
                    .Where(s => s.SettingKey.StartsWith("ExchangeRateSync.") && s.IsActive)
                    .ToDictionaryAsync(s => s.SettingKey, s => s.SettingValue);

                // Eğer ayarlar yoksa varsayılan değerleri ekle
                if (!settings.ContainsKey("ExchangeRateSync.Enabled"))
                    settings.Add("ExchangeRateSync.Enabled", "true");
                
                if (!settings.ContainsKey("ExchangeRateSync.Hour"))
                    settings.Add("ExchangeRateSync.Hour", "8");
                
                if (!settings.ContainsKey("ExchangeRateSync.Minute"))
                    settings.Add("ExchangeRateSync.Minute", "30");
                
                if (!settings.ContainsKey("ExchangeRateSync.Frequency"))
                    settings.Add("ExchangeRateSync.Frequency", "1");

                return ApiResponse<Dictionary<string, string>>.Ok(settings, "Döviz kuru ayarları başarıyla getirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Döviz kuru ayarları getirilirken hata oluştu");
                return ApiResponse<Dictionary<string, string>>.Fail(ex.Message, "Döviz kuru ayarları getirilirken hata oluştu");
            }
        }

        /// <summary>
        /// Döviz kuru senkronizasyon ayarlarını günceller
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateExchangeRateSettings([FromBody] Dictionary<string, string> settings)
        {
            try
            {
                if (settings == null)
                    return ApiResponse<bool>.Fail("Ayarlar boş olamaz");

                var username = User.Identity?.Name ?? "System";
                var now = DateTime.Now;

                foreach (var setting in settings)
                {
                    // Sadece izin verilen ayarları güncelle
                    if (!setting.Key.StartsWith("ExchangeRateSync."))
                        continue;

                    // Ayarın geçerliliğini kontrol et
                    if (setting.Key == "ExchangeRateSync.Enabled" && 
                        setting.Value != "true" && setting.Value != "false")
                        continue;

                    if (setting.Key == "ExchangeRateSync.Hour" && 
                        (!int.TryParse(setting.Value, out int hour) || hour < 0 || hour > 23))
                        continue;

                    if (setting.Key == "ExchangeRateSync.Minute" && 
                        (!int.TryParse(setting.Value, out int minute) || minute < 0 || minute > 59))
                        continue;

                    if (setting.Key == "ExchangeRateSync.Frequency" && 
                        (!int.TryParse(setting.Value, out int frequency) || frequency < 1 || frequency > 24))
                        continue;

                    // Ayarı güncelle veya ekle
                    var existingSetting = await _dbContext.GlobalSettings
                        .FirstOrDefaultAsync(s => s.SettingKey == setting.Key);

                    if (existingSetting != null)
                    {
                        existingSetting.SettingValue = setting.Value;
                        existingSetting.UpdatedAt = now;
                        existingSetting.UpdatedBy = username;
                    }
                    else
                    {
                        string description = setting.Key switch
                        {
                            "ExchangeRateSync.Enabled" => "Döviz kuru otomatik senkronizasyonu aktif mi? (true/false)",
                            "ExchangeRateSync.Hour" => "Döviz kuru senkronizasyonu için saat (0-23)",
                            "ExchangeRateSync.Minute" => "Döviz kuru senkronizasyonu için dakika (0-59)",
                            "ExchangeRateSync.Frequency" => "Günde kaç kez senkronizasyon yapılacak (1-24)",
                            _ => setting.Key
                        };

                        _dbContext.GlobalSettings.Add(new GlobalSetting
                        {
                            SettingKey = setting.Key,
                            SettingValue = setting.Value,
                            Description = description,
                            IsActive = true,
                            CreatedAt = now,
                            CreatedBy = username
                        });
                    }
                }

                await _dbContext.SaveChangesAsync();
                return ApiResponse<bool>.Ok(true, "Döviz kuru ayarları başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Döviz kuru ayarları güncellenirken hata oluştu");
                return ApiResponse<bool>.Fail(ex.Message, "Döviz kuru ayarları güncellenirken hata oluştu");
            }
        }
    }
}
