using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
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
    public class CustomerFinancialController : ControllerBase
    {
        private readonly ILogger<CustomerFinancialController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICustomerFinancialService _customerFinancialService;

        public CustomerFinancialController(
            ILogger<CustomerFinancialController> logger,
            IConfiguration configuration,
            ICustomerFinancialService customerFinancialService)
        {
            _logger = logger;
            _configuration = configuration;
            _customerFinancialService = customerFinancialService;
        }

        /// <summary>
        /// Müşteri kredi limiti ve bakiye bilgilerini getirir
        /// </summary>
        [HttpGet("{customerCode}/credit-info")]
        public async Task<ActionResult<ApiResponse<CustomerCreditInfoResponse>>> GetCustomerCreditInfo(string customerCode)
        {
            try
            {
                _logger.LogInformation("Müşteri kredi bilgileri getiriliyor: {CustomerCode}", customerCode);

                var creditInfo = await _customerFinancialService.GetCustomerCreditInfoAsync(customerCode);

                if (creditInfo == null)
                {
                    return NotFound(new ApiResponse<CustomerCreditInfoResponse>
                    {
                        Success = false,
                        Message = $"Müşteri kredi bilgileri bulunamadı: {customerCode}"
                    });
                }

                return Ok(new ApiResponse<CustomerCreditInfoResponse>
                {
                    Success = true,
                    Message = "Müşteri kredi bilgileri başarıyla getirildi",
                    Data = creditInfo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri kredi bilgileri getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerCreditInfoResponse>
                {
                    Success = false,
                    Message = "Müşteri kredi bilgileri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri finansal bilgilerini günceller
        /// </summary>
        [HttpPost("{customerCode}/financial")]
        public async Task<ActionResult<ApiResponse<CustomerFinancialUpdateResponse>>> UpdateCustomerFinancial(string customerCode, [FromBody] CustomerFinancialUpdateRequest request)
        {
            try
            {
                _logger.LogInformation("Müşteri finansal bilgileri güncelleniyor: {CustomerCode}", customerCode);

                if (request == null)
                {
                    return BadRequest(new ApiResponse<CustomerFinancialUpdateResponse>
                    {
                        Success = false,
                        Message = "Geçersiz istek"
                    });
                }

                // Müşteri kodu kontrolü
                request.CustomerCode = customerCode;

                var result = await _customerFinancialService.UpdateCustomerFinancialAsync(request);

                if (result == null)
                {
                    return NotFound(new ApiResponse<CustomerFinancialUpdateResponse>
                    {
                        Success = false,
                        Message = $"Müşteri bulunamadı: {customerCode}"
                    });
                }

                return Ok(new ApiResponse<CustomerFinancialUpdateResponse>
                {
                    Success = true,
                    Message = "Müşteri finansal bilgileri başarıyla güncellendi",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri finansal bilgileri güncellenirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerFinancialUpdateResponse>
                {
                    Success = false,
                    Message = "Müşteri finansal bilgileri güncellenirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri işlemlerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="startDate">Başlangıç tarihi (opsiyonel)</param>
        /// <param name="endDate">Bitiş tarihi (opsiyonel)</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa boyutu</param>
        /// <returns>Müşteri borç/alacak hareketleri</returns>
        [HttpGet("{customerCode}/transactions")]
        [ProducesResponseType(typeof(ApiResponse<PagedResponse<CustomerTransactionResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerTransactions(
            string customerCode, 
            [FromQuery] DateTime? startDate = null, 
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 20)
        {
            try
            {
                _logger.LogInformation("Müşteri borç/alacak bilgileri getirme isteği alındı: {CustomerCode}", customerCode);

                // Sayfa parametrelerini kontrol et
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 20;

                var pagedTransactions = await _customerFinancialService.GetCustomerTransactionsPagedAsync(customerCode, startDate, endDate, pageNumber, pageSize);
                
                if (pagedTransactions == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = $"Müşteri bulunamadı: {customerCode}",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<PagedResponse<CustomerTransactionResponse>>
                {
                    Success = true,
                    Message = "Müşteri borç/alacak bilgileri başarıyla getirildi",
                    Data = pagedTransactions
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri borç/alacak bilgileri getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Müşteri borç/alacak bilgileri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
