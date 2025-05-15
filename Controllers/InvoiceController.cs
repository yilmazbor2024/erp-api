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
        /// Toptan satış faturalarını listeler
        /// </summary>
        [HttpGet("wholesale")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetWholesaleInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
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
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetWholesalePurchaseInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
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
        /// Masraf alış faturalarını listeler
        /// </summary>
        [HttpGet("expense")]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetExpenseInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
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
        /// Tüm faturaları listeler
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<InvoiceListResult>>> GetAllInvoices([FromQuery] InvoiceListRequest request)
        {
            try
            {
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
                _logger.LogError(ex, "Faturalar getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Faturalar getirilirken bir hata oluştu: " + ex.Message
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
