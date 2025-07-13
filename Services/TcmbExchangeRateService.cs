using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Logging;
using Erp.Models.ExchangeRate;
using ErpMobile.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ErpMobile.Api.Data;

namespace Erp.Services
{
    public class TcmbExchangeRateService
    {
        private readonly ILogger<TcmbExchangeRateService> _logger;
        private readonly HttpClient _httpClient;
        private readonly NanoServiceDbContext _dbContext;

        public TcmbExchangeRateService(ILogger<TcmbExchangeRateService> logger, HttpClient httpClient, NanoServiceDbContext dbContext)
        {
            _logger = logger;
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Güncel döviz kurlarını getirir. Önce veritabanından bugünün kurlarını kontrol eder,
        /// yoksa TCMB'den çeker ve veritabanına kaydeder.
        /// </summary>
        public async Task<List<ExchangeRateDto>> GetLatestExchangeRatesAsync()
        {
            try
            {
                // Bugünün tarihini al
                var today = DateTime.Today;
                
                // Önce veritabanında bugünün kurları var mı kontrol et
                var dbRates = await GetExchangeRatesFromDbAsync(today);
                
                // Veritabanında bugünün kurları varsa döndür
                if (dbRates.Any())
                {
                    _logger.LogInformation($"Güncel kur bilgileri veritabanından alındı. Tarih: {today.ToShortDateString()}");
                    return dbRates;
                }
                
                // Veritabanında yoksa TCMB'den çek ve veritabanına kaydet
                _logger.LogInformation("Veritabanında güncel kur bilgisi bulunamadı. TCMB'den alınıyor.");
                var rates = await FetchLatestRatesFromTcmbAsync();
                
                // Kurları veritabanına kaydet
                await SaveExchangeRatesToDbAsync(rates);
                
                return rates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Güncel kur bilgileri alınırken hata oluştu.");
                throw;
            }
        }
        
        /// <summary>
        /// TCMB'den güncel kurları çeker.
        /// </summary>
        private async Task<List<ExchangeRateDto>> FetchLatestRatesFromTcmbAsync()
        {
            // Denenecek URL'lerin listesi
            var urlsToTry = new List<string>
            {
                "https://www.tcmb.gov.tr/kurlar/today.xml",
                "http://www.tcmb.gov.tr/kurlar/today.xml",
                // Alternatif olarak kendi API'mizde önbelleklenmiş bir kopya kullanabiliriz
                // "https://b2b.edikravat.tr/api/cache/tcmb/today.xml"
            };
            
            List<Exception> exceptions = new List<Exception>();
            
            // Her URL'yi sırayla dene
            foreach (var url in urlsToTry)
            {
                try
                {                    
                    _logger.LogInformation($"TCMB kur verileri alınıyor: {url}");
                    
                    // XML'i indir
                    string xmlContent = await _httpClient.GetStringAsync(url);
                    
                    // XML'i parse et
                    var rates = ParseTcmbXml(xmlContent);
                    
                    _logger.LogInformation($"TCMB'den {rates.Count} adet kur bilgisi başarıyla alındı.");
                    
                    return rates;
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogWarning(ex, $"TCMB'den kur bilgileri alınırken HTTP hatası oluştu. URL: {url}");
                    exceptions.Add(ex);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"TCMB'den kur bilgileri alınırken hata oluştu. URL: {url}");
                    exceptions.Add(ex);
                }
            }
            
            // Tüm URL'ler başarısız olursa, son çalışma günü verilerini almayı dene
            try 
            {
                _logger.LogWarning("Güncel kur verileri alınamadı. Son çalışma günü verileri alınıyor.");
                return await FetchLastWorkingDayRatesFromTcmbAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TCMB'den kur bilgileri alınırken tüm denemeler başarısız oldu.");
                throw new AggregateException("TCMB'den kur bilgileri alınamadı.", exceptions);
            }
        }

        /// <summary>
        /// Son çalışma gününe ait döviz kurlarını getirir. Önce veritabanından kontrol eder,
        /// yoksa TCMB'den çeker ve veritabanına kaydeder.
        /// </summary>
        public async Task<List<ExchangeRateDto>> GetLastWorkingDayRatesAsync()
        {
            try
            {
                // Son 10 günü kontrol et
                for (int i = 1; i <= 10; i++)
                {
                    DateTime date = DateTime.Today.AddDays(-i);
                    
                    // Hafta sonu ise atla
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        continue;
                    }
                    
                    // Önce veritabanında bu tarihe ait kurlar var mı kontrol et
                    var dbRates = await GetExchangeRatesFromDbAsync(date);
                    
                    // Veritabanında varsa döndür
                    if (dbRates.Any())
                    {
                        _logger.LogInformation($"Son çalışma günü kur bilgileri veritabanından alındı. Tarih: {date.ToShortDateString()}");
                        return dbRates;
                    }
                    
                    // Veritabanında yoksa TCMB'den çekmeyi dene
                    try
                    {
                        var rates = await FetchExchangeRatesFromTcmbByDateAsync(date);
                        if (rates.Any())
                        {
                            // Kurları veritabanına kaydet
                            await SaveExchangeRatesToDbAsync(rates);
                            return rates;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"{date.ToShortDateString()} tarihli kur bilgileri TCMB'den alınamadı.");
                        // Bir sonraki güne geç
                        continue;
                    }
                }
                
                // Son çare olarak TCMB'den son çalışma günü kurlarını çekmeyi dene
                var lastWorkingDayRates = await FetchLastWorkingDayRatesFromTcmbAsync();
                await SaveExchangeRatesToDbAsync(lastWorkingDayRates);
                return lastWorkingDayRates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Son çalışma günü kur bilgileri alınırken hata oluştu.");
                throw;
            }
        }
        
        /// <summary>
        /// TCMB'den son çalışma gününe ait kurları çeker.
        /// </summary>
        private async Task<List<ExchangeRateDto>> FetchLastWorkingDayRatesFromTcmbAsync()
        {
            try
            {
                // Son 10 günü kontrol et
                for (int i = 1; i <= 10; i++)
                {
                    DateTime date = DateTime.Today.AddDays(-i);
                    
                    // Hafta sonu ise atla
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        continue;
                    }
                    
                    var rates = await FetchExchangeRatesFromTcmbByDateAsync(date);
                    if (rates.Any())
                    {
                        return rates;
                    }
                }
                
                throw new Exception("Son 10 gün içinde çalışma günü kur verisi bulunamadı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TCMB'den son çalışma günü kur bilgileri alınırken hata oluştu.");
                throw;
            }
        }
        
        /// <summary>
        /// Belirli bir tarihe ait kurları TCMB'den çeker.
        /// </summary>
        private async Task<List<ExchangeRateDto>> FetchExchangeRatesFromTcmbByDateAsync(DateTime date)
        {
            string formattedDate = date.ToString("yyyyMMdd");
            string year = date.ToString("yyyy");
            string month = date.ToString("MM");
            
            // TCMB'nin arşiv kurlar XML servisi
            string url = $"https://www.tcmb.gov.tr/kurlar/{year}{month}/{formattedDate}.xml";
            
            try
            {
                // XML'i indir
                string xmlContent = await _httpClient.GetStringAsync(url);
                
                // XML'i parse et
                var rates = ParseTcmbXml(xmlContent);
                
                _logger.LogInformation($"TCMB'den {date.ToShortDateString()} tarihli {rates.Count} adet kur bilgisi başarıyla alındı.");
                
                return rates;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"{date.ToShortDateString()} tarihli kur bilgileri TCMB'den alınamadı.");
                return new List<ExchangeRateDto>();
            }
        }

        private List<ExchangeRateDto> ParseTcmbXml(string xmlContent)
        {
            var rates = new List<ExchangeRateDto>();
            
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            
            var tarih_Date = xmlDoc.SelectSingleNode("//Tarih_Date");
            string date = tarih_Date?.Attributes["Tarih"]?.Value ?? DateTime.Today.ToString("dd.MM.yyyy");
            DateTime exchangeDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            
            var currencies = xmlDoc.SelectNodes("//Currency");
            
            if (currencies != null)
            {
                foreach (XmlNode currency in currencies)
                {
                    string currencyCode = currency.Attributes["CurrencyCode"]?.Value;
                    string currencyName = currency.SelectSingleNode("CurrencyName")?.InnerText;
                    
                    // Alış ve satış kurları
                    decimal.TryParse(currency.SelectSingleNode("ForexBuying")?.InnerText.Replace(".", ","), out decimal forexBuying);
                    decimal.TryParse(currency.SelectSingleNode("ForexSelling")?.InnerText.Replace(".", ","), out decimal forexSelling);
                    decimal.TryParse(currency.SelectSingleNode("BanknoteBuying")?.InnerText.Replace(".", ","), out decimal banknoteBuying);
                    decimal.TryParse(currency.SelectSingleNode("BanknoteSelling")?.InnerText.Replace(".", ","), out decimal banknoteSelling);
                    
                    // Birim değeri
                    int.TryParse(currency.SelectSingleNode("Unit")?.InnerText, out int unit);
                    
                    var rate = new ExchangeRateDto
                    {
                        Date = exchangeDate,
                        CurrencyCode = currencyCode,
                        CurrencyName = currencyName,
                        ForexBuying = forexBuying,
                        ForexSelling = forexSelling,
                        BanknoteBuying = banknoteBuying,
                        BanknoteSelling = banknoteSelling,
                        Unit = unit,
                        Source = "TCMB"
                    };
                    
                    rates.Add(rate);
                }
            }
            
            // TRY'yi de ekle
            rates.Add(new ExchangeRateDto
            {
                Date = exchangeDate,
                CurrencyCode = "TRY",
                CurrencyName = "Türk Lirası",
                ForexBuying = 1,
                ForexSelling = 1,
                BanknoteBuying = 1,
                BanknoteSelling = 1,
                Unit = 1,
                Source = "TCMB"
            });
            
            return rates;
        }
        
        /// <summary>
        /// Belirli bir tarihe ait döviz kurlarını veritabanından getirir.
        /// </summary>
        private async Task<List<ExchangeRateDto>> GetExchangeRatesFromDbAsync(DateTime date)
        {
            try
            {
                // Tarih başlangıcı ve sonu (günün tamamı)
                var startDate = date.Date;
                var endDate = startDate.AddDays(1).AddTicks(-1);
                
                // Veritabanından kurları çek
                var dbRates = await _dbContext.ExchangeRates
                    .Where(r => r.Date >= startDate && r.Date <= endDate)
                    .ToListAsync();
                
                if (!dbRates.Any())
                {
                    return new List<ExchangeRateDto>();
                }
                
                // Entity'leri DTO'lara dönüştür
                var result = new List<ExchangeRateDto>();
                var currencies = dbRates.Select(r => r.CurrencyCode).Distinct().ToList();
                
                foreach (var currencyCode in currencies)
                {
                    // Her döviz kodu için en son kayıtları al
                    var latestRates = dbRates
                        .Where(r => r.CurrencyCode == currencyCode)
                        .OrderByDescending(r => r.Date)
                        .ToList();
                    
                    if (!latestRates.Any()) continue;
                    
                    var dto = new ExchangeRateDto
                    {
                        Date = date,
                        CurrencyCode = currencyCode,
                        CurrencyName = currencyCode, // Tam isim veritabanında yoksa kod kullan
                        Unit = 1,
                        Source = "Database"
                    };
                    
                    // Farklı kur tiplerini bul
                    var forexBuying = latestRates.FirstOrDefault(r => r.Type == "ForexBuying");
                    var forexSelling = latestRates.FirstOrDefault(r => r.Type == "ForexSelling");
                    var banknoteBuying = latestRates.FirstOrDefault(r => r.Type == "BanknoteBuying");
                    var banknoteSelling = latestRates.FirstOrDefault(r => r.Type == "BanknoteSelling");
                    
                    dto.ForexBuying = forexBuying?.Rate ?? 0;
                    dto.ForexSelling = forexSelling?.Rate ?? 0;
                    dto.BanknoteBuying = banknoteBuying?.Rate ?? 0;
                    dto.BanknoteSelling = banknoteSelling?.Rate ?? 0;
                    
                    result.Add(dto);
                }
                
                // TRY yoksa ekle
                if (!result.Any(r => r.CurrencyCode == "TRY"))
                {
                    result.Add(new ExchangeRateDto
                    {
                        Date = date,
                        CurrencyCode = "TRY",
                        CurrencyName = "Türk Lirası",
                        ForexBuying = 1,
                        ForexSelling = 1,
                        BanknoteBuying = 1,
                        BanknoteSelling = 1,
                        Unit = 1,
                        Source = "Database"
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{date.ToShortDateString()} tarihli kur bilgileri veritabanından alınırken hata oluştu.");
                return new List<ExchangeRateDto>();
            }
        }
        
        /// <summary>
        /// Döviz kurlarını veritabanına kaydeder.
        /// </summary>
        private async Task<bool> SaveExchangeRatesToDbAsync(List<ExchangeRateDto> rates)
        {
            if (rates == null || !rates.Any())
            {
                return false;
            }
            
            try
            {
                var date = rates.FirstOrDefault()?.Date ?? DateTime.Today;
                
                // Her döviz için farklı kur tiplerini kaydet
                foreach (var rate in rates)
                {
                    // ForexBuying
                    if (rate.ForexBuying > 0)
                    {
                        var forexBuying = new ExchangeRate
                        {
                            Date = date,
                            CurrencyCode = rate.CurrencyCode,
                            RelationCurrencyCode = "TRY",
                            Rate = rate.ForexBuying,
                            Source = rate.Source ?? "TCMB",
                            Type = "ForexBuying",
                            CreatedAt = DateTime.Now
                        };
                        await _dbContext.ExchangeRates.AddAsync(forexBuying);
                    }
                    
                    // ForexSelling
                    if (rate.ForexSelling > 0)
                    {
                        var forexSelling = new ExchangeRate
                        {
                            Date = date,
                            CurrencyCode = rate.CurrencyCode,
                            RelationCurrencyCode = "TRY",
                            Rate = rate.ForexSelling,
                            Source = rate.Source ?? "TCMB",
                            Type = "ForexSelling",
                            CreatedAt = DateTime.Now
                        };
                        await _dbContext.ExchangeRates.AddAsync(forexSelling);
                    }
                    
                    // BanknoteBuying
                    if (rate.BanknoteBuying > 0)
                    {
                        var banknoteBuying = new ExchangeRate
                        {
                            Date = date,
                            CurrencyCode = rate.CurrencyCode,
                            RelationCurrencyCode = "TRY",
                            Rate = rate.BanknoteBuying,
                            Source = rate.Source ?? "TCMB",
                            Type = "BanknoteBuying",
                            CreatedAt = DateTime.Now
                        };
                        await _dbContext.ExchangeRates.AddAsync(banknoteBuying);
                    }
                    
                    // BanknoteSelling
                    if (rate.BanknoteSelling > 0)
                    {
                        var banknoteSelling = new ExchangeRate
                        {
                            Date = date,
                            CurrencyCode = rate.CurrencyCode,
                            RelationCurrencyCode = "TRY",
                            Rate = rate.BanknoteSelling,
                            Source = rate.Source ?? "TCMB",
                            Type = "BanknoteSelling",
                            CreatedAt = DateTime.Now
                        };
                        await _dbContext.ExchangeRates.AddAsync(banknoteSelling);
                    }
                }
                
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"{date.ToShortDateString()} tarihli {rates.Count} adet döviz kuru veritabanına kaydedildi.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Döviz kurları veritabanına kaydedilirken hata oluştu.");
                return false;
            }
        }
        
        /// <summary>
        /// Günlük olarak TCMB'den kurları çekip veritabanına kaydeder.
        /// Bu metod zamanlayıcı tarafından günde bir kez çağrılmalıdır.
        /// </summary>
        public async Task<bool> SyncExchangeRatesAsync()
        {
            try
            {
                _logger.LogInformation("Günlük döviz kuru senkronizasyonu başlıyor...");
                
                // Bugünün tarihini al
                var today = DateTime.Today;
                
                // Önce veritabanında bugünün kurları var mı kontrol et
                var dbRates = await GetExchangeRatesFromDbAsync(today);
                
                // Veritabanında bugünün kurları varsa güncelleme yapma
                if (dbRates.Any())
                {
                    _logger.LogInformation($"Bugünün ({today.ToShortDateString()}) kurları zaten veritabanında mevcut. Güncelleme yapılmadı.");
                    return true;
                }
                
                // TCMB'den güncel kurları çek
                var rates = await FetchLatestRatesFromTcmbAsync();
                
                if (!rates.Any())
                {
                    _logger.LogWarning("TCMB'den kur bilgileri alınamadı. Senkronizasyon başarısız.");
                    return false;
                }
                
                // Kurları veritabanına kaydet
                var result = await SaveExchangeRatesToDbAsync(rates);
                
                if (result)
                {
                    _logger.LogInformation($"Günlük döviz kuru senkronizasyonu başarıyla tamamlandı. {rates.Count} adet kur güncellendi.");
                }
                else
                {
                    _logger.LogWarning("Döviz kurları veritabanına kaydedilemedi.");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Günlük döviz kuru senkronizasyonu sırasında hata oluştu.");
                return false;
            }
        }
    }
}
