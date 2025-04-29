using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Data;
using ErpMobile.Api.Interfaces;
using erp_api.Models.Requests;
using erp_api.Models.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Yeni müşteri servisi
    /// </summary>
    public class CustomerServiceNew : ICustomerServiceNew
    {
        private readonly ILogger<CustomerServiceNew> _logger;
        private readonly IConfiguration _configuration;
        private readonly ErpDbContext _context;

        public CustomerServiceNew(ILogger<CustomerServiceNew> logger, IConfiguration configuration, ErpDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        /// <summary>
        /// Yeni müşteri oluşturur
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        public async Task<CustomerCreateResponseNew> CreateCustomerAsync(CustomerCreateRequestNew request)
        {
            _logger.LogInformation("Müşteri oluşturma işlemi başlatıldı: {CustomerCode}", request.CustomerCode);
            
            var response = new CustomerCreateResponseNew
            {
                CustomerCode = request.CustomerCode,
                Success = false,
                Message = string.Empty,
                ErrorDetails = string.Empty
            };

            using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
            {
                SqlTransaction transaction = null;
                
                try
                {
                    await connection.OpenAsync();
                    
                    // Müşteri kodu varlık kontrolü
                    var existingCustomer = await GetCustomerByCodeAsync(connection, request.CustomerCode);
                    if (existingCustomer != null)
                    {
                        response.Success = false;
                        response.Message = $"Müşteri kodu '{request.CustomerCode}' sistemde zaten kayıtlı.";
                        _logger.LogWarning("Müşteri oluşturma başarısız: {CustomerCode} kodu zaten mevcut.", request.CustomerCode);
                        return response;
                    }
                    
                    // Transaction başlat
                    transaction = connection.BeginTransaction();
                    _logger.LogInformation("Müşteri oluşturma transaction başlatıldı: {CustomerCode}", request.CustomerCode);
                    
                    try
                    {
                        // Ana müşteri kaydını oluştur
                        await InsertCustomerAsync(connection, transaction, request);
                        _logger.LogInformation("Ana müşteri kaydı oluşturuldu: {CustomerCode}", request.CustomerCode);
                        
                        // Müşteri açıklama kaydını oluştur
                        await InsertCustomerDescriptionAsync(connection, transaction, request);
                        _logger.LogInformation("Müşteri açıklama kaydı oluşturuldu: {CustomerCode}", request.CustomerCode);
                        
                        // Müşteri adres kayıtlarını oluştur
                        if (request.Addresses != null && request.Addresses.Any())
                        {
                            await InsertCustomerAddressesAsync(connection, transaction, request);
                            _logger.LogInformation("Müşteri adres kayıtları oluşturuldu: {CustomerCode}, Adres Sayısı: {AddressCount}", 
                                request.CustomerCode, request.Addresses.Count);
                        }
                        
                        // Müşteri iletişim bilgisi kayıtlarını oluştur
                        if (request.Communications != null && request.Communications.Any())
                        {
                            await InsertCustomerCommunicationsAsync(connection, transaction, request);
                            _logger.LogInformation("Müşteri iletişim bilgisi kayıtları oluşturuldu: {CustomerCode}, İletişim Sayısı: {CommunicationCount}", 
                                request.CustomerCode, request.Communications.Count);
                        }
                        
                        // Müşteri iletişim kişisi kayıtlarını oluştur
                        if (request.Contacts != null && request.Contacts.Any())
                        {
                            await InsertCustomerContactsAsync(connection, transaction, request);
                            _logger.LogInformation("Müşteri iletişim kişisi kayıtları oluşturuldu: {CustomerCode}, Kişi Sayısı: {ContactCount}", 
                                request.CustomerCode, request.Contacts.Count);
                        }
                        
                        // Transaction'ı commit et
                        transaction.Commit();
                        _logger.LogInformation("Müşteri oluşturma transaction commit edildi: {CustomerCode}", request.CustomerCode);
                        
                        response.Success = true;
                        response.Message = $"Müşteri '{request.CustomerCode}' başarıyla oluşturuldu.";
                        
                        // Oluşturulan müşteri bilgilerini yanıta ekle
                        response.CustomerName = request.CustomerName;
                        response.CustomerTypeCode = 3; // Sabit müşteri tipi kodu
                        response.AddressCount = request.Addresses?.Count ?? 0;
                        response.CommunicationCount = request.Communications?.Count ?? 0;
                        response.ContactCount = request.Contacts?.Count ?? 0;
                        
                        return response;
                    }
                    catch (Exception ex)
                    {
                        // Hata durumunda transaction'ı rollback et
                        transaction?.Rollback();
                        _logger.LogError(ex, "Müşteri oluşturma transaction rollback edildi: {CustomerCode}, Hata: {ErrorMessage}", 
                            request.CustomerCode, ex.Message);
                        
                        response.Success = false;
                        response.Message = "Müşteri oluşturma işlemi sırasında bir hata oluştu.";
                        response.ErrorDetails = $"{ex.Message} {(ex.InnerException != null ? $" - {ex.InnerException.Message}" : "")}";
                        
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Müşteri oluşturma işlemi başlatılırken bir hata oluştu: {CustomerCode}, Hata: {ErrorMessage}", 
                        request.CustomerCode, ex.Message);
                    
                    response.Success = false;
                    response.Message = "Müşteri oluşturma işlemi başlatılırken bir hata oluştu.";
                    response.ErrorDetails = $"{ex.Message} {(ex.InnerException != null ? $" - {ex.InnerException.Message}" : "")}";
                    
                    return response;
                }
                finally
                {
                    // Connection'ı kapat
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Müşteri kodunun varlığını kontrol eder
        /// </summary>
        private async Task<bool> CheckCustomerExistsAsync(SqlConnection connection, SqlTransaction transaction, string customerCode)
        {
            var sql = @"
                -- Ana müşteri tablosunda kontrol
                IF EXISTS (SELECT 1 FROM cdCurrAcc WITH(NOLOCK) WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode)
                    SELECT 1 AS Exists
                -- Müşteri açıklama tablosunda kontrol
                ELSE IF EXISTS (SELECT 1 FROM cdCurrAccDesc WITH(NOLOCK) WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode)
                    SELECT 1 AS Exists
                -- Müşteri adres tablosunda kontrol
                ELSE IF EXISTS (SELECT 1 FROM prCurrAccPostalAddress WITH(NOLOCK) WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode)
                    SELECT 1 AS Exists
                -- Müşteri varsayılan değerler tablosunda kontrol
                ELSE IF EXISTS (SELECT 1 FROM prCurrAccDefault WITH(NOLOCK) WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode)
                    SELECT 1 AS Exists
                -- Müşteri iletişim kişileri tablosunda kontrol
                ELSE IF EXISTS (SELECT 1 FROM prCurrAccContact WITH(NOLOCK) WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode)
                    SELECT 1 AS Exists
                -- Müşteri iletişim bilgileri tablosunda kontrol
                ELSE IF EXISTS (SELECT 1 FROM prCurrAccCommunication WITH(NOLOCK) WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode)
                    SELECT 1 AS Exists
                ELSE
                    SELECT 0 AS Exists";

            var exists = await connection.ExecuteScalarAsync<int>(sql, new { CustomerCode = customerCode }, transaction);
            return exists == 1;
        }

        /// <summary>
        /// Ana müşteri tablosuna kayıt ekler
        /// </summary>
        private async Task InsertCustomerAsync(SqlConnection connection, SqlTransaction transaction, CustomerCreateRequestNew request)
        {
            var sql = @"
                -- MERGE sorgusu ile ana müşteri tablosuna kayıt ekle
                MERGE cdCurrAcc WITH (HOLDLOCK) AS target
                USING (
                    SELECT 
                        @CustomerCode AS CurrAccCode,
                        3 AS CurrAccTypeCode
                ) AS source
                ON (
                    target.CurrAccCode = source.CurrAccCode AND 
                    target.CurrAccTypeCode = source.CurrAccTypeCode
                )
                WHEN NOT MATCHED THEN
                    INSERT (
                        CurrAccCode, CurrAccTypeCode, FirstName, LastName, TitleCode, Patronym, IdentityNum,
                        TaxNumber, MersisNum, IsIndividualAcc, DataLanguageCode, CreditLimit, CurrencyCode, CompanyCode,
                        OfficeCode, IsVIP, IsSendAdvertSMS, IsSendAdvertMail, ExchangeTypeCode, DueDateFormulaCode,
                        BankCode, BankBranchCode, BankAccTypeCode, IBAN, SWIFTCode, BankAccNo, MinBalance, IsBlocked,
                        CustomerDiscountGrCode, VendorTypeCode, RetailSalePriceGroupCode, WholesalePriceGroupCode,
                        AccountOpeningDate, AccountClosingDate, PromotionGroupCode, SalesChannelCode, UseManufacturing,
                        IsLocked, LockedDate, BarcodeTypeCode, CostCenterCode, UseBankAccOnStore, CustomerPaymentPlanGrCode,
                        GLTypeCode, IsSubjectToEInvoice, IsArrangeCommercialInvoice, CustomerMarkupGrCode, CurrAccLotGrCode,
                        AllowOnlySelectedCurrency, PermitCreditBalance, IsSubjectToEShipment,
                        CustomerASNNumberIsRequiredForShipments, PurchaseRequisitionRequired, UseDBSIntegration,
                        DBSAccountCode, UseSerialNumberTracking, EInvoiceStartDate, EShipmentStartDate,
                        VendorPaymentPlanGrCode, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate, RowGuid
                    )
                    VALUES (
                        @CustomerCode, 3, 
                        ISNULL(@FirstName, N''), 
                        ISNULL(@LastName, N''), 
                        ISNULL(@TitleCode, N''), 
                        ISNULL(@Patronym, N''), 
                        ISNULL(@CustomerIdentityNumber, N''), 
                        ISNULL(@TaxNumber, N''),
                        ISNULL(@MersisNum, N''), 
                        ISNULL(@IsIndividualAcc, 0), 
                        ISNULL(@DataLanguageCode, N'TR'), 
                        ISNULL(@CreditLimit, 0),
                        ISNULL(@CurrencyCode, N'TRY'), 
                        @CompanyCode, 
                        ISNULL(@OfficeCode, N'M'), 
                        ISNULL(@IsVIP, 0),
                        ISNULL(@IsSendAdvertSMS, 0), 
                        ISNULL(@IsSendAdvertMail, 0), 
                        ISNULL(@ExchangeTypeCode, 0),
                        ISNULL(@DueDateFormulaCode, N''), 
                        ISNULL(@BankCode, N''), 
                        ISNULL(@BankBranchCode, N''), 
                        ISNULL(@BankAccTypeCode, 0),
                        ISNULL(@IBAN, N''), 
                        ISNULL(@SWIFTCode, N''), 
                        ISNULL(@BankAccNo, N''), 
                        ISNULL(@MinBalance, 0),
                        ISNULL(@IsBlocked, 0), 
                        ISNULL(@CustomerDiscountGrCode, N''), 
                        ISNULL(@VendorTypeCode, 0),
                        ISNULL(@RetailSalePriceGroupCode, N''), 
                        ISNULL(@WholesalePriceGroupCode, N''), 
                        ISNULL(@AccountOpeningDate, CONVERT(DATE, GETDATE())),
                        ISNULL(@AccountClosingDate, CONVERT(DATE, '19000101')), 
                        ISNULL(@PromotionGroupCode, N''), 
                        ISNULL(@SalesChannelCode, N''),
                        ISNULL(@UseManufacturing, 0), 
                        ISNULL(@IsLocked, 0), 
                        ISNULL(@LockedDate, CONVERT(DATE, '19000101')),
                        ISNULL(@BarcodeTypeCode, N'Def'), 
                        ISNULL(@CostCenterCode, N''), 
                        ISNULL(@UseBankAccOnStore, 0),
                        ISNULL(@CustomerPaymentPlanGrCode, N''), 
                        ISNULL(@GLTypeCode, N''), 
                        ISNULL(@IsSubjectToEInvoice, 0),
                        ISNULL(@IsArrangeCommercialInvoice, 0), 
                        ISNULL(@CustomerMarkupGrCode, N''), 
                        ISNULL(@CurrAccLotGrCode, N''),
                        ISNULL(@AllowOnlySelectedCurrency, 0), 
                        ISNULL(@PermitCreditBalance, 0), 
                        ISNULL(@IsSubjectToEShipment, 0),
                        ISNULL(@CustomerASNNumberIsRequiredForShipments, 0), 
                        ISNULL(@PurchaseRequisitionRequired, 0),
                        ISNULL(@UseDBSIntegration, 0), 
                        ISNULL(@DBSAccountCode, N''), 
                        ISNULL(@UseSerialNumberTracking, 0),
                        ISNULL(@EInvoiceStartDate, CONVERT(DATE, '19000101')), 
                        ISNULL(@EShipmentStartDate, CONVERT(DATE, '19000101')),
                        ISNULL(@VendorPaymentPlanGrCode, N''), 
                        @CreatedUserName, 
                        GETDATE(), 
                        @LastUpdatedUserName, 
                        GETDATE(),
                        NEWID()
                    );";

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerCode", request.CustomerCode);
            parameters.Add("@FirstName", request.CustomerName); // Bireysel müşteri değilse CustomerName kullanılır
            parameters.Add("@LastName", request.CustomerSurname ?? string.Empty);
            parameters.Add("@TitleCode", request.TitleCode ?? string.Empty);
            parameters.Add("@Patronym", request.Patronym ?? string.Empty);
            parameters.Add("@CustomerIdentityNumber", request.CustomerIdentityNumber ?? string.Empty);
            parameters.Add("@TaxNumber", request.TaxNumber ?? string.Empty);
            parameters.Add("@MersisNum", request.MersisNum ?? string.Empty);
            parameters.Add("@IsIndividualAcc", request.IsIndividualAcc);
            parameters.Add("@DataLanguageCode", request.DataLanguageCode ?? "TR");
            parameters.Add("@CreditLimit", request.CreditLimit);
            parameters.Add("@CurrencyCode", request.CurrencyCode ?? "TRY");
            parameters.Add("@CompanyCode", request.CompanyCode, System.Data.DbType.Int64);
            parameters.Add("@OfficeCode", request.OfficeCode ?? "M");
            parameters.Add("@IsVIP", request.IsVIP);
            parameters.Add("@IsSendAdvertSMS", request.IsSendAdvertSMS);
            parameters.Add("@IsSendAdvertMail", request.IsSendAdvertMail);
            parameters.Add("@ExchangeTypeCode", request.ExchangeTypeCode);
            parameters.Add("@DueDateFormulaCode", request.DueDateFormulaCode ?? string.Empty);
            parameters.Add("@BankCode", request.BankCode ?? string.Empty);
            parameters.Add("@BankBranchCode", request.BankBranchCode ?? string.Empty);
            parameters.Add("@BankAccTypeCode", request.BankAccTypeCode);
            parameters.Add("@IBAN", request.IBAN ?? string.Empty);
            parameters.Add("@SWIFTCode", request.SWIFTCode ?? string.Empty);
            parameters.Add("@BankAccNo", request.BankAccNo ?? string.Empty);
            parameters.Add("@MinBalance", request.MinBalance);
            parameters.Add("@IsBlocked", request.IsBlocked);
            parameters.Add("@CustomerDiscountGrCode", request.DiscountGroupCode ?? string.Empty);
            parameters.Add("@VendorTypeCode", request.VendorTypeCode);
            parameters.Add("@RetailSalePriceGroupCode", request.RetailSalePriceGroupCode ?? string.Empty);
            parameters.Add("@WholesalePriceGroupCode", request.WholesalePriceGroupCode ?? string.Empty);
            parameters.Add("@AccountOpeningDate", request.AccountOpeningDate ?? DateTime.Now);
            parameters.Add("@AccountClosingDate", request.AccountClosingDate ?? new DateTime(1900, 1, 1));
            parameters.Add("@PromotionGroupCode", request.PromotionGroupCode ?? string.Empty);
            parameters.Add("@SalesChannelCode", request.SalesChannelCode ?? string.Empty);
            parameters.Add("@UseManufacturing", request.UseManufacturing);
            parameters.Add("@IsLocked", request.IsLocked);
            parameters.Add("@LockedDate", request.LockedDate ?? new DateTime(1900, 1, 1));
            parameters.Add("@BarcodeTypeCode", request.BarcodeTypeCode ?? "Def");
            parameters.Add("@CostCenterCode", request.CostCenterCode ?? string.Empty);
            parameters.Add("@UseBankAccOnStore", request.UseBankAccOnStore);
            parameters.Add("@CustomerPaymentPlanGrCode", request.CustomerPaymentPlanGrCode ?? string.Empty);
            parameters.Add("@GLTypeCode", request.GLTypeCode ?? string.Empty);
            parameters.Add("@IsSubjectToEInvoice", request.IsSubjectToEInvoice);
            parameters.Add("@IsArrangeCommercialInvoice", request.IsArrangeCommercialInvoice);
            parameters.Add("@CustomerMarkupGrCode", request.CustomerMarkupGrCode ?? string.Empty);
            parameters.Add("@CurrAccLotGrCode", request.CurrAccLotGrCode ?? string.Empty);
            parameters.Add("@AllowOnlySelectedCurrency", request.AllowOnlySelectedCurrency);
            parameters.Add("@PermitCreditBalance", request.PermitCreditBalance);
            parameters.Add("@IsSubjectToEShipment", request.IsSubjectToEShipment);
            parameters.Add("@CustomerASNNumberIsRequiredForShipments", request.CustomerASNNumberIsRequiredForShipments);
            parameters.Add("@PurchaseRequisitionRequired", request.PurchaseRequisitionRequired);
            parameters.Add("@UseDBSIntegration", request.UseDBSIntegration);
            parameters.Add("@DBSAccountCode", request.DBSAccountCode ?? string.Empty);
            parameters.Add("@UseSerialNumberTracking", request.UseSerialNumberTracking);
            parameters.Add("@EInvoiceStartDate", request.EInvoiceStartDate ?? new DateTime(1900, 1, 1));
            parameters.Add("@EShipmentStartDate", request.EShipmentStartDate ?? new DateTime(1900, 1, 1));
            parameters.Add("@VendorPaymentPlanGrCode", request.VendorPaymentPlanGrCode ?? string.Empty);
            parameters.Add("@CreatedUserName", request.CreatedUserName ?? "SYSTEM");
            parameters.Add("@LastUpdatedUserName", request.LastUpdatedUserName ?? "SYSTEM");

            try
            {
                await connection.ExecuteAsync(sql, parameters, transaction);
                _logger.LogInformation("Müşteri kaydı başarıyla oluşturuldu: {CustomerCode}", request.CustomerCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri kaydı oluşturulurken hata: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Müşteri açıklama tablosuna kayıt ekler
        /// </summary>
        private async Task InsertCustomerDescriptionAsync(SqlConnection connection, SqlTransaction transaction, CustomerCreateRequestNew request)
        {
            var sql = @"
                -- MERGE sorgusu ile müşteri açıklama tablosuna kayıt ekle
                MERGE cdCurrAccDesc WITH (HOLDLOCK) AS target
                USING (
                    SELECT 
                        @CustomerCode AS CurrAccCode,
                        3 AS CurrAccTypeCode,
                        @LangCode AS LangCode
                ) AS source
                ON (
                    target.CurrAccCode = source.CurrAccCode AND 
                    target.CurrAccTypeCode = source.CurrAccTypeCode AND
                    target.LangCode = source.LangCode
                )
                WHEN MATCHED THEN
                    UPDATE SET
                        CurrAccDescription = RTRIM(ISNULL(@FirstName, N'') + N' ' + ISNULL(@LastName, N'')),
                        LastUpdatedUserName = @LastUpdatedUserName,
                        LastUpdatedDate = GETDATE()
                WHEN NOT MATCHED THEN
                    INSERT (
                        CurrAccCode, 
                        CurrAccTypeCode, 
                        CurrAccDescription, 
                        LangCode, 
                        CreatedUserName, 
                        CreatedDate, 
                        LastUpdatedUserName, 
                        LastUpdatedDate
                    )
                    VALUES (
                        @CustomerCode, 
                        3, 
                        RTRIM(ISNULL(@FirstName, N'') + N' ' + ISNULL(@LastName, N'')), 
                        @LangCode, 
                        @CreatedUserName, 
                        GETDATE(), 
                        @LastUpdatedUserName, 
                        GETDATE()
                    );";

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerCode", request.CustomerCode);
            parameters.Add("@FirstName", request.CustomerName); 
            parameters.Add("@LastName", request.CustomerSurname ?? string.Empty);
            parameters.Add("@LangCode", request.DataLanguageCode ?? "TR");
            parameters.Add("@LastUpdatedUserName", request.LastUpdatedUserName);
            parameters.Add("@CreatedUserName", request.CreatedUserName);

            var result = await connection.ExecuteAsync(sql, parameters, transaction);
            
            if (result == 0)
            {
                throw new Exception($"Müşteri açıklama kaydı oluşturulamadı: {request.CustomerCode}");
            }
            
            _logger.LogInformation("Müşteri açıklama tablosuna kayıt eklendi: {CustomerCode}", request.CustomerCode);
        }

        /// <summary>
        /// Müşteri adreslerini ekler ve varsayılan adresi belirler
        /// </summary>
        /// <returns>Varsayılan adres ID</returns>
        private async Task<Guid?> InsertCustomerAddressesAsync(SqlConnection connection, SqlTransaction transaction, CustomerCreateRequestNew request)
        {
            if (request.Addresses == null || !request.Addresses.Any())
            {
                _logger.LogInformation("Müşteri için adres bilgisi bulunamadı: {CustomerCode}", request.CustomerCode);
                return null;
            }

            Guid? defaultPostalAddressId = null;

            foreach (var address in request.Addresses)
            {
                // Generate a new PostalAddressID if not provided
                var postalAddressId = address.PostalAddressID ?? Guid.NewGuid();
                
                if (address.IsDefault)
                {
                    defaultPostalAddressId = postalAddressId;
                }

                // Insert address
                var addressSql = @"
                    INSERT INTO prCurrAccPostalAddress (
                        CurrAccCode, CurrAccTypeCode, ContactID, SubCurrAccID, PostalAddressID,
                        AddressTypeCode, DrivingDirections, Address, ZipCode, CountryCode,
                        StateCode, CityCode, DistrictCode, TaxOfficeCode, TaxNumber,
                        IsBlocked, SiteName, BuildingName, BuildingNum, FloorNum,
                        DoorNum, QuarterName, Street, QuarterCode, StreetCode,
                        AddressID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    )
                    VALUES (
                        @CurrAccCode, @CurrAccTypeCode, NULL, NULL, @PostalAddressID,
                        @AddressTypeCode, @DrivingDirections, @Address, @ZipCode, @CountryCode,
                        @StateCode, @CityCode, @DistrictCode, @TaxOfficeCode, @TaxNumber,
                        @IsBlocked, @SiteName, @BuildingName, @BuildingNum, @FloorNum,
                        @DoorNum, @QuarterName, @Street, @QuarterCode, @StreetCode,
                        @AddressID, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                    )";

                var addressParams = new
                {
                    CurrAccCode = request.CustomerCode,
                    CurrAccTypeCode = 3, // Customer type code
                    PostalAddressID = postalAddressId,
                    AddressTypeCode = address.AddressTypeCode,
                    DrivingDirections = address.DrivingDirections ?? string.Empty,
                    Address = address.Address,
                    ZipCode = address.ZipCode ?? string.Empty,
                    CountryCode = address.CountryCode,
                    StateCode = address.StateCode,
                    CityCode = address.CityCode,
                    DistrictCode = address.DistrictCode,
                    TaxOfficeCode = string.Empty, // Trace'de boş string olarak gönderiliyor
                    TaxNumber = address.TaxNumber ?? string.Empty,
                    IsBlocked = address.IsBlocked,
                    SiteName = address.SiteName ?? string.Empty,
                    BuildingName = address.BuildingName ?? string.Empty,
                    BuildingNum = address.BuildingNum ?? string.Empty,
                    FloorNum = address.FloorNum,
                    DoorNum = address.DoorNum,
                    QuarterName = address.QuarterName ?? string.Empty,
                    Street = address.Street ?? string.Empty,
                    QuarterCode = address.QuarterCode.HasValue ? (long)address.QuarterCode.Value : (long?)null,
                    StreetCode = address.StreetCode.HasValue ? (long)address.StreetCode.Value : (long?)null,
                    AddressID = 0, // AddressID bigint tipinde, bu yüzden 0 değerini kullanıyoruz
                    CreatedUserName = request.CreatedUserName,
                    LastUpdatedUserName = request.LastUpdatedUserName
                };

                await connection.ExecuteAsync(addressSql, addressParams, transaction);

                // Set default address in prCurrAccDefault if this is the default address
                if (address.IsDefault)
                {
                    // First check if a record exists for this customer in prCurrAccDefault
                    var defaultCheckSql = @"
                        SELECT COUNT(1) FROM prCurrAccDefault WITH(NOLOCK)
                        WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode";

                    var defaultExists = await connection.ExecuteScalarAsync<int>(defaultCheckSql, 
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = 3 }, transaction) > 0;

                    // Use MERGE with HOLDLOCK to handle both INSERT and UPDATE scenarios safely
                    var defaultSql = @"
                        MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                        USING (
                            SELECT 
                                @CurrAccCode AS CurrAccCode,
                                @CurrAccTypeCode AS CurrAccTypeCode,
                                @PostalAddressID AS PostalAddressID
                        ) AS source
                        ON (
                            target.CurrAccCode = source.CurrAccCode AND 
                            target.CurrAccTypeCode = source.CurrAccTypeCode
                        )
                        WHEN MATCHED THEN
                            UPDATE SET 
                                target.PostalAddressID = source.PostalAddressID,
                                target.LastUpdatedUserName = @LastUpdatedUserName,
                                target.LastUpdatedDate = GETDATE()
                        WHEN NOT MATCHED THEN
                            INSERT (
                                CurrAccCode, CurrAccTypeCode, PostalAddressID,
                                CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                            )
                            VALUES (
                                @CurrAccCode, @CurrAccTypeCode, @PostalAddressID,
                                @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                            );";

                    var defaultParams = new
                    {
                        CurrAccCode = request.CustomerCode,
                        CurrAccTypeCode = 3, // Customer type code
                        PostalAddressID = postalAddressId,
                        CreatedUserName = request.CreatedUserName,
                        LastUpdatedUserName = request.LastUpdatedUserName
                    };

                    await connection.ExecuteAsync(defaultSql, defaultParams, transaction);

                    // If this is a billing or shipping address, set those specific fields too
                    if (address.IsBillingAddress)
                    {
                        var billingSql = @"
                            UPDATE prCurrAccDefault WITH (ROWLOCK)
                            SET BillingAddressID = @PostalAddressID,
                                LastUpdatedUserName = @LastUpdatedUserName,
                                LastUpdatedDate = GETDATE()
                            WHERE CurrAccCode = @CurrAccCode 
                            AND CurrAccTypeCode = @CurrAccTypeCode";

                        await connection.ExecuteAsync(billingSql, defaultParams, transaction);
                    }

                    if (address.IsShippingAddress)
                    {
                        var shippingSql = @"
                            UPDATE prCurrAccDefault WITH (ROWLOCK)
                            SET ShippingAddressID = @PostalAddressID,
                                LastUpdatedUserName = @LastUpdatedUserName,
                                LastUpdatedDate = GETDATE()
                            WHERE CurrAccCode = @CurrAccCode 
                            AND CurrAccTypeCode = @CurrAccTypeCode";

                        await connection.ExecuteAsync(shippingSql, defaultParams, transaction);
                    }
                }
            }
            
            return defaultPostalAddressId;
        }

        /// <summary>
        /// Müşteri iletişim bilgilerini ekler ve varsayılan iletişim bilgisini belirler
        /// </summary>
        /// <returns>Varsayılan iletişim bilgisi ID</returns>
        private async Task<Guid?> InsertCustomerCommunicationsAsync(SqlConnection connection, SqlTransaction transaction, CustomerCreateRequestNew request)
        {
            if (request.Communications == null || !request.Communications.Any())
            {
                _logger.LogInformation("Müşteri için iletişim bilgisi bulunamadı: {CustomerCode}", request.CustomerCode);
                return null;
            }

            Guid? defaultCommunicationId = null;

            foreach (var communication in request.Communications)
            {
                // Generate a new CommunicationID if not provided
                var communicationId = communication.CommunicationID ?? Guid.NewGuid();
                
                if (communication.IsDefault)
                {
                    defaultCommunicationId = communicationId;
                }

                // Insert communication
                var commSql = @"
                    INSERT INTO prCurrAccCommunication (
                        CommunicationID, CurrAccCode, CurrAccTypeCode, SubCurrAccID, ContactID,
                        CommunicationTypeCode, CommAddress, CanSendAdvert, IsBlocked, IsConfirmed,
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    )
                    VALUES (
                        @CommunicationID, @CurrAccCode, @CurrAccTypeCode, NULL, NULL,
                        @CommunicationTypeCode, @CommAddress, @CanSendAdvert, @IsBlocked, @IsConfirmed,
                        @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                    )";

                var commParams = new
                {
                    CommunicationID = communicationId,
                    CurrAccCode = request.CustomerCode,
                    CurrAccTypeCode = 3, // Customer type code
                    CommunicationTypeCode = communication.CommunicationTypeCode,
                    CommAddress = communication.CommAddress,
                    CanSendAdvert = communication.CanSendAdvert,
                    IsBlocked = communication.IsBlocked,
                    IsConfirmed = communication.IsConfirmed,
                    CreatedUserName = request.CreatedUserName,
                    LastUpdatedUserName = request.LastUpdatedUserName
                };

                await connection.ExecuteAsync(commSql, commParams, transaction);

                // Set default communication in prCurrAccDefault if this is the default communication
                if (communication.IsDefault)
                {
                    // First check if a record exists for this customer in prCurrAccDefault
                    var defaultCheckSql = @"
                        SELECT COUNT(1) FROM prCurrAccDefault WITH(NOLOCK)
                        WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode";

                    var defaultExists = await connection.ExecuteScalarAsync<int>(defaultCheckSql, 
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = 3 }, transaction) > 0;

                    string defaultSql;
                    
                    if (defaultExists)
                    {
                        // Update existing record
                        defaultSql = @"
                            UPDATE prCurrAccDefault WITH (ROWLOCK)
                            SET CommunicationID = @CommunicationID,
                                LastUpdatedUserName = @LastUpdatedUserName,
                                LastUpdatedDate = GETDATE()
                            WHERE CurrAccCode = @CurrAccCode 
                            AND CurrAccTypeCode = @CurrAccTypeCode";
                    }
                    else
                    {
                        // Insert new record with MERGE to handle race conditions
                        defaultSql = @"
                            MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                            USING (
                                SELECT 
                                    @CurrAccCode AS CurrAccCode,
                                    @CurrAccTypeCode AS CurrAccTypeCode,
                                    @CommunicationID AS CommunicationID
                            ) AS source
                            ON (
                                target.CurrAccCode = source.CurrAccCode AND 
                                target.CurrAccTypeCode = source.CurrAccTypeCode
                            )
                            WHEN MATCHED THEN
                                UPDATE SET 
                                    target.CommunicationID = source.CommunicationID,
                                    target.LastUpdatedUserName = @LastUpdatedUserName,
                                    target.LastUpdatedDate = GETDATE()
                            WHEN NOT MATCHED THEN
                                INSERT (
                                    CurrAccCode, CurrAccTypeCode, CommunicationID,
                                    CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                                )
                                VALUES (
                                    @CurrAccCode, @CurrAccTypeCode, @CommunicationID,
                                    @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                                );";
                    }

                    var defaultParams = new
                    {
                        CurrAccCode = request.CustomerCode,
                        CurrAccTypeCode = 3, // Customer type code
                        CommunicationID = communicationId,
                        CreatedUserName = request.CreatedUserName,
                        LastUpdatedUserName = request.LastUpdatedUserName
                    };

                    await connection.ExecuteAsync(defaultSql, defaultParams, transaction);

                    // Set specific communication types in prCurrAccDefault based on the communication type
                    if (communication.CommunicationTypeCode == "1") // Email
                    {
                        var emailSql = @"
                            UPDATE prCurrAccDefault WITH (ROWLOCK)
                            SET EArchieveEMailCommunicationID = @CommunicationID,
                                LastUpdatedUserName = @LastUpdatedUserName,
                                LastUpdatedDate = GETDATE()
                            WHERE CurrAccCode = @CurrAccCode 
                            AND CurrAccTypeCode = @CurrAccTypeCode";

                        await connection.ExecuteAsync(emailSql, defaultParams, transaction);
                    }
                    else if (communication.CommunicationTypeCode == "2") // Mobile
                    {
                        var mobileSql = @"
                            UPDATE prCurrAccDefault WITH (ROWLOCK)
                            SET EArchieveMobileCommunicationID = @CommunicationID,
                                LastUpdatedUserName = @LastUpdatedUserName,
                                LastUpdatedDate = GETDATE()
                            WHERE CurrAccCode = @CurrAccCode 
                            AND CurrAccTypeCode = @CurrAccTypeCode";

                        await connection.ExecuteAsync(mobileSql, defaultParams, transaction);
                    }
                    else if (communication.CommunicationTypeCode == "3") // Office Phone
                    {
                        var officeSql = @"
                            UPDATE prCurrAccDefault WITH (ROWLOCK)
                            SET OfficePhoneID = @CommunicationID,
                                LastUpdatedUserName = @LastUpdatedUserName,
                                LastUpdatedDate = GETDATE()
                            WHERE CurrAccCode = @CurrAccCode 
                            AND CurrAccTypeCode = @CurrAccTypeCode";

                        await connection.ExecuteAsync(officeSql, defaultParams, transaction);
                    }
                    else if (communication.CommunicationTypeCode == "4") // Home Phone
                    {
                        var homeSql = @"
                            UPDATE prCurrAccDefault WITH (ROWLOCK)
                            SET HomePhoneID = @CommunicationID,
                                LastUpdatedUserName = @LastUpdatedUserName,
                                LastUpdatedDate = GETDATE()
                            WHERE CurrAccCode = @CurrAccCode 
                            AND CurrAccTypeCode = @CurrAccTypeCode";

                        await connection.ExecuteAsync(homeSql, defaultParams, transaction);
                    }
                }
            }
            
            return defaultCommunicationId;
        }

        /// <summary>
        /// Müşteri iletişim kişilerini ekler ve varsayılan iletişim kişisini belirler
        /// </summary>
        /// <returns>Varsayılan iletişim kişisi ID</returns>
        private async Task<Guid?> InsertCustomerContactsAsync(SqlConnection connection, SqlTransaction transaction, CustomerCreateRequestNew request)
        {
            if (request.Contacts == null || !request.Contacts.Any())
            {
                _logger.LogInformation("Müşteri için iletişim kişisi bilgisi bulunamadı: {CustomerCode}", request.CustomerCode);
                return null;
            }

            Guid? defaultContactId = null;

            foreach (var contact in request.Contacts)
            {
                // Generate a new ContactID if not provided
                var contactId = contact.ContactID ?? Guid.NewGuid();

                if (contact.IsDefault)
                {
                    defaultContactId = contactId;
                }

                // Insert contact
                var contactSql = @"
                    INSERT INTO prCurrAccContact (
                        ContactID, CurrAccCode, CurrAccTypeCode, SubCurrAccID, ContactTypeCode,
                        TitleCode, JobTitleCode, FirstName, LastName, IdentityNum,
                        IsAuthorized, IsBlocked, CreatedUserName, CreatedDate, 
                        LastUpdatedUserName, LastUpdatedDate
                    )
                    VALUES (
                        @ContactID, @CurrAccCode, @CurrAccTypeCode, NULL, @ContactTypeCode,
                        @TitleCode, @JobTitleCode, @FirstName, @LastName, @IdentityNum,
                        @IsAuthorized, @IsBlocked, @CreatedUserName, GETDATE(),
                        @LastUpdatedUserName, GETDATE()
                    )";

                var contactParams = new
                {
                    ContactID = contactId,
                    CurrAccCode = request.CustomerCode,
                    CurrAccTypeCode = 3, // Customer type code
                    ContactTypeCode = contact.ContactTypeCode,
                    TitleCode = contact.TitleCode ?? string.Empty,
                    JobTitleCode = contact.JobTitleCode ?? string.Empty,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    IdentityNum = contact.IdentityNum ?? string.Empty,
                    IsAuthorized = contact.IsAuthorized,
                    IsBlocked = contact.IsBlocked,
                    CreatedUserName = request.CreatedUserName,
                    LastUpdatedUserName = request.LastUpdatedUserName
                };

                await connection.ExecuteAsync(contactSql, contactParams, transaction);

                // Set default contact in prCurrAccDefault if this is the default contact
                if (contact.IsDefault)
                {
                    // First check if a record exists for this customer in prCurrAccDefault
                    var defaultCheckSql = @"
                        SELECT COUNT(1) FROM prCurrAccDefault WITH(NOLOCK)
                        WHERE CurrAccCode = @CurrAccCode AND CurrAccTypeCode = @CurrAccTypeCode";

                    var defaultExists = await connection.ExecuteScalarAsync<int>(defaultCheckSql, 
                        new { CurrAccCode = request.CustomerCode, CurrAccTypeCode = 3 }, transaction) > 0;

                    string defaultSql;
                    
                    if (defaultExists)
                    {
                        // Update existing record
                        defaultSql = @"
                            UPDATE prCurrAccDefault WITH (ROWLOCK)
                            SET ContactID = @ContactID,
                                LastUpdatedUserName = @LastUpdatedUserName,
                                LastUpdatedDate = GETDATE()
                            WHERE CurrAccCode = @CurrAccCode 
                            AND CurrAccTypeCode = @CurrAccTypeCode";
                    }
                    else
                    {
                        // Insert new record with MERGE to handle race conditions
                        defaultSql = @"
                            MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                            USING (
                                SELECT 
                                    @CurrAccCode AS CurrAccCode,
                                    @CurrAccTypeCode AS CurrAccTypeCode,
                                    @ContactID AS ContactID
                            ) AS source
                            ON (
                                target.CurrAccCode = source.CurrAccCode AND 
                                target.CurrAccTypeCode = source.CurrAccTypeCode
                            )
                            WHEN MATCHED THEN
                                UPDATE SET 
                                    target.ContactID = source.ContactID,
                                    target.LastUpdatedUserName = @LastUpdatedUserName,
                                    target.LastUpdatedDate = GETDATE()
                            WHEN NOT MATCHED THEN
                                INSERT (
                                    CurrAccCode, CurrAccTypeCode, ContactID,
                                    CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                                )
                                VALUES (
                                    @CurrAccCode, @CurrAccTypeCode, @ContactID,
                                    @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                                );";
                    }

                    var defaultParams = new
                    {
                        CurrAccCode = request.CustomerCode,
                        CurrAccTypeCode = 3, // Customer type code
                        ContactID = contactId,
                        CreatedUserName = request.CreatedUserName,
                        LastUpdatedUserName = request.LastUpdatedUserName
                    };

                    await connection.ExecuteAsync(defaultSql, defaultParams, transaction);
                }
            }
            
            return defaultContactId;
        }

        /// <summary>
        /// Müşteri koduna göre müşteri bilgisini getirir
        /// </summary>
        /// <returns>Müşteri varsa müşteri bilgisi, yoksa null</returns>
        private async Task<object> GetCustomerByCodeAsync(SqlConnection connection, string customerCode)
        {
            var sql = @"
                -- Ana müşteri tablosunda kontrol
                IF EXISTS (SELECT 1 FROM cdCurrAcc WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3)
                BEGIN
                    SELECT 'cdCurrAcc' AS TableName, CurrAccCode, CurrAccTypeCode FROM cdCurrAcc 
                    WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3
                    RETURN
                END
                
                -- Müşteri açıklama tablosunda kontrol
                IF EXISTS (SELECT 1 FROM cdCurrAccDesc WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3)
                BEGIN
                    SELECT 'cdCurrAccDesc' AS TableName, CurrAccCode, CurrAccTypeCode FROM cdCurrAccDesc 
                    WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3
                    RETURN
                END
                
                -- Müşteri adres tablosunda kontrol
                IF EXISTS (SELECT 1 FROM prCurrAccPostalAddress WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3)
                BEGIN
                    SELECT 'prCurrAccPostalAddress' AS TableName, CurrAccCode, CurrAccTypeCode FROM prCurrAccPostalAddress 
                    WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3
                    RETURN
                END
                
                -- Müşteri varsayılan değerler tablosunda kontrol
                IF EXISTS (SELECT 1 FROM prCurrAccDefault WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3)
                BEGIN
                    SELECT 'prCurrAccDefault' AS TableName, CurrAccCode, CurrAccTypeCode FROM prCurrAccDefault 
                    WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3
                    RETURN
                END
                
                -- Müşteri iletişim kişileri tablosunda kontrol
                IF EXISTS (SELECT 1 FROM prCurrAccContact WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3)
                BEGIN
                    SELECT 'prCurrAccContact' AS TableName, CurrAccCode, CurrAccTypeCode FROM prCurrAccContact 
                    WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3
                    RETURN
                END
                
                -- Müşteri iletişim bilgileri tablosunda kontrol
                IF EXISTS (SELECT 1 FROM prCurrAccCommunication WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3)
                BEGIN
                    SELECT 'prCurrAccCommunication' AS TableName, CurrAccCode, CurrAccTypeCode FROM prCurrAccCommunication 
                    WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3
                    RETURN
                END
                
                -- Hiçbir tabloda bulunamadı
                SELECT NULL AS TableName, NULL AS CurrAccCode, NULL AS CurrAccTypeCode WHERE 1 = 0";

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerCode", customerCode);
            
            var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, parameters);
            
            return result;
        }

        /// <summary>
        /// Müşteri güncelleme metodu
        /// </summary>
        /// <param name="request">Müşteri güncelleme isteği</param>
        /// <returns>Müşteri güncelleme yanıtı</returns>
        public async Task<CustomerUpdateResponseNew> UpdateCustomerAsync(CustomerUpdateRequestNew request)
        {
            _logger.LogInformation("Müşteri güncelleme işlemi başlatıldı: {CustomerCode}", request.CustomerCode);
            
            var response = new CustomerUpdateResponseNew
            {
                CustomerCode = request.CustomerCode,
                Success = false,
                Message = string.Empty,
                ErrorDetails = string.Empty
            };

            using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
            {
                SqlTransaction transaction = null;
                
                try
                {
                    await connection.OpenAsync();
                    
                    // Müşteri kodu varlık kontrolü
                    var existingCustomer = await GetCustomerByCodeAsync(connection, request.CustomerCode);
                    if (existingCustomer == null)
                    {
                        response.Success = false;
                        response.Message = $"Müşteri kodu '{request.CustomerCode}' sistemde bulunamadı.";
                        _logger.LogWarning("Müşteri güncelleme başarısız: {CustomerCode} kodu bulunamadı.", request.CustomerCode);
                        return response;
                    }
                    
                    // Transaction başlat
                    transaction = connection.BeginTransaction();
                    _logger.LogInformation("Müşteri güncelleme transaction başlatıldı: {CustomerCode}", request.CustomerCode);
                    
                    try
                    {
                        // Ana müşteri kaydını güncelle
                        await UpdateCustomerMainAsync(connection, transaction, request);
                        _logger.LogInformation("Ana müşteri kaydı güncellendi: {CustomerCode}", request.CustomerCode);
                        
                        // Müşteri açıklama kaydını güncelle
                        await UpdateCustomerDescriptionAsync(connection, transaction, request);
                        _logger.LogInformation("Müşteri açıklama kaydı güncellendi: {CustomerCode}", request.CustomerCode);
                        
                        // Müşteri adres kayıtlarını güncelle
                        if (request.Addresses != null && request.Addresses.Any())
                        {
                            await UpdateCustomerAddressesAsync(connection, transaction, request);
                            _logger.LogInformation("Müşteri adres kayıtları güncellendi: {CustomerCode}, Adres Sayısı: {AddressCount}", 
                                request.CustomerCode, request.Addresses.Count);
                            
                            response.AddressCount = request.Addresses.Count;
                        }
                        
                        // Müşteri iletişim bilgisi kayıtlarını güncelle
                        if (request.Communications != null && request.Communications.Any())
                        {
                            await UpdateCustomerCommunicationsAsync(connection, transaction, request);
                            _logger.LogInformation("Müşteri iletişim bilgisi kayıtları güncellendi: {CustomerCode}, İletişim Sayısı: {CommunicationCount}", 
                                request.CustomerCode, request.Communications.Count);
                            
                            response.CommunicationCount = request.Communications.Count;
                        }
                        
                        // Müşteri iletişim kişisi kayıtlarını güncelle
                        if (request.Contacts != null && request.Contacts.Any())
                        {
                            await UpdateCustomerContactsAsync(connection, transaction, request);
                            _logger.LogInformation("Müşteri iletişim kişisi kayıtları güncellendi: {CustomerCode}, Kişi Sayısı: {ContactCount}", 
                                request.CustomerCode, request.Contacts.Count);
                            
                            response.ContactCount = request.Contacts.Count;
                        }
                        
                        // Transaction commit
                        transaction.Commit();
                        _logger.LogInformation("Müşteri güncelleme transaction commit edildi: {CustomerCode}", request.CustomerCode);
                        
                        response.Success = true;
                        response.Message = $"Müşteri '{request.CustomerCode}' başarıyla güncellendi.";
                        response.CustomerName = request.CustomerName;
                        response.CustomerTypeCode = 3; // Müşteri tipi kodu
                        
                        return response;
                    }
                    catch (Exception ex)
                    {
                        // Transaction rollback
                        transaction?.Rollback();
                        _logger.LogError(ex, "Müşteri güncelleme sırasında bir hata oluştu: {CustomerCode}, Hata: {ErrorMessage}", 
                            request.CustomerCode, ex.Message);
                        
                        response.Success = false;
                        response.Message = "Müşteri güncelleme işlemi sırasında bir hata oluştu.";
                        response.ErrorDetails = ex.Message;
                        
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Müşteri güncelleme işlemi başlatılırken bir hata oluştu: {CustomerCode}, Hata: {ErrorMessage}", 
                        request.CustomerCode, ex.Message);
                    
                    response.Success = false;
                    response.Message = "Müşteri güncelleme işlemi başlatılırken bir hata oluştu.";
                    response.ErrorDetails = ex.Message;
                    
                    return response;
                }
            }
        }

        /// <summary>
        /// Ana müşteri kaydını günceller
        /// </summary>
        private async Task UpdateCustomerMainAsync(SqlConnection connection, SqlTransaction transaction, CustomerUpdateRequestNew request)
        {
            var sql = @"
                UPDATE cdCurrAcc
                SET 
                    CurrAccTypeCode = 3,
                    OfficeCode = @OfficeCode,
                    TaxOfficeCode = @TaxOfficeCode,
                    TaxNumber = @TaxNumber,
                    CurrencyCode = @CurrencyCode,
                    CreditLimit = @CreditLimit,
                    CompanyCode = @CompanyCode,
                    LastUpdatedUserName = @LastUpdatedUserName,
                    LastUpdatedDate = GETDATE()
                WHERE 
                    CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3";

            var parameters = new
            {
                CustomerCode = request.CustomerCode,
                OfficeCode = request.OfficeCode ?? string.Empty,
                TaxOfficeCode = request.TaxOfficeCode ?? string.Empty,
                TaxNumber = request.TaxNumber ?? string.Empty,
                CurrencyCode = request.CurrencyCode ?? "TRY",
                CreditLimit = request.CreditLimit,
                CompanyCode = request.CompanyCode,
                LastUpdatedUserName = request.LastUpdatedUserName
            };

            await connection.ExecuteAsync(sql, parameters, transaction);
        }

        /// <summary>
        /// Müşteri açıklama kaydını günceller
        /// </summary>
        private async Task UpdateCustomerDescriptionAsync(SqlConnection connection, SqlTransaction transaction, CustomerUpdateRequestNew request)
        {
            var sql = @"
                UPDATE cdCurrAccDesc
                SET 
                    CurrAccDescription = RTRIM(@FirstName + N' ' + @LastName),
                    LastUpdatedUserName = @LastUpdatedUserName,
                    LastUpdatedDate = GETDATE()
                WHERE 
                    CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3 AND LangCode = @LangCode";

            var parameters = new
            {
                CustomerCode = request.CustomerCode,
                FirstName = request.CustomerName ?? string.Empty,
                LastName = request.CustomerSurname ?? string.Empty,
                LangCode = request.DataLanguageCode ?? "TR",
                LastUpdatedUserName = request.LastUpdatedUserName
            };

            await connection.ExecuteAsync(sql, parameters, transaction);
        }

        /// <summary>
        /// Müşteri adres kayıtlarını günceller
        /// </summary>
        private async Task UpdateCustomerAddressesAsync(SqlConnection connection, SqlTransaction transaction, CustomerUpdateRequestNew request)
        {
            foreach (var address in request.Addresses)
            {
                // SP'deki mantığa göre güncelleme:
                // 1. Adres ID'si varsa ve adres bilgileri boş değilse güncelle
                // 2. Adres ID'si varsa ve adres bilgileri boşsa sil
                // 3. Adres ID'si yoksa ve adres bilgileri boş değilse yeni kayıt ekle

                if (address.PostalAddressID != Guid.Empty)
                {
                    // Adres ID'si varsa, önce varlığını kontrol et
                    var checkSql = @"
                        SELECT COUNT(1) 
                        FROM prCurrAccPostalAddress 
                        WHERE PostalAddressID = @PostalAddressID";

                    var exists = await connection.ExecuteScalarAsync<int>(checkSql, new { PostalAddressID = address.PostalAddressID }, transaction) > 0;

                    if (exists)
                    {
                        // Adres bilgileri boş değilse güncelle
                        if (!string.IsNullOrWhiteSpace(address.CityCode) || 
                            !string.IsNullOrWhiteSpace(address.DistrictCode) || 
                            !string.IsNullOrWhiteSpace(address.AddressTypeCode) || 
                            !string.IsNullOrWhiteSpace(address.Address))
                        {
                            // Şehir koduna göre eyalet ve ülke kodlarını al
                            string stateCode = string.Empty;
                            string countryCode = "TR"; // Varsayılan ülke kodu

                            if (!string.IsNullOrWhiteSpace(address.CityCode))
                            {
                                var locationSql = @"
                                    SELECT ISNULL(cdCity.StateCode, '') AS StateCode, ISNULL(cdState.CountryCode, '') AS CountryCode
                                    FROM cdCity WITH(NOLOCK)
                                    LEFT OUTER JOIN cdState WITH(NOLOCK) ON cdCity.StateCode = cdState.StateCode AND cdState.IsBlocked = 0
                                    WHERE CityCode = @CityCode";

                                var locationInfo = await connection.QueryFirstOrDefaultAsync(locationSql, new { CityCode = address.CityCode }, transaction);
                                
                                if (locationInfo != null)
                                {
                                    stateCode = locationInfo.StateCode;
                                    countryCode = !string.IsNullOrWhiteSpace(locationInfo.CountryCode) ? locationInfo.CountryCode : countryCode;
                                }
                            }

                            // Adresi güncelle
                            var updateSql = @"
                                UPDATE prCurrAccPostalAddress
                                SET CityCode = @CityCode, 
                                    DistrictCode = @DistrictCode, 
                                    StreetCode = @StreetCode, 
                                    QuarterCode = @QuarterCode, 
                                    StateCode = @StateCode, 
                                    CountryCode = @CountryCode,
                                    AddressTypeCode = @AddressTypeCode, 
                                    Address = @Address,
                                    ZipCode = @ZipCode,
                                    TaxOfficeCode = @TaxOfficeCode,
                                    TaxNumber = @TaxNumber,
                                    IsBlocked = @IsBlocked,
                                    SiteName = @SiteName,
                                    BuildingName = @BuildingName,
                                    BuildingNum = @BuildingNum,
                                    FloorNum = @FloorNum,
                                    DoorNum = @DoorNum,
                                    QuarterName = @QuarterName,
                                    Street = @Street,
                                    LastUpdatedDate = GETDATE(), 
                                    LastUpdatedUserName = @LastUpdatedUserName
                                WHERE PostalAddressID = @PostalAddressID";

                            var updateParams = new
                            {
                                PostalAddressID = address.PostalAddressID,
                                CityCode = address.CityCode ?? string.Empty,
                                DistrictCode = address.DistrictCode ?? string.Empty,
                                StreetCode = address.StreetCode ?? 0,
                                QuarterCode = address.QuarterCode ?? 0,
                                StateCode = stateCode,
                                CountryCode = countryCode,
                                AddressTypeCode = address.AddressTypeCode,
                                Address = address.Address,
                                ZipCode = address.ZipCode ?? string.Empty,
                                TaxOfficeCode = string.Empty, // Trace'de boş string olarak gönderiliyor
                                TaxNumber = address.TaxNumber ?? string.Empty,
                                IsBlocked = address.IsBlocked,
                                SiteName = address.SiteName ?? string.Empty,
                                BuildingName = address.BuildingName ?? string.Empty,
                                BuildingNum = address.BuildingNum ?? string.Empty,
                                FloorNum = address.FloorNum,
                                DoorNum = address.DoorNum,
                                QuarterName = address.QuarterName ?? string.Empty,
                                Street = address.Street ?? string.Empty,
                                LastUpdatedUserName = request.LastUpdatedUserName
                            };

                            await connection.ExecuteAsync(updateSql, updateParams, transaction);
                        }
                        else
                        {
                            // Adres bilgileri boşsa, önce prCurrAccDefault tablosundaki referansları temizle
                            var updateDefaultsSql = @"
                                UPDATE prCurrAccDefault
                                SET  PostalAddressID        = CASE WHEN PostalAddressID    = @PostalAddressID THEN NULL ELSE PostalAddressID    END       
                                    ,ShippingAddressID      = CASE WHEN ShippingAddressID  = @PostalAddressID THEN NULL ELSE ShippingAddressID  END  
                                    ,BillingAddressID       = CASE WHEN BillingAddressID   = @PostalAddressID THEN NULL ELSE BillingAddressID   END  
                                    ,LastUpdatedUserName    = @LastUpdatedUserName      
                                    ,LastUpdatedDate        = GETDATE() 
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode";

                            var updateDefaultsParams = new
                            {
                                PostalAddressID = address.PostalAddressID,
                                CustomerCode = address.CustomerCode,
                                LastUpdatedUserName = request.LastUpdatedUserName
                            };

                            await connection.ExecuteAsync(updateDefaultsSql, updateDefaultsParams, transaction);

                            // Adres kaydını sil
                            var deleteSql = @"
                                DELETE FROM prCurrAccPostalAddress 
                                WHERE PostalAddressID = @PostalAddressID";

                            await connection.ExecuteAsync(deleteSql, new { PostalAddressID = address.PostalAddressID }, transaction);
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(address.Address))
                {
                    // Yeni adres kaydı ekle
                    var postalAddressID = Guid.NewGuid();
                    
                    // Şehir koduna göre eyalet ve ülke kodlarını al
                    string stateCode = string.Empty;
                    string countryCode = "TR"; // Varsayılan ülke kodu

                    if (!string.IsNullOrWhiteSpace(address.CityCode))
                    {
                        var locationSql = @"
                            SELECT ISNULL(cdCity.StateCode, '') AS StateCode, ISNULL(cdState.CountryCode, '') AS CountryCode
                            FROM cdCity WITH(NOLOCK)
                            LEFT OUTER JOIN cdState WITH(NOLOCK) ON cdCity.StateCode = cdState.StateCode AND cdState.IsBlocked = 0
                            WHERE CityCode = @CityCode";

                        var locationInfo = await connection.QueryFirstOrDefaultAsync(locationSql, new { CityCode = address.CityCode }, transaction);
                        
                        if (locationInfo != null)
                        {
                            stateCode = locationInfo.StateCode;
                            countryCode = !string.IsNullOrWhiteSpace(locationInfo.CountryCode) ? locationInfo.CountryCode : countryCode;
                        }
                    }
                    
                    var sql = @"
                        INSERT INTO prCurrAccPostalAddress (
                            PostalAddressID, CurrAccTypeCode, CurrAccCode, SubCurrAccID, ContactID, 
                            AddressTypeCode, DrivingDirections, Address, ZipCode, CountryCode, 
                            StateCode, CityCode, DistrictCode, TaxOfficeCode, TaxNumber, 
                            IsBlocked, SiteName, BuildingName, BuildingNum, FloorNum, 
                            DoorNum, QuarterName, Street, QuarterCode, StreetCode, 
                            AddressID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                        ) VALUES (
                            @PostalAddressID, 3, @CustomerCode, NULL, NULL, 
                            @AddressTypeCode, @DrivingDirections, @Address, @ZipCode, @CountryCode, 
                            @StateCode, @CityCode, @DistrictCode, @TaxOfficeCode, @TaxNumber, 
                            @IsBlocked, @SiteName, @BuildingName, @BuildingNum, @FloorNum, 
                            @DoorNum, @QuarterName, @Street, @QuarterCode, @StreetCode, 
                            @AddressID, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                        )";

                    var parameters = new
                    {
                        PostalAddressID = postalAddressID,
                        CustomerCode = address.CustomerCode,
                        AddressTypeCode = address.AddressTypeCode,
                        DrivingDirections = address.DrivingDirections ?? string.Empty,
                        Address = address.Address,
                        ZipCode = address.ZipCode ?? string.Empty,
                        CountryCode = countryCode,
                        StateCode = stateCode,
                        CityCode = address.CityCode ?? string.Empty,
                        DistrictCode = address.DistrictCode ?? string.Empty,
                        TaxOfficeCode = string.Empty, // Trace'de boş string olarak gönderiliyor
                        TaxNumber = address.TaxNumber ?? string.Empty,
                        IsBlocked = address.IsBlocked,
                        SiteName = address.SiteName ?? string.Empty,
                        BuildingName = address.BuildingName ?? string.Empty,
                        BuildingNum = address.BuildingNum ?? string.Empty,
                        FloorNum = address.FloorNum,
                        DoorNum = address.DoorNum,
                        QuarterName = address.QuarterName ?? string.Empty,
                        Street = address.Street ?? string.Empty,
                        QuarterCode = address.QuarterCode ?? 0,
                        StreetCode = address.StreetCode ?? 0,
                        AddressID = 0, // AddressID bigint tipinde, bu yüzden 0 değerini kullanıyoruz
                        CreatedUserName = request.CreatedUserName,
                        LastUpdatedUserName = request.LastUpdatedUserName
                    };

                    await connection.ExecuteAsync(sql, parameters, transaction);
                }
            }
        }

        /// <summary>
        /// Müşteri iletişim bilgilerini günceller
        /// </summary>
        private async Task UpdateCustomerCommunicationsAsync(SqlConnection connection, SqlTransaction transaction, CustomerUpdateRequestNew request)
        {
            // İletişim tipi kodlarını al
            string mobileCommunicationTypeCode = "1"; // Varsayılan telefon tipi kodu
            string emailCommunicationTypeCode = "3"; // Varsayılan e-posta tipi kodu

            try
            {
                var communicationTypesSql = @"
                    SELECT 
                        ISNULL(MobileCommunicationTypeCode, '') AS MobileCommunicationTypeCode,
                        ISNULL(EmailCommunicationTypeCode, '') AS EmailCommunicationTypeCode
                    FROM dfGuidedSalesCustomerParameters WITH(NOLOCK)";

                var communicationTypes = await connection.QueryFirstOrDefaultAsync(communicationTypesSql, null, transaction);
                
                if (communicationTypes != null)
                {
                    mobileCommunicationTypeCode = !string.IsNullOrWhiteSpace(communicationTypes.MobileCommunicationTypeCode) 
                        ? communicationTypes.MobileCommunicationTypeCode 
                        : mobileCommunicationTypeCode;
                    
                    emailCommunicationTypeCode = !string.IsNullOrWhiteSpace(communicationTypes.EmailCommunicationTypeCode) 
                        ? communicationTypes.EmailCommunicationTypeCode 
                        : emailCommunicationTypeCode;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("İletişim tipi kodları alınırken hata oluştu: {ErrorMessage}. Varsayılan kodlar kullanılacak.", ex.Message);
            }

            foreach (var communication in request.Communications)
            {
                // İletişim tipi kodunu belirle
                string communicationTypeCode = communication.CommunicationTypeCode;
                
                // Eğer iletişim tipi kodu boşsa, iletişim adresine göre otomatik belirle
                if (string.IsNullOrWhiteSpace(communicationTypeCode) && !string.IsNullOrWhiteSpace(communication.CommAddress))
                {
                    if (communication.CommAddress.Contains("@"))
                    {
                        communicationTypeCode = emailCommunicationTypeCode;
                    }
                    else
                    {
                        communicationTypeCode = mobileCommunicationTypeCode;
                    }
                }

                // SP'deki mantığa göre güncelleme:
                // 1. CommunicationID varsa ve CommAddress boş değilse güncelle
                // 2. CommunicationID varsa ve CommAddress boşsa sil
                // 3. CommunicationID yoksa yeni kayıt ekle

                if (communication.CommunicationID != Guid.Empty)
                {
                    // İletişim ID'si varsa, önce varlığını kontrol et
                    var checkSql = @"
                        SELECT COUNT(1) 
                        FROM prCurrAccCommunication 
                        WHERE CommunicationID = @CommunicationID";

                    var exists = await connection.ExecuteScalarAsync<int>(checkSql, new { CommunicationID = communication.CommunicationID }, transaction) > 0;

                    if (exists)
                    {
                        if (!string.IsNullOrWhiteSpace(communication.CommAddress))
                        {
                            // CommAddress boş değilse güncelle
                            var updateSql = @"
                                UPDATE prCurrAccCommunication
                                SET 
                                    CommunicationTypeCode = @CommunicationTypeCode,
                                    CommAddress = @CommAddress,
                                    CanSendAdvert = @CanSendAdvert,
                                    IsBlocked = @IsBlocked,
                                    IsConfirmed = @IsConfirmed,
                                    LastUpdatedUserName = @LastUpdatedUserName,
                                    LastUpdatedDate = GETDATE()
                                WHERE 
                                    CommunicationID = @CommunicationID";

                            var updateParams = new
                            {
                                CommunicationID = communication.CommunicationID,
                                CommunicationTypeCode = communicationTypeCode,
                                CommAddress = communication.CommAddress,
                                CanSendAdvert = communication.CanSendAdvert,
                                IsBlocked = communication.IsBlocked,
                                IsConfirmed = communication.IsConfirmed,
                                LastUpdatedUserName = request.LastUpdatedUserName
                            };

                            await connection.ExecuteAsync(updateSql, updateParams, transaction);
                        }
                        else
                        {
                            // CommAddress boşsa, önce prCurrAccDefault tablosundaki referansları temizle
                            var updateDefaultsSql = @"
                                UPDATE prCurrAccDefault
                                SET  CommunicationID                     = CASE WHEN CommunicationID                   = @CommunicationID THEN NULL ELSE CommunicationID                  END       
                                    ,EArchieveMobileCommunicationID      = CASE WHEN EArchieveMobileCommunicationID    = @CommunicationID THEN NULL ELSE EArchieveMobileCommunicationID   END    
                                    ,EArchieveEMailCommunicationID       = CASE WHEN EArchieveEMailCommunicationID     = @CommunicationID THEN NULL ELSE EArchieveEMailCommunicationID    END
                                    ,OfficePhoneID                       = CASE WHEN OfficePhoneID                    = @CommunicationID THEN NULL ELSE OfficePhoneID                   END
                                    ,HomePhoneID                         = CASE WHEN HomePhoneID                      = @CommunicationID THEN NULL ELSE HomePhoneID                     END
                                    ,BusinessMobileID                    = CASE WHEN BusinessMobileID                 = @CommunicationID THEN NULL ELSE BusinessMobileID                END
                                    ,PersonalMobileID                    = CASE WHEN PersonalMobileID                 = @CommunicationID THEN NULL ELSE PersonalMobileID                END
                                    ,LastUpdatedUserName                 = @LastUpdatedUserName      
                                    ,LastUpdatedDate                     = GETDATE() 
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode";

                            var updateDefaultsParams = new
                            {
                                CommunicationID = communication.CommunicationID,
                                CustomerCode = communication.CustomerCode,
                                LastUpdatedUserName = request.LastUpdatedUserName
                            };

                            await connection.ExecuteAsync(updateDefaultsSql, updateDefaultsParams, transaction);

                            // prCurrAccOptInOptOutStatus tablosundan ilgili kayıtları sil
                            var deleteOptInOutSql = @"
                                DELETE FROM prCurrAccOptInOptOutStatus 
                                WHERE CommunicationID = @CommunicationID";

                            await connection.ExecuteAsync(deleteOptInOutSql, new { CommunicationID = communication.CommunicationID }, transaction);

                            // İletişim kaydını sil
                            var deleteSql = @"
                                DELETE FROM prCurrAccCommunication 
                                WHERE CommunicationID = @CommunicationID";

                            await connection.ExecuteAsync(deleteSql, new { CommunicationID = communication.CommunicationID }, transaction);
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(communication.CommAddress))
                {
                    // Yeni iletişim bilgisi kaydı ekle
                    var communicationID = Guid.NewGuid();
                    
                    var sql = @"
                        INSERT INTO prCurrAccCommunication (
                            CommunicationID, CurrAccTypeCode, CurrAccCode, SubCurrAccID, ContactID, 
                            CommunicationTypeCode, CommAddress, CanSendAdvert, IsBlocked, IsConfirmed, 
                            CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                        ) VALUES (
                            @CommunicationID, 3, @CustomerCode, NULL, NULL, 
                            @CommunicationTypeCode, @CommAddress, @CanSendAdvert, @IsBlocked, @IsConfirmed, 
                            @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                        )";

                    var parameters = new
                    {
                        CommunicationID = communicationID,
                        CustomerCode = communication.CustomerCode,
                        CommunicationTypeCode = communicationTypeCode,
                        CommAddress = communication.CommAddress,
                        CanSendAdvert = communication.CanSendAdvert,
                        IsBlocked = communication.IsBlocked,
                        IsConfirmed = communication.IsConfirmed,
                        CreatedUserName = request.CreatedUserName,
                        LastUpdatedUserName = request.LastUpdatedUserName
                    };

                    await connection.ExecuteAsync(sql, parameters, transaction);
                }
            }
        }

        /// <summary>
        /// Müşteri iletişim kişilerini günceller
        /// </summary>
        private async Task UpdateCustomerContactsAsync(SqlConnection connection, SqlTransaction transaction, CustomerUpdateRequestNew request)
        {
            foreach (var contact in request.Contacts)
            {
                // ContactID varsa güncelle, yoksa yeni kayıt ekle
                if (contact.ContactID != Guid.Empty)
                {
                    var sql = @"
                        UPDATE prCurrAccContact
                        SET 
                            ContactTypeCode = @ContactTypeCode,
                            FirstName = @FirstName,
                            LastName = @LastName,
                            TitleCode = @TitleCode,
                            JobTitleCode = @JobTitleCode,
                            IsAuthorized = @IsAuthorized,
                            IdentityNum = @IdentityNum,
                            IsDefault = @IsDefault,
                            IsBlocked = @IsBlocked,
                            LastUpdatedUserName = @LastUpdatedUserName,
                            LastUpdatedDate = GETDATE()
                        WHERE 
                            ContactID = @ContactID AND CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3";

                    var parameters = new
                    {
                        ContactID = contact.ContactID,
                        CustomerCode = contact.CustomerCode,
                        ContactTypeCode = contact.ContactTypeCode,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        TitleCode = contact.TitleCode ?? string.Empty,
                        JobTitleCode = contact.JobTitleCode ?? string.Empty,
                        IsAuthorized = contact.IsAuthorized,
                        IdentityNum = contact.IdentityNum ?? string.Empty,
                        IsDefault = contact.IsDefault,
                        IsBlocked = contact.IsBlocked,
                        LastUpdatedUserName = request.LastUpdatedUserName
                    };

                    await connection.ExecuteAsync(sql, parameters, transaction);
                }
                else
                {
                    // Yeni iletişim kişisi kaydı ekle
                    var contactID = Guid.NewGuid();
                    
                    var sql = @"
                        INSERT INTO prCurrAccContact (
                            ContactID, CurrAccTypeCode, CurrAccCode, SubCurrAccID, 
                            ContactTypeCode, FirstName, LastName, TitleCode, JobTitleCode, 
                            IsAuthorized, IdentityNum, IsDefault, IsBlocked, 
                            CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                        ) VALUES (
                            @ContactID, 3, @CustomerCode, NULL, 
                            @ContactTypeCode, @FirstName, @LastName, @TitleCode, @JobTitleCode, 
                            @IsAuthorized, @IdentityNum, @IsDefault, @IsBlocked, 
                            @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                        )";

                    var parameters = new
                    {
                        ContactID = contactID,
                        CustomerCode = contact.CustomerCode,
                        ContactTypeCode = contact.ContactTypeCode,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        TitleCode = contact.TitleCode ?? string.Empty,
                        JobTitleCode = contact.JobTitleCode ?? string.Empty,
                        IsAuthorized = contact.IsAuthorized,
                        IdentityNum = contact.IdentityNum ?? string.Empty,
                        IsDefault = contact.IsDefault,
                        IsBlocked = contact.IsBlocked,
                        CreatedUserName = request.CreatedUserName,
                        LastUpdatedUserName = request.LastUpdatedUserName
                    };

                    await connection.ExecuteAsync(sql, parameters, transaction);
                }
            }
        }
    }
}
