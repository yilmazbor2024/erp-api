using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Common;
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
        private readonly IConfiguration _configuration;

        public VendorBasicController(
            ILogger<VendorBasicController> logger,
            ICustomerService customerService,
            IConfiguration configuration)
        {
            _logger = logger;
            _customerService = customerService;
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
                
                var vendors = await _customerService.GetCustomerListAsync(filter);
                
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
    }
}
