using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Services;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.DatabaseBackup;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    public class BackupSettingsController : ControllerBase
    {
        private readonly ILogger<BackupSettingsController> _logger;
        private readonly IGlobalSettingsService _globalSettings;
        private readonly IDatabaseBackupService _backupService;

        public BackupSettingsController(
            ILogger<BackupSettingsController> logger,
            IGlobalSettingsService globalSettings,
            IDatabaseBackupService backupService)
        {
            _logger = logger;
            _globalSettings = globalSettings;
            _backupService = backupService;
        }

        /// <summary>
        /// Yedekleme ayarlarını getirir
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<BackupSettings>>> GetBackupSettings()
        {
            try
            {
                _logger.LogInformation("Yedekleme ayarları istendi");
                
                var settings = new BackupSettings
                {
                    BackupPath = await _globalSettings.GetSettingAsync<string>("DatabaseBackup:BackupPath", "C:\\Backups"),
                    AutoBackupEnabled = await _globalSettings.GetSettingAsync<bool>("DatabaseBackup:AutoBackupEnabled", false),
                    ScheduleTime = await _globalSettings.GetSettingAsync<string>("DatabaseBackup:ScheduleTime", "03:00:00"),
                    RetentionDays = await _globalSettings.GetSettingAsync<int>("DatabaseBackup:RetentionDays", 30),
                    MaxBackupsPerDatabase = await _globalSettings.GetSettingAsync<int>("DatabaseBackup:MaxBackupsPerDatabase", 10),
                    MinimumFreeSpaceGB = await _globalSettings.GetSettingAsync<double>("DatabaseBackup:MinimumFreeSpaceGB", 5.0)
                };
                
                // Yedekleme dizininin durumunu kontrol et
                var backupPathInfo = new DirectoryInfo(settings.BackupPath);
                if (backupPathInfo.Exists)
                {
                    var driveInfo = new DriveInfo(backupPathInfo.Root.FullName);
                    settings.AvailableFreeSpaceGB = Math.Round(driveInfo.AvailableFreeSpace / (1024.0 * 1024 * 1024), 2);
                    settings.TotalSizeGB = Math.Round(driveInfo.TotalSize / (1024.0 * 1024 * 1024), 2);
                    settings.DirectoryExists = true;
                }
                else
                {
                    settings.DirectoryExists = false;
                    settings.AvailableFreeSpaceGB = 0;
                    settings.TotalSizeGB = 0;
                }
                
                // Toplam yedek sayısını getir
                settings.TotalBackupCount = await _backupService.GetTotalBackupCountAsync();
                
                return Ok(new ApiResponse<BackupSettings>(settings, true, "Yedekleme ayarları başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme ayarları getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<BackupSettings>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }

        /// <summary>
        /// Yedekleme ayarlarını günceller
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateBackupSettings([FromBody] BackupSettings settings)
        {
            try
            {
                _logger.LogInformation("Yedekleme ayarları güncelleme isteği alındı: {0}", 
                    System.Text.Json.JsonSerializer.Serialize(settings));
                
                if (settings == null)
                {
                    return BadRequest(new ApiResponse<bool>(false, false, "Ayarlar boş olamaz"));
                }
                
                // Yedekleme dizinini kontrol et
                if (!string.IsNullOrEmpty(settings.BackupPath))
                {
                    try
                    {
                        // Dizin yoksa oluştur
                        if (!Directory.Exists(settings.BackupPath))
                        {
                            Directory.CreateDirectory(settings.BackupPath);
                            _logger.LogInformation("Yedekleme dizini oluşturuldu: {0}", settings.BackupPath);
                        }
                        
                        // Yazma izni kontrolü
                        var testFile = Path.Combine(settings.BackupPath, "test_write_permission.tmp");
                        System.IO.File.WriteAllText(testFile, "test");
                        System.IO.File.Delete(testFile);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Yedekleme dizini oluşturulamadı veya yazma izni yok: {0}", settings.BackupPath);
                        return BadRequest(new ApiResponse<bool>(false, false, $"Yedekleme dizini oluşturulamadı veya yazma izni yok: {ex.Message}"));
                    }
                }
                
                // Kullanıcı adını al
                var username = User.Identity?.Name ?? "System";
                
                // Ayarları kaydet
                await _globalSettings.SaveSettingAsync("DatabaseBackup:BackupPath", settings.BackupPath, username);
                await _globalSettings.SaveSettingAsync("DatabaseBackup:AutoBackupEnabled", settings.AutoBackupEnabled, username);
                await _globalSettings.SaveSettingAsync("DatabaseBackup:ScheduleTime", settings.ScheduleTime, username);
                await _globalSettings.SaveSettingAsync("DatabaseBackup:RetentionDays", settings.RetentionDays, username);
                await _globalSettings.SaveSettingAsync("DatabaseBackup:MaxBackupsPerDatabase", settings.MaxBackupsPerDatabase, username);
                await _globalSettings.SaveSettingAsync("DatabaseBackup:MinimumFreeSpaceGB", settings.MinimumFreeSpaceGB, username);
                
                _logger.LogInformation("Yedekleme ayarları başarıyla güncellendi");
                
                return Ok(new ApiResponse<bool>(true, true, "Yedekleme ayarları başarıyla güncellendi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme ayarları güncellenirken hata oluştu");
                return StatusCode(500, new ApiResponse<bool>(false, false, $"Hata oluştu: {ex.Message}"));
            }
        }
        
        /// <summary>
        /// Yedekleme dizininin durumunu kontrol eder
        /// </summary>
        [HttpGet("check-backup-path")]
        public ActionResult<ApiResponse<BackupPathStatus>> CheckBackupPath([FromQuery] string path)
        {
            try
            {
                _logger.LogInformation("Yedekleme dizini kontrolü istendi: {0}", path);
                
                if (string.IsNullOrEmpty(path))
                {
                    return BadRequest(new ApiResponse<BackupPathStatus>(null, false, "Dizin yolu boş olamaz"));
                }
                
                var status = new BackupPathStatus
                {
                    Path = path,
                    Exists = Directory.Exists(path),
                    IsWritable = false,
                    AvailableFreeSpaceGB = 0,
                    TotalSizeGB = 0
                };
                
                if (status.Exists)
                {
                    try
                    {
                        // Yazma izni kontrolü
                        var testFile = Path.Combine(path, "test_write_permission.tmp");
                        System.IO.File.WriteAllText(testFile, "test");
                        System.IO.File.Delete(testFile);
                        status.IsWritable = true;
                        
                        // Disk alanı bilgisi
                        var driveInfo = new DriveInfo(new DirectoryInfo(path).Root.FullName);
                        status.AvailableFreeSpaceGB = Math.Round(driveInfo.AvailableFreeSpace / (1024.0 * 1024 * 1024), 2);
                        status.TotalSizeGB = Math.Round(driveInfo.TotalSize / (1024.0 * 1024 * 1024), 2);
                    }
                    catch
                    {
                        status.IsWritable = false;
                    }
                }
                
                return Ok(new ApiResponse<BackupPathStatus>(status, true, "Dizin durumu başarıyla kontrol edildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme dizini kontrolü sırasında hata oluştu: {0}", path);
                return StatusCode(500, new ApiResponse<BackupPathStatus>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }
        
        /// <summary>
        /// Yedekleme istatistiklerini getirir
        /// </summary>
        [HttpGet("statistics")]
        public async Task<ActionResult<ApiResponse<BackupStatistics>>> GetBackupStatistics()
        {
            try
            {
                _logger.LogInformation("Yedekleme istatistikleri istendi");
                
                var stats = await _backupService.GetBackupStatisticsAsync();
                
                return Ok(new ApiResponse<BackupStatistics>(stats, true, "Yedekleme istatistikleri başarıyla getirildi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme istatistikleri getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<BackupStatistics>(null, false, $"Hata oluştu: {ex.Message}"));
            }
        }
    }
}
