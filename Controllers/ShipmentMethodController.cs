using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Services.ShipmentMethod;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Route("api/v1/[controller]s")]
    [EnableCors]
    public class ShipmentMethodController : ControllerBase
    {
        private readonly ILogger<ShipmentMethodController> _logger;
        private readonly IShipmentMethodService _shipmentMethodService;

        public ShipmentMethodController(
            ILogger<ShipmentMethodController> logger,
            IShipmentMethodService shipmentMethodService)
        {
            _logger = logger;
            _shipmentMethodService = shipmentMethodService;
        }

        /// <summary>
        /// Sevkiyat yöntemlerini listeler
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ShipmentMethodResponse>>>> GetShipmentMethods()
        {
            try
            {
                var response = await _shipmentMethodService.GetShipmentMethodsAsync();
                
                if (!response.Success)
                {
                    return StatusCode(500, response);
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevkiyat yöntemleri listelenirken hata oluştu");
                return StatusCode(500, new ApiResponse<List<ShipmentMethodResponse>>
                {
                    Success = false,
                    Message = "Sevkiyat yöntemleri listelenirken bir hata oluştu: " + ex.Message
                });
            }
        }
    }
}
