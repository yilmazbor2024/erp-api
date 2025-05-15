using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Data;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Geliştirilmiş müşteri işlemleri için servis sınıfı
    /// </summary>
    public class CustomerServiceNew : ICustomerServiceNew
    {
        private readonly ILogger<CustomerServiceNew> _logger;
        private readonly ErpDbContext _context;
        private readonly IConfiguration _configuration;

        public CustomerServiceNew(ILogger<CustomerServiceNew> logger, ErpDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Yeni müşteri oluşturur
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        public async Task<CustomerCreateResponseNew> CreateCustomerAsync(CustomerCreateRequestNew request)
        {
            if (request == null)
            {
                return new CustomerCreateResponseNew
                {
                    Success = false,
                    Message = "Geçersiz istek"
                };
            }

            try
            {
                _logger.LogInformation("Yeni müşteri oluşturuluyor: {CustomerCode}", request.CustomerCode);

                // Müşteri kodu elle verilmişse, doğrudan kullan
                if (string.IsNullOrEmpty(request.CustomerCode))
                {
                    // Müşteri kodu otomatik oluşturulacak
                    try
                    {
                        // Veritabanı bağlantısı
                        using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                        {
                            await connection.OpenAsync();
                            
                            // Müşteri tipi koduna göre önek belirle
                            string prefix;
                            int nextNumber = 1;
                            
                            if (request.CustomerTypeCode == 3) // Müşteri
                            {
                                // Önce 120. ile başlayan son kodu kontrol et
                                string query = "SELECT TOP 1 CurrAccCode FROM cdCurrAcc WHERE CurrAccCode LIKE '120.%' ORDER BY CurrAccCode DESC";
                                string lastCode = await connection.QueryFirstOrDefaultAsync<string>(query);
                                
                                if (!string.IsNullOrEmpty(lastCode))
                                {
                                    // "120." sonrasını sayısal değere çevir
                                    string lastNumberStr = lastCode.Substring(4);
                                    if (int.TryParse(lastNumberStr, out int lastNum))
                                    {
                                        // Eğer 999'dan büyükse, 121 serisine geç
                                        if (lastNum >= 999)
                                        {
                                            // 121. ile başlayan son kodu kontrol et
                                            query = "SELECT TOP 1 CurrAccCode FROM cdCurrAcc WHERE CurrAccCode LIKE '121.%' ORDER BY CurrAccCode DESC";
                                            string lastCode121 = await connection.QueryFirstOrDefaultAsync<string>(query);
                                            
                                            if (!string.IsNullOrEmpty(lastCode121))
                                            {
                                                // "121." sonrasını sayısal değere çevir
                                                string lastNum121Str = lastCode121.Substring(4);
                                                if (int.TryParse(lastNum121Str, out int lastNum121))
                                                {
                                                    prefix = "121.";
                                                    nextNumber = lastNum121 + 1;
                                                }
                                                else
                                                {
                                                    prefix = "121.";
                                                    nextNumber = 1;
                                                }
                                            }
                                            else
                                            {
                                                prefix = "121.";
                                                nextNumber = 1;
                                            }
                                        }
                                        else
                                        {
                                            // 120 serisinde devam et
                                            prefix = "120.";
                                            nextNumber = lastNum + 1;
                                        }
                                    }
                                    else
                                    {
                                        prefix = "120.";
                                        nextNumber = 1;
                                    }
                                }
                                else
                                {
                                    prefix = "120.";
                                    nextNumber = 1;
                                }
                            }
                            else // Tedarikçi
                            {
                                prefix = "320.";
                                string query = $"SELECT TOP 1 CurrAccCode FROM cdCurrAcc WHERE CurrAccCode LIKE '{prefix}%' ORDER BY CurrAccCode DESC";
                                string lastCode = await connection.QueryFirstOrDefaultAsync<string>(query);
                                
                                if (!string.IsNullOrEmpty(lastCode))
                                {
                                    // "320." sonrasını sayısal değere çevir
                                    string lastNumberStr = lastCode.Substring(4);
                                    if (int.TryParse(lastNumberStr, out int lastNum))
                                    {
                                        nextNumber = lastNum + 1;
                                    }
                                }
                            }
                            
                            // Yeni kodu ayarla
                            string newCode;
                            if (nextNumber < 1000)
                            {
                                newCode = $"{prefix}{nextNumber:000}";
                            }
                            else
                            {
                                newCode = $"{prefix}{nextNumber}";
                            }
                            
                            request.CustomerCode = newCode;
                            _logger.LogInformation("Otomatik müşteri kodu oluşturuldu: {CustomerCode}", request.CustomerCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Müşteri kodu otomatik oluşturulurken hata oluştu");
                        return new CustomerCreateResponseNew
                        {
                            Success = false,
                            Message = "Müşteri kodu otomatik oluşturulurken hata oluştu",
                            ErrorDetails = ex.Message
                        };
                    }
                }
                else
                {
                    _logger.LogInformation("Müşteri kodu elle verildi: {CustomerCode}", request.CustomerCode);
                }

                // Veritabanı bağlantısı
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Müşteri zaten var mı kontrol et
                    var existingCustomer = await connection.QueryFirstOrDefaultAsync<int>(
                        "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode",
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = request.CustomerTypeCode });

                    if (existingCustomer > 0)
                    {
                        return new CustomerCreateResponseNew
                        {
                            Success = false,
                            Message = "Bu müşteri kodu zaten kullanılıyor",
                            ErrorDetails = $"Müşteri kodu '{request.CustomerCode}' zaten kullanımda"
                        };
                    }

                    // Müşteri oluşturma için parametreleri hazırla
                    var parameters = new DynamicParameters();
                    parameters.Add("@CurrAccTypeCode", request.CustomerTypeCode);
                    parameters.Add("@CurrAccCode", request.CustomerCode);
                    parameters.Add("@FirstName", request.CustomerName);
                    parameters.Add("@LastName", request.CustomerSurname);
                    parameters.Add("@CompanyCode", request.CompanyCode);
                    parameters.Add("@OfficeCode", request.OfficeCode);
                    parameters.Add("@CreatedUserName", request.CreatedUserName);
                    parameters.Add("@DataLanguageCode", "TR"); // Varsayılan dil kodu
                    parameters.Add("@IsIndividualAcc", request.IsIndividualAcc);

                    // Saklı prosedür yerine doğrudan SQL sorgusu kullan
                    string insertQuery = @"
                        INSERT INTO cdCurrAcc (
                            CurrAccTypeCode, CurrAccCode, FirstName, LastName, 
                            CompanyCode, OfficeCode, CreatedUserName, 
                            CreatedDate, DataLanguageCode, IsIndividualAcc
                        ) VALUES (
                            @CurrAccTypeCode, @CurrAccCode, @FirstName, @LastName, 
                            @CompanyCode, @OfficeCode, @CreatedUserName, 
                            GETDATE(), @DataLanguageCode, @IsIndividualAcc
                        )";
                    
                    await connection.ExecuteAsync(insertQuery, parameters);
                    
                    // cdCurrAccDesc tablosuna açıklama ekle
                    string description = request.IsIndividualAcc 
                        ? $"{request.CustomerName} {request.CustomerSurname}".Trim() 
                        : request.CustomerName.Trim();
                    
                    string insertDescQuery = @"
                        INSERT INTO cdCurrAccDesc (
                            CurrAccTypeCode, CurrAccCode, LangCode, CurrAccDescription
                        ) VALUES (
                            @CurrAccTypeCode, @CurrAccCode, @LangCode, @CurrAccDescription
                        )";
                    
                    var descParameters = new DynamicParameters();
                    descParameters.Add("@CurrAccTypeCode", request.CustomerTypeCode);
                    descParameters.Add("@CurrAccCode", request.CustomerCode);
                    descParameters.Add("@LangCode", "TR"); // Varsayılan dil kodu
                    descParameters.Add("@CurrAccDescription", description);
                    
                    await connection.ExecuteAsync(insertDescQuery, descParameters);

                    // Başarılı yanıt döndür
                    return new CustomerCreateResponseNew
                    {
                        Success = true,
                        Message = "Müşteri başarıyla oluşturuldu",
                        CustomerCode = request.CustomerCode,
                        CustomerName = request.CustomerName,
                        CompanyCode = request.CompanyCode.ToString(),
                        OfficeCode = request.OfficeCode
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri oluşturma hatası: {Message}", ex.Message);
                return new CustomerCreateResponseNew
                {
                    Success = false,
                    Message = "Müşteri oluşturma işlemi başarısız",
                    ErrorDetails = ex.Message
                };
            }
        }

        /// <summary>
        /// Müşteri bilgilerini günceller (Geliştirilmiş versiyon)
        /// </summary>
        /// <param name="request">Müşteri güncelleme isteği</param>
        /// <returns>Güncellenen müşteri bilgileri</returns>
        public async Task<CustomerUpdateResponseNew> UpdateCustomerAsync(CustomerUpdateRequestNew request)
        {
            if (request == null)
            {
                return new CustomerUpdateResponseNew
                {
                    Success = false,
                    Message = "Geçersiz istek"
                };
            }

            try
            {
                _logger.LogInformation("Müşteri güncelleniyor: {CustomerCode}", request.CustomerCode);

                // Veritabanı bağlantısı
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Müşteri var mı kontrol et
                    var existingCustomer = await connection.QueryFirstOrDefaultAsync<int>(
                        "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode",
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = request.CustomerTypeCode });

                    if (existingCustomer == 0)
                    {
                        return new CustomerUpdateResponseNew
                        {
                            Success = false,
                            Message = "Müşteri bulunamadı",
                            ErrorDetails = $"Müşteri kodu '{request.CustomerCode}' bulunamadı"
                        };
                    }

                    // Müşteri güncelleme için parametreleri hazırla
                    var parameters = new DynamicParameters();
                    parameters.Add("@CurrAccTypeCode", request.CustomerTypeCode);
                    parameters.Add("@CurrAccCode", request.CustomerCode);
                    parameters.Add("@FirstName", request.CustomerName);
                    parameters.Add("@LastName", request.CustomerSurname);
                    parameters.Add("@TitleCode", request.TitleCode);
                    parameters.Add("@Patronym", request.Patronym);
                    parameters.Add("@IsIndividualAcc", request.IsIndividualAcc);

                    // SQL sorgusu ile güncelleme yap
                    string updateQuery = @"
                        UPDATE cdCurrAcc SET
                            FirstName = @FirstName,
                            LastName = @LastName,
                            TitleCode = @TitleCode,
                            Patronym = @Patronym,
                            IsIndividualAcc = @IsIndividualAcc,
                            LastUpdatedDate = GETDATE()
                        WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode";

                    await connection.ExecuteAsync(updateQuery, parameters);
                    
                    // cdCurrAccDesc tablosundaki açıklamayı güncelle
                    string description = request.IsIndividualAcc 
                        ? $"{request.CustomerName} {request.CustomerSurname}".Trim() 
                        : request.CustomerName.Trim();
                    
                    // Önce açıklama var mı kontrol et
                    var descExists = await connection.QueryFirstOrDefaultAsync<int>(
                        "SELECT COUNT(1) FROM cdCurrAccDesc WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode AND LangCode = @LangCode",
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = request.CustomerTypeCode, LangCode = "TR" });
                    
                    if (descExists > 0)
                    {
                        // Açıklama varsa güncelle
                        string updateDescQuery = @"
                            UPDATE cdCurrAccDesc SET
                                CurrAccDescription = @CurrAccDescription
                            WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode AND LangCode = @LangCode";
                        
                        var descParameters = new DynamicParameters();
                        descParameters.Add("@CurrAccTypeCode", request.CustomerTypeCode);
                        descParameters.Add("@CurrAccCode", request.CustomerCode);
                        descParameters.Add("@LangCode", "TR"); // Varsayılan dil kodu
                        descParameters.Add("@CurrAccDescription", description);
                        
                        await connection.ExecuteAsync(updateDescQuery, descParameters);
                    }
                    else
                    {
                        // Açıklama yoksa ekle
                        string insertDescQuery = @"
                            INSERT INTO cdCurrAccDesc (
                                CurrAccTypeCode, CurrAccCode, LangCode, CurrAccDescription
                            ) VALUES (
                                @CurrAccTypeCode, @CurrAccCode, @LangCode, @CurrAccDescription
                            )";
                        
                        var descParameters = new DynamicParameters();
                        descParameters.Add("@CurrAccTypeCode", request.CustomerTypeCode);
                        descParameters.Add("@CurrAccCode", request.CustomerCode);
                        descParameters.Add("@LangCode", "TR"); // Varsayılan dil kodu
                        descParameters.Add("@CurrAccDescription", description);
                        
                        await connection.ExecuteAsync(insertDescQuery, descParameters);
                    }

                    // Başarılı yanıt döndür
                    return new CustomerUpdateResponseNew
                    {
                        Success = true,
                        Message = "Müşteri başarıyla güncellendi",
                        CustomerCode = request.CustomerCode,
                        CustomerName = request.CustomerName,
                        CustomerTypeCode = request.CustomerTypeCode
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri güncelleme hatası: {Message}", ex.Message);
                return new CustomerUpdateResponseNew
                {
                    Success = false,
                    Message = "Müşteri güncelleme işlemi başarısız",
                    ErrorDetails = ex.Message
                };
            }
        }
    }
}
