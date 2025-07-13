using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ErpMobile.Api.Services
{
    public class AutoBackupService : BackgroundService
    {
        private readonly ILogger<AutoBackupService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer _timer;
        private TimeSpan _scheduleTime;
        private bool _autoBackupEnabled;

        public AutoBackupService(
            ILogger<AutoBackupService> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
            
            // Ayarlar başlangıçta varsayılan olarak ayarlanır
            // Gerçek değerler LoadSettingsAsync metodu ile asenkron olarak yüklenecek
            _autoBackupEnabled = false;
            _scheduleTime = TimeSpan.Parse("03:00:00");
            
            _logger.LogInformation("Otomatik yedekleme servisi başlatıldı. Ayarlar yükleniyor...");
        }

        /// <summary>
        /// GlobalSettings tablosundan yedekleme ayarlarını yükler
        /// </summary>
        private async Task<bool> LoadSettingsAsync()
        {
            try
            {
                // Scoped servisler için yeni bir scope oluştur
                using var scope = _serviceScopeFactory.CreateScope();
                var globalSettings = scope.ServiceProvider.GetRequiredService<IGlobalSettingsService>();
                
                // Otomatik yedekleme aktif mi?
                _autoBackupEnabled = await globalSettings.GetSettingAsync<bool>("DatabaseBackup:AutoBackupEnabled", false);
                
                // Yedekleme saati
                var scheduleTimeStr = await globalSettings.GetSettingAsync<string>("DatabaseBackup:ScheduleTime", "03:00:00");
                if (!TimeSpan.TryParse(scheduleTimeStr, out _scheduleTime))
                {
                    _scheduleTime = TimeSpan.Parse("03:00:00");
                    _logger.LogWarning("Geçersiz ScheduleTime formatı. Varsayılan saat 03:00 kullanılıyor.");
                }
                
                _logger.LogInformation("Otomatik yedekleme ayarları yüklendi. Aktif: {0}, Zaman: {1}", 
                    _autoBackupEnabled, _scheduleTime);
                    
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme ayarları yüklenirken hata oluştu");
                return false;
            }
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // GlobalSettings tablosundan ayarları yükle
            await LoadSettingsAsync();
            
            if (!_autoBackupEnabled)
            {
                _logger.LogInformation("Otomatik yedekleme devre dışı bırakılmış.");
                return;
            }

            // İlk çalışma zamanını hesapla
            var now = DateTime.Now;
            var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 
                _scheduleTime.Hours, _scheduleTime.Minutes, _scheduleTime.Seconds);
            
            // Eğer planlanan zaman geçmişse, bir sonraki güne ayarla
            if (now > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            // İlk çalışma için beklenecek süreyi hesapla
            var firstDelay = scheduledTime - now;
            _logger.LogInformation("İlk otomatik yedekleme {Time} sonra çalışacak.", firstDelay);

            // Timer'ı başlat
            _timer = new Timer(DoBackup, null, firstDelay, TimeSpan.FromDays(1));

            await Task.CompletedTask;
        }

        private async void DoBackup(object state)
        {
            try
            {
                _logger.LogInformation("Otomatik yedekleme başlatılıyor...");
                
                // Scope oluştur ve DatabaseBackupService'i al
                using (var scope = _serviceProvider.CreateScope())
                {
                    var backupService = scope.ServiceProvider.GetRequiredService<IDatabaseBackupService>();
                    
                    // Tüm aktif veritabanları al
                    var databases = await backupService.GetDatabasesAsync();
                    
                    foreach (var db in databases)
                    {
                        try
                        {
                            _logger.LogInformation("{0} veritabanı için otomatik yedekleme başlatılıyor...", db.DatabaseName);
                            
                            // Tam yedek al
                            var backupResult = await backupService.CreateFullBackupAsync(db.Id);
                            
                            if (backupResult != null)
                            {
                                _logger.LogInformation("{0} veritabanı için otomatik yedekleme tamamlandı. Dosya: {1}", 
                                    db.DatabaseName, backupResult.BackupPath);
                            }
                            else
                            {
                                _logger.LogWarning("{0} veritabanı için otomatik yedekleme başarısız oldu.", db.DatabaseName);
                            }
                        }
                        catch (Exception dbEx)
                        {
                            _logger.LogError(dbEx, "{0} veritabanı için otomatik yedekleme sırasında hata oluştu.", db.DatabaseName);
                        }
                    }
                    
                    // Eski yedekleri temizle
                    await CleanupOldBackups(backupService);
                }
                
                _logger.LogInformation("Otomatik yedekleme işlemi tamamlandı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Otomatik yedekleme sırasında hata oluştu.");
            }
        }
        
        private async Task CleanupOldBackups(IDatabaseBackupService backupService)
        {
            try
            {
                // GlobalSettings tablosundan saklama süresini al (varsayılan 30 gün)
                int retentionDays = 30;
                
                // Scoped servisler için yeni bir scope oluştur
                using (var settingsScope = _serviceScopeFactory.CreateScope())
                {
                    var globalSettings = settingsScope.ServiceProvider.GetRequiredService<IGlobalSettingsService>();
                    retentionDays = await globalSettings.GetSettingAsync<int>("DatabaseBackup:RetentionDays", 30);
                }
                
                if (retentionDays <= 0)
                {
                    _logger.LogInformation("Yedek temizleme devre dışı bırakılmış (RetentionDays <= 0).");
                    return;
                }
                
                _logger.LogInformation("Eski yedekler temizleniyor. Saklama süresi: {0} gün", retentionDays);
                
                // Belirtilen günden eski yedekleri temizle
                DateTime cutoffDate = DateTime.Now.AddDays(-retentionDays);
                int deletedCount = await backupService.CleanupOldBackupsAsync();
                
                _logger.LogInformation("{0} eski yedek dosyası temizlendi.", deletedCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eski yedekleri temizlerken hata oluştu.");
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Otomatik yedekleme servisi durduruluyor...");
            
            _timer?.Change(Timeout.Infinite, 0);
            
            await base.StopAsync(stoppingToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
