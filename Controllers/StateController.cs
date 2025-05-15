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
    public class StateController : ControllerBase
    {
        private readonly ILogger<StateController> _logger;
        private readonly IStateService _stateService;

        public StateController(ILogger<StateController> logger, IStateService stateService)
        {
            _logger = logger;
            _stateService = stateService;
        }

        /// <summary>
        /// State listesini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu (örn. TR)</param>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<StateResponse>>>> GetStates([FromQuery] string langCode = "TR")
        {
            try
            {
                var states = await _stateService.GetStatesAsync(langCode);
                return Ok(ApiResponse<List<StateResponse>>.Ok(states, "State listesi başarıyla getirildi."));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "State listesi getirilirken hata oluştu.");
                return StatusCode(500, ApiResponse<List<StateResponse>>.Fail(ex.Message, "State listesi getirilirken bir hata oluştu."));
            }
        }
    }
}
