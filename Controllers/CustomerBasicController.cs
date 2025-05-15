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
    public class CustomerBasicController : ControllerBase
    {
        private readonly ILogger<CustomerBasicController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;

        public CustomerBasicController(
            ILogger<CustomerBasicController> logger,
            ICustomerService customerService,
            IConfiguration configuration)
        {
            _logger = logger;
            _customerService = customerService;
            _configuration = configuration;
        }

        /// <summary>
        /// Müşteri listesini getirir
        /// </summary>
        /// <param name="filter">Filtreleme parametreleri</param>
        /// <returns>Müşteri listesi</returns>
        [HttpGet]
        [Route("")]
        [Route("customers")]
        public async Task<ActionResult<ApiResponse<PagedResponse<CustomerListResponse>>>> GetCustomers([FromQuery] CustomerFilterRequest filter)
        {
            try
            {
                _logger.LogInformation("Müşteri listesi getiriliyor");

                // Sayfa parametrelerini kontrol et
                if (filter == null)
                {
                    filter = new CustomerFilterRequest
                    {
                        PageNumber = 1,
                        PageSize = 20
                    };
                }

                // CurrAccTypeCode = 3 olan kayıtları getir (Müşteriler)
                filter.CurrAccTypeCode = 3;
                
                var customers = await _customerService.GetCustomerListAsync(filter);
                
                return Ok(new ApiResponse<PagedResponse<CustomerListResponse>>
                {
                    Success = true,
                    Message = "Müşteri listesi başarıyla getirildi",
                    Data = customers
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri listesi getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PagedResponse<CustomerListResponse>>
                {
                    Success = false,
                    Message = "Müşteri listesi getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri detaylarını getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri detayları</returns>
        [HttpGet("{customerCode}")]
        public async Task<ActionResult<ApiResponse<CustomerDetailResponse>>> GetCustomerByCode(string customerCode)
        {
            try
            {
                _logger.LogInformation("Müşteri detayları getiriliyor: {CustomerCode}", customerCode);

                var customer = await _customerService.GetCustomerByCodeAsync(customerCode, 3); // CurrAccTypeCode = 3 (Müşteri)

                if (customer == null)
                {
                    return NotFound(new ApiResponse<CustomerDetailResponse>
                    {
                        Success = false,
                        Message = $"Müşteri bulunamadı: {customerCode}"
                    });
                }

                return Ok(new ApiResponse<CustomerDetailResponse>
                {
                    Success = true,
                    Message = "Müşteri detayları başarıyla getirildi",
                    Data = customer
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri detayları getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerDetailResponse>
                {
                    Success = false,
                    Message = "Müşteri detayları getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
        


        /// <summary>
        /// Müşteri tiplerini getirir
        /// </summary>
        [HttpGet("customer-types")]
        public async Task<ActionResult<ApiResponse<List<CustomerTypeResponse>>>> GetCustomerTypes()
        {
            try
            {
                _logger.LogInformation("Müşteri tipleri getiriliyor");

                var customerTypes = await _customerService.GetCustomerTypesAsync();

                return Ok(new ApiResponse<List<CustomerTypeResponse>>
                {
                    Success = true,
                    Message = "Müşteri tipleri başarıyla getirildi",
                    Data = customerTypes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri tipleri getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<CustomerTypeResponse>>
                {
                    Success = false,
                    Message = "Müşteri tipleri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Sadece tedarikçileri getirir (CurrAccTypeCode = 1)
        /// </summary>
        /// <param name="filter">Filtreleme parametreleri</param>
        /// <returns>Tedarikçi listesi</returns>
        [HttpGet]
        [Route("vendors")]
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
        /// Sadece müşterileri getirir (CurrAccTypeCode = 3)
        /// </summary>
        /// <param name="filter">Filtreleme parametreleri</param>
        /// <returns>Müşteri listesi</returns>
        [HttpGet]
        [Route("customers-only")]
        public async Task<ActionResult<ApiResponse<PagedResponse<CustomerListResponse>>>> GetCustomersOnly([FromQuery] CustomerFilterRequest filter)
        {
            try
            {
                _logger.LogInformation("Sadece müşteri listesi getiriliyor");

                // Sayfa parametrelerini kontrol et
                if (filter == null)
                {
                    filter = new CustomerFilterRequest
                    {
                        PageNumber = 1,
                        PageSize = 20
                    };
                }

                // CurrAccTypeCode = 3 olan kayıtları getir (Müşteriler)
                filter.CurrAccTypeCode = 3;
                
                var customers = await _customerService.GetCustomerListAsync(filter);
                
                return Ok(new ApiResponse<PagedResponse<CustomerListResponse>>
                {
                    Success = true,
                    Message = "Müşteri listesi başarıyla getirildi",
                    Data = customers
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri listesi getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<PagedResponse<CustomerListResponse>>
                {
                    Success = false,
                    Message = "Müşteri listesi getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
