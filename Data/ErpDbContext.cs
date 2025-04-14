using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Data
{
    public class ErpDbContext
    {
        private readonly string _connectionString;
        private readonly ILogger<ErpDbContext> _logger;

        public ErpDbContext(string connectionString, ILogger<ErpDbContext> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<DataTable> ExecuteQueryAsync(string query, SqlParameter[] parameters)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

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
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

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
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

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
                var connection = new SqlConnection(_connectionString);
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