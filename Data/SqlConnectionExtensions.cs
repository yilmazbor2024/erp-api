using System;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace ErpMobile.Api.Data
{
    /// <summary>
    /// SqlConnection için extension metodları
    /// </summary>
    public static class SqlConnectionExtensions
    {
        /// <summary>
        /// SqlConnection nesnesinin bağlantı dizesini güncel bağlantı dizesi ile değiştirir
        /// </summary>
        public static void UpdateConnectionString(this SqlConnection connection)
        {
            if (connection == null)
                return;

            try
            {
                // Güncel bağlantı dizesini al
                string currentConnectionString = ErpConnectionStringProvider.CurrentConnectionString;
                
                if (string.IsNullOrEmpty(currentConnectionString))
                    return;
                
                // SqlConnection sınıfındaki _connectionString private field'ını reflection ile değiştir
                Type connectionType = typeof(SqlConnection);
                FieldInfo connectionStringField = connectionType.GetField("_connectionString", BindingFlags.NonPublic | BindingFlags.Instance);
                
                if (connectionStringField != null)
                {
                    connectionStringField.SetValue(connection, currentConnectionString);
                    Console.WriteLine("SqlConnection bağlantı dizesi güncellendi");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SqlConnection bağlantı dizesi güncellenirken hata: {ex.Message}");
            }
        }
    }
}
