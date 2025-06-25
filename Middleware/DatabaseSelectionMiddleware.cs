using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ErpMobile.Api.Data;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration.Memory;

namespace ErpMobile.Api.Middleware
{
    public class DatabaseSelectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseSelectionMiddleware> _logger;
        private readonly IConfigurationRoot _configRoot;

        public DatabaseSelectionMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<DatabaseSelectionMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
            _configRoot = configuration as IConfigurationRoot;
        }

        public async Task InvokeAsync(HttpContext context, NanoServiceDbContext dbContext)
        {
            // Veritabanı ID'sini header'dan al
            _logger.LogInformation($"DatabaseSelectionMiddleware çalıştırılıyor. Path: {context.Request.Path}");
            
            if (context.Request.Headers.TryGetValue("X-Database-Id", out var databaseIdValues))
            {
                var databaseId = databaseIdValues.FirstOrDefault();
                
                if (!string.IsNullOrEmpty(databaseId))
                {
                    _logger.LogInformation($"Veritabanı seçimi header'dan alındı: {databaseId}");
                    
                    try
                    {
                        // Veritabanı bilgilerini al
                        var database = await dbContext.Databases.FirstOrDefaultAsync(d => d.Id.ToString() == databaseId && d.IsActive);
                        
                        if (database != null)
                        {
                            // Bağlantı dizesini al veya oluştur
                            var connectionString = database.ConnectionString;
                            if (string.IsNullOrEmpty(connectionString))
                            {
                                var defaultConnectionString = _configuration.GetConnectionString("ErpConnection");
                                if (string.IsNullOrEmpty(defaultConnectionString))
                                {
                                    _logger.LogWarning("ErpConnection bağlantı dizesi appsettings.json'da bulunamadı, veritabanı tablosundaki bağlantı dizesi kullanılacak");
                                    // Veritabanı tablosunda da bağlantı dizesi yoksa hata log'u
                                    if (string.IsNullOrEmpty(database.ConnectionString))
                                    {
                                        _logger.LogError("Veritabanı tablosunda da bağlantı dizesi bulunamadı!");
                                    }
                                    connectionString = database.ConnectionString; // Veritabanı tablosundaki bağlantı dizesini kullan
                                }
                                else
                                {
                                    var builder = new SqlConnectionStringBuilder(defaultConnectionString);
                                    builder.InitialCatalog = database.DatabaseName;
                                    connectionString = builder.ConnectionString;
                                }
                            }
                            
                            // HttpContext'e bağlantı bilgilerini ekle
                            context.Items["SelectedDatabaseConnectionString"] = connectionString;
                            context.Items["SelectedDatabaseName"] = database.DatabaseName;
                            context.Items["SelectedDatabaseId"] = databaseId;
                            
                            _logger.LogInformation($"Veritabanı bağlantısı değiştirildi: {database.DatabaseName}");
                            _logger.LogInformation($"Kullanılan bağlantı dizesi: {connectionString}");
                            
                            // Runtime'da IConfiguration'daki connection string'leri değiştir
                            UpdateConnectionStrings(connectionString);
                        }
                        else
                        {
                            _logger.LogWarning($"Veritabanı bulunamadı veya aktif değil: {databaseId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Veritabanı seçimi sırasında hata oluştu: {databaseId}");
                    }
                }
            }
            
            await _next(context);
        }
        
        private void UpdateConnectionStrings(string newConnectionString)
        {
            try
            {
                // HttpContext'e bağlantı dizesini eklemek yeterli olacak
                // Program.cs'de ErpDbContext oluşturulurken bu değer kullanılacak
                _logger.LogInformation($"Bağlantı dizesi güncellendi: {newConnectionString}");
                
                // Statik değişkene ata
                ErpConnectionStringProvider.CurrentConnectionString = newConnectionString;
                
                // IConfiguration'daki bağlantı dizesini değiştir
                try
                {
                    // Yeni bir bağlantı dizesi dictionary'si oluştur
                    var connectionStrings = new Dictionary<string, string>
                    {
                        { "ErpConnection", newConnectionString },
                        { "NanoServiceConnection", _configuration.GetConnectionString("NanoServiceConnection") }
                    };

                    // Yeni bir MemoryConfigurationSource oluştur
                    var memorySource = new MemoryConfigurationSource
                    {
                        InitialData = new[]
                        {
                            new KeyValuePair<string, string>("ConnectionStrings:ErpConnection", newConnectionString)
                        }
                    };

                    // ConfigurationRoot'a yeni kaynağı ekle
                    if (_configRoot != null)
                    {
                        _configRoot.GetType()
                            .GetMethod("AddSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            ?.Invoke(_configRoot, new object[] { new MemoryConfigurationProvider(memorySource) });
                        
                        _logger.LogInformation("IConfiguration'a yeni bağlantı dizesi kaynağı eklendi");
                    }
                    else
                    {
                        _logger.LogWarning("_configRoot null, IConfiguration güncellenemedi");
                    }
                }
                catch (Exception configEx)
                {
                    _logger.LogError(configEx, "IConfiguration güncellenirken hata oluştu");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bağlantı dizelerini güncellerken hata oluştu");
            }
        }
    }

    // Extension method for middleware registration
    public static class DatabaseSelectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseDatabaseSelection(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DatabaseSelectionMiddleware>();
        }
    }
}
