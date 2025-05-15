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
    /// Müşteri iletişim işlemleri için servis sınıfı
    /// </summary>
    public class CustomerCommunicationService : ICustomerCommunicationService
    {
        private readonly ILogger<CustomerCommunicationService> _logger;
        private readonly IConfiguration _configuration;

        public CustomerCommunicationService(ILogger<CustomerCommunicationService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<CustomerCommunicationResponse>> GetCustomerCommunicationsAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            c.CommunicationID AS CommunicationId,
                            c.CurrAccTypeCode,
                            c.CurrAccCode AS CustomerCode,
                            c.CommunicationTypeCode,
                            ct.CommunicationTypeDescription,
                            c.CommAddress,
                            c.IsBlocked,
                            CASE WHEN cad.CommunicationID IS NOT NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM prCurrAccCommunication c WITH(NOLOCK)
                        LEFT JOIN cdCommunicationTypeDesc ct WITH(NOLOCK) ON ct.CommunicationTypeCode = c.CommunicationTypeCode AND ct.LangCode = 'TR'
                        LEFT JOIN prCurrAccDefault cad WITH(NOLOCK) ON cad.CurrAccTypeCode = c.CurrAccTypeCode AND cad.CurrAccCode = c.CurrAccCode AND cad.CommunicationID = c.CommunicationID
                        WHERE c.CurrAccTypeCode = 3 AND c.CurrAccCode = @CustomerCode
                        ORDER BY c.CommunicationTypeCode";

                    var communications = await connection.QueryAsync<CustomerCommunicationResponse>(sql, new { CustomerCode = customerCode });
                    return communications?.ToList() ?? new List<CustomerCommunicationResponse>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer communications. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        public async Task<CustomerCommunicationResponse> GetCommunicationByIdAsync(string customerCode, string communicationId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            c.CommunicationID AS CommunicationId,
                            c.CurrAccTypeCode,
                            c.CurrAccCode AS CustomerCode,
                            c.CommunicationTypeCode,
                            ct.CommunicationTypeDescription,
                            c.CommAddress,
                            c.IsBlocked,
                            CASE WHEN cad.CommunicationID IS NOT NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM prCurrAccCommunication c WITH(NOLOCK)
                        LEFT JOIN cdCommunicationTypeDesc ct WITH(NOLOCK) ON ct.CommunicationTypeCode = c.CommunicationTypeCode AND ct.LangCode = 'TR'
                        LEFT JOIN prCurrAccDefault cad WITH(NOLOCK) ON cad.CurrAccTypeCode = c.CurrAccTypeCode AND cad.CurrAccCode = c.CurrAccCode AND cad.CommunicationID = c.CommunicationID
                        WHERE c.CurrAccTypeCode = 3 AND c.CurrAccCode = @CustomerCode AND c.CommunicationID = @CommunicationID";

                    var communication = await connection.QueryFirstOrDefaultAsync<CustomerCommunicationResponse>(sql, new { CustomerCode = customerCode, CommunicationID = communicationId });
                    return communication;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting communication by id. CustomerCode: {CustomerCode}, CommunicationId: {CommunicationId}", customerCode, communicationId);
                throw;
            }
        }

        public async Task<CustomerCommunicationResponse> CreateCommunicationAsync(string customerCode, CommunicationCreateRequest request)
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
                            var communicationId = Guid.NewGuid();

                            var sql = @"
                                INSERT INTO prCurrAccCommunication WITH(ROWLOCK)
                                (
                                    CommunicationID, CurrAccTypeCode, CurrAccCode, CommunicationTypeCode, CommAddress,
                                    IsBlocked, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                                )
                                VALUES
                                (
                                    @CommunicationID, 3, @CustomerCode, @CommunicationTypeCode, @CommAddress,
                                    @IsBlocked, 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE()
                                )";

                            await connection.ExecuteAsync(sql, new
                            {
                                CommunicationID = communicationId,
                                CustomerCode = customerCode,
                                request.CommunicationTypeCode,
                                request.CommAddress,
                                request.IsBlocked
                            }, transaction);

                            if (request.IsDefault)
                            {
                                var mergeDefaultSql = @"
                                    MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                                    USING (SELECT
                                        3 AS CurrAccTypeCode,
                                        @CustomerCode AS CurrAccCode,
                                        @CommunicationID AS CommunicationID
                                    ) AS source
                                    ON (target.CurrAccTypeCode = source.CurrAccTypeCode AND target.CurrAccCode = source.CurrAccCode)
                                    WHEN MATCHED THEN
                                        UPDATE SET
                                            CommunicationID = source.CommunicationID,
                                            LastUpdatedUserName = 'SYSTEM',
                                            LastUpdatedDate = GETDATE()
                                    WHEN NOT MATCHED THEN
                                        INSERT (CurrAccTypeCode, CurrAccCode, CommunicationID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                                        VALUES (source.CurrAccTypeCode, source.CurrAccCode, source.CommunicationID, 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE());";

                                await connection.ExecuteAsync(mergeDefaultSql, new
                                {
                                    CustomerCode = customerCode,
                                    CommunicationID = communicationId
                                }, transaction);
                            }

                            await transaction.CommitAsync();
                            return await GetCommunicationByIdAsync(customerCode, communicationId.ToString());
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred while creating communication for customer {CustomerCode}. Request: {@Request}", customerCode, request);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating communication for customer {CustomerCode}. Request: {@Request}", customerCode, request);
                throw;
            }
        }

        public async Task<CustomerCommunicationResponse> UpdateCommunicationAsync(string customerCode, string communicationTypeCode, CommunicationUpdateRequest request)
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
                            // Önce iletişim bilgisini bul
                            var findSql = @"
                                SELECT CommunicationID
                                FROM prCurrAccCommunication
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND CommunicationTypeCode = @CommunicationTypeCode";

                            var communicationId = await connection.ExecuteScalarAsync<Guid>(findSql, new
                            {
                                CustomerCode = customerCode,
                                CommunicationTypeCode = communicationTypeCode
                            }, transaction);

                            if (communicationId == Guid.Empty)
                            {
                                throw new Exception($"Communication not found for customer {customerCode} with communication type {communicationTypeCode}");
                            }

                            // İletişim bilgisini güncelle
                            var updateSql = @"
                                UPDATE prCurrAccCommunication SET
                                    CommAddress = @CommAddress,
                                    IsBlocked = @IsBlocked,
                                    LastUpdatedUserName = 'SYSTEM',
                                    LastUpdatedDate = GETDATE()
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND CommunicationTypeCode = @CommunicationTypeCode";

                            await connection.ExecuteAsync(updateSql, new
                            {
                                CustomerCode = customerCode,
                                CommunicationTypeCode = communicationTypeCode,
                                request.CommAddress,
                                request.IsBlocked
                            }, transaction);

                            // Varsayılan iletişim bilgisi ise güncelle
                            if (request.IsDefault)
                            {
                                var mergeDefaultSql = @"
                                    MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                                    USING (SELECT
                                        3 AS CurrAccTypeCode,
                                        @CustomerCode AS CurrAccCode,
                                        @CommunicationID AS CommunicationID
                                    ) AS source
                                    ON (target.CurrAccTypeCode = source.CurrAccTypeCode AND target.CurrAccCode = source.CurrAccCode)
                                    WHEN MATCHED THEN
                                        UPDATE SET
                                            CommunicationID = source.CommunicationID,
                                            LastUpdatedUserName = 'SYSTEM',
                                            LastUpdatedDate = GETDATE()
                                    WHEN NOT MATCHED THEN
                                        INSERT (CurrAccTypeCode, CurrAccCode, CommunicationID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                                        VALUES (source.CurrAccTypeCode, source.CurrAccCode, source.CommunicationID, 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE());";

                                await connection.ExecuteAsync(mergeDefaultSql, new
                                {
                                    CustomerCode = customerCode,
                                    CommunicationID = communicationId
                                }, transaction);
                            }

                            await transaction.CommitAsync();
                            return await GetCommunicationByIdAsync(customerCode, communicationId.ToString());
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred while updating communication. CustomerCode: {CustomerCode}, CommunicationTypeCode: {CommunicationTypeCode}, Request: {@Request}", customerCode, communicationTypeCode, request);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating communication. CustomerCode: {CustomerCode}, CommunicationTypeCode: {CommunicationTypeCode}, Request: {@Request}", customerCode, communicationTypeCode, request);
                throw;
            }
        }

        public async Task<bool> DeleteCommunicationAsync(string customerCode, string communicationTypeCode)
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
                            // Önce iletişim bilgisini bul
                            var findSql = @"
                                SELECT CommunicationID
                                FROM prCurrAccCommunication
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND CommunicationTypeCode = @CommunicationTypeCode";

                            var communicationId = await connection.ExecuteScalarAsync<Guid>(findSql, new
                            {
                                CustomerCode = customerCode,
                                CommunicationTypeCode = communicationTypeCode
                            }, transaction);

                            if (communicationId == Guid.Empty)
                            {
                                return false; // İletişim bilgisi bulunamadı
                            }

                            // Varsayılan iletişim bilgisi kaydını temizle
                            var clearDefaultSql = @"
                                UPDATE prCurrAccDefault SET
                                    CommunicationID = NULL,
                                    LastUpdatedUserName = 'SYSTEM',
                                    LastUpdatedDate = GETDATE()
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND CommunicationID = @CommunicationID";

                            await connection.ExecuteAsync(clearDefaultSql, new
                            {
                                CustomerCode = customerCode,
                                CommunicationID = communicationId
                            }, transaction);

                            // İletişim bilgisini sil
                            var deleteSql = @"
                                DELETE FROM prCurrAccCommunication
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND CommunicationTypeCode = @CommunicationTypeCode";

                            var result = await connection.ExecuteAsync(deleteSql, new
                            {
                                CustomerCode = customerCode,
                                CommunicationTypeCode = communicationTypeCode
                            }, transaction);

                            await transaction.CommitAsync();
                            return result > 0;
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred while deleting communication. CustomerCode: {CustomerCode}, CommunicationTypeCode: {CommunicationTypeCode}", customerCode, communicationTypeCode);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting communication. CustomerCode: {CustomerCode}, CommunicationTypeCode: {CommunicationTypeCode}", customerCode, communicationTypeCode);
                throw;
            }
        }

        public async Task<List<CommunicationTypeResponse>> GetCommunicationTypesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            ct.CommunicationTypeCode,
                            ctd.CommunicationTypeDescription,
                            ct.IsBlocked
                        FROM cdCommunicationType ct WITH(NOLOCK)
                        LEFT JOIN cdCommunicationTypeDesc ctd WITH(NOLOCK) ON ctd.CommunicationTypeCode = ct.CommunicationTypeCode AND ctd.LangCode = 'TR'
                        ORDER BY ct.CommunicationTypeCode";

                    var result = await connection.QueryAsync<CommunicationTypeResponse>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting communication types. Will return empty list.");
                return new List<CommunicationTypeResponse>(); // Return empty on error
            }
        }

        public async Task<CommunicationTypeResponse> GetCommunicationTypeByCodeAsync(string code)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            ct.CommunicationTypeCode,
                            ctd.CommunicationTypeDescription,
                            ct.IsBlocked
                        FROM cdCommunicationType ct WITH(NOLOCK)
                        LEFT JOIN cdCommunicationTypeDesc ctd WITH(NOLOCK) ON ctd.CommunicationTypeCode = ct.CommunicationTypeCode AND ctd.LangCode = 'TR'
                        WHERE ct.CommunicationTypeCode = @Code";

                    var result = await connection.QueryFirstOrDefaultAsync<CommunicationTypeResponse>(sql, new { Code = code });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting communication type by code. Code: {Code}", code);
                throw;
            }
        }

        public async Task<bool> AddCustomerCommunicationAsync(CustomerCommunicationCreateRequestNew request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var result = await AddCustomerCommunicationInternalAsync(request, connection, transaction);
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Müşteri iletişim bilgisi ekleme sırasında hata: {CustomerCode}", request.CustomerCode);
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Müşteri için iletişim bilgisi ekler (dahili kullanım)
        /// </summary>
        /// <param name="request">Müşteri iletişim bilgisi ekleme isteği</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        public async Task<bool> AddCustomerCommunicationInternalAsync(CustomerCommunicationCreateRequestNew request, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                _logger.LogInformation("Müşteri iletişim bilgisi ekleme işlemi başlatıldı (dahili): {CustomerCode}, {CommunicationTypeCode}", request.CustomerCode, request.CommunicationTypeCode);
                
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
                
                // İletişim ID'si oluştur
                var communicationId = request.CommunicationID ?? Guid.NewGuid();
                
                // İletişim bilgisi ekleme SQL sorgusu
                var sql = @"
                    INSERT INTO prCurrAccCommunication (
                        CommunicationID, CurrAccTypeCode, CurrAccCode, SubCurrAccID, ContactID, CommunicationTypeCode, CommAddress, 
                        CanSendAdvert, IsBlocked, IsConfirmed, FormNumber,
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    ) VALUES (
                        @CommunicationID, @CurrAccTypeCode, @CurrAccCode, @SubCurrAccID, @ContactID, @CommunicationTypeCode, @CommAddress, 
                        @CanSendAdvert, @IsBlocked, @IsConfirmed, @FormNumber,
                        @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                    )";
                
                var parameters = new
                {
                    CommunicationID = communicationId,
                    CurrAccTypeCode = currAccTypeCode,
                    CurrAccCode = request.CustomerCode,
                    SubCurrAccID = (Guid?)null, // Null olabilir
                    ContactID = (Guid?)null, // Null olabilir
                    CommunicationTypeCode = request.CommunicationTypeCode,
                    CommAddress = request.CommAddress,
                    CanSendAdvert = request.CanSendAdvert,
                    IsBlocked = request.IsBlocked,
                    IsConfirmed = request.IsConfirmed,
                    FormNumber = "",
                    CreatedUserName = request.CreatedUserName ?? "SYSTEM",
                    LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                };
                
                await connection.ExecuteAsync(sql, parameters, transaction);
                
                // Varsayılan iletişim bilgisi ayarı
                if (request.IsDefault)
                {
                    await SetDefaultCommunicationAsync(request.CustomerCode, communicationId, connection, transaction);
                }
                
                _logger.LogInformation("Müşteri iletişim bilgisi ekleme işlemi tamamlandı (dahili): {CustomerCode}, {CommunicationTypeCode}", request.CustomerCode, request.CommunicationTypeCode);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri iletişim bilgisi ekleme işlemi sırasında hata oluştu (dahili): {CustomerCode}, {CommunicationTypeCode}, SQL Hata: {SqlError}", 
                    request.CustomerCode, request.CommunicationTypeCode, ex.InnerException?.Message ?? ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// Varsayılan iletişim bilgisini ayarlar
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="communicationId">İletişim bilgisi ID</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        public async Task<bool> SetDefaultCommunicationAsync(string customerCode, Guid communicationId, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                _logger.LogInformation("Varsayılan iletişim bilgisi ayarlama işlemi başlatıldı: {CustomerCode}, {CommunicationId}", customerCode, communicationId);
                
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
                
                // Varsayılan iletişim bilgisi ayarlama SQL sorgusu
                var sql = @"
                    INSERT INTO prCurrAccDefault (
                        CurrAccTypeCode, CurrAccCode, CommunicationID, 
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    ) VALUES (
                        @CurrAccTypeCode, @CurrAccCode, @CommunicationID, 
                        @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                    )";
                
                var parameters = new
                {
                    CurrAccTypeCode = currAccTypeCode,
                    CurrAccCode = customerCode,
                    CommunicationID = communicationId,
                    CreatedUserName = "SYSTEM",
                    LastUpdatedUserName = "SYSTEM"
                };
                
                await connection.ExecuteAsync(sql, parameters, transaction);
                
                _logger.LogInformation("Varsayılan iletişim bilgisi ayarlama işlemi tamamlandı: {CustomerCode}, {CommunicationId}", customerCode, communicationId);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Varsayılan iletişim bilgisi ayarlama işlemi sırasında hata oluştu: {CustomerCode}, {CommunicationId}, Hata: {Message}", customerCode, communicationId, ex.Message);
                return false;
            }
        }
    }
}
