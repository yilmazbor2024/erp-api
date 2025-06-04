using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Tax;
using ErpMobile.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxTypeService _taxTypeService;
        private readonly ILogger<TaxController> _logger;

        public TaxController(ITaxTypeService taxTypeService, ILogger<TaxController> logger)
        {
            _taxTypeService = taxTypeService;
            _logger = logger;
        }

        [HttpGet("types")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TaxTypeModel>>> GetTaxTypes()
        {
            try
            {
                _logger.LogInformation("Getting all tax types");
                var taxTypes = await _taxTypeService.GetAllTaxTypesAsync();
                return Ok(taxTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tax types");
                return StatusCode(500, "Internal server error occurred while retrieving tax types");
            }
        }
    }
}
