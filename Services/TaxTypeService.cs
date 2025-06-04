using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Models.Tax;
using ErpMobile.Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    public class TaxTypeService : ITaxTypeService
    {
        private readonly string _connectionString;
        private readonly ILogger<TaxTypeService> _logger;

        public TaxTypeService(IConfiguration configuration, ILogger<TaxTypeService> logger)
        {
            _connectionString = configuration.GetConnectionString("ErpConnection");
            _logger = logger;
        }

        public async Task<IEnumerable<TaxTypeModel>> GetAllTaxTypesAsync()
        {
            try
            {
                _logger.LogInformation("Başlangıç: GetAllTaxTypesAsync metodu çalıştırılıyor");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    _logger.LogInformation("Veritabanı bağlantısı açılıyor");
                    await connection.OpenAsync();
                    _logger.LogInformation("Veritabanı bağlantısı başarıyla açıldı");

                    var query = @"
                    SELECT
                        bsTaxType.TaxTypeCode,
                        bsTaxType.IsBlocked,
                        LangCode = 'tr',
                        TaxTypeDescription = RTRIM(LTRIM(ISNULL(bsTaxTypeDesc.TaxTypeDescription, SPACE(0))))
                    FROM bsTaxType WITH (NOLOCK)
                        LEFT OUTER JOIN bsTaxTypeDesc WITH (NOLOCK)
                            ON bsTaxTypeDesc.TaxTypeCode = bsTaxType.TaxTypeCode
                            AND bsTaxTypeDesc.LangCode = 'tr'
                    ORDER BY TaxTypeDescription";

                    _logger.LogInformation("SQL sorgusu çalıştırılıyor: {Query}", query);
                    var result = await connection.QueryAsync<TaxTypeModel>(query);
                    _logger.LogInformation("SQL sorgusu başarıyla çalıştırıldı, {Count} adet kayıt döndü", result?.Count() ?? 0);
                    
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Vergi tipleri alınırken hata oluştu");
                throw; // Hatayı yukarı fırlat, controller'da yakalanacak
            }
        }
    }
}
