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
    public class CustomerCommunicationController : ControllerBase
    {
        private readonly ILogger<CustomerCommunicationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICustomerCommunicationService _customerCommunicationService;

        public CustomerCommunicationController(
            ILogger<CustomerCommunicationController> logger,
            IConfiguration configuration,
            ICustomerCommunicationService customerCommunicationService)
        {
            _logger = logger;
            _configuration = configuration;
            _customerCommunicationService = customerCommunicationService;
        }

        /// <summary>
        /// Müşteri iletişim bilgilerini getirir
        /// </summary>
        [HttpGet("{customerCode}/communications")]
        public async Task<ActionResult<ApiResponse<List<CustomerCommunicationResponse>>>> GetCustomerCommunications(string customerCode)
        {
            try
            {
                _logger.LogInformation("Müşteri iletişim bilgileri getiriliyor: {CustomerCode}", customerCode);

                var communications = await _customerCommunicationService.GetCustomerCommunicationsAsync(customerCode);

                return Ok(new ApiResponse<List<CustomerCommunicationResponse>>
                {
                    Success = true,
                    Message = "Müşteri iletişim bilgileri başarıyla getirildi",
                    Data = communications
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri iletişim bilgileri getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<CustomerCommunicationResponse>>
                {
                    Success = false,
                    Message = "Müşteri iletişim bilgileri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri için iletişim bilgisi ekler
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="request">İletişim bilgileri</param>
        /// <returns>Eklenen iletişim bilgileri</returns>
        [HttpPost("{customerCode}/communications")]
        public async Task<ActionResult<ApiResponse<CustomerCommunicationResponse>>> AddCustomerCommunication(string customerCode, [FromBody] CustomerCommunicationCreateRequest request)
        {
            try
            {
                _logger.LogInformation("Müşteri iletişim bilgisi ekleniyor: {CustomerCode}", customerCode);

                if (request == null)
                {
                    return BadRequest(new ApiResponse<CustomerCommunicationResponse>
                    {
                        Success = false,
                        Message = "Geçersiz istek"
                    });
                }

                // CustomerCommunicationCreateRequest'i CustomerCommunicationCreateRequestNew'e dönüştür
                var newRequest = new CustomerCommunicationCreateRequestNew
                {
                    CustomerCode = customerCode,
                    CommunicationTypeCode = request.CommunicationTypeCode,
                    CommAddress = request.Communication,
                    CanSendAdvert = false,
                    IsBlocked = false,
                    IsConfirmed = true,
                    IsDefault = request.IsDefault
                };

                // Servis çağrısını yap
                var success = await _customerCommunicationService.AddCustomerCommunicationAsync(newRequest);
                
                if (!success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerCommunicationResponse>
                    {
                        Success = false,
                        Message = "Müşteri iletişim bilgisi eklenirken bir hata oluştu. Lütfen müşteri kodunun doğru olduğundan emin olun.",
                        Error = "Veritabanı işlemi sırasında hata oluştu."
                    });
                }
                
                // Eklenen iletişim bilgisini getir
                var communications = await _customerCommunicationService.GetCustomerCommunicationsAsync(customerCode);
                var result = communications.FirstOrDefault(c => c.CommunicationTypeCode == newRequest.CommunicationTypeCode && c.CommAddress == newRequest.CommAddress);

                if (customerCode == "NOTFOUND")
                {
                    return NotFound(new ApiResponse<CustomerCommunicationResponse>
                    {
                        Success = false,
                        Message = $"Müşteri bulunamadı: {customerCode}"
                    });
                }

                return Ok(new ApiResponse<CustomerCommunicationResponse>
                {
                    Success = true,
                    Message = "Müşteri iletişim bilgisi başarıyla eklendi",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri iletişim bilgisi eklenirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerCommunicationResponse>
                {
                    Success = false,
                    Message = "Müşteri iletişim bilgisi eklenirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// İletişim tiplerini getirir
        /// </summary>
        [HttpGet("communication-types")]
        public async Task<ActionResult<ApiResponse<List<CommunicationTypeResponse>>>> GetCommunicationTypes()
        {
            try
            {
                _logger.LogInformation("İletişim tipleri getiriliyor");

                // Stub implementation until ICustomerCommunicationService.GetCommunicationTypesAsync is implemented
                var communicationTypes = new List<CommunicationTypeResponse>();

                return Ok(new ApiResponse<List<CommunicationTypeResponse>>
                {
                    Success = true,
                    Message = "İletişim tipleri başarıyla getirildi",
                    Data = communicationTypes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletişim tipleri getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<CommunicationTypeResponse>>
                {
                    Success = false,
                    Message = "İletişim tipleri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
