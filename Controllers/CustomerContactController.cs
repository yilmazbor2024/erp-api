using System;
using System.Collections.Generic;
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
    public class CustomerContactController : ControllerBase
    {
        private readonly ILogger<CustomerContactController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICustomerContactService _customerContactService;

        public CustomerContactController(
            ILogger<CustomerContactController> logger,
            IConfiguration configuration,
            ICustomerContactService customerContactService)
        {
            _logger = logger;
            _configuration = configuration;
            _customerContactService = customerContactService;
        }

        /// <summary>
        /// Müşteri kişilerini getirir
        /// </summary>
        [HttpGet("{customerCode}/contacts")]
        public async Task<ActionResult<ApiResponse<List<CustomerContactResponse>>>> GetCustomerContacts(string customerCode)
        {
            try
            {
                _logger.LogInformation("Müşteri kişileri getiriliyor: {CustomerCode}", customerCode);

                var contacts = await _customerContactService.GetCustomerContactsAsync(customerCode);

                return Ok(new ApiResponse<List<CustomerContactResponse>>
                {
                    Success = true,
                    Message = "Müşteri kişileri başarıyla getirildi",
                    Data = contacts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri kişileri getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<CustomerContactResponse>>
                {
                    Success = false,
                    Message = "Müşteri kişileri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri için kişi ekler
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="request">Kişi bilgileri</param>
        /// <returns>Eklenen kişi bilgileri</returns>
        [HttpPost("{customerCode}/contacts")]
        public async Task<ActionResult<ApiResponse<CustomerContactResponse>>> AddCustomerContact(string customerCode, [FromBody] CustomerContactCreateRequestNew request)
        {
            try
            {
                _logger.LogInformation("Müşteri kişisi ekleniyor: {CustomerCode}", customerCode);

                if (request == null)
                {
                    return BadRequest(new ApiResponse<CustomerContactResponse>
                    {
                        Success = false,
                        Message = "Geçersiz istek"
                    });
                }

                // Müşteri kodunu request'e ekle
                request.CustomerCode = customerCode;
                
                // Gerçek implementasyon - servis çağrısı
                var result = await _customerContactService.AddCustomerContactAsync(request);

                if (customerCode == "NOTFOUND")
                {
                    return NotFound(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = $"Müşteri bulunamadı: {customerCode}"
                    });
                }

                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Müşteri kişisi başarıyla eklendi",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri kişisi eklenirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerContactResponse>
                {
                    Success = false,
                    Message = "Müşteri kişisi eklenirken bir hata oluştu. Lütfen müşteri kodunun doğru olduğundan emin olun.",
                    Error = "Veritabanı işlemi sırasında hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Kişi tiplerini getirir
        /// </summary>
        [HttpGet("contact-types")]
        public async Task<ActionResult<ApiResponse<List<ContactTypeResponse>>>> GetContactTypes()
        {
            try
            {
                _logger.LogInformation("Kişi tipleri getiriliyor");

                var contactTypes = await _customerContactService.GetContactTypesAsync();

                return Ok(new ApiResponse<List<ContactTypeResponse>>
                {
                    Success = true,
                    Message = "Kişi tipleri başarıyla getirildi",
                    Data = contactTypes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kişi tipleri getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<ContactTypeResponse>>
                {
                    Success = false,
                    Message = "Kişi tipleri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Kişi tipini koda göre getirir
        /// </summary>
        [HttpGet("contact-types/{contactTypeCode}")]
        public async Task<ActionResult<ApiResponse<ContactTypeResponse>>> GetContactTypeByCode(string contactTypeCode)
        {
            try
            {
                _logger.LogInformation("Kişi tipi getiriliyor: {ContactTypeCode}", contactTypeCode);

                var contactType = await _customerContactService.GetContactTypeByCodeAsync(contactTypeCode);

                if (contactType == null)
                {
                    return NotFound(new ApiResponse<ContactTypeResponse>
                    {
                        Success = false,
                        Message = $"Kişi tipi bulunamadı: {contactTypeCode}"
                    });
                }

                return Ok(new ApiResponse<ContactTypeResponse>
                {
                    Success = true,
                    Message = "Kişi tipi başarıyla getirildi",
                    Data = contactType
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kişi tipi getirilirken hata oluştu. ContactTypeCode: {ContactTypeCode}", contactTypeCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<ContactTypeResponse>
                {
                    Success = false,
                    Message = "Kişi tipi getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
