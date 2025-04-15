using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using erp_api.Models.Responses;
using erp_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using erp_api.Models.Common;

namespace erp_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly ILogger<WarehouseController> _logger;

        public WarehouseController(IWarehouseService warehouseService, ILogger<WarehouseController> logger)
        {
            _warehouseService = warehouseService;
            _logger = logger;
        }

        /// <summary>
        /// Get all warehouses
        /// </summary>
        /// <returns>List of warehouses</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<WarehouseResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWarehouses()
        {
            try
            {
                var warehouses = await _warehouseService.GetWarehousesAsync();
                return Ok(new ApiResponse<List<WarehouseResponse>>(warehouses, true, "Warehouses retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting warehouses");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ApiResponse<string>(null, false, "An error occurred while getting warehouses.", ex.Message));
            }
        }

        /// <summary>
        /// Get warehouse by code
        /// </summary>
        /// <param name="warehouseCode">The warehouse code</param>
        /// <returns>Warehouse details</returns>
        [HttpGet("{warehouseCode}")]
        [ProducesResponseType(typeof(ApiResponse<WarehouseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWarehouseByCode(string warehouseCode)
        {
            try
            {
                var warehouse = await _warehouseService.GetWarehouseByCodeAsync(warehouseCode);
                if (warehouse == null)
                {
                    return NotFound(new ApiResponse<string>(null, false, $"Warehouse not found with code: {warehouseCode}"));
                }
                return Ok(new ApiResponse<WarehouseResponse>(warehouse, true, "Warehouse retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting warehouse details. WarehouseCode: {WarehouseCode}", warehouseCode);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ApiResponse<string>(null, false, "An error occurred while getting warehouse details.", ex.Message));
            }
        }

        /// <summary>
        /// Get all tax offices
        /// </summary>
        /// <returns>List of tax offices</returns>
        [HttpGet("tax-offices")]
        [ProducesResponseType(typeof(ApiResponse<List<TaxOfficeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaxOffices()
        {
            try
            {
                var taxOffices = await _warehouseService.GetTaxOfficesAsync();
                return Ok(new ApiResponse<List<TaxOfficeResponse>>(taxOffices, true, "Tax offices retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting tax offices");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ApiResponse<string>(null, false, "An error occurred while getting tax offices.", ex.Message));
            }
        }

        /// <summary>
        /// Get all offices
        /// </summary>
        /// <returns>List of offices</returns>
        [HttpGet("offices")]
        [ProducesResponseType(typeof(ApiResponse<List<OfficeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOffices()
        {
            try
            {
                var offices = await _warehouseService.GetOfficesAsync();
                return Ok(new ApiResponse<List<OfficeResponse>>(offices, true, "Offices retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting offices");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new ApiResponse<string>(null, false, "An error occurred while getting offices.", ex.Message));
            }
        }
    }
} 