using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Interfaces;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Müşteri adres işlemleri için servis sınıfı
    /// </summary>
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly ILogger<CustomerAddressService> _logger;
        private readonly IConfiguration _configuration;

        public CustomerAddressService(ILogger<CustomerAddressService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<AddressTypeResponse>> GetAddressTypesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            at.AddressTypeCode,
                            atd.AddressTypeDescription,
                            at.IsBlocked
                        FROM cdAddressType at WITH(NOLOCK)
                        LEFT JOIN cdAddressTypeDesc atd WITH(NOLOCK) ON atd.AddressTypeCode = at.AddressTypeCode AND atd.LangCode = 'TR'
                        ORDER BY at.AddressTypeCode";

                    var result = await connection.QueryAsync<AddressTypeResponse>(sql);
                    return result.ToList();
                }
            }
            catch (SqlException ex) // Catch specific SQL errors
            {
                // Log specific SQL errors
                _logger.LogWarning(ex, "Database error occurred while getting address types. Will return empty list.");
                return new List<AddressTypeResponse>(); // Return empty on SQL error
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting address types. Will return empty list.");
                return new List<AddressTypeResponse>(); // Return empty on general error
            }
        }

        public async Task<AddressTypeResponse> GetAddressTypeByCodeAsync(string code)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            at.AddressTypeCode,
                            atd.AddressTypeDescription,
                            at.IsBlocked
                        FROM cdAddressType at WITH(NOLOCK)
                        LEFT JOIN cdAddressTypeDesc atd WITH(NOLOCK) ON atd.AddressTypeCode = at.AddressTypeCode AND atd.LangCode = 'TR'
                        WHERE at.AddressTypeCode = @Code";

                    var result = await connection.QueryFirstOrDefaultAsync<AddressTypeResponse>(sql, new { Code = code });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting address type by code. Code: {Code}", code);
                throw;
            }
        }

        public async Task<AddressTypeResponse> CreateAddressTypeAsync(AddressTypeCreateRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Adres tipi ekleme
                            var sql = @"
                                INSERT INTO cdAddressType (
                                    AddressTypeCode, IsBlocked, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName
                                )
                                VALUES (
                                    @AddressTypeCode, @IsBlocked, GETDATE(), @CreatedUserName, GETDATE(), @LastUpdatedUserName
                                )";

                            await connection.ExecuteAsync(sql, new
                            {
                                request.AddressTypeCode,
                                request.IsBlocked,
                                request.CreatedUserName,
                                request.LastUpdatedUserName
                            }, transaction);

                            // Adres tipi açıklaması ekleme
                            if (!string.IsNullOrEmpty(request.AddressTypeDescription))
                            {
                                var descSql = @"
                                    INSERT INTO cdAddressTypeDesc (
                                        AddressTypeCode, LangCode, AddressTypeDescription
                                    )
                                    VALUES (
                                        @AddressTypeCode, 'TR', @AddressTypeDescription
                                    )";

                                await connection.ExecuteAsync(descSql, new
                                {
                                    request.AddressTypeCode,
                                    request.AddressTypeDescription
                                }, transaction);
                            }

                            await transaction.CommitAsync();
                            return await GetAddressTypeByCodeAsync(request.AddressTypeCode);
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred during address type creation. Request: {@Request}", request);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during address type creation. Request: {@Request}", request);
                throw;
            }
        }

        public async Task<AddressTypeResponse> UpdateAddressTypeAsync(string code, AddressTypeUpdateRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Adres tipi güncelleme
                            var sql = @"
                                UPDATE cdAddressType SET
                                    IsBlocked = @IsBlocked,
                                    LastUpdatedDate = GETDATE(),
                                    LastUpdatedUserName = @LastUpdatedUserName
                                WHERE AddressTypeCode = @AddressTypeCode";

                            await connection.ExecuteAsync(sql, new
                            {
                                AddressTypeCode = code,
                                request.IsBlocked,
                                request.LastUpdatedUserName
                            }, transaction);

                            // Adres tipi açıklaması güncelleme
                            if (!string.IsNullOrEmpty(request.AddressTypeDescription))
                            {
                                var descSql = @"
                                    MERGE cdAddressTypeDesc WITH (HOLDLOCK) AS target
                                    USING (SELECT 
                                        @AddressTypeCode AS AddressTypeCode,
                                        'TR' AS LangCode,
                                        @AddressTypeDescription AS AddressTypeDescription
                                    ) AS source
                                    ON (target.AddressTypeCode = source.AddressTypeCode AND target.LangCode = source.LangCode)
                                    WHEN MATCHED THEN
                                        UPDATE SET AddressTypeDescription = source.AddressTypeDescription
                                    WHEN NOT MATCHED THEN
                                        INSERT (AddressTypeCode, LangCode, AddressTypeDescription)
                                        VALUES (source.AddressTypeCode, source.LangCode, source.AddressTypeDescription);";

                                await connection.ExecuteAsync(descSql, new
                                {
                                    AddressTypeCode = code,
                                    request.AddressTypeDescription
                                }, transaction);
                            }

                            await transaction.CommitAsync();
                            return await GetAddressTypeByCodeAsync(code);
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred during address type update. Code: {Code}, Request: {@Request}", code, request);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during address type update. Code: {Code}, Request: {@Request}", code, request);
                throw;
            }
        }

        public async Task<bool> DeleteAddressTypeAsync(string code)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Önce açıklama tablosundan silme
                            var descSql = @"DELETE FROM cdAddressTypeDesc WHERE AddressTypeCode = @AddressTypeCode";
                            await connection.ExecuteAsync(descSql, new { AddressTypeCode = code }, transaction);

                            // Sonra ana tablodan silme
                            var sql = @"DELETE FROM cdAddressType WHERE AddressTypeCode = @AddressTypeCode";
                            var result = await connection.ExecuteAsync(sql, new { AddressTypeCode = code }, transaction);

                            await transaction.CommitAsync();
                            return result > 0;
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred during address type deletion. Code: {Code}", code);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during address type deletion. Code: {Code}", code);
                throw;
            }
        }

        public async Task<List<AddressResponse>> GetAddressesAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            pa.PostalAddressID AS PostalAddressId,
                            pa.CurrAccTypeCode,
                            pa.CurrAccCode AS CustomerCode,
                            pa.AddressTypeCode,
                            at.AddressTypeDescription,
                            pa.Address,
                            pa.CountryCode,
                            cd.CountryDescription,
                            pa.StateCode,
                            sd.StateDescription,
                            pa.CityCode,
                            cid.CityDescription,
                            pa.DistrictCode,
                            dd.DistrictDescription,
                            pa.ZipCode AS PostalCode,
                            pa.IsBlocked,
                            CASE WHEN cad.PostalAddressID IS NOT NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM prCurrAccPostalAddress pa WITH(NOLOCK)
                        LEFT JOIN cdAddressTypeDesc at WITH(NOLOCK) ON at.AddressTypeCode = pa.AddressTypeCode AND at.LangCode = 'TR'
                        LEFT JOIN cdCountryDesc cd WITH(NOLOCK) ON cd.CountryCode = pa.CountryCode AND cd.LangCode = 'TR'
                        LEFT JOIN cdStateDesc sd WITH(NOLOCK) ON sd.StateCode = pa.StateCode AND sd.LangCode = 'TR'
                        LEFT JOIN cdCityDesc cid WITH(NOLOCK) ON cid.CityCode = pa.CityCode AND cid.LangCode = 'TR'
                        LEFT JOIN cdDistrictDesc dd WITH(NOLOCK) ON dd.DistrictCode = pa.DistrictCode AND dd.LangCode = 'TR'
                        LEFT JOIN prCurrAccDefault cad WITH(NOLOCK) ON cad.CurrAccTypeCode = pa.CurrAccTypeCode AND cad.CurrAccCode = pa.CurrAccCode AND cad.PostalAddressID = pa.PostalAddressID
                        WHERE pa.CurrAccTypeCode = 3 AND pa.CurrAccCode = @CustomerCode
                        ORDER BY pa.AddressTypeCode";

                    var addresses = await connection.QueryAsync<AddressResponse>(sql, new { CustomerCode = customerCode });
                    return addresses.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting addresses for customer. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        public async Task<AddressResponse> GetAddressByIdAsync(string customerCode, string addressId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            pa.PostalAddressID AS PostalAddressId,
                            pa.CurrAccTypeCode,
                            pa.CurrAccCode AS CustomerCode,
                            pa.AddressTypeCode,
                            at.AddressTypeDescription,
                            pa.Address,
                            pa.CountryCode,
                            cd.CountryDescription,
                            pa.StateCode,
                            sd.StateDescription,
                            pa.CityCode,
                            cid.CityDescription,
                            pa.DistrictCode,
                            dd.DistrictDescription,
                            pa.ZipCode AS PostalCode,
                            pa.IsBlocked,
                            CASE WHEN cad.PostalAddressID IS NOT NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM prCurrAccPostalAddress pa WITH(NOLOCK)
                        LEFT JOIN cdAddressTypeDesc at WITH(NOLOCK) ON at.AddressTypeCode = pa.AddressTypeCode AND at.LangCode = 'TR'
                        LEFT JOIN cdCountryDesc cd WITH(NOLOCK) ON cd.CountryCode = pa.CountryCode AND cd.LangCode = 'TR'
                        LEFT JOIN cdStateDesc sd WITH(NOLOCK) ON sd.StateCode = pa.StateCode AND sd.LangCode = 'TR'
                        LEFT JOIN cdCityDesc cid WITH(NOLOCK) ON cid.CityCode = pa.CityCode AND cid.LangCode = 'TR'
                        LEFT JOIN cdDistrictDesc dd WITH(NOLOCK) ON dd.DistrictCode = pa.DistrictCode AND dd.LangCode = 'TR'
                        LEFT JOIN prCurrAccDefault cad WITH(NOLOCK) ON cad.CurrAccTypeCode = pa.CurrAccTypeCode AND cad.CurrAccCode = pa.CurrAccCode AND cad.PostalAddressID = pa.PostalAddressID
                        WHERE pa.CurrAccTypeCode = 3 AND pa.CurrAccCode = @CustomerCode AND pa.PostalAddressID = @AddressId";

                    var address = await connection.QueryFirstOrDefaultAsync<AddressResponse>(sql, new { CustomerCode = customerCode, AddressId = addressId });
                    return address;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting address by id. CustomerCode: {CustomerCode}, AddressId: {AddressId}", customerCode, addressId);
                throw;
            }
        }

        public async Task<AddressResponse> CreateAddressAsync(string customerCode, AddressCreateRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var postalAddressId = Guid.NewGuid();

                            var addressSql = @"
                                INSERT INTO prCurrAccPostalAddress (
                                    PostalAddressID, CurrAccTypeCode, CurrAccCode, AddressTypeCode, AddressID, Address, CountryCode,
                                    StateCode, CityCode, DistrictCode, PostalCode, QuarterCode, QuarterName, BoulevardCode,
                                    StreetCode, Street, Road, DrivingDirections, TaxOfficeCode, TaxNumber, IsBlocked,
                                    CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                                )
                                VALUES
                                (
                                    @PostalAddressID, 3, @CustomerCode, @AddressTypeCode, 0, @Address, @CountryCode, @StateCode, @CityCode,
                                    @DistrictCode, @PostalCode, '', '', '', 0, 0, 0, '', '', '', @IsBlocked, 'SYSTEM',
                                    GETDATE(), 'SYSTEM', GETDATE()
                                )";

                            await connection.ExecuteAsync(addressSql, new
                            {
                                PostalAddressID = postalAddressId,
                                CustomerCode = customerCode,
                                request.AddressTypeCode,
                                request.Address,
                                request.CountryCode,
                                request.StateCode,
                                request.CityCode,
                                request.DistrictCode,
                                PostalCode = request.ZipCode,
                                request.IsBlocked
                            }, transaction);

                            // If this is the default address, update the default address using MERGE
                            if (request.IsDefault)
                            {
                                var mergeDefaultSql = @"
                                    MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                                    USING (SELECT
                                        3 AS CurrAccTypeCode,
                                        @CustomerCode AS CurrAccCode,
                                        @PostalAddressID AS PostalAddressID
                                    ) AS source
                                    ON (target.CurrAccTypeCode = source.CurrAccTypeCode AND target.CurrAccCode = source.CurrAccCode)
                                    WHEN MATCHED THEN
                                        UPDATE SET
                                            PostalAddressID = source.PostalAddressID,
                                            LastUpdatedUserName = 'SYSTEM',
                                            LastUpdatedDate = GETDATE()
                                    WHEN NOT MATCHED THEN
                                        INSERT (CurrAccTypeCode, CurrAccCode, PostalAddressID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                                        VALUES (source.CurrAccTypeCode, source.CurrAccCode, source.PostalAddressID, 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE());";

                                await connection.ExecuteAsync(mergeDefaultSql, new
                                {
                                    CustomerCode = customerCode,
                                    PostalAddressID = postalAddressId
                                }, transaction);
                            }

                            await transaction.CommitAsync();

                            // Return the created address by fetching it again
                            // Pass the GUID as string to GetAddressByIdAsync
                            return await GetAddressByIdAsync(customerCode, postalAddressId.ToString());
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred while creating address for customer {CustomerCode}. Request: {@Request}", customerCode, request);
                            throw; // Re-throw after rollback
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch potential connection errors
                _logger.LogError(ex, "Error occurred while creating address for customer {CustomerCode}. Request: {@Request}", customerCode, request);
                throw; // Re-throw
            }
        }

        public async Task<AddressResponse> UpdateAddressAsync(string customerCode, string addressTypeCode, AddressUpdateRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Önce adresi bul
                            var findSql = @"
                                SELECT PostalAddressID
                                FROM prCurrAccPostalAddress
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND AddressTypeCode = @AddressTypeCode";

                            var postalAddressId = await connection.ExecuteScalarAsync<Guid>(findSql, new
                            {
                                CustomerCode = customerCode,
                                AddressTypeCode = addressTypeCode
                            }, transaction);

                            if (postalAddressId == Guid.Empty)
                            {
                                throw new Exception($"Address not found for customer {customerCode} with address type {addressTypeCode}");
                            }

                            // Adresi güncelle
                            var updateSql = @"
                                UPDATE prCurrAccPostalAddress SET
                                    Address = @Address,
                                    CountryCode = @CountryCode,
                                    StateCode = @StateCode,
                                    CityCode = @CityCode,
                                    DistrictCode = @DistrictCode,
                                    PostalCode = @PostalCode,
                                    IsBlocked = @IsBlocked,
                                    LastUpdatedUserName = 'SYSTEM',
                                    LastUpdatedDate = GETDATE()
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND AddressTypeCode = @AddressTypeCode";

                            await connection.ExecuteAsync(updateSql, new
                            {
                                CustomerCode = customerCode,
                                AddressTypeCode = addressTypeCode,
                                request.Address,
                                request.CountryCode,
                                request.StateCode,
                                request.CityCode,
                                request.DistrictCode,
                                PostalCode = request.ZipCode,
                                request.IsBlocked
                            }, transaction);

                            // Varsayılan adres ise güncelle
                            if (request.IsDefault)
                            {
                                var mergeDefaultSql = @"
                                    MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                                    USING (SELECT
                                        3 AS CurrAccTypeCode,
                                        @CustomerCode AS CurrAccCode,
                                        @PostalAddressID AS PostalAddressID
                                    ) AS source
                                    ON (target.CurrAccTypeCode = source.CurrAccTypeCode AND target.CurrAccCode = source.CurrAccCode)
                                    WHEN MATCHED THEN
                                        UPDATE SET
                                            PostalAddressID = source.PostalAddressID,
                                            LastUpdatedUserName = 'SYSTEM',
                                            LastUpdatedDate = GETDATE()
                                    WHEN NOT MATCHED THEN
                                        INSERT (CurrAccTypeCode, CurrAccCode, PostalAddressID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                                        VALUES (source.CurrAccTypeCode, source.CurrAccCode, source.PostalAddressID, 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE());";

                                await connection.ExecuteAsync(mergeDefaultSql, new
                                {
                                    CustomerCode = customerCode,
                                    PostalAddressID = postalAddressId
                                }, transaction);
                            }

                            await transaction.CommitAsync();
                            return await GetAddressByIdAsync(customerCode, postalAddressId.ToString());
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred while updating address. CustomerCode: {CustomerCode}, AddressTypeCode: {AddressTypeCode}, Request: {@Request}", customerCode, addressTypeCode, request);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating address. CustomerCode: {CustomerCode}, AddressTypeCode: {AddressTypeCode}, Request: {@Request}", customerCode, addressTypeCode, request);
                throw;
            }
        }

        public async Task<bool> DeleteAddressAsync(string customerCode, string addressTypeCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Önce adresi bul
                            var findSql = @"
                                SELECT PostalAddressID
                                FROM prCurrAccPostalAddress
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND AddressTypeCode = @AddressTypeCode";

                            var postalAddressId = await connection.ExecuteScalarAsync<Guid>(findSql, new
                            {
                                CustomerCode = customerCode,
                                AddressTypeCode = addressTypeCode
                            }, transaction);

                            if (postalAddressId == Guid.Empty)
                            {
                                return false; // Adres bulunamadı
                            }

                            // Varsayılan adres kaydını temizle
                            var clearDefaultSql = @"
                                UPDATE prCurrAccDefault SET
                                    PostalAddressID = NULL,
                                    LastUpdatedUserName = 'SYSTEM',
                                    LastUpdatedDate = GETDATE()
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND PostalAddressID = @PostalAddressID";

                            await connection.ExecuteAsync(clearDefaultSql, new
                            {
                                CustomerCode = customerCode,
                                PostalAddressID = postalAddressId
                            }, transaction);

                            // Adresi sil
                            var deleteSql = @"
                                DELETE FROM prCurrAccPostalAddress
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND AddressTypeCode = @AddressTypeCode";

                            var result = await connection.ExecuteAsync(deleteSql, new
                            {
                                CustomerCode = customerCode,
                                AddressTypeCode = addressTypeCode
                            }, transaction);

                            await transaction.CommitAsync();
                            return result > 0;
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred while deleting address. CustomerCode: {CustomerCode}, AddressTypeCode: {AddressTypeCode}", customerCode, addressTypeCode);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting address. CustomerCode: {CustomerCode}, AddressTypeCode: {AddressTypeCode}", customerCode, addressTypeCode);
                throw;
            }
        }

        public async Task<List<CustomerAddressResponse>> GetCustomerAddressesAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            pa.PostalAddressID AS PostalAddressId,
                            pa.CurrAccTypeCode,
                            pa.CurrAccCode AS CustomerCode,
                            pa.AddressTypeCode,
                            at.AddressTypeDescription,
                            pa.Address,
                            pa.CountryCode,
                            cd.CountryDescription,
                            pa.StateCode,
                            sd.StateDescription,
                            pa.CityCode,
                            cid.CityDescription,
                            pa.DistrictCode,
                            dd.DistrictDescription,
                            pa.ZipCode AS PostalCode,
                            pa.IsBlocked,
                            CASE WHEN cad.PostalAddressID IS NOT NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM prCurrAccPostalAddress pa WITH(NOLOCK)
                        LEFT JOIN cdAddressTypeDesc at WITH(NOLOCK) ON at.AddressTypeCode = pa.AddressTypeCode AND at.LangCode = 'TR'
                        LEFT JOIN cdCountryDesc cd WITH(NOLOCK) ON cd.CountryCode = pa.CountryCode AND cd.LangCode = 'TR'
                        LEFT JOIN cdStateDesc sd WITH(NOLOCK) ON sd.StateCode = pa.StateCode AND sd.LangCode = 'TR'
                        LEFT JOIN cdCityDesc cid WITH(NOLOCK) ON cid.CityCode = pa.CityCode AND cid.LangCode = 'TR'
                        LEFT JOIN cdDistrictDesc dd WITH(NOLOCK) ON dd.DistrictCode = pa.DistrictCode AND dd.LangCode = 'TR'
                        LEFT JOIN prCurrAccDefault cad WITH(NOLOCK) ON cad.CurrAccTypeCode = pa.CurrAccTypeCode AND cad.CurrAccCode = pa.CurrAccCode AND cad.PostalAddressID = pa.PostalAddressID
                        WHERE pa.CurrAccTypeCode = 3 AND pa.CurrAccCode = @CustomerCode
                        ORDER BY pa.AddressTypeCode";

                    var addresses = await connection.QueryAsync<CustomerAddressResponse>(sql, new { CustomerCode = customerCode });
                    return addresses.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer addresses. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        public async Task<bool> AddCustomerAddressAsync(CustomerAddressCreateRequestNew request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            
            try
            {
                var sqlTransaction = (Microsoft.Data.SqlClient.SqlTransaction)transaction;
                var result = await AddCustomerAddressInternalAsync(request, connection, sqlTransaction);
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Müşteri adres ekleme sırasında hata: {CustomerCode}, {Message}", request.CustomerCode, ex.Message);
                throw;
            }
        }

        public async Task<bool> AddCustomerAddressInternalAsync(CustomerAddressCreateRequestNew request, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                _logger.LogInformation("Müşteri adres ekleme işlemi başlatıldı (dahili): {CustomerCode}, {AddressTypeCode}", request.CustomerCode, request.AddressTypeCode);
                
                // Müşteri varlık kontrolü ve tipi
                var customer = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "SELECT CurrAccTypeCode FROM cdCurrAcc WHERE CurrAccCode = @CustomerCode",
                    new { CustomerCode = request.CustomerCode },
                    transaction);

                if (customer == null)
                {
                    _logger.LogError("Müşteri bulunamadı: {CustomerCode}", request.CustomerCode);
                    return false;
                }
                
                byte currAccTypeCode = customer.CurrAccTypeCode;
                
                // Adres ID'si oluştur
                var addressId = request.PostalAddressID ?? Guid.NewGuid();
                
                // Adres ekleme SQL sorgusu
                var sql = @"
                    INSERT INTO prCurrAccPostalAddress (
                        PostalAddressID, CurrAccTypeCode, CurrAccCode, SubCurrAccID, ContactID, AddressTypeCode, CountryCode, 
                        StateCode, CityCode, DistrictCode, Address, ZipCode, 
                        TaxOfficeCode, TaxNumber, Street, QuarterName, DrivingDirections,
                        AddressID, SiteName, BuildingName, BuildingNum, FloorNum, DoorNum,
                        QuarterCode, Boulevard, StreetCode, Road, IsBlocked,
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    ) VALUES (
                        @PostalAddressID, @CurrAccTypeCode, @CurrAccCode, @SubCurrAccID, @ContactID, @AddressTypeCode, @CountryCode, 
                        @StateCode, @CityCode, @DistrictCode, @Address, @ZipCode, 
                        @TaxOfficeCode, @TaxNumber, @Street, @QuarterName, @DrivingDirections,
                        @AddressID, @SiteName, @BuildingName, @BuildingNum, @FloorNum, @DoorNum,
                        @QuarterCode, @Boulevard, @StreetCode, @Road, @IsBlocked,
                        @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                    )";
                
                var parameters = new
                {
                    PostalAddressID = addressId,
                    CurrAccTypeCode = currAccTypeCode,
                    CurrAccCode = request.CustomerCode,
                    SubCurrAccID = (Guid?)null, // Null olabilir
                    ContactID = (Guid?)null, // Null olabilir
                    AddressTypeCode = request.AddressTypeCode,
                    CountryCode = request.CountryCode,
                    StateCode = request.StateCode,
                    CityCode = request.CityCode,
                    DistrictCode = request.DistrictCode,
                    Address = request.Address,
                    ZipCode = request.ZipCode,
                    TaxOfficeCode = request.TaxOfficeCode ?? "KADIKOY",
                    TaxNumber = request.TaxNumber ?? "",
                    Street = request.Street ?? "",
                    QuarterName = request.QuarterName ?? "",
                    DrivingDirections = request.DrivingDirections ?? "",
                    AddressID = 0, // Varsayılan değer
                    SiteName = "", // Varsayılan değer
                    BuildingName = "", // Varsayılan değer
                    BuildingNum = "", // Varsayılan değer
                    FloorNum = (short)0, // Varsayılan değer
                    DoorNum = (short)0, // Varsayılan değer
                    QuarterCode = 0, // Varsayılan değer
                    Boulevard = "", // Varsayılan değer
                    StreetCode = 0, // Varsayılan değer
                    Road = "", // Varsayılan değer
                    IsBlocked = false, // Varsayılan değer
                    CreatedUserName = request.CreatedUserName ?? "SYSTEM",
                    LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                };
                
                try
                {
                    _logger.LogInformation("SQL sorgusu: {SQL}", sql);
                    _logger.LogInformation("Parametreler: CurrAccTypeCode={CurrAccTypeCode}, CurrAccCode={CurrAccCode}, AddressTypeCode={AddressTypeCode}", 
                        currAccTypeCode, request.CustomerCode, request.AddressTypeCode);
                    
                    await connection.ExecuteAsync(sql, parameters, transaction);
                    
                    // Adres kaydının başarılı olup olmadığını kontrol et
                    var checkSql = "SELECT COUNT(*) FROM prCurrAccPostalAddress WHERE PostalAddressID = @PostalAddressID";
                    var count = await connection.ExecuteScalarAsync<int>(checkSql, new { PostalAddressID = addressId }, transaction);
                    
                    _logger.LogInformation("Adres kaydı kontrolü: {Count} kayıt bulundu", count);
                    
                    if (count == 0)
                    {
                        _logger.LogError("Adres kaydı oluşturulamadı: {AddressId}", addressId);
                    }
                    
                    // Varsayılan adres ayarı
                    if (request.IsDefault)
                    {
                        await SetDefaultAddressAsync(request.CustomerCode, addressId, connection, transaction);
                    }
                    
                    _logger.LogInformation("Müşteri adres bilgisi ekleme işlemi tamamlandı (dahili): {CustomerCode}, {AddressTypeCode}", request.CustomerCode, request.AddressTypeCode);
                    
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Adres ekleme hatası: {Error}, SQL: {SQL}", ex.Message, sql);
                    _logger.LogError("Hata detayı: {StackTrace}", ex.StackTrace);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri adres ekleme işlemi sırasında hata oluştu (dahili): {CustomerCode}, {AddressTypeCode}, SQL Hata: {SqlError}", 
                    request.CustomerCode, request.AddressTypeCode, ex.InnerException?.Message ?? ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// Varsayılan adresi ayarlar
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="addressId">Adres ID</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        public async Task<bool> SetDefaultAddressAsync(string customerCode, Guid addressId, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                _logger.LogInformation("Varsayılan adres ayarlama işlemi başlatıldı: {CustomerCode}, {AddressId}", customerCode, addressId);
                
                // Müşteri tipi kodunu al (varsayılan olarak 1 - müşteri)
                byte currAccTypeCode = 1;
                try {
                    currAccTypeCode = await connection.QueryFirstOrDefaultAsync<byte>(
                        "SELECT CurrAccTypeCode FROM cdCurrAcc WHERE CurrAccCode = @CustomerCode",
                        new { CustomerCode = customerCode },
                        transaction);
                } catch (Exception ex) {
                    _logger.LogWarning("Müşteri tipi kodu alınamadı, varsayılan değer (1) kullanılacak: {Error}", ex.Message);
                }
                
                // Önce mevcut kaydı sil (PRIMARY KEY çakışmasını önlemek için)
                var deleteSql = @"
                    DELETE FROM prCurrAccDefault 
                    WHERE CurrAccTypeCode = @CurrAccTypeCode AND CurrAccCode = @CurrAccCode";
                
                await connection.ExecuteAsync(deleteSql, new { 
                    CurrAccTypeCode = currAccTypeCode, 
                    CurrAccCode = customerCode 
                }, transaction);
                
                // Varsayılan adres ayarlama SQL sorgusu
                var sql = @"
                    INSERT INTO prCurrAccDefault (
                        CurrAccTypeCode, CurrAccCode, PostalAddressID, 
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    ) VALUES (
                        @CurrAccTypeCode, @CurrAccCode, @PostalAddressID, 
                        @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                    )";
                
                var parameters = new
                {
                    CurrAccTypeCode = currAccTypeCode,
                    CurrAccCode = customerCode,
                    PostalAddressID = addressId,
                    CreatedUserName = "SYSTEM",
                    LastUpdatedUserName = "SYSTEM"
                };
                
                await connection.ExecuteAsync(sql, parameters, transaction);
                
                _logger.LogInformation("Varsayılan adres ayarlama işlemi tamamlandı: {CustomerCode}, {AddressId}", customerCode, addressId);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Varsayılan adres ayarlama işlemi sırasında hata oluştu: {CustomerCode}, {AddressId}, Hata: {Message}", customerCode, addressId, ex.Message);
                return false;
            }
        }
    }
}
