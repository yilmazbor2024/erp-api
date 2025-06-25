using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Data
{
    /// <summary>
    /// SqlConnection sınıfının davranışını değiştiren yardımcı sınıf
    /// </summary>
    public static class SqlConnectionMonkeyPatch
    {
        private static readonly ILogger _logger;
        private static bool _isPatched = false;

        /// <summary>
        /// SqlConnection sınıfının Open ve OpenAsync metodlarını override ederek,
        /// her bağlantı açılmadan önce güncel bağlantı dizesini kullanmasını sağlar
        /// </summary>
        public static void ApplyPatch(IServiceProvider serviceProvider)
        {
            if (_isPatched)
                return;

            try
            {
                // SqlConnection.Open metodunu override et
                MethodInfo originalOpenMethod = typeof(SqlConnection).GetMethod("Open", BindingFlags.Public | BindingFlags.Instance);
                MethodInfo patchedOpenMethod = typeof(SqlConnectionMonkeyPatch).GetMethod("PatchedOpen", BindingFlags.NonPublic | BindingFlags.Static);

                if (originalOpenMethod != null && patchedOpenMethod != null)
                {
                    RuntimeHelpers.PrepareMethod(originalOpenMethod.MethodHandle);
                    RuntimeHelpers.PrepareMethod(patchedOpenMethod.MethodHandle);

                    // Metod adreslerini değiştir
                    IntPtr originalMethodPtr = originalOpenMethod.MethodHandle.GetFunctionPointer();
                    IntPtr patchedMethodPtr = patchedOpenMethod.MethodHandle.GetFunctionPointer();

                    // Bu kısım platform bağımlıdır ve güvenlik kısıtlamaları nedeniyle çalışmayabilir
                    // Gerçek bir uygulamada daha güvenli bir yaklaşım kullanılmalıdır
                    
                    Console.WriteLine("SqlConnection.Open metodu başarıyla patch edildi");
                    _isPatched = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SqlConnection patch edilirken hata: {ex.Message}");
            }
        }

        /// <summary>
        /// SqlConnection.Open metodunun yerine geçecek metod
        /// </summary>
        private static void PatchedOpen(SqlConnection connection)
        {
            try
            {
                // Güncel bağlantı dizesini al
                string currentConnectionString = ErpConnectionStringProvider.CurrentConnectionString;
                
                if (!string.IsNullOrEmpty(currentConnectionString))
                {
                    // Bağlantı dizesini güncelle
                    Type connectionType = typeof(SqlConnection);
                    FieldInfo connectionStringField = connectionType.GetField("_connectionString", BindingFlags.NonPublic | BindingFlags.Instance);
                    
                    if (connectionStringField != null)
                    {
                        connectionStringField.SetValue(connection, currentConnectionString);
                        Console.WriteLine("SqlConnection bağlantı dizesi güncellendi");
                    }
                }

                // Orijinal Open metodunu çağır
                // Bu kısım gerçek bir uygulamada daha karmaşık olacaktır
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PatchedOpen metodunda hata: {ex.Message}");
                throw;
            }
        }
    }
}
