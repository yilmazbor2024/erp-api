using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WebPush;
using System.Text.Json;
using ErpMobile.Api.Models.Notification;
using ErpMobile.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace ErpMobile.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly NanoServiceDbContext _context;
        private readonly WebPushClient _webPushClient;
        private readonly VapidDetails _vapidDetails;
        
        // Desteklenen bildirim türleri
        private static readonly Dictionary<string, string> _supportedNotificationTypes = new Dictionary<string, string>
        {
            { "CashTransaction", "Kasa Hareketleri" },
            { "Invoice", "Fatura İşlemleri" },
            { "Customer", "Müşteri İşlemleri" },
            { "DatabaseBackup", "Veritabanı Yedekleme" },
            { "UserActivity", "Kullanıcı Aktiviteleri" },
            { "SystemAlert", "Sistem Uyarıları" }
        };
        
        // Desteklenen eylem türleri
        private static readonly Dictionary<string, string> _supportedActionTypes = new Dictionary<string, string>
        {
            { "Create", "Oluşturma" },
            { "Update", "Güncelleme" },
            { "Delete", "Silme" },
            { "Complete", "Tamamlama" },
            { "Error", "Hata" },
            { "Login", "Giriş" },
            { "Logout", "Çıkış" }
        };

        public NotificationService(
            ILogger<NotificationService> logger,
            IConfiguration configuration,
            NanoServiceDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _webPushClient = new WebPushClient();
            
            // VAPID anahtarlarını yapılandırmadan al
            var vapidPublicKey = _configuration["PushNotification:VapidDetails:PublicKey"];
            var vapidPrivateKey = _configuration["PushNotification:VapidDetails:PrivateKey"];
            var vapidSubject = _configuration["PushNotification:VapidDetails:Subject"]; // Genellikle "mailto:your-email@example.com"
            
            if (string.IsNullOrEmpty(vapidPublicKey) || string.IsNullOrEmpty(vapidPrivateKey) || string.IsNullOrEmpty(vapidSubject))
            {
                _logger.LogWarning("VAPID anahtarları yapılandırılmamış. Push bildirimleri gönderilemeyecek.");
            }
            else
            {
                _vapidDetails = new VapidDetails(vapidSubject, vapidPublicKey, vapidPrivateKey);
            }
        }

        /// <summary>
        /// Push bildirimi gönderir
        /// </summary>
        /// <param name="notification">Bildirim içeriği</param>
        /// <returns>İşlem başarılı ise true</returns>
        public async Task<bool> SendPushNotificationAsync(PushNotification notification)
        {
            try
            {
                // Güvenlik kontrolleri
                if (_vapidDetails == null)
                {
                    _logger.LogWarning("VAPID anahtarları yapılandırılmamış. Push bildirimi gönderilemedi.");
                    return false;
                }
                
                // Bildirim içeriği güvenlik kontrolü
                if (notification == null || string.IsNullOrEmpty(notification.Title) || string.IsNullOrEmpty(notification.Body))
                {
                    _logger.LogWarning("Geçersiz bildirim içeriği. Push bildirimi gönderilemedi.");
                    return false;
                }

                // Tüm kayıtlı abonelikleri al
                var subscriptions = await _context.PushSubscriptions
                    .Where(s => s.IsActive)
                    .ToListAsync();
                
                if (subscriptions == null || !subscriptions.Any())
                {
                    _logger.LogInformation("Kayıtlı push aboneliği bulunamadı. Bildirim gönderilemedi.");
                    return false;
                }
                
                return await SendPushNotificationAsync(notification, subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Push bildirimi gönderilirken hata oluştu.");
                return false;
            }
        }
        
        /// <summary>
        /// Belirli bir kullanıcıya push bildirimi gönderir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="title">Bildirim başlığı</param>
        /// <param name="body">Bildirim içeriği</param>
        /// <param name="url">Bildirime tıklandığında açılacak URL</param>
        /// <returns>İşlem başarılı ise true</returns>
        public async Task<bool> SendPushNotificationAsync(string userId, string title, string body, string url = null)
        {
            try
            {
                // Güvenlik kontrolleri
                if (_vapidDetails == null)
                {
                    _logger.LogWarning("VAPID anahtarları yapılandırılmamış. Push bildirimi gönderilemedi.");
                    return false;
                }
                
                // Bildirim içeriği güvenlik kontrolü
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
                {
                    _logger.LogWarning("Geçersiz bildirim içeriği. Push bildirimi gönderilemedi.");
                    return false;
                }

                // Kullanıcının kayıtlı aboneliklerini al
                Guid userGuid;
                List<ErpMobile.Api.Entities.PushSubscription> subscriptions = new List<ErpMobile.Api.Entities.PushSubscription>();
                
                if (Guid.TryParse(userId, out userGuid))
                {
                    subscriptions = await _context.PushSubscriptions
                        .Where(s => s.UserId == userGuid && s.IsActive)
                        .ToListAsync();
                }
                
                if (subscriptions == null || !subscriptions.Any())
                {
                    _logger.LogInformation("Kullanıcı için kayıtlı push aboneliği bulunamadı. UserId: {UserId}", userId);
                    return false;
                }
                
                var notification = new PushNotification
                {
                    Title = title,
                    Body = body,
                    Icon = "/assets/icons/logo.png",
                    Badge = "/assets/icons/badge.png",
                    Url = url
                };
                
                return await SendPushNotificationAsync(notification, subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcıya push bildirimi gönderilirken hata oluştu. UserId: {UserId}", userId);
                return false;
            }
        }
        
        /// <summary>
        /// Belirtilen aboneliklere push bildirimi gönderir
        /// </summary>
        /// <param name="notification">Bildirim içeriği</param>
        /// <param name="subscriptions">Bildirim gönderilecek abonelikler</param>
        /// <returns>İşlem başarılı ise true</returns>
        private async Task<bool> SendPushNotificationAsync(PushNotification notification, List<ErpMobile.Api.Entities.PushSubscription> subscriptions)
        {
            try
            {
                if (subscriptions == null || !subscriptions.Any())
                {
                    _logger.LogInformation("Bildirim gönderilecek abonelik bulunamadı.");
                    return false;
                }
                
                _logger.LogInformation("{Count} adet aboneliğe push bildirimi gönderiliyor.", subscriptions.Count);
                
                // Bildirim içeriğini JSON olarak hazırla
                var notificationPayload = new
                {
                    notification.Title,
                    notification.Body,
                    Url = notification.Url ?? "",
                    notification.Icon,
                    notification.Badge,
                    notification.Data
                };
                
                string payload = JsonSerializer.Serialize(notificationPayload);
                int successCount = 0;
                
                // Her abonelik için bildirim gönder
                foreach (var subscription in subscriptions)
                {
                    try
                    {
                        var webPushSubscription = new WebPush.PushSubscription(
                            subscription.Endpoint,
                            subscription.P256dh,
                            subscription.Auth
                        );
                        
                        await _webPushClient.SendNotificationAsync(webPushSubscription, payload, _vapidDetails);
                        successCount++;
                    }
                    catch (WebPushException ex)
                    {
                        _logger.LogError(ex, "Push bildirimi gönderilirken WebPush hatası oluştu. StatusCode: {StatusCode}, Endpoint: {Endpoint}",
                            ex.StatusCode, subscription.Endpoint);
                        
                        // Abonelik geçersiz olmuş olabilir, devre dışı bırak
                        if (ex.StatusCode == System.Net.HttpStatusCode.Gone || 
                            ex.StatusCode == System.Net.HttpStatusCode.NotFound ||
                            ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            subscription.IsActive = false;
                            _context.PushSubscriptions.Update(subscription);
                            await _context.SaveChangesAsync();
                            
                            _logger.LogInformation("Geçersiz push aboneliği devre dışı bırakıldı. Endpoint: {Endpoint}", subscription.Endpoint);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Push bildirimi gönderilirken beklenmeyen hata oluştu. Endpoint: {Endpoint}", subscription.Endpoint);
                    }
                }
                
                _logger.LogInformation("{SuccessCount}/{TotalCount} aboneliğe bildirim başarıyla gönderildi.", 
                    successCount, subscriptions.Count);
                
                return successCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Push bildirimi gönderme işlemi sırasında hata oluştu.");
                return false;
            }
        }
        
        /// <summary>
        /// Yeni push aboneliği kaydeder
        /// </summary>
        /// <param name="subscription">Abonelik bilgileri</param>
        /// <returns>İşlem başarılı ise true</returns>
        public async Task<bool> SaveSubscriptionAsync(PushSubscriptionRequest subscription)
        {
            try
            {
                _logger.LogInformation("Push aboneliği kaydediliyor. UserId: {UserId}, Endpoint: {Endpoint}", 
                    subscription.UserId, subscription.Endpoint);
                
                if (string.IsNullOrEmpty(subscription.Endpoint) || 
                    string.IsNullOrEmpty(subscription.P256dh) || 
                    string.IsNullOrEmpty(subscription.Auth))
                {
                    _logger.LogWarning("Geçersiz push abonelik bilgileri.");
                    return false;
                }
                
                // Mevcut bir abonelik var mı?
                var existingSubscription = await _context.PushSubscriptions
                    .FirstOrDefaultAsync(s => s.Endpoint == subscription.Endpoint);
                
                if (existingSubscription != null)
                {
                    // Mevcut aboneliği güncelle
                    existingSubscription.P256dh = subscription.P256dh;
                    existingSubscription.Auth = subscription.Auth;
                    existingSubscription.UserId = subscription.UserId;
                    existingSubscription.IsActive = true;
                    existingSubscription.UpdatedAt = DateTime.Now;
                    
                    _context.PushSubscriptions.Update(existingSubscription);
                    await _context.SaveChangesAsync();
                    
                    _logger.LogInformation("Mevcut push aboneliği güncellendi. UserId: {UserId}, Endpoint: {Endpoint}", 
                        existingSubscription.UserId.HasValue ? existingSubscription.UserId.Value.ToString() : "null", subscription.Endpoint);
                    return true;
                }
                
                // Yeni abonelik oluştur
                var newSubscription = new ErpMobile.Api.Entities.PushSubscription
                {
                    Id = Guid.NewGuid(),
                    Endpoint = subscription.Endpoint,
                    P256dh = subscription.P256dh,
                    Auth = subscription.Auth,
                    UserAgent = subscription.UserAgent,
                    UserId = subscription.UserId,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                
                _context.PushSubscriptions.Add(newSubscription);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Yeni push aboneliği eklendi. UserId: {UserId}, Endpoint: {Endpoint}", 
                    newSubscription.UserId.HasValue ? newSubscription.UserId.Value.ToString() : "null", subscription.Endpoint);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Push aboneliği kaydedilirken hata oluştu.");
                return false;
            }
        }
        
        /// <summary>
        /// Push aboneliğini siler
        /// </summary>
        /// <param name="endpoint">Abonelik endpoint'i</param>
        /// <returns>İşlem başarılı ise true</returns>
        public async Task<bool> DeleteSubscriptionAsync(string endpoint)
        {
            try
            {
                if (string.IsNullOrEmpty(endpoint))
                {
                    _logger.LogWarning("Geçersiz endpoint.");
                    return false;
                }
                
                var subscription = await _context.PushSubscriptions
                    .FirstOrDefaultAsync(s => s.Endpoint == endpoint);
                
                if (subscription == null)
                {
                    _logger.LogWarning("Silinecek abonelik bulunamadı. Endpoint: {Endpoint}", endpoint);
                    return false;
                }
                
                _context.PushSubscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Push aboneliği silindi. UserId: {UserId}, Endpoint: {Endpoint}", 
                    subscription.UserId, subscription.Endpoint);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Push aboneliği silinirken hata oluştu.");
                return false;
            }
        }

        /// <summary>
        /// VAPID public key'i döndürür
        /// </summary>
        /// <returns>VAPID public key</returns>
        public string GetVapidPublicKey()
        {
            return _configuration["PushNotification:VapidDetails:PublicKey"];
        }
        
        /// <summary>
        /// Kullanıcı için bildirim tercihlerini alır
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <returns>Bildirim tercihleri</returns>
        public async Task<IEnumerable<NotificationPreferenceFlatDto>> GetNotificationPreferencesForUserAsync(string userId)
        {
            try
            {
                _logger.LogInformation("Kullanıcı bildirim tercihleri alınıyor. UserId: {UserId}", userId);
                
                // Kullanıcı tercihleri
                var userPreferences = await _context.NotificationPreferences
                    .Where(p => p.UserId == userId)
                    .ToListAsync();
                
                // Global tercihler
                var globalPreferences = await _context.NotificationPreferences
                    .Where(p => p.UserId == null)
                    .ToListAsync();
                
                // Düz liste olarak tüm bildirim tercihlerini oluştur
                var result = new List<NotificationPreferenceFlatDto>();
                
                // Desteklenen tüm bildirim türleri ve eylemler için tercih oluştur
                foreach (var notificationType in _supportedNotificationTypes)
                {
                    foreach (var actionType in _supportedActionTypes)
                    {
                        // Kullanıcı tercihi var mı?
                        var userPreference = userPreferences
                            .FirstOrDefault(p => p.NotificationType == notificationType.Key && p.ActionType == actionType.Key);
                        
                        // Global tercih var mı?
                        var globalPreference = globalPreferences
                            .FirstOrDefault(p => p.NotificationType == notificationType.Key && p.ActionType == actionType.Key);
                        
                        // Tercihi belirle (kullanıcı tercihi varsa onu kullan, yoksa global tercih)
                        bool isEnabled = true; // Varsayılan olarak etkin
                        bool isUserPreference = false;
                        bool isGlobalPreference = false;
                        
                        if (userPreference != null)
                        {
                            isEnabled = userPreference.IsEnabled;
                            isUserPreference = true;
                        }
                        else if (globalPreference != null)
                        {
                            isEnabled = globalPreference.IsEnabled;
                            isGlobalPreference = true;
                        }
                        
                        // Tercihi listeye ekle
                        result.Add(new NotificationPreferenceFlatDto
                        {
                            NotificationType = notificationType.Key,
                            NotificationTypeDisplayName = notificationType.Value,
                            ActionType = actionType.Key,
                            ActionTypeDisplayName = actionType.Value,
                            IsEnabled = isEnabled,
                            IsUserPreference = isUserPreference,
                            IsGlobalPreference = isGlobalPreference
                        });
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı bildirim tercihleri alınırken hata oluştu. UserId: {UserId}", userId);
                return new List<NotificationPreferenceFlatDto>();
            }
        }
        
        /// <summary>
        /// Kullanıcı bildirim tercihini günceller
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="notificationType">Bildirim türü</param>
        /// <param name="actionType">Eylem türü</param>
        /// <param name="isEnabled">Etkin mi?</param>
        /// <returns>İşlem sonucu</returns>
        public async Task<bool> UpdateUserNotificationPreferenceAsync(string userId, string notificationType, string actionType, bool isEnabled)
        {
            try
            {
                _logger.LogInformation("Kullanıcı bildirim tercihi güncelleniyor. UserId: {UserId}, NotificationType: {NotificationType}, ActionType: {ActionType}, IsEnabled: {IsEnabled}", 
                    userId, notificationType, actionType, isEnabled);
                
                // Geçerlilik kontrolü
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(notificationType) || string.IsNullOrEmpty(actionType))
                {
                    _logger.LogWarning("Geçersiz bildirim tercihi parametreleri.");
                    return false;
                }
                
                // Mevcut tercih var mı?
                var preference = await _context.NotificationPreferences
                    .FirstOrDefaultAsync(p => p.UserId == userId && 
                                       p.NotificationType == notificationType && 
                                       p.ActionType == actionType);
                
                if (preference != null)
                {
                    // Mevcut tercihi güncelle
                    preference.IsEnabled = isEnabled;
                    preference.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // Yeni tercih oluştur
                    preference = new NotificationPreference
                    {
                        UserId = userId,
                        NotificationType = notificationType,
                        ActionType = actionType,
                        IsEnabled = isEnabled,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    
                    await _context.NotificationPreferences.AddAsync(preference);
                }
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı bildirim tercihi güncellenirken hata oluştu.");
                return false;
            }
        }
        
        /// <summary>
        /// Global bildirim tercihini günceller (admin için)
        /// </summary>
        /// <param name="notificationType">Bildirim türü</param>
        /// <param name="actionType">Eylem türü</param>
        /// <param name="isEnabled">Etkin mi?</param>
        /// <returns>İşlem sonucu</returns>
        public async Task<bool> UpdateGlobalNotificationPreferenceAsync(string notificationType, string actionType, bool isEnabled)
        {
            try
            {
                _logger.LogInformation("Global bildirim tercihi güncelleniyor. NotificationType: {NotificationType}, ActionType: {ActionType}, IsEnabled: {IsEnabled}", 
                    notificationType, actionType, isEnabled);
                
                // Geçerlilik kontrolü
                if (string.IsNullOrEmpty(notificationType) || string.IsNullOrEmpty(actionType))
                {
                    _logger.LogWarning("Geçersiz bildirim tercihi parametreleri.");
                    return false;
                }
                
                // Mevcut tercih var mı?
                var preference = await _context.NotificationPreferences
                    .FirstOrDefaultAsync(p => p.UserId == null && 
                                       p.NotificationType == notificationType && 
                                       p.ActionType == actionType);
                
                if (preference != null)
                {
                    // Mevcut tercihi güncelle
                    preference.IsEnabled = isEnabled;
                    preference.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // Yeni tercih oluştur
                    preference = new NotificationPreference
                    {
                        UserId = null,
                        NotificationType = notificationType,
                        ActionType = actionType,
                        IsEnabled = isEnabled,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    
                    await _context.NotificationPreferences.AddAsync(preference);
                }
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Global bildirim tercihi güncellenirken hata oluştu.");
                return false;
            }
        }
        
        /// <summary>
        /// Kullanıcı bildirim tercihini siler ve global tercihe geri döner
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="notificationType">Bildirim türü</param>
        /// <param name="actionType">Eylem türü</param>
        /// <returns>İşlem sonucu</returns>
        public async Task<bool> ResetUserNotificationPreferenceAsync(string userId, string notificationType, string actionType)
        {
            try
            {
                _logger.LogInformation("Kullanıcı bildirim tercihi sıfırlanıyor. UserId: {UserId}, NotificationType: {NotificationType}, ActionType: {ActionType}", 
                    userId, notificationType, actionType);
                
                // Geçerlilik kontrolü
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(notificationType) || string.IsNullOrEmpty(actionType))
                {
                    _logger.LogWarning("Geçersiz bildirim tercihi parametreleri.");
                    return false;
                }
                
                // Mevcut tercih var mı?
                var preference = await _context.NotificationPreferences
                    .FirstOrDefaultAsync(p => p.UserId == userId && 
                                       p.NotificationType == notificationType && 
                                       p.ActionType == actionType);
                
                if (preference != null)
                {
                    // Tercihi sil
                    _context.NotificationPreferences.Remove(preference);
                    await _context.SaveChangesAsync();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı bildirim tercihi sıfırlanırken hata oluştu.");
                return false;
            }
        }
        
        /// <summary>
        /// Yedekleme tamamlandığında bildirim gönderir
        /// </summary>
        /// <param name="databaseName">Veritabanı adı</param>
        /// <param name="backupType">Yedekleme tipi</param>
        /// <param name="success">Başarılı mı</param>
        /// <param name="message">Mesaj</param>
        /// <returns>İşlem başarılı ise true</returns>
        public async Task<bool> SendBackupCompletedNotificationAsync(string databaseName, string backupType, bool success, string message)
        {
            try
            {
                _logger.LogInformation("Yedekleme tamamlandı bildirimi gönderiliyor. Database: {Database}, BackupType: {BackupType}, Success: {Success}", 
                    databaseName, backupType, success);
                
                // Admin kullanıcılarına bildirim gönder
                // Not: Gerçek uygulamada admin kullanıcıları veritabanından çekilmelidir
                var adminUserIds = new List<string> { "admin", "system" };
                
                string title = success ? "Yedekleme Başarılı" : "Yedekleme Hatası";
                string body = $"{databaseName} veritabanı {backupType} yedeklemesi {(success ? "başarıyla tamamlandı" : "başarısız oldu")}. {message}";
                string url = "/admin/backups";
                
                bool allSuccess = true;
                foreach (var adminId in adminUserIds)
                {
                    // Her admin için bildirim tercihi kontrolü yap
                    bool shouldSend = await ShouldSendNotificationAsync(adminId, "DatabaseBackup", success ? "Complete" : "Error");
                    
                    if (shouldSend)
                    {
                        bool result = await SendPushNotificationAsync(adminId, title, body, url);
                        if (!result) allSuccess = false;
                    }
                }
                
                return allSuccess;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme tamamlandı bildirimi gönderilirken hata oluştu. Database: {Database}", databaseName);
                return false;
            }
        }
        
        /// <summary>
        /// Belirli bir kullanıcı için bildirim tercihlerini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği (null ise genel ayarlar)</param>
        /// <returns>Bildirim tercihleri</returns>
        public async Task<NotificationPreferenceDto> GetNotificationPreferencesAsync(string userId)
        {
            try
            {
                _logger.LogInformation("Bildirim tercihleri getiriliyor. UserId: {UserId}", userId ?? "Global");
                
                // Desteklenen tüm bildirim türlerini al
                var notificationTypes = await GetSupportedNotificationTypesAsync();
                
                // Kullanıcı tercihlerini getir
                var userPreferences = string.IsNullOrEmpty(userId) 
                    ? await _context.NotificationPreferences.Where(p => p.UserId == null).ToListAsync()
                    : await _context.NotificationPreferences.Where(p => p.UserId == userId).ToListAsync();
                
                // Global tercihleri getir (kullanıcı tercihleri için fallback olarak kullanılacak)
                var globalPreferences = string.IsNullOrEmpty(userId)
                    ? new List<NotificationPreference>()
                    : await _context.NotificationPreferences.Where(p => p.UserId == null).ToListAsync();
                
                // Her bildirim türü için kullanıcı tercihlerini uygula
                foreach (var typePreference in notificationTypes)
                {
                    foreach (var actionPreference in typePreference.Actions)
                    {
                        actionPreference.IsEnabled = GetEffectivePreference(
                            userPreferences, 
                            globalPreferences, 
                            typePreference.NotificationType, 
                            actionPreference.ActionType);
                    }
                }
                
                // NotificationPreferenceDto oluştur
                var result = new NotificationPreferenceDto
                {
                    UserId = userId,
                    Preferences = notificationTypes
                };
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bildirim tercihleri getirilirken hata oluştu. UserId: {UserId}", userId ?? "Global");
                throw;
            }
        }
        
        /// <summary>
        /// Desteklenen bildirim türlerini getirir
        /// </summary>
        /// <returns>Desteklenen bildirim türleri listesi</returns>
        public async Task<List<NotificationTypePreference>> GetSupportedNotificationTypesAsync()
        {
            try
            {
                _logger.LogInformation("Desteklenen bildirim türleri getiriliyor");
                
                // Bu metot async olarak tanımlanmış ama şu an için senkron çalışıyor
                // İleride veritabanından dinamik olarak çekilebilir
                var result = new List<NotificationTypePreference>();
                
                foreach (var notificationType in _supportedNotificationTypes)
                {
                    var typePreference = new NotificationTypePreference
                    {
                        NotificationType = notificationType.Key,
                        DisplayName = notificationType.Value,
                        Actions = new List<NotificationActionPreference>()
                    };
                    
                    foreach (var actionType in _supportedActionTypes)
                    {
                        typePreference.Actions.Add(new NotificationActionPreference
                        {
                            ActionType = actionType.Key,
                            DisplayName = actionType.Value,
                            IsEnabled = true // Varsayılan olarak tüm bildirimler etkin
                        });
                    }
                    
                    result.Add(typePreference);
                }
                
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Desteklenen bildirim türleri getirilirken hata oluştu");
                throw;
            }
        }
        
        // Kullanıcı ve global tercihlerden efektif tercihi belirler
        private bool GetEffectivePreference(List<NotificationPreference> userPreferences, List<NotificationPreference> globalPreferences, 
            string notificationType, string actionType)
        {
            // Önce kullanıcı tercihine bak
            var userPreference = userPreferences.FirstOrDefault(p => 
                p.NotificationType == notificationType && p.ActionType == actionType);
                
            if (userPreference != null)
                return userPreference.IsEnabled;
                
            // Kullanıcı tercihi yoksa global tercihe bak
            var globalPreference = globalPreferences.FirstOrDefault(p => 
                p.NotificationType == notificationType && p.ActionType == actionType);
                
            if (globalPreference != null)
                return globalPreference.IsEnabled;
                
            // Hiç tercih yoksa varsayılan olarak true döndür
            return true;
        }
        
        /// <summary>
        /// Belirli bir bildirim türü ve eylemi için bildirim gönderilip gönderilmeyeceğini kontrol eder
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="notificationType">Bildirim türü</param>
        /// <param name="actionType">Eylem türü</param>
        /// <returns>Bildirim gönderilecekse true</returns>
        public async Task<bool> ShouldSendNotificationAsync(string userId, string notificationType, string actionType)
        {
            try
            {
                // Geçerlilik kontrolü
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(notificationType) || string.IsNullOrEmpty(actionType))
                {
                    _logger.LogWarning("Geçersiz bildirim kontrolü parametreleri. Varsayılan olarak bildirim gönderilecek.");
                    return true; // Varsayılan olarak bildirim gönder
                }
                
                // Kullanıcı tercihi var mı?
                var userPreference = await _context.NotificationPreferences
                    .FirstOrDefaultAsync(p => p.UserId == userId && 
                                       p.NotificationType == notificationType && 
                                       p.ActionType == actionType);
                
                if (userPreference != null)
                {
                    return userPreference.IsEnabled;
                }
                
                // Global tercih var mı?
                var globalPreference = await _context.NotificationPreferences
                    .FirstOrDefaultAsync(p => p.UserId == null && 
                                       p.NotificationType == notificationType && 
                                       p.ActionType == actionType);
                
                if (globalPreference != null)
                {
                    return globalPreference.IsEnabled;
                }
                
                // Hiçbir tercih yoksa varsayılan olarak bildirim gönder
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bildirim kontrolü yapılırken hata oluştu. Varsayılan olarak bildirim gönderilecek.");
                return true; // Hata durumunda varsayılan olarak bildirim gönder
            }
        }
        
        /// <summary>
        /// Bildirim tercihlerini kaydeder
        /// </summary>
        /// <param name="preferences">Bildirim tercihleri</param>
        /// <param name="username">İşlemi yapan kullanıcı</param>
        /// <returns>İşlem başarılı ise true</returns>
        public async Task<bool> SaveNotificationPreferencesAsync(NotificationPreferenceDto preferences, string username)
        {
            try
            {
                if (preferences == null || preferences.Preferences == null)
                {
                    _logger.LogWarning("Geçersiz bildirim tercihleri.");
                    return false;
                }
                
                // Mevcut tercihleri al
                var existingPreferences = await _context.NotificationPreferences
                    .Where(p => p.UserId == preferences.UserId)
                    .ToListAsync();
                
                // Yeni tercihler listesi
                var newPreferences = new List<NotificationPreference>();
                
                foreach (var typePreference in preferences.Preferences)
                {
                    foreach (var actionPreference in typePreference.Actions)
                    {
                        // Mevcut tercihi bul
                        var existingPreference = existingPreferences
                            .FirstOrDefault(p => p.NotificationType == typePreference.NotificationType && 
                                          p.ActionType == actionPreference.ActionType);
                        
                        if (existingPreference != null)
                        {
                            // Mevcut tercihi güncelle
                            existingPreference.IsEnabled = actionPreference.IsEnabled;
                            existingPreference.UpdatedAt = DateTime.Now;
                            _context.NotificationPreferences.Update(existingPreference);
                        }
                        else
                        {
                            // Yeni tercih oluştur
                            newPreferences.Add(new NotificationPreference
                            {
                                UserId = preferences.UserId,
                                NotificationType = typePreference.NotificationType,
                                ActionType = actionPreference.ActionType,
                                IsEnabled = actionPreference.IsEnabled,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            });
                        }
                    }
                }
                
                // Yeni tercihleri ekle
                if (newPreferences.Any())
                {
                    await _context.NotificationPreferences.AddRangeAsync(newPreferences);
                }
                
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("{Username} tarafından bildirim tercihleri güncellendi. Kullanıcı: {UserId}", 
                    username, preferences.UserId ?? "Global");
                    
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bildirim tercihleri kaydedilirken hata oluştu.");
                return false;
            }
        }
    }
}
