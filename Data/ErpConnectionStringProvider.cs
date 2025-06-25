using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ErpMobile.Api.Data
{
    /// <summary>
    /// ERP veritabanı bağlantı dizesini global olarak tutan yardımcı sınıf
    /// </summary>
    public static class ErpConnectionStringProvider
    {
        private static IServiceProvider _serviceProvider;
        private static IConfiguration _configuration;

        /// <summary>
        /// Servis sağlayıcıyı ayarlar
        /// </summary>
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        /// <summary>
        /// Geçerli ERP veritabanı bağlantı dizesi
        /// </summary>
        public static string CurrentConnectionString { get; set; }

        /// <summary>
        /// Güncel bağlantı dizesini alır
        /// </summary>
        public static string GetConnectionString()
        {
            // 1. Önce CurrentConnectionString'i kontrol et
            if (!string.IsNullOrEmpty(CurrentConnectionString))
            {
                return CurrentConnectionString;
            }

            // 2. HttpContext'ten almayı dene
            var httpContextAccessor = _serviceProvider?.GetService<IHttpContextAccessor>();
            if (httpContextAccessor?.HttpContext?.Items["SelectedDatabaseConnectionString"] is string httpContextConnectionString)
            {
                return httpContextConnectionString;
            }

            // 3. Son olarak appsettings.json'dan al
            return _configuration?.GetConnectionString("ErpConnection");
        }
    }
}
