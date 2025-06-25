using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ErpMobile.Api.Data
{
    public class ErpDbContext : DbContext
    {
        private readonly ILogger<ErpDbContext> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ErpDbContext(DbContextOptions<ErpDbContext> options, ILogger<ErpDbContext> logger, IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Eğer options zaten yapılandırılmışsa, tekrar yapılandırmaya gerek yok
            if (optionsBuilder.IsConfigured)
                return;
                
            // Bağlantı dizesini al
            string connectionString = null;
            
            // Önce ErpConnectionStringProvider'dan almayı dene
            if (!string.IsNullOrEmpty(ErpConnectionStringProvider.CurrentConnectionString))
            {
                connectionString = ErpConnectionStringProvider.CurrentConnectionString;
                _logger.LogInformation($"OnConfiguring: ErpConnectionStringProvider'dan bağlantı dizesi kullanılıyor");
            }
            // Sonra HttpContext'ten almayı dene
            else if (_httpContextAccessor?.HttpContext?.Items["SelectedDatabaseConnectionString"] is string httpContextConnectionString)
            {
                connectionString = httpContextConnectionString;
                _logger.LogInformation($"OnConfiguring: HttpContext'ten bağlantı dizesi kullanılıyor");
            }
            
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            else
            {
                _logger.LogWarning("OnConfiguring: Bağlantı dizesi bulunamadı!");
            }
        }

        public async Task<DataTable> ExecuteQueryAsync(string query, SqlParameter[] parameters)
        {
            try
            {
                using var connection = await GetConnectionAsync();

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);

                var dataTable = new DataTable();
                using var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);

                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SQL sorgusu çalıştırılırken hata oluştu");
                throw;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters)
        {
            try
            {
                using var connection = await GetConnectionAsync();

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);

                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SQL sorgusu çalıştırılırken hata oluştu");
                throw;
            }
        }

        public async Task<object> ExecuteScalarAsync(string query, SqlParameter[] parameters)
        {
            try
            {
                using var connection = await GetConnectionAsync();

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);

                return await command.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SQL sorgusu çalıştırılırken hata oluştu");
                throw;
            }
        }

        public async Task<SqlDataReader> ExecuteReaderAsync(string query, SqlParameter[]? parameters = null)
        {
            var connection = await GetConnectionAsync();
            var command = new SqlCommand(query, connection);
            
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        private async Task<SqlConnection> GetConnectionAsync()
        {
            try
            {
                // DbContext'in bağlantı dizesini al
                string connectionString = Database.GetConnectionString();
                _logger.LogInformation($"DbContext bağlantı dizesi: {connectionString}");
                
                // HttpContext üzerinden seçilen veritabanı bağlantı bilgisini kontrol et
                if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                {
                    _logger.LogInformation("HttpContext mevcut, veritabanı bilgisi kontrol ediliyor");
                    
                    if (_httpContextAccessor.HttpContext.Items.TryGetValue("SelectedDatabaseConnectionString", out var selectedConnectionString) && 
                        selectedConnectionString != null)
                    {
                        connectionString = selectedConnectionString.ToString();
                        _logger.LogInformation($"Seçilen veritabanı bağlantısı kullanılıyor: {_httpContextAccessor.HttpContext.Items["SelectedDatabaseName"]}");
                        _logger.LogInformation($"Yeni bağlantı dizesi: {connectionString}");
                    }
                }
                
                // ErpConnectionStringProvider'dan bağlantı dizesini kontrol et
                if (!string.IsNullOrEmpty(ErpConnectionStringProvider.CurrentConnectionString))
                {
                    connectionString = ErpConnectionStringProvider.CurrentConnectionString;
                    _logger.LogInformation($"ErpConnectionStringProvider'dan bağlantı dizesi kullanılıyor: {connectionString}");
                }
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Bağlantı dizesi bulunamadı!");
                }
                
                var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veritabanı bağlantısı oluşturulurken hata oluştu");
                throw;
            }
        }
    }
} 