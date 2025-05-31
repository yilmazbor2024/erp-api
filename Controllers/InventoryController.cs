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
        /// Çok amaçlı envanter/stok sorgulama endpoint'i
        /// </summary>
        /// <param name="barcode">Barkod (opsiyonel)</param>
        /// <param name="productCode">Ürün kodu (opsiyonel)</param>
        /// <param name="productDescription">Ürün açıklaması (opsiyonel)</param>
        /// <param name="colorCode">Renk kodu (opsiyonel)</param>
        /// <param name="itemDim1Code">Beden kodu (opsiyonel)</param>
        /// <param name="warehouseCode">Depo kodu (opsiyonel)</param>
        /// <param name="showOnlyPositiveStock">Sadece stok miktarı sıfırdan büyük olanları göster (opsiyonel, varsayılan: false)</param>
        /// <returns>Envanter/stok bilgileri listesi</returns>
        [HttpGet("stock/multi-purpose")]
        public async Task<ActionResult<ApiResponse<List<InventoryStockModel>>>> GetInventoryStockMultiPurpose(
            [FromQuery] string barcode = null,
            [FromQuery] string productCode = null,
            [FromQuery] string productDescription = null,
            [FromQuery] string colorCode = null,
            [FromQuery] string itemDim1Code = null,
            [FromQuery] string warehouseCode = null,
            [FromQuery] bool showOnlyPositiveStock = false)
        {
            try
            {
                // En az bir parametre belirtilmiş olmalı
                if (string.IsNullOrEmpty(barcode) && 
                    string.IsNullOrEmpty(productCode) && 
                    string.IsNullOrEmpty(productDescription) && 
                    string.IsNullOrEmpty(colorCode) && 
                    string.IsNullOrEmpty(itemDim1Code) && 
                    string.IsNullOrEmpty(warehouseCode) && 
                    !showOnlyPositiveStock)
                {
                    return BadRequest(new ApiResponse<List<InventoryStockModel>>(null, false, "En az bir arama kriteri belirtilmelidir", "BadRequest"));
                }

                var inventoryStock = await _inventoryRepository.GetInventoryStockMultiPurposeAsync(
                    barcode, 
                    productCode, 
                    productDescription, 
                    colorCode, 
                    itemDim1Code, 
                    warehouseCode, 
                    showOnlyPositiveStock);

                if (inventoryStock == null || inventoryStock.Count == 0)
                {
                    return NotFound(new ApiResponse<List<InventoryStockModel>>(null, false, "Belirtilen kriterlere uygun envanter/stok bilgisi bulunamadı", "NotFound"));
                }

                return Ok(new ApiResponse<List<InventoryStockModel>>(inventoryStock, true, "Envanter/stok bilgisi başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Çok amaçlı envanter/stok sorgusu yapılırken hata oluştu.");
                return StatusCode(500, new ApiResponse<List<InventoryStockModel>>(null, false, "Envanter/stok bilgisi getirilirken bir hata oluştu.", "InternalServerError"));
            }
        }


    }
}
