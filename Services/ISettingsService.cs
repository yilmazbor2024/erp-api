using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Common;

namespace ErpMobile.Api.Services
{
    public interface ISettingsService
    {
        // Kullanıcı ayarları
        Task<ApiResponse<IEnumerable<UserSettingDto>>> GetUserSettingsAsync(string userId);
        Task<ApiResponse<UserSettingDto>> GetUserSettingByKeyAsync(string userId, string key);
        Task<ApiResponse<UserSettingDto>> CreateUserSettingAsync(string userId, UserSettingCreateUpdateDto dto);
        Task<ApiResponse<UserSettingDto>> UpdateUserSettingAsync(string userId, string key, UserSettingCreateUpdateDto dto);
        Task<ApiResponse<bool>> DeleteUserSettingAsync(string userId, string key);
        
        // Modül bazlı kullanıcı ayarları
        Task<ApiResponse<dynamic>> GetUserModuleSettingsAsync(string userId, string moduleKey);
        Task<ApiResponse<UserSettingDto>> UpdateUserModuleSettingsAsync(string userId, string moduleKey, string settingsJson, string description = null);
        Task<ApiResponse<bool>> ResetUserModuleSettingsAsync(string userId, string moduleKey);
        
        // Barkod ayarları için özel metodlar (geriye uyumluluk)
        Task<ApiResponse<BarcodeSettingsDto>> GetUserBarcodeSettingsAsync(string userId);
        Task<ApiResponse<BarcodeSettingsDto>> UpdateUserBarcodeSettingsAsync(string userId, BarcodeSettingsDto dto);
        
        // Genel ayarlar
        Task<ApiResponse<IEnumerable<GlobalSettingDto>>> GetAllGlobalSettingsAsync(bool includeInactive = false);
        Task<ApiResponse<GlobalSettingDto>> GetGlobalSettingByKeyAsync(string key);
        Task<ApiResponse<GlobalSettingDto>> CreateGlobalSettingAsync(GlobalSettingCreateUpdateDto dto);
        Task<ApiResponse<GlobalSettingDto>> UpdateGlobalSettingAsync(string key, GlobalSettingCreateUpdateDto dto);
        Task<ApiResponse<bool>> DeleteGlobalSettingAsync(string key);
        
        // Modül bazlı genel ayarlar
        Task<ApiResponse<dynamic>> GetGlobalModuleSettingsAsync(string moduleKey);
        Task<ApiResponse<GlobalSettingDto>> UpdateGlobalModuleSettingsAsync(string moduleKey, string settingsJson, string description = null);
        Task<ApiResponse<bool>> ResetGlobalModuleSettingsAsync(string moduleKey);
        
        // Barkod ayarları için genel ayarlar (geriye uyumluluk)
        Task<ApiResponse<BarcodeSettingsDto>> GetGlobalBarcodeSettingsAsync();
        Task<ApiResponse<BarcodeSettingsDto>> UpdateGlobalBarcodeSettingsAsync(BarcodeSettingsDto dto);
    }
}
