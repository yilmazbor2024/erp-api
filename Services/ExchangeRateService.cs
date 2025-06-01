using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Dto;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Data;

namespace ErpMobile.Api.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly ErpDbContext _erpDbContext;
        private readonly ILogger<ExchangeRateService> _logger;

        public ExchangeRateService(
            ErpDbContext erpDbContext,
            ILogger<ExchangeRateService> logger = null)
        {
            _erpDbContext = erpDbContext;
            _logger = logger;
        }

        public async Task<PagedResult<ExchangeRateDto>> GetExchangeRatesAsync(
            DateTime startDate, 
            DateTime endDate,
            string source,
            int page,
            int pageSize)
        {
            try
            {
                
                _logger?.LogInformation($"Döviz kurları getiriliyor: {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd}, Kaynak: {source}");
                
                // SQL sorgusu oluştur - sadece SELECT işlemi
                string query = @"
                SELECT * FROM (
                SELECT  Date
                      , CurrencyCode
                      , CurrencyDescription			= ISNULL((SELECT CurrencyDescription FROM cdCurrencyDesc WITH(NOLOCK) WHERE cdCurrencyDesc.CurrencyCode = AllExchangeRates.CurrencyCode AND cdCurrencyDesc.LangCode = N'TR'), SPACE(0))
                      , RelationCurrencyCode
                      , [İlişkili Para Birimi Adı]	= ISNULL((SELECT CurrencyDescription FROM cdCurrencyDesc WITH(NOLOCK) WHERE cdCurrencyDesc.CurrencyCode = AllExchangeRates.RelationCurrencyCode AND cdCurrencyDesc.LangCode = N'TR'), SPACE(0))
                      
                      , FreeMarketBuyingRate		= MAX(CASE WHEN ExchangeTypeCode = 1 THEN Rate ELSE 0 END)
                      , FreeMarketSellingRate		= MAX(CASE WHEN ExchangeTypeCode = 2 THEN Rate ELSE 0 END)
                      , CashBuyingRate				= MAX(CASE WHEN ExchangeTypeCode = 3 THEN Rate ELSE 0 END)
                      , CashSellingRate				= MAX(CASE WHEN ExchangeTypeCode = 4 THEN Rate ELSE 0 END)
                      , BanknoteBuyingRate			= MAX(CASE WHEN ExchangeTypeCode = 5 THEN Rate ELSE 0 END)
                      , BanknoteSellingRate			= MAX(CASE WHEN ExchangeTypeCode = 6 THEN Rate ELSE 0 END)
                      , BankForInformationPurposes	= MAX(CASE WHEN ExchangeTypeCode = 7 THEN Rate ELSE 0 END)
                FROM AllExchangeRates
                GROUP BY Date
                      , CurrencyCode
                      , RelationCurrencyCode
                ) Query WHERE ([Date] BETWEEN @startDate AND @endDate)";

                // Kaynak filtresi ekle (bu sorgu için geçerli değil, ama ileride gerekebilir)
                if (!string.IsNullOrEmpty(source))
                {
                    // Bu sorgu için kaynak filtresi uygulanmıyor, çünkü AllExchangeRates tablosunda Source kolonu yok
                    // Bunun yerine ExchangeTypeCode kullanılıyor
                    _logger?.LogWarning("Kaynak filtresi AllExchangeRates tablosunda desteklenmiyor");
                }

                query += @" ORDER BY Date DESC, CurrencyCode
                    OFFSET @offset ROWS
                    FETCH NEXT @pageSize ROWS ONLY";

                // Parametreleri oluştur
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@startDate", startDate),
                    new SqlParameter("@endDate", endDate),
                    new SqlParameter("@offset", (page - 1) * pageSize),
                    new SqlParameter("@pageSize", pageSize)
                };

                // Kaynak parametresi ekle
                if (!string.IsNullOrEmpty(source))
                {
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[parameters.Length - 1] = new SqlParameter("@source", source);
                }

                // Toplam kayıt sayısını almak için sorgu
                string countQuery = @"
                    SELECT COUNT(*) FROM (
                    SELECT DISTINCT Date, CurrencyCode, RelationCurrencyCode
                    FROM AllExchangeRates
                    WHERE Date BETWEEN @startDate AND @endDate
                    ) CountQuery";

                // Kaynak filtresi ekle (bu sorgu için geçerli değil)

                // Verileri çek
                var dataTable = await _erpDbContext.ExecuteQueryAsync(query, parameters);
                
                // Toplam kayıt sayısını al
                var totalCountObj = await _erpDbContext.ExecuteScalarAsync(countQuery, parameters);
                int totalCount = totalCountObj != null ? Convert.ToInt32(totalCountObj) : 0;
                
                if (dataTable.Rows.Count == 0)
                {
                    _logger?.LogWarning("Veritabanında döviz kuru verisi bulunamadı");
                    return new PagedResult<ExchangeRateDto>
                    {
                        Items = new List<ExchangeRateDto>(),
                        TotalCount = 0,
                        PageNumber = page,
                        PageSize = pageSize
                    };
                }
                
                // DataTable'ı DTO listesine dönüştür
                var items = new List<ExchangeRateDto>();
                
                foreach (DataRow row in dataTable.Rows)
                {
                    var dto = new ExchangeRateDto
                    {
                        Date = Convert.ToDateTime(row["Date"]),
                        CurrencyCode = row["CurrencyCode"].ToString(),
                        RelationCurrencyCode = row["RelationCurrencyCode"].ToString(),
                        // Para birimi açıklamaları doğrudan sorgudan al
                        CurrencyDescription = row["CurrencyDescription"] != DBNull.Value ? row["CurrencyDescription"].ToString() : row["CurrencyCode"].ToString(),
                        RelationCurrencyDescription = row["İlişkili Para Birimi Adı"] != DBNull.Value ? row["İlişkili Para Birimi Adı"].ToString() : row["RelationCurrencyCode"].ToString(),
                        FreeMarketBuyingRate = row["FreeMarketBuyingRate"] != DBNull.Value ? Convert.ToDecimal(row["FreeMarketBuyingRate"]) : 0,
                        FreeMarketSellingRate = row["FreeMarketSellingRate"] != DBNull.Value ? Convert.ToDecimal(row["FreeMarketSellingRate"]) : 0,
                        CashBuyingRate = row["CashBuyingRate"] != DBNull.Value ? Convert.ToDecimal(row["CashBuyingRate"]) : 0,
                        CashSellingRate = row["CashSellingRate"] != DBNull.Value ? Convert.ToDecimal(row["CashSellingRate"]) : 0,
                        BanknoteBuyingRate = row["BanknoteBuyingRate"] != DBNull.Value ? Convert.ToDecimal(row["BanknoteBuyingRate"]) : 0,
                        BanknoteSellingRate = row["BanknoteSellingRate"] != DBNull.Value ? Convert.ToDecimal(row["BanknoteSellingRate"]) : 0,
                        BankForInformationPurposes = row["BankForInformationPurposes"] != DBNull.Value ? Convert.ToDecimal(row["BankForInformationPurposes"]) : 0,
                        Source = string.IsNullOrEmpty(source) ? null : source
                    };
                    
                    items.Add(dto);
                }

                return new PagedResult<ExchangeRateDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = page,
                    PageSize = pageSize
                    // TotalPages özelliği readonly olduğu için burada atama yapılamaz
                    // TotalPages otomatik olarak hesaplanacak
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Döviz kurları getirilirken hata oluştu: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ExchangeRateDto>> GetExchangeRatesByDateAsync(
            DateTime date,
            string source = null)
        {
            try
            {
                // DateTime parametresi artık nullable değil
                
                _logger?.LogInformation($"Tarih için döviz kurları getiriliyor: {date:yyyy-MM-dd}, Kaynak: {source}");
                
                // SQL sorgusu oluştur - sadece SELECT işlemi
                string query = @"
                SELECT * FROM (
                SELECT  Date
                      , CurrencyCode
                      , CurrencyDescription			= ISNULL((SELECT CurrencyDescription FROM cdCurrencyDesc WITH(NOLOCK) WHERE cdCurrencyDesc.CurrencyCode = AllExchangeRates.CurrencyCode AND cdCurrencyDesc.LangCode = N'TR'), SPACE(0))
                      , RelationCurrencyCode
                      , [İlişkili Para Birimi Adı]	= ISNULL((SELECT CurrencyDescription FROM cdCurrencyDesc WITH(NOLOCK) WHERE cdCurrencyDesc.CurrencyCode = AllExchangeRates.RelationCurrencyCode AND cdCurrencyDesc.LangCode = N'TR'), SPACE(0))
                      
                      , FreeMarketBuyingRate		= MAX(CASE WHEN ExchangeTypeCode = 1 THEN Rate ELSE 0 END)
                      , FreeMarketSellingRate		= MAX(CASE WHEN ExchangeTypeCode = 2 THEN Rate ELSE 0 END)
                      , CashBuyingRate				= MAX(CASE WHEN ExchangeTypeCode = 3 THEN Rate ELSE 0 END)
                      , CashSellingRate				= MAX(CASE WHEN ExchangeTypeCode = 4 THEN Rate ELSE 0 END)
                      , BanknoteBuyingRate			= MAX(CASE WHEN ExchangeTypeCode = 5 THEN Rate ELSE 0 END)
                      , BanknoteSellingRate			= MAX(CASE WHEN ExchangeTypeCode = 6 THEN Rate ELSE 0 END)
                      , BankForInformationPurposes	= MAX(CASE WHEN ExchangeTypeCode = 7 THEN Rate ELSE 0 END)
                FROM AllExchangeRates
                GROUP BY Date
                      , CurrencyCode
                      , RelationCurrencyCode
                ) Query WHERE ([Date] = @date)";

                // Kaynak filtresi ekle (bu sorgu için geçerli değil, ama ileride gerekebilir)
                if (!string.IsNullOrEmpty(source))
                {
                    // Bu sorgu için kaynak filtresi uygulanmıyor, çünkü AllExchangeRates tablosunda Source kolonu yok
                    // Bunun yerine ExchangeTypeCode kullanılıyor
                    _logger?.LogWarning("Kaynak filtresi AllExchangeRates tablosunda desteklenmiyor");
                }

                query += " ORDER BY CurrencyCode";

                // Parametreleri oluştur
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@date", date)
                };

                // Kaynak parametresi ekle
                if (!string.IsNullOrEmpty(source))
                {
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[parameters.Length - 1] = new SqlParameter("@source", source);
                }

                // Verileri çek
                var dataTable = await _erpDbContext.ExecuteQueryAsync(query, parameters);
                
                if (dataTable.Rows.Count == 0)
                {
                    _logger?.LogWarning($"{date.ToString("yyyy-MM-dd")} tarihi için döviz kuru verisi bulunamadı");
                    return new List<ExchangeRateDto>();
                }
                
                // DataTable'ı DTO listesine dönüştür
                var items = new List<ExchangeRateDto>();
                
                foreach (DataRow row in dataTable.Rows)
                {
                    var dto = new ExchangeRateDto
                    {
                        Date = Convert.ToDateTime(row["Date"]),
                        CurrencyCode = row["CurrencyCode"].ToString(),
                        RelationCurrencyCode = row["RelationCurrencyCode"].ToString(),
                        // Para birimi açıklamaları doğrudan sorgudan al
                        CurrencyDescription = row["CurrencyDescription"] != DBNull.Value ? row["CurrencyDescription"].ToString() : row["CurrencyCode"].ToString(),
                        RelationCurrencyDescription = row["İlişkili Para Birimi Adı"] != DBNull.Value ? row["İlişkili Para Birimi Adı"].ToString() : row["RelationCurrencyCode"].ToString(),
                        FreeMarketBuyingRate = row["FreeMarketBuyingRate"] != DBNull.Value ? Convert.ToDecimal(row["FreeMarketBuyingRate"]) : 0,
                        FreeMarketSellingRate = row["FreeMarketSellingRate"] != DBNull.Value ? Convert.ToDecimal(row["FreeMarketSellingRate"]) : 0,
                        CashBuyingRate = row["CashBuyingRate"] != DBNull.Value ? Convert.ToDecimal(row["CashBuyingRate"]) : 0,
                        CashSellingRate = row["CashSellingRate"] != DBNull.Value ? Convert.ToDecimal(row["CashSellingRate"]) : 0,
                        BanknoteBuyingRate = row["BanknoteBuyingRate"] != DBNull.Value ? Convert.ToDecimal(row["BanknoteBuyingRate"]) : 0,
                        BanknoteSellingRate = row["BanknoteSellingRate"] != DBNull.Value ? Convert.ToDecimal(row["BanknoteSellingRate"]) : 0,
                        BankForInformationPurposes = row["BankForInformationPurposes"] != DBNull.Value ? Convert.ToDecimal(row["BankForInformationPurposes"]) : 0,
                        Source = string.IsNullOrEmpty(source) ? null : source
                    };
                    
                    items.Add(dto);
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Tarih için döviz kurları getirilirken hata oluştu: {ex.Message}");
                throw;
            }
        }
        
        public async Task<decimal> GetConversionRateAsync(string fromCurrency, string toCurrency, DateTime date, string source = null)
        {
            try
            {
                // DateTime parametresi artık nullable değil
                source = string.IsNullOrEmpty(source) ? "FreeMarket" : source;
                
                _logger?.LogInformation($"Dönüşüm kuru getiriliyor: {fromCurrency} -> {toCurrency}, Tarih: {date:yyyy-MM-dd}, Kaynak: {source}");
                
                // TRY -> TRY durumunda 1 döndür
                if (fromCurrency == toCurrency)
                {
                    return 1m;
                }
                
                // TRY -> Yabancı Para dönüşümü
                if (fromCurrency == "TRY")
                {
                    // Alış kuru kullanarak TRY'den yabancı paraya çevirme
                    string query = @"
                        SELECT TOP 1 BanknoteBuyingRate 
                        FROM AllExchangeRates 
                        WHERE Date = @date 
                        AND CurrencyCode = @currencyCode 
                        AND RelationCurrencyCode = 'TRY'";
                    
                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@date", date),
                        new SqlParameter("@currencyCode", toCurrency)
                    };
                    
                    var rateObj = await _erpDbContext.ExecuteScalarAsync(query, parameters);
                    decimal rate = rateObj != null ? Convert.ToDecimal(rateObj) : 0;
                    
                    _logger?.LogInformation($"TRY -> {toCurrency} dönüşüm kuru: {rate}");
                    
                    // TRY'den yabancı paraya çevirirken, 1 TRY kaç birim yabancı para olduğunu hesapla
                    return rate > 0 ? 1 / rate : 0;
                }
                
                // Yabancı Para -> TRY dönüşümü
                if (toCurrency == "TRY")
                {
                    // Satış kuru kullanarak yabancı paradan TRY'ye çevirme
                    string query = @"
                        SELECT TOP 1 BanknoteSellingRate 
                        FROM AllExchangeRates 
                        WHERE Date = @date 
                        AND CurrencyCode = @currencyCode 
                        AND RelationCurrencyCode = 'TRY'";
                    
                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@date", date),
                        new SqlParameter("@currencyCode", fromCurrency)
                    };
                    
                    var rateObj = await _erpDbContext.ExecuteScalarAsync(query, parameters);
                    decimal rate = rateObj != null ? Convert.ToDecimal(rateObj) : 0;
                    
                    _logger?.LogInformation($"{fromCurrency} -> TRY dönüşüm kuru: {rate}");
                    
                    return rate;
                }
                
                // Yabancı Para -> Yabancı Para dönüşümü (çapraz kur)
                // İlk para biriminden TRY'ye, sonra TRY'den ikinci para birimine dönüşüm yapılır
                
                // İlk para biriminden TRY'ye dönüşüm (satış kuru)
                string fromRateQuery = @"
                    SELECT TOP 1 BanknoteSellingRate 
                    FROM AllExchangeRates 
                    WHERE Date = @date 
                    AND CurrencyCode = @currencyCode 
                    AND RelationCurrencyCode = 'TRY'";
                
                var fromRateParameters = new SqlParameter[]
                {
                    new SqlParameter("@date", date),
                    new SqlParameter("@currencyCode", fromCurrency)
                };
                
                var fromRateObj = await _erpDbContext.ExecuteScalarAsync(fromRateQuery, fromRateParameters);
                decimal fromRate = fromRateObj != null ? Convert.ToDecimal(fromRateObj) : 0;
                
                _logger?.LogInformation($"{fromCurrency} -> TRY dönüşüm kuru: {fromRate}");
                
                if (fromRate <= 0)
                {
                    _logger?.LogWarning($"{fromCurrency} için dönüşüm kuru bulunamadı.");
                    return 0;
                }
                
                // TRY'den ikinci para birimine dönüşüm (alış kuru)
                string toRateQuery = @"
                    SELECT TOP 1 BanknoteBuyingRate 
                    FROM AllExchangeRates 
                    WHERE Date = @date 
                    AND CurrencyCode = @currencyCode 
                    AND RelationCurrencyCode = 'TRY'";
                
                var toRateParameters = new SqlParameter[]
                {
                    new SqlParameter("@date", date),
                    new SqlParameter("@currencyCode", toCurrency)
                };
                
                var toRateObj = await _erpDbContext.ExecuteScalarAsync(toRateQuery, toRateParameters);
                decimal toRate = toRateObj != null ? Convert.ToDecimal(toRateObj) : 0;
                
                _logger?.LogInformation($"TRY -> {toCurrency} dönüşüm kuru: {toRate}");
                
                if (toRate <= 0)
                {
                    _logger?.LogWarning($"{toCurrency} için dönüşüm kuru bulunamadı.");
                    return 0;
                }
                
                // Çapraz kur hesaplama: fromCurrency/TRY * TRY/toCurrency = fromCurrency/toCurrency
                decimal crossRate = fromRate / toRate;
                _logger?.LogInformation($"Çapraz kur hesaplandı: {fromCurrency}/{toCurrency} = {crossRate}");
                
                return crossRate;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger?.LogError(sqlEx, $"SQL hatası oluştu: {sqlEx.Message}, Hata Kodu: {sqlEx.Number}, Durum: {sqlEx.State}, Sunucu: {sqlEx.Server}");
                // Veritabanı bağlantı hatası durumunda döviz kurları listesinden hesaplamayı dene
                return await CalculateConversionRateFromExchangeRatesList(fromCurrency, toCurrency, date, source);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Dönüşüm kuru getirilirken hata oluştu: {ex.Message}");
                // Genel hata durumunda döviz kurları listesinden hesaplamayı dene
                return await CalculateConversionRateFromExchangeRatesList(fromCurrency, toCurrency, date, source);
            }
        }

        /// <summary>
        /// Döviz kurları listesinden dönüşüm oranını hesaplar
        /// </summary>
        /// <param name="fromCurrency">Kaynak para birimi</param>
        /// <param name="toCurrency">Hedef para birimi</param>
        /// <param name="date">Tarih</param>
        /// <param name="source">Kaynak</param>
        /// <returns>Dönüşüm oranı</returns>
        private async Task<decimal> CalculateConversionRateFromExchangeRatesList(string fromCurrency, string toCurrency, DateTime date, string source = null)
        {
            try
            {
                // Döviz kurları listesini al
                var exchangeRates = await GetExchangeRatesByDateAsync(date, source);
                if (exchangeRates == null || !exchangeRates.Any())
                {
                    _logger?.LogWarning($"Döviz kurları bulunamadı: {date:yyyy-MM-dd}");
                    return 0;
                }

                // TRY -> Yabancı Para dönüşümü
                if (fromCurrency == "TRY")
                {
                    var toRate = exchangeRates.FirstOrDefault(r => r.CurrencyCode == toCurrency && r.RelationCurrencyCode == "TRY");
                    if (toRate == null)
                    {
                        _logger?.LogWarning($"Hedef para birimi için kur bulunamadı: {toCurrency}");
                        return 0;
                    }
                    
                    return toRate.BanknoteBuyingRate > 0 ? 1 / toRate.BanknoteBuyingRate.GetValueOrDefault() : 0;
                }

                // Yabancı Para -> TRY dönüşümü
                if (toCurrency == "TRY")
                {
                    var fromRate = exchangeRates.FirstOrDefault(r => r.CurrencyCode == fromCurrency && r.RelationCurrencyCode == "TRY");
                    if (fromRate == null)
                    {
                        _logger?.LogWarning($"Kaynak para birimi için kur bulunamadı: {fromCurrency}");
                        return 0;
                    }
                    
                    return fromRate.BanknoteSellingRate.GetValueOrDefault();
                }

                // Yabancı Para -> Yabancı Para dönüşümü (çapraz kur)
                var fromCurrencyRate = exchangeRates.FirstOrDefault(r => r.CurrencyCode == fromCurrency && r.RelationCurrencyCode == "TRY");
                var toCurrencyRate = exchangeRates.FirstOrDefault(r => r.CurrencyCode == toCurrency && r.RelationCurrencyCode == "TRY");

                if (fromCurrencyRate == null || toCurrencyRate == null)
                {
                    _logger?.LogWarning($"Kaynak veya hedef para birimi için kur bulunamadı: {fromCurrency}/{toCurrency}");
                    return 0;
                }

                if (fromCurrencyRate.BanknoteSellingRate.GetValueOrDefault() <= 0 || toCurrencyRate.BanknoteBuyingRate.GetValueOrDefault() <= 0)
                {
                    _logger?.LogWarning($"Kaynak veya hedef para birimi için geçerli kur bulunamadı: {fromCurrency}/{toCurrency}");
                    return 0;
                }

                // Çapraz kur hesaplama: (fromCurrency/TRY) / (toCurrency/TRY)
                var crossRate = fromCurrencyRate.BanknoteSellingRate.GetValueOrDefault() / toCurrencyRate.BanknoteBuyingRate.GetValueOrDefault();
                _logger?.LogInformation($"Çapraz kur hesaplandı (liste): {fromCurrency}/{toCurrency} = {crossRate}");
                
                return crossRate;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Döviz kurları listesinden dönüşüm oranı hesaplanırken hata oluştu: {ex.Message}");
                return 0;
            }
        }
    }
}