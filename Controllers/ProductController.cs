using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
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
    }
}
