using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class WarehouseTransferController : ControllerBase
    {
        private readonly IWarehouseTransferRepository _warehouseTransferRepository;
        private readonly ILogger<WarehouseTransferController> _logger;

        public WarehouseTransferController(
            IWarehouseTransferRepository warehouseTransferRepository,
            ILogger<WarehouseTransferController> logger)
        {
            _warehouseTransferRepository = warehouseTransferRepository;
            _logger = logger;
        }

        /// <summary>
        /// Depolar arası sevk listesini getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<List<WarehouseTransferResponse>>>> GetWarehouseTransfers(
            [FromQuery] string sourceWarehouseCode = null,
            [FromQuery] string targetWarehouseCode = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var transfers = await _warehouseTransferRepository.GetWarehouseTransfersAsync(
                    sourceWarehouseCode, targetWarehouseCode, startDate, endDate);
                
                return Ok(new ApiResponse<List<WarehouseTransferResponse>>
                {
                    Success = true,
                    Data = transfers,
                    Message = "Depolar arası sevk listesi başarıyla getirildi."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depolar arası sevk listesi getirilirken hata oluştu");
                return BadRequest(new ApiResponse<List<WarehouseTransferResponse>>
                {
                    Success = false,
                    Message = "Depolar arası sevk listesi getirilirken bir hata oluştu."
                });
            }
        }

        /// <summary>
        /// Belirli bir sevk kaydını getirir
        /// </summary>
        [HttpGet("{transferNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<WarehouseTransferDetailResponse>>> GetWarehouseTransferByNumber(string transferNumber)
        {
            try
            {
                var transfer = await _warehouseTransferRepository.GetWarehouseTransferByNumberAsync(transferNumber);
                
                if (transfer == null)
                {
                    return NotFound(new ApiResponse<WarehouseTransferDetailResponse>
                    {
                        Success = false,
                        Message = $"Sevk kaydı bulunamadı. Fiş No: {transferNumber}"
                    });
                }
                
                return Ok(new ApiResponse<WarehouseTransferDetailResponse>
                {
                    Success = true,
                    Data = transfer,
                    Message = "Sevk kaydı başarıyla getirildi."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydı getirilirken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                return BadRequest(new ApiResponse<WarehouseTransferDetailResponse>
                {
                    Success = false,
                    Message = "Sevk kaydı getirilirken bir hata oluştu."
                });
            }
        }

        /// <summary>
        /// Yeni bir sevk fiş numarası oluşturur
        /// </summary>
        [HttpGet("generate-number")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<string>>> GenerateTransferNumber()
        {
            try
            {
                var transferNumber = await _warehouseTransferRepository.GenerateTransferNumberAsync();
                
                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Data = transferNumber,
                    Message = "Sevk fiş numarası oluşturuldu"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk fiş numarası oluşturulurken hata oluştu");
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Sevk fiş numarası oluşturulurken hata oluştu"
                });
            }
        }
        
        /// <summary>
        /// Depolar arası sevk işlemlerinde kullanılacak depo listesini getirir
        /// </summary>
        [HttpGet("warehouses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<List<WarehouseResponse>>>> GetWarehouses()
        {
            try
            {
                var warehouses = await _warehouseTransferRepository.GetWarehousesAsync();
                return Ok(new ApiResponse<List<WarehouseResponse>>
                {
                    Success = true,
                    Data = warehouses,
                    Message = "Depo listesi başarıyla getirildi"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depo listesi getirilirken hata oluştu");
                return BadRequest(new ApiResponse<List<WarehouseResponse>>
                {
                    Success = false,
                    Message = "Depo listesi getirilirken hata oluştu"
                });
            }
        }
        
        /// <summary>
        /// Sevk kaydını düzenleme için kilitler
        /// </summary>
        [HttpPost("{transferNumber}/lock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<bool>>> LockWarehouseTransfer(
            string transferNumber, 
            [FromBody] string comment = null)
        {
            try
            {
                var userName = User.Identity.Name;
                var result = await _warehouseTransferRepository.LockWarehouseTransferAsync(transferNumber, userName, comment);
                
                if (result)
                {
                    return Ok(new ApiResponse<bool>
                    {
                        Success = true,
                        Data = true,
                        Message = "Sevk kaydı başarıyla kilitlendi"
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "Sevk kaydı kilitlenemedi. Başka bir kullanıcı tarafından kilitlenmiş olabilir."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydı kilitlenirken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Sevk kaydı kilitlenirken hata oluştu"
                });
            }
        }
        
        /// <summary>
        /// Sevk kaydının kilidini kaldırır
        /// </summary>
        [HttpPost("{transferNumber}/unlock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<bool>>> UnlockWarehouseTransfer(string transferNumber)
        {
            try
            {
                var userName = User.Identity.Name;
                var result = await _warehouseTransferRepository.UnlockWarehouseTransferAsync(transferNumber, userName);
                
                if (result)
                {
                    return Ok(new ApiResponse<bool>
                    {
                        Success = true,
                        Data = true,
                        Message = "Sevk kaydının kilidi başarıyla kaldırıldı"
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "Sevk kaydının kilidi kaldırılamadı. Kaydı başka bir kullanıcı kilitlemiş olabilir."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydının kilidi kaldırılırken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Sevk kaydının kilidi kaldırılırken hata oluştu"
                });
            }
        }
        
        /// <summary>
        /// Sevk kaydının kilit durumunu kontrol eder
        /// </summary>
        [HttpGet("{transferNumber}/lock-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<object>>> CheckWarehouseTransferLockStatus(string transferNumber)
        {
            try
            {
                var lockStatus = await _warehouseTransferRepository.CheckWarehouseTransferLockStatusAsync(transferNumber);
                
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new
                    {
                        IsLocked = lockStatus.IsLocked,
                        LockedByUser = lockStatus.LockedByUser,
                        LockDate = lockStatus.LockDate
                    },
                    Message = lockStatus.IsLocked 
                        ? $"Sevk kaydı {lockStatus.LockedByUser} tarafından kilitlenmiş" 
                        : "Sevk kaydı kilitli değil"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydının kilit durumu kontrol edilirken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Sevk kaydının kilit durumu kontrol edilirken hata oluştu"
                });
            }
        }

        /// <summary>
        /// Yeni bir depolar arası sevk kaydı oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<string>>> CreateWarehouseTransfer([FromBody] WarehouseTransferRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Geçersiz veri formatı.",
                        ValidationErrors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList()
                    });
                }
                
                // Kullanıcı adını al
                var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                
                // Sevk kaydını oluştur
                var transferNumber = await _warehouseTransferRepository.CreateWarehouseTransferAsync(request, userName);
                
                return CreatedAtAction(nameof(GetWarehouseTransferByNumber), new { transferNumber }, new ApiResponse<string>
                {
                    Success = true,
                    Data = transferNumber,
                    Message = "Depolar arası sevk kaydı başarıyla oluşturuldu."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depolar arası sevk kaydı oluşturulurken hata oluştu");
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Depolar arası sevk kaydı oluşturulurken bir hata oluştu."
                });
            }
        }

        /// <summary>
        /// Sevk işlemini onaylar
        /// </summary>
        [HttpPut("{transferNumber}/approve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> ApproveWarehouseTransfer(string transferNumber)
        {
            try
            {
                // Kullanıcı adını al
                var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                
                // Sevk kaydını onayla
                var result = await _warehouseTransferRepository.ApproveWarehouseTransferAsync(transferNumber, userName);
                
                if (!result)
                {
                    return NotFound(new ApiResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = $"Onaylanacak sevk kaydı bulunamadı. Fiş No: {transferNumber}"
                    });
                }
                
                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Sevk kaydı başarıyla onaylandı."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydı onaylanırken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Sevk kaydı onaylanırken bir hata oluştu."
                });
            }
        }

        /// <summary>
        /// Sevk işlemini iptal eder
        /// </summary>
        [HttpPut("{transferNumber}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> CancelWarehouseTransfer(string transferNumber)
        {
            try
            {
                // Kullanıcı adını al
                var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                
                // Sevk kaydını iptal et
                var result = await _warehouseTransferRepository.CancelWarehouseTransferAsync(transferNumber, userName);
                
                if (!result)
                {
                    return NotFound(new ApiResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = $"İptal edilecek sevk kaydı bulunamadı veya kayıt onaylanmış durumda. Fiş No: {transferNumber}"
                    });
                }
                
                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Sevk kaydı başarıyla iptal edildi."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydı iptal edilirken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Sevk kaydı iptal edilirken bir hata oluştu."
                });
            }
        }
    }
}
