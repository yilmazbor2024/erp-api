using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErpMobile.Api.Services;
using ErpMobile.Api.Models.DatabaseBackup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Entities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin")]
    public class DatabaseBackupController : ControllerBase
    {
        private readonly IDatabaseBackupService _backupService;
        private readonly ILogger<DatabaseBackupController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IGlobalSettingsService _globalSettings;

        public DatabaseBackupController(
            IDatabaseBackupService backupService,
            ILogger<DatabaseBackupController> logger,
            IConfiguration configuration,
            IGlobalSettingsService globalSettings)
        {
            _backupService = backupService;
            _logger = logger;
            _configuration = configuration;
            _globalSettings = globalSettings;
        }

        /// <summary>
        /// Veritabanı yedeklerini listeler
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <param name="days">Kaç günlük yedekleri listeleyeceğimiz (varsayılan: 7)</param>
        /// <returns>Yedek listesi</returns>
        [HttpGet("{databaseId}")]
        public async Task<ActionResult<ApiResponse<List<DatabaseBackupDto>>>> GetBackups(Guid databaseId, [FromQuery] int days = 7)
        {
            try
            {
                var backups = await _backupService.GetBackupsAsync(databaseId, days);
                return Ok(new ApiResponse<List<DatabaseBackupDto>>(backups, true, "Yedekler başarıyla listelendi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekler listelenirken hata oluştu");
                return StatusCode(500, new ApiResponse<List<DatabaseBackupDto>>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }

        /// <summary>
        /// Tam yedekleme işlemi yapar
        /// </summary>
        /// <param name="request">Yedekleme isteği</param>
        /// <returns>Yedekleme sonucu</returns>
        [HttpPost("full-backup")]
        public async Task<ActionResult<ApiResponse<BackupResult>>> CreateFullBackup([FromBody] BackupRequest request)
        {
            try
            {
                _logger.LogInformation("Tam yedekleme isteği alındı. Request: {0}", System.Text.Json.JsonSerializer.Serialize(request));
                
                if (request == null)
                {
                    _logger.LogWarning("Yedekleme isteği boş.");
                    return BadRequest(new ApiResponse<BackupResult>(null, false, "Yedekleme isteği boş."));
                }
                
                // DatabaseId kontrolü
                if (request.DatabaseId == Guid.Empty)
                {
                    _logger.LogWarning("Geçersiz veritabanı ID: {0}", request.DatabaseId);
                    return BadRequest(new ApiResponse<BackupResult>(null, false, "Geçersiz veritabanı ID. Lütfen geçerli bir GUID değeri gönderin."));
                }
                
                _logger.LogInformation("Tam yedekleme işlemi başlatılıyor. DatabaseId: {0}", request.DatabaseId);
                var result = await _backupService.CreateFullBackupAsync(request.DatabaseId);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<BackupResult>(result, true, result.Message));
                }
                else
                {
                    return BadRequest(new ApiResponse<BackupResult>(result, false, result.Message));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tam yedekleme sırasında hata oluştu");
                return StatusCode(500, new ApiResponse<BackupResult>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }

        /// <summary>
        /// Diferansiyel yedekleme işlemi yapar
        /// </summary>
        /// <param name="request">Yedekleme isteği</param>
        /// <returns>Yedekleme sonucu</returns>
        [HttpPost("differential-backup")]
        public async Task<ActionResult<ApiResponse<BackupResult>>> CreateDifferentialBackup([FromBody] BackupRequest request)
        {
            try
            {
                var result = await _backupService.CreateDifferentialBackupAsync(request.DatabaseId);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<BackupResult>(result, true, result.Message));
                }
                else
                {
                    return BadRequest(new ApiResponse<BackupResult>(result, false, result.Message));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Diferansiyel yedekleme sırasında hata oluştu");
                return StatusCode(500, new ApiResponse<BackupResult>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }

        /// <summary>
        /// Veritabanı yedeğini geri yükler
        /// </summary>
        /// <param name="request">Geri yükleme isteği</param>
        /// <returns>Geri yükleme sonucu</returns>
        [HttpPost("restore")]
        public async Task<ActionResult<ApiResponse<RestoreResult>>> RestoreBackup([FromBody] RestoreRequest request)
        {
            try
            {
                var result = await _backupService.RestoreBackupAsync(request.DatabaseId, request.BackupId);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<RestoreResult>(result, true, result.Message));
                }
                else
                {
                    return BadRequest(new ApiResponse<RestoreResult>(result, false, result.Message));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedek geri yüklenirken hata oluştu");
                return StatusCode(500, new ApiResponse<RestoreResult>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }

        /// <summary>
        /// Veritabanı yedeğini indirir
        /// </summary>
        /// <param name="backupId">Yedek ID</param>
        /// <returns>Yedek dosyası</returns>
        [HttpGet("download/{backupId}")]
        public async Task<ActionResult> DownloadBackup(Guid backupId)
        {
            try
            {
                var backup = await _backupService.GetBackupByIdAsync(backupId);
                if (backup == null)
                {
                    return NotFound(new ApiResponse<object>(null, false, "Yedek bulunamadı"));
                }
                
                var filePath = backup.BackupPath;
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new ApiResponse<object>(null, false, "Yedek dosyası bulunamadı"));
                }
                
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, "application/octet-stream", backup.BackupFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedek indirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }

        /// <summary>
        /// Sistemdeki veritabanlarını listeler
        /// </summary>
        /// <returns>Veritabanı listesi</returns>
        [HttpGet("databases")]
        public async Task<ActionResult<ApiResponse<List<DatabaseInfo>>>> GetDatabases()
        {
            try
            {
                _logger.LogInformation("GetDatabases endpoint çağrıldı.");
                var databases = await _backupService.GetDatabasesAsync();
                _logger.LogInformation("{0} veritabanı bulundu ve listeleniyor.", databases.Count);
                return Ok(new ApiResponse<List<DatabaseInfo>>(databases, true, "Veritabanları başarıyla listelendi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veritabanları listelenirken hata oluştu");
                return StatusCode(500, new ApiResponse<List<DatabaseInfo>>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }

        /// <summary>
        /// Veritabanı tablolarını listeler
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <returns>Tablo listesi</returns>
        [HttpGet("tables/{databaseId}")]
        public async Task<ActionResult<ApiResponse<List<TableInfo>>>> GetTables(Guid databaseId)
        {
            try
            {
                var tables = await _backupService.GetTablesAsync(databaseId);
                return Ok(new ApiResponse<List<TableInfo>>(tables, true, "Tablolar başarıyla listelendi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tablolar listelenirken hata oluştu");
                return StatusCode(500, new ApiResponse<List<TableInfo>>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }

        /// <summary>
        /// SQL sorgusu çalıştırır
        /// </summary>
        /// <param name="request">Sorgu isteği</param>
        /// <returns>Sorgu sonucu</returns>
        [HttpPost("query")]
        public async Task<ActionResult<ApiResponse<QueryResult>>> ExecuteQuery([FromBody] QueryRequest request)
        {
            try
            {
                var result = await _backupService.ExecuteQueryAsync(request.DatabaseId, request.SqlQuery);
                return Ok(new ApiResponse<QueryResult>(result, true, "Sorgu başarıyla çalıştırıldı"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sorgu çalıştırılırken hata oluştu");
                return StatusCode(500, new ApiResponse<QueryResult>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }
        
        /// <summary>
        /// Veritabanı yedekleme ayarlarını getirir
        /// </summary>
        /// <returns>Yedekleme ayarları</returns>
        [HttpGet("settings")]
        public async Task<ActionResult<ApiResponse<BackupSettings>>> GetBackupSettings()
        {
            try
            {
                var settings = new BackupSettings
                {
                    BackupPath = await _globalSettings.GetSettingAsync<string>("DatabaseBackup:Path", 
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups")),
                    ScheduleTime = await _globalSettings.GetSettingAsync<string>("DatabaseBackup:ScheduleTime", "03:00:00"),
                    RetentionDays = await _globalSettings.GetSettingAsync<int>("DatabaseBackup:RetentionDays", 30),
                    AutoBackupEnabled = await _globalSettings.GetSettingAsync<bool>("DatabaseBackup:AutoBackupEnabled", false)
                };
                
                return Ok(new ApiResponse<BackupSettings>(settings, true, "Yedekleme ayarları başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme ayarları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<BackupSettings>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }
        
        /// <summary>
        /// Veritabanı yedekleme ayarlarını günceller
        /// </summary>
        /// <param name="settings">Yeni ayarlar</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("settings")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateBackupSettings([FromBody] BackupSettings settings)
        {
            try
            {
                if (settings == null)
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Ayarlar boş olamaz"));
                }
                
                // Yol kontrolü
                if (string.IsNullOrWhiteSpace(settings.BackupPath))
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Yedekleme dizini belirtilmelidir"));
                }
                
                // Zaman formatı kontrolü
                if (!TimeSpan.TryParse(settings.ScheduleTime, out _))
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Geçersiz zaman formatı. HH:mm:ss formatında olmalıdır"));
                }
                
                // Saklama süresi kontrolü
                if (settings.RetentionDays < 0)
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Saklama süresi negatif olamaz"));
                }
                
                var username = User.Identity?.Name ?? "System";
                
                // Ayarları kaydet
                await _globalSettings.SaveSettingAsync("DatabaseBackup:Path", settings.BackupPath, username);
                await _globalSettings.SaveSettingAsync("DatabaseBackup:ScheduleTime", settings.ScheduleTime, username);
                await _globalSettings.SaveSettingAsync("DatabaseBackup:RetentionDays", settings.RetentionDays, username);
                await _globalSettings.SaveSettingAsync("DatabaseBackup:AutoBackupEnabled", settings.AutoBackupEnabled, username);
                
                // Yedekleme dizinini kontrol et ve oluştur
                if (!Directory.Exists(settings.BackupPath))
                {
                    try
                    {
                        Directory.CreateDirectory(settings.BackupPath);
                        _logger.LogInformation("Yedekleme dizini oluşturuldu: {Path}", settings.BackupPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Yedekleme dizini oluşturulurken hata oluştu: {Path}", settings.BackupPath);
                        return StatusCode(500, new ApiResponse<bool>(false, false, $"Yedekleme dizini oluşturulamadı: {ex.Message}"));
                    }
                }
                
                return Ok(new ApiResponse<bool>(true, true, "Yedekleme ayarları başarıyla güncellendi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme ayarları güncellenirken hata oluştu");
                return StatusCode(500, new ApiResponse<bool>(false, false, $"Hata oluştu: {ex.Message}"));
            }
        }
    }
}
