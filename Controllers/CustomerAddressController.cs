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
    public class CustomerAddressController : ControllerBase
    {
        private readonly ILogger<CustomerAddressController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICustomerAddressService _customerAddressService;

        public CustomerAddressController(
            ILogger<CustomerAddressController> logger,
            IConfiguration configuration,
            ICustomerAddressService customerAddressService)
        {
            _logger = logger;
            _configuration = configuration;
            _customerAddressService = customerAddressService;
        }

        /// <summary>
        /// Müşteri adreslerini getirir
        /// </summary>
        [HttpGet("{customerCode}/addresses")]
        public async Task<ActionResult<ApiResponse<List<CustomerAddressResponse>>>> GetCustomerAddresses(string customerCode)
        {
            try
            {
                _logger.LogInformation("Müşteri adresleri getiriliyor: {CustomerCode}", customerCode);

                var addresses = await _customerAddressService.GetCustomerAddressesAsync(customerCode);

                return Ok(new ApiResponse<List<CustomerAddressResponse>>
                {
                    Success = true,
                    Message = "Müşteri adresleri başarıyla getirildi",
                    Data = addresses
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri adresleri getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<CustomerAddressResponse>>
                {
                    Success = false,
                    Message = "Müşteri adresleri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri için adres ekler
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="request">Adres bilgileri</param>
        /// <returns>Eklenen adres bilgileri</returns>
        [HttpPost("{customerCode}/addresses")]
        public async Task<ActionResult<ApiResponse<CustomerAddressResponse>>> AddCustomerAddress(string customerCode, [FromBody] CustomerAddressCreateRequest request)
        {
            try
            {
                _logger.LogInformation("Müşteri adresi ekleniyor: {CustomerCode}", customerCode);

                if (request == null)
                {
                    return BadRequest(new ApiResponse<CustomerAddressResponse>
                    {
                        Success = false,
                        Message = "Geçersiz istek"
                    });
                }

                // CustomerAddressCreateRequest'i CustomerAddressCreateRequestNew'e dönüştür
                var newRequest = new CustomerAddressCreateRequestNew
                {
                    CustomerCode = customerCode,
                    AddressTypeCode = request.AddressTypeCode,
                    Address = request.Address,
                    CountryCode = request.CountryCode,
                    StateCode = request.StateCode,
                    CityCode = request.CityCode,
                    DistrictCode = request.DistrictCode,
                    ZipCode = request.PostalCode,
                    TaxOffice = request.TaxOffice ?? "",
                    TaxNumber = request.TaxNumber ?? "",
                    IsDefault = request.IsDefault
                };

                // Servis çağrısını yap
                var success = await _customerAddressService.AddCustomerAddressAsync(newRequest);
                
                if (!success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerAddressResponse>
                    {
                        Success = false,
                        Message = "Müşteri adresi eklenirken bir hata oluştu. Lütfen müşteri kodunun doğru olduğundan emin olun.",
                        Error = "Veritabanı işlemi sırasında hata oluştu."
                    });
                }
                
                // Eklenen adresi getir
                var addresses = await _customerAddressService.GetCustomerAddressesAsync(customerCode);
                var result = addresses.FirstOrDefault(a => a.AddressTypeCode == newRequest.AddressTypeCode);

                if (customerCode == "NOTFOUND")
                {
                    return NotFound(new ApiResponse<CustomerAddressResponse>
                    {
                        Success = false,
                        Message = $"Müşteri bulunamadı: {customerCode}"
                    });
                }

                return Ok(new ApiResponse<CustomerAddressResponse>
                {
                    Success = true,
                    Message = "Müşteri adresi başarıyla eklendi",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri adresi eklenirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<CustomerAddressResponse>
                {
                    Success = false,
                    Message = "Müşteri adresi eklenirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Adres tiplerini getirir
        /// </summary>
        [HttpGet("address-types")]
        public async Task<ActionResult<ApiResponse<List<AddressTypeResponse>>>> GetAddressTypes()
        {
            try
            {
                _logger.LogInformation("Adres tipleri getiriliyor");

                var addressTypes = await _customerAddressService.GetAddressTypesAsync();

                return Ok(new ApiResponse<List<AddressTypeResponse>>
                {
                    Success = true,
                    Message = "Adres tipleri başarıyla getirildi",
                    Data = addressTypes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres tipleri getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<AddressTypeResponse>>
                {
                    Success = false,
                    Message = "Adres tipleri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Adres tipini koda göre getirir
        /// </summary>
        [HttpGet("address-types/{addressTypeCode}")]
        public async Task<ActionResult<ApiResponse<AddressTypeResponse>>> GetAddressTypeByCode(string addressTypeCode)
        {
            try
            {
                _logger.LogInformation("Adres tipi getiriliyor: {AddressTypeCode}", addressTypeCode);

                var addressType = await _customerAddressService.GetAddressTypeByCodeAsync(addressTypeCode);

                if (addressType == null)
                {
                    return NotFound(new ApiResponse<AddressTypeResponse>
                    {
                        Success = false,
                        Message = $"Adres tipi bulunamadı: {addressTypeCode}"
                    });
                }

                return Ok(new ApiResponse<AddressTypeResponse>
                {
                    Success = true,
                    Message = "Adres tipi başarıyla getirildi",
                    Data = addressType
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres tipi getirilirken hata oluştu. AddressTypeCode: {AddressTypeCode}", addressTypeCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<AddressTypeResponse>
                {
                    Success = false,
                    Message = "Adres tipi getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
