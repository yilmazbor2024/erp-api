using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Dapper;
using ErpMobile.Api.Data;
using ErpMobile.Api.Models.Customer;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Contact;

namespace ErpMobile.Api.Services
{
    public class CustomerBasicService : ICustomerService
    {
        private readonly ILogger<CustomerBasicService> _logger;
        private readonly IConfiguration _configuration;
        private readonly CustomerAddressService _addressService;
        private readonly CustomerContactService _contactService;
        private readonly CustomerCommunicationService _communicationService;
        private readonly CustomerLocationService _locationService;

        public CustomerBasicService(ILogger<CustomerBasicService> logger, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _configuration = configuration;
            
            // Alt servis sınıflarını oluştur - her biri için doğru tipte logger oluştur
            _addressService = new CustomerAddressService(loggerFactory.CreateLogger<CustomerAddressService>(), configuration);
            _contactService = new CustomerContactService(loggerFactory.CreateLogger<CustomerContactService>(), configuration);
            _communicationService = new CustomerCommunicationService(loggerFactory.CreateLogger<CustomerCommunicationService>(), configuration);
            _locationService = new CustomerLocationService(loggerFactory.CreateLogger<CustomerLocationService>(), configuration);
            
            _logger.LogInformation("[CustomerBasicService.Constructor] - CustomerBasicService başlatıldı");
        }

        /// <summary>
        /// Yeni müşteri oluşturur (Geliştirilmiş versiyon) - Detaylı yanıt döndürür
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        public async Task<CustomerDetailResponse> CreateCustomerDetailAsync(CustomerCreateRequestNew request)
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.CreateCustomerDetailAsync] - Geliştirilmiş API ile müşteri oluşturma isteği alındı");
                
                // Bu metot şu an için desteklenmiyor
                throw new NotImplementedException("Bu metot henüz uygulanmadı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.CreateCustomerDetailAsync] - Geliştirilmiş API ile müşteri oluşturma hatası: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Müşterinin var olup olmadığını kontrol eder
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri varsa true, yoksa false</returns>
        public async Task<bool> CustomerExistsAsync(string customerCode)
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.CustomerExistsAsync] - Müşteri varlık kontrolü başlatıldı: {CustomerCode}", customerCode);
                
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var query = "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3";
                    _logger.LogInformation("[CustomerBasicService.CustomerExistsAsync] - SQL: {Query}", query);
                    
                    var count = await connection.ExecuteScalarAsync<int>(query, new { CustomerCode = customerCode });
                    _logger.LogInformation("[CustomerBasicService.CustomerExistsAsync] - Sonuç: {Count}", count);
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.CustomerExistsAsync] - Müşteri varlık kontrolü sırasında hata oluştu: {CustomerCode}", customerCode);
                return false;
            }
        }

        public async Task<PagedResponse<CustomerListResponse>> GetCustomerListAsync(CustomerFilterRequest filter)
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.GetCustomerListAsync] - Müşteri listesi getirme isteği alındı");
                
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Create the base query as a CTE (Common Table Expression)
                    var sql = @"
                        WITH CustomerData AS (
                        SELECT CustomerCode = cdCurrAcc.CurrAccCode
                            , CustomerName = CASE WHEN (cdCurrAcc.CurrAccTypeCode = 8) OR (cdCurrAcc.CurrAccTypeCode = 4 AND cdCurrAcc.IsIndividualAcc = 1)
                                                THEN ISNULL(cdCurrAcc.FirstLastName, SPACE(0))
                                                ELSE ISNULL(cdCurrAccDesc.CurrAccDescription, SPACE(0))
                                                END
                            , cdCurrAcc.CustomerTypeCode
                            , CustomerTypeDescription = ISNULL((SELECT CustomerTypeDescription FROM bsCustomerTypeDesc WHERE bsCustomerTypeDesc.CustomerTypeCode = cdCurrAcc.CustomerTypeCode AND bsCustomerTypeDesc.LangCode = N'TR'),SPACE(0))
                            , CreatedDate = cdCurrAcc.Createddate
                            , CreatedUsername = cdCurrAcc.CreatedUsername
                            , cdCurrAcc.CurrencyCode
                            , IsVIP
                            , PromotionGroupDescription = ISNULL(cdPromotionGroupDesc.PromotionGroupDescription, SPACE(0))
                            , cdCurrAcc.CompanyCode
                            , cdCurrAcc.OfficeCode
                            , OfficeDescription = ISNULL((SELECT OfficeDescription FROM cdOfficeDesc WITH(NOLOCK) WHERE cdOfficeDesc.OfficeCode = cdCurrAcc.OfficeCode AND cdOfficeDesc.LangCode = N'TR'),SPACE(0))
                            , OfficeCountryCode = ISNULL((SELECT CountryCode FROM dfOfficeDefault WITH(NOLOCK) WHERE cdCurrAcc.OfficeCode = dfOfficeDefault.OfficeCode), SPACE(0))
                            , CountryDescription = ISNULL((SELECT CountryDescription FROM cdCountryDesc WITH(NOLOCK) WHERE cdCountryDesc.CountryCode = prCurrAccPostalAddress.CountryCode AND cdCountryDesc.LangCode = N'TR') , N'Türkiye')
                            , CityDescription = ISNULL((SELECT CityDescription FROM cdCityDesc WITH(NOLOCK) WHERE cdCityDesc.CityCode = prCurrAccPostalAddress.CityCode AND cdCityDesc.LangCode = N'TR') , SPACE(0))
                            , DistrictDescription = ISNULL((SELECT DistrictDescription FROM cdDistrictDesc WITH(NOLOCK) WHERE cdDistrictDesc.DistrictCode = prCurrAccPostalAddress.DistrictCode AND cdDistrictDesc.LangCode = N'TR') , SPACE(0))
                            , IdentityNum = cdCurrAcc.IdentityNum
                            , TaxNumber = cdCurrAcc.TaxNumber
                            , TaxOfficeCode = cdCurrAcc.TaxOfficeCode
                            , VendorCode = ISNULL(prCustomerVendorAccount.VendorCode, SPACE(0))
                            , IsSubjectToEInvoice = cdCurrAcc.IsSubjectToEInvoice
                            , UseDBSIntegration = cdCurrAcc.UseDBSIntegration
                            , cdCurrAcc.IsBlocked
                            , CurrAccType = cdCurrAcc.CurrAccTypeCode
                            , CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode
                            , Debit = ISNULL((SELECT SUM(ISNULL(cabcLoc.Debit, 0)) 
                                     FROM trCurrAccBook cab WITH(NOLOCK)
                                     LEFT OUTER JOIN trCurrAccBookCurrency cabcLoc WITH(NOLOCK) ON cabcLoc.CurrAccBookID = cab.CurrAccBookID AND cab.LocalCurrencyCode = cabcLoc.CurrencyCode
                                     WHERE cab.CurrAccCode = cdCurrAcc.CurrAccCode), 0)
                            , Credit = ISNULL((SELECT SUM(ISNULL(cabcLoc.Credit, 0)) 
                                     FROM trCurrAccBook cab WITH(NOLOCK)
                                     LEFT OUTER JOIN trCurrAccBookCurrency cabcLoc WITH(NOLOCK) ON cabcLoc.CurrAccBookID = cab.CurrAccBookID AND cab.LocalCurrencyCode = cabcLoc.CurrencyCode
                                     WHERE cab.CurrAccCode = cdCurrAcc.CurrAccCode), 0)
                            , Balance = ISNULL((SELECT SUM(ISNULL(cabcLoc.Credit, 0) - ISNULL(cabcLoc.Debit, 0)) 
                                     FROM trCurrAccBook cab WITH(NOLOCK)
                                     LEFT OUTER JOIN trCurrAccBookCurrency cabcLoc WITH(NOLOCK) ON cabcLoc.CurrAccBookID = cab.CurrAccBookID AND cab.LocalCurrencyCode = cabcLoc.CurrencyCode
                                     WHERE cab.CurrAccCode = cdCurrAcc.CurrAccCode), 0)
                            FROM cdCurrAcc WITH(NOLOCK)
                                    LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) ON cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR'
                                    LEFT OUTER JOIN cdPromotionGroupDesc WITH(NOLOCK) ON cdPromotionGroupDesc.PromotionGroupCode = cdCurrAcc.PromotionGroupCode AND cdPromotionGroupDesc.LangCode = N'TR'
                                    LEFT OUTER JOIN prCustomerVendorAccount WITH(NOLOCK) ON prCustomerVendorAccount.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode AND prCustomerVendorAccount.CurrAccCode = cdCurrAcc.CurrAccCode
                                    LEFT OUTER JOIN prCurrAccDefault WITH(NOLOCK) ON prCurrAccDefault.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode AND prCurrAccDefault.CurrAccCode = cdCurrAcc.CurrAccCode
                                    LEFT OUTER JOIN prCurrAccPostalAddress WITH(NOLOCK) ON prCurrAccPostalAddress.PostalAddressID = prCurrAccDefault.PostalAddressID
                            WHERE cdCurrAcc.CurrAccCode <> SPACE(0))";

                    // Create the WHERE conditions for filtering
                    var whereClause = "";
                    var parameters = new DynamicParameters();

                    // CurrAccTypeCode filtresi
                    if (filter.CurrAccTypeCode.HasValue)
                    {
                        whereClause += " AND CurrAccTypeCode = @CurrAccTypeCode";
                        parameters.Add("@CurrAccTypeCode", filter.CurrAccTypeCode.Value);
                    }

                    // Enhanced search capabilities - if search term is present in CustomerName or CustomerCode
                    if (!string.IsNullOrEmpty(filter.CustomerName))
                    {
                        whereClause += " AND (CustomerName LIKE '%' + @CustomerName + '%' OR CustomerCode LIKE '%' + @CustomerName + '%')";
                        parameters.Add("@CustomerName", filter.CustomerName);
                    }

                    // Diğer filtre koşulları...

                    // Prepare sorting
                    var sortColumn = string.IsNullOrEmpty(filter.SortColumn) ? "CustomerCode" : filter.SortColumn;
                    var sortDirection = string.IsNullOrEmpty(filter.SortDirection) ||
                                       (filter.SortDirection.ToLower() != "asc" && filter.SortDirection.ToLower() != "desc")
                                       ? "asc" : filter.SortDirection;

                    // Set paging parameters
                    parameters.Add("@PageNumber", filter.PageNumber);
                    parameters.Add("@PageSize", filter.PageSize);

                    // Execute count query - include full CTE declaration
                    var countSql = $@"{sql}
                        SELECT COUNT(*) FROM CustomerData
                        WHERE 1=1 {whereClause}";

                    var totalRecords = await connection.ExecuteScalarAsync<int>(countSql, parameters);

                    // Execute main query with pagination
                    var dataSql = $@"{sql}
                        SELECT * FROM CustomerData
                        WHERE 1=1 {whereClause}
                        ORDER BY {sortColumn} {sortDirection}
                        OFFSET (@PageNumber - 1) * @PageSize ROWS
                        FETCH NEXT @PageSize ROWS ONLY";

                    var customers = await connection.QueryAsync<CustomerListResponse>(dataSql, parameters);

                    return new PagedResponse<CustomerListResponse>
                    {
                        Data = customers.ToList(),
                        PageNumber = filter.PageNumber,
                        PageSize = filter.PageSize,
                        TotalRecords = totalRecords,
                        TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize)
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.GetCustomerListAsync] - Müşteri listesi getirme sırasında hata oluştu. Filter: {@Filter}", filter);
                throw;
            }
        }

        public async Task<CustomerDetailResponse> GetCustomerByCodeAsync(string customerCode, int? currAccTypeCode = null)
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.GetCustomerByCodeAsync] - Müşteri detay bilgisi getirme isteği alındı: {CustomerCode}", customerCode);
                
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            c.CurrAccCode AS CustomerCode,
                            CASE 
                                WHEN (c.CurrAccTypeCode = 8) OR (c.CurrAccTypeCode = 4 AND c.IsIndividualAcc = 1)
                                                THEN ISNULL(c.FirstLastName, '')
                                ELSE ISNULL(cd.CurrAccDescription, '')
                            END AS CustomerName,
                            c.CustomerTypeCode,
                            ISNULL(ctd.CustomerTypeDescription, '') AS CustomerTypeDescription,
                            c.CreatedDate,
                            c.CreatedUsername,
                            c.CurrencyCode,
                            c.IsVIP,
                            c.PromotionGroupCode,
                            ISNULL(pgd.PromotionGroupDescription, '') AS PromotionGroupDescription,
                            c.CompanyCode,
                            c.OfficeCode,
                            ISNULL(od.OfficeDescription, '') AS OfficeDescription,
                            c.IdentityNum,
                            c.TaxNumber,
                            c.TaxOfficeCode,
                            c.IsSubjectToEInvoice,
                            CASE WHEN c.EShipmentStartDate > '1900-01-01' THEN 1 ELSE 0 END AS IsSubjectToEWaybill,
                            c.EInvoiceStartDate,
                            c.EShipmentStartDate AS EWaybillStartDate,
                            c.UseDBSIntegration,
                            c.IsBlocked,
                            c.IsIndividualAcc AS IsRealPerson,
                            c.CurrAccTypeCode
                        FROM cdCurrAcc c WITH(NOLOCK)
                        LEFT JOIN cdCurrAccDesc cd WITH(NOLOCK) ON cd.CurrAccTypeCode = c.CurrAccTypeCode AND cd.CurrAccCode = c.CurrAccCode AND cd.LangCode = 'TR'
                        LEFT JOIN bsCustomerTypeDesc ctd WITH(NOLOCK) ON ctd.CustomerTypeCode = c.CustomerTypeCode AND ctd.LangCode = 'TR'
                        LEFT JOIN cdPromotionGroupDesc pgd WITH(NOLOCK) ON pgd.PromotionGroupCode = c.PromotionGroupCode AND pgd.LangCode = 'TR'
                        LEFT JOIN cdOfficeDesc od WITH(NOLOCK) ON od.OfficeCode = c.OfficeCode AND od.LangCode = 'TR'
                        WHERE c.CurrAccCode = @CustomerCode";

                    // CurrAccTypeCode filtresi ekle
                    if (currAccTypeCode.HasValue)
                    {
                        sql += " AND c.CurrAccTypeCode = @CurrAccTypeCode";
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("@CustomerCode", customerCode);
                    
                    if (currAccTypeCode.HasValue)
                    {
                        parameters.Add("@CurrAccTypeCode", currAccTypeCode.Value);
                    }

                    var customer = await connection.QueryFirstOrDefaultAsync<CustomerDetailResponse>(sql, parameters);

                    if (customer == null)
                    {
                        return null;
                    }

                    // Adres bilgilerini getir
                    customer.Addresses = (await _addressService.GetCustomerAddressesAsync(customerCode)).ToList();

                    // İletişim bilgilerini getir
                    customer.Communications = (await _communicationService.GetCustomerCommunicationsAsync(customerCode)).ToList();

                    // Kişi bilgilerini getir
                    customer.Contacts = (await _contactService.GetCustomerContactsAsync(customerCode)).ToList();

                    // Finansal bilgileri getir
                    try
                    {
                        var financialSql = @"
                            SELECT 
                                SUM(ISNULL(Loc_Debit, 0)) AS TotalDebit,
                                SUM(ISNULL(Loc_Credit, 0)) AS TotalCredit,
                                SUM(ISNULL(Loc_Debit, 0) - ISNULL(Loc_Credit, 0)) AS Balance
                            FROM (
                                SELECT
                                    cab.CurrAccBookID,
                                    cab.CurrAccTypeCode,
                                    cab.CurrAccCode,
                                    cab.DocumentDate,
                                    cab.DocumentNumber,
                                    cab.DueDate,
                                    cab.LineDescription,
                                    cab.DocCurrencyCode AS Doc_CurrencyCode,
                                    ISNULL(cabcDoc.Debit, 0) AS Doc_Debit,
                                    ISNULL(cabcDoc.Credit, 0) AS Doc_Credit,
                                    cab.LocalCurrencyCode AS Loc_CurrencyCode,
                                    ISNULL(cabcLoc.ExchangeRate, 0) AS Loc_ExchangeRate,
                                    ISNULL(cabcLoc.Debit, 0) AS Loc_Debit,
                                    ISNULL(cabcLoc.Credit, 0) AS Loc_Credit,
                                    gd.CompanyCurrencyCode AS Com_CurrencyCode,
                                    ISNULL(cabcCom.ExchangeRate, 0) AS Com_ExchangeRate,
                                    ISNULL(cabcCom.Debit, 0) AS Com_Debit,
                                    ISNULL(cabcCom.Credit, 0) AS Com_Credit
                                FROM trCurrAccBook cab WITH(NOLOCK)
                                INNER JOIN dfGlobalDefault gd WITH(NOLOCK) ON gd.GlobalDefaultCode = 1
                                LEFT OUTER JOIN trCurrAccBookCurrency cabcDoc WITH(NOLOCK) ON cabcDoc.CurrAccBookID = cab.CurrAccBookID AND cab.DocCurrencyCode = cabcDoc.CurrencyCode
                                LEFT OUTER JOIN trCurrAccBookCurrency cabcLoc WITH(NOLOCK) ON cabcLoc.CurrAccBookID = cab.CurrAccBookID AND cab.LocalCurrencyCode = cabcLoc.CurrencyCode
                                LEFT OUTER JOIN trCurrAccBookCurrency cabcCom WITH(NOLOCK) ON cabcCom.CurrAccBookID = cab.CurrAccBookID AND gd.CompanyCurrencyCode = cabcCom.CurrencyCode
                                WHERE cab.CurrAccCode = @CustomerCode
                            ) AS FinancialData";
                        
                        

                        var financialInfo = await connection.QueryFirstOrDefaultAsync<dynamic>(financialSql, new { CustomerCode = customerCode });
                        
                        if (financialInfo != null)
                        {
                            customer.TotalDebit = financialInfo.TotalDebit;
                            customer.TotalCredit = financialInfo.TotalCredit;
                            customer.Balance = financialInfo.Balance;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "[CustomerBasicService.GetCustomerByCodeAsync] - Finansal bilgiler getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                        // Finansal bilgileri getirirken hata oluşursa, işlemi durdurmadan devam et
                    }

                    return customer;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.GetCustomerByCodeAsync] - Müşteri detay bilgisi getirme sırasında hata oluştu. CustomerCode: {CustomerCode}, CurrAccTypeCode: {CurrAccTypeCode}", customerCode, currAccTypeCode);
                throw;
            }
        }

        public async Task<CustomerDetailResponse> CreateCustomerAsync(CustomerCreateRequestNew request)
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - Geliştirilmiş API ile müşteri oluşturma isteği alındı");
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Temel müşteri bilgilerini oluştur
                            _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - Temel müşteri bilgileri oluşturuluyor");
                            string insertQuery = @"
                                INSERT INTO cdCurrAcc (
                                    CurrAccTypeCode, CurrAccCode, CustomerTypeCode, CurrencyCode, IsVIP, PromotionGroupCode,
                                    CompanyCode, OfficeCode, ";
                                    
                            // Koşullu alanlar
                            if (!string.IsNullOrEmpty(request.IdentityNum)) insertQuery += "IdentityNum, ";
                            if (!string.IsNullOrEmpty(request.TaxNumber)) insertQuery += "TaxNumber, ";
                            if (!string.IsNullOrEmpty(request.TaxOfficeCode)) insertQuery += "TaxOfficeCode, ";
                            
                            // Kalan alanlar
                            insertQuery += @"IsSubjectToEInvoice,
                                    UseDBSIntegration, IsBlocked, CreatedDate, CreatedUsername, LastUpdatedDate, LastUpdatedUsername,
                                    IsIndividualAcc)
                                VALUES (
                                    3, @CustomerCode, @CustomerTypeCode, @CurrencyCode, @IsVIP, @PromotionGroupCode,
                                    @CompanyCode, @OfficeCode, ";
                                    
                            // Koşullu değerler
                            if (!string.IsNullOrEmpty(request.IdentityNum)) insertQuery += "@IdentityNum, ";
                            if (!string.IsNullOrEmpty(request.TaxNumber)) insertQuery += "@TaxNumber, ";
                            if (!string.IsNullOrEmpty(request.TaxOfficeCode)) insertQuery += "@TaxOfficeCode, ";
                            
                            // Kalan değerler
                            insertQuery += @"@IsSubjectToEInvoice,
                                    @UseDBSIntegration, @IsBlocked, GETDATE(), @CreatedUserName, GETDATE(), @LastUpdatedUserName,
                                    @IsRealPerson)";
                            
                            var parameters = new
                            {
                                request.CustomerCode,
                                request.CustomerTypeCode,
                                request.CurrencyCode,
                                request.IsVIP,
                                request.PromotionGroupCode,
                                request.CompanyCode,
                                request.OfficeCode,
                                IdentityNum = !string.IsNullOrEmpty(request.IdentityNum) ? request.IdentityNum : null,
                                TaxNumber = !string.IsNullOrEmpty(request.TaxNumber) ? request.TaxNumber : null,
                                TaxOfficeCode = !string.IsNullOrEmpty(request.TaxOfficeCode) ? request.TaxOfficeCode : null,
                                request.IsSubjectToEInvoice,
                                request.UseDBSIntegration,
                                request.IsBlocked,
                                request.CreatedUserName,
                                request.LastUpdatedUserName,
                                request.IsRealPerson
                                // MersisNum, TitleCode, Patronym, DueDateFormulaCode, DiscountGroupCode, PaymentPlanGroupCode, RiskLimit, CreditLimit parametreleri kaldırıldı
                            };
                            
                            await connection.ExecuteAsync(insertQuery, parameters, transaction);

                            // Müşteri açıklamasını oluştur
                            _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - Müşteri açıklaması oluşturuluyor");
                            string descInsertQuery = @"
                                INSERT INTO cdCurrAccDesc (
                                    CurrAccTypeCode, CurrAccCode, LangCode, CurrAccDescription
                                )
                                VALUES (
                                    3, @CustomerCode, 'TR', @CustomerName
                                )";

                            await connection.ExecuteAsync(descInsertQuery, new
                            {
                                request.CustomerCode,
                                request.CustomerName
                            }, transaction);
                            
                            // prCurrAccDefault tablosuna kayıt ekleme
                            _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - prCurrAccDefault tablosuna kayıt ekleme");
                            var defaultSql = @"
                                INSERT INTO prCurrAccDefault (
                                    CurrAccTypeCode, CurrAccCode, CreatedUserName, CreatedDate, 
                                    LastUpdatedUserName, LastUpdatedDate
                                )
                                VALUES (
                                    3, @CustomerCode, @CreatedUserName, GETDATE(), 
                                    @LastUpdatedUserName, GETDATE()
                                )";

                            await connection.ExecuteAsync(defaultSql, new
                            {
                                request.CustomerCode,
                                CreatedUserName = request.CreatedUserName ?? "SYSTEM",
                                LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                            }, transaction);

                            // Adres bilgilerini ekleme
                            _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - Adres bilgileri ekleniyor");
                            if (request.Addresses != null && request.Addresses.Any())
                            {
                                foreach (var address in request.Addresses)
                                {
                                    // CustomerAddressService'i kullanarak adresi ekle
                                    var addressRequest = new CustomerAddressCreateRequestNew
                                    {
                                        CustomerCode = request.CustomerCode,
                                        AddressTypeCode = address.AddressTypeCode,
                                        Address = address.Address,
                                        CountryCode = address.CountryCode,
                                        StateCode = address.StateCode,
                                        CityCode = address.CityCode,
                                        DistrictCode = address.DistrictCode,
                                        TaxOfficeCode = address.TaxOfficeCode,
                                        TaxNumber = address.TaxNumber,
                                        IsBlocked = address.IsBlocked,
                                        IsDefault = address.IsDefault,
                                        CreatedUserName = request.CreatedUserName,
                                        LastUpdatedUserName = request.LastUpdatedUserName
                                    };

                                    await _addressService.AddCustomerAddressInternalAsync(addressRequest, connection, transaction);
                                }

                                // Varsayılan adresi otomatik seçilecek şekilde güncelle
                                var defaultAddress = request.Addresses.FirstOrDefault(a => a.IsDefault) ?? request.Addresses.First();
                                await SetDefaultAddress(connection, transaction, request.CustomerCode, defaultAddress.AddressTypeCode);
                            }

                            // İletişim bilgilerini ekleme
                            _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - İletişim bilgileri ekleniyor");
                            if (request.Communications != null && request.Communications.Any())
                            {
                                foreach (var communication in request.Communications)
                                {
                                    // CustomerCommunicationService'i kullanarak iletişim bilgisini ekle
                                    var communicationRequest = new CustomerCommunicationCreateRequestNew
                                    {
                                        CustomerCode = request.CustomerCode,
                                        CommunicationTypeCode = communication.CommunicationTypeCode,
                                        CommAddress = communication.CommAddress,
                                        CanSendAdvert = communication.CanSendAdvert, // Doğru özellik adı kullanılıyor
                                        IsBlocked = communication.IsBlocked,
                                        IsConfirmed = false, // Varsayılan değer atandı
                                        IsDefault = communication.IsDefault,
                                        CreatedUserName = request.CreatedUserName ?? "SYSTEM",
                                        LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                                    };

                                    await _communicationService.AddCustomerCommunicationInternalAsync(communicationRequest, connection, transaction);
                                }

                                // Varsayılan iletişimi prCurrAccDefault tablosuna yaz
                                var defaultComm = request.Communications.FirstOrDefault(c => c.IsDefault);
                                if (defaultComm != null)
                                {
                                    await SetDefaultCommunication(connection, transaction, request.CustomerCode, defaultComm.CommunicationTypeCode);
                                }
                            }

                            // Kişi bilgilerini ekleme
                            _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - Kişi bilgileri ekleniyor");
                            if (request.Contacts != null && request.Contacts.Any())
                            {
                                foreach (var contact in request.Contacts)
                                {
                                    var contactRequest = new CustomerContactCreateRequestNew
                                    {
                                        CustomerCode = request.CustomerCode,
                                        ContactTypeCode = "C", // Bağlantılı kişi tipi kodu (C: BAĞLANTILI)
                                        FirstName = contact.FirstName ?? "", // FirstName alanını doğru şekilde kullan
                                        LastName = contact.LastName ?? "", // LastName alanını doğru şekilde kullan
                                        IdentityNum = contact.IdentityNum, // IdentityNum kullanılmalı
                                        IsBlocked = false, // Sabit false olarak ayarla
                                        IsAuthorized = false, // Sabit false olarak ayarla
                                        IsDefault = contact.IsDefault,
                                        CreatedUserName = request.CreatedUserName ?? "SYSTEM",
                                        LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                                    };

                                    await _contactService.AddCustomerContactInternalAsync(contactRequest, connection, transaction);
                                }
                            }

                            await transaction.CommitAsync();

                            // Dönüş türünü düzeltiyoruz
                            return await GetCustomerByCodeAsync(request.CustomerCode);
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "[CustomerBasicService.CreateCustomerAsync] - Müşteri oluşturma sırasında hata oluştu. Request: {@Request}", request);
                            throw; // Re-throw the exception after logging and rollback
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.CreateCustomerAsync] - Müşteri oluşturma sırasında hata oluştu. Request: {@Request}", request);
                throw;
            }
        }

        public async Task<CustomerResponse> CreateCustomerAsync(CustomerCreateRequest request)
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - Müşteri oluşturma isteği alındı");
                _logger.LogInformation("Müşteri oluşturma işlemi başlatıldı: {CustomerCode}", request.CustomerCode);

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Müşteri temel bilgilerini oluştur
                            _logger.LogInformation("[CustomerBasicService.CreateCustomerAsync] - Temel müşteri bilgileri oluşturuluyor");
                            var customerId = await CreateCustomerBasicAsync(connection, transaction, request);

                            // İşlemi tamamla
                            await transaction.CommitAsync();

                            // Oluşturulan müşteriyi getir
                            return await GetCustomerByCodeAsync(request.CustomerCode);
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "[CustomerBasicService.CreateCustomerAsync] - Müşteri oluşturma işlemi sırasında hata oluştu: {CustomerCode}, Hata: {Message}", request.CustomerCode, ex.Message);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.CreateCustomerAsync] - Müşteri oluşturma işlemi sırasında hata oluştu: {CustomerCode}, Hata: {Message}", request.CustomerCode, ex.Message);
                throw;
            }
        }

        public async Task<CustomerDetailResponse> GetCustomerByIdAsync(string id)
        {
            // ID burada CustomerCode olarak kullanılıyor
            return await GetCustomerByCodeAsync(id, null);
        }

        public async Task<List<CustomerTypeResponse>> GetCustomerTypesAsync()
        {
            _logger.LogInformation("[CustomerBasicService.GetCustomerTypesAsync] - Müşteri tipleri getirme isteği alındı");
            return await GetCustomerTypesInternalAsync("TR");
        }

        // Dahili metot - dil kodu parametresi alır
        private async Task<List<CustomerTypeResponse>> GetCustomerTypesInternalAsync(string langCode)
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.GetCustomerTypesInternalAsync] - Dahili müşteri tipleri getirme isteği alındı: {LangCode}", langCode);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT
                            bsCurrAccType.CurrAccTypeCode as Code,
                            bsCurrAccType.IsBlocked,
                            @LangCode as LangCode,
                            RTRIM(LTRIM(ISNULL(CurrAccTypeDescription, SPACE(0)))) as Description
                        FROM bsCurrAccType WITH (NOLOCK) 
                        LEFT OUTER JOIN bsCurrAccTypeDesc WITH (NOLOCK) 
                            ON bsCurrAccTypeDesc.CurrAccTypeCode = bsCurrAccType.CurrAccTypeCode
                            AND bsCurrAccTypeDesc.LangCode = @LangCode
                        ORDER BY bsCurrAccType.CurrAccTypeCode";

                    var parameters = new DynamicParameters();
                    parameters.Add("@LangCode", langCode);

                    var result = await connection.QueryAsync<CustomerTypeResponse>(sql, parameters);
                    return result.ToList();
                }
            }
            catch (SqlException ex) // Catch specific SQL errors
            {
                // Log specific SQL errors
                _logger.LogWarning(ex, "[CustomerBasicService.GetCustomerTypesInternalAsync] - Database error occurred while getting customer types. Will return empty list.");
                return new List<CustomerTypeResponse>(); // Return empty on SQL error
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.GetCustomerTypesInternalAsync] - Error occurred while getting customer types. Will return empty list.");
                return new List<CustomerTypeResponse>(); // Return empty on general error
            }
        }

        public async Task<List<CustomerDiscountGroupResponse>> GetCustomerDiscountGroupsAsync()
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.GetCustomerDiscountGroupsAsync] - Müşteri indirim grupları getirme isteği alındı");
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            dg.DiscountGroupCode,
                            dgd.DiscountGroupDescription,
                            dg.IsBlocked
                        FROM cdDiscountGroup dg WITH(NOLOCK)
                        LEFT JOIN cdDiscountGroupDesc dgd WITH(NOLOCK) ON dgd.DiscountGroupCode = dg.DiscountGroupCode AND dgd.LangCode = 'TR'
                        ORDER BY dg.DiscountGroupCode";

                    var result = await connection.QueryAsync<CustomerDiscountGroupResponse>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.GetCustomerDiscountGroupsAsync] - Error occurred while getting customer discount groups. Will return empty list.");
                return new List<CustomerDiscountGroupResponse>(); // Return empty on error
            }
        }

        public async Task<bool> UpdateCustomerAsync(CustomerUpdateRequest request)
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.UpdateCustomerAsync] - Müşteri güncelleme isteği alındı: {CustomerCode}", request.CustomerCode);
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Temel müşteri bilgilerini güncelleme
                            _logger.LogInformation("[CustomerBasicService.UpdateCustomerAsync] - Temel müşteri bilgileri güncelleniyor");
                            var sql = @"
                                UPDATE cdCurrAcc SET
                                    CustomerTypeCode = @CustomerTypeCode,
                                    CurrencyCode = @CurrencyCode,
                                    IsVIP = @IsVIP,
                                    PromotionGroupCode = @PromotionGroupCode,
                                    CompanyCode = @CompanyCode,
                                    OfficeCode = @OfficeCode,
                                    IdentityNum = @IdentityNum,
                                    TaxNumber = @TaxNumber,
                                    TaxOfficeCode = @TaxOfficeCode,
                                    IsSubjectToEInvoice = @IsSubjectToEInvoice,
                                    UseDBSIntegration = @UseDBSIntegration,
                                    IsBlocked = @IsBlocked,
                                    LastUpdatedDate = GETDATE(),
                                    LastUpdatedUsername = @LastUpdatedUserName,
                                    IsIndividualAcc = @IsRealPerson
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode";

                            await connection.ExecuteAsync(sql, new
                            {
                                request.CustomerCode,
                                request.CustomerTypeCode,
                                request.CurrencyCode,
                                request.IsVIP,
                                request.PromotionGroupCode,
                                request.CompanyCode,
                                request.OfficeCode,
                                request.IdentityNum,
                                request.TaxNumber,
                                request.TaxOfficeCode,
                                request.IsSubjectToEInvoice,
                                request.UseDBSIntegration,
                                request.IsBlocked,
                                request.LastUpdatedUserName,
                                request.IsRealPerson
                            }, transaction);

                            // Müşteri açıklamasını güncelleme
                            if (!string.IsNullOrEmpty(request.CustomerName))
                            {
                                var descSql = @"
                                    MERGE cdCurrAccDesc WITH (HOLDLOCK) AS target
                                    USING (SELECT 
                                        3 AS CurrAccTypeCode,
                                        @CustomerCode AS CurrAccCode,
                                        'TR' AS LangCode,
                                        @CustomerName AS CurrAccDescription
                                    ) AS source
                                    ON (target.CurrAccTypeCode = source.CurrAccTypeCode AND target.CurrAccCode = source.CurrAccCode AND target.LangCode = source.LangCode)
                                    WHEN MATCHED THEN
                                        UPDATE SET CurrAccDescription = source.CurrAccDescription
                                    WHEN NOT MATCHED THEN
                                        INSERT (CurrAccTypeCode, CurrAccCode, LangCode, CurrAccDescription)
                                        VALUES (source.CurrAccTypeCode, source.CurrAccCode, source.LangCode, source.CurrAccDescription);";

                                await connection.ExecuteAsync(descSql, new
                                {
                                    request.CustomerCode,
                                    request.CustomerName
                                }, transaction);
                            }

                            await transaction.CommitAsync();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "[CustomerBasicService.UpdateCustomerAsync] - Müşteri güncellenirken hata oluştu. Request: {@Request}", request);
                            throw; // Re-throw the exception after logging and rollback
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.UpdateCustomerAsync] - Müşteri güncellenirken hata oluştu. Request: {@Request}", request);
                throw;
            }
        }

        /// <summary>
        /// Müşteri bilgilerini günceller (Geliştirilmiş versiyon)
        /// </summary>
        /// <param name="request">Müşteri güncelleme isteği</param>
        /// <returns>Güncellenen müşteri bilgileri</returns>
        public async Task<CustomerUpdateResponseNew> UpdateCustomerAsync(CustomerUpdateRequestNew request)
        {
            _logger.LogInformation("Müşteri güncelleme işlemi başlatıldı: {CustomerCode}", request.CustomerCode);
            
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Müşteri temel bilgilerini güncelleme SQL sorgusu
                            _logger.LogInformation("[CustomerBasicService.UpdateCustomerAsync] - Müşteri temel bilgileri güncelleniyor");
                            var sql = @"
                                UPDATE cdCurrAcc
                                SET
                                    CurrAccDesc = @CurrAccDesc,
                                    TaxNumber = @TaxNumber,
                                    TaxOfficeCode = @TaxOfficeCode,
                                    CurrAccTypeCode = @CurrAccTypeCode,
                                    IdentityNumber = @IdentityNumber,
                                    CreditLimit = @CreditLimit,
                                    MinBalance = @MinBalance,
                                    PaymentTerm = 30,
                                    LastUpdatedUserName = @LastUpdatedUserName,
                                    LastUpdatedDate = GETDATE()
                                WHERE
                                    CurrAccCode = @CurrAccCode";
                            
                            var parameters = new
                            {
                                CurrAccCode = request.CustomerCode,
                                CurrAccDesc = request.CustomerName,
                                TaxNumber = request.TaxNumber,
                                TaxOfficeCode = request.TaxOfficeCode,
                                CurrAccTypeCode = request.CustomerTypeCode,
                                IdentityNumber = request.CustomerIdentityNumber, // Orijinal halini geri getir
                                CreditLimit = 0, // Frontend'den gönderilmeyecek, varsayılan değer kullanılıyor
                                MinBalance = request.MinBalance,
                                LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                            };
                            
                            await connection.ExecuteAsync(sql, parameters, transaction);
                            
                            // Müşteri açıklama bilgilerini güncelleme
                            _logger.LogInformation("[CustomerBasicService.UpdateCustomerAsync] - Müşteri açıklama bilgileri güncelleniyor");
                            var descSql = @"
                                UPDATE cdCurrAccDesc
                                SET
                                    CurrAccDesc = @CurrAccDesc,
                                    LastUpdatedUserName = @LastUpdatedUserName,
                                    LastUpdatedDate = GETDATE()
                                WHERE
                                    CurrAccCode = @CurrAccCode
                                    AND LangCode = @LangCode";
                            
                            var descParameters = new
                            {
                                CurrAccCode = request.CustomerCode,
                                CurrAccDesc = request.CustomerName,
                                LangCode = request.DataLanguageCode ?? "TR",
                                LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                            };
                            
                            await connection.ExecuteAsync(descSql, descParameters, transaction);
                            
                            // İletişim bilgilerini güncelleme
                            if (request.Communications != null && request.Communications.Any())
                            {
                                foreach (var comm in request.Communications)
                                {
                                    // İletişim bilgisini CustomerCommunicationCreateRequestNew'e dönüştür
                                    var commRequest = new CustomerCommunicationCreateRequestNew
                                    {
                                        CustomerCode = request.CustomerCode,
                                        CommunicationTypeCode = comm.CommunicationTypeCode,
                                        CommAddress = comm.CommAddress,
                                        CanSendAdvert = comm.CanSendAdvert,
                                        IsBlocked = comm.IsBlocked,
                                        IsConfirmed = comm.IsConfirmed,
                                        IsDefault = comm.IsDefault,
                                        CreatedUserName = request.LastUpdatedUserName ?? "SYSTEM",
                                        LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                                    };
                                    
                                    await _communicationService.AddCustomerCommunicationInternalAsync(commRequest, connection, transaction);
                                }
                            }
                            
                            // Adres bilgilerini güncelleme
                            if (request.Addresses != null && request.Addresses.Any())
                            {
                                foreach (var addr in request.Addresses)
                                {
                                    // Adres bilgisini CustomerAddressCreateRequestNew'e dönüştür
                                    var addrRequest = new CustomerAddressCreateRequestNew
                                    {
                                        CustomerCode = request.CustomerCode,
                                        AddressTypeCode = addr.AddressTypeCode,
                                        CountryCode = addr.CountryCode,
                                        StateCode = addr.StateCode,
                                        CityCode = addr.CityCode,
                                        DistrictCode = addr.DistrictCode,
                                        Address = addr.Address,
                                        IsDefault = addr.IsDefault,
                                        CreatedUserName = request.LastUpdatedUserName ?? "SYSTEM",
                                        LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                                    };
                                    
                                    await _addressService.AddCustomerAddressInternalAsync(addrRequest, connection, transaction);
                                }
                            }
                            
                            // Kişi bilgilerini güncelleme
                            if (request.Contacts != null && request.Contacts.Any())
                            {
                                foreach (var contact in request.Contacts)
                                {
                                    // Kişi bilgisini CustomerContactCreateRequestNew'e dönüştür
                                    var contactRequest = new CustomerContactCreateRequestNew
                                    {
                                        CustomerCode = request.CustomerCode,
                                        ContactTypeCode = contact.ContactTypeCode,
                                        FirstName = contact.FirstName,
                                        LastName = contact.LastName,
                                        IdentityNum = contact.IdentityNum, // IdentityNum kullanılmalı
                                        IsBlocked = contact.IsBlocked,
                                        IsAuthorized = contact.IsAuthorized,
                                        IsDefault = contact.IsDefault,
                                        CreatedUserName = request.LastUpdatedUserName ?? "SYSTEM",
                                        LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                                    };
                                    
                                    await _contactService.AddCustomerContactInternalAsync(contactRequest, connection, transaction);
                                }
                            }
                            
                            // İşlemi tamamla
                            await transaction.CommitAsync();
                            
                            // Başarılı yanıt döndür
                            return new CustomerUpdateResponseNew
                            {
                                Success = true,
                                Message = "Müşteri bilgileri başarıyla güncellendi",
                                CustomerCode = request.CustomerCode
                            };
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "[CustomerBasicService.UpdateCustomerAsync] - Müşteri güncelleme işlemi sırasında hata oluştu: {CustomerCode}, Hata: {Message}", request.CustomerCode, ex.Message);
                            
                            return new CustomerUpdateResponseNew
                            {
                                Success = false,
                                Message = $"Müşteri güncelleme işlemi sırasında hata oluştu: {ex.Message}",
                                CustomerCode = request.CustomerCode
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.CreateCustomerAsync] - Müşteri oluşturma işlemi sırasında hata oluştu: {CustomerCode}, Hata: {Message}", request.CustomerCode, ex.Message);
                
                return new CustomerUpdateResponseNew
                {
                    Success = false,
                    Message = $"Müşteri güncelleme işlemi sırasında hata oluştu: {ex.Message}",
                    CustomerCode = request.CustomerCode
                };
            }
        }
        /// <summary>
        /// Müşteri finansal bilgilerini günceller
        /// </summary>
        /// <param name="request">Müşteri finansal bilgi güncelleme isteği</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        public async Task<CustomerFinancialUpdateResponse> UpdateCustomerFinancialAsync(CustomerFinancialUpdateRequest request)
        {
            _logger.LogInformation("Müşteri finansal bilgileri güncelleme işlemi başlatıldı: {CustomerCode}", request.CustomerCode);
            
            var response = new CustomerFinancialUpdateResponse
            {
                CustomerCode = request.CustomerCode,
                Success = false,
                Message = string.Empty,
                CreditLimit = request.CreditLimit ?? 0,
                CurrencyCode = request.CurrencyCode,
                PaymentPlanCode = request.CustomerPaymentPlanGrCode,
                UpdatedDate = DateTime.Now,
                UpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
            };
            
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    
                    // Önce müşterinin var olup olmadığını kontrol et
                    var checkQuery = "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3";
                    var exists = await connection.ExecuteScalarAsync<int>(checkQuery, new { CustomerCode = request.CustomerCode }) > 0;
                    
                    if (!exists)
                    {
                        response.Success = false;
                        response.Message = $"Müşteri bulunamadı: {request.CustomerCode}";
                        return response;
                    }
                    
                    // Müşteri finansal bilgilerini güncelleme SQL sorgusu
                    _logger.LogInformation("[CustomerBasicService.UpdateCustomerFinancialAsync] - Müşteri finansal bilgileri güncelleniyor");
                    var sql = @"
                        UPDATE cdCurrAcc
                        SET
                            CurrencyCode = @CurrencyCode,
                            CreditLimit = @CreditLimit,
                            RiskLimit = @RiskLimit,
                            MinBalance = @MinBalance,
                            PaymentTerm = @PaymentTerm,
                            DueDateFormulaCode = @DueDateFormulaCode,
                            BankCode = @BankCode,
                            BankBranchCode = @BankBranchCode,
                            BankAccTypeCode = @BankAccTypeCode,
                            IBAN = @IBAN,
                            SWIFTCode = @SWIFTCode,
                            BankAccNo = @BankAccNo,
                            CustomerDiscountGrCode = @CustomerDiscountGrCode,
                            CustomerMarkupGrCode = @CustomerMarkupGrCode,
                            CustomerPaymentPlanGrCode = @CustomerPaymentPlanGrCode,
                            VendorPaymentPlanGrCode = @VendorPaymentPlanGrCode,
                            RetailSalePriceGroupCode = @RetailSalePriceGroupCode,
                            WholesalePriceGroupCode = @WholesalePriceGroupCode,
                            LastUpdatedUserName = @LastUpdatedUserName,
                            LastUpdatedDate = GETDATE()
                        WHERE
                            CurrAccTypeCode = 3 AND
                            CurrAccCode = @CurrAccCode";
                    
                    var parameters = new
                    {
                        CurrAccCode = request.CustomerCode,
                        CurrencyCode = request.CurrencyCode,
                        CreditLimit = request.CreditLimit ?? 0,
                        RiskLimit = request.RiskLimit ?? 0,
                        MinBalance = request.MinBalance ?? 0,
                        PaymentTerm = request.PaymentTerm ?? 30,
                        DueDateFormulaCode = request.DueDateFormulaCode,
                        BankCode = request.BankCode,
                        BankBranchCode = request.BankBranchCode,
                        BankAccTypeCode = request.BankAccTypeCode ?? 0,
                        IBAN = request.IBAN,
                        SWIFTCode = request.SWIFTCode,
                        BankAccNo = request.BankAccNo,
                        CustomerDiscountGrCode = request.CustomerDiscountGrCode,
                        CustomerMarkupGrCode = request.CustomerMarkupGrCode,
                        CustomerPaymentPlanGrCode = request.CustomerPaymentPlanGrCode,
                        VendorPaymentPlanGrCode = request.VendorPaymentPlanGrCode,
                        RetailSalePriceGroupCode = request.RetailSalePriceGroupCode,
                        WholesalePriceGroupCode = request.WholesalePriceGroupCode,
                        LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                    };
                    
                    var result = await connection.ExecuteAsync(sql, parameters);
                    
                    _logger.LogInformation("[CustomerBasicService.UpdateCustomerFinancialAsync] - Müşteri finansal bilgileri güncelleme işlemi tamamlandı: {CustomerCode}, Etkilenen satır: {AffectedRows}", request.CustomerCode, result);
                    
                    if (result > 0)
                    {
                        response.Success = true;
                        response.Message = "Müşteri finansal bilgileri başarıyla güncellendi";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Müşteri finansal bilgileri güncellenemedi";
                    }
                    
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.UpdateCustomerFinancialAsync] - Müşteri finansal bilgileri güncellenirken hata oluştu: {CustomerCode}, Hata: {Message}", request.CustomerCode, ex.Message);
                
                response.Success = false;
                response.Message = $"Müşteri finansal bilgileri güncellenirken hata oluştu: {ex.Message}";
                
                return response;
            }
        }

        /// <summary>
        /// Müşteri temel bilgilerini oluşturur
        /// </summary>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri ID'si</returns>
        private async Task<Guid> CreateCustomerBasicAsync(SqlConnection connection, SqlTransaction transaction, CustomerCreateRequest request)
        {
            // Müşteri ID'si oluştur
            var customerId = Guid.NewGuid();
            
            // Müşteri temel bilgilerini ekleme SQL sorgusu
            var sql = @"
                INSERT INTO cdCurrAcc (
                    CurrAccID, CurrAccCode, CurrAccDesc, OfficeCode, CompanyCode, 
                    CurrAccTypeCode, TaxNumber, TaxOfficeCode, 
                    IdentityNumber, CreditLimit, RiskLimit, PaymentTerm, 
                    CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                ) VALUES (
                    @CurrAccID, @CurrAccCode, @CurrAccDesc, @OfficeCode, @CompanyCode, 
                    @CurrAccTypeCode, @TaxNumber, @TaxOfficeCode, 
                    @IdentityNumber, @CreditLimit, @RiskLimit, @PaymentTerm, 
                    @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                )";
            
            var parameters = new
            {
                CurrAccID = customerId,
                CurrAccCode = request.CustomerCode,
                CurrAccDesc = request.CustomerName,
                OfficeCode = request.OfficeCode ?? "M",
                CompanyCode = request.CompanyCode ?? "1",
                CurrAccTypeCode = request.CustomerTypeCode,
                TaxNumber = request.TaxNumber,
                TaxOfficeCode = request.TaxOfficeCode,
                IdentityNumber = request.CustomerIdentityNumber, // Orijinal halini geri getir
                CreditLimit = 0, // Frontend'den gönderilmeyecek, varsayılan değer kullanılıyor
                RiskLimit = 0, // Varsayılan değer
                PaymentTerm = 30, // Varsayılan değer
                CreatedUserName = request.CreatedUserName ?? "SYSTEM",
                LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
            };
            
            await connection.ExecuteAsync(sql, parameters, transaction);
            
            // Müşteri açıklama bilgilerini ekleme
            var descSql = @"
                INSERT INTO cdCurrAccDesc (
                    CurrAccCode, LangCode, CurrAccDesc, 
                    CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                ) VALUES (
                    @CurrAccCode, @LangCode, @CurrAccDesc, 
                    @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                )";
            
            var descParameters = new
            {
                CurrAccCode = request.CustomerCode,
                LangCode = request.DataLanguageCode ?? "TR",
                CurrAccDesc = request.CustomerName,
                CreatedUserName = request.CreatedUserName ?? "SYSTEM",
                LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
            };
            
            await connection.ExecuteAsync(descSql, descParameters, transaction);
            
            return customerId;
        }

        #region Default Operations
        private async Task SetDefaultAddress(SqlConnection connection, SqlTransaction transaction, string customerCode, string addressTypeCode)
        {
            try
            {
                var sql = @"
                    MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                    USING (
                        SELECT 
                            pa.PostalAddressID,
                            @CustomerCode AS CurrAccCode,
                            3 AS CurrAccTypeCode,
                            'A' AS DefaultTypeCode
                        FROM prCurrAccPostalAddress pa
                        WHERE pa.CurrAccCode = @CustomerCode 
                        AND pa.CurrAccTypeCode = 3
                        AND pa.AddressTypeCode = @AddressTypeCode
                    ) AS source
                    ON (
                        target.CurrAccCode = source.CurrAccCode AND 
                        target.CurrAccTypeCode = source.CurrAccTypeCode AND 
                        target.DefaultTypeCode = source.DefaultTypeCode
                    )
                    WHEN MATCHED THEN
                        UPDATE SET PostalAddressID = source.PostalAddressID
                    WHEN NOT MATCHED THEN
                        INSERT (PostalAddressID, CurrAccCode, CurrAccTypeCode, DefaultTypeCode)
                        VALUES (source.PostalAddressID, source.CurrAccCode, source.CurrAccTypeCode, source.DefaultTypeCode);";

                await connection.ExecuteAsync(sql, new
                {
                    CustomerCode = customerCode,
                    AddressTypeCode = addressTypeCode
                }, transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting default address for customer {CustomerCode} with address type {AddressTypeCode}", customerCode, addressTypeCode);
                throw;
            }
        }

        private async Task SetDefaultCommunication(SqlConnection connection, SqlTransaction transaction, string customerCode, string communicationTypeCode)
        {
            try
            {
                var sql = @"
                    MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                    USING (
                        SELECT 
                            c.CommunicationID,
                            @CustomerCode AS CurrAccCode,
                            3 AS CurrAccTypeCode,
                            'C' AS DefaultTypeCode
                        FROM prCurrAccCommunication c
                        WHERE c.CurrAccCode = @CustomerCode 
                        AND c.CurrAccTypeCode = 3
                        AND c.CommunicationTypeCode = @CommunicationTypeCode
                    ) AS source
                    ON (
                        target.CurrAccCode = source.CurrAccCode AND 
                        target.CurrAccTypeCode = source.CurrAccTypeCode AND 
                        target.DefaultTypeCode = source.DefaultTypeCode
                    )
                    WHEN MATCHED THEN
                        UPDATE SET CommunicationID = source.CommunicationID
                    WHEN NOT MATCHED THEN
                        INSERT (CommunicationID, CurrAccCode, CurrAccTypeCode, DefaultTypeCode)
                        VALUES (source.CommunicationID, source.CurrAccCode, source.CurrAccTypeCode, source.DefaultTypeCode);";

                await connection.ExecuteAsync(sql, new
                {
                    CustomerCode = customerCode,
                    CommunicationTypeCode = communicationTypeCode
                }, transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting default communication for customer {CustomerCode} with communication type {CommunicationTypeCode}", customerCode, communicationTypeCode);
                throw;
            }
        }

        private async Task SetDefaultContact(SqlConnection connection, SqlTransaction transaction, string customerCode, string contactTypeCode)
        {
            try
            {
                var sql = @"
                    MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                    USING (
                        SELECT 
                            c.ContactID,
                            @CustomerCode AS CurrAccCode,
                            3 AS CurrAccTypeCode,
                            'P' AS DefaultTypeCode
                        FROM prCurrAccContact c
                        WHERE c.CurrAccCode = @CustomerCode 
                        AND c.CurrAccTypeCode = 3
                        AND c.ContactTypeCode = @ContactTypeCode
                    ) AS source
                    ON (
                        target.CurrAccCode = source.CurrAccCode AND 
                        target.CurrAccTypeCode = source.CurrAccTypeCode AND 
                        target.DefaultTypeCode = source.DefaultTypeCode
                    )
                    WHEN MATCHED THEN
                        UPDATE SET ContactID = source.ContactID
                    WHEN NOT MATCHED THEN
                        INSERT (ContactID, CurrAccCode, CurrAccTypeCode, DefaultTypeCode)
                        VALUES (source.ContactID, source.CurrAccCode, source.CurrAccTypeCode, source.DefaultTypeCode);";

                await connection.ExecuteAsync(sql, new
                {
                    CustomerCode = customerCode,
                    ContactTypeCode = contactTypeCode
                }, transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting default contact for customer {CustomerCode} with contact type {ContactTypeCode}", customerCode, contactTypeCode);
                throw;
            }
        }
        #endregion

        #region Address Operations
        public async Task<List<AddressTypeResponse>> GetAddressTypesAsync()
        {
            _logger.LogInformation("[CustomerBasicService.GetAddressTypesAsync] - Adres tipleri getirme isteği alındı");
            return await _addressService.GetAddressTypesAsync();
        }

        public async Task<AddressTypeResponse> GetAddressTypeByCodeAsync(string code)
        {
            _logger.LogInformation("[CustomerBasicService.GetAddressTypeByCodeAsync] - Adres tipi getirme isteği alındı: {Code}", code);
            return await _addressService.GetAddressTypeByCodeAsync(code);
        }

        public async Task<AddressTypeResponse> CreateAddressTypeAsync(AddressTypeCreateRequest request)
        {
            return await _addressService.CreateAddressTypeAsync(request);
        }

        public async Task<AddressTypeResponse> UpdateAddressTypeAsync(string code, AddressTypeUpdateRequest request)
        {
            return await _addressService.UpdateAddressTypeAsync(code, request);
        }

        public async Task<bool> DeleteAddressTypeAsync(string code)
        {
            _logger.LogInformation("[CustomerBasicService.DeleteAddressTypeAsync] - Adres tipi silme isteği alındı: {Code}", code);
            return await _addressService.DeleteAddressTypeAsync(code);
        }

        public async Task<List<AddressResponse>> GetAddressesAsync(string customerCode)
        {
            return await _addressService.GetAddressesAsync(customerCode);
        }

        public async Task<AddressResponse> GetAddressByIdAsync(string customerCode, string addressId)
        {
            return await _addressService.GetAddressByIdAsync(customerCode, addressId);
        }

        public async Task<AddressResponse> CreateAddressAsync(string customerCode, ErpMobile.Api.Models.Customer.AddressCreateRequest request)
        {
            // Tip dönüşümü yaparak uygun formatta parametre gönderiyoruz
            var addressRequest = new ErpMobile.Api.Models.Requests.AddressCreateRequest
            {
                AddressTypeCode = request.AddressTypeCode,
                Address = request.Address,
                CityCode = request.CityCode,
                DistrictCode = request.DistrictCode,
                CountryCode = request.CountryCode,
            
                IsDefault = request.IsDefault
            };
            return await _addressService.CreateAddressAsync(customerCode, addressRequest);
        }

        public async Task<AddressResponse> UpdateAddressAsync(string customerCode, string addressTypeCode, AddressUpdateRequest request)
        {
            return await _addressService.UpdateAddressAsync(customerCode, addressTypeCode, request);
        }

        public async Task<bool> DeleteAddressAsync(string customerCode, string addressTypeCode)
        {
            return await _addressService.DeleteAddressAsync(customerCode, addressTypeCode);
        }

        public async Task<List<CustomerAddressResponse>> GetCustomerAddressesAsync(string customerCode)
        {
            _logger.LogInformation("[CustomerBasicService.GetCustomerAddressesAsync] - Müşteri adres listesi getirme isteği alındı: {CustomerCode}", customerCode);
            return await _addressService.GetCustomerAddressesAsync(customerCode);
        }
        #endregion

        #region Contact Operations
        public async Task<List<ErpMobile.Api.Models.Responses.ContactResponse>> GetContactsAsync(string customerCode)
        {
            return await _contactService.GetContactsAsync(customerCode);
        }

        public async Task<ErpMobile.Api.Models.Responses.ContactResponse> GetContactByIdAsync(string customerCode, string contactId)
        {
            return await _contactService.GetContactByIdAsync(customerCode, contactId);
        }

        public async Task<ErpMobile.Api.Models.Responses.ContactResponse> CreateContactAsync(string customerCode, ErpMobile.Api.Models.Contact.ContactCreateRequest request)
        {
            // Tip dönüşümü yaparak uygun formatta parametre gönderiyoruz
            var contactRequest = new ErpMobile.Api.Models.Requests.ContactCreateRequest
            {
                ContactTypeCode = request.ContactTypeCode,
                Contact = $"{request.FirstName} {request.LastName}",
                IsDefault = false // IsDefault property'si Contact sınıfında bulunmuyor, varsayılan değer kullanıyoruz
            };
            return await _contactService.CreateContactAsync(customerCode, contactRequest);
        }

        public async Task<ErpMobile.Api.Models.Responses.ContactResponse> UpdateContactAsync(string customerCode, string contactTypeCode, ContactUpdateRequest request)
        {
            return await _contactService.UpdateContactAsync(customerCode, contactTypeCode, request);
        }

        public async Task<bool> DeleteContactAsync(string customerCode, string contactTypeCode)
        {
            _logger.LogInformation("[CustomerBasicService.DeleteContactAsync] - Kişi silme isteği alındı: {CustomerCode}, {ContactTypeCode}", customerCode, contactTypeCode);
            return await _contactService.DeleteContactAsync(customerCode, contactTypeCode);
        }

        public async Task<List<CustomerContactResponse>> GetCustomerContactsAsync(string customerCode)
        {
            _logger.LogInformation("[CustomerBasicService.GetCustomerContactsAsync] - Müşteri kişi listesi getirme isteği alındı: {CustomerCode}", customerCode);
            return await _contactService.GetCustomerContactsAsync(customerCode);
        }

        public async Task<List<ContactTypeResponse>> GetContactTypesAsync()
        {
            _logger.LogInformation("[CustomerBasicService.GetContactTypesAsync] - Kişi tipleri getirme isteği alındı");
            return await _contactService.GetContactTypesAsync();
        }

        public async Task<ContactTypeResponse> GetContactTypeByCodeAsync(string code)
        {
            _logger.LogInformation("[CustomerBasicService.GetContactTypeByCodeAsync] - Kişi tipi getirme isteği alındı: {Code}", code);
            return await _contactService.GetContactTypeByCodeAsync(code);
        }
        #endregion

        #region Communication Operations
        public async Task<List<CustomerCommunicationResponse>> GetCustomerCommunicationsAsync(string customerCode)
        {
            _logger.LogInformation("[CustomerBasicService.GetCustomerCommunicationsAsync] - Müşteri iletişim bilgileri getirme isteği alındı: {CustomerCode}", customerCode);
            return await _communicationService.GetCustomerCommunicationsAsync(customerCode);
        }
        #endregion

        #region Location Operations
        public async Task<List<RegionResponse>> GetRegionsAsync()
        {
            _logger.LogInformation("[CustomerBasicService.GetRegionsAsync] - Bölge listesi getirme isteği alındı");
            return await _locationService.GetRegionsAsync();
        }

        public async Task<List<StateResponse>> GetStatesAsync(string countryCode = null)
        {
            _logger.LogInformation("[CustomerBasicService.GetStatesAsync] - İl listesi getirme isteği alındı");
            return await _locationService.GetStatesAsync();
        }

        public async Task<List<CityResponse>> GetCitiesAsync()
        {
            _logger.LogInformation("[CustomerBasicService.GetCitiesAsync] - Şehir listesi getirme isteği alındı");
            return await _locationService.GetCitiesAsync();
        }

        public async Task<List<CityResponse>> GetCitiesByStateAsync(string stateCode)
        {
            _logger.LogInformation("[CustomerBasicService.GetCitiesByStateAsync] - İle göre şehir listesi getirme isteği alındı: {StateCode}", stateCode);
            return await _locationService.GetCitiesByStateAsync(stateCode);
        }

        public async Task<List<CityResponse>> GetCitiesByRegionAsync(string regionCode)
        {
            _logger.LogInformation("[CustomerBasicService.GetCitiesByRegionAsync] - Bölgeye göre şehir listesi getirme isteği alındı: {RegionCode}", regionCode);
            return await _locationService.GetCitiesByRegionAsync(regionCode);
        }

        public async Task<List<DistrictResponse>> GetDistrictsByCityAsync(string cityCode)
        {
            _logger.LogInformation("[CustomerBasicService.GetDistrictsByCityAsync] - Şehire göre ilçe listesi getirme isteği alındı: {CityCode}", cityCode);
            return await _locationService.GetDistrictsByCityAsync(cityCode);
        }

        public async Task<List<DistrictResponse>> GetAllDistrictsAsync()
        {
            _logger.LogInformation("[CustomerBasicService.GetAllDistrictsAsync] - Tüm ilçe listesi getirme isteği alındı");
            return await _locationService.GetAllDistrictsAsync();
        }

        public async Task<List<CustomerPaymentPlanGroupResponse>> GetCustomerPaymentPlanGroupsAsync()
        {
            try
            {
                _logger.LogInformation("[CustomerBasicService.GetCustomerPaymentPlanGroupsAsync] - Müşteri ödeme planı grupları getirme isteği alındı");
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            pg.PaymentPlanGroupCode,
                            pgd.PaymentPlanGroupDescription,
                            pg.IsBlocked
                        FROM cdPaymentPlanGroup pg WITH(NOLOCK)
                        LEFT JOIN cdPaymentPlanGroupDesc pgd WITH(NOLOCK) ON pgd.PaymentPlanGroupCode = pg.PaymentPlanGroupCode AND pgd.LangCode = 'TR'
                        ORDER BY pg.PaymentPlanGroupCode";

                    var result = await connection.QueryAsync<CustomerPaymentPlanGroupResponse>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CustomerBasicService.GetCustomerPaymentPlanGroupsAsync] - Müşteri ödeme planı grupları getirilirken hata oluştu. Boş liste döndürülecek.");
                return new List<CustomerPaymentPlanGroupResponse>(); // Return empty on error
            }
        }

        public async Task<List<ErpMobile.Api.Models.TaxOfficeResponse>> GetTaxOfficesAsync()
        {
            _logger.LogInformation("[CustomerBasicService.GetTaxOfficesAsync] - Vergi daireleri getirme isteği alındı");
            var taxOffices = await _locationService.GetTaxOfficesAsync();
            
            // Responses namespace'indeki TaxOfficeResponse tipini Models namespace'indeki TaxOfficeResponse tipine dönüştür
            var result = new List<ErpMobile.Api.Models.TaxOfficeResponse>();
            foreach (var office in taxOffices)
            {
                result.Add(new ErpMobile.Api.Models.TaxOfficeResponse
                {
                    TaxOfficeCode = office.TaxOfficeCode,
                    TaxOfficeDescription = office.TaxOfficeDescription,
                    CityCode = office.CityCode,
                    CityDescription = null, // Models.Responses.TaxOfficeResponse'da CityDescription yok
                    IsBlocked = office.IsBlocked
                });
            }
            
            return result;
        }

        // GetBankAccountsAsync metodu artık CustomerLocationService sınıfına taşındı
        #endregion
    }
}
