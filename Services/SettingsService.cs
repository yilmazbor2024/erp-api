using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly NanoServiceDbContext _context;
        private readonly ILogger<SettingsService> _logger;
        
        // Barkod ayarları için sabit anahtar
        private const string BARCODE_SETTINGS_KEY = "barcode_settings";

        public SettingsService(NanoServiceDbContext context, ILogger<SettingsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Kullanıcı Ayarları

        /// <summary>
        /// Kullanıcının tüm ayarlarını getirir
        /// </summary>
        public async Task<ApiResponse<IEnumerable<UserSettingDto>>> GetUserSettingsAsync(string userId)
        {
            try
            {
                var settings = await _context.UserSettings
                    .Where(s => s.UserId == userId)
                    .Select(s => new UserSettingDto
                    {
                        Id = s.Id,
                        UserId = s.UserId,
                        SettingKey = s.SettingKey,
                        SettingValue = s.SettingValue,
                        Description = s.Description,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .ToListAsync();
                
                return ApiResponse<IEnumerable<UserSettingDto>>.Ok(settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ayarları getirilirken hata oluştu. UserId: {UserId}", userId);
                return ApiResponse<IEnumerable<UserSettingDto>>.Fail("Kullanıcı ayarları getirilirken bir hata oluştu.");
            }
        }

        /// <summary>
        /// Kullanıcının belirli bir ayarını getirir
        /// </summary>
        public async Task<ApiResponse<UserSettingDto>> GetUserSettingByKeyAsync(string userId, string key)
        {
            try
            {
                var setting = await _context.UserSettings
                    .Where(s => s.UserId == userId && s.SettingKey == key)
                    .Select(s => new UserSettingDto
                    {
                        Id = s.Id,
                        UserId = s.UserId,
                        SettingKey = s.SettingKey,
                        SettingValue = s.SettingValue,
                        Description = s.Description,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .FirstOrDefaultAsync();
                
                if (setting == null)
                {
                    return ApiResponse<UserSettingDto>.Fail("Belirtilen ayar bulunamadı.");
                }
                
                return ApiResponse<UserSettingDto>.Ok(setting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ayarı getirilirken hata oluştu. UserId: {UserId}, Key: {Key}", userId, key);
                return ApiResponse<UserSettingDto>.Fail("Kullanıcı ayarı getirilirken bir hata oluştu.");
            }
        }

        /// <summary>
        /// Kullanıcı için yeni bir ayar oluşturur
        /// </summary>
        public async Task<ApiResponse<UserSettingDto>> CreateUserSettingAsync(string userId, UserSettingCreateUpdateDto dto)
        {
            try
            {
                // Aynı key ile ayar var mı kontrol et
                var existingSetting = await _context.UserSettings
                    .FirstOrDefaultAsync(s => s.UserId == userId && s.SettingKey == dto.SettingKey);
                
                if (existingSetting != null)
                {
                    return ApiResponse<UserSettingDto>.Fail("Bu anahtar ile zaten bir ayar bulunmaktadır. Güncelleme yapınız.");
                }
                
                var setting = new UserSetting
                {
                    UserId = userId,
                    SettingKey = dto.SettingKey,
                    SettingValue = dto.SettingValue,
                    Description = dto.Description,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId
                };
                
                _context.UserSettings.Add(setting);
                await _context.SaveChangesAsync();
                
                var result = new UserSettingDto
                {
                    Id = setting.Id,
                    UserId = setting.UserId,
                    SettingKey = setting.SettingKey,
                    SettingValue = setting.SettingValue,
                    Description = setting.Description,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };
                
                return ApiResponse<UserSettingDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ayarı oluşturulurken hata oluştu. UserId: {UserId}, Key: {Key}", userId, dto.SettingKey);
                return ApiResponse<UserSettingDto>.Fail("Kullanıcı ayarı oluşturulurken bir hata oluştu.");
            }
        }

        /// <summary>
        /// Kullanıcının belirli bir ayarını günceller
        /// </summary>
        public async Task<ApiResponse<UserSettingDto>> UpdateUserSettingAsync(string userId, string key, UserSettingCreateUpdateDto dto)
        {
            try
            {
                var setting = await _context.UserSettings
                    .FirstOrDefaultAsync(s => s.UserId == userId && s.SettingKey == key);
                
                if (setting == null)
                {
                    return ApiResponse<UserSettingDto>.Fail("Güncellenecek ayar bulunamadı.");
                }
                
                // Anahtar değiştirilmek isteniyorsa, yeni anahtarın benzersiz olup olmadığını kontrol et
                if (key != dto.SettingKey)
                {
                    var existingSetting = await _context.UserSettings
                        .FirstOrDefaultAsync(s => s.UserId == userId && s.SettingKey == dto.SettingKey);
                    
                    if (existingSetting != null)
                    {
                        return ApiResponse<UserSettingDto>.Fail("Yeni anahtar ile zaten bir ayar bulunmaktadır.");
                    }
                    
                    setting.SettingKey = dto.SettingKey;
                }
                
                setting.SettingValue = dto.SettingValue;
                setting.Description = dto.Description;
                setting.UpdatedAt = DateTime.Now;
                setting.UpdatedBy = userId;
                
                await _context.SaveChangesAsync();
                
                var result = new UserSettingDto
                {
                    Id = setting.Id,
                    UserId = setting.UserId,
                    SettingKey = setting.SettingKey,
                    SettingValue = setting.SettingValue,
                    Description = setting.Description,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };
                
                return ApiResponse<UserSettingDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ayarı güncellenirken hata oluştu. UserId: {UserId}, Key: {Key}", userId, key);
                return ApiResponse<UserSettingDto>.Fail("Kullanıcı ayarı güncellenirken bir hata oluştu.");
            }
        }

        /// <summary>
        /// Kullanıcının belirli bir ayarını siler
        /// </summary>
        public async Task<ApiResponse<bool>> DeleteUserSettingAsync(string userId, string key)
        {
            try
            {
                var setting = await _context.UserSettings
                    .FirstOrDefaultAsync(s => s.UserId == userId && s.SettingKey == key);
                
                if (setting == null)
                {
                    return ApiResponse<bool>.Fail("Silinecek ayar bulunamadı.");
                }
                
                _context.UserSettings.Remove(setting);
                await _context.SaveChangesAsync();
                
                return ApiResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ayarı silinirken hata oluştu. UserId: {UserId}, Key: {Key}", userId, key);
                return ApiResponse<bool>.Fail("Kullanıcı ayarı silinirken bir hata oluştu.");
            }
        }

        #endregion
        
        #region Modül Bazlı Kullanıcı Ayarları
        
        /// <summary>
        /// Kullanıcının belirli bir modül için ayarlarını getirir
        /// </summary>
        public async Task<ApiResponse<dynamic>> GetUserModuleSettingsAsync(string userId, string moduleKey)
        {
            try
            {
                var setting = await _context.UserSettings
                    .FirstOrDefaultAsync(s => s.UserId == userId && s.SettingKey == moduleKey);
                
                if (setting == null)
                {
                    // Kullanıcı için modül ayarı yoksa, global ayarları kontrol et
                    var globalResponse = await GetGlobalModuleSettingsAsync(moduleKey);
                    return ApiResponse<dynamic>.Ok(globalResponse.Data);
                }
                
                // JSON string'i parse etmeye çalış
                try
                {
                    var jsonSettings = System.Text.Json.JsonSerializer.Deserialize<dynamic>(setting.SettingValue);
                    return ApiResponse<dynamic>.Ok(jsonSettings);
                }
                catch
                {
                    // JSON parse edilemezse, düz string olarak dön
                    return ApiResponse<dynamic>.Ok(setting.SettingValue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı modül ayarları getirilirken hata oluştu. UserId: {UserId}, ModuleKey: {ModuleKey}", userId, moduleKey);
                return ApiResponse<dynamic>.Fail($"{moduleKey} modülü için kullanıcı ayarları getirilirken bir hata oluştu.");
            }
        }
        
        /// <summary>
        /// Kullanıcının belirli bir modül için ayarlarını günceller
        /// </summary>
        public async Task<ApiResponse<UserSettingDto>> UpdateUserModuleSettingsAsync(string userId, string moduleKey, string settingsJson, string description = null)
        {
            try
            {
                var setting = await _context.UserSettings
                    .FirstOrDefaultAsync(s => s.UserId == userId && s.SettingKey == moduleKey);
                
                if (setting == null)
                {
                    // Yeni ayar oluştur
                    setting = new UserSetting
                    {
                        UserId = userId,
                        SettingKey = moduleKey,
                        SettingValue = settingsJson,
                        Description = description ?? $"{moduleKey} modülü için kullanıcı ayarları",
                        CreatedAt = DateTime.Now,
                        CreatedBy = userId
                    };
                    
                    _context.UserSettings.Add(setting);
                }
                else
                {
                    // Mevcut ayarı güncelle
                    setting.SettingValue = settingsJson;
                    if (!string.IsNullOrEmpty(description))
                    {
                        setting.Description = description;
                    }
                    setting.UpdatedAt = DateTime.Now;
                    setting.UpdatedBy = userId;
                }
                
                await _context.SaveChangesAsync();
                
                var result = new UserSettingDto
                {
                    Id = setting.Id,
                    UserId = setting.UserId,
                    SettingKey = setting.SettingKey,
                    SettingValue = setting.SettingValue,
                    Description = setting.Description,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };
                
                return ApiResponse<UserSettingDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı modül ayarları güncellenirken hata oluştu. UserId: {UserId}, ModuleKey: {ModuleKey}", userId, moduleKey);
                return ApiResponse<UserSettingDto>.Fail($"{moduleKey} modülü için kullanıcı ayarları güncellenirken bir hata oluştu.");
            }
        }
        
        /// <summary>
        /// Kullanıcının belirli bir modül için ayarlarını sıfırlar (siler)
        /// </summary>
        public async Task<ApiResponse<bool>> ResetUserModuleSettingsAsync(string userId, string moduleKey)
        {
            try
            {
                var setting = await _context.UserSettings
                    .FirstOrDefaultAsync(s => s.UserId == userId && s.SettingKey == moduleKey);
                
                if (setting != null)
                {
                    _context.UserSettings.Remove(setting);
                    await _context.SaveChangesAsync();
                }
                
                return ApiResponse<bool>.Ok(true, $"{moduleKey} modülü için kullanıcı ayarları sıfırlandı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı modül ayarları sıfırlanırken hata oluştu. UserId: {UserId}, ModuleKey: {ModuleKey}", userId, moduleKey);
                return ApiResponse<bool>.Fail($"{moduleKey} modülü için kullanıcı ayarları sıfırlanırken bir hata oluştu.");
            }
        }
        
        #endregion
        
        #region Barkod Ayarları (Kullanıcı)
        
        /// <summary>
        /// Kullanıcının barkod ayarlarını getirir (Geriye uyumluluk için)
        /// </summary>
        public async Task<ApiResponse<BarcodeSettingsDto>> GetUserBarcodeSettingsAsync(string userId)
        {
            try
            {
                // Yeni modül bazlı metodu kullan
                var response = await GetUserModuleSettingsAsync(userId, BARCODE_SETTINGS_KEY);
                
                if (!response.Success || response.Data == null)
                {
                    return ApiResponse<BarcodeSettingsDto>.Fail(response.Message);
                }
                
                var result = new BarcodeSettingsDto
                {
                    Settings = response.Data.ToString(),
                    Description = "Barkod ayarları"
                };
                
                return ApiResponse<BarcodeSettingsDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı barkod ayarları getirilirken hata oluştu. UserId: {UserId}", userId);
                return ApiResponse<BarcodeSettingsDto>.Fail("Kullanıcı barkod ayarları getirilirken bir hata oluştu.");
            }
        }
        
        /// <summary>
        /// Kullanıcının barkod ayarlarını günceller (Geriye uyumluluk için)
        /// </summary>
        public async Task<ApiResponse<BarcodeSettingsDto>> UpdateUserBarcodeSettingsAsync(string userId, BarcodeSettingsDto dto)
        {
            try
            {
                // Yeni modül bazlı metodu kullan
                var response = await UpdateUserModuleSettingsAsync(userId, BARCODE_SETTINGS_KEY, dto.Settings, dto.Description);
                
                if (!response.Success)
                {
                    return ApiResponse<BarcodeSettingsDto>.Fail(response.Message);
                }
                
                var result = new BarcodeSettingsDto
                {
                    Settings = response.Data.SettingValue,
                    Description = response.Data.Description
                };
                
                return ApiResponse<BarcodeSettingsDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı barkod ayarları güncellenirken hata oluştu. UserId: {UserId}", userId);
                return ApiResponse<BarcodeSettingsDto>.Fail("Kullanıcı barkod ayarları güncellenirken bir hata oluştu.");
            }
        }
        
        #endregion

        #region Genel Ayarlar

        /// <summary>
        /// Tüm genel ayarları getirir
        /// </summary>
        public async Task<ApiResponse<IEnumerable<GlobalSettingDto>>> GetAllGlobalSettingsAsync(bool includeInactive = false)
        {
            try
            {
                var query = _context.GlobalSettings.AsQueryable();
                
                if (!includeInactive)
                {
                    query = query.Where(s => s.IsActive);
                }
                
                var settings = await query
                    .Select(s => new GlobalSettingDto
                    {
                        Id = s.Id,
                        SettingKey = s.SettingKey,
                        SettingValue = s.SettingValue,
                        Description = s.Description,
                        IsActive = s.IsActive,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .ToListAsync();
                
                return ApiResponse<IEnumerable<GlobalSettingDto>>.Ok(settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel ayarlar getirilirken hata oluştu.");
                return ApiResponse<IEnumerable<GlobalSettingDto>>.Fail("Genel ayarlar getirilirken bir hata oluştu.");
            }
        }

        /// <summary>
        /// Belirli bir genel ayarı getirir
        /// </summary>
        public async Task<ApiResponse<GlobalSettingDto>> GetGlobalSettingByKeyAsync(string key)
        {
            try
            {
                var setting = await _context.GlobalSettings
                    .Where(s => s.SettingKey == key)
                    .Select(s => new GlobalSettingDto
                    {
                        Id = s.Id,
                        SettingKey = s.SettingKey,
                        SettingValue = s.SettingValue,
                        Description = s.Description,
                        IsActive = s.IsActive,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .FirstOrDefaultAsync();
                
                if (setting == null)
                {
                    return ApiResponse<GlobalSettingDto>.Fail("Belirtilen ayar bulunamadı.");
                }
                
                return ApiResponse<GlobalSettingDto>.Ok(setting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel ayar getirilirken hata oluştu. Key: {Key}", key);
                return ApiResponse<GlobalSettingDto>.Fail("Genel ayar getirilirken bir hata oluştu.");
            }
        }

        /// <summary>
        /// Yeni bir genel ayar oluşturur
        /// </summary>
        public async Task<ApiResponse<GlobalSettingDto>> CreateGlobalSettingAsync(GlobalSettingCreateUpdateDto dto)
        {
            try
            {
                // Aynı key ile ayar var mı kontrol et
                var existingSetting = await _context.GlobalSettings
                    .FirstOrDefaultAsync(s => s.SettingKey == dto.SettingKey);
                
                if (existingSetting != null)
                {
                    return ApiResponse<GlobalSettingDto>.Fail("Bu anahtar ile zaten bir ayar bulunmaktadır. Güncelleme yapınız.");
                }
                
                var currentUserId = "admin"; // TODO: Gerçek kullanıcı ID'sini al
                
                var setting = new GlobalSetting
                {
                    SettingKey = dto.SettingKey,
                    SettingValue = dto.SettingValue,
                    Description = dto.Description,
                    IsActive = dto.IsActive,
                    CreatedAt = DateTime.Now,
                    CreatedBy = currentUserId
                };
                
                _context.GlobalSettings.Add(setting);
                await _context.SaveChangesAsync();
                
                var result = new GlobalSettingDto
                {
                    Id = setting.Id,
                    SettingKey = setting.SettingKey,
                    SettingValue = setting.SettingValue,
                    Description = setting.Description,
                    IsActive = setting.IsActive,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };
                
                return ApiResponse<GlobalSettingDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel ayar oluşturulurken hata oluştu. Key: {Key}", dto.SettingKey);
                return ApiResponse<GlobalSettingDto>.Fail("Genel ayar oluşturulurken bir hata oluştu.");
            }
        }

        /// <summary>
        /// Belirli bir genel ayarı günceller
        /// </summary>
        public async Task<ApiResponse<GlobalSettingDto>> UpdateGlobalSettingAsync(string key, GlobalSettingCreateUpdateDto dto)
        {
            try
            {
                var setting = await _context.GlobalSettings
                    .FirstOrDefaultAsync(s => s.SettingKey == key);
                
                if (setting == null)
                {
                    return ApiResponse<GlobalSettingDto>.Fail("Güncellenecek ayar bulunamadı.");
                }
                
                var currentUserId = "admin"; // TODO: Gerçek kullanıcı ID'sini al
                
                // Anahtar değiştirilmek isteniyorsa, yeni anahtarın benzersiz olup olmadığını kontrol et
                if (key != dto.SettingKey)
                {
                    var existingSetting = await _context.GlobalSettings
                        .FirstOrDefaultAsync(s => s.SettingKey == dto.SettingKey);
                    
                    if (existingSetting != null)
                    {
                        return ApiResponse<GlobalSettingDto>.Fail("Yeni anahtar ile zaten bir ayar bulunmaktadır.");
                    }
                    
                    setting.SettingKey = dto.SettingKey;
                }
                
                setting.SettingValue = dto.SettingValue;
                setting.Description = dto.Description;
                setting.IsActive = dto.IsActive;
                setting.UpdatedAt = DateTime.Now;
                setting.UpdatedBy = currentUserId;
                
                await _context.SaveChangesAsync();
                
                var result = new GlobalSettingDto
                {
                    Id = setting.Id,
                    SettingKey = setting.SettingKey,
                    SettingValue = setting.SettingValue,
                    Description = setting.Description,
                    IsActive = setting.IsActive,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };
                
                return ApiResponse<GlobalSettingDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel ayar güncellenirken hata oluştu. Key: {Key}", key);
                return ApiResponse<GlobalSettingDto>.Fail("Genel ayar güncellenirken bir hata oluştu.");
            }
        }

        /// <summary>
        /// Belirli bir genel ayarı siler
        /// </summary>
        public async Task<ApiResponse<bool>> DeleteGlobalSettingAsync(string key)
        {
            try
            {
                var setting = await _context.GlobalSettings
                    .FirstOrDefaultAsync(s => s.SettingKey == key);
                
                if (setting == null)
                {
                    return ApiResponse<bool>.Fail("Silinecek ayar bulunamadı.");
                }
                
                _context.GlobalSettings.Remove(setting);
                await _context.SaveChangesAsync();
                
                return ApiResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel ayar silinirken hata oluştu. Key: {Key}", key);
                return ApiResponse<bool>.Fail("Genel ayar silinirken bir hata oluştu.");
            }
        }

        #endregion
        
        #region Modül Bazlı Genel Ayarlar
        
        /// <summary>
        /// Belirli bir modül için genel ayarları getirir
        /// </summary>
        public async Task<ApiResponse<dynamic>> GetGlobalModuleSettingsAsync(string moduleKey)
        {
            try
            {
                var setting = await _context.GlobalSettings
                    .FirstOrDefaultAsync(s => s.SettingKey == moduleKey && s.IsActive);
                
                if (setting == null)
                {
                    return ApiResponse<dynamic>.Ok(null, $"{moduleKey} modülü için genel ayar bulunamadı.");
                }
                
                // JSON string'i parse etmeye çalış
                try
                {
                    var jsonSettings = System.Text.Json.JsonSerializer.Deserialize<dynamic>(setting.SettingValue);
                    return ApiResponse<dynamic>.Ok(jsonSettings);
                }
                catch
                {
                    // JSON parse edilemezse, düz string olarak dön
                    return ApiResponse<dynamic>.Ok(setting.SettingValue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Modül genel ayarları getirilirken hata oluştu. ModuleKey: {ModuleKey}", moduleKey);
                return ApiResponse<dynamic>.Fail($"{moduleKey} modülü için genel ayarlar getirilirken bir hata oluştu.");
            }
        }
        
        /// <summary>
        /// Belirli bir modül için genel ayarları günceller
        /// </summary>
        public async Task<ApiResponse<GlobalSettingDto>> UpdateGlobalModuleSettingsAsync(string moduleKey, string settingsJson, string description = null)
        {
            try
            {
                var setting = await _context.GlobalSettings
                    .FirstOrDefaultAsync(s => s.SettingKey == moduleKey);
                
                var currentUserId = "admin"; // TODO: Gerçek kullanıcı ID'sini al
                
                if (setting == null)
                {
                    // Yeni ayar oluştur
                    setting = new GlobalSetting
                    {
                        SettingKey = moduleKey,
                        SettingValue = settingsJson,
                        Description = description ?? $"{moduleKey} modülü için genel ayarlar",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = currentUserId
                    };
                    
                    _context.GlobalSettings.Add(setting);
                }
                else
                {
                    // Mevcut ayarı güncelle
                    setting.SettingValue = settingsJson;
                    if (!string.IsNullOrEmpty(description))
                    {
                        setting.Description = description;
                    }
                    setting.UpdatedAt = DateTime.Now;
                    setting.UpdatedBy = currentUserId;
                }
                
                await _context.SaveChangesAsync();
                
                var result = new GlobalSettingDto
                {
                    Id = setting.Id,
                    SettingKey = setting.SettingKey,
                    SettingValue = setting.SettingValue,
                    Description = setting.Description,
                    IsActive = setting.IsActive,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt
                };
                
                return ApiResponse<GlobalSettingDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Modül genel ayarları güncellenirken hata oluştu. ModuleKey: {ModuleKey}", moduleKey);
                return ApiResponse<GlobalSettingDto>.Fail($"{moduleKey} modülü için genel ayarlar güncellenirken bir hata oluştu.");
            }
        }
        
        /// <summary>
        /// Belirli bir modül için genel ayarları sıfırlar (siler)
        /// </summary>
        public async Task<ApiResponse<bool>> ResetGlobalModuleSettingsAsync(string moduleKey)
        {
            try
            {
                var setting = await _context.GlobalSettings
                    .FirstOrDefaultAsync(s => s.SettingKey == moduleKey);
                
                if (setting != null)
                {
                    _context.GlobalSettings.Remove(setting);
                    await _context.SaveChangesAsync();
                }
                
                return ApiResponse<bool>.Ok(true, $"{moduleKey} modülü için genel ayarlar sıfırlandı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Modül genel ayarları sıfırlanırken hata oluştu. ModuleKey: {ModuleKey}", moduleKey);
                return ApiResponse<bool>.Fail($"{moduleKey} modülü için genel ayarlar sıfırlanırken bir hata oluştu.");
            }
        }
        
        #endregion
        
        #region Barkod Ayarları (Genel)
        
        /// <summary>
        /// Genel barkod ayarlarını getirir (Geriye uyumluluk için)
        /// </summary>
        public async Task<ApiResponse<BarcodeSettingsDto>> GetGlobalBarcodeSettingsAsync()
        {
            try
            {
                // Yeni modül bazlı metodu kullan
                var response = await GetGlobalModuleSettingsAsync(BARCODE_SETTINGS_KEY);
                
                if (!response.Success || response.Data == null)
                {
                    return ApiResponse<BarcodeSettingsDto>.Fail(response.Message ?? "Genel barkod ayarları bulunamadı.");
                }
                
                var result = new BarcodeSettingsDto
                {
                    Settings = response.Data.ToString(),
                    Description = "Genel barkod ayarları"
                };
                
                return ApiResponse<BarcodeSettingsDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel barkod ayarları getirilirken hata oluştu.");
                return ApiResponse<BarcodeSettingsDto>.Fail("Genel barkod ayarları getirilirken bir hata oluştu.");
            }
        }
        
        /// <summary>
        /// Genel barkod ayarlarını günceller (Geriye uyumluluk için)
        /// </summary>
        public async Task<ApiResponse<BarcodeSettingsDto>> UpdateGlobalBarcodeSettingsAsync(BarcodeSettingsDto dto)
        {
            try
            {
                // Yeni modül bazlı metodu kullan
                var response = await UpdateGlobalModuleSettingsAsync(BARCODE_SETTINGS_KEY, dto.Settings, dto.Description);
                
                if (!response.Success)
                {
                    return ApiResponse<BarcodeSettingsDto>.Fail(response.Message);
                }
                
                var result = new BarcodeSettingsDto
                {
                    Settings = response.Data.SettingValue,
                    Description = response.Data.Description
                };
                
                return ApiResponse<BarcodeSettingsDto>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel barkod ayarları güncellenirken hata oluştu.");
                return ApiResponse<BarcodeSettingsDto>.Fail("Genel barkod ayarları güncellenirken bir hata oluştu.");
            }
        }
        
        #endregion
    }
}
