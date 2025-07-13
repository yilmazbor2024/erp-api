using System;
using System.Threading.Tasks;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Notification;
using ErpMobile.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [Route("api/v1/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly INotificationService _notificationService;

        public NotificationController(
            ILogger<NotificationController> logger,
            INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        /// <summary>
        /// VAPID public key'i döndürür
        /// </summary>
        /// <returns>VAPID public key</returns>
        [HttpGet("vapid-public-key")]
        public IActionResult GetVapidPublicKey()
        {
            try
            {
                var publicKey = _notificationService.GetVapidPublicKey();
                if (string.IsNullOrEmpty(publicKey))
                {
                    return NotFound(new ApiResponse<string>(null, false, "VAPID public key bulunamadı."));
                }

                return Ok(new ApiResponse<string>(publicKey, true, "VAPID public key başarıyla alındı."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "VAPID public key alınırken hata oluştu.");
                return StatusCode(500, new ApiResponse<string>(null, false, "VAPID public key alınırken hata oluştu.", ex.Message));
            }
        }

        /// <summary>
        /// Push bildirimi aboneliği kaydeder
        /// </summary>
        /// <param name="subscription">Abonelik bilgileri</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] PushSubscriptionRequest subscription)
        {
            try
            {
                if (subscription == null || string.IsNullOrEmpty(subscription.Endpoint) || 
                    string.IsNullOrEmpty(subscription.P256dh) || string.IsNullOrEmpty(subscription.Auth))
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Geçersiz abonelik bilgileri."));
                }

                // User-Agent bilgisini al
                subscription.UserAgent = Request.Headers["User-Agent"].ToString();

                // Kullanıcı oturum açmışsa, kullanıcı ID'sini ekle
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        subscription.UserId = Guid.Parse(userId);
                    }
                }

                var result = await _notificationService.SaveSubscriptionAsync(subscription);
                if (!result)
                {
                    return StatusCode(500, new ApiResponse<bool>(false, false, "Abonelik kaydedilemedi."));
                }

                return Ok(new ApiResponse<bool>(true, true, "Abonelik başarıyla kaydedildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Push bildirimi aboneliği kaydedilirken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>(false, false, "Push bildirimi aboneliği kaydedilirken hata oluştu.", ex.Message));
            }
        }

        /// <summary>
        /// Push bildirimi aboneliğini siler
        /// </summary>
        /// <param name="endpoint">Abonelik endpoint'i</param>
        /// <returns>İşlem sonucu</returns>
        [HttpDelete("unsubscribe")]
        public async Task<IActionResult> Unsubscribe([FromQuery] string endpoint)
        {
            try
            {
                if (string.IsNullOrEmpty(endpoint))
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Geçersiz endpoint."));
                }

                var result = await _notificationService.DeleteSubscriptionAsync(endpoint);
                if (!result)
                {
                    return NotFound(new ApiResponse<bool>(false, false, "Abonelik bulunamadı."));
                }

                return Ok(new ApiResponse<bool>(true, true, "Abonelik başarıyla silindi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Push bildirimi aboneliği silinirken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>(false, false, "Push bildirimi aboneliği silinirken hata oluştu.", ex.Message));
            }
        }

        /// <summary>
        /// Test bildirimi gönderir (sadece Admin rolü)
        /// </summary>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("send-test")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendTestNotification()
        {
            try
            {
                var notification = new PushNotification
                {
                    Title = "Test Bildirimi",
                    Body = "Bu bir test bildirimidir.",
                    Icon = "/assets/icons/logo.png",
                    Badge = "/assets/icons/badge.png"
                };

                var result = await _notificationService.SendPushNotificationAsync(notification);
                if (!result)
                {
                    return StatusCode(500, new ApiResponse<bool>(false, false, "Test bildirimi gönderilemedi."));
                }

                return Ok(new ApiResponse<bool>(true, true, "Test bildirimi başarıyla gönderildi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Test bildirimi gönderilirken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>(false, false, "Test bildirimi gönderilirken hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Kullanıcı bildirim tercihlerini getirir
        /// </summary>
        /// <returns>Kullanıcı bildirim tercihleri</returns>
        [HttpGet("preferences")]
        [Authorize]
        public async Task<IActionResult> GetNotificationPreferences()
        {
            try
            {
                var userId = User.Identity.Name;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new ApiResponse<object>(null, false, "Kullanıcı kimliği bulunamadı."));
                }
                
                var preferences = await _notificationService.GetNotificationPreferencesForUserAsync(userId);
                return Ok(new ApiResponse<IEnumerable<NotificationPreferenceFlatDto>>(preferences, true, "Bildirim tercihleri başarıyla alındı."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bildirim tercihleri alınırken hata oluştu.");
                return StatusCode(500, new ApiResponse<IEnumerable<NotificationPreferenceFlatDto>>(null, false, "Bildirim tercihleri alınırken hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Kullanıcı bildirim tercihini günceller
        /// </summary>
        /// <param name="request">Güncelleme isteği</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPut("preferences")]
        [Authorize]
        public async Task<IActionResult> UpdateNotificationPreference([FromBody] UpdateNotificationPreferenceRequest request)
        {
            try
            {
                var userId = User.Identity.Name;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new ApiResponse<bool>(false, false, "Kullanıcı kimliği bulunamadı."));
                }
                
                // Geçerlilik kontrolü
                if (string.IsNullOrEmpty(request.NotificationType) || string.IsNullOrEmpty(request.ActionType))
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Geçersiz bildirim tercihi parametreleri."));
                }
                
                var result = await _notificationService.UpdateUserNotificationPreferenceAsync(
                    userId, 
                    request.NotificationType, 
                    request.ActionType, 
                    request.IsEnabled);
                    
                if (!result)
                {
                    return StatusCode(500, new ApiResponse<bool>(false, false, "Bildirim tercihi güncellenemedi."));
                }
                
                return Ok(new ApiResponse<bool>(true, true, "Bildirim tercihi başarıyla güncellendi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bildirim tercihi güncellenirken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>(false, false, "Bildirim tercihi güncellenirken hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Kullanıcı bildirim tercihini sıfırlar (global tercihe döner)
        /// </summary>
        /// <param name="notificationType">Bildirim türü</param>
        /// <param name="actionType">Eylem türü</param>
        /// <returns>İşlem sonucu</returns>
        [HttpDelete("preferences")]
        [Authorize]
        public async Task<IActionResult> ResetNotificationPreference([FromQuery] string notificationType, [FromQuery] string actionType)
        {
            try
            {
                var userId = User.Identity.Name;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new ApiResponse<bool>(false, false, "Kullanıcı kimliği bulunamadı."));
                }
                
                // Geçerlilik kontrolü
                if (string.IsNullOrEmpty(notificationType) || string.IsNullOrEmpty(actionType))
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Geçersiz bildirim tercihi parametreleri."));
                }
                
                var result = await _notificationService.ResetUserNotificationPreferenceAsync(userId, notificationType, actionType);
                if (!result)
                {
                    return StatusCode(500, new ApiResponse<bool>(false, false, "Bildirim tercihi sıfırlanamadı."));
                }
                
                return Ok(new ApiResponse<bool>(true, true, "Bildirim tercihi başarıyla sıfırlandı."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bildirim tercihi sıfırlanırken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>(false, false, "Bildirim tercihi sıfırlanırken hata oluştu.", ex.Message));
            }
        }
        
        /// <summary>
        /// Global bildirim tercihini günceller (sadece Admin rolü)
        /// </summary>
        /// <param name="request">Güncelleme isteği</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPut("global-preferences")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGlobalNotificationPreference([FromBody] UpdateNotificationPreferenceRequest request)
        {
            try
            {
                // Geçerlilik kontrolü
                if (string.IsNullOrEmpty(request.NotificationType) || string.IsNullOrEmpty(request.ActionType))
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Geçersiz bildirim tercihi parametreleri."));
                }
                
                var result = await _notificationService.UpdateGlobalNotificationPreferenceAsync(
                    request.NotificationType, 
                    request.ActionType, 
                    request.IsEnabled);
                    
                if (!result)
                {
                    return StatusCode(500, new ApiResponse<bool>(false, false, "Global bildirim tercihi güncellenemedi."));
                }
                
                return Ok(new ApiResponse<bool>(true, true, "Global bildirim tercihi başarıyla güncellendi."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Global bildirim tercihi güncellenirken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>(false, false, "Global bildirim tercihi güncellenirken hata oluştu.", ex.Message));
            }
        }
    }
}
