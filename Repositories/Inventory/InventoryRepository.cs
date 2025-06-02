using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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

        public async Task<List<InventoryStockModel>> GetInventoryStockMultiPurposeAsync(string barcode = null, string productCode = null, string productDescription = null, string colorCode = null, string itemDim1Code = null, string warehouseCode = null, bool showOnlyPositiveStock = false)
{
    try
    {
var result = new List<InventoryStockModel>();

// Renkli konsol logları ekleyelim
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\n==================== REPOSITORY STOK SORGULAMA PARAMETRELERİ ====================");
Console.WriteLine($"Barkod: {barcode ?? "null"}");
Console.WriteLine($"Ürün Kodu: {productCode ?? "null"}");
Console.WriteLine($"Ürün Açıklaması: {productDescription ?? "null"}");
Console.WriteLine($"Renk Kodu: {colorCode ?? "null"}");
Console.WriteLine($"Beden Kodu: {itemDim1Code ?? "null"}");
Console.WriteLine($"Depo Kodu: {warehouseCode ?? "null"}");
Console.WriteLine($"Sadece Pozitif Stok: {showOnlyPositiveStock}");
Console.WriteLine("===================================================================\n");
Console.ResetColor();

_logger.LogInformation($"Çok amaçlı envanter/stok sorgusu yapılıyor. Barkod: {barcode}, Ürün Kodu: {productCode}, Ürün Açıklaması: {productDescription}, Renk Kodu: {colorCode}, Beden Kodu: {itemDim1Code}, Depo Kodu: {warehouseCode}, Sadece Pozitif Stok: {showOnlyPositiveStock}");

        var query = new StringBuilder();
        query.AppendLine(@"
            WITH AllBarcodes AS (
                SELECT
                    prItemBarcode.ItemTypeCode,
                    prItemBarcode.ItemCode,
                    prItemBarcode.ColorCode,
                    prItemBarcode.ItemDim1Code,
                    UsedBarcode = prItemBarcode.Barcode,
                    SortOrder = 1
                FROM prItemBarcode WITH (NOLOCK)
                INNER JOIN cdItem WITH (NOLOCK)
                    ON prItemBarcode.ItemTypeCode = cdItem.ItemTypeCode
                    AND prItemBarcode.ItemCode = cdItem.ItemCode
                WHERE cdItem.IsBlocked = 0

                UNION ALL

                SELECT
                    prItemBatchBarcode.ItemTypeCode,
                    prItemBatchBarcode.ItemCode,
                    prItemBatchBarcode.ColorCode,
                    prItemBatchBarcode.ItemDim1Code,
                    UsedBarcode = prItemBatchBarcode.BatchBarcode,
                    SortOrder = 2
                FROM prItemBatchBarcode WITH (NOLOCK)
                INNER JOIN cdItem WITH (NOLOCK)
                    ON prItemBatchBarcode.ItemTypeCode = cdItem.ItemTypeCode
                    AND prItemBatchBarcode.ItemCode = cdItem.ItemCode
                WHERE cdItem.IsBlocked = 0

                UNION ALL

                SELECT
                    cdRoll.ItemTypeCode,
                    cdRoll.ItemCode,
                    cdRoll.ColorCode,
                    cdRoll.ItemDim1Code,
                    UsedBarcode = cdRoll.RollNumber,
                    SortOrder = 3
                FROM cdRoll WITH (NOLOCK)
                INNER JOIN cdItem WITH (NOLOCK)
                    ON cdRoll.ItemCode = cdItem.ItemCode
                    AND cdRoll.ItemTypeCode = cdItem.ItemTypeCode
                WHERE cdItem.IsBlocked = 0
            ),
            GroupedBarcodes AS (
                SELECT
                    ItemTypeCode,
                    ItemCode,
                    ColorCode,
                    ItemDim1Code,
                    AllUsedBarcodes = STRING_AGG(UsedBarcode, ', ') WITHIN GROUP (ORDER BY SortOrder, UsedBarcode)
                FROM AllBarcodes
                GROUP BY ItemTypeCode, ItemCode, ColorCode, ItemDim1Code
            ),
            FilteredWarehouses AS (
                SELECT DISTINCT
                    w.WarehouseCode,
                    w.OfficeCode
                FROM cdWarehouse AS w WITH (NOLOCK)
                INNER JOIN cdCurrAcc AS ca WITH (NOLOCK)
                    ON ca.CurrAccTypeCode = w.CurrAccTypeCode
                    AND ca.CurrAccCode = w.CurrAccCode
                WHERE
                    (CASE WHEN 0 = 1 THEN ca.CustomerTypeCode ELSE 1 END = 2
                    OR CASE WHEN 0 = 1 THEN ca.CustomerTypeCode ELSE 2 END = 1
                    OR ((w.WarehouseOwnerCode IN ( 1, 2, 3 ) OR ca.CustomerTypeCode = 0)
                        AND w.WarehouseOwnerCode <> 4))
            ),
            InventoryRaw AS (
                SELECT
                    trStock.CompanyCode,
                    trStock.OfficeCode,
                    trStock.StoreCode,
                    trStock.WarehouseCode,
                    ProductCode = trStock.ItemCode,
                    trStock.ColorCode,
                    trStock.ItemDim1Code,
                    InventoryQty1 = SUM(TRY_CAST(trStock.In_Qty1 AS FLOAT) - TRY_CAST(trStock.Out_Qty1 AS FLOAT)),
                    TransferNotApprovedQty1 = 0
                FROM trStock WITH (NOLOCK)
                WHERE
                    trStock.ItemTypeCode = 1
                    AND trStock.OperationDate <= CAST(GETDATE() AS DATETIME)
                    AND trStock.WarehouseCode IN (SELECT WarehouseCode FROM FilteredWarehouses)
                GROUP BY
                    trStock.CompanyCode,
                    trStock.OfficeCode,
                    trStock.StoreCode,
                    trStock.WarehouseCode,
                    trStock.ItemCode,
                    trStock.ColorCode,
                    trStock.ItemDim1Code

                UNION ALL

                SELECT
                    ITNA.CompanyCode,
                    ITNA.OfficeCode,
                    ITNA.StoreCode,
                    ITNA.WarehouseCode,
                    ProductCode = ITNA.ItemCode,
                    ITNA.ColorCode,
                    ITNA.ItemDim1Code,
                    InventoryQty1 = 0,
                    TransferNotApprovedQty1 = SUM(TRY_CAST(ITNA.TransferNotApprovedQty1 AS FLOAT))
                FROM ItemTransferNotApprovedByDate(CAST(GETDATE() AS DATETIME)) AS ITNA -- GETDATE() kullanıldı
                WHERE
                    ITNA.ItemTypeCode = 1
                    AND ITNA.WarehouseCode IN (SELECT WarehouseCode FROM FilteredWarehouses)
                GROUP BY
                    ITNA.CompanyCode,
                    ITNA.OfficeCode,
                    ITNA.StoreCode,
                    ITNA.WarehouseCode,
                    ITNA.ItemCode,
                    ITNA.ColorCode,
                    ITNA.ItemDim1Code

                UNION ALL

                SELECT
                    tf.CompanyCode,
                    Stores.OfficeCode,
                    StoreCode = Stores.CurrAccCode,
                    WarehouseCode = tf.WarehouseCode,
                    ProductCode = ISNULL(tsl.ItemCode, til.ItemCode),
                    ColorCode = ISNULL(tsl.ColorCode, til.ColorCode),
                    ItemDim1Code = ISNULL(tsl.ItemDim1Code, til.ItemDim1Code),
                    InventoryQty1 = 0,
                    TransferNotApprovedQty1 = SUM(ISNULL(TRY_CAST(tsl.Qty1 AS FLOAT), TRY_CAST(til.Qty1 AS FLOAT)))
                FROM trForthcomingItems AS tf WITH (NOLOCK)
                INNER JOIN cdCurrAcc AS Stores WITH (NOLOCK)
                    ON Stores.CurrAccTypeCode = 5
                    AND Stores.CurrAccCode = tf.StoreCode
                    AND Stores.CurrAccCode <> SPACE(0)
                LEFT JOIN trInvoiceLine AS til WITH (NOLOCK)
                    ON til.InvoiceHeaderID = tf.HeaderID
                    AND til.ItemTypeCode = 1
                    AND tf.ProcessFlowCode = 7
                LEFT JOIN trShipmentLine AS tsl WITH (NOLOCK)
                    ON tsl.ShipmentHeaderID = tf.HeaderID
                    AND tsl.ItemTypeCode = 1
                    AND tf.ProcessFlowCode = 6
                WHERE
                    tf.IsPosted = 0
                    AND (til.InvoiceHeaderID IS NOT NULL OR tsl.ShipmentHeaderID IS NOT NULL)
                    AND tf.TransactionDate <= CAST(GETDATE() AS DATETIME) -- GETDATE() kullanıldı
                    AND 0 = 1 -- Bu koşulun ne anlama geldiğini kontrol edin ve gerekiyorsa düzeltin.
                    AND tf.WarehouseCode IN (SELECT WarehouseCode FROM FilteredWarehouses)
                GROUP BY
                    tf.CompanyCode,
                    Stores.OfficeCode,
                    Stores.CurrAccCode,
                    tf.WarehouseCode,
                    ISNULL(tsl.ItemCode, til.ItemCode),
                    ISNULL(tsl.ColorCode, til.ColorCode),
                    ISNULL(tsl.ItemDim1Code, til.ItemDim1Code)

                UNION ALL

                SELECT
                    trf.CompanyCode,
                    Stores.OfficeCode,
                    StoreCode = CASE WHEN Stores.CurrAccTypeCode = 5 THEN Stores.CurrAccCode ELSE SPACE(0) END,
                    WarehouseCode = trf.WarehouseCode,
                    ProductCode = ISNULL(tsl.ItemCode, til.ItemCode),
                    ColorCode = ISNULL(tsl.ColorCode, til.ColorCode),
                    ItemDim1Code = ISNULL(tsl.ItemDim1Code, til.ItemDim1Code),
                    InventoryQty1 = 0,
                    TransferNotApprovedQty1 = SUM(ISNULL(TRY_CAST(tsl.Qty1 AS FLOAT), TRY_CAST(til.Qty1 AS FLOAT)))
                FROM trReturnedForthcomingItems AS trf WITH (NOLOCK)
                INNER JOIN cdWarehouse AS Stores WITH (NOLOCK)
                    ON Stores.WarehouseCode = trf.WarehouseCode
                LEFT JOIN trInvoiceLine AS til WITH (NOLOCK)
                    ON til.InvoiceHeaderID = trf.HeaderID
                    AND til.ItemTypeCode = 1
                    AND trf.ProcessFlowCode = 7
                LEFT JOIN trShipmentLine AS tsl WITH (NOLOCK)
                    ON tsl.ShipmentHeaderID = trf.HeaderID
                    AND tsl.ItemTypeCode = 1
                    AND trf.ProcessFlowCode = 6
                WHERE
                    trf.IsPosted = 0
                    AND (til.InvoiceHeaderID IS NOT NULL OR tsl.ShipmentHeaderID IS NOT NULL)
                    AND trf.TransactionDate <= CAST(GETDATE() AS DATETIME) -- GETDATE() kullanıldı
                    AND 0 = 1 -- Bu koşulun ne anlama geldiğini kontrol edin ve gerekiyorsa düzeltin.
                    AND trf.WarehouseCode IN (SELECT WarehouseCode FROM FilteredWarehouses)
                GROUP BY
                    trf.CompanyCode,
                    Stores.OfficeCode,
                    Stores.CurrAccTypeCode,
                    Stores.CurrAccCode,
                    trf.WarehouseCode,
                    ISNULL(tsl.ItemCode, til.ItemCode),
                    ISNULL(tsl.ColorCode, til.ColorCode),
                    ISNULL(tsl.ItemDim1Code, til.ItemDim1Code)
            ),
            AggregatedInventory AS (
                SELECT
                    CompanyCode = 1,
                    OfficeCode = IR.OfficeCode,
                    WarehouseCode = IR.WarehouseCode,
                    ProductCode = IR.ProductCode,
                    ColorCode = IR.ColorCode,
                    ItemDim1Code = IR.ItemDim1Code,
                    InventoryQty1 = SUM(IR.InventoryQty1),
                    TransferNotApprovedQty1 = SUM(IR.TransferNotApprovedQty1)
                FROM InventoryRaw AS IR
                WHERE TRY_CAST(IR.CompanyCode AS INT) = 1
                GROUP BY
                    IR.OfficeCode,
                    IR.WarehouseCode,
                    IR.ProductCode,
                    IR.ColorCode,
                    IR.ItemDim1Code
            )
            SELECT
                ItemTypeCode          = piv.ItemTypeCode, -- Modelde bu alanı kullanın
                ItemCode              = piv.ItemCode,
                UsedBarcode           = ISNULL(GB.AllUsedBarcodes, SPACE(0)), -- Tüm barkodları getir
                ItemDescription       = ISNULL(cdItemDesc.ItemDescription, SPACE(0)),
                ColorDescription      = ISNULL(cdColorDesc.ColorDescription, SPACE(0)),
                BinCode               = SPACE(0), -- Boş bırakıldı, eğer bir bin kodu tablonuz varsa birleştirin
                UnitOfMeasureCode     = cdItem.UnitOfMeasureCode1,
                BarcodeTypeCode       = SPACE(0), -- Boş bırakıldı
                ColorCode             = piv.ColorCode,
                ItemDim1Code          = piv.ItemDim1Code,
                ItemDim2TypeCode      = SPACE(0), -- Boş bırakıldı
                ItemDim2Code          = SPACE(0), -- Boş bırakıldı
                ItemDim3Code          = SPACE(0), -- Boş bırakıldı
                Qty                   = ISNULL(AI.InventoryQty1 + AI.TransferNotApprovedQty1, 0),
                WarehouseCode         = ISNULL(AI.WarehouseCode, SPACE(0)),
                WarehouseName         = ISNULL(cdWareHouseDesc.WareHouseDescription, SPACE(0)),
                VariantIsBlocked      = ISNULL(cdItem.IsBlocked, 0), -- cdItem.IsBlocked'ı kullanabiliriz
                IsNotExist            = 0 -- Bu alanın mantığını sorguya dahil etmek için bir koşul eklenmeli
            FROM
                prItemVariant AS piv WITH (NOLOCK)
            INNER JOIN
                cdItem WITH (NOLOCK) ON piv.ItemTypeCode = cdItem.ItemTypeCode
                                    AND piv.ItemCode = cdItem.ItemCode
            LEFT JOIN
                cdItemDesc WITH (NOLOCK) ON piv.ItemTypeCode = cdItemDesc.ItemTypeCode
                                        AND piv.ItemCode = cdItemDesc.ItemCode
                                        AND cdItemDesc.LangCode = N'TR'
            LEFT JOIN
                cdColorDesc WITH (NOLOCK) ON piv.ColorCode = cdColorDesc.ColorCode
                                         AND cdColorDesc.LangCode = N'TR'
            LEFT JOIN
                AggregatedInventory AS AI ON piv.ItemCode = AI.ProductCode
                                          AND piv.ColorCode = AI.ColorCode
                                          AND piv.ItemDim1Code = AI.ItemDim1Code
            LEFT JOIN
                GroupedBarcodes AS GB ON piv.ItemTypeCode = GB.ItemTypeCode
                                      AND piv.ItemCode = GB.ItemCode
                                      AND piv.ColorCode = GB.ColorCode
                                      AND piv.ItemDim1Code = GB.ItemDim1Code
            LEFT JOIN
                cdOfficeDesc WITH (NOLOCK) ON AI.OfficeCode = cdOfficeDesc.OfficeCode
                                          AND cdOfficeDesc.LangCode = N'TR'
            LEFT JOIN
                cdWareHouseDesc WITH (NOLOCK) ON AI.WarehouseCode = cdWareHouseDesc.WarehouseCode
                                              AND cdWareHouseDesc.LangCode = N'TR'
            WHERE
                1=1 -- Dinamik filtreler buraya eklenecek
        ");

        // Dinamik filtreleri ekle
        if (!string.IsNullOrEmpty(barcode))
        {
            // Eğer tam eşleşme aranıyorsa:
            // query.Append(" AND GB.AllUsedBarcodes = @Barcode");
            // Eğer barkod listede geçiyorsa (STRING_SPLIT ile daha doğru):
            query.Append(" AND EXISTS (SELECT 1 FROM STRING_SPLIT(ISNULL(GB.AllUsedBarcodes, ''), ',') WHERE TRIM(value) = @Barcode)");
            // Veya LIKE ile esnek arama:
            // query.Append(" AND ISNULL(GB.AllUsedBarcodes, '') LIKE '%' + @Barcode + '%'");
        }

        if (!string.IsNullOrEmpty(productCode))
        {
            query.Append(" AND piv.ItemCode LIKE '%' + @ProductCode + '%'");
        }

        if (!string.IsNullOrEmpty(productDescription))
        {
            query.Append(" AND id.ItemDescription LIKE '%' + @ProductDescription + '%'");
        }

        if (!string.IsNullOrEmpty(colorCode))
        {
            query.Append(" AND piv.ColorCode = @ColorCode");
        }

        if (!string.IsNullOrEmpty(itemDim1Code))
        {
            query.Append(" AND piv.ItemDim1Code = @ItemDim1Code");
        }

        if (!string.IsNullOrEmpty(warehouseCode))
        {
            query.Append(" AND AI.WarehouseCode = @WarehouseCode"); // AI.WarehouseCode kullanıldı
        }

        if (showOnlyPositiveStock)
        {
            query.Append(" AND ISNULL(AI.InventoryQty1 + AI.TransferNotApprovedQty1, 0) > 0");
        }

        // Eğer hiçbir barkod filtresi uygulanmadıysa ve barkod modelde gösteriliyorsa, null değerleri filtrelemek isteyebilirsiniz.
        // Örneğin, sadece barkodu olan ürünleri getirmek için:
        // query.Append(" AND GB.AllUsedBarcodes IS NOT NULL AND GB.AllUsedBarcodes <> ''");


        query.AppendLine(" ORDER BY piv.ItemCode, piv.ColorCode, piv.ItemDim1Code");

        // SQL sorgusu ve parametreleri renkli olarak yazdıran loglar
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n==================== SQL SORGUSU ====================");
        Console.WriteLine(query.ToString());
        Console.WriteLine("===================================================================\n");
        Console.ResetColor();

        _logger.LogInformation("Veritabanı bağlantısı açılıyor.");

        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                _logger.LogInformation("Veritabanı bağlantısı başarıyla açıldı");

                _logger.LogInformation("SQL sorgusu çalıştırılıyor.");
                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    if (!string.IsNullOrEmpty(barcode))
                    {
                        command.Parameters.AddWithValue("@Barcode", barcode);
                    }

                    if (!string.IsNullOrEmpty(productCode))
                    {
                        command.Parameters.AddWithValue("@ProductCode", productCode);
                    }

                    if (!string.IsNullOrEmpty(productDescription))
                    {
                        command.Parameters.AddWithValue("@ProductDescription", productDescription);
                    }

                    if (!string.IsNullOrEmpty(colorCode))
                    {
                        command.Parameters.AddWithValue("@ColorCode", colorCode);
                    }

                    if (!string.IsNullOrEmpty(itemDim1Code))
                    {
                        command.Parameters.AddWithValue("@ItemDim1Code", itemDim1Code);
                    }

                    if (!string.IsNullOrEmpty(warehouseCode))
                    {
                        command.Parameters.AddWithValue("@WarehouseCode", warehouseCode);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        int rowCount = 0;
                        while (await reader.ReadAsync())
                        {
                            rowCount++;
                            var inventoryStock = new InventoryStockModel
                            {
                                ItemTypeCode = reader["ItemTypeCode"]?.ToString(),
                                ItemCode = reader["ItemCode"]?.ToString(),
                                UsedBarcode = reader["UsedBarcode"]?.ToString(),
                                ItemDescription = reader["ItemDescription"]?.ToString(),
                                ColorDescription = reader["ColorDescription"]?.ToString(),
                                BinCode = reader["BinCode"]?.ToString(), // Boş bırakıldı veya veritabanından al
                                UnitOfMeasureCode = reader["UnitOfMeasureCode"]?.ToString(),
                                BarcodeTypeCode = reader["BarcodeTypeCode"]?.ToString(), // Boş bırakıldı
                                ColorCode = reader["ColorCode"]?.ToString(),
                                ItemDim1Code = reader["ItemDim1Code"]?.ToString(),
                                ItemDim2TypeCode = reader["ItemDim2TypeCode"]?.ToString(), // Boş bırakıldı
                                ItemDim2Code = reader["ItemDim2Code"]?.ToString(), // Boş bırakıldı
                                ItemDim3Code = reader["ItemDim3Code"]?.ToString(), // Boş bırakıldı
                                Qty = reader["Qty"] != DBNull.Value ? Convert.ToDecimal(reader["Qty"]) : 0,
                                WarehouseCode = reader["WarehouseCode"]?.ToString(),
                                WarehouseName = reader["WarehouseName"]?.ToString(),
                                VariantIsBlocked = reader["VariantIsBlocked"] != DBNull.Value ? Convert.ToBoolean(reader["VariantIsBlocked"]) : false,
                                IsNotExist = reader["IsNotExist"] != DBNull.Value ? Convert.ToBoolean(reader["IsNotExist"]) : false
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
        _logger.LogError(ex, $"Çok amaçlı envanter/stok sorgusu yapılırken hata oluştu.");
        _logger.LogError($"Hata detayı: {ex.Message}");
        if (ex.InnerException != null)
        {
            _logger.LogError($"Inner exception: {ex.InnerException.Message}");
        }
        _logger.LogError($"Stack trace: {ex.StackTrace}");

        return new List<InventoryStockModel>();
    }
    }
   }
 }


 