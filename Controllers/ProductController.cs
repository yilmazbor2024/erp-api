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
        public async Task<ActionResult<ApiResponse<List<ProductVariantModel>>>> GetProducts(
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
                if (pageNumber < 1)
                {
                    return BadRequest(new ApiResponse<List<ProductVariantModel>>(null, false, "Sayfa numarası 1'den küçük olamaz", "BadRequest"));
                }

                if (pageSize < 1 || pageSize > 1000)
                {
                    return BadRequest(new ApiResponse<List<ProductVariantModel>>(null, false, "Sayfa boyutu 1 ile 1000 arasında olmalıdır", "BadRequest"));
                }

                // Not: Şu anda repository'de tüm ürünleri getiren bir metot yok.
                // Geçici olarak, barkod bazlı arama metodunu kullanacağız.
                // İlerleyen aşamalarda, tüm ürünleri getiren ve filtreleme yapan bir repository metodu eklenmelidir.
                
                // Örnek bir barkod kullanarak ürünleri getiriyoruz (bu ideal değil)
                var products = await _productRepository.GetProductVariantsByBarcodeAsync(productCode ?? "*");
                
                // Filtreleme işlemleri
                if (!string.IsNullOrEmpty(productCode))
                {
                    products = products.Where(p => p.ProductCode.Contains(productCode, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                
                if (!string.IsNullOrEmpty(productDescription))
                {
                    products = products.Where(p => p.ProductDescription != null && p.ProductDescription.Contains(productDescription, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                
                if (!string.IsNullOrEmpty(productTypeCode))
                {
                    products = products.Where(p => p.ProductTypeCode == productTypeCode).ToList();
                }
                
                if (isBlocked.HasValue)
                {
                    products = products.Where(p => p.IsBlocked == isBlocked.Value).ToList();
                }
                
                // Sıralama işlemleri
                if (!string.IsNullOrEmpty(sortBy))
                {
                    // Sıralama alanına göre sıralama yap
                    switch (sortBy.ToLower())
                    {
                        case "productcode":
                            products = sortDirection.ToLower() == "desc" ? 
                                products.OrderByDescending(p => p.ProductCode).ToList() : 
                                products.OrderBy(p => p.ProductCode).ToList();
                            break;
                        case "productdescription":
                            products = sortDirection.ToLower() == "desc" ? 
                                products.OrderByDescending(p => p.ProductDescription).ToList() : 
                                products.OrderBy(p => p.ProductDescription).ToList();
                            break;
                        case "producttypecode":
                            products = sortDirection.ToLower() == "desc" ? 
                                products.OrderByDescending(p => p.ProductTypeCode).ToList() : 
                                products.OrderBy(p => p.ProductTypeCode).ToList();
                            break;
                        default:
                            products = products.OrderBy(p => p.ProductCode).ToList();
                            break;
                    }
                }
                
                // Sayfalama işlemleri
                var totalCount = products.Count;
                var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
                var pagedProducts = products
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                
                if (pagedProducts.Count == 0)
                {
                    return NotFound(new ApiResponse<List<ProductVariantModel>>(null, false, "Belirtilen kriterlere uygun ürün bulunamadı", "NotFound"));
                }
                
                // Yanıt oluştur
                var response = new ApiResponse<List<ProductVariantModel>>
                {
                    Data = pagedProducts,
                    Success = true,
                    Message = $"Toplam {totalCount} üründen {pagedProducts.Count} tanesi başarıyla getirildi. Sayfa: {pageNumber}/{pageCount}"
                };
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürünler getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<List<ProductVariantModel>>(null, false, "Ürünler getirilirken bir hata oluştu.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Gets a product by its code
        /// </summary>
        /// <param name="productCode">The product code</param>
        /// <returns>The product details</returns>
        [HttpGet("{productCode}")]
        public async Task<ActionResult<ApiResponse<List<ProductVariantModel>>>> GetProductByCode(string productCode)
        {
            try
            {
                if (string.IsNullOrEmpty(productCode))
                {
                    return BadRequest(new ApiResponse<List<ProductVariantModel>>(null, false, "Ürün kodu boş olamaz", "BadRequest"));
                }

                // Ürün varyantlarını getirmek için repository metodunu çağır
                // Not: Burada barkod yerine ürün kodu kullanıyoruz, bu nedenle repository'de yeni bir metot eklenmesi gerekebilir
                // Şimdilik mevcut metodu kullanacağız
                var variants = await _productRepository.GetProductVariantsByBarcodeAsync(productCode);

                if (variants == null || variants.Count == 0)
                {
                    return NotFound(new ApiResponse<List<ProductVariantModel>>(null, false, $"{productCode} kodlu ürün bulunamadı", "NotFound"));
                }

                return Ok(new ApiResponse<List<ProductVariantModel>>(variants, true, "Ürün başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün kodu ile ürün aranırken hata oluştu. Ürün Kodu: {ProductCode}", productCode);
                return StatusCode(500, new ApiResponse<List<ProductVariantModel>>(null, false, "Ürün getirilirken bir hata oluştu.", "InternalServerError"));
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
        /// Ürün tiplerini getirir
        /// </summary>
        /// <returns>Ürün tipleri listesi</returns>
        [HttpGet("types")]
        public async Task<ActionResult<ApiResponse<List<ProductTypeModel>>>> GetProductTypes()
        {
            try
            {
                // Repository'den ürün tiplerini getir
                var types = await _productRepository.GetProductTypesAsync();

                if (types == null || types.Count == 0)
                {
                    return NotFound(new ApiResponse<List<ProductTypeModel>>(null, false, "Ürün tipi bulunamadı", "NotFound"));
                }

                return Ok(new ApiResponse<List<ProductTypeModel>>(types, true, "Ürün tipleri başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün tipleri getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<List<ProductTypeModel>>(null, false, "Ürün tipleri getirilirken bir hata oluştu.", "InternalServerError"));
            }
        }

        /// <summary>
        /// Ölçü birimlerini getirir
        /// </summary>
        /// <returns>Ölçü birimleri listesi</returns>
        [HttpGet("units-of-measure")]
        public async Task<ActionResult<ApiResponse<List<UnitOfMeasureModel>>>> GetUnitsOfMeasure()
        {
            try
            {
                // Repository'den ölçü birimlerini getir
                var units = await _productRepository.GetUnitsOfMeasureAsync();

                if (units == null || units.Count == 0)
                {
                    return NotFound(new ApiResponse<List<UnitOfMeasureModel>>(null, false, "Ölçü birimi bulunamadı", "NotFound"));
                }

                return Ok(new ApiResponse<List<UnitOfMeasureModel>>(units, true, "Ölçü birimleri başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ölçü birimleri getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<List<UnitOfMeasureModel>>(null, false, "Ölçü birimleri getirilirken bir hata oluştu.", "InternalServerError"));
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
        public async Task<ActionResult<ApiResponse<List<ProductPriceListModel>>>> Get(string productCode)
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

        /// <summary>
        /// Tüm ürün fiyat listesini getirir
        /// </summary>
        /// <param name="page">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
        /// <param name="startDate">Başlangıç tarihi (yyyy-MM-dd formatında)</param>
        /// <param name="endDate">Bitiş tarihi (yyyy-MM-dd formatında)</param>
        /// <param name="companyCode">Şirket kodu</param>
        /// <returns>Ürün fiyat listesi detayları</returns>
        [HttpGet("all-price-lists")]
        public async Task<ActionResult<ApiResponse<List<ProductPriceListDetailModel>>>> GetAllPriceList(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] string startDate = null,
            [FromQuery] string endDate = null,
            [FromQuery] int companyCode = 1)
        {
            try
            {
                if (page < 1)
                {
                    return BadRequest(new ApiResponse<List<ProductPriceListDetailModel>>(null, false, "Sayfa numarası 1'den küçük olamaz", "BadRequest"));
                }

                if (pageSize < 1 || pageSize > 1000)
                {
                    return BadRequest(new ApiResponse<List<ProductPriceListDetailModel>>(null, false, "Sayfa boyutu 1 ile 1000 arasında olmalıdır", "BadRequest"));
                }

                // Tarih parametrelerini işleme
                DateTime? parsedStartDate = null;
                DateTime? parsedEndDate = null;

                if (!string.IsNullOrEmpty(startDate))
                {
                    if (DateTime.TryParse(startDate, out DateTime startDateValue))
                    {
                        parsedStartDate = startDateValue;
                    }
                    else
                    {
                        return BadRequest(new ApiResponse<List<ProductPriceListDetailModel>>(null, false, "Başlangıç tarihi geçerli bir tarih formatında olmalıdır (yyyy-MM-dd)", "BadRequest"));
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    if (DateTime.TryParse(endDate, out DateTime endDateValue))
                    {
                        parsedEndDate = endDateValue;
                    }
                    else
                    {
                        return BadRequest(new ApiResponse<List<ProductPriceListDetailModel>>(null, false, "Bitiş tarihi geçerli bir tarih formatında olmalıdır (yyyy-MM-dd)", "BadRequest"));
                    }
                }

                _logger.LogInformation("Fiyat listesi getiriliyor. Sayfa: {Page}, Sayfa Boyutu: {PageSize}, Başlangıç Tarihi: {StartDate}, Bitiş Tarihi: {EndDate}, Şirket Kodu: {CompanyCode}", 
                    page, pageSize, parsedStartDate, parsedEndDate, companyCode);
                
                var priceList = await _productRepository.GetAllProductPriceListAsync(page, pageSize, parsedStartDate, parsedEndDate, companyCode);

                if (priceList == null || priceList.Count == 0)
                {
                    _logger.LogWarning("Belirtilen kriterlere uygun fiyat listesi bulunamadı. Sayfa: {Page}, Sayfa Boyutu: {PageSize}, Başlangıç Tarihi: {StartDate}, Bitiş Tarihi: {EndDate}, Şirket Kodu: {CompanyCode}", 
                        page, pageSize, parsedStartDate, parsedEndDate, companyCode);
                    return NotFound(new ApiResponse<List<ProductPriceListDetailModel>>(null, false, "Belirtilen kriterlere uygun fiyat listesi bulunamadı", "NotFound"));
                }

                return Ok(new ApiResponse<List<ProductPriceListDetailModel>>(priceList, true, $"Toplam {priceList.Count} adet fiyat listesi kaydı başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm ürün fiyat listesi getirilirken hata oluştu. Sayfa: {Page}, Sayfa Boyutu: {PageSize}", page, pageSize);
                return StatusCode(500, new ApiResponse<List<ProductPriceListDetailModel>>(null, false, "Ürün fiyat listesi getirilirken bir hata oluştu.", "InternalServerError"));
            }
        }
    }
}
