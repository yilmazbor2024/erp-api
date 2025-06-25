using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ErpMobile.Api.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ErpDbContext _erpDbContext;
        private readonly ILogger<TestController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TestController(
            ErpDbContext erpDbContext,
            ILogger<TestController> logger,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _erpDbContext = erpDbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        [HttpGet("database-info")]
        public async Task<IActionResult> GetDatabaseInfo()
        {
            try
            {
                // HttpContext'ten veritabanı bilgilerini al
                var databaseId = _httpContextAccessor.HttpContext?.Items["SelectedDatabaseId"]?.ToString();
                var databaseName = _httpContextAccessor.HttpContext?.Items["SelectedDatabaseName"]?.ToString();
                var connectionString = _httpContextAccessor.HttpContext?.Items["SelectedDatabaseConnectionString"]?.ToString();

                // Mevcut veritabanı adını sorgula
                string query = "SELECT DB_NAME() AS CurrentDatabase";
                var result = await _erpDbContext.ExecuteQueryAsync(query, new SqlParameter[] { });
                
                string currentDb = "Bilinmiyor";
                if (result.Rows.Count > 0)
                {
                    currentDb = result.Rows[0]["CurrentDatabase"]?.ToString() ?? "Bilinmiyor";
                }

                // Tüm bilgileri döndür
                return Ok(new
                {
                    RequestedDatabaseId = databaseId ?? "Belirtilmemiş",
                    RequestedDatabaseName = databaseName ?? "Belirtilmemiş",
                    ActualDatabaseName = currentDb,
                    Headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                    HttpContextItems = _httpContextAccessor.HttpContext?.Items
                        .Where(i => i.Key is string)
                        .ToDictionary(i => i.Key.ToString(), i => i.Value?.ToString() ?? "null")
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veritabanı bilgisi alınırken hata oluştu");
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
        
        [HttpGet("connection-test")]
        public IActionResult TestConnectionStrings()
        {
            try
            {
                // appsettings.json'daki bağlantı dizelerini al
                var defaultConnection = _configuration.GetConnectionString("DefaultConnection");
                var erpConnection = _configuration.GetConnectionString("ErpConnection");
                
                // HttpContext'teki bağlantı dizesini al
                var dynamicConnection = _httpContextAccessor.HttpContext?.Items["SelectedDatabaseConnectionString"]?.ToString();
                
                // Tüm bağlantı dizelerini döndür
                return Ok(new
                {
                    DefaultConnection = defaultConnection ?? "Bulunamadı",
                    ErpConnection = erpConnection ?? "Bulunamadı",
                    DynamicConnection = dynamicConnection ?? "Bulunamadı",
                    ConfigurationProviders = (_configuration as IConfigurationRoot)?.Providers
                        .Select(p => p.GetType().Name)
                        .ToList(),
                    Message = "Bağlantı dizeleri başarıyla alındı. Dinamik bağlantı dizesi varsa, appsettings.json'daki bağlantı dizesi olmadan da çalışabilir."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
        
        [HttpGet("direct-connection-test")]
        public async Task<IActionResult> TestDirectConnection()
        {
            try
            {
                var results = new List<object>();
                
                // 1. ErpDbContext üzerinden bağlantı testi
                string query1 = "SELECT DB_NAME() AS CurrentDatabase, @@SERVERNAME AS ServerName";
                var result1 = await _erpDbContext.ExecuteQueryAsync(query1, new SqlParameter[] { });
                
                string currentDb1 = "Bilinmiyor";
                string serverName1 = "Bilinmiyor";
                if (result1.Rows.Count > 0)
                {
                    currentDb1 = result1.Rows[0]["CurrentDatabase"]?.ToString() ?? "Bilinmiyor";
                    serverName1 = result1.Rows[0]["ServerName"]?.ToString() ?? "Bilinmiyor";
                }
                
                results.Add(new { Method = "ExecuteQueryAsync", Database = currentDb1, Server = serverName1 });
                
                // 2. ExecuteScalarAsync testi
                string query2 = "SELECT DB_NAME()";
                var result2 = await _erpDbContext.ExecuteScalarAsync(query2, new SqlParameter[] { });
                string currentDb2 = result2?.ToString() ?? "Bilinmiyor";
                
                results.Add(new { Method = "ExecuteScalarAsync", Database = currentDb2 });
                
                // 3. ExecuteNonQueryAsync testi (sadece bağlantı kontrolü için)
                string query3 = "SELECT 1";
                await _erpDbContext.ExecuteNonQueryAsync(query3, new SqlParameter[] { });
                
                results.Add(new { Method = "ExecuteNonQueryAsync", Status = "Başarılı" });
                
                // 4. ExecuteReaderAsync testi
                string query4 = "SELECT DB_NAME() AS CurrentDatabase";
                string currentDb4 = "Bilinmiyor";
                
                using (var reader = await _erpDbContext.ExecuteReaderAsync(query4))
                {
                    if (reader.Read())
                    {
                        currentDb4 = reader["CurrentDatabase"]?.ToString() ?? "Bilinmiyor";
                    }
                }
                
                results.Add(new { Method = "ExecuteReaderAsync", Database = currentDb4 });
                
                // 5. Doğrudan SqlConnection testi
                var connectionString = _httpContextAccessor.HttpContext?.Items["SelectedDatabaseConnectionString"]?.ToString();
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new SqlCommand("SELECT DB_NAME() AS CurrentDatabase", connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                if (reader.Read())
                                {
                                    string directDb = reader["CurrentDatabase"]?.ToString() ?? "Bilinmiyor";
                                    results.Add(new { Method = "DirectSqlConnection", Database = directDb });
                                }
                            }
                        }
                    }
                }
                
                // Bağlantı dizesi bilgilerini göster
                var databaseId = _httpContextAccessor.HttpContext?.Items["SelectedDatabaseId"]?.ToString();
                var databaseName = _httpContextAccessor.HttpContext?.Items["SelectedDatabaseName"]?.ToString();
                
                return Ok(new
                {
                    Results = results,
                    RequestedDatabaseId = databaseId ?? "Belirtilmemiş",
                    RequestedDatabaseName = databaseName ?? "Belirtilmemiş",
                    ConnectionString = MaskPassword(connectionString ?? "Belirtilmemiş")
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bağlantı testi sırasında hata oluştu");
                return StatusCode(500, new { error = ex.Message });
            }
        }
        
        private string MaskPassword(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return connectionString;
                
            // Password=xyz; veya Password="xyz"; gibi kısımları maskele
            var builder = new SqlConnectionStringBuilder(connectionString);
            builder.Password = "*****";
            return builder.ConnectionString;
        }
    }
}
