using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Invoice;
using ErpMobile.Api.Services.Invoice;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Route("api/v1/[controller]s")]
    [Route("api/[controller]s")]
    [EnableCors]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoiceService _invoiceService;
        private readonly IConfiguration _configuration;

        public InvoiceController(
            ILogger<InvoiceController> logger,
            IInvoiceService invoiceService,
            IConfiguration configuration)
        {
            _logger = logger;
            _invoiceService = invoiceService;
            _configuration = configuration;
        }

        /// <summary>
        /// Fatura numarası oluşturur
        /// </summary>
        [HttpGet("generate-number")]
        public async Task<ActionResult<ApiResponse<string>>> GenerateInvoiceNumber([FromQuery] string processCode)
        {
            try
            {
                // Process kodu kontrolü
                if (string.IsNullOrEmpty(processCode))
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Process kodu boş olamaz"
                    });
                }
                
                var response = await _invoiceService.GenerateInvoiceNumberAsync(processCode);
                
                if (!response.Success)
                {
                    return StatusCode(500, response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura numarası oluşturulurken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Fatura numarası oluşturulurken bir hata oluştu: " + ex.Message
                });
            }
        }
        
        /// <summary>
        /// Toptan satış faturalarını listeler
        /// </summary>
        [HttpGet("wholesale")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetWholesaleInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }
                
                // Toptan satış faturaları için ProcessCode'u otomatik olarak ayarla
                request.ProcessCode = "WS"; // Toptan Satış olarak ayarla
                
                // Diğer alanları varsayılan değerlerle doldur
                request.StoreCode = request.StoreCode ?? "";
                request.VendorCode = request.VendorCode ?? "";
                request.CompanyCode = request.CompanyCode ?? "";
                request.CustomerCode = request.CustomerCode ?? "";
                request.InvoiceNumber = request.InvoiceNumber ?? "";
                request.WarehouseCode = request.WarehouseCode ?? "";
            
                var response = await _invoiceService.GetWholesaleInvoicesAsync(request);
                
                if (!response.Success)
                {
                    return StatusCode(500, response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan satış faturaları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Toptan satış faturaları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Toptan satış faturası detaylarını getirir
        /// </summary>
        [HttpGet("wholesale/{id}")]
        public async Task<ActionResult<ApiResponse<InvoiceHeaderModel>>> GetWholesaleInvoiceById(int id)
        {
            try
            {
                var response = await _invoiceService.GetWholesaleInvoiceByIdAsync(id);
                
                if (!response.Success)
                {
                    return NotFound(response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Toptan satış faturası getirilirken hata oluştu. ID: {id}");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Toptan satış faturası getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Yeni bir toptan satış faturası oluşturur
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<InvoiceHeaderModel>>> CreateWholesaleInvoice([FromBody] CreateInvoiceRequest request)
        {
            try
            {
                var response = await _invoiceService.CreateWholesaleInvoiceAsync(request);
                
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan satış faturası oluşturulurken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Toptan satış faturası oluşturulurken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Toptan alış faturalarını listeler
        /// </summary>
        [HttpGet("wholesale-purchase")]
        [HttpGet("purchase")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetWholesalePurchaseInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }

                // ProcessCode'u otomatik olarak "BP" (Toptan Alış) olarak ayarla
                request.ProcessCode = "BP";
                
                // Diğer alanları varsayılan değerlerle doldur
                request.StoreCode = request.StoreCode ?? "";
                request.VendorCode = request.VendorCode ?? "";
                request.CompanyCode = request.CompanyCode ?? "";
                request.CustomerCode = request.CustomerCode ?? "";
                request.InvoiceNumber = request.InvoiceNumber ?? "";
                request.WarehouseCode = request.WarehouseCode ?? "";
           
                var response = await _invoiceService.GetWholesalePurchaseInvoicesAsync(request);
                
                if (!response.Success)
                {
                    return StatusCode(500, response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan alış faturaları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Toptan alış faturaları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Toptan alış faturası detaylarını getirir
        /// </summary>
        [HttpGet("purchase/{id}")]
        public async Task<ActionResult<ApiResponse<InvoiceHeaderModel>>> GetWholesalePurchaseInvoiceById(int id)
        {
            try
            {
                var response = await _invoiceService.GetWholesalePurchaseInvoiceByIdAsync(id);
                
                if (!response.Success)
                {
                    return NotFound(response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Toptan alış faturası getirilirken hata oluştu. ID: {id}");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Toptan alış faturası getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Yeni bir toptan alış faturası oluşturur
        /// </summary>
        [HttpPost("purchase")]
        public async Task<ActionResult<ApiResponse<InvoiceHeaderModel>>> CreateWholesalePurchaseInvoice([FromBody] CreateInvoiceRequest request)
        {
            try
            {
                var response = await _invoiceService.CreateWholesalePurchaseInvoiceAsync(request);
                
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan alış faturası oluşturulurken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Toptan alış faturası oluşturulurken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Masraf faturalarını listeler
        /// </summary>
        [HttpGet("expense")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetExpenseInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }

                // ProcessCode'u otomatik olarak "EP" (Masraf Alış) olarak ayarla
                request.ProcessCode = "EP";
                
                // Diğer alanları varsayılan değerlerle doldur
                request.StoreCode = request.StoreCode ?? "001";
                request.VendorCode = request.VendorCode ?? "";
                request.CompanyCode = request.CompanyCode ?? "001";
                request.CustomerCode = request.CustomerCode ?? "";
                request.InvoiceNumber = request.InvoiceNumber ?? "";
                request.WarehouseCode = request.WarehouseCode ?? "001";
              

                var response = await _invoiceService.GetExpenseInvoicesAsync(request);
                
                if (!response.Success)
                {
                    return StatusCode(500, response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Masraf faturaları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Masraf faturaları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Masraf faturası detaylarını getirir
        /// </summary>
        [HttpGet("expense/{id}")]
        public async Task<ActionResult<ApiResponse<InvoiceHeaderModel>>> GetExpenseInvoiceById(int id)
        {
            try
            {
                var response = await _invoiceService.GetExpenseInvoiceByIdAsync(id);
                
                if (!response.Success)
                {
                    return NotFound(response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Masraf faturası getirilirken hata oluştu. ID: {id}");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Masraf faturası getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Yeni bir masraf faturası oluşturur
        /// </summary>
        [HttpPost("expense")]
        public async Task<ActionResult<ApiResponse<InvoiceHeaderModel>>> CreateExpenseInvoice([FromBody] CreateInvoiceRequest request)
        {
            try
            {
                var response = await _invoiceService.CreateExpenseInvoiceAsync(request);
                
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Masraf faturası oluşturulurken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Masraf faturası oluşturulurken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Fatura detaylarını getirir
        /// </summary>
        [HttpGet("{id}/details")]
        public async Task<ActionResult<ApiResponse<List<InvoiceDetailModel>>>> GetInvoiceDetails(int id)
        {
            try
            {
                var response = await _invoiceService.GetInvoiceDetailsAsync(id);
                
                if (!response.Success)
                {
                    return NotFound(response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura detayları getirilirken hata oluştu. ID: {id}");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Fatura detayları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Fatura ödeme detaylarını getirir
        /// </summary>
        [HttpGet("{id}/payment-details")]
        public async Task<ActionResult<ApiResponse<List<InvoicePaymentDetailModel>>>> GetInvoicePaymentDetails(string id)
        {
            try
            {
                var response = await _invoiceService.GetInvoicePaymentDetailsAsync(id);
                
                if (!response.Success)
                {
                    return NotFound(response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura ödeme detayları getirilirken hata oluştu. ID: {id}");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Fatura ödeme detayları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Sipariş bazlı faturaları listeler - ProcessCode kullanılır, diğer parametreler opsiyoneldir
        /// </summary>
        [HttpGet("order-based")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetOrderBasedInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }
                
                // Varsayılan değerleri ayarla
                request.Page = request.Page <= 0 ? 1 : request.Page;
                request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;
                request.LangCode = request.LangCode ?? "TR";
                
                // Sipariş bazlı faturaları getir
                var result = await _invoiceService.GetOrderBasedInvoicesAsync(request);
                
                return Ok(new ApiResponse<InvoiceListResult>
                {
                    Success = true,
                    Message = result.items.Count > 0 ? $"{result.totalCount} adet sipariş bazlı fatura bulundu" : "Sipariş bazlı fatura bulunamadı",
                    Data = new InvoiceListResult
                    {
                        Items = result.items,
                        TotalCount = result.totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş bazlı faturalar getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Sipariş bazlı faturalar getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }
        
        /// <summary>
        /// İrsaliye bazlı faturaları listeler - ProcessCode kullanılır, diğer parametreler opsiyoneldir
        /// </summary>
        [HttpGet("shipment-based")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetShipmentBasedInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }
                
                // Varsayılan değerleri ayarla
                request.Page = request.Page <= 0 ? 1 : request.Page;
                request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;
                request.LangCode = request.LangCode ?? "TR";
                
                // İrsaliye bazlı faturaları getir
                var result = await _invoiceService.GetShipmentBasedInvoicesAsync(request);
                
                return Ok(new ApiResponse<InvoiceListResult>
                {
                    Success = true,
                    Message = result.items.Count > 0 ? $"{result.totalCount} adet irsaliye bazlı fatura bulundu" : "İrsaliye bazlı fatura bulunamadı",
                    Data = new InvoiceListResult
                    {
                        Items = result.items,
                        TotalCount = result.totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İrsaliye bazlı faturalar getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "İrsaliye bazlı faturalar getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }
        
        /// <summary>
        /// Direkt toptan satış faturalarını listeler - 
        /// </summary>
        [HttpGet("direct-sales")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetDirectSalesInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }
                
                // Varsayılan değerleri ayarla
                request.Page = request.Page <= 0 ? 1 : request.Page;
                request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;
                request.LangCode = request.LangCode ?? "TR";
                request.ProcessCode = "WS"; // Toptan Satış
                
                // Direkt satış faturalarını getir
                var result = await _invoiceService.GetDirectSalesInvoicesAsync(request);
                
                return Ok(new ApiResponse<InvoiceListResult>
                {
                    Success = true,
                    Message = result.items.Count > 0 ? $"{result.totalCount} adet direkt satış faturası bulundu" : "Direkt satış faturası bulunamadı",
                    Data = new InvoiceListResult
                    {
                        Items = result.items,
                        TotalCount = result.totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Direkt satış faturaları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Direkt satış faturaları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Sipariş bazlı alış faturalarını listeler
        /// </summary>
        [HttpGet("order-based-purchase")]
        public async Task<ActionResult<List<InvoiceHeaderModel>>> GetOrderBasedPurchaseInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }
                
                // Varsayılan değerleri ayarla
                request.StoreCode = request.StoreCode ?? "001";
                request.VendorCode = request.VendorCode ?? "";
                request.CompanyCode = request.CompanyCode ?? "1";
                request.InvoiceNumber = request.InvoiceNumber ?? "";
                request.WarehouseCode = request.WarehouseCode ?? "001";
            
                var (items, totalCount) = await _invoiceService.GetOrderBasedPurchaseInvoicesAsync(request);
                
                if (items == null || items.Count == 0)
                {
                    return Ok(new List<InvoiceHeaderModel>());
                }
                
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş bazlı alış faturaları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Sipariş bazlı alış faturaları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }
        
        /// <summary>
        /// İrsaliye bazlı alış faturalarını listeler
        /// </summary>
        [HttpGet("shipment-based-purchase")]
        public async Task<ActionResult<List<InvoiceHeaderModel>>> GetShipmentBasedPurchaseInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }
                
                // Varsayılan değerleri ayarla
                request.StoreCode = request.StoreCode ?? "001";
                request.VendorCode = request.VendorCode ?? "";
                request.CompanyCode = request.CompanyCode ?? "1";
                request.InvoiceNumber = request.InvoiceNumber ?? "";
                request.WarehouseCode = request.WarehouseCode ?? "001";
           
                var (items, totalCount) = await _invoiceService.GetShipmentBasedPurchaseInvoicesAsync(request);
                
                if (items == null || items.Count == 0)
                {
                    return Ok(new List<InvoiceHeaderModel>());
                }
                
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İrsaliye bazlı alış faturaları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "İrsaliye bazlı alış faturaları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Direkt toptan alış faturalarını listeler
        /// </summary>
        [HttpGet("direct-wholesale-purchase")]
        public async Task<ActionResult<List<InvoiceHeaderModel>>> GetDirectWholesalePurchaseInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }
                
                // Varsayılan değerleri ayarla
                request.StoreCode = request.StoreCode ?? "001";
                request.VendorCode = request.VendorCode ?? "";
                request.CompanyCode = request.CompanyCode ?? "1";
                request.InvoiceNumber = request.InvoiceNumber ?? "";
                request.WarehouseCode = request.WarehouseCode ?? "001";
     
                var (items, totalCount) = await _invoiceService.GetDirectWholesalePurchaseInvoicesAsync(request);
                
                if (items == null || items.Count == 0)
                {
                    return Ok(new List<InvoiceHeaderModel>());
                }
                
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Direkt toptan alış faturaları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Direkt toptan alış faturaları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Tüm faturaları listeler - ProcessCode kullanılır, diğer parametreler opsiyoneldir
        /// </summary>
        [HttpGet]
        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetAllInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();
                
                // Null kontrolü
                if (request == null)
                {
                    request = new InvoiceListRequest();
                }

                // ProcessCode kullanılıyor,  
                // ProcessCode yoksa uyarı ver ama zorunlu tutma
                if (string.IsNullOrEmpty(request.ProcessCode))
                {
                    _logger.LogWarning("ProcessCode belirtilmemiş, tüm faturalar listelenecek");
                }
                
                // Diğer alanları varsayılan değerlerle doldur
                request.StoreCode = request.StoreCode ?? "001";
                request.VendorCode = request.VendorCode ?? "";
                request.CompanyCode = request.CompanyCode ?? "001";
                request.CustomerCode = request.CustomerCode ?? "";
                request.InvoiceNumber = request.InvoiceNumber ?? "";
                request.WarehouseCode = request.WarehouseCode ?? "001";
        
                // Model durumunu temizle - tüm doğrulama hatalarını görmezden gel
                ModelState.Clear();

                // Gelen isteğin parametrelerini logla
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n===== GetAllInvoices REQUEST PARAMETERS =====\n");
                Console.WriteLine($"Page/PageNumber: {request.Page}");
                Console.WriteLine($"PageSize: {request.PageSize}");
                Console.WriteLine($"SortBy: {request.SortBy}");
                Console.WriteLine($"SortDirection: {request.SortDirection}");
                Console.WriteLine($"InvoiceNumber: {request.InvoiceNumber}");
                Console.WriteLine($"ProcessCode: {request.ProcessCode}");
                Console.WriteLine($"CustomerCode: {request.CustomerCode}");
                Console.WriteLine($"VendorCode: {request.VendorCode}");
                Console.WriteLine($"StartDate: {request.StartDate}");
                Console.WriteLine($"EndDate: {request.EndDate}");
                Console.WriteLine($"CompanyCode: {request.CompanyCode}");
                Console.WriteLine($"StoreCode: {request.StoreCode}");
                Console.WriteLine($"WarehouseCode: {request.WarehouseCode}");
              
                Console.ResetColor();
                
                // Doğrudan tüm faturaları getiren servisi çağır
                var response = await _invoiceService.GetAllInvoicesAsync(request);
                
                if (!response.Success)
                {
                    return StatusCode(500, response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Faturalar getirilirken hata oluştu: {Message}", ex.Message);
                
                // Hata mesajını daha detaylı hale getir
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " - " + ex.InnerException.Message;
                }
                
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Faturalar getirilirken bir hata oluştu: " + errorMessage
                });
            }
        }

        /// <summary>
        /// Test amaçlı fatura tablolarını inceler
        /// </summary>
        [HttpGet("test-tables")]
        public async Task<ActionResult<object>> TestInvoiceTables()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("ErpConnection");
                var result = new Dictionary<string, object>();
                
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Fatura başlık tablosunu kontrol et
                    var invoiceHeaderQuery = "SELECT TOP 10 * FROM trInvoiceHeader";
                    var invoiceHeaders = new List<Dictionary<string, object>>();
                    
                    using (var command = new SqlCommand(invoiceHeaderQuery, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            invoiceHeaders.Add(row);
                        }
                    }
                    
                    result["trInvoiceHeader"] = invoiceHeaders;
                    
                    // Fatura detay tablosunu kontrol et
                    var invoiceDetailQuery = "SELECT TOP 10 * FROM trInvoiceDetail";
                    var invoiceDetails = new List<Dictionary<string, object>>();
                    
                    using (var command = new SqlCommand(invoiceDetailQuery, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            invoiceDetails.Add(row);
                        }
                    }
                    
                    result["trInvoiceDetail"] = invoiceDetails;
                    
                    // Fatura tipi tablosunu kontrol et
                    var invoiceTypeQuery = "SELECT * FROM bsInvoiceType";
                    var invoiceTypes = new List<Dictionary<string, object>>();
                    
                    using (var command = new SqlCommand(invoiceTypeQuery, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            invoiceTypes.Add(row);
                        }
                    }
                    
                    result["bsInvoiceType"] = invoiceTypes;
                    
                    // Masraf tipi tablosunu kontrol et
                    var expenseTypeQuery = "SELECT * FROM cdExpenseType";
                    var expenseTypes = new List<Dictionary<string, object>>();
                    
                    using (var command = new SqlCommand(expenseTypeQuery, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            expenseTypes.Add(row);
                        }
                    }
                    
                    result["cdExpenseType"] = expenseTypes;
                }
                
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Fatura tabloları başarıyla getirildi",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura tabloları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Fatura tabloları getirilirken bir hata oluştu: " + ex.Message
                });
            }
        }
    }
}
