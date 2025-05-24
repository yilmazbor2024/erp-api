using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Inventory;
using ErpMobile.Api.Repositories.Inventory;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(
            ILogger<InventoryController> logger,
            IInventoryRepository inventoryRepository)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
        }

        /// <summary>
        /// Barkod ile envanter/stok bilgisini getirir
        /// </summary>
        /// <param name="barcode">Ürün barkodu</param>
        /// <returns>Envanter/stok bilgisi</returns>
        [HttpGet("stock/by-barcode/{barcode}")]
        public async Task<ActionResult<ApiResponse<List<InventoryStockModel>>>> GetInventoryStockByBarcode(string barcode)
        {
            try
            {
                if (string.IsNullOrEmpty(barcode))
                {
                    return BadRequest(new ApiResponse<List<InventoryStockModel>>(null, false, "Barkod boş olamaz", "BadRequest"));
                }

                var inventoryStock = await _inventoryRepository.GetInventoryStockByBarcodeAsync(barcode);

                if (inventoryStock == null || inventoryStock.Count == 0)
                {
                    return NotFound(new ApiResponse<List<InventoryStockModel>>(null, false, $"{barcode} barkoduna sahip envanter/stok bilgisi bulunamadı", "NotFound"));
                }

                return Ok(new ApiResponse<List<InventoryStockModel>>(inventoryStock, true, "Envanter/stok bilgisi başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Barkod ile envanter/stok bilgisi aranırken hata oluştu. Barkod: {Barcode}", barcode);
                return StatusCode(500, new ApiResponse<List<InventoryStockModel>>(null, false, "Envanter/stok bilgisi getirilirken bir hata oluştu.", "InternalServerError"));
            }
        }
    }
}
