using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Route("api/v1/Vendor")]
    public class VendorBasicController : ControllerBase
    {
        private readonly ILogger<VendorBasicController> _logger;
        private readonly ICustomerService _customerService;
        private readonly ICustomerServiceNew _customerServiceNew;
        private readonly IConfiguration _configuration;

        public VendorBasicController(
            ILogger<VendorBasicController> logger,
            ICustomerService customerService,
            ICustomerServiceNew customerServiceNew,
            IConfiguration configuration)
        {
            _logger = logger;
            _customerService = customerService;
            _customerServiceNew = customerServiceNew;
            _configuration = configuration;
        }

        /// <summary>
        /// Tedarikçi listesini getirir
        /// </summary>
        /// <param name="filter">Filtreleme parametreleri</param>
        /// <returns>Tedarikçi listesi</returns>
        [HttpGet]
        [Route("")]
        [Route("list")]
        public async Task<ActionResult<ApiResponse<PagedResponse<CustomerListResponse>>>> GetVendors([FromQuery] CustomerFilterRequest filter)
        {
            try
            {
                _logger.LogInformation("Tedarikçi listesi getiriliyor");

                // Sayfa parametrelerini kontrol et
                if (filter == null)
                {
                    filter = new CustomerFilterRequest
                    {
                        PageNumber = 1,
                        PageSize = 20
                    };
                }

                // CurrAccTypeCode = 1 olan kayıtları getir (Tedarikçiler)
                filter.CurrAccTypeCode = 1;
                
                // Filter değerlerini logla
                _logger.LogInformation("Tedarikçi listesi için filtre değerleri: CurrAccTypeCode={CurrAccTypeCode}, PageNumber={PageNumber}, PageSize={PageSize}", 
                    filter.CurrAccTypeCode, filter.PageNumber, filter.PageSize);
                
                var vendors = await _customerService.GetCustomerListAsync(filter);
                
                // Sonuçları logla
                _logger.LogInformation("Tedarikçi listesi sonuçları: TotalCount={TotalCount}, TotalPages={TotalPages}, Items={ItemsCount}", 
                    vendors.TotalCount, vendors.TotalPages, vendors.Items?.Count ?? 0);
                
                if (vendors.Items?.Count == 0)
                {
                    _logger.LogWarning("Tedarikçi listesi boş döndü. Filtre değerleri: CurrAccTypeCode={CurrAccTypeCode}", filter.CurrAccTypeCode);
                }
                
                return Ok(new ApiResponse<PagedResponse<CustomerListResponse>>
                {
                    Success = true,
                    Message = "Tedarikçi listesi başarıyla getirildi",
                    Data = vendors
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi listesi getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PagedResponse<CustomerListResponse>>
                {
                    Success = false,
                    Message = "Tedarikçi listesi getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Tedarikçi detaylarını getirir
        /// </summary>
        /// <param name="vendorCode">Tedarikçi kodu</param>
        /// <returns>Tedarikçi detayları</returns>
        [HttpGet("{vendorCode}")]
        public async Task<ActionResult<ApiResponse<CustomerDetailResponse>>> GetVendorByCode(string vendorCode)
        {
            try
            {
                _logger.LogInformation("Tedarikçi detayları getiriliyor: {VendorCode}", vendorCode);

                var vendor = await _customerService.GetCustomerByCodeAsync(vendorCode, 1); // CurrAccTypeCode = 1 (Tedarikçi)

                if (vendor == null)
                {
                    return NotFound(new ApiResponse<CustomerDetailResponse>
                    {
                        Success = false,
                        Message = $"Tedarikçi bulunamadı: {vendorCode}"
                    });
                }

                return Ok(new ApiResponse<CustomerDetailResponse>
                {
                    Success = true,
                    Message = "Tedarikçi detayları başarıyla getirildi",
                    Data = vendor
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi detayları getirilirken hata oluştu. VendorCode: {VendorCode}", vendorCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerDetailResponse>
                {
                    Success = false,
                    Message = "Tedarikçi detayları getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Yeni tedarikçi oluşturur
        /// </summary>
        /// <param name="request">Tedarikçi oluşturma isteği</param>
        /// <returns>Oluşturulan tedarikçi bilgileri</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CustomerCreateResponseNew>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CustomerCreateResponseNew>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateVendor([FromBody] CustomerCreateRequestNew request)
        {
            try
            {
                _logger.LogInformation("Tedarikçi oluşturma isteği alındı");
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Geçersiz tedarikçi bilgileri",
                        Data = "Lütfen tüm zorunlu alanları doldurun."
                    });
                }
                
                // Tedarikçi tipi olarak ayarla (CustomerTypeCode = 1)
                request.CustomerTypeCode = 1;
                
                var result = await _customerServiceNew.CreateCustomerAsync(request);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<CustomerCreateResponseNew>
                    {
                        Success = true,
                        Message = "Tedarikçi başarıyla oluşturuldu",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<CustomerCreateResponseNew>
                    {
                        Success = false,
                        Message = "Tedarikçi oluşturma işlemi başarısız",
                        Data = result
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi oluşturma sırasında hata: {ErrorMessage}", ex.Message);
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Tedarikçi oluşturma sırasında bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Tedarikçi güncelleme
        /// </summary>
        /// <param name="request">Tedarikçi güncelleme isteği</param>
        /// <returns>Tedarikçi güncelleme yanıtı</returns>
        [HttpPut("{vendorCode}")]
        [ProducesResponseType(typeof(ApiResponse<CustomerUpdateResponseNew>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CustomerUpdateResponseNew>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVendor(string vendorCode, [FromBody] CustomerUpdateRequestNew request)
        {
            try
            {
                _logger.LogInformation("Tedarikçi güncelleme isteği alındı: {VendorCode}", vendorCode);
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Geçersiz tedarikçi bilgileri",
                        Data = "Lütfen tüm zorunlu alanları doldurun."
                    });
                }
                
                // Tedarikçi kodu ve tipini ayarla
                request.CustomerCode = vendorCode;
                request.CustomerTypeCode = 1; // Tedarikçi tipi
                
                var result = await _customerServiceNew.UpdateCustomerAsync(request);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<CustomerUpdateResponseNew>
                    {
                        Success = true,
                        Message = "Tedarikçi başarıyla güncellendi",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<CustomerUpdateResponseNew>
                    {
                        Success = false,
                        Message = "Tedarikçi güncelleme işlemi başarısız",
                        Data = result
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi güncelleme sırasında hata: {ErrorMessage}", ex.Message);
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Tedarikçi güncelleme sırasında bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
    }
}
