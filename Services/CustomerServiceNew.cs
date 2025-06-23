using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Results;
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
            _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Başlatıldı\u001b[0m");
            
            if (request == null)
            {
                _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Geçersiz istek (null)\u001b[0m");
                return new CustomerCreateResponseNew
                {
                    Success = false,
                    Message = "Geçersiz istek"
                };
            }

            try
            {
                _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Yeni müşteri oluşturuluyor: {CustomerCode}\u001b[0m", request.CustomerCode);

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
                                // Önce 121. ile başlayan son kodu kontrol et
                                string query = "SELECT TOP 1 CurrAccCode FROM cdCurrAcc WHERE CurrAccCode LIKE '121.%' ORDER BY CurrAccCode DESC";
                                string lastCode = await connection.QueryFirstOrDefaultAsync<string>(query);
                                
                                if (!string.IsNullOrEmpty(lastCode))
                                {
                                    // "121." sonrasını sayısal değere çevir
                                    string lastNumberStr = lastCode.Substring(4);
                                    if (int.TryParse(lastNumberStr, out int lastNum))
                                    {
                                        // Eğer 9999'dan büyükse, 122 serisine geç
                                        if (lastNum >= 9999)
                                        {
                                            // 122. ile başlayan son kodu kontrol et
                                            query = "SELECT TOP 1 CurrAccCode FROM cdCurrAcc WHERE CurrAccCode LIKE '122.%' ORDER BY CurrAccCode DESC";
                                            string lastCode122 = await connection.QueryFirstOrDefaultAsync<string>(query);
                                            
                                            if (!string.IsNullOrEmpty(lastCode122))
                                            {
                                                // "122." sonrasını sayısal değere çevir
                                                string lastNum122Str = lastCode122.Substring(4);
                                                if (int.TryParse(lastNum122Str, out int lastNum122))
                                                {
                                                    prefix = "122.";
                                                    nextNumber = lastNum122 + 1;
                                                }
                                                else
                                                {
                                                    prefix = "122.";
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
                                            // 121 serisinde devam et
                                            prefix = "121.";
                                            nextNumber = lastNum + 1;
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
                                    prefix = "121.";
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
                            
                            // Benzersiz bir müşteri kodu bulana kadar döngü
                            string newCode;
                            bool isCodeUnique = false;
                            int attempts = 0;
                            int maxAttempts = 100; // Sonsuz döngüye girmemek için maksimum deneme sayısı
                            
                            while (!isCodeUnique && attempts < maxAttempts)
                            {
                                // Yeni kodu oluştur
                                if (nextNumber < 1000)
                                {
                                    newCode = $"{prefix}{nextNumber:000}";
                                }
                                else
                                {
                                    newCode = $"{prefix}{nextNumber}";
                                }
                                
                                // Bu kod zaten kullanımda mı kontrol et
                                var existingCount = await connection.QueryFirstOrDefaultAsync<int>(
                                    "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CurrAccCode",
                                    new { CurrAccCode = newCode });
                                
                                if (existingCount == 0)
                                {
                                    // Benzersiz kod bulundu
                                    isCodeUnique = true;
                                    request.CustomerCode = newCode;
                                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Otomatik müşteri kodu oluşturuldu: {CustomerCode}\u001b[0m", request.CustomerCode);
                                }
                                else
                                {
                                    // Bu kod zaten kullanımda, sonraki sayıyı dene
                                    nextNumber++;
                                    attempts++;
                                    _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri kodu '{Code}' zaten kullanımda, yeni kod deneniyor...\u001b[0m", newCode);
                                }
                            }
                            
                            if (!isCodeUnique)
                            {
                                _logger.LogError("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Benzersiz müşteri kodu oluşturulamadı, maksimum deneme sayısına ulaşıldı\u001b[0m");
                                throw new Exception("Benzersiz müşteri kodu oluşturulamadı, lütfen daha sonra tekrar deneyin.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri kodu otomatik oluşturulurken hata oluştu\u001b[0m");
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
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri kodu elle verildi: {CustomerCode}\u001b[0m", request.CustomerCode);
                }

                // Veritabanı bağlantısı
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri kodu kontrolü yapılıyor: {CustomerCode}\u001b[0m", request.CustomerCode);
                    // Müşteri zaten var mı kontrol et
                    var existingCustomer = await connection.QueryFirstOrDefaultAsync<int>(
                        "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode",
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = request.CustomerTypeCode });
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri kodu kontrolü sonucu: {ExistingCount}\u001b[0m", existingCustomer);

                    if (existingCustomer > 0)
                    {
                        _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri kodu zaten kullanımda: {CustomerCode}\u001b[0m", request.CustomerCode);
                        return new CustomerCreateResponseNew
                        {
                            Success = false,
                            Message = "Bu müşteri kodu zaten kullanılıyor",
                            ErrorDetails = $"Müşteri kodu '{request.CustomerCode}' zaten kullanımda"
                        };
                    }

                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri oluşturma parametreleri hazırlanıyor\u001b[0m");
                    // Müşteri oluşturma için parametreleri hazırla
                    var parameters = new DynamicParameters();
                    parameters.Add("@CurrAccTypeCode", request.CustomerTypeCode);
                    parameters.Add("@CurrAccCode", request.CustomerCode);
                    parameters.Add("@CompanyCode", request.CompanyCode);
                    parameters.Add("@OfficeCode", request.OfficeCode);
                    parameters.Add("@CreatedUserName", request.CreatedUserName);
                    parameters.Add("@DataLanguageCode", "TR"); // Varsayılan dil kodu
                    parameters.Add("@IsIndividualAcc", request.IsIndividualAcc);
                    
                    // Koşullu alanlar
                    if (request.IsIndividualAcc)
                    {
                        parameters.Add("@FirstName", request.CustomerName);
                        parameters.Add("@LastName", request.CustomerSurname);
                        if (!string.IsNullOrEmpty(request.TaxNumber))
                        {
                            parameters.Add("@TaxNumber", request.TaxNumber);
                        }
                        
                        // TC Kimlik numarası bireysel müşteriler için zorunlu
                        if (!string.IsNullOrEmpty(request.IdentityNum))
                        {
                            parameters.Add("@IdentityNum", request.IdentityNum);
                            _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - TC Kimlik numarası parametresi eklendi: {IdentityNum}\u001b[0m", request.IdentityNum);
                        }
                        else
                        {
                            _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - TC Kimlik numarası boş!\u001b[0m");
                        }
                    }
                    else
                    {
                        parameters.Add("@FirstName", request.CustomerName);
                        parameters.Add("@LastName", string.Empty);
                    }
                    
                    // CurrencyCode alanını ekle
                    if (!string.IsNullOrEmpty(request.CurrencyCode))
                    {
                        parameters.Add("@CurrencyCode", request.CurrencyCode);
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Para birimi parametresi eklendi: {CurrencyCode}\u001b[0m", request.CurrencyCode);
                    }
                    else
                    {
                        // Frontend'den para birimi gelmediğinde varsayılan olarak TRY ekle
                        parameters.Add("@CurrencyCode", "TRY");
                        _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Para birimi gelmedi! Varsayılan olarak TRY eklendi.\u001b[0m");
                    }
                    
                    if (!string.IsNullOrEmpty(request.TaxNumber) && !request.IsIndividualAcc)
                    {
                        parameters.Add("@TaxNumber", request.TaxNumber);
                    }
                    
                    if (!string.IsNullOrEmpty(request.TaxOfficeCode))
                    {
                        parameters.Add("@TaxOfficeCode", request.TaxOfficeCode);
                    }
                    
                    if (request.IsSubjectToEInvoice)
                    {
                        parameters.Add("@IsSubjectToEInvoice", true);
                        parameters.Add("@EInvoiceStartDate", request.EInvoiceStartDate ?? DateTime.Now);
                    }
                    
                    if (request.IsSubjectToEShipment)
                    {
                        parameters.Add("@IsSubjectToEShipment", true);
                        parameters.Add("@EShipmentStartDate", request.EShipmentStartDate ?? DateTime.Now);
                    }

                    // Dinamik SQL sorgusu oluştur
                    var columnNames = new List<string> { 
                        "CurrAccTypeCode", "CurrAccCode", "FirstName", "LastName",
                        "CompanyCode", "OfficeCode", "CreatedUserName",
                        "CreatedDate", "DataLanguageCode", "IsIndividualAcc"
                    };
                    
                    var parameterNames = new List<string> {
                        "@CurrAccTypeCode", "@CurrAccCode", "@FirstName", "@LastName",
                        "@CompanyCode", "@OfficeCode", "@CreatedUserName",
                        "GETDATE()", "@DataLanguageCode", "@IsIndividualAcc"
                    };
                    
                    // Koşullu alanları ekle
                    if (!string.IsNullOrEmpty(request.IdentityNum))
                    {
                        columnNames.Add("IdentityNum"); // Veritabanı sütun adı IdentityNum olarak düzeltildi
                        parameterNames.Add("@IdentityNum");
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Kimlik numarası eklendi: {IdentityNum}\u001b[0m", request.IdentityNum);
                    }
                    
                    if (!string.IsNullOrEmpty(request.TaxNumber))
                    {
                        columnNames.Add("TaxNumber");
                        parameterNames.Add("@TaxNumber");
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Vergi numarası eklendi: {TaxNumber}\u001b[0m", request.TaxNumber);
                    }
                    else
                    {
                        _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Vergi numarası boş olduğu için SQL sorgusuna eklenmedi!\u001b[0m");
                    }
                    
                    if (!string.IsNullOrEmpty(request.TaxOfficeCode))
                    {
                        columnNames.Add("TaxOfficeCode");
                        parameterNames.Add("@TaxOfficeCode");
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Vergi dairesi eklendi: {TaxOfficeCode}\u001b[0m", request.TaxOfficeCode);
                    }
                    else
                    {
                        _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Vergi dairesi boş olduğu için SQL sorgusuna eklenmedi!\u001b[0m");
                    }
                    
                    // CurrencyCode alanını ekle
                    if (!string.IsNullOrEmpty(request.CurrencyCode))
                    {
                        columnNames.Add("CurrencyCode");
                        parameterNames.Add("@CurrencyCode");
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Para birimi ekleniyor: {CurrencyCode}\u001b[0m", request.CurrencyCode);
                    }
                    else
                    {
                        // Frontend'den para birimi gelmediğinde varsayılan olarak TRY ekle
                        columnNames.Add("CurrencyCode");
                        parameterNames.Add("@CurrencyCode");
                        _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Para birimi gelmedi! SQL sorgusuna varsayılan olarak TRY eklendi.\u001b[0m");
                    }
                    
                    // E-Fatura mükellefi kontrolü
                    if (request.IsSubjectToEInvoice)
                    {
                        columnNames.Add("IsSubjectToEInvoice");
                        parameterNames.Add("@IsSubjectToEInvoice");
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-Fatura mükellefi eklendi\u001b[0m");
                        
                        if (request.EInvoiceStartDate.HasValue)
                        {
                            columnNames.Add("EInvoiceStartDate");
                            parameterNames.Add("@EInvoiceStartDate");
                            _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-Fatura başlangıç tarihi eklendi: {Date}\u001b[0m", 
                                request.EInvoiceStartDate.Value.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            // E-Fatura başlangıç tarihi gelmediğinde bugünün tarihini ekle
                            columnNames.Add("EInvoiceStartDate");
                            parameterNames.Add("@EInvoiceStartDate");
                            parameters.Add("@EInvoiceStartDate", DateTime.Now.Date);
                            _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-Fatura başlangıç tarihi gelmedi! Bugünün tarihi eklendi: {Date}\u001b[0m", 
                                DateTime.Now.Date.ToString("yyyy-MM-dd"));
                        }
                    }
                    
                    // E-İrsaliye mükellefi kontrolü
                    if (request.IsSubjectToEShipment)
                    {
                        columnNames.Add("IsSubjectToEShipment");
                        parameterNames.Add("@IsSubjectToEShipment");
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-İrsaliye mükellefi eklendi\u001b[0m");
                        
                        if (request.EShipmentStartDate.HasValue)
                        {
                            columnNames.Add("EShipmentStartDate");
                            parameterNames.Add("@EShipmentStartDate");
                            _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-İrsaliye başlangıç tarihi eklendi: {Date}\u001b[0m", 
                                request.EShipmentStartDate.Value.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            // E-İrsaliye başlangıç tarihi gelmediğinde bugünün tarihini ekle
                            columnNames.Add("EShipmentStartDate");
                            parameterNames.Add("@EShipmentStartDate");
                            parameters.Add("@EShipmentStartDate", DateTime.Now.Date);
                            _logger.LogWarning("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-İrsaliye başlangıç tarihi gelmedi! Bugünün tarihi eklendi: {Date}\u001b[0m", 
                                DateTime.Now.Date.ToString("yyyy-MM-dd"));
                        }
                    }
                    
                    // SQL sorgusunu oluştur
                    string insertQuery = $@"
                        INSERT INTO cdCurrAcc (
                            {string.Join(", ", columnNames)}
                        ) VALUES (
                            {string.Join(", ", parameterNames)}
                        )";
                    
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri oluşturma işlemi başladı\u001b[0m");
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri tipi: {Type}\u001b[0m", request.IsIndividualAcc ? "Bireysel" : "Kurumsal");
                    
                    // Frontend'den gelen verileri kontrol et ve logla
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Vergi Numarası: {TaxNumber}\u001b[0m", 
                        string.IsNullOrEmpty(request.TaxNumber) ? "GELMEDI" : request.TaxNumber);
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Vergi Dairesi: {TaxOfficeCode}\u001b[0m", 
                        string.IsNullOrEmpty(request.TaxOfficeCode) ? "GELMEDI" : request.TaxOfficeCode);
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-Fatura Mükellefi: {IsSubjectToEInvoice}\u001b[0m", request.IsSubjectToEInvoice);
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-Fatura Başlangıç Tarihi: {EInvoiceStartDate}\u001b[0m", 
                        request.EInvoiceStartDate.HasValue ? request.EInvoiceStartDate.Value.ToString("yyyy-MM-dd") : "GELMEDI");
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-İrsaliye Mükellefi: {IsSubjectToEShipment}\u001b[0m", request.IsSubjectToEShipment);
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-İrsaliye Başlangıç Tarihi: {EShipmentStartDate}\u001b[0m", 
                        request.EShipmentStartDate.HasValue ? request.EShipmentStartDate.Value.ToString("yyyy-MM-dd") : "GELMEDI");
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Para Birimi: {CurrencyCode}\u001b[0m", 
                        string.IsNullOrEmpty(request.CurrencyCode) ? "GELMEDI" : request.CurrencyCode);
                    await connection.ExecuteAsync(insertQuery, parameters);
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri kaydı eklendi\u001b[0m");
                    
                    // Koşullu alanların loglanması
                    if (request.IsIndividualAcc)
                    {
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Gerçek kişi müşteri oluşturuldu: FirstName={FirstName}, LastName={LastName}\u001b[0m", request.CustomerName, request.CustomerSurname);
                    }
                    else
                    {
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Tüzel kişi müşteri oluşturuldu: CustomerName={CustomerName}\u001b[0m", request.CustomerName);
                    }
                    
                    // Özetleyici log mesajları
                    if (request.IsSubjectToEInvoice)
                    {
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-Fatura mükellefi: EInvoiceStartDate={Date}\u001b[0m", 
                            request.EInvoiceStartDate.HasValue ? request.EInvoiceStartDate.Value.ToString("yyyy-MM-dd") : "GELMEDI (Bugünün tarihi kullanıldı)");
                    }
                    
                    if (request.IsSubjectToEShipment)
                    {
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - E-İrsaliye mükellefi: EShipmentStartDate={Date}\u001b[0m", 
                            request.EShipmentStartDate.HasValue ? request.EShipmentStartDate.Value.ToString("yyyy-MM-dd") : "GELMEDI (Bugünün tarihi kullanıldı)");
                    }
                    
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Para birimi: {CurrencyCode}\u001b[0m", 
                        string.IsNullOrEmpty(request.CurrencyCode) ? "GELMEDI (TRY kullanıldı)" : request.CurrencyCode);
                    
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Vergi numarası: {TaxNumber}, Vergi dairesi: {TaxOfficeCode}\u001b[0m", 
                        string.IsNullOrEmpty(request.TaxNumber) ? "GELMEDI" : request.TaxNumber,
                        string.IsNullOrEmpty(request.TaxOfficeCode) ? "GELMEDI" : request.TaxOfficeCode);

                    
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

                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - İşlem başarılı, müşteri oluşturuldu: {CustomerCode}\u001b[0m", request.CustomerCode);
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
                _logger.LogError(ex, "\u001b[33m[CustomerServiceNew.CreateCustomerAsync] - Müşteri oluşturma hatası: {Message}\u001b[0m", ex.Message);
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
            _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Başlatıldı\u001b[0m");
            
            if (request == null)
            {
                _logger.LogWarning("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Geçersiz istek (null)\u001b[0m");
                return new CustomerUpdateResponseNew
                {
                    Success = false,
                    Message = "Geçersiz istek"
                };
            }

            try
            {
                _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri güncelleniyor: {CustomerCode}\u001b[0m", request.CustomerCode);

                // Veritabanı bağlantısı
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri kontrolü yapılıyor: {CustomerCode}\u001b[0m", request.CustomerCode);
                    // Müşteri var mı kontrol et
                    var existingCustomer = await connection.QueryFirstOrDefaultAsync<int>(
                        "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode",
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = request.CustomerTypeCode });
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri kontrolü sonucu: {ExistingCount}\u001b[0m", existingCustomer);

                    if (existingCustomer == 0)
                    {
                        _logger.LogWarning("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri bulunamadı: {CustomerCode}\u001b[0m", request.CustomerCode);
                        return new CustomerUpdateResponseNew
                        {
                            Success = false,
                            Message = "Müşteri bulunamadı",
                            ErrorDetails = $"Müşteri kodu '{request.CustomerCode}' bulunamadı"
                        };
                    }

                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri güncelleme parametreleri hazırlanıyor\u001b[0m");
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

                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri kaydı güncelleniyor: {SQL}\u001b[0m", updateQuery);
                    await connection.ExecuteAsync(updateQuery, parameters);
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri kaydı güncellendi\u001b[0m");
                    
                    // cdCurrAccDesc tablosundaki açıklamayı güncelle
                    string description = request.IsIndividualAcc 
                        ? $"{request.CustomerName} {request.CustomerSurname}".Trim() 
                        : request.CustomerName.Trim();
                    
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri açıklama kaydı kontrolü yapılıyor\u001b[0m");
                    // Önce açıklama var mı kontrol et
                    var descExists = await connection.QueryFirstOrDefaultAsync<int>(
                        "SELECT COUNT(1) FROM cdCurrAccDesc WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode AND LangCode = @LangCode",
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = request.CustomerTypeCode, LangCode = "TR" });
                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri açıklama kaydı kontrolü sonucu: {ExistingCount}\u001b[0m", descExists);
                    
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
                        
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri açıklama kaydı güncelleniyor\u001b[0m");
                        await connection.ExecuteAsync(updateDescQuery, descParameters);
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri açıklama kaydı güncellendi\u001b[0m");
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
                        
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri açıklama kaydı ekleniyor\u001b[0m");
                        await connection.ExecuteAsync(insertDescQuery, descParameters);
                        _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri açıklama kaydı eklendi\u001b[0m");
                    }

                    _logger.LogInformation("\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - İşlem başarılı, müşteri güncellendi: {CustomerCode}\u001b[0m", request.CustomerCode);
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
                _logger.LogError(ex, "\u001b[33m[CustomerServiceNew.UpdateCustomerAsync] - Müşteri güncelleme hatası: {Message}\u001b[0m", ex.Message);
                return new CustomerUpdateResponseNew
                {
                    Success = false,
                    Message = "Müşteri güncelleme işlemi başarısız",
                    ErrorDetails = ex.Message
                };
            }
        }
        /// <summary>
    /// Müşteri adres bilgisi ekler
    /// </summary>
    /// <param name="request">Müşteri adres bilgisi ekleme isteği</param>
    /// <returns>Eklenen adres bilgisi</returns>
    public async Task<CustomerAddressResponse> AddCustomerAddressAsync(CustomerAddressRequest request)
    {
        _logger.LogInformation("[CustomerServiceNew.AddCustomerAddressAsync] - Başlatıldı");
        
        if (request == null)
        {
            _logger.LogWarning("[CustomerServiceNew.AddCustomerAddressAsync] - Geçersiz istek (null)");
            return new CustomerAddressResponse
            {
                Success = false,
                Message = "Geçersiz istek"
            };
        }

        try
        {
            _logger.LogInformation("[CustomerServiceNew.AddCustomerAddressAsync] - Müşteri adresi ekleniyor: {CustomerCode}", request.CustomerCode);

            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();

            // Adres ID oluştur
            var addressId = Guid.NewGuid();

            // Adres ekle
            string insertQuery = @"
                INSERT INTO prCurrAccAddress (
                    AddressID, CurrAccTypeCode, CurrAccCode, AddressTypeCode,
                    CountryCode, CityCode, TownCode, DistrictCode, PostalCode, Address,
                    IsDefault, SubCurrAccID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                ) VALUES (
                    @AddressID, @CurrAccTypeCode, @CurrAccCode, @AddressTypeCode,
                    @CountryCode, @CityCode, @TownCode, @DistrictCode, @PostalCode, @Address,
                    @IsDefault, @SubCurrAccID, @CreatedUserName, @CreatedDate, @LastUpdatedUserName, @LastUpdatedDate
                )";

            var parameters = new DynamicParameters();
            parameters.Add("@AddressID", addressId);
            parameters.Add("@CurrAccTypeCode", 3); // Müşteri tipi kodu
            parameters.Add("@CurrAccCode", request.CustomerCode);
            parameters.Add("@AddressTypeCode", request.AddressTypeCode);
            parameters.Add("@CountryCode", request.CountryCode);
            parameters.Add("@CityCode", request.CityCode);
            parameters.Add("@TownCode", request.TownCode ?? string.Empty);
            parameters.Add("@DistrictCode", request.DistrictCode);
            parameters.Add("@PostalCode", request.PostalCode ?? string.Empty);
            parameters.Add("@Address", request.Address);
            parameters.Add("@IsDefault", request.IsDefault);
            parameters.Add("@SubCurrAccID", null); // DBNull.Value yerine null kullanıyoruz
            parameters.Add("@CreatedUserName", request.CreatedUserName);
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@LastUpdatedUserName", request.LastUpdatedUserName);
            parameters.Add("@LastUpdatedDate", DateTime.Now);

            await connection.ExecuteAsync(insertQuery, parameters);

            _logger.LogInformation("[CustomerServiceNew.AddCustomerAddressAsync] - Müşteri adresi eklendi: {CustomerCode}, AddressID: {AddressID}", request.CustomerCode, addressId);

            return new CustomerAddressResponse
            {
                Success = true,
                AddressId = addressId,
                CustomerCode = request.CustomerCode,
                Message = "Adres başarıyla eklendi"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[CustomerServiceNew.AddCustomerAddressAsync] - Müşteri adresi eklenirken hata oluştu: {Message}", ex.Message);
            return new CustomerAddressResponse
            {
                Success = false,
                Message = $"Adres eklenirken bir hata oluştu: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Müşteri iletişim bilgisi ekler
    /// </summary>
    /// <param name="request">Müşteri iletişim bilgisi ekleme isteği</param>
    /// <returns>Eklenen iletişim bilgisi</returns>
    public async Task<CustomerCommunicationResponse> AddCustomerCommunicationAsync(CustomerCommunicationRequest request)
    {
        _logger.LogInformation("[CustomerServiceNew.AddCustomerCommunicationAsync] - Başlatıldı");
        
        if (request == null)
        {
            _logger.LogWarning("[CustomerServiceNew.AddCustomerCommunicationAsync] - Geçersiz istek (null)");
            return new CustomerCommunicationResponse
            {
                Success = false,
                Message = "Geçersiz istek"
            };
        }

        try
        {
            _logger.LogInformation("[CustomerServiceNew.AddCustomerCommunicationAsync] - Müşteri iletişim bilgisi ekleniyor: {CustomerCode}", request.CustomerCode);

            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();

            // İletişim ID oluştur
            var communicationId = Guid.NewGuid();

            // İletişim bilgisi ekle
            string insertQuery = @"
                INSERT INTO prCurrAccCommunication (
                    CommunicationID, CurrAccTypeCode, CurrAccCode, CommunicationTypeCode,
                    CommAddress, IsDefault, IsBlocked, CanSendAdvert, IsConfirmed, SubCurrAccID, 
                    CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                ) VALUES (
                    @CommunicationID, @CurrAccTypeCode, @CurrAccCode, @CommunicationTypeCode,
                    @CommAddress, @IsDefault, @IsBlocked, @CanSendAdvert, @IsConfirmed, @SubCurrAccID, 
                    @CreatedUserName, @CreatedDate, @LastUpdatedUserName, @LastUpdatedDate
                )";

            var parameters = new DynamicParameters();
            parameters.Add("@CommunicationID", communicationId);
            parameters.Add("@CurrAccTypeCode", 3); // Müşteri tipi kodu
            parameters.Add("@CurrAccCode", request.CustomerCode);
            parameters.Add("@CommunicationTypeCode", request.CommunicationTypeCode);
            parameters.Add("@CommAddress", request.CommunicationValue);
            parameters.Add("@IsDefault", request.IsDefault);
            parameters.Add("@IsBlocked", false);
            parameters.Add("@CanSendAdvert", request.CanSendAdvert);
            parameters.Add("@IsConfirmed", request.IsConfirmed);
            parameters.Add("@SubCurrAccID", null); // DBNull.Value yerine null kullanıyoruz
            parameters.Add("@CreatedUserName", request.CreatedUserName);
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@LastUpdatedUserName", request.LastUpdatedUserName);
            parameters.Add("@LastUpdatedDate", DateTime.Now);

            await connection.ExecuteAsync(insertQuery, parameters);

            _logger.LogInformation("[CustomerServiceNew.AddCustomerCommunicationAsync] - Müşteri iletişim bilgisi eklendi: {CustomerCode}, CommunicationID: {CommunicationID}", request.CustomerCode, communicationId);

            return new CustomerCommunicationResponse
            {
                Success = true,
                CommunicationId = communicationId,
                CustomerCode = request.CustomerCode,
                CommunicationTypeCode = request.CommunicationTypeCode,
                CommunicationValue = request.CommunicationValue,
                IsDefault = request.IsDefault,
                CanSendAdvert = request.CanSendAdvert,
                IsConfirmed = request.IsConfirmed,
                CreatedDate = DateTime.Now,
                CreatedUserName = request.CreatedUserName,
                Message = "İletişim bilgisi başarıyla eklendi"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[CustomerServiceNew.AddCustomerCommunicationAsync] - Müşteri iletişim bilgisi eklenirken hata oluştu: {Message}", ex.Message);
            return new CustomerCommunicationResponse
            {
                Success = false,
                Message = $"İletişim bilgisi eklenirken bir hata oluştu: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Müşteri kişi bilgisi ekler
    /// </summary>
    /// <param name="request">Müşteri kişi bilgisi ekleme isteği</param>
    /// <returns>Eklenen kişi bilgisi</returns>
    public async Task<CustomerContactResponse> AddCustomerContactAsync(CustomerContactCreateRequestNew request)
    {
        _logger.LogInformation("[CustomerServiceNew.AddCustomerContactAsync] - Başlatıldı");
        
        if (request == null)
        {
            _logger.LogWarning("[CustomerServiceNew.AddCustomerContactAsync] - Geçersiz istek (null)");
            return new CustomerContactResponse
            {
                Success = false,
                Message = "Geçersiz istek"
            };
        }

        try
        {
            _logger.LogInformation("[CustomerServiceNew.AddCustomerContactAsync] - Müşteri kişi bilgisi ekleniyor: {CustomerCode}", request.CustomerCode);

            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();

            // Kişi ID oluştur
            var contactId = Guid.NewGuid();

            // Kişi bilgisi ekle
            string insertQuery = @"
                INSERT INTO prCurrAccContact (
                    ContactID, CurrAccTypeCode, CurrAccCode, ContactTypeCode,
                    FirstName, LastName, IsAuthorized, IdentityNum, SubCurrAccID,
                    CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                ) VALUES (
                    @ContactID, @CurrAccTypeCode, @CurrAccCode, @ContactTypeCode,
                    @FirstName, @LastName, @IsAuthorized, @IdentityNum, @SubCurrAccID,
                    @CreatedUserName, @CreatedDate, @LastUpdatedUserName, @LastUpdatedDate
                )";

            var parameters = new DynamicParameters();
            parameters.Add("@ContactID", contactId);
            parameters.Add("@CurrAccTypeCode", 3); // Müşteri tipi kodu
            parameters.Add("@CurrAccCode", request.CustomerCode);
            parameters.Add("@ContactTypeCode", request.ContactTypeCode);
            parameters.Add("@FirstName", request.FirstName);
            parameters.Add("@LastName", request.LastName);
            parameters.Add("@IsAuthorized", request.IsAuthorized);
            parameters.Add("@IdentityNum", string.IsNullOrEmpty(request.IdentityNum) ? null : request.IdentityNum); // DBNull.Value yerine null kullanıyoruz
            parameters.Add("@SubCurrAccID", null); // DBNull.Value yerine null kullanıyoruz
            parameters.Add("@CreatedUserName", request.CreatedUserName);
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@LastUpdatedUserName", request.LastUpdatedUserName);
            parameters.Add("@LastUpdatedDate", DateTime.Now);

            await connection.ExecuteAsync(insertQuery, parameters);

            _logger.LogInformation("[CustomerServiceNew.AddCustomerContactAsync] - Müşteri kişi bilgisi eklendi: {CustomerCode}, ContactID: {ContactID}", request.CustomerCode, contactId);

            return new CustomerContactResponse
            {
                Success = true,
                ContactId = contactId,
                CustomerCode = request.CustomerCode,
                ContactTypeCode = request.ContactTypeCode,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsAuthorized = request.IsAuthorized,
                CreatedDate = DateTime.Now,
                CreatedUserName = request.CreatedUserName,
                Message = "Kişi bilgisi başarıyla eklendi"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[CustomerServiceNew.AddCustomerContactAsync] - Müşteri kişi bilgisi eklenirken hata oluştu: {Message}", ex.Message);
            return new CustomerContactResponse
            {
                Success = false,
                Message = $"Kişi bilgisi eklenirken bir hata oluştu: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Token ile müşteri adres bilgilerini kaydeder
    /// </summary>
    /// <param name="request">Müşteri adres bilgisi ekleme isteği</param>
    /// <returns>Adres kaydetme sonucu</returns>
    public async Task<CustomerAddressResult> CreateCustomerAddressAsync(CustomerAddressCreateRequest request)
    {
        _logger.LogInformation("[CustomerServiceNew.CreateCustomerAddressAsync] - Başlatıldı");
        
        if (request == null)
        {
            _logger.LogWarning("[CustomerServiceNew.CreateCustomerAddressAsync] - Geçersiz istek (null)");
            return new CustomerAddressResult
            {
                Success = false,
                Message = "Geçersiz istek"
            };
        }

        // Gelen isteği detaylı logla
        _logger.LogInformation("[CustomerServiceNew.CreateCustomerAddressAsync] - Gelen istek: CustomerCode={CustomerCode}, AddressTypeCode={AddressTypeCode}, CountryCode={CountryCode}, StateCode={StateCode}, CityCode={CityCode}, DistrictCode={DistrictCode}", 
            request.CustomerCode,
            request.AddressTypeCode ?? "NULL",
            request.CountryCode ?? "NULL",
            request.StateCode ?? "NULL",
            request.CityCode ?? "NULL",
            request.DistrictCode ?? "NULL");

        // Zorunlu alanların kontrolü
        if (string.IsNullOrEmpty(request.CustomerCode) || request.CustomerCode == "")
        {
            _logger.LogWarning("[CustomerServiceNew.CreateCustomerAddressAsync] - Müşteri kodu boş veya boş string");
            
            // Token ile gelen müşteri kodu kullanılabilir, ancak bu durumda controller'dan gelmeli
            // Bu noktada sadece hata dönüyoruz, controller'da gerekli düzeltme yapılacak
            return new CustomerAddressResult
            {
                Success = false,
                Message = "Müşteri kodu boş olamaz"
            };
        }

        try
        {
            _logger.LogInformation("[CustomerServiceNew.CreateCustomerAddressAsync] - Müşteri adres bilgisi kaydediliyor: {CustomerCode}", request.CustomerCode);

            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();

            // Adres ID oluştur - Guid kullanıyoruz, normal akışla aynı olması için
            var addressId = Guid.NewGuid();

            // SQL sorgusunu hazırla - NULL değerleri doğru şekilde işleyecek şekilde
            string insertQuery = "INSERT INTO prCurrAccPostalAddress (";
            string columns = "PostalAddressID, CurrAccTypeCode, CurrAccCode";
            string values = "@PostalAddressID, @CurrAccTypeCode, @CurrAccCode";
            
            // Parametreleri hazırla
            var parameters = new DynamicParameters();
            parameters.Add("@PostalAddressID", addressId);
            parameters.Add("@CurrAccTypeCode", 3); // Müşteri tipi kodu - normal akışla aynı
            parameters.Add("@CurrAccCode", request.CustomerCode);
            
            // Adres tipi
            if (!string.IsNullOrEmpty(request.AddressTypeCode))
            {
                columns += ", AddressTypeCode";
                values += ", @AddressTypeCode";
                parameters.Add("@AddressTypeCode", request.AddressTypeCode);
            }
            
            // Ülke kodu
            if (!string.IsNullOrEmpty(request.CountryCode))
            {
                columns += ", CountryCode";
                values += ", @CountryCode";
                parameters.Add("@CountryCode", request.CountryCode);
            }
            
            // Bölge kodu
            if (!string.IsNullOrEmpty(request.StateCode))
            {
                columns += ", StateCode";
                values += ", @StateCode";
                parameters.Add("@StateCode", request.StateCode);
            }
            
            // Şehir kodu
            if (!string.IsNullOrEmpty(request.CityCode))
            {
                columns += ", CityCode";
                values += ", @CityCode";
                parameters.Add("@CityCode", request.CityCode);
            }
            
            // İlçe kodu - boş değilse ekle
            if (!string.IsNullOrEmpty(request.DistrictCode) && request.DistrictCode != "")
            {
                columns += ", DistrictCode";
                values += ", @DistrictCode";
                parameters.Add("@DistrictCode", request.DistrictCode);
                _logger.LogInformation("[CustomerServiceNew.CreateCustomerAddressAsync] - İlçe kodu eklendi: {DistrictCode}", request.DistrictCode);
            }
            else
            {
                _logger.LogWarning("[CustomerServiceNew.CreateCustomerAddressAsync] - İlçe kodu boş! SQL sorgusuna eklenmedi.");
            }
            
            // Adres
            if (!string.IsNullOrEmpty(request.Address))
            {
                columns += ", Address";
                values += ", @Address";
                parameters.Add("@Address", request.Address);
            }
            
            // Diğer zorunlu alanlar
            columns += ", IsBlocked, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate";
            values += ", @IsBlocked, @CreatedUserName, @CreatedDate, @LastUpdatedUserName, @LastUpdatedDate";
            
            parameters.Add("@IsBlocked", false);
            parameters.Add("@CreatedUserName", request.CreatedUserName ?? "SYSTEM");
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@LastUpdatedUserName", request.LastUpdatedUserName ?? "SYSTEM");
            parameters.Add("@LastUpdatedDate", DateTime.Now);
            
            // SQL sorgusunu tamamla
            insertQuery += columns + ") VALUES (" + values + ")";
            
            _logger.LogInformation("[CustomerServiceNew.CreateCustomerAddressAsync] - SQL sorgusu: {Query}", insertQuery);
            _logger.LogInformation("[CustomerServiceNew.CreateCustomerAddressAsync] - Parametreler: {Parameters}", 
                string.Join(", ", parameters.ParameterNames.Select(p => $"{p}={parameters.Get<object>(p)}")));

            await connection.ExecuteAsync(insertQuery, parameters);

            _logger.LogInformation("[CustomerServiceNew.CreateCustomerAddressAsync] - Müşteri adres bilgisi kaydedildi: {CustomerCode}, AddressID: {AddressID}", request.CustomerCode, addressId);

            return new CustomerAddressResult
            {
                Success = true,
                AddressId = addressId,
                Message = "Adres bilgisi başarıyla kaydedildi"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[CustomerServiceNew.CreateCustomerAddressAsync] - Müşteri adres bilgisi kaydedilirken hata oluştu: {Message}", ex.Message);
            return new CustomerAddressResult
            {
                Success = false,
                Message = $"Adres bilgisi kaydedilirken bir hata oluştu: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Token ile müşteri iletişim bilgilerini kaydeder
    /// </summary>
    /// <param name="request">Müşteri iletişim bilgisi ekleme isteği</param>
    /// <returns>İletişim kaydetme sonucu</returns>
    public async Task<CustomerCommunicationResult> CreateCustomerCommunicationAsync(CustomerCommunicationCreateRequest request)
    {
        _logger.LogInformation("[CustomerServiceNew.CreateCustomerCommunicationAsync] - Başlatıldı");
        
        if (request == null)
        {
            _logger.LogWarning("[CustomerServiceNew.CreateCustomerCommunicationAsync] - Geçersiz istek (null)");
            return new CustomerCommunicationResult
            {
                Success = false,
                Message = "Geçersiz istek"
            };
        }

        try
        {
            _logger.LogInformation("[CustomerServiceNew.CreateCustomerCommunicationAsync] - Müşteri iletişim bilgisi kaydediliyor: {CustomerCode}", request.CustomerCode);

            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();

            // İletişim ID oluştur - Guid kullanıyoruz, normal akışla aynı olması için
            var communicationId = Guid.NewGuid();

            // İletişim bilgisi ekle - prCurrAccCommunication tablosunu kullanıyoruz (normal akışla aynı)
            string insertQuery = @"
                INSERT INTO prCurrAccCommunication (
                    CommunicationID, CurrAccTypeCode, CurrAccCode, CommunicationTypeCode,
                    CommAddress, IsBlocked, CanSendAdvert, IsConfirmed, SubCurrAccID, 
                    CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                ) VALUES (
                    @CommunicationID, @CurrAccTypeCode, @CurrAccCode, @CommunicationTypeCode,
                    @CommAddress, @IsBlocked, @CanSendAdvert, @IsConfirmed, @SubCurrAccID, 
                    @CreatedUserName, @CreatedDate, @LastUpdatedUserName, @LastUpdatedDate
                )";

            var parameters = new DynamicParameters();
            parameters.Add("@CommunicationID", communicationId);
            parameters.Add("@CurrAccTypeCode", 3); // Müşteri tipi kodu - normal akışla aynı
            parameters.Add("@CurrAccCode", request.CustomerCode);
            parameters.Add("@CommunicationTypeCode", request.CommunicationTypeCode);
            parameters.Add("@CommAddress", request.Communication); // CommAddress kullanıyoruz (normal akışla aynı)
            parameters.Add("@IsBlocked", false); // IsBlocked eklendi - normal akışla aynı
            parameters.Add("@CanSendAdvert", true); // CanSendAdvert eklendi - varsayılan değer
            parameters.Add("@IsConfirmed", false); // IsConfirmed eklendi - varsayılan değer
            parameters.Add("@SubCurrAccID", null); // SubCurrAccID eklendi - normal akışla aynı
            parameters.Add("@CreatedUserName", request.CreatedUserName ?? "SYSTEM");
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@LastUpdatedUserName", request.LastUpdatedUserName ?? "SYSTEM");
            parameters.Add("@LastUpdatedDate", DateTime.Now);

            await connection.ExecuteAsync(insertQuery, parameters);

            _logger.LogInformation("[CustomerServiceNew.CreateCustomerCommunicationAsync] - Müşteri iletişim bilgisi kaydedildi: {CustomerCode}, CommunicationID: {CommunicationID}", request.CustomerCode, communicationId);

            return new CustomerCommunicationResult
            {
                Success = true,
                CommunicationId = communicationId,
                Message = "İletişim bilgisi başarıyla kaydedildi"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[CustomerServiceNew.CreateCustomerCommunicationAsync] - Müşteri iletişim bilgisi kaydedilirken hata oluştu: {Message}", ex.Message);
            return new CustomerCommunicationResult
            {
                Success = false,
                Message = $"İletişim bilgisi kaydedilirken bir hata oluştu: {ex.Message}"
            };
        }
    }
}
}
