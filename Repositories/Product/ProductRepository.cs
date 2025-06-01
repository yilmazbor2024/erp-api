using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Product;

namespace ErpMobile.Api.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProductRepository(
            ILogger<ProductRepository> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ErpConnection");
        }

        public async Task<List<ProductVariantModel>> GetProductVariantsByBarcodeAsync(string barcode)
        {
            try
            {
                var result = new List<ProductVariantModel>();

                if (string.IsNullOrEmpty(barcode))
                {
                    return result;
                }

                // SQL sorgusu - barkod ile ürün varyantlarını arama
                var query = @"
                SELECT ProductCode              = prItemVariant.ItemCode
                     , ProductDescription       = ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = prItemVariant.ItemTypeCode AND cdItemDesc.ItemCode = prItemVariant.ItemCode AND cdItemDesc.LangCode = N'TR'), SPACE(0))
                     , ColorCode                = prItemVariant.ColorCode
                     , ColorDescription         = ISNULL((SELECT ColorDescription FROM cdColorDesc WITH(NOLOCK) WHERE cdColorDesc.ColorCode = prItemVariant.ColorCode AND cdColorDesc.LangCode = N'TR'), SPACE(0))
                     , ManufacturerColorCode    = ISNULL(prItemColorAttributes.ManufacturerColorCode,SPACE(0))
                     , ItemDim1Code             = prItemVariant.ItemDim1Code
                     , ItemDim2Code             = prItemVariant.ItemDim2Code
                     , ItemDim3Code             = prItemVariant.ItemDim3Code
                     , BarcodeTypeCode          = ISNULL(prItemBarcode.BarcodeTypeCode , SPACE(0))
                     , Barcode                  = ISNULL(prItemBarcode.Barcode , SPACE(0))
                     , NotHaveBarcodes          = CAST((CASE ISNULL(prItemBarcode.Barcode, SPACE(0)) WHEN SPACE(0) THEN 1 ELSE 0 END) AS bit)
                     , prItemBarcode.Qty
                     , ProductTypeCode          = cdItem.ProductTypeCode
                     , ProductTypeDescription   = ISNULL((SELECT ProductTypeDescription FROM bsProductTypeDesc WITH(NOLOCK) WHERE bsProductTypeDesc.ProductTypeCode = cdItem.ProductTypeCode AND bsProductTypeDesc.LangCode = N'TR'), SPACE(0))
                     , UnitOfMeasureCode1       = cdItem.UnitOfMeasureCode1
                     , UnitOfMeasureCode2       = cdItem.UnitOfMeasureCode2
                     , ProductHierarchyID       = cdItem.ProductHierarchyID
                     , ProductHierarchyLevel01
                     , ProductHierarchyLevel02
                     , ProductHierarchyLevel03
                     , ProductHierarchyLevel04
                     , ProductHierarchyLevel05
                     , ProductHierarchyLevel06
                     , ProductCollectionGrCode  = cdItem.ProductCollectionGrCode
                     , SeasonCode , SeasonDescription
                     , CollectionCode , CollectionDescription
                     , StoryBoardCode , StoryBoardDescription
                     , IsBlocked                = cdItem.IsBlocked
                     , SalesPrice1              = 0 -- Varsayılan fiyat
                     , VatRate                  = 10 -- Varsayılan KDV oranı
                FROM prItemVariant WITH(NOLOCK)
                        LEFT OUTER JOIN (SELECT prItemBarcode.*, BarcodeCount = (SELECT COUNT(Barcode) FROM prItemBarcode A 
                                                                                        WHERE A.ItemTypeCode        = prItemBarcode.ItemTypeCode
                                                                                            AND A.ItemCode          = prItemBarcode.ItemCode
                                                                                            AND A.ColorCode         = prItemBarcode.ColorCode
                                                                                            AND A.ItemDim1Code      = prItemBarcode.ItemDim1Code
                                                                                            AND A.ItemDim2Code      = prItemBarcode.ItemDim2Code
                                                                                            AND A.ItemDim3Code      = prItemBarcode.ItemDim3Code
                                                                                            AND A.BarcodeTypeCode   = prItemBarcode.BarcodeTypeCode
                                                                                            AND 0 = 1)
                                                FROM prItemBarcode WITH(NOLOCK)) AS prItemBarcode
                            ON prItemBarcode.ItemTypeCode   = prItemVariant.ItemTypeCode
                            AND prItemBarcode.ItemCode      = prItemVariant.ItemCode
                            AND prItemBarcode.ColorCode     = prItemVariant.ColorCode
                            AND prItemBarcode.ItemDim1Code  = prItemVariant.ItemDim1Code
                            AND prItemBarcode.ItemDim2Code  = prItemVariant.ItemDim2Code
                            AND prItemBarcode.ItemDim3Code  = prItemVariant.ItemDim3Code
                        INNER JOIN cdItem WITH(NOLOCK)
                            ON cdItem.ItemTypeCode          = prItemVariant.ItemTypeCode
                            AND cdItem.ItemCode             = prItemVariant.ItemCode
                        INNER JOIN ProductHierarchy(N'TR')
                            ON ProductHierarchy.ProductHierarchyID = cdItem.ProductHierarchyID
                        INNER JOIN ProductCollection(N'TR')
                            ON ProductCollection.ProductCollectionGrCode = cdItem.ProductCollectionGrCode
                        LEFT OUTER JOIN ProductAttributesFilter
                            ON ProductAttributesFilter.ItemTypeCode      = cdItem.ItemTypeCode
                            AND ProductAttributesFilter.ItemCode        = cdItem.ItemCode
                        LEFT OUTER JOIN prItemColorAttributes WITH(NOLOCK)
                            ON prItemColorAttributes.ItemTypeCode = prItemVariant.ItemTypeCode
                            AND prItemColorAttributes.ItemCode = prItemVariant.ItemCode
                            AND prItemColorAttributes.ColorCode = prItemVariant.ColorCode
                WHERE prItemVariant.ItemTypeCode = 1    
                AND (CASE WHEN 0 = 1 THEN ISNULL(prItemBarcode.BarcodeCount,SPACE(0)) ELSE 2 END > 1)
                AND prItemBarcode.Barcode = @Barcode";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Barcode", barcode);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var productVariant = new ProductVariantModel
                                {
                                    ProductCode = reader["ProductCode"]?.ToString(),
                                    ProductDescription = reader["ProductDescription"]?.ToString(),
                                    ColorCode = reader["ColorCode"]?.ToString(),
                                    ColorDescription = reader["ColorDescription"]?.ToString(),
                                    ManufacturerColorCode = reader["ManufacturerColorCode"]?.ToString(),
                                    ItemDim1Code = reader["ItemDim1Code"]?.ToString(),
                                    ItemDim2Code = reader["ItemDim2Code"]?.ToString(),
                                    ItemDim3Code = reader["ItemDim3Code"]?.ToString(),
                                    BarcodeTypeCode = reader["BarcodeTypeCode"]?.ToString(),
                                    Barcode = reader["Barcode"]?.ToString(),
                                    NotHaveBarcodes = Convert.ToBoolean(reader["NotHaveBarcodes"]),
                                    Qty = reader["Qty"] != DBNull.Value ? Convert.ToDecimal(reader["Qty"]) : null,
                                    ProductTypeCode = reader["ProductTypeCode"]?.ToString(),
                                    ProductTypeDescription = reader["ProductTypeDescription"]?.ToString(),
                                    UnitOfMeasureCode1 = reader["UnitOfMeasureCode1"]?.ToString(),
                                    UnitOfMeasureCode2 = reader["UnitOfMeasureCode2"]?.ToString(),
                                    ProductHierarchyID = reader["ProductHierarchyID"]?.ToString(),
                                    ProductHierarchyLevel01 = reader["ProductHierarchyLevel01"]?.ToString(),
                                    ProductHierarchyLevel02 = reader["ProductHierarchyLevel02"]?.ToString(),
                                    ProductHierarchyLevel03 = reader["ProductHierarchyLevel03"]?.ToString(),
                                    ProductHierarchyLevel04 = reader["ProductHierarchyLevel04"]?.ToString(),
                                    ProductHierarchyLevel05 = reader["ProductHierarchyLevel05"]?.ToString(),
                                    ProductHierarchyLevel06 = reader["ProductHierarchyLevel06"]?.ToString(),
                                    ProductCollectionGrCode = reader["ProductCollectionGrCode"]?.ToString(),
                                    SeasonCode = reader["SeasonCode"]?.ToString(),
                                    SeasonDescription = reader["SeasonDescription"]?.ToString(),
                                    CollectionCode = reader["CollectionCode"]?.ToString(),
                                    CollectionDescription = reader["CollectionDescription"]?.ToString(),
                                    StoryBoardCode = reader["StoryBoardCode"]?.ToString(),
                                    StoryBoardDescription = reader["StoryBoardDescription"]?.ToString(),
                                    IsBlocked = Convert.ToBoolean(reader["IsBlocked"]),
                                    SalesPrice1 = reader["SalesPrice1"] != DBNull.Value ? Convert.ToDecimal(reader["SalesPrice1"]) : 0,
                                    VatRate = reader["VatRate"] != DBNull.Value ? Convert.ToDecimal(reader["VatRate"]) : null
                                };

                                result.Add(productVariant);
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Barkod ile ürün varyantları aranırken hata oluştu. Barkod: {Barcode}", barcode);
                throw;
            }
        }
        
        public async Task<List<ProductVariantModel>> GetProductVariantsByProductCodeOrDescriptionAsync(string searchText)
        {
            try
            {
                var result = new List<ProductVariantModel>();

                if (string.IsNullOrEmpty(searchText))
                {
                    return result;
                }

                // SQL sorgusu - ürün kodu veya açıklaması ile ürün varyantlarını arama
                var query = @"
                SELECT ProductCode              = prItemVariant.ItemCode
                     , ProductDescription       = ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = prItemVariant.ItemTypeCode AND cdItemDesc.ItemCode = prItemVariant.ItemCode AND cdItemDesc.LangCode = N'TR'), SPACE(0))
                     , ColorCode                = prItemVariant.ColorCode
                     , ColorDescription         = ISNULL((SELECT ColorDescription FROM cdColorDesc WITH(NOLOCK) WHERE cdColorDesc.ColorCode = prItemVariant.ColorCode AND cdColorDesc.LangCode = N'TR'), SPACE(0))
                     , ManufacturerColorCode    = ISNULL(prItemColorAttributes.ManufacturerColorCode,SPACE(0))
                     , ItemDim1Code             = prItemVariant.ItemDim1Code
                     , ItemDim2Code             = prItemVariant.ItemDim2Code
                     , ItemDim3Code             = prItemVariant.ItemDim3Code
                     , BarcodeTypeCode          = ISNULL(prItemBarcode.BarcodeTypeCode , SPACE(0))
                     , Barcode                  = ISNULL(prItemBarcode.Barcode , SPACE(0))
                     , NotHaveBarcodes          = CAST((CASE ISNULL(prItemBarcode.Barcode, SPACE(0)) WHEN SPACE(0) THEN 1 ELSE 0 END) AS bit)
                     , prItemBarcode.Qty
                     , ProductTypeCode          = cdItem.ProductTypeCode
                     , ProductTypeDescription   = ISNULL((SELECT ProductTypeDescription FROM bsProductTypeDesc WITH(NOLOCK) WHERE bsProductTypeDesc.ProductTypeCode = cdItem.ProductTypeCode AND bsProductTypeDesc.LangCode = N'TR'), SPACE(0))
                     , UnitOfMeasureCode1       = cdItem.UnitOfMeasureCode1
                     , UnitOfMeasureCode2       = cdItem.UnitOfMeasureCode2
                     , ProductHierarchyID       = cdItem.ProductHierarchyID
                     , ProductHierarchyLevel01
                     , ProductHierarchyLevel02
                     , ProductHierarchyLevel03
                     , ProductHierarchyLevel04
                     , ProductHierarchyLevel05
                     , ProductHierarchyLevel06
                     , ProductCollectionGrCode  = cdItem.ProductCollectionGrCode
                     , SeasonCode , SeasonDescription
                     , CollectionCode , CollectionDescription
                     , StoryBoardCode , StoryBoardDescription
                     , IsBlocked                = cdItem.IsBlocked
                     , SalesPrice1              = 0 -- Varsayılan fiyat
                     , VatRate                  = 10 -- Varsayılan KDV oranı
                FROM prItemVariant WITH(NOLOCK)
                        LEFT OUTER JOIN (SELECT prItemBarcode.*, BarcodeCount = (SELECT COUNT(Barcode) FROM prItemBarcode A 
                                                                                        WHERE A.ItemTypeCode        = prItemBarcode.ItemTypeCode
                                                                                            AND A.ItemCode          = prItemBarcode.ItemCode
                                                                                            AND A.ColorCode         = prItemBarcode.ColorCode
                                                                                            AND A.ItemDim1Code      = prItemBarcode.ItemDim1Code
                                                                                            AND A.ItemDim2Code      = prItemBarcode.ItemDim2Code
                                                                                            AND A.ItemDim3Code      = prItemBarcode.ItemDim3Code
                                                                                            AND A.BarcodeTypeCode   = prItemBarcode.BarcodeTypeCode
                                                                                            AND 0 = 1)
                                                FROM prItemBarcode WITH(NOLOCK)) AS prItemBarcode
                            ON prItemBarcode.ItemTypeCode   = prItemVariant.ItemTypeCode
                            AND prItemBarcode.ItemCode      = prItemVariant.ItemCode
                            AND prItemBarcode.ColorCode     = prItemVariant.ColorCode
                            AND prItemBarcode.ItemDim1Code  = prItemVariant.ItemDim1Code
                            AND prItemBarcode.ItemDim2Code  = prItemVariant.ItemDim2Code
                            AND prItemBarcode.ItemDim3Code  = prItemVariant.ItemDim3Code
                        INNER JOIN cdItem WITH(NOLOCK)
                            ON cdItem.ItemTypeCode          = prItemVariant.ItemTypeCode
                            AND cdItem.ItemCode             = prItemVariant.ItemCode
                        INNER JOIN ProductHierarchy(N'TR')
                            ON ProductHierarchy.ProductHierarchyID = cdItem.ProductHierarchyID
                        INNER JOIN ProductCollection(N'TR')
                            ON ProductCollection.ProductCollectionGrCode = cdItem.ProductCollectionGrCode
                        LEFT OUTER JOIN ProductAttributesFilter
                            ON ProductAttributesFilter.ItemTypeCode      = cdItem.ItemTypeCode
                            AND ProductAttributesFilter.ItemCode        = cdItem.ItemCode
                        LEFT OUTER JOIN prItemColorAttributes WITH(NOLOCK)
                            ON prItemColorAttributes.ItemTypeCode = prItemVariant.ItemTypeCode
                            AND prItemColorAttributes.ItemCode = prItemVariant.ItemCode
                            AND prItemColorAttributes.ColorCode = prItemVariant.ColorCode
                WHERE prItemVariant.ItemTypeCode = 1    
                AND (CASE WHEN 0 = 1 THEN ISNULL(prItemBarcode.BarcodeCount,SPACE(0)) ELSE 2 END > 1)
                AND (prItemVariant.ItemCode LIKE @SearchText OR 
                     EXISTS (SELECT 1 FROM cdItemDesc WITH(NOLOCK) 
                             WHERE cdItemDesc.ItemTypeCode = prItemVariant.ItemTypeCode 
                             AND cdItemDesc.ItemCode = prItemVariant.ItemCode 
                             AND cdItemDesc.LangCode = N'TR' 
                             AND cdItemDesc.ItemDescription LIKE @SearchText))";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var productVariant = new ProductVariantModel
                                {
                                    ProductCode = reader["ProductCode"]?.ToString(),
                                    ProductDescription = reader["ProductDescription"]?.ToString(),
                                    ColorCode = reader["ColorCode"]?.ToString(),
                                    ColorDescription = reader["ColorDescription"]?.ToString(),
                                    ManufacturerColorCode = reader["ManufacturerColorCode"]?.ToString(),
                                    ItemDim1Code = reader["ItemDim1Code"]?.ToString(),
                                    ItemDim2Code = reader["ItemDim2Code"]?.ToString(),
                                    ItemDim3Code = reader["ItemDim3Code"]?.ToString(),
                                    BarcodeTypeCode = reader["BarcodeTypeCode"]?.ToString(),
                                    Barcode = reader["Barcode"]?.ToString(),
                                    NotHaveBarcodes = Convert.ToBoolean(reader["NotHaveBarcodes"]),
                                    Qty = reader["Qty"] != DBNull.Value ? Convert.ToDecimal(reader["Qty"]) : null,
                                    ProductTypeCode = reader["ProductTypeCode"]?.ToString(),
                                    ProductTypeDescription = reader["ProductTypeDescription"]?.ToString(),
                                    UnitOfMeasureCode1 = reader["UnitOfMeasureCode1"]?.ToString(),
                                    UnitOfMeasureCode2 = reader["UnitOfMeasureCode2"]?.ToString(),
                                    ProductHierarchyID = reader["ProductHierarchyID"]?.ToString(),
                                    ProductHierarchyLevel01 = reader["ProductHierarchyLevel01"]?.ToString(),
                                    ProductHierarchyLevel02 = reader["ProductHierarchyLevel02"]?.ToString(),
                                    ProductHierarchyLevel03 = reader["ProductHierarchyLevel03"]?.ToString(),
                                    ProductHierarchyLevel04 = reader["ProductHierarchyLevel04"]?.ToString(),
                                    ProductHierarchyLevel05 = reader["ProductHierarchyLevel05"]?.ToString(),
                                    ProductHierarchyLevel06 = reader["ProductHierarchyLevel06"]?.ToString(),
                                    ProductCollectionGrCode = reader["ProductCollectionGrCode"]?.ToString(),
                                    SeasonCode = reader["SeasonCode"]?.ToString(),
                                    SeasonDescription = reader["SeasonDescription"]?.ToString(),
                                    CollectionCode = reader["CollectionCode"]?.ToString(),
                                    CollectionDescription = reader["CollectionDescription"]?.ToString(),
                                    StoryBoardCode = reader["StoryBoardCode"]?.ToString(),
                                    StoryBoardDescription = reader["StoryBoardDescription"]?.ToString(),
                                    IsBlocked = Convert.ToBoolean(reader["IsBlocked"]),
                                    SalesPrice1 = reader["SalesPrice1"] != DBNull.Value ? Convert.ToDecimal(reader["SalesPrice1"]) : 0,
                                    VatRate = reader["VatRate"] != DBNull.Value ? Convert.ToDecimal(reader["VatRate"]) : null
                                };

                                result.Add(productVariant);
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün kodu veya açıklaması ile ürün varyantları aranırken hata oluştu. Arama metni: {SearchText}", searchText);
                throw;
            }
        }

        public async Task<List<ProductPriceListModel>> GetProductPriceListAsync(string productCode)
        {
            try
            {
                var result = new List<ProductPriceListModel>();

                if (string.IsNullOrEmpty(productCode))
                {
                    return result;
                }

                // Ürün koduna göre fiyat listesi getirme için SQL sorgusu
                _logger.LogInformation($"Fiyat listesi için SQL sorgusu çalıştırılıyor. Ürün Kodu: {productCode}");
                var query = @"
                  SELECT 
                    l.SortOrder AS SortOrder,
                    l.ItemTypeCode AS ItemTypeCode,
                    l.Price AS BirimFiyat,
                    ISNULL((SELECT ItemTypeDescription FROM bsItemTypeDesc WITH(NOLOCK) 
                           WHERE bsItemTypeDesc.ItemTypeCode = l.ItemTypeCode 
                           AND bsItemTypeDesc.LangCode = 'tr'), SPACE(0)) AS ItemTypeDescription,
                    l.ItemCode AS ItemCode,
                    ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) 
                           WHERE cdItemDesc.ItemTypeCode = l.ItemTypeCode 
                           AND cdItemDesc.ItemCode = l.ItemCode 
                           AND cdItemDesc.LangCode = 'tr'), SPACE(0)) AS ItemDescription,
                    l.ColorCode AS ColorCode,
                    ISNULL((SELECT ColorDescription FROM cdColorDesc WITH(NOLOCK) 
                           WHERE cdColorDesc.ColorCode = l.ColorCode 
                           AND cdColorDesc.LangCode = 'tr'), SPACE(0)) AS ColorDescription,
                    l.ItemDim1Code AS ItemDim1Code,
                    l.ItemDim2Code AS ItemDim2Code,
                    l.ItemDim3Code AS ItemDim3Code,
                    l.UnitOfMeasureCode AS UnitOfMeasureCode,
                    l.PaymentPlanCode AS PaymentPlanCode,
                    l.LineDescription AS LineDescription,
                    l.DocCurrencyCode AS DocCurrencyCode,
                    l.IsDisabled AS IsDisabled,
                    l.DisableDate AS DisableDate,
                    l.PriceListHeaderID AS HeaderID
                FROM trPriceListLine l WITH(NOLOCK)
                INNER JOIN trPriceListHeader h WITH(NOLOCK) 
                    ON l.PriceListHeaderID = h.PriceListHeaderID
					WHERE ItemCode = @productCode
                ";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@productCode", productCode);
                        command.Parameters.AddWithValue("@LangCode", "TR");

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var priceList = new ProductPriceListModel
                                {
                                    PriceListNumber = reader["PriceListNumber"]?.ToString(),
                                    PriceGroupCode = reader["PriceGroupCode"]?.ToString(),
                                    PriceGroupDescription = reader["PriceGroupDescription"]?.ToString(),
                                    PriceListTypeCode = reader["PriceListTypeCode"]?.ToString(),
                                    PriceListTypeDescription = reader["PriceListTypeDescription"]?.ToString(),
                                    PriceListDate = reader["PriceListDate"] != DBNull.Value ? Convert.ToDateTime(reader["PriceListDate"]) : null,
                                    ValidDate = reader["PriceListDate"] != DBNull.Value ? Convert.ToDateTime(reader["PriceListDate"]) : null, // PriceListDate kullanıyoruz
                                    ValidTime = null, // Yeni sorguda bu alan yok
                                    CompanyCode = reader["CompanyCode"]?.ToString(),
                                    IsConfirmed = true, // Yeni sorguda bu alan yok, varsayılan olarak true
                                    IsCompleted = true, // Yeni sorguda bu alan yok, varsayılan olarak true
                                    IsLocked = false, // Yeni sorguda bu alan yok, varsayılan olarak false
                                    ApplicationCode = "", // Yeni sorguda bu alan yok
                                    ApplicationDescription = "", // Yeni sorguda bu alan yok
                                    CreatedUserName = "", // Yeni sorguda bu alan yok
                                    LastUpdatedUserName = "", // Yeni sorguda bu alan yok
                                    PriceListHeaderID = Convert.ToInt32(reader["HeaderID"]),
                                    ApplicationID = 0, // Yeni sorguda bu alan yok
                                    Price = reader["BirimFiyat"] != DBNull.Value ? Convert.ToDecimal(reader["BirimFiyat"]) : 0, // BirimFiyat alanını kullanıyoruz
                                    VatRate = 10, // Varsayılan KDV oranı
                                    ProductCode = reader["ItemCode"]?.ToString() // ItemCode alanını kullanıyoruz
                                };

                                result.Add(priceList);
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ürün koduna göre fiyat listesi aranırken hata oluştu. Ürün Kodu: {productCode}");
                _logger.LogError($"Hata detayı: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner exception: {ex.InnerException.Message}");
                }
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                
                // Boş liste döndür
                return new List<ProductPriceListModel>();
            }
        }

        public async Task<List<ProductPriceListDetailModel>> GetAllProductPriceListAsync(
            int page = 1, 
            int pageSize = 50, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            int companyCode = 1)
        {
            try
            {
                var result = new List<ProductPriceListDetailModel>();
                
                // Varsayılan tarih aralığı: Geçerli ayın başından bugüne
                if (!startDate.HasValue)
                {
                    startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                }
                
                if (!endDate.HasValue)
                {
                    endDate = DateTime.Now;
                }
                
                _logger.LogInformation($"Tüm ürün fiyat listesi getiriliyor. Sayfa: {page}, Sayfa Boyutu: {pageSize}, Tarih Aralığı: {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd}, Şirket Kodu: {companyCode}");

                // Sayfalama için offset hesapla
                int offset = (page - 1) * pageSize;
                
                // Tamamen yeniden yazılmış basit SQL sorgusu
                var query = @"
                SELECT 
                    l.SortOrder AS SortOrder,
                    l.ItemTypeCode AS ItemTypeCode,
                    l.Price AS BirimFiyat,
                    ISNULL((SELECT ItemTypeDescription FROM bsItemTypeDesc WITH(NOLOCK) 
                           WHERE bsItemTypeDesc.ItemTypeCode = l.ItemTypeCode 
                           AND bsItemTypeDesc.LangCode = @LangCode), SPACE(0)) AS ItemTypeDescription,
                    l.ItemCode AS ItemCode,
                    ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) 
                           WHERE cdItemDesc.ItemTypeCode = l.ItemTypeCode 
                           AND cdItemDesc.ItemCode = l.ItemCode 
                           AND cdItemDesc.LangCode = @LangCode), SPACE(0)) AS ItemDescription,
                    l.ColorCode AS ColorCode,
                    ISNULL((SELECT ColorDescription FROM cdColorDesc WITH(NOLOCK) 
                           WHERE cdColorDesc.ColorCode = l.ColorCode 
                           AND cdColorDesc.LangCode = @LangCode), SPACE(0)) AS ColorDescription,
                    l.ItemDim1Code AS ItemDim1Code,
                    l.ItemDim2Code AS ItemDim2Code,
                    l.ItemDim3Code AS ItemDim3Code,
                    l.UnitOfMeasureCode AS UnitOfMeasureCode,
                    l.PaymentPlanCode AS PaymentPlanCode,
                    l.LineDescription AS LineDescription,
                    l.DocCurrencyCode AS DocCurrencyCode,
                    l.IsDisabled AS IsDisabled,
                    l.DisableDate AS DisableDate,
                    l.PriceListHeaderID AS HeaderID
                FROM trPriceListLine l WITH(NOLOCK)
                INNER JOIN trPriceListHeader h WITH(NOLOCK) 
                    ON l.PriceListHeaderID = h.PriceListHeaderID
                WHERE h.PriceListDate BETWEEN @StartDate AND @EndDate
                AND h.CompanyCode = @CompanyCode
                ORDER BY l.ItemCode, l.ColorCode, l.ItemDim1Code
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

                try
                {
                    _logger.LogInformation("SQL sorgusu: {Query}", query);
                    _logger.LogInformation("Parametreler: LangCode=TR, PageSize={PageSize}, Offset={Offset}, StartDate={StartDate}, EndDate={EndDate}, CompanyCode={CompanyCode}", 
                        pageSize, offset, startDate?.ToString("yyyy-MM-dd") ?? "null", endDate?.ToString("yyyy-MM-dd") ?? "null", companyCode);
                    
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        _logger.LogInformation("Veritabanı bağlantısı başarıyla açıldı: {ConnectionString}", _connectionString);

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LangCode", "TR");
                            command.Parameters.AddWithValue("@PageSize", pageSize);
                            command.Parameters.AddWithValue("@Offset", offset);
                            
                            // Tarih parametrelerini SQL Server formatında gönder
                            var formattedStartDate = startDate.HasValue ? startDate.Value.ToString("yyyyMMdd") : "19000101";
                            var formattedEndDate = endDate.HasValue ? endDate.Value.ToString("yyyyMMdd") : "99991231";
                            
                            command.Parameters.AddWithValue("@StartDate", formattedStartDate);
                            command.Parameters.AddWithValue("@EndDate", formattedEndDate);
                            command.Parameters.AddWithValue("@CompanyCode", companyCode);
                            
                            _logger.LogInformation("Tarih parametreleri: StartDate={StartDate}, EndDate={EndDate}", formattedStartDate, formattedEndDate);
                            _logger.LogInformation($"SQL sorgusu çalıştırılıyor. Sayfa: {page}, Offset: {offset}");

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                _logger.LogInformation("SQL sorgusu başarıyla çalıştırıldı");
                                
                                int rowCount = 0;
                                while (await reader.ReadAsync())
                                {
                                    rowCount++;
                                    var priceListDetail = new ProductPriceListDetailModel
                                    {
                                        SortOrder = reader["SortOrder"] != DBNull.Value ? Convert.ToInt32(reader["SortOrder"]) : 0,
                                        ItemTypeCode = reader["ItemTypeCode"]?.ToString(),
                                        ItemTypeDescription = reader["ItemTypeDescription"]?.ToString(),
                                        ItemCode = reader["ItemCode"]?.ToString(),
                                        ItemDescription = reader["ItemDescription"]?.ToString(),
                                        ColorCode = reader["ColorCode"]?.ToString(),
                                        ColorDescription = reader["ColorDescription"]?.ToString(),
                                        ItemDim1Code = reader["ItemDim1Code"]?.ToString(),
                                        ItemDim2Code = reader["ItemDim2Code"]?.ToString(),
                                        ItemDim3Code = reader["ItemDim3Code"]?.ToString(),
                                        UnitOfMeasureCode = reader["UnitOfMeasureCode"]?.ToString(),
                                        PaymentPlanCode = reader["PaymentPlanCode"]?.ToString(),
                                        LineDescription = reader["LineDescription"]?.ToString(),
                                        BirimFiyat = reader["BirimFiyat"] != DBNull.Value ? Convert.ToDecimal(reader["BirimFiyat"]) : 0,
                                        DocCurrencyCode = reader["DocCurrencyCode"]?.ToString(),
                                        IsDisabled = reader["IsDisabled"] != DBNull.Value && Convert.ToBoolean(reader["IsDisabled"]),
                                        DisableDate = reader["DisableDate"] != DBNull.Value ? Convert.ToDateTime(reader["DisableDate"]) : null,
                                        HeaderID = reader["HeaderID"]?.ToString()
                                    };

                                    result.Add(priceListDetail);
                                }
                                
                                _logger.LogInformation($"Toplam {rowCount} adet fiyat listesi kaydı bulundu");
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
                _logger.LogError(ex, $"Tüm ürün fiyat listesi getirilirken hata oluştu. Sayfa: {page}, Sayfa Boyutu: {pageSize}");
                _logger.LogError($"Hata detayı: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner exception: {ex.InnerException.Message}");
                }
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                
                // Boş liste döndür
                return new List<ProductPriceListDetailModel>();
            }
        }

        /// <summary>
        /// Ürün tiplerini getirir
        /// </summary>
        /// <returns>Ürün tipleri listesi</returns>
        public async Task<List<ProductTypeModel>> GetProductTypesAsync()
        {
            try
            {
                var productTypes = new List<ProductTypeModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                        SELECT 
                            pt.ProductTypeCode AS Code, 
                            ptd.ProductTypeDescription AS Description
                        FROM 
                            dbo.bsProductType pt
                        LEFT JOIN 
                            dbo.bsProductTypeDesc ptd ON pt.ProductTypeCode = ptd.ProductTypeCode AND ptd.LangCode = N'TR'
                        ORDER BY 
                            pt.ProductTypeCode";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var productType = new ProductTypeModel
                                {
                                    Code = reader["Code"]?.ToString(),
                                    Description = reader["Description"]?.ToString()
                                };

                                productTypes.Add(productType);
                            }
                        }
                    }
                }

                return productTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün tipleri getirilirken hata oluştu");
                throw;
            }
        }

        /// <summary>
        /// Ölçü birimlerini getirir
        /// </summary>
        /// <returns>Ölçü birimleri listesi</returns>
        public async Task<List<UnitOfMeasureModel>> GetUnitsOfMeasureAsync()
        {
            try
            {
                var unitsOfMeasure = new List<UnitOfMeasureModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                        SELECT 
                            uom.UnitOfMeasureCode AS Code, 
                            uomd.UnitOfMeasureDescription AS Description
                        FROM 
                            dbo.bsUnitOfMeasure uom
                        LEFT JOIN 
                            dbo.bsUnitOfMeasureDesc uomd ON uom.UnitOfMeasureCode = uomd.UnitOfMeasureCode AND uomd.LangCode = N'TR'
                        ORDER BY 
                            uom.UnitOfMeasureCode";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var unitOfMeasure = new UnitOfMeasureModel
                                {
                                    Code = reader["Code"]?.ToString(),
                                    Description = reader["Description"]?.ToString()
                                };

                                unitsOfMeasure.Add(unitOfMeasure);
                            }
                        }
                    }
                }

                return unitsOfMeasure;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ölçü birimleri getirilirken hata oluştu");
                throw;
            }
        }
    }
}
