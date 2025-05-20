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
                    
                    // Diğer koşullu alanlar
                    if (!string.IsNullOrEmpty(request.IdentityNum))
                    {
                        parameters.Add("@IdentityNum", request.IdentityNum);
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
    }
}
