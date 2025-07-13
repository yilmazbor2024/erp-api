using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Erp.Services;
using System.Linq;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Data;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Döviz kurlarını günlük olarak otomatik senkronize eden arka plan servisi.
    /// </summary>
    public class ExchangeRateBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExchangeRateBackgroundService> _logger;
        private DateTime _nextRunTime;

        // Varsayılan ayarlar (GlobalSettings tablosunda ayar bulunamazsa kullanılır)
        private const int DEFAULT_HOUR_TO_RUN = 8;
        private const int DEFAULT_MINUTE_TO_RUN = 30;
        private const int DEFAULT_SYNC_FREQUENCY = 1; // Günde kaç kez (varsayılan: 1)

        public ExchangeRateBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<ExchangeRateBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _nextRunTime = CalculateNextRunTime(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Döviz kuru senkronizasyon servisi başlatıldı. İlk çalışma zamanı: {NextRun}", _nextRunTime);

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var delay = _nextRunTime > now
                    ? _nextRunTime - now
                    : TimeSpan.FromSeconds(5); // Eğer geçmiş bir zaman ise, 5 saniye sonra çalıştır

                _logger.LogDebug("Döviz kuru senkronizasyonu için bekleniyor: {Delay}", delay);
                
                await Task.Delay(delay, stoppingToken);
                
                if (!stoppingToken.IsCancellationRequested)
                {
                    await SyncExchangeRatesAsync();
                    _nextRunTime = CalculateNextRunTime(DateTime.Now);
                    _logger.LogInformation("Sonraki döviz kuru senkronizasyonu: {NextRun}", _nextRunTime);
                }
            }
        }
        
        /// <summary>
        /// Bir sonraki çalışma zamanını hesaplar (her gün belirtilen saatte)
        /// </summary>
        private DateTime CalculateNextRunTime(DateTime fromTime)
        {
            try
            {
                // GlobalSettings tablosundan ayarları oku
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<NanoServiceDbContext>();
                    
                    // Çalışma saati ayarı
                    var hourSetting = dbContext.GlobalSettings
                        .FirstOrDefault(s => s.SettingKey == "ExchangeRateSync.Hour" && s.IsActive);
                    
                    // Çalışma dakikası ayarı
                    var minuteSetting = dbContext.GlobalSettings
                        .FirstOrDefault(s => s.SettingKey == "ExchangeRateSync.Minute" && s.IsActive);
                    
                    // Günlük çalışma sıklığı ayarı
                    var frequencySetting = dbContext.GlobalSettings
                        .FirstOrDefault(s => s.SettingKey == "ExchangeRateSync.Frequency" && s.IsActive);
                    
                    // Ayarları parse et veya varsayılanları kullan
                    int hourToRun = int.TryParse(hourSetting?.SettingValue, out int h) ? h : DEFAULT_HOUR_TO_RUN;
                    int minuteToRun = int.TryParse(minuteSetting?.SettingValue, out int m) ? m : DEFAULT_MINUTE_TO_RUN;
                    int frequency = int.TryParse(frequencySetting?.SettingValue, out int f) ? f : DEFAULT_SYNC_FREQUENCY;
                    
                    // Geçerli aralıkta olduğundan emin ol
                    hourToRun = Math.Clamp(hourToRun, 0, 23);
                    minuteToRun = Math.Clamp(minuteToRun, 0, 59);
                    frequency = Math.Clamp(frequency, 1, 24); // Günde en fazla 24 kez (saatte bir)
                    
                    // Temel çalışma zamanını hesapla
                    var baseRunTime = new DateTime(fromTime.Year, fromTime.Month, fromTime.Day, hourToRun, minuteToRun, 0);
                    
                    // Günde birden fazla çalışacaksa, sıklığa göre hesapla
                    if (frequency > 1)
                    {
                        // 24 saati frequency'e böl
                        int intervalHours = 24 / frequency;
                        
                        // Şu anki saatten sonraki ilk çalışma zamanını bul
                        for (int i = 0; i < frequency; i++)
                        {
                            var candidateTime = baseRunTime.AddHours(i * intervalHours);
                            if (candidateTime > fromTime)
                            {
                                return candidateTime;
                            }
                        }
                        
                        // Bugün için kalan çalışma zamanı yoksa, yarının ilk zamanını döndür
                        return baseRunTime.AddDays(1);
                    }
                    else
                    {
                        // Günde bir kez çalışacaksa
                        if (baseRunTime <= fromTime)
                        {
                            // Eğer bugünün çalışma saati geçtiyse, yarına ayarla
                            baseRunTime = baseRunTime.AddDays(1);
                        }
                        
                        return baseRunTime;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Döviz kuru senkronizasyon ayarları okunurken hata oluştu. Varsayılan ayarlar kullanılacak.");
                
                // Hata durumunda varsayılan ayarları kullan
                var defaultRunTime = new DateTime(fromTime.Year, fromTime.Month, fromTime.Day, 
                                                DEFAULT_HOUR_TO_RUN, DEFAULT_MINUTE_TO_RUN, 0);
                
                if (defaultRunTime <= fromTime)
                {
                    defaultRunTime = defaultRunTime.AddDays(1);
                }
                
                return defaultRunTime;
            }
        }

        private async Task SyncExchangeRatesAsync()
        {
            try
            {
                _logger.LogInformation("Döviz kuru senkronizasyonu başlatılıyor...");
                
                // Senkronizasyon aktif mi kontrolü
                bool isEnabled = true;
                
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<NanoServiceDbContext>();
                    var enabledSetting = dbContext.GlobalSettings
                        .FirstOrDefault(s => s.SettingKey == "ExchangeRateSync.Enabled" && s.IsActive);
                    
                    // Eğer ayar varsa ve değeri "false" ise senkronizasyonu devre dışı bırak
                    if (enabledSetting != null && enabledSetting.SettingValue.ToLower() == "false")
                    {
                        isEnabled = false;
                    }
                    
                    if (!isEnabled)
                    {
                        _logger.LogInformation("Döviz kuru senkronizasyonu sistem ayarlarında devre dışı bırakılmış. İşlem yapılmayacak.");
                        return;
                    }
                    
                    var tcmbService = scope.ServiceProvider.GetRequiredService<TcmbExchangeRateService>();
                    var result = await tcmbService.SyncExchangeRatesAsync();
                    
                    if (result)
                    {
                        _logger.LogInformation("Döviz kuru senkronizasyonu başarıyla tamamlandı.");
                    }
                    else
                    {
                        _logger.LogWarning("Döviz kuru senkronizasyonu tamamlandı, ancak bazı sorunlar olabilir.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Döviz kuru senkronizasyonu sırasında hata oluştu.");
            }
        }
    }
}
