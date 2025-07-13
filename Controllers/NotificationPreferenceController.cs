using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Notification;
using ErpMobile.Api.Services;
using System.Security.Claims;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/v1/notification-preferences")]
    [Authorize]
    public class NotificationPreferenceController : ControllerBase
    {
        private readonly ILogger<NotificationPreferenceController> _logger;
        private readonly INotificationService _notificationService;
        
        public NotificationPreferenceController(
            ILogger<NotificationPreferenceController> logger,
            INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }
        
        /// <summary>
        /// Kullanıcının bildirim tercihlerini getirir
        /// </summary>
        /// <returns>Kullanıcının bildirim tercihleri</returns>
        [HttpGet("user")]
        public async Task<ActionResult<ApiResponse<NotificationPreferenceDto>>> GetUserPreferences()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new ApiResponse<NotificationPreferenceDto>(null, false, "Kullanıcı kimliği bulunamadı."));
                }
                
                var preferences = await _notificationService.GetNotificationPreferencesAsync(userId);
                return Ok(new ApiResponse<NotificationPreferenceDto>(preferences, true, "Kullanıcı bildirim tercihleri başarıyla getirildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı bildirim tercihleri getirilirken hata oluştu.");
                return StatusCode(500, new ApiResponse<NotificationPreferenceDto>(null, false, "Kullanıcı bildirim tercihleri getirilirken hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Kullanıcının bildirim tercihlerini günceller
        /// </summary>
        /// <param name="preferences">Bildirim tercihleri</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("user")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateUserPreferences([FromBody] NotificationPreferenceDto preferences)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new ApiResponse<bool>(false, false, "Kullanıcı kimliği bulunamadı."));
                }
                
                // Kullanıcı kimliğini ayarla (gelen veriye güvenme)
                preferences.UserId = userId;
                
                var username = User.FindFirstValue(ClaimTypes.Name) ?? userId;
                var result = await _notificationService.SaveNotificationPreferencesAsync(preferences, username);
                
                if (!result)
                {
                    return StatusCode(500, new ApiResponse<bool>(false, false, "Bildirim tercihleri kaydedilemedi."));
                }
                
                return Ok(new ApiResponse<bool>(true, true, "Bildirim tercihleri başarıyla kaydedildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı bildirim tercihleri güncellenirken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>(false, false, "Kullanıcı bildirim tercihleri güncellenirken hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Genel bildirim tercihlerini getirir (sadece admin)
        /// </summary>
        /// <returns>Genel bildirim tercihleri</returns>
        [HttpGet("global")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<NotificationPreferenceDto>>> GetGlobalPreferences()
        {
            try
            {
                var preferences = await _notificationService.GetNotificationPreferencesAsync(null);
                return Ok(new ApiResponse<NotificationPreferenceDto>(preferences, true, "Genel bildirim tercihleri başarıyla getirildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel bildirim tercihleri getirilirken hata oluştu.");
                return StatusCode(500, new ApiResponse<NotificationPreferenceDto>(null, false, "Genel bildirim tercihleri getirilirken hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Genel bildirim tercihlerini günceller (sadece admin)
        /// </summary>
        /// <param name="preferences">Bildirim tercihleri</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("global")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateGlobalPreferences([FromBody] NotificationPreferenceDto preferences)
        {
            try
            {
                // Genel tercihler için UserId null olmalı
                preferences.UserId = null;
                
                var username = User.FindFirstValue(ClaimTypes.Name) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _notificationService.SaveNotificationPreferencesAsync(preferences, username);
                
                if (!result)
                {
                    return StatusCode(500, new ApiResponse<bool>(false, false, "Genel bildirim tercihleri kaydedilemedi."));
                }
                
                return Ok(new ApiResponse<bool>(true, true, "Genel bildirim tercihleri başarıyla kaydedildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genel bildirim tercihleri güncellenirken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>(false, false, "Genel bildirim tercihleri güncellenirken hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Desteklenen bildirim türlerini getirir
        /// </summary>
        /// <returns>Desteklenen bildirim türleri</returns>
        [HttpGet("supported-types")]
        public async Task<ActionResult<ApiResponse<List<NotificationTypePreference>>>> GetSupportedNotificationTypes()
        {
            try
            {
                var types = await _notificationService.GetSupportedNotificationTypesAsync();
                return Ok(new ApiResponse<List<NotificationTypePreference>>(types, true, "Desteklenen bildirim türleri başarıyla getirildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Desteklenen bildirim türleri getirilirken hata oluştu.");
                return StatusCode(500, new ApiResponse<List<NotificationTypePreference>>(null, false, "Desteklenen bildirim türleri getirilirken hata oluştu.", ex.Message));
            }
        }
    }
}
