using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Inventory;

namespace ErpMobile.Api.Repositories.Inventory
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ILogger<InventoryRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public InventoryRepository(
            ILogger<InventoryRepository> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ErpConnection");
        }

        public async Task<List<InventoryStockModel>> GetInventoryStockByBarcodeAsync(string barcode)
        {
            try
            {
                var result = new List<InventoryStockModel>();
                
                if (string.IsNullOrEmpty(barcode))
                {
                    return result;
                }
                
                _logger.LogInformation($"Barkod ile envanter/stok bilgisi aranıyor: {barcode}");

                // Stored procedure yerine doğrudan SQL sorgusu kullanalım
                _logger.LogInformation($"Barkod için SQL sorgusu çalıştırılıyor. Barkod: {barcode}");
                var query = @"
                SELECT ItemTypeCode          = cdItem.ItemTypeCode
                     , UsedBarcode           = prItemBarcode.Barcode
                     , ItemDescription       = ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = prItemVariant.ItemTypeCode AND cdItemDesc.ItemCode = prItemVariant.ItemCode AND cdItemDesc.LangCode = @LangCode), SPACE(0))
                     , ColorDescription      = ISNULL((SELECT ColorDescription FROM cdColorDesc WITH(NOLOCK) WHERE cdColorDesc.ColorCode = prItemVariant.ColorCode AND cdColorDesc.LangCode = @LangCode), SPACE(0))
                     , BinCode               = ISNULL(trInventoryItem.BinCode, SPACE(0))
                     , UnitOfMeasureCode     = prItemBarcode.UnitOfMeasureCode
                     , BarcodeTypeCode       = prItemBarcode.BarcodeTypeCode
                     , ColorCode             = prItemVariant.ColorCode
                     , ItemDim1Code          = prItemVariant.ItemDim1Code
                     , ItemDim2TypeCode      = prItemVariant.ItemDim2TypeCode
                     , ItemDim2Code          = prItemVariant.ItemDim2Code
                     , ItemDim3Code          = prItemVariant.ItemDim3Code
                     , Qty                   = ISNULL(trInventoryItem.Qty, 0)
                     , VariantIsBlocked      = CAST(ISNULL(prItemVariant.IsBlocked, 0) AS bit)
                     , IsNotExist            = CAST(0 AS bit)
                FROM prItemBarcode WITH(NOLOCK)
                    INNER JOIN prItemVariant WITH(NOLOCK)
                        ON prItemVariant.ItemTypeCode = prItemBarcode.ItemTypeCode
                        AND prItemVariant.ItemCode = prItemBarcode.ItemCode
                        AND prItemVariant.ColorCode = prItemBarcode.ColorCode
                        AND prItemVariant.ItemDim1Code = prItemBarcode.ItemDim1Code
                        AND prItemVariant.ItemDim2Code = prItemBarcode.ItemDim2Code
                        AND prItemVariant.ItemDim3Code = prItemBarcode.ItemDim3Code
                    INNER JOIN cdItem WITH(NOLOCK)
                        ON cdItem.ItemTypeCode = prItemVariant.ItemTypeCode
                        AND cdItem.ItemCode = prItemVariant.ItemCode
                    LEFT OUTER JOIN trInventoryItem WITH(NOLOCK)
                        ON trInventoryItem.ItemTypeCode = prItemVariant.ItemTypeCode
                        AND trInventoryItem.ItemCode = prItemVariant.ItemCode
                        AND trInventoryItem.ColorCode = prItemVariant.ColorCode
                        AND trInventoryItem.ItemDim1Code = prItemVariant.ItemDim1Code
                        AND trInventoryItem.ItemDim2Code = prItemVariant.ItemDim2Code
                        AND trInventoryItem.ItemDim3Code = prItemVariant.ItemDim3Code
                WHERE prItemBarcode.Barcode = @Barcode
                ";

                _logger.LogInformation($"Veritabanı bağlantısı açılıyor. ConnectionString: {_connectionString.Substring(0, Math.Min(20, _connectionString.Length))}...");
                
                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        _logger.LogInformation("Veritabanı bağlantısı başarıyla açıldı");

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Barcode", barcode);
                            command.Parameters.AddWithValue("@LangCode", "TR");
                            _logger.LogInformation($"SQL sorgusu çalıştırılıyor. Barkod: {barcode}");

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                _logger.LogInformation("SQL sorgusu başarıyla çalıştırıldı");
                                
                                int rowCount = 0;
                                while (await reader.ReadAsync())
                                {
                                    rowCount++;
                                    var inventoryStock = new InventoryStockModel
                                    {
                                        ItemTypeCode = reader["ItemTypeCode"]?.ToString(),
                                        UsedBarcode = reader["UsedBarcode"]?.ToString(),
                                        ItemDescription = reader["ItemDescription"]?.ToString(),
                                        ColorDescription = reader["ColorDescription"]?.ToString(),
                                        BinCode = reader["BinCode"]?.ToString(),
                                        UnitOfMeasureCode = reader["UnitOfMeasureCode"]?.ToString(),
                                        BarcodeTypeCode = reader["BarcodeTypeCode"]?.ToString(),
                                        ColorCode = reader["ColorCode"]?.ToString(),
                                        ItemDim1Code = reader["ItemDim1Code"]?.ToString(),
                                        ItemDim2TypeCode = reader["ItemDim2TypeCode"]?.ToString(),
                                        ItemDim2Code = reader["ItemDim2Code"]?.ToString(),
                                        ItemDim3Code = reader["ItemDim3Code"]?.ToString(),
                                        Qty = reader["Qty"] != DBNull.Value ? Convert.ToDecimal(reader["Qty"]) : 0,
                                        VariantIsBlocked = Convert.ToBoolean(reader["VariantIsBlocked"]),
                                        IsNotExist = Convert.ToBoolean(reader["IsNotExist"])
                                    };

                                    result.Add(inventoryStock);
                                }
                                
                                _logger.LogInformation($"Toplam {rowCount} adet stok kaydı bulundu");
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    _logger.LogError(sqlEx, $"SQL hatası oluştu: {sqlEx.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Veritabanı işlemi sırasında hata oluştu: {ex.Message}");
                    throw;
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Barkod ile envanter/stok bilgisi aranırken hata oluştu. Barkod: {barcode}");
                _logger.LogError($"Hata detayı: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner exception: {ex.InnerException.Message}");
                }
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                
                // Boş liste döndür
                return new List<InventoryStockModel>();
            }
        }
    }
}
