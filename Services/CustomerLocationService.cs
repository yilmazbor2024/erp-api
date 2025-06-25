using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Dapper;
using ErpMobile.Api.Models.Customer;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Interfaces;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Müşteri konum işlemleri için servis sınıfı
    /// </summary>
    public class CustomerLocationService : ICustomerLocationService
    {
        private readonly ILogger<CustomerLocationService> _logger;
        private readonly IConfiguration _configuration;

        public CustomerLocationService(ILogger<CustomerLocationService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<RegionResponse>> GetRegionsAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    try
                    {
                        var sql = @"
                            SELECT 
                                s.StateCode AS RegionCode,
                                sd.StateDescription AS RegionDescription,
                                s.IsBlocked
                            FROM cdState s WITH(NOLOCK)
                            LEFT JOIN cdStateDesc sd WITH(NOLOCK) ON sd.StateCode = s.StateCode AND sd.LangCode = 'TR'
                            ORDER BY s.StateCode";

                        var result = await connection.QueryAsync<RegionResponse>(sql);
                        return result.ToList();
                    }
                    catch (SqlException ex) // Catch specific SQL errors
                    {
                        _logger.LogError(ex, "SQL error occurred while getting regions. Will return empty list.");
                        return new List<RegionResponse>(); // Return empty on SQL error
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting regions. Will return empty list.");
                return new List<RegionResponse>(); // Return empty on general error
            }
        }

  
        public async Task<List<StateResponse>> GetStatesAsync(string countryCode = null)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            s.StateCode,
                            sd.StateDescription,
                            s.CountryCode,
                            cd.CountryDescription,
                            s.IsBlocked
                        FROM cdState s WITH(NOLOCK)
                        LEFT JOIN cdStateDesc sd WITH(NOLOCK) ON sd.StateCode = s.StateCode AND sd.LangCode = 'TR'
                        LEFT JOIN cdCountryDesc cd WITH(NOLOCK) ON cd.CountryCode = s.CountryCode AND cd.LangCode = 'TR'";
                    
                    if (!string.IsNullOrEmpty(countryCode))
                    {
                        sql += " WHERE s.CountryCode = @CountryCode";
                    }
                    
                    sql += " ORDER BY s.StateCode";

                    var result = await connection.QueryAsync<StateResponse>(sql, new { CountryCode = countryCode });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting states. CountryCode: {CountryCode}. Will return empty list.", countryCode);
                return new List<StateResponse>(); // Return empty on error
            }
        }

        public async Task<List<CityResponse>> GetCitiesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    try
                    {
                        var sql = @"
                            SELECT 
                                c.CityCode,
                                cd.CityDescription,
                                c.StateCode,
                                sd.StateDescription,
                                c.IsBlocked
                            FROM cdCity c WITH(NOLOCK)
                            LEFT JOIN cdCityDesc cd WITH(NOLOCK) ON cd.CityCode = c.CityCode AND cd.LangCode = 'TR'
                            LEFT JOIN cdStateDesc sd WITH(NOLOCK) ON sd.StateCode = c.StateCode AND sd.LangCode = 'TR'
                            ORDER BY c.CityCode";

                        var result = await connection.QueryAsync<CityResponse>(sql);
                        return result.ToList();
                    }
                    catch (SqlException ex) // Catch specific SQL errors
                    {
                        _logger.LogError(ex, "SQL error occurred while getting cities. Will return empty list.");
                        return new List<CityResponse>(); // Return empty on SQL error
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting cities. Will return empty list.");
                return new List<CityResponse>(); // Return empty on general error
            }
        }

        public async Task<List<CityResponse>> GetCitiesByStateAsync(string stateCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    try
                    {
                        var sql = @"
                            SELECT 
                                c.CityCode,
                                cd.CityDescription,
                                c.StateCode,
                                sd.StateDescription,
                                c.IsBlocked
                            FROM cdCity c WITH(NOLOCK)
                            LEFT JOIN cdCityDesc cd WITH(NOLOCK) ON cd.CityCode = c.CityCode AND cd.LangCode = 'TR'
                            LEFT JOIN cdStateDesc sd WITH(NOLOCK) ON sd.StateCode = c.StateCode AND sd.LangCode = 'TR'
                            WHERE c.StateCode = @StateCode
                            ORDER BY c.CityCode";

                        var result = await connection.QueryAsync<CityResponse>(sql, new { StateCode = stateCode });
                        return result.ToList();
                    }
                    catch (SqlException ex) // Catch specific SQL errors
                    {
                        _logger.LogError(ex, "SQL error occurred while getting cities by state. StateCode: {StateCode}. Will return empty list.", stateCode);
                        return new List<CityResponse>(); // Return empty on SQL error
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting cities by state. StateCode: {StateCode}. Will return empty list.", stateCode);
                return new List<CityResponse>(); // Return empty on general error
            }
        }

        public async Task<List<CityResponse>> GetCitiesByRegionAsync(string stateCode)
        {
            // Bu metot artık GetCitiesByStateAsync ile aynı işlevi görüyor
            // Geriye dönük uyumluluk için tutuyoruz
            return await GetCitiesByStateAsync(stateCode);
        }

        public async Task<List<DistrictResponse>> GetDistrictsByCityAsync(string cityCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    try
                    {
                        var sql = @"
                            SELECT 
                                d.DistrictCode,
                                dd.DistrictDescription,
                                d.CityCode,
                                cd.CityDescription,
                                d.IsBlocked
                            FROM cdDistrict d WITH(NOLOCK)
                            LEFT JOIN cdDistrictDesc dd WITH(NOLOCK) ON dd.DistrictCode = d.DistrictCode AND dd.LangCode = 'TR'
                            LEFT JOIN cdCityDesc cd WITH(NOLOCK) ON cd.CityCode = d.CityCode AND cd.LangCode = 'TR'
                            WHERE d.CityCode = @CityCode
                            ORDER BY d.DistrictCode";

                        var result = await connection.QueryAsync<DistrictResponse>(sql, new { CityCode = cityCode });
                        return result.ToList();
                    }
                    catch (SqlException ex) // Catch specific SQL errors
                    {
                        _logger.LogError(ex, "SQL error occurred while getting districts by city. CityCode: {CityCode}. Will return empty list.", cityCode);
                        return new List<DistrictResponse>(); // Return empty on SQL error
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting districts by city. CityCode: {CityCode}. Will return empty list.", cityCode);
                return new List<DistrictResponse>(); // Return empty on general error
            }
        }

        public async Task<List<DistrictResponse>> GetAllDistrictsAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    try
                    {
                        var sql = @"
                            SELECT 
                                d.DistrictCode,
                                dd.DistrictDescription,
                                d.CityCode,
                                cd.CityDescription,
                                d.IsBlocked
                            FROM cdDistrict d WITH(NOLOCK)
                            LEFT JOIN cdDistrictDesc dd WITH(NOLOCK) ON dd.DistrictCode = d.DistrictCode AND dd.LangCode = 'TR'
                            LEFT JOIN cdCityDesc cd WITH(NOLOCK) ON cd.CityCode = d.CityCode AND cd.LangCode = 'TR'
                            ORDER BY d.DistrictCode";

                        var result = await connection.QueryAsync<DistrictResponse>(sql);
                        return result.ToList();
                    }
                    catch (SqlException ex) // Catch specific SQL errors
                    {
                        _logger.LogError(ex, "SQL error occurred while getting all districts. Will return empty list.");
                        return new List<DistrictResponse>(); // Return empty on SQL error
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all districts. Will return empty list.");
                return new List<DistrictResponse>(); // Return empty on general error
            }
        }
        
        /// <summary>
        /// Banka hesaplarını getirir
        /// </summary>
        public async Task<List<ErpMobile.Api.Models.Responses.BankAccountResponse>> GetBankAccountsAsync(string customerCode = null)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            CAST(ba.BankAccountID AS UNIQUEIDENTIFIER) AS Id,
                            ISNULL(ca.CurrAccCode, '') AS CustomerCode,
                            ba.BankCode,
                            bd.BankDescription AS BankName,
                            ba.BranchCode,
                            brd.BranchDescription AS BranchName,
                            ba.AccountNo AS AccountNumber,
                            ba.IBAN,
                            ba.CurrencyCode,
                            CASE WHEN ba.IsBlocked = 0 THEN 1 ELSE 0 END AS IsActive,
                            CASE WHEN bad.IsDefault = 1 THEN 1 ELSE 0 END AS IsDefault
                        FROM cdBankAccount ba WITH(NOLOCK)
                        LEFT JOIN cdBankAccountDesc bad WITH(NOLOCK) ON bad.BankAccountCode = ba.BankAccountCode AND bad.LangCode = 'TR'
                        LEFT JOIN cdBankDesc bd WITH(NOLOCK) ON bd.BankCode = ba.BankCode AND bd.LangCode = 'TR'
                        LEFT JOIN cdBankBranchDesc brd WITH(NOLOCK) ON brd.BankCode = ba.BankCode AND brd.BranchCode = ba.BranchCode AND brd.LangCode = 'TR'
                        LEFT JOIN cdCurrAcc ca WITH(NOLOCK) ON ca.CurrAccCode = ba.CurrAccCode
                        WHERE (@CustomerCode IS NULL OR ca.CurrAccCode = @CustomerCode)
                        ORDER BY ba.BankAccountCode";

                    var result = await connection.QueryAsync<ErpMobile.Api.Models.Responses.BankAccountResponse>(sql, new { CustomerCode = customerCode });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting bank accounts. CustomerCode: {CustomerCode}. Will return empty list.", customerCode);
                return new List<ErpMobile.Api.Models.Responses.BankAccountResponse>(); // Return empty on error
            }
        }

        /// <summary>
        /// Vergi dairelerini getirir
        /// </summary>
        public async Task<List<TaxOfficeResponse>> GetTaxOfficesAsync()
        {
            try
            {
                string langCode = "TR"; // Sabit dil kodu kullan
                _logger.LogInformation("GetTaxOfficesAsync başlatılıyor. LangCode: {LangCode}", langCode);
                
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    _logger.LogInformation("Veritabanı bağlantısı açıldı");

                    var sql = @"
                        SELECT 
                            t.TaxOfficeCode,
                            tod.TaxOfficeDescription,
                            t.CityCode,
                            cd.CityDescription,
                            t.IsBlocked
                        FROM cdTaxOffice AS t WITH (NOLOCK)
                        LEFT JOIN cdTaxOfficeDesc AS tod WITH (NOLOCK) ON tod.TaxOfficeCode = t.TaxOfficeCode AND tod.LangCode = @LangCode
                        LEFT JOIN cdCityDesc AS cd WITH (NOLOCK) ON cd.CityCode = t.CityCode AND cd.LangCode = @LangCode
                        ORDER BY t.TaxOfficeCode";

                    _logger.LogInformation("SQL sorgusu çalıştırılıyor: {Sql}", sql);
                    var result = await connection.QueryAsync<TaxOfficeResponse>(sql, new { LangCode = langCode });
                    var resultList = result.ToList();
                    _logger.LogInformation("Vergi daireleri başarıyla alındı. Kayıt sayısı: {Count}", resultList.Count);
                    
                    if (resultList.Count > 0)
                    {
                        _logger.LogInformation("Vergi dairesi örnek kayıt: TaxOfficeCode={TaxOfficeCode}, TaxOfficeDescription={TaxOfficeDescription}", 
                            resultList[0].TaxOfficeCode, resultList[0].TaxOfficeDescription);
                    }
                    else
                    {
                        _logger.LogWarning("Vergi dairesi kaydı bulunamadı!");
                    }
                    
                    return resultList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting tax offices. Will return empty list.");
                return new List<TaxOfficeResponse>(); // Return empty on general error
            }
        }
    }
}
