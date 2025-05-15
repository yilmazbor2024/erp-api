using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Responses;
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
    [Route("api/v1/Customer")]
    public class CustomerLocationController : ControllerBase
    {
        private readonly ILogger<CustomerLocationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICustomerLocationService _customerLocationService;

        public CustomerLocationController(
            ILogger<CustomerLocationController> logger,
            IConfiguration configuration,
            ICustomerLocationService customerLocationService)
        {
            _logger = logger;
            _configuration = configuration;
            _customerLocationService = customerLocationService;
        }

        /// <summary>
        /// Bölgeleri getirir
        /// </summary>
        [HttpGet("regions")]
        public async Task<ActionResult<ApiResponse<List<RegionResponse>>>> GetRegions()
        {
            try
            {
                _logger.LogInformation("Bölgeler getiriliyor");

                var regions = await _customerLocationService.GetRegionsAsync();

                return Ok(new ApiResponse<List<RegionResponse>>
                {
                    Success = true,
                    Message = "Bölgeler başarıyla getirildi",
                    Data = regions
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölgeler getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<RegionResponse>>
                {
                    Success = false,
                    Message = "Bölgeler getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Bölgeleri getirir (States)
        /// </summary>
        [HttpGet("states")]
        public async Task<ActionResult<ApiResponse<List<StateResponse>>>> GetStates()
        {
            try
            {
                _logger.LogInformation("Bölgeler (States) getiriliyor");

                var states = await _customerLocationService.GetStatesAsync();

                return Ok(new ApiResponse<List<StateResponse>>
                {
                    Success = true,
                    Message = "Bölgeler başarıyla getirildi",
                    Data = states
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölgeler getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<StateResponse>>
                {
                    Success = false,
                    Message = "Bölgeler getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Şehirleri getirir (Cities)
        /// </summary>
        [HttpGet("cities")]
        public async Task<ActionResult<ApiResponse<List<CityResponse>>>> GetCities()
        {
            try
            {
                _logger.LogInformation("Şehirler getiriliyor");

                var cities = await _customerLocationService.GetCitiesAsync();

                return Ok(new ApiResponse<List<CityResponse>>
                {
                    Success = true,
                    Message = "Şehirler başarıyla getirildi",
                    Data = cities
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şehirler getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<CityResponse>>
                {
                    Success = false,
                    Message = "Şehirler getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// İle göre şehirleri getirir
        /// </summary>
        [HttpGet("states/{stateCode}/cities")]
        public async Task<ActionResult<ApiResponse<List<CityResponse>>>> GetCitiesByState(string stateCode)
        {
            try
            {
                _logger.LogInformation("İle göre şehirler getiriliyor: {StateCode}", stateCode);

                var cities = await _customerLocationService.GetCitiesByStateAsync(stateCode);

                return Ok(new ApiResponse<List<CityResponse>>
                {
                    Success = true,
                    Message = "Şehirler başarıyla getirildi",
                    Data = cities
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İle göre şehirler getirilirken hata oluştu. StateCode: {StateCode}", stateCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<CityResponse>>
                {
                    Success = false,
                    Message = "İle göre şehirler getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Bölgeye göre şehirleri getirir
        /// </summary>
        [HttpGet("regions/{regionCode}/cities")]
        public async Task<ActionResult<ApiResponse<List<CityResponse>>>> GetCitiesByRegion(string regionCode)
        {
            try
            {
                _logger.LogInformation("Bölgeye göre şehirler getiriliyor: {RegionCode}", regionCode);

                var cities = await _customerLocationService.GetCitiesByRegionAsync(regionCode);

                return Ok(new ApiResponse<List<CityResponse>>
                {
                    Success = true,
                    Message = "Şehirler başarıyla getirildi",
                    Data = cities
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölgeye göre şehirler getirilirken hata oluştu. RegionCode: {RegionCode}", regionCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<CityResponse>>
                {
                    Success = false,
                    Message = "Bölgeye göre şehirler getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// İlçeleri getirir (Districts)
        /// </summary>
        [HttpGet("districts")]
        public async Task<ActionResult<ApiResponse<List<DistrictResponse>>>> GetDistricts()
        {
            try
            {
                _logger.LogInformation("İlçeler getiriliyor");

                // Stub implementation until ICustomerLocationService.GetDistrictsAsync is implemented
                var districts = new List<DistrictResponse>();

                return Ok(new ApiResponse<List<DistrictResponse>>
                {
                    Success = true,
                    Message = "İlçeler başarıyla getirildi",
                    Data = districts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İlçeler getirilirken hata oluştu");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<DistrictResponse>>
                {
                    Success = false,
                    Message = "İlçeler getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Şehre göre ilçeleri getirir
        /// </summary>
        [HttpGet("cities/{cityCode}/districts")]
        public async Task<ActionResult<ApiResponse<List<DistrictResponse>>>> GetDistrictsByCity(string cityCode)
        {
            try
            {
                _logger.LogInformation("Şehre göre ilçeler getiriliyor: {CityCode}", cityCode);

                var districts = await _customerLocationService.GetDistrictsByCityAsync(cityCode);

                return Ok(new ApiResponse<List<DistrictResponse>>
                {
                    Success = true,
                    Message = "İlçeler başarıyla getirildi",
                    Data = districts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şehre göre ilçeler getirilirken hata oluştu. CityCode: {CityCode}", cityCode);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<DistrictResponse>>
                {
                    Success = false,
                    Message = "Şehre göre ilçeler getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
