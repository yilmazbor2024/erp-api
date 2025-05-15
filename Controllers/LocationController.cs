using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        private readonly ILocationService _locationService;

        public LocationController(
            ILogger<LocationController> logger,
            ICountryService countryService,
            IStateService stateService,
            ILocationService locationService)
        {
            _logger = logger;
            _countryService = countryService;
            _stateService = stateService;
            _locationService = locationService;
        }

        /// <summary>
        /// Ülke listesini getirir
        /// </summary>
        [HttpGet("countries")]
        public async Task<ActionResult<ApiResponse<List<CountryResponse>>>> GetCountries([FromQuery] string langCode = "TR")
        {
            try
            {
                var countries = await _countryService.GetCountriesAsync(langCode);
                return Ok(ApiResponse<List<CountryResponse>>.Ok(countries, "Ülke listesi başarıyla getirildi."));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Ülke listesi getirilirken hata oluştu.");
                return StatusCode(500, ApiResponse<List<CountryResponse>>.Fail(ex.Message, "Ülke listesi getirilirken bir hata oluştu."));
            }
        }

        /// <summary>
        /// Bölge listesini getirir
        /// </summary>
        [HttpGet("states")]
        public async Task<ActionResult<ApiResponse<List<StateResponse>>>> GetStates([FromQuery] string langCode = "TR")
        {
            try
            {
                var states = await _stateService.GetStatesAsync(langCode);
                return Ok(ApiResponse<List<StateResponse>>.Ok(states, "Bölge listesi başarıyla getirildi."));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Bölge listesi getirilirken hata oluştu.");
                return StatusCode(500, ApiResponse<List<StateResponse>>.Fail(ex.Message, "Bölge listesi getirilirken bir hata oluştu."));
            }
        }

        /// <summary>
        /// Hiyerarşik lokasyon verisini getirir
        /// </summary>
        [HttpGet("hierarchy")]
        public async Task<ActionResult<ApiResponse<LocationHierarchyResponse>>> GetHierarchy([FromQuery] string langCode = "TR", [FromQuery] string countryCode = "TR")
        {
            try
            {
                var hierarchy = await _locationService.GetLocationHierarchyAsync(langCode, countryCode);
                return Ok(ApiResponse<LocationHierarchyResponse>.Ok(hierarchy, "Lokasyon hiyerarşisi başarıyla getirildi."));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Lokasyon hiyerarşisi getirilirken hata oluştu.");
                return StatusCode(500, ApiResponse<LocationHierarchyResponse>.Fail(ex.Message, "Lokasyon hiyerarşisi getirilirken bir hata oluştu."));
            }
        }
    }
}
