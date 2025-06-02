using System;
using System.Threading.Tasks;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Customer;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerDefaultController : ControllerBase
    {
        private readonly ILogger<CustomerDefaultController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICustomerDefaultService _customerDefaultService;

        public CustomerDefaultController(
            ILogger<CustomerDefaultController> logger,
            IConfiguration configuration,
            ICustomerDefaultService customerDefaultService)
        {
            _logger = logger;
            _configuration = configuration;
            _customerDefaultService = customerDefaultService;
        }

        /// <summary>
        /// Müşteri varsayılan adres ve iletişim bilgilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri varsayılan bilgileri</returns>
        [HttpGet("{customerCode}/defaults")]
        public async Task<ActionResult<ApiResponse<CustomerDefaultAddressResponse>>> GetCustomerDefaults(string customerCode)
        {
            try
            {
                _logger.LogInformation("Müşteri varsayılan bilgileri getiriliyor: {CustomerCode}", customerCode);

                var customerDefaults = await _customerDefaultService.GetCustomerDefaultsAsync(customerCode);

                if (customerDefaults == null)
                {
                    return NotFound(new ApiResponse<CustomerDefaultAddressResponse>
                    {
                        Success = false,
                        Message = $"Müşteri varsayılan bilgileri bulunamadı: {customerCode}"
                    });
                }

                return Ok(new ApiResponse<CustomerDefaultAddressResponse>
                {
                    Success = true,
                    Message = "Müşteri varsayılan bilgileri başarıyla getirildi",
                    Data = customerDefaults
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri varsayılan bilgileri getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerDefaultAddressResponse>
                {
                    Success = false,
                    Message = "Müşteri varsayılan bilgileri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
