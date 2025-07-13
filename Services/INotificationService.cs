using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Notification;

namespace ErpMobile.Api.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Push bildirimi gönderir
        /// </summary>
        /// <param name="notification">Bildirim içeriği</param>
        /// <returns>İşlem başarılı ise true</returns>
        Task<bool> SendPushNotificationAsync(PushNotification notification);
        
        /// <summary>
        /// Belirli bir kullanıcıya push bildirimi gönderir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="title">Bildirim başlığı</param>
        /// <param name="body">Bildirim içeriği</param>
        /// <param name="url">Bildirime tıklandığında açılacak URL</param>
        /// <returns>İşlem başarılı ise true</returns>
        Task<bool> SendPushNotificationAsync(string userId, string title, string body, string url = null);

        /// <summary>
        /// Yeni push aboneliği kaydeder
        /// </summary>
        /// <param name="subscription">Abonelik bilgileri</param>
        /// <returns>İşlem başarılı ise true</returns>
        Task<bool> SaveSubscriptionAsync(PushSubscriptionRequest subscription);

        /// <summary>
        /// Push aboneliğini siler
        /// </summary>
        /// <param name="endpoint">Abonelik endpoint'i</param>
        /// <returns>İşlem başarılı ise true</returns>
        Task<bool> DeleteSubscriptionAsync(string endpoint);

        /// <summary>
        /// VAPID public key'i döndürür
        /// </summary>
        /// <returns>VAPID public key</returns>
        string GetVapidPublicKey();
        
        /// <summary>
        /// Yedekleme tamamlandığında bildirim gönderir
        /// </summary>
        /// <param name="databaseName">Veritabanı adı</param>
        /// <param name="backupType">Yedekleme tipi</param>
        /// <param name="success">Başarılı mı</param>
        /// <param name="message">Mesaj</param>
        /// <returns>İşlem başarılı ise true</returns>
        Task<bool> SendBackupCompletedNotificationAsync(string databaseName, string backupType, bool success, string message);
        
        /// <summary>
        /// Belirli bir kullanıcı için bildirim tercihlerini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği (null ise genel ayarlar)</param>
        /// <returns>Bildirim tercihleri</returns>
        Task<NotificationPreferenceDto> GetNotificationPreferencesAsync(string userId);
        
        /// <summary>
        /// Bildirim tercihlerini kaydeder
        /// </summary>
        /// <param name="preferences">Bildirim tercihleri</param>
        /// <param name="username">İşlemi yapan kullanıcı</param>
        /// <returns>İşlem başarılı ise true</returns>
        Task<bool> SaveNotificationPreferencesAsync(NotificationPreferenceDto preferences, string username);
        
        /// <summary>
        /// Belirli bir bildirim türü ve eylemi için bildirim gönderilip gönderilmeyeceğini kontrol eder
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="notificationType">Bildirim türü (CashTransaction, Invoice, Customer, etc.)</param>
        /// <param name="actionType">Bildirim alt türü (Create, Update, Delete, etc.)</param>
        /// <returns>Bildirim gönderilecekse true</returns>
        Task<bool> ShouldSendNotificationAsync(string userId, string notificationType, string actionType);
        
        /// <summary>
        /// Desteklenen bildirim türlerini getirir
        /// </summary>
        /// <returns>Desteklenen bildirim türleri listesi</returns>
        Task<List<NotificationTypePreference>> GetSupportedNotificationTypesAsync();
        
        /// <summary>
        /// Kullanıcı bildirim tercihlerini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <returns>Bildirim tercihleri</returns>
        Task<IEnumerable<NotificationPreferenceFlatDto>> GetNotificationPreferencesForUserAsync(string userId);
        
        /// <summary>
        /// Kullanıcı bildirim tercihini günceller
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="notificationType">Bildirim türü</param>
        /// <param name="actionType">Eylem türü</param>
        /// <param name="isEnabled">Etkin mi?</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> UpdateUserNotificationPreferenceAsync(string userId, string notificationType, string actionType, bool isEnabled);
        
        /// <summary>
        /// Global bildirim tercihini günceller (yönetici yetkisi gerektirir)
        /// </summary>
        /// <param name="notificationType">Bildirim türü</param>
        /// <param name="actionType">Eylem türü</param>
        /// <param name="isEnabled">Etkin mi?</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> UpdateGlobalNotificationPreferenceAsync(string notificationType, string actionType, bool isEnabled);
        
        /// <summary>
        /// Kullanıcı bildirim tercihini siler ve global tercihe geri döner
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="notificationType">Bildirim türü</param>
        /// <param name="actionType">Eylem türü</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> ResetUserNotificationPreferenceAsync(string userId, string notificationType, string actionType);
    }
}
