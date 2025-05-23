using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Product;
using ErpMobile.Api.Repositories.Product;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductController(
            ILogger<ProductController> logger,
            IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Gets a list of products with optional filtering
        /// </summary>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="sortBy">Field to sort by</param>
        /// <param name="sortDirection">Sort direction (asc or desc)</param>
        /// <param name="productCode">Filter by product code</param>
        /// <param name="productDescription">Filter by product description</param>
        /// <param name="productTypeCode">Filter by product type code</param>
        /// <param name="isBlocked">Filter by blocked status</param>
        /// <returns>List of products</returns>
        [HttpGet]
        public ActionResult<ApiResponse<object>> GetProducts(
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1,
            [FromQuery] string sortBy = "productCode",
            [FromQuery] string sortDirection = "asc",
            [FromQuery] string productCode = null,
            [FromQuery] string productDescription = null,
            [FromQuery] string productTypeCode = null,
            [FromQuery] bool? isBlocked = null)
        {
            try
            {
                // Mock response for demonstration purposes
                var items = new List<object>
                {
                    new
                    {
                        productCode = "P001",
                        productDescription = "Sample Product 1",
                        productTypeCode = "PT001",
                        productTypeDescription = "Sample Type 1",
                        itemDimTypeCode = "DT001",
                        itemDimTypeDescription = "Dimension Type 1",
                        unitOfMeasureCode1 = "UOM1",
                        unitOfMeasureCode2 = "UOM2",
                        companyBrandCode = "CB001",
                        usePOS = true,
                        useStore = true,
                        useRoll = false,
                        useBatch = true,
                        generateSerialNumber = false,
                        useSerialNumber = true,
                        isUTSDeclaratedItem = false,
                        createdDate = DateTime.Now.AddDays(-30),
                        lastUpdatedDate = DateTime.Now.AddDays(-5),
                        isBlocked = false
                    },
                    new
                    {
                        productCode = "P002",
                        productDescription = "Sample Product 2",
                        productTypeCode = "PT002",
                        productTypeDescription = "Sample Type 2",
                        itemDimTypeCode = "DT002",
                        itemDimTypeDescription = "Dimension Type 2",
                        unitOfMeasureCode1 = "UOM3",
                        unitOfMeasureCode2 = "UOM4",
                        companyBrandCode = "CB002",
                        usePOS = false,
                        useStore = true,
                        useRoll = true,
                        useBatch = false,
                        generateSerialNumber = true,
                        useSerialNumber = true,
                        isUTSDeclaratedItem = true,
                        createdDate = DateTime.Now.AddDays(-20),
                        lastUpdatedDate = DateTime.Now.AddDays(-2),
                        isBlocked = false
                    }
                };

                var response = new
                {
                    items = items,
                    totalCount = 2,
                    pageCount = 1,
                    currentPage = pageNumber,
                    pageSize = pageSize
                };

                return Ok(new ApiResponse<object>(response, true, "Products retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products");
                return StatusCode(500, new ApiResponse<object>(null, false, "An error occurred while retrieving products.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Gets a product by its code
        /// </summary>
        /// <param name="productCode">The product code</param>
        /// <returns>The product details</returns>
        [HttpGet("{productCode}")]
        public ActionResult<ApiResponse<object>> GetProductByCode(string productCode)
        {
            try
            {
                // Mock response for demonstration purposes
                var product = new
                {
                    productCode = productCode,
                    productDescription = "Sample Product",
                    productTypeCode = "PT001",
                    productTypeDescription = "Sample Type",
                    itemDimTypeCode = "DT001",
                    itemDimTypeDescription = "Dimension Type",
                    unitOfMeasureCode1 = "UOM1",
                    unitOfMeasureCode2 = "UOM2",
                    companyBrandCode = "CB001",
                    usePOS = true,
                    useStore = true,
                    useRoll = false,
                    useBatch = true,
                    generateSerialNumber = false,
                    useSerialNumber = true,
                    isUTSDeclaratedItem = false,
                    createdDate = DateTime.Now.AddDays(-30),
                    lastUpdatedDate = DateTime.Now.AddDays(-5),
                    isBlocked = false
                };

                return Ok(new ApiResponse<object>(product, true, "Product retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product with code {ProductCode}", productCode);
                return StatusCode(500, new ApiResponse<object>(null, false, $"An error occurred while retrieving product with code {productCode}.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="product">The product data</param>
        /// <returns>The created product</returns>
        [HttpPost]
        public ActionResult<ApiResponse<object>> CreateProduct([FromBody] object product)
        {
            try
            {
                // Mock response for demonstration purposes
                return Created($"api/product/P003", new ApiResponse<object>(product, true, "Product created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, new ApiResponse<object>(null, false, "An error occurred while creating product.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="productCode">The product code</param>
        /// <param name="product">The updated product data</param>
        /// <returns>Success or failure response</returns>
        [HttpPut("{productCode}")]
        public ActionResult<ApiResponse<bool>> UpdateProduct(string productCode, [FromBody] object product)
        {
            try
            {
                return Ok(new ApiResponse<bool>(true, true, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with code {ProductCode}", productCode);
                return StatusCode(500, new ApiResponse<bool>(false, false, $"An error occurred while updating product with code {productCode}.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="productCode">The product code</param>
        /// <returns>Success or failure response</returns>
        [HttpDelete("{productCode}")]
        public ActionResult<ApiResponse<bool>> DeleteProduct(string productCode)
        {
            try
            {
                return Ok(new ApiResponse<bool>(true, true, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with code {ProductCode}", productCode);
                return StatusCode(500, new ApiResponse<bool>(false, false, $"An error occurred while deleting product with code {productCode}.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Gets a list of product types
        /// </summary>
        /// <returns>List of product types</returns>
        [HttpGet("types")]
        public ActionResult<ApiResponse<object>> GetProductTypes()
        {
            try
            {
                // Mock response for demonstration purposes
                var types = new List<object>
                {
                    new { code = "PT001", description = "Sample Type 1" },
                    new { code = "PT002", description = "Sample Type 2" },
                    new { code = "PT003", description = "Sample Type 3" }
                };

                return Ok(new ApiResponse<object>(types, true, "Product types retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product types");
                return StatusCode(500, new ApiResponse<object>(null, false, "An error occurred while retrieving product types.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Gets a list of units of measure
        /// </summary>
        /// <returns>List of units of measure</returns>
        [HttpGet("units-of-measure")]
        public ActionResult<ApiResponse<object>> GetUnitsOfMeasure()
        {
            try
            {
                // Mock response for demonstration purposes
                var units = new List<object>
                {
                    new { code = "UOM1", description = "Piece" },
                    new { code = "UOM2", description = "Kilogram" },
                    new { code = "UOM3", description = "Meter" },
                    new { code = "UOM4", description = "Liter" }
                };

                return Ok(new ApiResponse<object>(units, true, "Units of measure retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting units of measure");
                return StatusCode(500, new ApiResponse<object>(null, false, "An error occurred while retrieving units of measure.", "InternalServerError"));
            }
        }
        /// <summary>
        /// Barkod ile ürün varyantlarını arar
        /// </summary>
        /// <param name="barcode">Ürün barkodu</param>
        /// <returns>Ürün varyant listesi</returns>
        [HttpGet("variants/by-barcode/{barcode}")]
        public async Task<ActionResult<ApiResponse<List<ProductVariantModel>>>> GetProductVariantsByBarcode(string barcode)
        {
            try
            {
                if (string.IsNullOrEmpty(barcode))
                {
                    return BadRequest(new ApiResponse<List<ProductVariantModel>>(null, false, "Barkod boş olamaz", "BadRequest"));
                }

                var variants = await _productRepository.GetProductVariantsByBarcodeAsync(barcode);

                if (variants == null || variants.Count == 0)
                {
                    return NotFound(new ApiResponse<List<ProductVariantModel>>(null, false, $"{barcode} barkoduna sahip ürün bulunamadı", "NotFound"));
                }

                return Ok(new ApiResponse<List<ProductVariantModel>>(variants, true, "Ürün varyantları başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Barkod ile ürün varyantları aranırken hata oluştu. Barkod: {Barcode}", barcode);
                return StatusCode(500, new ApiResponse<List<ProductVariantModel>>(null, false, "Ürün varyantları getirilirken bir hata oluştu.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Ürün koduna göre fiyat listesini getirir
        /// </summary>
        /// <param name="productCode">Ürün kodu</param>
        /// <returns>Ürün fiyat listesi</returns>
        [HttpGet("price-list/{productCode}")]
        public async Task<ActionResult<ApiResponse<List<ProductPriceListModel>>>> GetProductPriceList(string productCode)
        {
            try
            {
                if (string.IsNullOrEmpty(productCode))
                {
                    return BadRequest(new ApiResponse<List<ProductPriceListModel>>(null, false, "Ürün kodu boş olamaz", "BadRequest"));
                }

                var priceList = await _productRepository.GetProductPriceListAsync(productCode);

                if (priceList == null || priceList.Count == 0)
                {
                    return NotFound(new ApiResponse<List<ProductPriceListModel>>(null, false, $"{productCode} kodlu ürün için fiyat listesi bulunamadı", "NotFound"));
                }

                return Ok(new ApiResponse<List<ProductPriceListModel>>(priceList, true, "Ürün fiyat listesi başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün koduna göre fiyat listesi aranırken hata oluştu. Ürün Kodu: {ProductCode}", productCode);
                return StatusCode(500, new ApiResponse<List<ProductPriceListModel>>(null, false, "Ürün fiyat listesi getirilirken bir hata oluştu.", "InternalServerError"));
            }
        }
    }
}
