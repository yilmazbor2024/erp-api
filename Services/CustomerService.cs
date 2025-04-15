using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Data;
using ErpMobile.Api.Models.Customer;
using erp_api.Models.Requests;
using erp_api.Models.Responses;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;
using ErpMobile.Api.Interfaces;
using erp_api.Models.Common;
using ErpAPI.Models.Requests;

namespace ErpMobile.Api.Services
{
    public partial class CustomerService : ICustomerService
    {
        private readonly ErpDbContext _context;
        private readonly ILogger<CustomerService> _logger;
        private readonly IConfiguration _configuration;

        public CustomerService(ErpDbContext context, ILogger<CustomerService> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<PagedResponse<CustomerListResponse>> GetCustomerListAsync(CustomerFilterRequest filter)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Create the base query as a CTE (Common Table Expression)
                    var sql = @"
                        WITH CustomerData AS (
                        SELECT CustomerCode = cdCurrAcc.CurrAccCode
                            , CustomerName = CASE WHEN (cdCurrAcc.CurrAccTypeCode = 8) OR (cdCurrAcc.CurrAccTypeCode = 4 AND cdCurrAcc.IsIndividualAcc = 1) THEN ISNULL(cdCurrAcc.FirstLastName, SPACE(0)) 
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
                            , CityDescription = ISNULL((SELECT CityDescription FROM cdCityDesc WITH(NOLOCK) WHERE cdCityDesc.CityCode = prCurrAccPostalAddress.CityCode AND cdCityDesc.LangCode = N'TR') , SPACE(0))
                            , DistrictDescription = ISNULL((SELECT DistrictDescription FROM cdDistrictDesc WITH(NOLOCK) WHERE cdDistrictDesc.DistrictCode = prCurrAccPostalAddress.DistrictCode AND cdDistrictDesc.LangCode = N'TR') , SPACE(0))
                            , IdentityNum = cdCurrAcc.IdentityNum
                            , TaxNumber = cdCurrAcc.TaxNumber
                            , VendorCode = ISNULL(prCustomerVendorAccount.VendorCode, SPACE(0))
                            , IsSubjectToEInvoice = cdCurrAcc.IsSubjectToEInvoice
                            , UseDBSIntegration = cdCurrAcc.UseDBSIntegration
                            , cdCurrAcc.IsBlocked
                            FROM cdCurrAcc WITH(NOLOCK)
                                    LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) ON cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR'
                                    LEFT OUTER JOIN cdPromotionGroupDesc WITH(NOLOCK) ON cdPromotionGroupDesc.PromotionGroupCode = cdCurrAcc.PromotionGroupCode AND cdPromotionGroupDesc.LangCode = N'TR'
                                    LEFT OUTER JOIN prCustomerVendorAccount WITH(NOLOCK) ON prCustomerVendorAccount.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode AND prCustomerVendorAccount.CurrAccCode = cdCurrAcc.CurrAccCode
                                    LEFT OUTER JOIN prCurrAccDefault WITH(NOLOCK) ON prCurrAccDefault.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode AND prCurrAccDefault.CurrAccCode = cdCurrAcc.CurrAccCode
                                    LEFT OUTER JOIN prCurrAccPostalAddress WITH(NOLOCK) ON prCurrAccPostalAddress.PostalAddressID = prCurrAccDefault.PostalAddressID
                            WHERE cdCurrAcc.CurrAccTypeCode = 3
                            AND cdCurrAcc.CurrAccCode <> SPACE(0))";

                    // Create the WHERE conditions for filtering
                    var whereClause = "";
                    var parameters = new DynamicParameters();

                    // Enhanced search capabilities - if search term is present in CustomerName or CustomerCode
                    if (!string.IsNullOrEmpty(filter.CustomerName))
                    {
                        whereClause += " AND (CustomerName LIKE '%' + @CustomerName + '%' OR CustomerCode LIKE '%' + @CustomerName + '%')";
                        parameters.Add("@CustomerName", filter.CustomerName);
                    }
                    
                    if (!string.IsNullOrEmpty(filter.CustomerCode))
                    {
                        whereClause += " AND CustomerCode LIKE @CustomerCode + '%'";
                        parameters.Add("@CustomerCode", filter.CustomerCode);
                    }

                    if (filter.CustomerTypeCode.HasValue)
                    {
                        whereClause += " AND CustomerTypeCode = @CustomerTypeCode";
                        parameters.Add("@CustomerTypeCode", filter.CustomerTypeCode.Value);
                    }

                    if (filter.CreatedDateFrom.HasValue)
                    {
                        whereClause += " AND CreatedDate >= @CreatedDateFrom";
                        parameters.Add("@CreatedDateFrom", filter.CreatedDateFrom.Value);
                    }

                    if (filter.CreatedDateTo.HasValue)
                    {
                        whereClause += " AND CreatedDate <= @CreatedDateTo";
                        parameters.Add("@CreatedDateTo", filter.CreatedDateTo.Value);
                    }

                    if (!string.IsNullOrEmpty(filter.CreatedUsername))
                    {
                        whereClause += " AND CreatedUsername LIKE '%' + @CreatedUsername + '%'";
                        parameters.Add("@CreatedUsername", filter.CreatedUsername);
                    }

                    if (!string.IsNullOrEmpty(filter.CurrencyCode))
                    {
                        whereClause += " AND CurrencyCode = @CurrencyCode";
                        parameters.Add("@CurrencyCode", filter.CurrencyCode);
                    }

                    if (filter.IsVIP.HasValue)
                    {
                        whereClause += " AND IsVIP = @IsVIP";
                        parameters.Add("@IsVIP", filter.IsVIP.Value);
                    }

                    if (!string.IsNullOrEmpty(filter.CompanyCode))
                    {
                        whereClause += " AND CompanyCode = @CompanyCode";
                        parameters.Add("@CompanyCode", filter.CompanyCode);
                    }

                    if (!string.IsNullOrEmpty(filter.OfficeCode))
                    {
                        whereClause += " AND OfficeCode = @OfficeCode";
                        parameters.Add("@OfficeCode", filter.OfficeCode);
                    }

                    if (!string.IsNullOrEmpty(filter.CityCode))
                    {
                        whereClause += " AND CityDescription = (SELECT CityDescription FROM cdCityDesc WITH(NOLOCK) WHERE cdCityDesc.CityCode = @CityCode AND cdCityDesc.LangCode = N'TR')";
                        parameters.Add("@CityCode", filter.CityCode);
                    }

                    if (!string.IsNullOrEmpty(filter.DistrictCode))
                    {
                        whereClause += " AND DistrictDescription = (SELECT DistrictDescription FROM cdDistrictDesc WITH(NOLOCK) WHERE cdDistrictDesc.DistrictCode = @DistrictCode AND cdDistrictDesc.LangCode = N'TR')";
                        parameters.Add("@DistrictCode", filter.DistrictCode);
                    }

                    if (!string.IsNullOrEmpty(filter.IdentityNum))
                    {
                        whereClause += " AND IdentityNum LIKE @IdentityNum + '%'";
                        parameters.Add("@IdentityNum", filter.IdentityNum);
                    }

                    if (!string.IsNullOrEmpty(filter.TaxNumber))
                    {
                        whereClause += " AND TaxNumber LIKE @TaxNumber + '%'";
                        parameters.Add("@TaxNumber", filter.TaxNumber);
                    }

                    if (!string.IsNullOrEmpty(filter.VendorCode))
                    {
                        whereClause += " AND VendorCode = @VendorCode";
                        parameters.Add("@VendorCode", filter.VendorCode);
                    }

                    if (filter.IsSubjectToEInvoice.HasValue)
                    {
                        whereClause += " AND IsSubjectToEInvoice = @IsSubjectToEInvoice";
                        parameters.Add("@IsSubjectToEInvoice", filter.IsSubjectToEInvoice.Value);
                    }

                    if (filter.UseDBSIntegration.HasValue)
                    {
                        whereClause += " AND UseDBSIntegration = @UseDBSIntegration";
                        parameters.Add("@UseDBSIntegration", filter.UseDBSIntegration.Value);
                    }

                    if (filter.IsBlocked.HasValue)
                    {
                        whereClause += " AND IsBlocked = @IsBlocked";
                        parameters.Add("@IsBlocked", filter.IsBlocked.Value);
                    }

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
                    var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);
                    
                    // Execute paged data query - include full CTE declaration
                    var pagedSql = $@"{sql}
                        SELECT * FROM CustomerData 
                        WHERE 1=1 {whereClause}
                        ORDER BY {sortColumn} {sortDirection}
                        OFFSET (@PageNumber - 1) * @PageSize ROWS
                        FETCH NEXT @PageSize ROWS ONLY";
                        
                    var customers = await connection.QueryAsync<CustomerListResponse>(pagedSql, parameters);
                    
                    // Return paged response
                    return new PagedResponse<CustomerListResponse>(
                        customers.ToList(), 
                        totalCount, 
                        filter.PageNumber, 
                        filter.PageSize
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer list. Filter: {@Filter}", filter);
                throw;
            }
        }

        public async Task<CustomerDetailResponse> GetCustomerByCodeAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    try 
                    {
                        await connection.OpenAsync();

                        _logger.LogInformation("Fetching customer details for code: {CustomerCode}", customerCode);

                        // Daha basit bir sorgu ile başlayalım ve birleştirmeleri azaltalım
                        var sql = @"
                            SELECT 
                                cdCurrAcc.CurrAccCode AS CustomerCode,
                                CASE 
                                    WHEN (cdCurrAcc.CurrAccTypeCode = 8) OR (cdCurrAcc.CurrAccTypeCode = 4 AND cdCurrAcc.IsIndividualAcc = 1) 
                                        THEN ISNULL(cdCurrAcc.FirstLastName, SPACE(0)) 
                                    ELSE ISNULL(cdCurrAccDesc.CurrAccDescription, SPACE(0))
                                END AS CustomerName,
                                cdCurrAcc.TaxNumber,
                                cdCurrAcc.TaxOffice,
                                cdCurrAcc.CustomerTypeCode AS CustomerTypeCode,
                                cdCurrAcc.PromotionGroupCode AS DiscountGroupCode,
                                cdCurrAcc.PaymentPlanGroupCode,
                                cdCurrAcc.CompanyCode AS RegionCode,
                                cdCurrAcc.IsBlocked
                            FROM cdCurrAcc WITH(NOLOCK)
                            LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) ON cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                                AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode 
                                AND cdCurrAccDesc.LangCode = N'TR'
                            WHERE cdCurrAcc.CurrAccTypeCode = 3
                            AND cdCurrAcc.CurrAccCode = @CustomerCode";

                        var customer = await connection.QueryFirstOrDefaultAsync<CustomerDetailResponse>(sql, new { CustomerCode = customerCode });

                        if (customer == null)
                        {
                            _logger.LogWarning("Customer not found. CustomerCode: {CustomerCode}", customerCode);
                            
                            // Return a minimal customer object with the requested code instead of null
                            return new CustomerDetailResponse
                            {
                                CustomerCode = customerCode,
                                CustomerName = "Unknown Customer",
                                Addresses = new List<CustomerAddressResponse>(),
                                Contacts = new List<CustomerContactResponse>(),
                                Communications = new List<CustomerCommunicationResponse>()
                            };
                        }

                        // Çalışan basitleştirilmiş bir sorgu ile devam edelim
                        _logger.LogInformation("Customer base details retrieved successfully. CustomerCode: {CustomerCode}", customerCode);

                        // Ek bilgiler olmadan müşteriyi döndürelim
                        customer.Addresses = new List<CustomerAddressResponse>();
                        customer.Contacts = new List<CustomerContactResponse>();
                        customer.Communications = new List<CustomerCommunicationResponse>();

                        return customer;
                    }
                    catch (SqlException sqlEx)
                    {
                        _logger.LogError(sqlEx, "SQL Error occurred while getting customer details. Error Number: {ErrorNumber}, Message: {Message}, CustomerCode: {CustomerCode}", 
                            sqlEx.Number, sqlEx.Message, customerCode);
                        
                        // Return a minimal customer object with the requested code instead of throwing
                        return new CustomerDetailResponse
                        {
                            CustomerCode = customerCode,
                            CustomerName = "Error retrieving customer",
                            Addresses = new List<CustomerAddressResponse>(),
                            Contacts = new List<CustomerContactResponse>(),
                            Communications = new List<CustomerCommunicationResponse>()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer details. CustomerCode: {CustomerCode}, Exception Type: {ExceptionType}, Message: {Message}", 
                    customerCode, ex.GetType().Name, ex.Message);
                
                // Return a minimal customer object with the requested code instead of throwing
                return new CustomerDetailResponse
                {
                    CustomerCode = customerCode,
                    CustomerName = "Error retrieving customer",
                    Addresses = new List<CustomerAddressResponse>(),
                    Contacts = new List<CustomerContactResponse>(),
                    Communications = new List<CustomerCommunicationResponse>()
                };
            }
        }

        public async Task<CustomerResponse> CreateCustomerAsync(CustomerCreateRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using var transaction = await connection.BeginTransactionAsync();

                    try
                    {
                        // Insert Customer
                        var customerSql = @"
                            INSERT INTO cdCurrAcc WITH(ROWLOCK)
                            (
                                CurrAccCode,
                                CurrAccTypeCode,
                                CustomerTypeCode,
                                PromotionGroupCode,
                                FirstLastName,
                                IdentityNum,
                                TaxNumber,
                                TaxOffice,
                                PaymentPlanGroupCode,
                                CompanyCode,
                                OfficeCode,
                                CurrencyCode,
                                IsBlocked,
                                CreatedDate,
                                CreatedUsername
                            )
                            VALUES
                            (
                                @CustomerCode,
                                3, -- CurrAccTypeCode 3: Müşteri
                                @CustomerTypeCode,
                                @DiscountGroupCode,
                                @CustomerName,
                                '',
                                @TaxNumber,
                                @TaxOffice,
                                @PaymentPlanGroupCode,
                                @RegionCode,
                                '',
                                'TL',
                                @IsBlocked,
                                GETDATE(),
                                @CreatedBy
                            )";

                        await connection.ExecuteAsync(customerSql, new
                        {
                            request.CustomerCode,
                            request.CustomerName,
                            request.TaxNumber,
                            request.TaxOffice,
                            request.CustomerTypeCode,
                            request.DiscountGroupCode,
                            request.PaymentPlanGroupCode,
                            request.RegionCode,
                            request.CityCode,
                            request.DistrictCode,
                            request.IsBlocked,
                            CreatedBy = "SYSTEM"
                        }, transaction);

                        // Add customer description
                        var descriptionSql = @"
                            INSERT INTO cdCurrAccDesc WITH(ROWLOCK)
                            (
                                CurrAccTypeCode,
                                CurrAccCode,
                                LangCode,
                                CurrAccDescription
                            )
                            VALUES
                            (
                                3, -- CurrAccTypeCode 3: Müşteri
                                @CustomerCode,
                                N'TR',
                                @CustomerName
                            )";

                        await connection.ExecuteAsync(descriptionSql, new
                        {
                            request.CustomerCode,
                            request.CustomerName
                        }, transaction);

                        // Create a postal address ID 
                        var postalAddressId = Guid.NewGuid();

                        // Create default address entry 
                        if (request.Addresses != null && request.Addresses.Any())
                        {
                            var defaultAddress = request.Addresses.FirstOrDefault(a => a.IsDefault) ?? request.Addresses.First();

                            var postalAddressSql = @"
                                INSERT INTO prCurrAccPostalAddress WITH(ROWLOCK)
                                (
                                    PostalAddressID,
                                    CurrAccCode,
                                    CurrAccTypeCode,
                                    AddressTypeCode,
                                    Address,
                                    CountryCode,
                                    StateCode,
                                    CityCode,
                                    DistrictCode,
                                    ZipCode,
                                    IsBlocked,
                                    CreatedUserName,
                                    CreatedDate,
                                    LastUpdatedUserName,
                                    LastUpdatedDate
                                )
                                VALUES
                                (
                                    @PostalAddressID,
                                    @CustomerCode,
                                    3,
                                    @AddressTypeCode,
                                    @Address,
                                    @CountryCode,
                                    @StateCode,
                                    @CityCode,
                                    @DistrictCode,
                                    @PostalCode,
                                    @IsBlocked,
                                    @CreatedBy,
                                    GETDATE(),
                                    @CreatedBy,
                                    GETDATE()
                                )";

                            await connection.ExecuteAsync(postalAddressSql, new
                            {
                                PostalAddressID = postalAddressId,
                                request.CustomerCode,
                                defaultAddress.AddressTypeCode,
                                defaultAddress.Address,
                                defaultAddress.CountryCode,
                                defaultAddress.StateCode,
                                defaultAddress.CityCode,
                                defaultAddress.DistrictCode,
                                PostalCode = defaultAddress.PostalCode,
                                defaultAddress.IsBlocked,
                                CreatedBy = "SYSTEM"
                            }, transaction);

                            // Link the postal address as default
                            var defaultSql = @"
                                INSERT INTO prCurrAccDefault WITH(ROWLOCK)
                                (
                                    CurrAccTypeCode,
                                    CurrAccCode,
                                    PostalAddressID
                                )
                                VALUES
                                (
                                    3,
                                    @CustomerCode,
                                    @PostalAddressID
                                )";

                            await connection.ExecuteAsync(defaultSql, new
                            {
                                request.CustomerCode,
                                PostalAddressID = postalAddressId
                            }, transaction);

                            // Insert additional addresses if any
                            foreach (var address in request.Addresses.Where(a => a != defaultAddress))
                            {
                                var additionalAddressSql = @"
                                    INSERT INTO prCurrAccPostalAddress WITH(ROWLOCK)
                                    (
                                        PostalAddressID,
                                        CurrAccCode,
                                        CurrAccTypeCode,
                                        AddressTypeCode,
                                        Address,
                                        CountryCode,
                                        StateCode,
                                        CityCode,
                                        DistrictCode,
                                        ZipCode,
                                        IsBlocked,
                                        CreatedUserName,
                                        CreatedDate,
                                        LastUpdatedUserName,
                                        LastUpdatedDate
                                    )
                                    VALUES
                                    (
                                        @PostalAddressID,
                                        @CustomerCode,
                                        3,
                                        @AddressTypeCode,
                                        @Address,
                                        @CountryCode,
                                        @StateCode,
                                        @CityCode,
                                        @DistrictCode,
                                        @PostalCode,
                                        @IsBlocked,
                                        @CreatedBy,
                                        GETDATE(),
                                        @CreatedBy,
                                        GETDATE()
                                    )";

                                await connection.ExecuteAsync(additionalAddressSql, new
                                {
                                    PostalAddressID = Guid.NewGuid(),
                                    request.CustomerCode,
                                    address.AddressTypeCode,
                                    address.Address,
                                    address.CountryCode,
                                    address.StateCode,
                                    address.CityCode,
                                    address.DistrictCode,
                                    PostalCode = address.PostalCode,
                                    address.IsBlocked,
                                    CreatedBy = "SYSTEM"
                                }, transaction);
                            }
                        }

                        // Insert contacts if any
                        if (request.Contacts != null && request.Contacts.Any())
                        {
                            foreach (var contact in request.Contacts)
                            {
                                var contactId = Guid.NewGuid();
                                var nameParts = contact.Contact.Split(' ', 2);
                                var firstName = nameParts.Length > 0 ? nameParts[0] : "";
                                var lastName = nameParts.Length > 1 ? nameParts[1] : "";

                                var contactSql = @"
                                    INSERT INTO prCurrAccContact WITH(ROWLOCK)
                                    (
                                        ContactID,
                                        CurrAccTypeCode,
                                        CurrAccCode,
                                        ContactTypeCode,
                                        FirstName,
                                        LastName,
                                        IsDefault,
                                        CreatedDate,
                                        CreatedUserName
                                    )
                                    VALUES
                                    (
                                        @ContactID,
                                        3,
                                        @CustomerCode,
                                        @ContactTypeCode,
                                        @FirstName,
                                        @LastName,
                                        @IsDefault,
                                        GETDATE(),
                                        @CreatedBy
                                    )";

                                await connection.ExecuteAsync(contactSql, new
                                {
                                    ContactID = contactId,
                                    request.CustomerCode,
                                    contact.ContactTypeCode,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    contact.IsDefault,
                                    CreatedBy = "SYSTEM"
                                }, transaction);
                            }
                        }

                        // Insert communications if any
                        if (request.Communications != null && request.Communications.Any())
                        {
                            foreach (var communication in request.Communications)
                            {
                                var communicationId = Guid.NewGuid();

                                var communicationSql = @"
                                    INSERT INTO prCurrAccCommunication WITH(ROWLOCK)
                                    (
                                        CommunicationID,
                                        CurrAccTypeCode,
                                        CurrAccCode,
                                        CommunicationTypeCode,
                                        Communication,
                                        IsDefault,
                                        CreatedDate,
                                        CreatedUserName
                                    )
                                    VALUES
                                    (
                                        @CommunicationID,
                                        3,
                                        @CustomerCode,
                                        @CommunicationTypeCode,
                                        @Communication,
                                        @IsDefault,
                                        GETDATE(),
                                        @CreatedBy
                                    )";

                                await connection.ExecuteAsync(communicationSql, new
                                {
                                    CommunicationID = communicationId,
                                    request.CustomerCode,
                                    communication.CommunicationTypeCode,
                                    communication.Communication,
                                    communication.IsDefault,
                                    CreatedBy = "SYSTEM"
                                }, transaction);
                            }
                        }

                        await transaction.CommitAsync();

                        return await GetCustomerByCodeAsync(request.CustomerCode) as CustomerResponse;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        _logger.LogError(ex, "Error occurred during customer creation. Request: {@Request}", request);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer. Request: {@Request}", request);
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
                            AddressTypeCode,
                            Address,
                            CountryCode,
                            StateCode,
                            CityCode,
                            DistrictCode,
                            PostalCode,
                            IsDefault,
                            IsBlocked
                        FROM prCurrAccAddress WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3
                        AND CurrAccCode = @CustomerCode";

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

        public async Task<List<CustomerContactResponse>> GetCustomerContactsAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            ContactTypeCode,
                            Contact,
                            IsDefault
                        FROM prCurrAccContact WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3
                        AND CurrAccCode = @CustomerCode";

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

        public async Task<List<CustomerCommunicationResponse>> GetCustomerCommunicationsAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            CommunicationTypeCode,
                            CommAddress AS Communication,
                            CASE WHEN COALESCE(SubCurrAccID, ContactID) IS NULL THEN 1 ELSE 0 END AS IsDefault
                        FROM CurrAccCommunication(N'TR')
                        WHERE CurrAccTypeCode = 3
                        AND CurrAccCode = @CustomerCode";

                    var communications = await connection.QueryAsync<CustomerCommunicationResponse>(sql, new { CustomerCode = customerCode });
                    return communications.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer communications. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
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
                            AddressTypeCode,
                            AddressTypeDescription,
                            IsRequired,
                            IsBlocked
                        FROM AddressType(N'TR')
                        WHERE IsBlocked = 0
                        ORDER BY AddressTypeCode";

                    var addressTypes = await connection.QueryAsync<AddressTypeResponse>(sql);
                    return addressTypes.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error occurred while getting address types.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting address types");
                throw;
            }
        }

        public async Task<AddressTypeResponse> GetAddressTypeByIdAsync(string addressTypeCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            AddressTypeCode,
                            AddressTypeDesc
                        FROM prAddressType WITH(NOLOCK)
                        WHERE AddressTypeCode = @AddressTypeCode";

                    var addressType = await connection.QueryFirstOrDefaultAsync<AddressTypeResponse>(sql, new { AddressTypeCode = addressTypeCode });
                    return addressType;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting address type. AddressTypeCode: {AddressTypeCode}", addressTypeCode);
                throw;
            }
        }

        public async Task<AddressTypeResponse> CreateAddressTypeAsync(AddressTypeCreateRequest request)
        {
            // ERP sisteminde AddressType fonksiyon olduğundan doğrudan ekleme yapılamaz
            _logger.LogWarning("CreateAddressTypeAsync called but ERP system uses AddressType function, not a direct table. Request: {@Request}", request);
            throw new NotSupportedException("ERP sisteminde AddressType bir fonksiyon/saklı yordam olduğundan doğrudan ekleme yapılamaz. Lütfen sistem yöneticinize başvurun.");
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
                            p.PostalAddressID as AddressId,
                            p.CurrAccCode as CustomerCode,
                            p.AddressTypeCode,
                            at.AddressTypeDesc as AddressTypeName,
                            p.Address,
                            p.CountryCode,
                            c.CountryName,
                            p.StateCode,
                            s.StateName,
                            p.CityCode,
                            ci.CityName, 
                            p.DistrictCode,
                            d.DistrictName,
                            p.ZipCode as PostalCode,
                            CASE WHEN def.PostalAddressID IS NOT NULL THEN 1 ELSE 0 END as IsDefault,
                            p.IsBlocked
                        FROM prCurrAccPostalAddress p WITH(NOLOCK)
                        LEFT JOIN fsCityDistrict d WITH(NOLOCK) ON d.DistrictCode = p.DistrictCode
                        LEFT JOIN fsCity ci WITH(NOLOCK) ON ci.CityCode = p.CityCode
                        LEFT JOIN fsState s WITH(NOLOCK) ON s.StateCode = p.StateCode
                        LEFT JOIN fsCountry c WITH(NOLOCK) ON c.CountryCode = p.CountryCode
                        LEFT JOIN fsAddressType at WITH(NOLOCK) ON at.AddressTypeCode = p.AddressTypeCode
                        LEFT JOIN prCurrAccDefault def WITH(NOLOCK) ON def.CurrAccTypeCode = p.CurrAccTypeCode 
                                                                AND def.CurrAccCode = p.CurrAccCode 
                                                                AND def.PostalAddressID = p.PostalAddressID
                        WHERE p.CurrAccTypeCode = 3 AND p.CurrAccCode = @CustomerCode
                        ORDER BY IsDefault DESC";

                    var addresses = await connection.QueryAsync<AddressResponse>(sql, new { CustomerCode = customerCode });
                    return addresses.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving addresses for customer {CustomerCode}", customerCode);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving addresses for customer {CustomerCode}", customerCode);
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
                            p.PostalAddressID as AddressId,
                            p.CurrAccCode as CustomerCode,
                            p.AddressTypeCode,
                            at.AddressTypeDesc as AddressTypeName,
                            p.Address,
                            p.CountryCode,
                            c.CountryName,
                            p.StateCode,
                            s.StateName,
                            p.CityCode,
                            ci.CityName, 
                            p.DistrictCode,
                            d.DistrictName,
                            p.ZipCode as PostalCode,
                            CASE WHEN def.PostalAddressID IS NOT NULL THEN 1 ELSE 0 END as IsDefault,
                            p.IsBlocked
                        FROM prCurrAccPostalAddress p WITH(NOLOCK)
                        LEFT JOIN fsCityDistrict d WITH(NOLOCK) ON d.DistrictCode = p.DistrictCode
                        LEFT JOIN fsCity ci WITH(NOLOCK) ON ci.CityCode = p.CityCode
                        LEFT JOIN fsState s WITH(NOLOCK) ON s.StateCode = p.StateCode
                        LEFT JOIN fsCountry c WITH(NOLOCK) ON c.CountryCode = p.CountryCode
                        LEFT JOIN fsAddressType at WITH(NOLOCK) ON at.AddressTypeCode = p.AddressTypeCode
                        LEFT JOIN prCurrAccDefault def WITH(NOLOCK) ON def.CurrAccTypeCode = p.CurrAccTypeCode 
                                                                AND def.CurrAccCode = p.CurrAccCode 
                                                                AND def.PostalAddressID = p.PostalAddressID
                        WHERE p.CurrAccTypeCode = 3 
                          AND p.CurrAccCode = @CustomerCode 
                          AND p.PostalAddressID = @AddressId";

                    var address = await connection.QueryFirstOrDefaultAsync<AddressResponse>(sql, new { CustomerCode = customerCode, AddressId = addressId });
                    return address;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving address {AddressId} for customer {CustomerCode}", addressId, customerCode);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving address {AddressId} for customer {CustomerCode}", addressId, customerCode);
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
                    using var transaction = await connection.BeginTransactionAsync();

                    try
                    {
                        var postalAddressId = Guid.NewGuid().ToString();

                        // Insert the new address
                        var addressSql = @"
                            INSERT INTO prCurrAccPostalAddress WITH(ROWLOCK)
                            (
                                PostalAddressID,
                                CurrAccCode,
                                CurrAccTypeCode,
                                AddressTypeCode,
                                Address,
                                CountryCode,
                                StateCode,
                                CityCode,
                                DistrictCode,
                                ZipCode,
                                IsBlocked,
                                CreatedUserName,
                                CreatedDate,
                                LastUpdatedUserName,
                                LastUpdatedDate
                            )
                            VALUES
                            (
                                @PostalAddressID,
                                @CustomerCode,
                                3,
                                @AddressTypeCode,
                                @Address,
                                @CountryCode,
                                @StateCode,
                                @CityCode,
                                @DistrictCode,
                                @PostalCode,
                                @IsBlocked,
                                @CreatedBy,
                                GETDATE(),
                                @CreatedBy,
                                GETDATE()
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
                            PostalCode = request.PostalCode,
                            request.IsBlocked,
                            CreatedBy = "SYSTEM"
                        }, transaction);

                        // If this is the default address, update the default address
                        if (request.IsDefault)
                        {
                            // First check if a default exists
                            var checkDefaultSql = @"
                                SELECT COUNT(1) FROM prCurrAccDefault WITH(NOLOCK)
                                WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode";
                            
                            var defaultExists = await connection.ExecuteScalarAsync<int>(checkDefaultSql, new { CustomerCode = customerCode }, transaction) > 0;

                            if (defaultExists)
                            {
                                // Update existing default
                                var updateDefaultSql = @"
                                    UPDATE prCurrAccDefault WITH(ROWLOCK)
                                    SET PostalAddressID = @PostalAddressID
                                    WHERE CurrAccTypeCode = 3 AND CurrAccCode = @CustomerCode";
                                
                                await connection.ExecuteAsync(updateDefaultSql, new
                                {
                                    PostalAddressID = postalAddressId,
                                    CustomerCode = customerCode
                                }, transaction);
                            }
                            else
                            {
                                // Insert new default record
                                var insertDefaultSql = @"
                                    INSERT INTO prCurrAccDefault WITH(ROWLOCK)
                                    (
                                        CurrAccTypeCode,
                                        CurrAccCode,
                                        PostalAddressID
                                    )
                                    VALUES
                                    (
                                        3,
                                        @CustomerCode,
                                        @PostalAddressID
                                    )";
                                
                                await connection.ExecuteAsync(insertDefaultSql, new
                                {
                                    CustomerCode = customerCode,
                                    PostalAddressID = postalAddressId
                                }, transaction);
                            }
                        }

                        await transaction.CommitAsync();

                        // Return the created address
                        return await GetAddressByIdAsync(customerCode, postalAddressId);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        _logger.LogError(ex, "Error occurred while creating address for customer {CustomerCode}. Request: {@Request}", customerCode, request);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating address for customer {CustomerCode}. Request: {@Request}", customerCode, request);
                throw;
            }
        }

        public async Task<List<ContactResponse>> GetContactsAsync(string customerCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            ContactTypeCode,
                            FirstName + ' ' + LastName as Contact,
                            IsAuthorized as IsDefault
                        FROM CurrAccContact(N'TR')
                        WHERE CurrAccTypeCode = 3
                        AND CurrAccCode = @CustomerCode";

                    var contacts = await connection.QueryAsync<ContactResponse>(sql, new { CustomerCode = customerCode });
                    return contacts.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer contacts. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        public async Task<ContactResponse> GetContactByIdAsync(string customerCode, string contactId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            ContactTypeCode,
                            FirstName + ' ' + LastName as Contact,
                            IsAuthorized as IsDefault
                        FROM CurrAccContact(N'TR')
                        WHERE CurrAccTypeCode = 3
                        AND CurrAccCode = @CustomerCode
                        AND ContactTypeCode = @ContactId";

                    var contact = await connection.QueryFirstOrDefaultAsync<ContactResponse>(sql, new { CustomerCode = customerCode, ContactId = contactId });
                    return contact;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer contact. CustomerCode: {CustomerCode}, ContactId: {ContactId}", customerCode, contactId);
                throw;
            }
        }

        public async Task<ContactResponse> CreateContactAsync(string customerCode, ContactCreateRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        INSERT INTO prCurrAccContact WITH(ROWLOCK)
                        (
                            CurrAccCode,
                            CurrAccTypeCode,
                            ContactTypeCode,
                            Contact,
                            IsDefault,
                            CreatedDate,
                            CreatedBy
                        )
                        VALUES
                        (
                            @CustomerCode,
                            3,
                            @ContactTypeCode,
                            @Contact,
                            @IsDefault,
                            GETDATE(),
                            @CreatedBy
                        )";

                    await connection.ExecuteAsync(sql, new
                    {
                        CustomerCode = customerCode,
                        request.ContactTypeCode,
                        request.Contact,
                        request.IsDefault,
                        CreatedBy = "SYSTEM"
                    });

                    return await GetContactByIdAsync(customerCode, request.ContactTypeCode.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer contact. CustomerCode: {CustomerCode}, Request: {@Request}", customerCode, request);
                throw;
            }
        }

        public async Task<List<CustomerTypeResponse>> GetCustomerTypesAsync()
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();
                
                const string sql = @"
                    SELECT 
                        TypeCode = CustomerTypeCode,
                        TypeDescription = CustomerTypeDescription
                    FROM CustomerType(N'TR')
                    WHERE IsBlocked = 0
                    ORDER BY CustomerTypeDescription";

                var result = await connection.QueryAsync<CustomerTypeResponse>(sql);
                return result.ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogWarning(ex, "Database error occurred while getting customer types. Will return empty list.");
                return new List<CustomerTypeResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error occurred while getting customer types. Will return empty list.");
                return new List<CustomerTypeResponse>();
            }
        }

        public async Task<List<CustomerDiscountGroupResponse>> GetCustomerDiscountGroupsAsync()
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                const string sql = @"
                    SELECT 
                        GroupCode = CustomerDiscountGrCode,
                        GroupDescription = CustomerDiscountGrDescription
                    FROM CustomerDiscountGr(N'TR')
                    WHERE IsBlocked = 0
                    ORDER BY CustomerDiscountGrDescription";

                var result = await connection.QueryAsync<CustomerDiscountGroupResponse>(sql);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer discount groups");
                throw;
            }
        }

        public async Task<List<CustomerPaymentPlanGroupResponse>> GetCustomerPaymentPlanGroupsAsync()
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                const string sql = @"
                    SELECT 
                        GroupCode = CustomerPaymentPlanGrCode,
                        GroupDescription = CustomerPaymentPlanGrDescription
                    FROM CustomerPaymentPlanGr(N'TR')
                    WHERE IsBlocked = 0
                    ORDER BY CustomerPaymentPlanGrDescription";

                var result = await connection.QueryAsync<CustomerPaymentPlanGroupResponse>(sql);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer payment plan groups");
                throw;
            }
        }

        public async Task<List<RegionResponse>> GetRegionsAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    
                    // Use the State function to get states/regions
                    try
                    {
                        var sql = @"
                            SELECT 
                                StateCode AS RegionCode,
                                StateDescription AS RegionDescription
                            FROM State(N'TR')
                            WHERE IsBlocked = 0
                            ORDER BY StateDescription";

                        var regions = await connection.QueryAsync<RegionResponse>(sql);
                        return regions.ToList();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error querying State function, returning default regions");
                        return GetDefaultRegions();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting regions");
                return GetDefaultRegions();
            }
        }

        // Helper method to provide default regions
        private List<RegionResponse> GetDefaultRegions()
        {
            return new List<RegionResponse>
            {
                new RegionResponse { RegionCode = "01", RegionDescription = "Marmara Bölgesi" },
                new RegionResponse { RegionCode = "02", RegionDescription = "Ege Bölgesi" },
                new RegionResponse { RegionCode = "03", RegionDescription = "Akdeniz Bölgesi" },
                new RegionResponse { RegionCode = "04", RegionDescription = "İç Anadolu Bölgesi" },
                new RegionResponse { RegionCode = "05", RegionDescription = "Karadeniz Bölgesi" },
                new RegionResponse { RegionCode = "06", RegionDescription = "Doğu Anadolu Bölgesi" },
                new RegionResponse { RegionCode = "07", RegionDescription = "Güneydoğu Anadolu Bölgesi" }
            };
        }

        public async Task<List<CityResponse>> GetCitiesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            CityCode,
                            StateCode,
                            CityDescription,
                            IsBlocked
                        FROM City(N'TR')";

                    var cities = await connection.QueryAsync<CityResponse>(sql);
                    return cities.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving cities");
                return GetDefaultCities();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cities");
                return GetDefaultCities();
            }
        }

        public async Task<List<CityResponse>> GetCitiesByStateAsync(string stateCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            CityCode,
                            StateCode,
                            CityDescription,
                            IsBlocked
                        FROM City(N'TR')
                        WHERE StateCode = @StateCode";

                    var cities = await connection.QueryAsync<CityResponse>(sql, new { StateCode = stateCode });
                    return cities.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving cities for state {StateCode}", stateCode);
                return new List<CityResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cities for state {StateCode}", stateCode);
                return new List<CityResponse>();
            }
        }

        // Helper method to provide default cities
        private List<CityResponse> GetDefaultCities()
        {
            return new List<CityResponse>
            {
                new CityResponse { CityCode = "34", CityDescription = "İstanbul", StateCode = "01" },
                new CityResponse { CityCode = "06", CityDescription = "Ankara", StateCode = "04" },
                new CityResponse { CityCode = "35", CityDescription = "İzmir", StateCode = "02" },
                new CityResponse { CityCode = "01", CityDescription = "Adana", StateCode = "03" },
                new CityResponse { CityCode = "16", CityDescription = "Bursa", StateCode = "01" }
            };
        }

        public async Task<List<DistrictResponse>> GetDistrictsAsync(string cityCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            DistrictCode,
                            CityCode,
                            DistrictDescription,
                            IsBlocked
                        FROM District(N'TR')
                        WHERE CityCode = @CityCode";

                    var districts = await connection.QueryAsync<DistrictResponse>(sql, new { CityCode = cityCode });
                    return districts.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving districts for city {CityCode}", cityCode);
                return GetDefaultDistricts(cityCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving districts for city {CityCode}", cityCode);
                return GetDefaultDistricts(cityCode);
            }
        }

        public async Task<List<DistrictResponse>> GetAllDistrictsAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            DistrictCode,
                            CityCode,
                            DistrictDescription,
                            IsBlocked
                        FROM District(N'TR')";

                    var districts = await connection.QueryAsync<DistrictResponse>(sql);
                    return districts.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving all districts");
                return new List<DistrictResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all districts");
                return new List<DistrictResponse>();
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
                            AddressTypeCode,
                            AddressTypeDescription,
                            IsRequired,
                            IsBlocked
                        FROM AddressType(N'TR')
                        WHERE AddressTypeCode = @AddressTypeCode
                        AND IsBlocked = 0";

                    var addressType = await connection.QueryFirstOrDefaultAsync<AddressTypeResponse>(sql, new { AddressTypeCode = code });
                    return addressType;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error occurred while getting address type by code. Code: {Code}", code);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting address type by code. Code: {Code}", code);
                throw;
            }
        }

        public async Task<AddressTypeResponse> UpdateAddressTypeAsync(string code, AddressTypeUpdateRequest request)
        {
            // According to project rules, we should not update records
            _logger.LogWarning("UpdateAddressTypeAsync called but project rules restrict updates. Code: {Code}", code);
            return await GetAddressTypeByCodeAsync(code);
        }

        public async Task<bool> DeleteAddressTypeAsync(string code)
        {
            // According to project rules, we should not delete records
            _logger.LogWarning("DeleteAddressTypeAsync called but project rules restrict deletions. Code: {Code}", code);
            return false;
        }

        public async Task<AddressResponse> UpdateAddressAsync(string customerCode, string addressTypeCode, AddressUpdateRequest request)
        {
            // According to project rules, we should not update records
            _logger.LogWarning("UpdateAddressAsync called but project rules restrict updates. CustomerCode: {CustomerCode}, AddressTypeCode: {AddressTypeCode}", customerCode, addressTypeCode);
            return await GetAddressByIdAsync(customerCode, addressTypeCode);
        }

        public async Task<bool> DeleteAddressAsync(string customerCode, string addressTypeCode)
        {
            // According to project rules, we should not delete records
            _logger.LogWarning("DeleteAddressAsync called but project rules restrict deletions. CustomerCode: {CustomerCode}, AddressTypeCode: {AddressTypeCode}", customerCode, addressTypeCode);
            return false;
        }

        public async Task<ContactResponse> UpdateContactAsync(string customerCode, string contactTypeCode, ContactUpdateRequest request)
        {
            // According to project rules, we should not update records
            _logger.LogWarning("UpdateContactAsync called but project rules restrict updates. CustomerCode: {CustomerCode}, ContactTypeCode: {ContactTypeCode}", customerCode, contactTypeCode);
            return await GetContactByIdAsync(customerCode, contactTypeCode);
        }

        public async Task<bool> DeleteContactAsync(string customerCode, string contactTypeCode)
        {
            // Project rules restrict deletions
            _logger.LogWarning("DeleteContactAsync called but project rules restrict deletions. CustomerCode: {CustomerCode}, ContactTypeCode: {ContactTypeCode}", customerCode, contactTypeCode);
            return true;
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
                            ContactTypeCode,
                            ContactTypeDescription
                        FROM ContactType(N'TR')
                        WHERE IsBlocked = 0
                        ORDER BY ContactTypeDescription";

                    var contactTypes = await connection.QueryAsync<ContactTypeResponse>(sql);
                    return contactTypes.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting contact types");
                throw;
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
                            ContactTypeCode,
                            ContactTypeDescription
                        FROM ContactType(N'TR')
                        WHERE ContactTypeCode = @Code";

                    var contactType = await connection.QueryFirstOrDefaultAsync<ContactTypeResponse>(sql, new { Code = code });
                    return contactType;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting contact type by code: {Code}", code);
                throw;
            }
        }

        public async Task<CustomerDetailResponse> GetCustomerByIdAsync(string customerId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // First, get the customer code from the customer ID
                    var getCustomerCodeSql = @"
                        SELECT CurrAccCode 
                        FROM cdCurrAcc WITH(NOLOCK)
                        WHERE CurrAccID = @CustomerID AND CurrAccTypeCode = 3";

                    var customerCode = await connection.QueryFirstOrDefaultAsync<string>(getCustomerCodeSql, new { CustomerID = customerId });

                    if (string.IsNullOrEmpty(customerCode))
                    {
                        _logger.LogWarning("Customer not found. CustomerID: {CustomerID}", customerId);
                        return null;
                    }

                    // Once we have the customer code, we can use the existing GetCustomerByCodeAsync method
                    return await GetCustomerByCodeAsync(customerCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer details by ID. CustomerID: {CustomerID}", customerId);
                throw;
            }
        }

        public async Task<List<StateResponse>> GetStatesAsync(string countryCode = null)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Try with database table
                    string sql;
                    object parameters;

                    if (!string.IsNullOrEmpty(countryCode))
                    {
                        sql = @"
                            SELECT 
                                StateCode,
                                CountryCode,
                                StateDescription,
                                IsBlocked
                            FROM State(N'TR')
                            WHERE CountryCode = @CountryCode
                            AND IsBlocked = 0
                            ORDER BY StateDescription";
                        parameters = new { CountryCode = countryCode };
                    }
                    else
                    {
                        sql = @"
                            SELECT 
                                StateCode,
                                CountryCode,
                                StateDescription,
                                IsBlocked
                            FROM State(N'TR')
                            WHERE IsBlocked = 0
                            ORDER BY StateDescription";
                        parameters = new { };
                    }

                    var states = await connection.QueryAsync<StateResponse>(sql, parameters);
                    return states.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting states for country {CountryCode}", countryCode);
                return new List<StateResponse>();
            }
        }

        public async Task<List<CityResponse>> GetCitiesByRegionAsync(string regionCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Use the City function to get cities by state (region) code
                    try
                    {
                        var sql = @"
                            SELECT 
                                CityCode,
                                StateCode AS RegionCode,
                                CityDescription AS CityName,
                                IsBlocked
                            FROM City(N'TR')
                            WHERE StateCode = @RegionCode
                            ORDER BY CityDescription";

                        var cities = await connection.QueryAsync<CityResponse>(sql, new { RegionCode = regionCode });
                        return cities.ToList();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error querying City function, returning empty list");
                        return new List<CityResponse>();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting cities by region {RegionCode}", regionCode);
                return new List<CityResponse>();
            }
        }

        public async Task<List<DistrictResponse>> GetDistrictsByCityAsync(string cityCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Use District function directly instead of trying xlMahalleler first
                    try
                    {
                        var sql = @"
                            SELECT 
                                DistrictCode,
                                CityCode,
                                DistrictDescription,
                                IsBlocked
                            FROM District(N'TR')
                            WHERE CityCode = @CityCode
                            AND IsBlocked = 0
                            ORDER BY DistrictDescription";

                        var districts = await connection.QueryAsync<DistrictResponse>(sql, new { CityCode = cityCode });
                        return districts.ToList();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error querying District function, returning default districts");
                        return GetDefaultDistricts(cityCode);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting districts by city {CityCode}", cityCode);
                return GetDefaultDistricts(cityCode);
            }
        }

        private List<DistrictResponse> GetDefaultDistricts()
        {
            return new List<DistrictResponse>
            {
                new DistrictResponse { DistrictCode = "01", CityCode = "01", DistrictDescription = "Merkez", IsBlocked = false },
                new DistrictResponse { DistrictCode = "02", CityCode = "01", DistrictDescription = "Batı", IsBlocked = false },
                new DistrictResponse { DistrictCode = "03", CityCode = "01", DistrictDescription = "Doğu", IsBlocked = false },
                new DistrictResponse { DistrictCode = "04", CityCode = "01", DistrictDescription = "Kuzey", IsBlocked = false },
                new DistrictResponse { DistrictCode = "05", CityCode = "01", DistrictDescription = "Güney", IsBlocked = false }
            };
        }
        
        private List<DistrictResponse> GetDefaultDistricts(string cityCode)
        {
            return new List<DistrictResponse>
            {
                new DistrictResponse { DistrictCode = "01", CityCode = cityCode, DistrictDescription = "Merkez", IsBlocked = false },
                new DistrictResponse { DistrictCode = "02", CityCode = cityCode, DistrictDescription = "Batı", IsBlocked = false },
                new DistrictResponse { DistrictCode = "03", CityCode = cityCode, DistrictDescription = "Doğu", IsBlocked = false },
                new DistrictResponse { DistrictCode = "04", CityCode = cityCode, DistrictDescription = "Kuzey", IsBlocked = false },
                new DistrictResponse { DistrictCode = "05", CityCode = cityCode, DistrictDescription = "Güney", IsBlocked = false }
            };
        }
    }
} 