using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ErpMobile.Api.Models.Item;
using ErpMobile.Api.Data;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/Items")]
    [EnableCors]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ErpDbContext _context;

        public ItemController(
            ILogger<ItemController> logger,
            ErpDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Gets a list of items/products
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="sortBy">Field to sort by</param>
        /// <param name="sortDirection">Sort direction (asc or desc)</param>
        /// <param name="searchTerm">Filter by item code or description</param>
        /// <param name="productTypeCode">Filter by product type code</param>
        /// <param name="isBlocked">Filter by blocked status</param>
        /// <param name="itemTypeCode">Filter by item type code (1: Product, 2: Material)</param>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>List of items/products</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<object>>> GetItems(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "ProductCode",
            [FromQuery] string sortDirection = "asc",
            [FromQuery] string searchTerm = null,
            [FromQuery] string productTypeCode = null,
            [FromQuery] bool? isBlocked = null,
            [FromQuery] int itemTypeCode = 1,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                // Filtreleme koşullarını oluştur
                var whereClause = "WHERE cdItem.ItemTypeCode = @ItemTypeCode"; // ItemTypeCode parametresine göre filtrele (1: Ürün, 2: Malzeme)
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode),
                    new SqlParameter("@ItemTypeCode", itemTypeCode)
                };

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    whereClause += " AND (cdItem.ItemCode LIKE @SearchTerm OR cdItemDesc.ItemDescription LIKE @SearchTerm)";
                    parameters.Add(new SqlParameter("@SearchTerm", $"%{searchTerm}%"));
                }

                if (!string.IsNullOrEmpty(productTypeCode))
                {
                    whereClause += " AND cdItem.ProductTypeCode = @ProductTypeCode";
                    parameters.Add(new SqlParameter("@ProductTypeCode", productTypeCode));
                }

                if (isBlocked.HasValue)
                {
                    whereClause += " AND cdItem.IsBlocked = @IsBlocked";
                    parameters.Add(new SqlParameter("@IsBlocked", isBlocked.Value));
                }

                // Sıralama koşulunu oluştur
                var orderByClause = $"ORDER BY {sortBy} {sortDirection}";

                // Sayfalama için OFFSET-FETCH kullan
                var paginationClause = $"OFFSET {(page - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                // Toplam kayıt sayısını almak için sorgu
                var countQuery = $@"
                SELECT COUNT(1)
                FROM cdItem WITH (NOLOCK)
                LEFT JOIN cdItemDesc WITH (NOLOCK) ON cdItem.ItemTypeCode = cdItemDesc.ItemTypeCode 
                    AND cdItem.ItemCode = cdItemDesc.ItemCode 
                    AND cdItemDesc.LangCode = @LangCode
                LEFT JOIN bsProductType WITH (NOLOCK) ON cdItem.ProductTypeCode = bsProductType.ProductTypeCode
                LEFT JOIN bsProductTypeDesc WITH (NOLOCK) ON bsProductType.ProductTypeCode = bsProductTypeDesc.ProductTypeCode 
                    AND bsProductTypeDesc.LangCode = @LangCode
                LEFT JOIN bsItemDimType WITH (NOLOCK) ON cdItem.ItemDimTypeCode = bsItemDimType.ItemDimTypeCode
                LEFT JOIN bsItemDimTypeDesc WITH (NOLOCK) ON bsItemDimType.ItemDimTypeCode = bsItemDimTypeDesc.ItemDimTypeCode 
                    AND bsItemDimTypeDesc.LangCode = @LangCode
                {whereClause}";

                // Verileri almak için sorgu
                var fetchClause = $@"
                SELECT 
                    ProductCode = RTRIM(LTRIM(cdItem.ItemCode)),
                    ProductDescription = RTRIM(LTRIM(ISNULL(cdItemDesc.ItemDescription, ''))),
                    bsProductType.ProductTypeCode,
                    ProductTypeDescription = RTRIM(LTRIM(ISNULL(bsProductTypeDesc.ProductTypeDescription, ''))),
                    cdItem.ItemDimTypeCode,
                    ItemDimTypeDescription = RTRIM(LTRIM(ISNULL(bsItemDimTypeDesc.ItemDimTypeDescription, ''))),
                    UnitOfMeasureCode1 = RTRIM(LTRIM(cdItem.UnitOfMeasureCode1)),
                    UnitOfMeasureCode2 = RTRIM(LTRIM(cdItem.UnitOfMeasureCode2)),
                    CompanyBrandCode = '',
                    cdItem.UsePOS,
                    cdItem.UseStore,
                    cdItem.UseRoll,
                    cdItem.UseBatch,
                    cdItem.GenerateSerialNumber,
                    cdItem.UseSerialNumber,
                    cdItem.IsUTSDeclaratedItem,
                    cdItem.CreatedDate,
                    cdItem.LastUpdatedDate,
                    cdItem.IsBlocked
                FROM cdItem WITH (NOLOCK)
                LEFT JOIN cdItemDesc WITH (NOLOCK) ON cdItem.ItemTypeCode = cdItemDesc.ItemTypeCode 
                    AND cdItem.ItemCode = cdItemDesc.ItemCode 
                    AND cdItemDesc.LangCode = @LangCode
                LEFT JOIN bsProductType WITH (NOLOCK) ON cdItem.ProductTypeCode = bsProductType.ProductTypeCode
                LEFT JOIN bsProductTypeDesc WITH (NOLOCK) ON bsProductType.ProductTypeCode = bsProductTypeDesc.ProductTypeCode 
                    AND bsProductTypeDesc.LangCode = @LangCode
                LEFT JOIN bsItemDimType WITH (NOLOCK) ON cdItem.ItemDimTypeCode = bsItemDimType.ItemDimTypeCode
                LEFT JOIN bsItemDimTypeDesc WITH (NOLOCK) ON bsItemDimType.ItemDimTypeCode = bsItemDimTypeDesc.ItemDimTypeCode 
                    AND bsItemDimTypeDesc.LangCode = @LangCode
                {whereClause}
                {orderByClause}
                {paginationClause}";

                var query = $@"
                {fetchClause}";
                
                // Toplam kayıt sayısını al
                var totalCount = Convert.ToInt32(await _context.ExecuteScalarAsync(countQuery, parameters.ToArray()));
                
                // Verileri al
                var items = new List<ItemModel>();
                
                // Yeni parametre dizisi oluştur (aynı parametreleri tekrar kullanmak yerine)
                var queryParams = new List<SqlParameter>();
                foreach (var param in parameters)
                {
                    queryParams.Add(new SqlParameter(param.ParameterName, param.Value));
                }
                
                using (var reader = await _context.ExecuteReaderAsync(query, queryParams.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new ItemModel
                        {
                            ItemCode = reader["ProductCode"].ToString(),
                            ItemDescription = reader["ProductDescription"].ToString(),
                            ProductTypeCode = reader["ProductTypeCode"].ToString(),
                            ProductTypeDescription = reader["ProductTypeDescription"].ToString(),
                            ItemDimTypeCode = reader["ItemDimTypeCode"].ToString(),
                            ItemDimTypeDescription = reader["ItemDimTypeDescription"].ToString(),
                            UnitOfMeasureCode1 = reader["UnitOfMeasureCode1"].ToString(),
                            UnitOfMeasureCode2 = reader["UnitOfMeasureCode2"].ToString(),
                            CompanyBrandCode = reader["CompanyBrandCode"].ToString(),
                            UsePOS = Convert.ToBoolean(reader["UsePOS"]),
                            UseStore = Convert.ToBoolean(reader["UseStore"]),
                            UseRoll = Convert.ToBoolean(reader["UseRoll"]),
                            UseBatch = Convert.ToBoolean(reader["UseBatch"]),
                            GenerateSerialNumber = Convert.ToBoolean(reader["GenerateSerialNumber"]),
                            UseSerialNumber = Convert.ToBoolean(reader["UseSerialNumber"]),
                            IsUTSDeclaratedItem = Convert.ToBoolean(reader["IsUTSDeclaratedItem"]),
                            CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                            LastUpdatedDate = reader["LastUpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastUpdatedDate"]) : (DateTime?)null,
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                // Sayfalama bilgilerini hesapla
                var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
                
                // Sonucu döndür
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Items retrieved successfully",
                    Data = new
                    {
                        items,
                        totalCount,
                        pageCount,
                        currentPage = page,
                        pageSize
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting items");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Error getting items: " + ex.Message,
                    Data = string.Empty
                });
            }
        }
        /// <summary>
        /// Gets an item/product by its code
        /// </summary>
        /// <param name="itemCode">Item code</param>
        /// <param name="itemTypeCode">Item type code (1: Product, 2: Material)</param>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>Item/product details</returns>
        [HttpGet("{itemCode}")]
        public async Task<ActionResult<ApiResponse<ItemModel>>> GetItem(
            string itemCode,
            [FromQuery] int itemTypeCode = 1,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                if (string.IsNullOrEmpty(itemCode))
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Ürün kodu gereklidir",
                        Data = new object()
                    });
                }

                var query = $@"
                SELECT 
                    ProductCode = RTRIM(LTRIM(cdItem.ItemCode)),
                    ProductDescription = RTRIM(LTRIM(ISNULL(cdItemDesc.ItemDescription, ''))),
                    bsProductType.ProductTypeCode,
                    ProductTypeDescription = RTRIM(LTRIM(ISNULL(bsProductTypeDesc.ProductTypeDescription, ''))),
                    cdItem.ItemDimTypeCode,
                    ItemDimTypeDescription = RTRIM(LTRIM(ISNULL(bsItemDimTypeDesc.ItemDimTypeDescription, ''))),
                    UnitOfMeasureCode1 = RTRIM(LTRIM(cdItem.UnitOfMeasureCode1)),
                    UnitOfMeasureCode2 = RTRIM(LTRIM(cdItem.UnitOfMeasureCode2)),
                    CompanyBrandCode = '',
                    cdItem.UsePOS,
                    cdItem.UseStore,
                    cdItem.UseRoll,
                    cdItem.UseBatch,
                    cdItem.GenerateSerialNumber,
                    cdItem.UseSerialNumber,
                    cdItem.IsUTSDeclaratedItem,
                    cdItem.CreatedDate,
                    cdItem.LastUpdatedDate,
                    cdItem.IsBlocked
                FROM cdItem WITH (NOLOCK)
                LEFT JOIN cdItemDesc WITH (NOLOCK) ON cdItem.ItemTypeCode = cdItemDesc.ItemTypeCode 
                    AND cdItem.ItemCode = cdItemDesc.ItemCode 
                    AND cdItemDesc.LangCode = @LangCode
                LEFT JOIN bsProductType WITH (NOLOCK) ON cdItem.ProductTypeCode = bsProductType.ProductTypeCode
                LEFT JOIN bsProductTypeDesc WITH (NOLOCK) ON bsProductType.ProductTypeCode = bsProductTypeDesc.ProductTypeCode 
                    AND bsProductTypeDesc.LangCode = @LangCode
                LEFT JOIN bsItemDimType WITH (NOLOCK) ON cdItem.ItemDimTypeCode = bsItemDimType.ItemDimTypeCode
                LEFT JOIN bsItemDimTypeDesc WITH (NOLOCK) ON bsItemDimType.ItemDimTypeCode = bsItemDimTypeDesc.ItemDimTypeCode 
                    AND bsItemDimTypeDesc.LangCode = @LangCode
                WHERE cdItem.ItemTypeCode = @ItemTypeCode AND cdItem.ItemCode = @ItemCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", itemCode),
                    new SqlParameter("@LangCode", langCode),
                    new SqlParameter("@ItemTypeCode", itemTypeCode)
                };
                
                // Sorguyu çalıştır
                ItemModel item = null;
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    if (await reader.ReadAsync())
                    {
                        item = new ItemModel
                        {
                            ItemCode = reader["ProductCode"].ToString(),
                            ItemDescription = reader["ProductDescription"].ToString(),
                            ProductTypeCode = reader["ProductTypeCode"].ToString(),
                            ProductTypeDescription = reader["ProductTypeDescription"].ToString(),
                            ItemDimTypeCode = reader["ItemDimTypeCode"].ToString(),
                            ItemDimTypeDescription = reader["ItemDimTypeDescription"].ToString(),
                            UnitOfMeasureCode1 = reader["UnitOfMeasureCode1"].ToString(),
                            UnitOfMeasureCode2 = reader["UnitOfMeasureCode2"].ToString(),
                            CompanyBrandCode = reader["CompanyBrandCode"].ToString(),
                            UsePOS = Convert.ToBoolean(reader["UsePOS"]),
                            UseStore = Convert.ToBoolean(reader["UseStore"]),
                            UseRoll = Convert.ToBoolean(reader["UseRoll"]),
                            UseBatch = Convert.ToBoolean(reader["UseBatch"]),
                            GenerateSerialNumber = Convert.ToBoolean(reader["GenerateSerialNumber"]),
                            UseSerialNumber = Convert.ToBoolean(reader["UseSerialNumber"]),
                            IsUTSDeclaratedItem = Convert.ToBoolean(reader["IsUTSDeclaratedItem"]),
                            CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                            LastUpdatedDate = reader["LastUpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastUpdatedDate"]) : (DateTime?)null,
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        };
                    }
                }
                
                if (item == null)
                {
                    return NotFound(new ApiResponse<ItemModel>
                    {
                        Success = false,
                        Message = $"Item with code {itemCode} not found",
                        Data = null
                    });
                }
                
                return Ok(new ApiResponse<ItemModel>
                {
                    Success = true,
                    Message = "Item retrieved successfully",
                    Data = item
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting item");
                return StatusCode(500, new ApiResponse<ItemModel>
                {
                    Success = false,
                    Message = "Error getting item: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Gets item attribute types
        /// </summary>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>List of item attribute types</returns>
        [HttpGet("attribute-types")]
        public async Task<ActionResult<ApiResponse<List<ItemAttributeTypeModel>>>> GetItemAttributeTypes(
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        cdItemAttributeType.ItemTypeCode,
                        AttributeTypeCode = RTRIM(LTRIM(cdItemAttributeType.AttributeTypeCode)),
                        AttributeTypeDescription = RTRIM(LTRIM(ISNULL(cdItemAttributeTypeDesc.Description, ''))),
                        ProductHierarchyFilter = RTRIM(LTRIM(ISNULL(cdItemAttributeType.ProductHierarchyFilter, ''))),
                        cdItemAttributeType.IsRequired,
                        cdItemAttributeType.IsBlocked
                    FROM cdItemAttributeType WITH (NOLOCK)
                    LEFT JOIN cdItemAttributeTypeDesc WITH (NOLOCK) ON cdItemAttributeType.ItemTypeCode = cdItemAttributeTypeDesc.ItemTypeCode 
                        AND cdItemAttributeType.AttributeTypeCode = cdItemAttributeTypeDesc.AttributeTypeCode 
                        AND cdItemAttributeTypeDesc.LangCode = @LangCode
                    WHERE cdItemAttributeType.ItemTypeCode = 1
                    ORDER BY cdItemAttributeType.AttributeTypeCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };
                
                // Sorguyu çalıştır
                var attributeTypes = new List<ItemAttributeTypeModel>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        attributeTypes.Add(new ItemAttributeTypeModel
                        {
                            ItemTypeCode = Convert.ToInt32(reader["ItemTypeCode"]),
                            AttributeTypeCode = reader["AttributeTypeCode"].ToString(),
                            AttributeTypeDescription = reader["AttributeTypeDescription"].ToString(),
                            ProductHierarchyFilter = reader["ProductHierarchyFilter"].ToString(),
                            IsRequired = Convert.ToBoolean(reader["IsRequired"]),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<ItemAttributeTypeModel>>
                {
                    Success = true,
                    Message = "Item attribute types retrieved successfully",
                    Data = attributeTypes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting item attribute types");
                return StatusCode(500, new ApiResponse<List<ItemAttributeTypeModel>>
                {
                    Success = false,
                    Message = "Error getting item attribute types: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Gets item attributes
        /// </summary>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>List of item attributes</returns>
        [HttpGet("attributes")]
        public async Task<ActionResult<ApiResponse<List<ItemAttributeModel>>>> GetItemAttributes(
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        AttributeTypeCode = RTRIM(LTRIM(cdItemAttribute.AttributeTypeCode)),
                        AttributeTypeDescription = RTRIM(LTRIM(ISNULL(cdItemAttributeTypeDesc.Description, ''))),
                        AttributeCode = RTRIM(LTRIM(cdItemAttribute.AttributeCode)),
                        AttributeDescription = RTRIM(LTRIM(ISNULL(cdItemAttribute.Description, ''))),
                        ProductHierarchyFilter = RTRIM(LTRIM(ISNULL(cdItemAttribute.ProductHierarchyFilter, ''))),
                        cdItemAttribute.IsBlocked
                    FROM cdItemAttribute WITH (NOLOCK)
                    LEFT JOIN cdItemAttributeType WITH (NOLOCK) ON cdItemAttribute.ItemTypeCode = cdItemAttributeType.ItemTypeCode 
                        AND cdItemAttribute.AttributeTypeCode = cdItemAttributeType.AttributeTypeCode
                    LEFT JOIN cdItemAttributeTypeDesc WITH (NOLOCK) ON cdItemAttributeType.ItemTypeCode = cdItemAttributeTypeDesc.ItemTypeCode 
                        AND cdItemAttributeType.AttributeTypeCode = cdItemAttributeTypeDesc.AttributeTypeCode 
                        AND cdItemAttributeTypeDesc.LangCode = @LangCode
                    WHERE cdItemAttribute.ItemTypeCode = 1
                    ORDER BY cdItemAttribute.AttributeTypeCode, cdItemAttribute.AttributeCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };
                
                // Sorguyu çalıştır
                var attributes = new List<ItemAttributeModel>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        attributes.Add(new ItemAttributeModel
                        {
                            AttributeTypeCode = reader["AttributeTypeCode"].ToString(),
                            AttributeTypeDescription = reader["AttributeTypeDescription"].ToString(),
                            AttributeCode = reader["AttributeCode"].ToString(),
                            AttributeDescription = reader["AttributeDescription"].ToString(),
                            ProductHierarchyFilter = reader["ProductHierarchyFilter"].ToString(),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<ItemAttributeModel>>
                {
                    Success = true,
                    Message = "Item attributes retrieved successfully",
                    Data = attributes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting item attributes");
                return StatusCode(500, new ApiResponse<List<ItemAttributeModel>>
                {
                    Success = false,
                    Message = "Error getting item attributes: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Gets barcode types
        /// </summary>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>List of barcode types</returns>
        [HttpGet("barcode-types")]
        public async Task<ActionResult<ApiResponse<List<BarcodeTypeModel>>>> GetBarcodeTypes(
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        BarcodeTypeCode = RTRIM(LTRIM(cdBarcodeType.BarcodeTypeCode)),
                        BarcodeTypeDescription = RTRIM(LTRIM(ISNULL(cdBarcodeTypeDesc.Description, ''))),
                        StandardBarcodeTypeCode = RTRIM(LTRIM(cdBarcodeType.StandardBarcodeTypeCode)),
                        StandardBarcodeTypeDescription = RTRIM(LTRIM(ISNULL(bsStandardBarcodeTypeDesc.Description, ''))),
                        cdBarcodeType.IsBlocked
                    FROM cdBarcodeType WITH (NOLOCK)
                    LEFT JOIN cdBarcodeTypeDesc WITH (NOLOCK) ON cdBarcodeType.BarcodeTypeCode = cdBarcodeTypeDesc.BarcodeTypeCode 
                        AND cdBarcodeTypeDesc.LangCode = @LangCode
                    LEFT JOIN bsStandardBarcodeType WITH (NOLOCK) ON cdBarcodeType.StandardBarcodeTypeCode = bsStandardBarcodeType.StandardBarcodeTypeCode
                    LEFT JOIN bsStandardBarcodeTypeDesc WITH (NOLOCK) ON bsStandardBarcodeType.StandardBarcodeTypeCode = bsStandardBarcodeTypeDesc.StandardBarcodeTypeCode 
                        AND bsStandardBarcodeTypeDesc.LangCode = @LangCode
                    ORDER BY cdBarcodeType.BarcodeTypeCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };
                
                // Sorguyu çalıştır
                var barcodeTypes = new List<BarcodeTypeModel>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        barcodeTypes.Add(new BarcodeTypeModel
                        {
                            BarcodeTypeCode = reader["BarcodeTypeCode"].ToString(),
                            BarcodeTypeDescription = reader["BarcodeTypeDescription"].ToString(),
                            StandardBarcodeTypeCode = reader["StandardBarcodeTypeCode"].ToString(),
                            StandardBarcodeTypeDescription = reader["StandardBarcodeTypeDescription"].ToString(),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<BarcodeTypeModel>>
                {
                    Success = true,
                    Message = "Barcode types retrieved successfully",
                    Data = barcodeTypes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting barcode types");
                return StatusCode(500, new ApiResponse<List<BarcodeTypeModel>>
                {
                    Success = false,
                    Message = "Error getting barcode types: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Gets product types
        /// </summary>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>List of product types</returns>
        [HttpGet("product-types")]
        public async Task<ActionResult<ApiResponse<List<object>>>> GetProductTypes(
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        bsProductType.ProductTypeCode,
                        ProductTypeDescription
                    FROM bsProductType
                    LEFT JOIN bsProductTypeDesc ON bsProductType.ProductTypeCode = bsProductTypeDesc.ProductTypeCode
                    WHERE bsProductTypeDesc.LangCode = @LangCode
                    ORDER BY ProductTypeDescription";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };

                var productTypes = new List<object>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        productTypes.Add(new
                        {
                            ProductTypeCode = reader["ProductTypeCode"].ToString(),
                            ProductTypeDescription = reader["ProductTypeDescription"].ToString()
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<object>>
                {
                    Success = true,
                    Message = "Product types retrieved successfully",
                    Data = productTypes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product types");
                return StatusCode(500, new ApiResponse<List<object>>
                {
                    Success = false,
                    Message = "Error getting product types: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Gets unit of measures
        /// </summary>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>List of unit of measures</returns>
        [HttpGet("unit-of-measures")]
        public async Task<ActionResult<ApiResponse<List<object>>>> GetUnitOfMeasures(
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        UnitOfMeasureCode,
                        UnitOfMeasureDescription
                    FROM cdUnitOfMeasure
                    LEFT JOIN cdUnitOfMeasureDesc ON cdUnitOfMeasure.UnitOfMeasureCode = cdUnitOfMeasureDesc.UnitOfMeasureCode
                    WHERE cdUnitOfMeasureDesc.LangCode = @LangCode
                    ORDER BY UnitOfMeasureDescription";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };

                var unitOfMeasures = new List<object>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        unitOfMeasures.Add(new
                        {
                            UnitOfMeasureCode = reader["UnitOfMeasureCode"].ToString(),
                            UnitOfMeasureDescription = reader["UnitOfMeasureDescription"].ToString()
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<object>>
                {
                    Success = true,
                    Message = "Unit of measures retrieved successfully",
                    Data = unitOfMeasures
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unit of measures");
                return StatusCode(500, new ApiResponse<List<object>>
                {
                    Success = false,
                    Message = "Error getting unit of measures: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Creates a new item/product
        /// </summary>
        /// <param name="request">Item creation request</param>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>Created item</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ItemModel>>> CreateItem(
            [FromBody] CreateItemRequest request,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                // Check if item already exists
                var checkQuery = @"
                    SELECT COUNT(1) FROM cdItem 
                    WHERE ItemTypeCode = 1 AND ItemCode = @ItemCode";

                var checkParams = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", request.ItemCode)
                };

                var exists = Convert.ToInt32(await _context.ExecuteScalarAsync(checkQuery, checkParams.ToArray()));
                if (exists > 0)
                {
                    return Conflict(new ApiResponse<ItemModel>
                    {
                        Success = false,
                        Message = $"Item with code {request.ItemCode} already exists",
                        Data = null
                    });
                }

                // Insert into cdItem
                var insertItemQuery = @"
                    INSERT INTO cdItem (
                        ItemTypeCode, ItemCode, ItemDimTypeCode, ProductTypeCode, 
                        ProductHierarchyID, UnitOfMeasureCode1, UnitOfMeasureCode2,
                        UseRoll, UseBatch, UsePOS, UseStore, UseSerialNumber, 
                        GenerateSerialNumber, IsUTSDeclaratedItem, IsBlocked,
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate, RowGuid
                    )
                    VALUES (
                        1, @ItemCode, @ItemDimTypeCode, @ProductTypeCode, 
                        @ProductHierarchyID, @UnitOfMeasureCode1, @UnitOfMeasureCode2,
                        @UseRoll, @UseBatch, @UsePOS, @UseStore, @UseSerialNumber, 
                        @GenerateSerialNumber, @IsUTSDeclaratedItem, @IsBlocked,
                        @UserName, GETDATE(), @UserName, GETDATE(), NEWID()
                    )";

                var username = User.Identity?.Name ?? "system";
                var insertItemParams = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", request.ItemCode),
                    new SqlParameter("@ItemDimTypeCode", string.IsNullOrEmpty(request.ItemDimTypeCode) ? DBNull.Value : (object)request.ItemDimTypeCode),
                    new SqlParameter("@ProductTypeCode", string.IsNullOrEmpty(request.ProductTypeCode) ? DBNull.Value : (object)request.ProductTypeCode),
                    new SqlParameter("@ProductHierarchyID", request.ProductHierarchyID == 0 ? DBNull.Value : (object)request.ProductHierarchyID),
                    new SqlParameter("@UnitOfMeasureCode1", string.IsNullOrEmpty(request.UnitOfMeasureCode1) ? DBNull.Value : (object)request.UnitOfMeasureCode1),
                    new SqlParameter("@UnitOfMeasureCode2", string.IsNullOrEmpty(request.UnitOfMeasureCode2) ? DBNull.Value : (object)request.UnitOfMeasureCode2),
                    new SqlParameter("@UseRoll", request.UseRoll),
                    new SqlParameter("@UseBatch", request.UseBatch),
                    new SqlParameter("@UsePOS", request.UsePOS),
                    new SqlParameter("@UseStore", request.UseStore),
                    new SqlParameter("@UseSerialNumber", request.UseSerialNumber),
                    new SqlParameter("@GenerateSerialNumber", request.GenerateSerialNumber),
                    new SqlParameter("@IsUTSDeclaratedItem", request.IsUTSDeclaratedItem),
                    new SqlParameter("@IsBlocked", request.IsBlocked),
                    new SqlParameter("@UserName", username)
                };

                await _context.ExecuteNonQueryAsync(insertItemQuery, insertItemParams.ToArray());

                // Insert into cdItemDesc
                var insertDescQuery = @"
                    INSERT INTO cdItemDesc (
                        ItemTypeCode, ItemCode, LangCode, ItemDescription,
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate, RowGuid
                    )
                    VALUES (
                        1, @ItemCode, @LangCode, @ItemDescription,
                        @UserName, GETDATE(), @UserName, GETDATE(), NEWID()
                    )";

                var insertDescParams = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", request.ItemCode),
                    new SqlParameter("@LangCode", langCode),
                    new SqlParameter("@ItemDescription", request.ItemDescription),
                    new SqlParameter("@UserName", username)
                };

                await _context.ExecuteNonQueryAsync(insertDescQuery, insertDescParams.ToArray());

                // Get the created item
                return await GetItem(request.ItemCode, 1, langCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating item");
                return StatusCode(500, new ApiResponse<ItemModel>
                {
                    Success = false,
                    Message = "Error creating item: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Updates an existing item/product
        /// </summary>
        /// <param name="itemCode">Item code to update</param>
        /// <param name="request">Item update request</param>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>Updated item</returns>
        [HttpPut("{itemCode}")]
        public async Task<ActionResult<ApiResponse<ItemModel>>> UpdateItem(
            string itemCode,
            [FromBody] UpdateItemRequest request,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                // Check if item exists
                var checkQuery = @"
                    SELECT COUNT(1) FROM cdItem 
                    WHERE ItemTypeCode = 1 AND ItemCode = @ItemCode";

                var checkParams = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", itemCode)
                };

                var exists = Convert.ToInt32(await _context.ExecuteScalarAsync(checkQuery, checkParams.ToArray()));
                if (exists == 0)
                {
                    return NotFound(new ApiResponse<ItemModel>
                    {
                        Success = false,
                        Message = $"Item with code {itemCode} not found",
                        Data = null
                    });
                }

                // Update cdItem
                var updateItemQuery = @"
                    UPDATE cdItem SET
                        ItemDimTypeCode = CASE WHEN @ItemDimTypeCode IS NULL THEN ItemDimTypeCode ELSE @ItemDimTypeCode END,
                        ProductTypeCode = CASE WHEN @ProductTypeCode IS NULL THEN ProductTypeCode ELSE @ProductTypeCode END,
                        ProductHierarchyID = CASE WHEN @ProductHierarchyID IS NULL THEN ProductHierarchyID ELSE @ProductHierarchyID END,
                        UnitOfMeasureCode1 = CASE WHEN @UnitOfMeasureCode1 IS NULL THEN UnitOfMeasureCode1 ELSE @UnitOfMeasureCode1 END,
                        UnitOfMeasureCode2 = CASE WHEN @UnitOfMeasureCode2 IS NULL THEN UnitOfMeasureCode2 ELSE @UnitOfMeasureCode2 END,
                        UseRoll = CASE WHEN @UseRoll IS NULL THEN UseRoll ELSE @UseRoll END,
                        UseBatch = CASE WHEN @UseBatch IS NULL THEN UseBatch ELSE @UseBatch END,
                        UsePOS = CASE WHEN @UsePOS IS NULL THEN UsePOS ELSE @UsePOS END,
                        UseStore = CASE WHEN @UseStore IS NULL THEN UseStore ELSE @UseStore END,
                        UseSerialNumber = CASE WHEN @UseSerialNumber IS NULL THEN UseSerialNumber ELSE @UseSerialNumber END,
                        GenerateSerialNumber = CASE WHEN @GenerateSerialNumber IS NULL THEN GenerateSerialNumber ELSE @GenerateSerialNumber END,
                        IsUTSDeclaratedItem = CASE WHEN @IsUTSDeclaratedItem IS NULL THEN IsUTSDeclaratedItem ELSE @IsUTSDeclaratedItem END,
                        IsBlocked = CASE WHEN @IsBlocked IS NULL THEN IsBlocked ELSE @IsBlocked END,
                        LastUpdatedUserName = @UserName,
                        LastUpdatedDate = GETDATE()
                    WHERE ItemTypeCode = 1 AND ItemCode = @ItemCode";

                var username = User.Identity?.Name ?? "system";
                var updateItemParams = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", itemCode),
                    new SqlParameter("@ItemDimTypeCode", string.IsNullOrEmpty(request.ItemDimTypeCode) ? DBNull.Value : (object)request.ItemDimTypeCode),
                    new SqlParameter("@ProductTypeCode", string.IsNullOrEmpty(request.ProductTypeCode) ? DBNull.Value : (object)request.ProductTypeCode),
                    new SqlParameter("@ProductHierarchyID", request.ProductHierarchyID == null ? DBNull.Value : (object)request.ProductHierarchyID),
                    new SqlParameter("@UnitOfMeasureCode1", string.IsNullOrEmpty(request.UnitOfMeasureCode1) ? DBNull.Value : (object)request.UnitOfMeasureCode1),
                    new SqlParameter("@UnitOfMeasureCode2", string.IsNullOrEmpty(request.UnitOfMeasureCode2) ? DBNull.Value : (object)request.UnitOfMeasureCode2),
                    new SqlParameter("@UseRoll", request.UseRoll == null ? DBNull.Value : (object)request.UseRoll),
                    new SqlParameter("@UseBatch", request.UseBatch == null ? DBNull.Value : (object)request.UseBatch),
                    new SqlParameter("@UsePOS", request.UsePOS == null ? DBNull.Value : (object)request.UsePOS),
                    new SqlParameter("@UseStore", request.UseStore == null ? DBNull.Value : (object)request.UseStore),
                    new SqlParameter("@UseSerialNumber", request.UseSerialNumber == null ? DBNull.Value : (object)request.UseSerialNumber),
                    new SqlParameter("@GenerateSerialNumber", request.GenerateSerialNumber == null ? DBNull.Value : (object)request.GenerateSerialNumber),
                    new SqlParameter("@IsUTSDeclaratedItem", request.IsUTSDeclaratedItem == null ? DBNull.Value : (object)request.IsUTSDeclaratedItem),
                    new SqlParameter("@IsBlocked", request.IsBlocked == null ? DBNull.Value : (object)request.IsBlocked),
                    new SqlParameter("@UserName", username)
                };

                await _context.ExecuteNonQueryAsync(updateItemQuery, updateItemParams.ToArray());

                // Update cdItemDesc
                var updateDescQuery = @"
                    UPDATE cdItemDesc SET
                        ItemDescription = @ItemDescription,
                        LastUpdatedUserName = @UserName,
                        LastUpdatedDate = GETDATE()
                    WHERE ItemTypeCode = 1 AND ItemCode = @ItemCode AND LangCode = @LangCode";

                var updateDescParams = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", itemCode),
                    new SqlParameter("@LangCode", langCode),
                    new SqlParameter("@ItemDescription", request.ItemDescription),
                    new SqlParameter("@UserName", username)
                };

                var descUpdateResult = await _context.ExecuteNonQueryAsync(updateDescQuery, updateDescParams.ToArray());
                
                // If no description record exists for this language, create one
                if (descUpdateResult == 0)
                {
                    var insertDescQuery = @"
                        INSERT INTO cdItemDesc (
                            ItemTypeCode, ItemCode, LangCode, ItemDescription,
                            CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate, RowGuid
                        )
                        VALUES (
                            1, @ItemCode, @LangCode, @ItemDescription,
                            @UserName, GETDATE(), @UserName, GETDATE(), NEWID()
                        )";

                    await _context.ExecuteNonQueryAsync(insertDescQuery, updateDescParams.ToArray());
                }

                // Get the updated item
                return await GetItem(itemCode, 1, langCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating item");
                return StatusCode(500, new ApiResponse<ItemModel>
                {
                    Success = false,
                    Message = "Error updating item: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Deletes an item/product
        /// </summary>
        /// <param name="itemCode">Item code to delete</param>
        /// <returns>Success response</returns>
        [HttpDelete("{itemCode}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteItem(string itemCode)
        {
            try
            {
                // Check if item exists
                var checkQuery = @"
                    SELECT COUNT(1) FROM cdItem 
                    WHERE ItemTypeCode = 1 AND ItemCode = @ItemCode";

                var checkParams = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", itemCode)
                };

                var exists = Convert.ToInt32(await _context.ExecuteScalarAsync(checkQuery, checkParams.ToArray()));
                if (exists == 0)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Item with code {itemCode} not found",
                        Data = null
                    });
                }

                // In a real-world scenario, we might want to check for references before deleting
                // For example, check if the item is used in invoices, orders, etc.
                // For now, we'll implement a soft delete by setting IsBlocked = 1

                var updateQuery = @"
                    UPDATE cdItem SET
                        IsBlocked = 1,
                        LastUpdatedUserName = @UserName,
                        LastUpdatedDate = GETDATE()
                    WHERE ItemTypeCode = 1 AND ItemCode = @ItemCode";

                var username = User.Identity?.Name ?? "system";
                var updateParams = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", itemCode),
                    new SqlParameter("@UserName", username)
                };

                await _context.ExecuteNonQueryAsync(updateQuery, updateParams.ToArray());

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = $"Item with code {itemCode} has been blocked (soft deleted)",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting item");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Error deleting item: " + ex.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Depo listesini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu (varsayılan: TR)</param>
        /// <returns>Depo listesi</returns>
        [HttpGet("warehouses")]
        public async Task<ActionResult<ApiResponse<List<WarehouseModel>>>> GetWarehouses(
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        WarehouseCode,
                        WarehouseOwnerCode,
                        WarehouseTypeCode,
                        WarehouseCategoryCode,
                        OfficeCode,
                        CurrAccTypeCode,
                        CurrAccCode,
                        SubCurrAccID,
                        PermitNegativeStock,
                        WarnNegativeStock,
                        ControlStockLevel,
                        WarnStockLevelRate,
                        TotalArea,
                        WarehouseWidth,
                        WarehouseLength,
                        WarehouseHeight,
                        URNAddress,
                        EShipmentUrnAddress,
                        UseSection,
                        PermitRetailSubsequentDelivery,
                        IsDefault,
                        IsBlocked,
                        LangCode,
                        WarehouseDescription
                    FROM dbo.Warehouse(@LangCode)
                    WHERE IsBlocked = 0
                    ORDER BY WarehouseCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };

                var warehouses = new List<WarehouseModel>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        warehouses.Add(new WarehouseModel
                        {
                            WarehouseCode = reader["WarehouseCode"].ToString(),
                            WarehouseDescription = reader["WarehouseDescription"].ToString(),
                            WarehouseOwnerCode = reader["WarehouseOwnerCode"].ToString(),
                            WarehouseTypeCode = reader["WarehouseTypeCode"].ToString(),
                            WarehouseCategoryCode = reader["WarehouseCategoryCode"].ToString(),
                            OfficeCode = reader["OfficeCode"].ToString(),
                            CurrAccTypeCode = Convert.ToInt32(reader["CurrAccTypeCode"]),
                            CurrAccCode = reader["CurrAccCode"].ToString(),
                            PermitNegativeStock = Convert.ToBoolean(reader["PermitNegativeStock"]),
                            WarnNegativeStock = Convert.ToBoolean(reader["WarnNegativeStock"]),
                            IsDefault = Convert.ToBoolean(reader["IsDefault"]),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<WarehouseModel>>
                {
                    Success = true,
                    Message = "Depo listesi başarıyla getirildi",
                    Data = warehouses
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depo listesi getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Depo listesi getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Depo adres bilgilerini getirir
        /// </summary>
        /// <param name="warehouseCode">Depo kodu</param>
        /// <param name="langCode">Dil kodu (varsayılan: TR)</param>
        /// <returns>Depo adres bilgileri</returns>
        [HttpGet("warehouses/{warehouseCode}/address")]
        public async Task<ActionResult<ApiResponse<WarehouseAddressModel>>> GetWarehouseAddress(
            string warehouseCode,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        WarehouseCode,
                        Address,
                        SiteName,
                        BuildingName,
                        BuildingNum,
                        FloorNum,
                        DoorNum,
                        QuarterName,
                        Boulevard,
                        Street,
                        Road,
                        CountryCode,
                        CountryDescription,
                        StateCode,
                        StateDescription,
                        CityCode,
                        CityDescription,
                        DistrictCode,
                        DistrictDescription,
                        ZipCode,
                        DrivingDirections
                    FROM dbo.WarehousePostalAddress(@LangCode)
                    WHERE WarehouseCode = @WarehouseCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode),
                    new SqlParameter("@WarehouseCode", warehouseCode)
                };

                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    if (await reader.ReadAsync())
                    {
                        var warehouseAddress = new WarehouseAddressModel
                        {
                            WarehouseCode = reader["WarehouseCode"].ToString(),
                            Address = reader["Address"].ToString(),
                            SiteName = reader["SiteName"].ToString(),
                            BuildingName = reader["BuildingName"].ToString(),
                            BuildingNum = reader["BuildingNum"].ToString(),
                            FloorNum = reader["FloorNum"].ToString(),
                            DoorNum = reader["DoorNum"].ToString(),
                            QuarterName = reader["QuarterName"].ToString(),
                            Boulevard = reader["Boulevard"].ToString(),
                            Street = reader["Street"].ToString(),
                            Road = reader["Road"].ToString(),
                            CountryCode = reader["CountryCode"].ToString(),
                            CountryDescription = reader["CountryDescription"].ToString(),
                            StateCode = reader["StateCode"].ToString(),
                            StateDescription = reader["StateDescription"].ToString(),
                            CityCode = reader["CityCode"].ToString(),
                            CityDescription = reader["CityDescription"].ToString(),
                            DistrictCode = reader["DistrictCode"].ToString(),
                            DistrictDescription = reader["DistrictDescription"].ToString(),
                            ZipCode = reader["ZipCode"].ToString(),
                            DrivingDirections = reader["DrivingDirections"].ToString()
                        };
                        
                        return Ok(new ApiResponse<WarehouseAddressModel>
                        {
                            Success = true,
                            Message = "Depo adres bilgileri başarıyla getirildi",
                            Data = warehouseAddress
                        });
                    }
                    else
                    {
                        return NotFound(new ApiResponse<object>
                        {
                            Success = false,
                            Message = $"Depo bulunamadı: {warehouseCode}",
                            Data = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depo adres bilgileri getirilirken hata oluştu. Depo Kodu: {WarehouseCode}", warehouseCode);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Depo adres bilgileri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// KDV oranlarını getirir
        /// </summary>
        /// <param name="langCode">Dil kodu (varsayılan: TR)</param>
        /// <returns>KDV oranları listesi</returns>
        [HttpGet("vat-rates")]
        public async Task<ActionResult<ApiResponse<List<VatModel>>>> GetVatRates(
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        VatCode = RTRIM(LTRIM(cdVat.VatCode)),
                        VatDescription = RTRIM(LTRIM(ISNULL(cdVatDesc.VatDescription, ''))),
                        VatRate = cdVat.VatRate,
                        IsBlocked = cdVat.IsBlocked
                    FROM cdVat WITH (NOLOCK)
                    LEFT JOIN cdVatDesc WITH (NOLOCK) 
                        ON cdVat.VatCode = cdVatDesc.VatCode 
                        AND cdVatDesc.LangCode = @LangCode
                    WHERE cdVat.IsBlocked = 0
                    ORDER BY cdVat.VatRate";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };

                var vatRates = new List<VatModel>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        vatRates.Add(new VatModel
                        {
                            VatCode = reader["VatCode"].ToString(),
                            VatDescription = reader["VatDescription"].ToString(),
                            VatRate = Convert.ToDecimal(reader["VatRate"]),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<VatModel>>
                {
                    Success = true,
                    Message = "VAT rates retrieved successfully",
                    Data = vatRates
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting VAT rates");
                return StatusCode(500, new ApiResponse<List<VatModel>>
                {
                    Success = false,
                    Message = "An error occurred while getting VAT rates",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Ürün fiyat listesini getirir
        /// </summary>
        /// <param name="itemCode">Ürün kodu</param>
        /// <param name="priceListCode">Fiyat listesi kodu (boş ise tüm fiyat listeleri getirilir)</param>
        /// <param name="currencyCode">Para birimi kodu (boş ise tüm para birimleri getirilir)</param>
        /// <param name="langCode">Dil kodu (varsayılan: TR)</param>
        /// <returns>Ürün fiyat listesi</returns>
        [HttpGet("price-list")]
        public async Task<ActionResult<ApiResponse<List<ItemPriceModel>>>> GetItemPrices(
            [FromQuery] string itemCode,
            [FromQuery] string priceListCode = null,
            [FromQuery] string currencyCode = null,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                if (string.IsNullOrEmpty(itemCode))
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Ürün kodu gereklidir",
                        Data = null
                    });
                }

                var whereClause = "WHERE prItemBasePrice.ItemCode = @ItemCode AND prItemBasePrice.IsBlocked = 0";
                
                if (!string.IsNullOrEmpty(priceListCode))
                {
                    whereClause += " AND prItemBasePrice.PriceListCode = @PriceListCode";
                }
                
                if (!string.IsNullOrEmpty(currencyCode))
                {
                    whereClause += " AND prItemBasePrice.CurrencyCode = @CurrencyCode";
                }
                
                var query = $@"
                    SELECT 
                        ItemCode = RTRIM(LTRIM(prItemBasePrice.ItemCode)),
                        PriceListCode = RTRIM(LTRIM(prItemBasePrice.PriceListCode)),
                        PriceListDescription = RTRIM(LTRIM(ISNULL(trPriceListHeaderDesc.PriceListDescription, ''))),
                        CurrencyCode = RTRIM(LTRIM(prItemBasePrice.CurrencyCode)),
                        Price = prItemBasePrice.Price,
                        EffectiveDate = prItemBasePrice.EffectiveDate,
                        ExpirationDate = prItemBasePrice.ExpirationDate,
                        IsVatIncluded = prItemBasePrice.IsVatIncluded,
                        IsBlocked = prItemBasePrice.IsBlocked
                    FROM prItemBasePrice WITH (NOLOCK)
                    LEFT JOIN trPriceListHeader WITH (NOLOCK) 
                        ON prItemBasePrice.PriceListCode = trPriceListHeader.PriceListCode
                    LEFT JOIN trPriceListHeaderDesc WITH (NOLOCK) 
                        ON trPriceListHeader.PriceListCode = trPriceListHeaderDesc.PriceListCode 
                        AND trPriceListHeaderDesc.LangCode = @LangCode
                    {whereClause}
                    ORDER BY prItemBasePrice.EffectiveDate DESC";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@ItemCode", itemCode),
                    new SqlParameter("@LangCode", langCode)
                };

                if (!string.IsNullOrEmpty(priceListCode))
                {
                    parameters.Add(new SqlParameter("@PriceListCode", priceListCode));
                }
                
                if (!string.IsNullOrEmpty(currencyCode))
                {
                    parameters.Add(new SqlParameter("@CurrencyCode", currencyCode));
                }

                var prices = new List<ItemPriceModel>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        prices.Add(new ItemPriceModel
                        {
                            ItemCode = reader["ItemCode"]?.ToString() ?? string.Empty,
                            PriceListCode = reader["PriceListCode"]?.ToString() ?? string.Empty,
                            PriceListDescription = reader["PriceListDescription"]?.ToString() ?? string.Empty,
                            CurrencyCode = reader["CurrencyCode"]?.ToString() ?? string.Empty,
                            Price = Convert.ToDecimal(reader["Price"]),
                            EffectiveDate = Convert.ToDateTime(reader["EffectiveDate"]),
                            ExpirationDate = reader["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpirationDate"]) : (DateTime?)null,
                            IsVatIncluded = Convert.ToBoolean(reader["IsVatIncluded"]),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<ItemPriceModel>>
                {
                    Success = true,
                    Message = "Ürün fiyat listesi başarıyla getirildi",
                    Data = prices
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün fiyat listesi getirilirken hata oluştu. Ürün Kodu: {ItemCode}", itemCode);
                return StatusCode(500, new ApiResponse<List<ItemPriceModel>>
                {
                    Success = false,
                    Message = "Ürün fiyat listesi getirilirken bir hata oluştu",
                    Data = new List<ItemPriceModel>()
                });
            }
        }

        /// <summary>
        /// Barkod ile ürün arama
        /// </summary>
        /// <param name="barcode">Barkod</param>
        /// <param name="langCode">Dil kodu (varsayılan: TR)</param>
        /// <returns>Ürün bilgisi</returns>
        [HttpGet("search-by-barcode")]
        public async Task<ActionResult<ApiResponse<ItemModel>>> GetItemByBarcode(
            [FromQuery] string barcode,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                if (string.IsNullOrEmpty(barcode))
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Barkod gereklidir",
                        Data = new object()
                    });
                }

                var query = @"
                    SELECT 
                        i.ItemCode,
                        ItemDescription = RTRIM(LTRIM(ISNULL(id.ItemDescription, ''))),
                        i.ItemTypeCode,
                        i.UnitCode,
                        i.VatCode,
                        v.VatRate,
                        i.IsBlocked
                    FROM cdItem i WITH (NOLOCK)
                    LEFT JOIN cdItemDesc id WITH (NOLOCK) 
                        ON i.ItemCode = id.ItemCode 
                        AND id.LangCode = @LangCode
                    LEFT JOIN cdVat v WITH (NOLOCK)
                        ON i.VatCode = v.VatCode
                    INNER JOIN cdItemBarcode b WITH (NOLOCK)
                        ON i.ItemCode = b.ItemCode
                    WHERE b.Barcode = @Barcode
                    AND i.IsBlocked = 0";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Barcode", barcode),
                    new SqlParameter("@LangCode", langCode)
                };

                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    if (await reader.ReadAsync())
                    {
                        var item = new ItemModel
                        {
                            ItemCode = reader["ItemCode"].ToString() ?? string.Empty,
                            ItemDescription = reader["ItemDescription"].ToString() ?? string.Empty,
                            ItemTypeCode = Convert.ToInt32(reader["ItemTypeCode"]),
                            UnitCode = reader["UnitCode"].ToString() ?? string.Empty,
                            VatCode = reader["VatCode"].ToString() ?? string.Empty,
                            VatRate = Convert.ToDecimal(reader["VatRate"]),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        };
                        
                        return Ok(new ApiResponse<ItemModel>
                        {
                            Success = true,
                            Message = "Ürün başarıyla getirildi",
                            Data = item
                        });
                    }
                    else
                    {
                        return NotFound(new ApiResponse<object>
                        {
                            Success = false,
                            Message = $"Barkod ile ürün bulunamadı: {barcode}",
                            Data = new object()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Barkod ile ürün aranırken hata oluştu. Barkod: {Barcode}", barcode);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Barkod ile ürün aranırken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
