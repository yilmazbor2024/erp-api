using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Dapper;

namespace ErpMobile.Api.Repositories.Invoice
{
    // InvoiceRepository sınıfının uzantı metodlarını içeren partial sınıf
    public partial class InvoiceRepository
    {
        /// <summary>
        /// Fatura başlığı uzantı bilgilerini tpInvoiceHeaderExtension tablosuna kaydeder
        /// </summary>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Aktif transaction</param>
        /// <param name="invoiceHeaderId">Fatura başlığı ID'si</param>
        /// <param name="extensionData">Uzantı verileri</param>
        /// <returns></returns>
        public async Task CreateInvoiceHeaderExtensionAsync(SqlConnection connection, SqlTransaction transaction, Guid invoiceHeaderId, dynamic extensionData)
        {
            try
            {
                _logger.LogInformation($"Fatura başlığı uzantısı kaydediliyor. InvoiceHeaderID: {invoiceHeaderId}");
                
                // Gelen verileri logla
                _logger.LogInformation($"Extension verileri: PaymentMeansCode={extensionData.PaymentMeansCode}, " +
                                      $"PaymentChannelCode={extensionData.PaymentChannelCode}, " +
                                      $"IsIndividual={extensionData.IsIndividual}, " +
                                      $"DocumentDate={extensionData.DocumentDate}");
                
                // SQL sorgusu
                var sql = @"
                INSERT INTO tpInvoiceHeaderExtension (
                    InvoiceHeaderID,
                    PaymentMeansCode,
                    PaymentChannelCode,
                    IsIndividual,
                    DocumentDate,
                    CreatedUserName,
                    CreatedDate,
                    LastUpdatedUserName,
                    LastUpdatedDate
                ) VALUES (
                    @InvoiceHeaderID,
                    @PaymentMeansCode,
                    @PaymentChannelCode,
                    @IsIndividual,
                    @DocumentDate,
                    @CreatedUserName,
                    @CreatedDate,
                    @LastUpdatedUserName,
                    @LastUpdatedDate
                )";

                // Parametreler
                // Foreign key kısıtlaması nedeniyle PaymentMeansCode ve PaymentChannelCode değerlerini boş string olarak ayarlıyoruz
                // Veritabanı örneğinde bu değerler boş string olarak gönderilmiş
                string paymentMeansCode = "";
                
                _logger.LogInformation($"PaymentMeansCode değeri boş string olarak ayarlandı. Orijinal değer: {extensionData.PaymentMeansCode}");
                
                var parameters = new
                {
                    InvoiceHeaderID = invoiceHeaderId,
                    PaymentMeansCode = paymentMeansCode, // Boş string olarak ayarlandı
                    PaymentChannelCode = "", // Boş string olarak ayarlandı
                    IsIndividual = extensionData.IsIndividual,
                    DocumentDate = extensionData.DocumentDate,
                    CreatedUserName = "System",
                    CreatedDate = DateTime.Now,
                    LastUpdatedUserName = "System",
                    LastUpdatedDate = DateTime.Now
                };
                
                _logger.LogInformation($"SQL sorgusu: {sql}");
                _logger.LogInformation($"SQL parametreleri: InvoiceHeaderID={invoiceHeaderId}, " +
                                      $"PaymentMeansCode={parameters.PaymentMeansCode}, " +
                                      $"PaymentChannelCode={parameters.PaymentChannelCode}, " +
                                      $"IsIndividual={parameters.IsIndividual}, " +
                                      $"DocumentDate={parameters.DocumentDate}");

                // SQL sorgusunu çalıştır
                await connection.ExecuteAsync(sql, parameters, transaction);
                
                _logger.LogInformation($"Fatura başlığı uzantısı başarıyla kaydedildi. InvoiceHeaderID: {invoiceHeaderId}, PaymentMeansCode: {parameters.PaymentMeansCode}, PaymentChannelCode: {parameters.PaymentChannelCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura başlığı uzantısı kaydedilirken hata oluştu. InvoiceHeaderID: {invoiceHeaderId}");
                _logger.LogError($"Hata detayları: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }
    }
}
