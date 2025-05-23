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
                     , VatRate                  = 18 -- Varsayılan KDV oranı
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
                _logger.LogError(ex, $"Barkod ile ürün varyantları aranırken hata oluştu. Barkod: {barcode}");
                throw;
            }
        }
    }
}
