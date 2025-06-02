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
                // Renkli konsol logları ekleyelim
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n==================== STOK SORGULAMA PARAMETRELERİ ====================");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Barkod: {barcode ?? "null"}");
                Console.WriteLine($"Ürün Kodu: {productCode ?? "null"}");
                Console.WriteLine($"Ürün Açıklaması: {productDescription ?? "null"}");
                Console.WriteLine($"Renk Kodu: {colorCode ?? "null"}");
                Console.WriteLine($"Beden Kodu: {itemDim1Code ?? "null"}");
                Console.WriteLine($"Depo Kodu: {warehouseCode ?? "null"}");
                Console.WriteLine($"Sadece Pozitif Stok: {showOnlyPositiveStock}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===================================================================\n");
                Console.ResetColor();

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

                var result = await _inventoryRepository.GetInventoryStockMultiPurposeAsync(
                    barcode,
                    productCode,
                    productDescription,
                    colorCode,
                    itemDim1Code,
                    warehouseCode,
                    showOnlyPositiveStock);

                // Sonuç bilgisini yazdıralım
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n==================== STOK SORGULAMA SONUÇLARI ====================");
                Console.WriteLine($"Bulunan stok kaydı sayısı: {result?.Count ?? 0}");
                Console.WriteLine($"===================================================================\n");
                Console.ResetColor();

                if (result == null)
                {
                    // Veri bulunamadığında boş liste döndür, 404 yerine
                    return Ok(new ApiResponse<List<InventoryStockModel>>(new List<InventoryStockModel>(), true, "Belirtilen kriterlere uygun envanter/stok bilgisi bulunamadı"));
                }

                return Ok(new ApiResponse<List<InventoryStockModel>>(result, true, "Envanter/stok bilgisi başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n==================== STOK SORGULAMA HATASI ====================");
                Console.WriteLine($"Hata: {ex.Message}");
                Console.WriteLine($"===================================================================\n");
                Console.ResetColor();

                _logger.LogError(ex, "Çok amaçlı envanter/stok sorgusu yapılırken hata oluştu.");
                return StatusCode(500, new ApiResponse<List<InventoryStockModel>>(null, false, "Envanter/stok bilgisi getirilirken bir hata oluştu.", "InternalServerError"));
            }
        }


    }
}
