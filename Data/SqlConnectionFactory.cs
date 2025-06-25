using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace ErpMobile.Api.Data
{
    /// <summary>
    /// SQL bağlantılarını merkezi olarak yöneten fabrika sınıfı
    /// </summary>
    public class SqlConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SqlConnectionFactory> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SqlConnectionFactory(
            IConfiguration configuration,
            ILogger<SqlConnectionFactory> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Yeni bir SQL bağlantısı oluşturur ve açar
        /// </summary>
        public async Task<SqlConnection> CreateConnectionAsync()
        {
            try
            {
                // Bağlantı dizesini al
                string connectionString = GetConnectionString();
                
                _logger.LogInformation($"Yeni SQL bağlantısı oluşturuluyor. Bağlantı dizesi: {connectionString.Substring(0, Math.Min(20, connectionString.Length))}...");
                
                var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SQL bağlantısı oluşturulurken hata oluştu");
                throw;
            }
        }

        /// <summary>
        /// Güncel bağlantı dizesini alır
        /// </summary>
        public string GetConnectionString()
        {
            string connectionString = null;
            
            // 1. Önce ErpConnectionStringProvider'dan almayı dene
            if (!string.IsNullOrEmpty(ErpConnectionStringProvider.CurrentConnectionString))
            {
                connectionString = ErpConnectionStringProvider.CurrentConnectionString;
                _logger.LogInformation("ErpConnectionStringProvider'dan bağlantı dizesi alındı");
            }
            // 2. Sonra HttpContext'ten almayı dene
            else if (_httpContextAccessor?.HttpContext?.Items["SelectedDatabaseConnectionString"] is string httpContextConnectionString)
            {
                connectionString = httpContextConnectionString;
                _logger.LogInformation("HttpContext'ten bağlantı dizesi alındı");
            }
            // 3. Son olarak appsettings.json'dan al
            else
            {
                connectionString = _configuration.GetConnectionString("ErpConnection");
                _logger.LogInformation("IConfiguration'dan bağlantı dizesi alındı");
            }
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Bağlantı dizesi bulunamadı!");
            }
            
            return connectionString;
        }
    }
}
