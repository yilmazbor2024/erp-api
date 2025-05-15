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
    /// Müşteri kişi işlemleri için servis sınıfı
    /// </summary>
    public class CustomerContactService : ICustomerContactService
    {
        private readonly ILogger<CustomerContactService> _logger;
        private readonly IConfiguration _configuration;

        public CustomerContactService(ILogger<CustomerContactService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<ErpMobile.Api.Models.Responses.ContactResponse>> GetContactsAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            c.ContactID AS ContactId,
                            c.CurrAccTypeCode,
                            c.CurrAccCode AS CustomerCode,
                            c.ContactTypeCode,
                            ct.ContactTypeDescription,
                            c.TitleCode,
                            td.TitleDescription,
                            c.JobTitleCode,
                            jtd.JobTitleDescription,
                            c.FirstName,
                            c.LastName,
                            c.FirstName + ' ' + c.LastName AS Contact,
                            c.IsAuthorized,
                            c.IdentityNum,
                            c.IsBlocked,
                            CASE WHEN cad.ContactID IS NOT NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM prCurrAccContact c WITH(NOLOCK)
                        LEFT JOIN cdContactTypeDesc ct WITH(NOLOCK) ON ct.ContactTypeCode = c.ContactTypeCode AND ct.LangCode = 'TR'
                        LEFT JOIN cdTitleDesc td WITH(NOLOCK) ON td.TitleCode = c.TitleCode AND td.LangCode = 'TR'
                        LEFT JOIN cdJobTitleDesc jtd WITH(NOLOCK) ON jtd.JobTitleCode = c.JobTitleCode AND jtd.LangCode = 'TR'
                        LEFT JOIN prCurrAccDefault cad WITH(NOLOCK) ON cad.CurrAccTypeCode = c.CurrAccTypeCode AND cad.CurrAccCode = c.CurrAccCode AND cad.ContactID = c.ContactID
                        WHERE c.CurrAccTypeCode = 3 AND c.CurrAccCode = @CustomerCode
                        ORDER BY c.ContactTypeCode";

                    var contacts = await connection.QueryAsync<ErpMobile.Api.Models.Responses.ContactResponse>(sql, new { CustomerCode = customerCode });
                    return contacts?.ToList() ?? new List<ErpMobile.Api.Models.Responses.ContactResponse>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting contacts for customer. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        public async Task<ErpMobile.Api.Models.Responses.ContactResponse> GetContactByIdAsync(string customerCode, string contactId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            c.ContactID AS ContactId,
                            c.CurrAccTypeCode,
                            c.CurrAccCode AS CustomerCode,
                            c.ContactTypeCode,
                            ct.ContactTypeDescription,
                            c.TitleCode,
                            td.TitleDescription,
                            c.JobTitleCode,
                            jtd.JobTitleDescription,
                            c.FirstName,
                            c.LastName,
                            c.FirstName + ' ' + c.LastName AS Contact,
                            c.IsAuthorized,
                            c.IdentityNum,
                            c.IsBlocked,
                            CASE WHEN cad.ContactID IS NOT NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM prCurrAccContact c WITH(NOLOCK)
                        LEFT JOIN cdContactTypeDesc ct WITH(NOLOCK) ON ct.ContactTypeCode = c.ContactTypeCode AND ct.LangCode = 'TR'
                        LEFT JOIN cdTitleDesc td WITH(NOLOCK) ON td.TitleCode = c.TitleCode AND td.LangCode = 'TR'
                        LEFT JOIN cdJobTitleDesc jtd WITH(NOLOCK) ON jtd.JobTitleCode = c.JobTitleCode AND jtd.LangCode = 'TR'
                        LEFT JOIN prCurrAccDefault cad WITH(NOLOCK) ON cad.CurrAccTypeCode = c.CurrAccTypeCode AND cad.CurrAccCode = c.CurrAccCode AND cad.ContactID = c.ContactID
                        WHERE c.CurrAccTypeCode = 3 AND c.CurrAccCode = @CustomerCode AND c.ContactID = @ContactID";

                    var contact = await connection.QueryFirstOrDefaultAsync<ErpMobile.Api.Models.Responses.ContactResponse>(sql, new { CustomerCode = customerCode, ContactID = contactId });
                    return contact;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting contact by id. CustomerCode: {CustomerCode}, ContactId: {ContactId}", customerCode, contactId);
                throw;
            }
        }

        public async Task<ErpMobile.Api.Models.Responses.ContactResponse> CreateContactAsync(string customerCode, ContactCreateRequest request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                var contactId = Guid.NewGuid();

                // Split contact name into first and last name
                var nameParts = (request.Contact ?? "").Split(' ', 2); // Handle potential null
                var firstName = nameParts.Length > 0 ? nameParts[0] : "";
                var lastName = nameParts.Length > 1 ? nameParts[1] : "";

                var sql = @"
                    INSERT INTO prCurrAccContact WITH(ROWLOCK)
                    (
                        ContactID, CurrAccTypeCode, CurrAccCode, ContactTypeCode, TitleCode, JobTitleCode, FirstName, LastName,
                        IsAuthorized, IdentityNum, IsBlocked, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    )
                    VALUES
                    (
                        @ContactID, 3, @CustomerCode, @ContactTypeCode, '', '', @FirstName, @LastName, @IsDefault, '', 0,
                        @CreatedBy, GETDATE(), @CreatedBy, GETDATE()
                    )";

                await connection.ExecuteAsync(sql, new
                {
                    ContactID = contactId,
                    CustomerCode = customerCode,
                    request.ContactTypeCode,
                    FirstName = firstName,
                    LastName = lastName,
                    request.IsDefault,
                    CreatedBy = "SYSTEM"
                }, transaction);

                if (request.IsDefault)
                {
                    var mergeDefaultContactSql = @"
                        MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                        USING (SELECT
                            3 AS CurrAccTypeCode,
                            @CustomerCode AS CurrAccCode,
                            @ContactID AS ContactID
                        ) AS source
                        ON (target.CurrAccTypeCode = source.CurrAccTypeCode AND target.CurrAccCode = source.CurrAccCode)
                        WHEN MATCHED THEN
                            UPDATE SET
                                ContactID = source.ContactID,
                                LastUpdatedUserName = @CreatedBy,
                                LastUpdatedDate = GETDATE()
                        WHEN NOT MATCHED THEN
                            INSERT (CurrAccTypeCode, CurrAccCode, ContactID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                            VALUES (source.CurrAccTypeCode, source.CurrAccCode, source.ContactID, @CreatedBy, GETDATE(), @CreatedBy, GETDATE());";

                    await connection.ExecuteAsync(mergeDefaultContactSql, new
                    {
                        CustomerCode = customerCode,
                        ContactID = contactId,
                        CreatedBy = "SYSTEM"
                    }, transaction);
                }

                await transaction.CommitAsync();
                return await GetContactByIdAsync(customerCode, contactId.ToString());
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating contact for customer {CustomerCode}", customerCode);
                throw;
            }
        }

        public async Task<ErpMobile.Api.Models.Responses.ContactResponse> UpdateContactAsync(string customerCode, string contactTypeCode, ContactUpdateRequest request)
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
                            // Önce kontağı bul
                            var findSql = @"
                                SELECT ContactID
                                FROM prCurrAccContact
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND ContactTypeCode = @ContactTypeCode";

                            var contactId = await connection.ExecuteScalarAsync<Guid>(findSql, new
                            {
                                CustomerCode = customerCode,
                                ContactTypeCode = contactTypeCode
                            }, transaction);

                            if (contactId == Guid.Empty)
                            {
                                throw new Exception($"Contact not found for customer {customerCode} with contact type {contactTypeCode}");
                            }

                            // Split contact name into first and last name
                            var nameParts = (request.Contact ?? "").Split(' ', 2); // Handle potential null
                            var firstName = nameParts.Length > 0 ? nameParts[0] : "";
                            var lastName = nameParts.Length > 1 ? nameParts[1] : "";

                            // Kontağı güncelle
                            var updateSql = @"
                                UPDATE prCurrAccContact SET
                                    FirstName = @FirstName,
                                    LastName = @LastName,
                                    IsAuthorized = @IsDefault,
                                    IsBlocked = @IsBlocked,
                                    LastUpdatedUserName = 'SYSTEM',
                                    LastUpdatedDate = GETDATE()
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND ContactTypeCode = @ContactTypeCode";

                            await connection.ExecuteAsync(updateSql, new
                            {
                                CustomerCode = customerCode,
                                ContactTypeCode = contactTypeCode,
                                FirstName = firstName,
                                LastName = lastName,
                                IsDefault = request.IsDefault,
                                request.IsBlocked
                            }, transaction);

                            // Varsayılan kontak ise güncelle
                            if (request.IsDefault)
                            {
                                var mergeDefaultSql = @"
                                    MERGE prCurrAccDefault WITH (HOLDLOCK) AS target
                                    USING (SELECT
                                        3 AS CurrAccTypeCode,
                                        @CustomerCode AS CurrAccCode,
                                        @ContactID AS ContactID
                                    ) AS source
                                    ON (target.CurrAccTypeCode = source.CurrAccTypeCode AND target.CurrAccCode = source.CurrAccCode)
                                    WHEN MATCHED THEN
                                        UPDATE SET
                                            ContactID = source.ContactID,
                                            LastUpdatedUserName = 'SYSTEM',
                                            LastUpdatedDate = GETDATE()
                                    WHEN NOT MATCHED THEN
                                        INSERT (CurrAccTypeCode, CurrAccCode, ContactID, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                                        VALUES (source.CurrAccTypeCode, source.CurrAccCode, source.ContactID, 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE());";

                                await connection.ExecuteAsync(mergeDefaultSql, new
                                {
                                    CustomerCode = customerCode,
                                    ContactID = contactId
                                }, transaction);
                            }

                            await transaction.CommitAsync();
                            return await GetContactByIdAsync(customerCode, contactId.ToString());
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred while updating contact. CustomerCode: {CustomerCode}, ContactTypeCode: {ContactTypeCode}, Request: {@Request}", customerCode, contactTypeCode, request);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating contact. CustomerCode: {CustomerCode}, ContactTypeCode: {ContactTypeCode}, Request: {@Request}", customerCode, contactTypeCode, request);
                throw;
            }
        }

        public async Task<bool> DeleteContactAsync(string customerCode, string contactTypeCode)
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
                            // Önce kontağı bul
                            var findSql = @"
                                SELECT ContactID
                                FROM prCurrAccContact
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND ContactTypeCode = @ContactTypeCode";

                            var contactId = await connection.ExecuteScalarAsync<Guid>(findSql, new
                            {
                                CustomerCode = customerCode,
                                ContactTypeCode = contactTypeCode
                            }, transaction);

                            if (contactId == Guid.Empty)
                            {
                                return false; // Kontak bulunamadı
                            }

                            // Varsayılan kontak kaydını temizle
                            var clearDefaultSql = @"
                                UPDATE prCurrAccDefault SET
                                    ContactID = NULL,
                                    LastUpdatedUserName = 'SYSTEM',
                                    LastUpdatedDate = GETDATE()
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND ContactID = @ContactID";

                            await connection.ExecuteAsync(clearDefaultSql, new
                            {
                                CustomerCode = customerCode,
                                ContactID = contactId
                            }, transaction);

                            // Kontağı sil
                            var deleteSql = @"
                                DELETE FROM prCurrAccContact
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode AND ContactTypeCode = @ContactTypeCode";

                            var result = await connection.ExecuteAsync(deleteSql, new
                            {
                                CustomerCode = customerCode,
                                ContactTypeCode = contactTypeCode
                            }, transaction);

                            await transaction.CommitAsync();
                            return result > 0;
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            _logger.LogError(ex, "Error occurred while deleting contact. CustomerCode: {CustomerCode}, ContactTypeCode: {ContactTypeCode}", customerCode, contactTypeCode);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting contact. CustomerCode: {CustomerCode}, ContactTypeCode: {ContactTypeCode}", customerCode, contactTypeCode);
                throw;
            }
        }

        public async Task<List<CustomerContactResponse>> GetCustomerContactsAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            c.ContactID AS ContactId,
                            c.CurrAccTypeCode,
                            c.CurrAccCode AS CustomerCode,
                            c.ContactTypeCode,
                            ct.ContactTypeDescription,
                            c.TitleCode,
                            td.TitleDescription,
                            c.JobTitleCode,
                            jtd.JobTitleDescription,
                            c.FirstName,
                            c.LastName,
                            c.FirstName + ' ' + c.LastName AS Contact,
                            c.IsAuthorized,
                            c.IdentityNum,
                            c.IsBlocked,
                            CASE WHEN cad.ContactID IS NOT NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM prCurrAccContact c WITH(NOLOCK)
                        LEFT JOIN cdContactTypeDesc ct WITH(NOLOCK) ON ct.ContactTypeCode = c.ContactTypeCode AND ct.LangCode = 'TR'
                        LEFT JOIN cdTitleDesc td WITH(NOLOCK) ON td.TitleCode = c.TitleCode AND td.LangCode = 'TR'
                        LEFT JOIN cdJobTitleDesc jtd WITH(NOLOCK) ON jtd.JobTitleCode = c.JobTitleCode AND jtd.LangCode = 'TR'
                        LEFT JOIN prCurrAccDefault cad WITH(NOLOCK) ON cad.CurrAccTypeCode = c.CurrAccTypeCode AND cad.CurrAccCode = c.CurrAccCode AND cad.ContactID = c.ContactID
                        WHERE c.CurrAccTypeCode = 3 AND c.CurrAccCode = @CustomerCode
                        ORDER BY c.ContactTypeCode";

                    var contacts = await connection.QueryAsync<CustomerContactResponse>(sql, new { CustomerCode = customerCode });
                    return contacts.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer contacts. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        public async Task<List<ContactTypeResponse>> GetContactTypesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            ct.ContactTypeCode,
                            ctd.ContactTypeDescription,
                            ct.IsBlocked
                        FROM cdContactType ct WITH(NOLOCK)
                        LEFT JOIN cdContactTypeDesc ctd WITH(NOLOCK) ON ctd.ContactTypeCode = ct.ContactTypeCode AND ctd.LangCode = 'TR'
                        ORDER BY ct.ContactTypeCode";

                    var result = await connection.QueryAsync<ContactTypeResponse>(sql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting contact types. Will return empty list.");
                return new List<ContactTypeResponse>(); // Return empty on error
            }
        }

        public async Task<ContactTypeResponse> GetContactTypeByCodeAsync(string code)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            ct.ContactTypeCode,
                            ctd.ContactTypeDescription,
                            ct.IsBlocked
                        FROM cdContactType ct WITH(NOLOCK)
                        LEFT JOIN cdContactTypeDesc ctd WITH(NOLOCK) ON ctd.ContactTypeCode = ct.ContactTypeCode AND ctd.LangCode = 'TR'
                        WHERE ct.ContactTypeCode = @Code";

                    var result = await connection.QueryFirstOrDefaultAsync<ContactTypeResponse>(sql, new { Code = code });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting contact type by code. Code: {Code}", code);
                throw;
            }
        }

        public async Task<bool> AddCustomerContactAsync(CustomerContactCreateRequestNew request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                var sqlTransaction = (Microsoft.Data.SqlClient.SqlTransaction)transaction;
                var result = await AddCustomerContactInternalAsync(request, connection, sqlTransaction);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Müşteri kişi ekleme sırasında hata: {CustomerCode}", request.CustomerCode);
                throw;
            }
        }

        /// <summary>
        /// Müşteri için kişi bilgisi ekler (dahili kullanım)
        /// </summary>
        /// <param name="request">Müşteri kişi bilgisi ekleme isteği</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        public async Task<bool> AddCustomerContactInternalAsync(CustomerContactCreateRequestNew request, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                _logger.LogInformation("Müşteri kişi bilgisi ekleme işlemi başlatıldı (dahili): {CustomerCode}, {ContactTypeCode}", request.CustomerCode, request.ContactTypeCode);
                
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
                
                // Kişi ID'si oluştur
                var contactId = Guid.NewGuid();
                
                // Kişi bilgisi ekleme SQL sorgusu
                var sql = @"
                    INSERT INTO prCurrAccContact (
                        ContactID, CurrAccTypeCode, CurrAccCode, SubCurrAccID, ContactTypeCode, FirstName, LastName, 
                        TitleCode, JobTitleCode, IdentityNum, IsBlocked, IsAuthorized, 
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    ) VALUES (
                        @ContactID, @CurrAccTypeCode, @CurrAccCode, @SubCurrAccID, @ContactTypeCode, @FirstName, @LastName, 
                        @TitleCode, @JobTitleCode, @IdentityNum, @IsBlocked, @IsAuthorized, 
                        @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                    )";
                
                var parameters = new
                {
                    ContactID = contactId,
                    CurrAccTypeCode = currAccTypeCode,
                    CurrAccCode = request.CustomerCode,
                    SubCurrAccID = (Guid?)null, // Null olabilir
                    ContactTypeCode = request.ContactTypeCode,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    TitleCode = request.TitleCode,
                    JobTitleCode = request.JobTitleCode,
                    IdentityNum = request.IdentityNum,
                    IsBlocked = request.IsBlocked,
                    IsAuthorized = request.IsAuthorized,
                    CreatedUserName = request.CreatedUserName ?? "SYSTEM",
                    LastUpdatedUserName = request.LastUpdatedUserName ?? "SYSTEM"
                };
                
                await connection.ExecuteAsync(sql, parameters, transaction);
                
                // Varsayılan kişi bilgisi ayarı
                if (request.IsDefault)
                {
                    await SetDefaultContactAsync(request.CustomerCode, contactId, connection, transaction);
                }
                
                _logger.LogInformation("Müşteri kişi bilgisi ekleme işlemi tamamlandı (dahili): {CustomerCode}, {ContactTypeCode}", request.CustomerCode, request.ContactTypeCode);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri kişi bilgisi ekleme işlemi sırasında hata oluştu (dahili): {CustomerCode}, {ContactTypeCode}, SQL Hata: {SqlError}", 
                    request.CustomerCode, request.ContactTypeCode, ex.InnerException?.Message ?? ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// Varsayılan kişi bilgisini ayarlar
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="contactId">Kişi bilgisi ID</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        public async Task<bool> SetDefaultContactAsync(string customerCode, Guid contactId, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                _logger.LogInformation("Varsayılan kişi bilgisi ayarlama işlemi başlatıldı: {CustomerCode}, {ContactId}", customerCode, contactId);
                
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
                
                // Varsayılan kişi bilgisi ayarlama SQL sorgusu
                var sql = @"
                    INSERT INTO prCurrAccDefault (
                        CurrAccTypeCode, CurrAccCode, ContactID, 
                        CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate
                    ) VALUES (
                        @CurrAccTypeCode, @CurrAccCode, @ContactID, 
                        @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE()
                    )";
                
                var parameters = new
                {
                    CurrAccTypeCode = currAccTypeCode,
                    CurrAccCode = customerCode,
                    ContactID = contactId,
                    CreatedUserName = "SYSTEM",
                    LastUpdatedUserName = "SYSTEM"
                };
                
                await connection.ExecuteAsync(sql, parameters, transaction);
                
                _logger.LogInformation("Varsayılan kişi bilgisi ayarlama işlemi tamamlandı: {CustomerCode}, {ContactId}", customerCode, contactId);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Varsayılan kişi bilgisi ayarlama işlemi sırasında hata oluştu: {CustomerCode}, {ContactId}, Hata: {Message}", customerCode, contactId, ex.Message);
                return false;
            }
        }
    }
}
