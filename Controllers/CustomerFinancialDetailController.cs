using System;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerFinancialDetailController : ControllerBase
    {
        private readonly ILogger<CustomerFinancialDetailController> _logger;
        private readonly CustomerFinancialDetailService _customerFinancialDetailService;
        private readonly IConfiguration _configuration;

        public CustomerFinancialDetailController(
            ILogger<CustomerFinancialDetailController> logger,
            CustomerFinancialDetailService customerFinancialDetailService,
            IConfiguration configuration)
        {
            _logger = logger;
            _customerFinancialDetailService = customerFinancialDetailService;
            _configuration = configuration;
        }

        [HttpGet("{customerCode}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<ApiResponse<CustomerFinancialDetailResponse>>> GetCustomerFinancialDetailsByCode(string customerCode)
        {
            try
            {
                var env = _configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");
                if (env == "Development")
                {
                    // Skip token validation in development
                }

                var customerFinancialDetails = await _customerFinancialDetailService.GetCustomerFinancialDetailsByCodeAsync(customerCode);

                if (customerFinancialDetails == null)
                {
                    return NotFound(new ApiResponse<CustomerFinancialDetailResponse>
                    {
                        Success = false,
                        Message = "Müşteri bulunamadı",
                        Error = "Müşteri bulunamadı"
                    });
                }

                return Ok(new ApiResponse<CustomerFinancialDetailResponse>
                {
                    Data = customerFinancialDetails,
                    Success = true,
                    Message = "Müşteri finansal detayları başarıyla getirildi"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri finansal detayları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<CustomerFinancialDetailResponse>
                {
                    Success = false,
                    Message = "Müşteri finansal detayları getirilirken hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
