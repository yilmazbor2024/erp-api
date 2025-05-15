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
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;

        public CountryController(ILogger<CountryController> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        /// <summary>
        /// Ülke listesini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu (örn. TR)</param>
        [HttpGet]
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
    }
}
