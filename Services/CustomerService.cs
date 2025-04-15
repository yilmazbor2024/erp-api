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

namespace ErpMobile.Api.Services
{
    public class CustomerService : ICustomerService
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
                    await connection.OpenAsync();

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
                            ISNULL((SELECT CustomerTypeDescription FROM bsCustomerTypeDesc WHERE bsCustomerTypeDesc.CustomerTypeCode = cdCurrAcc.CustomerTypeCode AND bsCustomerTypeDesc.LangCode = N'TR'), SPACE(0)) AS CustomerTypeDescription,
                            cdCurrAcc.PromotionGroupCode AS DiscountGroupCode,
                            ISNULL(cdPromotionGroupDesc.PromotionGroupDescription, SPACE(0)) AS DiscountGroupDescription,
                            cdCurrAcc.PaymentPlanGroupCode,
                            ISNULL(ppg.PaymentPlanGroupDesc, SPACE(0)) AS PaymentPlanGroupDescription,
                            cdCurrAcc.CompanyCode AS RegionCode,
                            SPACE(0) AS RegionDescription,
                            ISNULL(prCurrAccPostalAddress.CityCode, SPACE(0)) AS CityCode,
                            ISNULL((SELECT CityDescription FROM cdCityDesc WITH(NOLOCK) WHERE cdCityDesc.CityCode = prCurrAccPostalAddress.CityCode AND cdCityDesc.LangCode = N'TR'), SPACE(0)) AS CityDescription,
                            ISNULL(prCurrAccPostalAddress.DistrictCode, SPACE(0)) AS DistrictCode,
                            ISNULL((SELECT DistrictDescription FROM cdDistrictDesc WITH(NOLOCK) WHERE cdDistrictDesc.DistrictCode = prCurrAccPostalAddress.DistrictCode AND cdDistrictDesc.LangCode = N'TR'), SPACE(0)) AS DistrictDescription,
                            cdCurrAcc.IsBlocked
                        FROM cdCurrAcc WITH(NOLOCK)
                            LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) ON cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR'
                            LEFT OUTER JOIN cdPromotionGroupDesc WITH(NOLOCK) ON cdPromotionGroupDesc.PromotionGroupCode = cdCurrAcc.PromotionGroupCode AND cdPromotionGroupDesc.LangCode = N'TR'
                            LEFT OUTER JOIN prPaymentPlanGroup ppg WITH(NOLOCK) ON ppg.PaymentPlanGroupCode = cdCurrAcc.PaymentPlanGroupCode
                            LEFT OUTER JOIN prCurrAccDefault WITH(NOLOCK) ON prCurrAccDefault.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode AND prCurrAccDefault.CurrAccCode = cdCurrAcc.CurrAccCode
                            LEFT OUTER JOIN prCurrAccPostalAddress WITH(NOLOCK) ON prCurrAccPostalAddress.PostalAddressID = prCurrAccDefault.PostalAddressID
                        WHERE cdCurrAcc.CurrAccTypeCode = 3
                        AND cdCurrAcc.CurrAccCode = @CustomerCode";

                    var customer = await connection.QueryFirstOrDefaultAsync<CustomerDetailResponse>(sql, new { CustomerCode = customerCode });

                    if (customer == null)
                    {
                        _logger.LogWarning("Customer not found. CustomerCode: {CustomerCode}", customerCode);
                        return null;
                    }

                    // Get customer addresses
                    var addressSql = @"
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
                        WHERE CurrAccCode = @CustomerCode";

                    customer.Addresses = (await connection.QueryAsync<CustomerAddressResponse>(addressSql, new { CustomerCode = customerCode })).ToList();

                    // Get customer contacts
                    var contactSql = @"
                        SELECT 
                            ContactTypeCode,
                            Contact,
                            IsDefault
                        FROM prCurrAccContact WITH(NOLOCK)
                        WHERE CurrAccCode = @CustomerCode";

                    customer.Contacts = (await connection.QueryAsync<CustomerContactResponse>(contactSql, new { CustomerCode = customerCode })).ToList();

                    // Get customer communications
                    var communicationSql = @"
                        SELECT 
                            CommunicationTypeCode,
                            Communication,
                            IsDefault
                        FROM prCurrAccCommunication WITH(NOLOCK)
                        WHERE CurrAccCode = @CustomerCode";

                    customer.Communications = (await connection.QueryAsync<CustomerCommunicationResponse>(communicationSql, new { CustomerCode = customerCode })).ToList();

                    return customer;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer details. CustomerCode: {CustomerCode}", customerCode);
                throw;
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
                            INSERT INTO prCurrAcc WITH(ROWLOCK)
                            (
                                CurrAccCode,
                                CurrAccDesc,
                                TaxNumber,
                                TaxOffice,
                                CurrAccTypeCode,
                                DiscountGroupCode,
                                PaymentPlanGroupCode,
                                RegionCode,
                                CityCode,
                                DistrictCode,
                                IsBlocked,
                                CreatedDate,
                                CreatedBy
                            )
                            VALUES
                            (
                                @CustomerCode,
                                @CustomerName,
                                @TaxNumber,
                                @TaxOffice,
                                @CustomerTypeCode,
                                @DiscountGroupCode,
                                @PaymentPlanGroupCode,
                                @RegionCode,
                                @CityCode,
                                @DistrictCode,
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

                        // Insert Addresses
                        if (request.Addresses != null && request.Addresses.Any())
                        {
                            var addressSql = @"
                                INSERT INTO prCurrAccAddress WITH(ROWLOCK)
                                (
                                    CurrAccCode,
                                    CurrAccTypeCode,
                                    AddressTypeCode,
                                    Address,
                                    CountryCode,
                                    StateCode,
                                    CityCode,
                                    DistrictCode,
                                    PostalCode,
                                    IsDefault,
                                    IsBlocked,
                                    CreatedDate,
                                    CreatedBy
                                )
                                VALUES
                                (
                                    @CustomerCode,
                                    3,
                                    @AddressTypeCode,
                                    @Address,
                                    @CountryCode,
                                    @StateCode,
                                    @CityCode,
                                    @DistrictCode,
                                    @PostalCode,
                                    @IsDefault,
                                    @IsBlocked,
                                    GETDATE(),
                                    @CreatedBy
                                )";

                            foreach (var address in request.Addresses)
                            {
                                await connection.ExecuteAsync(addressSql, new
                                {
                                    request.CustomerCode,
                                    address.AddressTypeCode,
                                    address.Address,
                                    address.CountryCode,
                                    address.StateCode,
                                    address.CityCode,
                                    address.DistrictCode,
                                    address.PostalCode,
                                    address.IsDefault,
                                    address.IsBlocked,
                                    CreatedBy = "SYSTEM"
                                }, transaction);
                            }
                        }

                        // Insert Contacts
                        if (request.Contacts != null && request.Contacts.Any())
                        {
                            var contactSql = @"
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

                            foreach (var contact in request.Contacts)
                            {
                                await connection.ExecuteAsync(contactSql, new
                                {
                                    request.CustomerCode,
                                    contact.ContactTypeCode,
                                    contact.Contact,
                                    contact.IsDefault,
                                    CreatedBy = "SYSTEM"
                                }, transaction);
                            }
                        }

                        // Insert Communications
                        if (request.Communications != null && request.Communications.Any())
                        {
                            var communicationSql = @"
                                INSERT INTO prCurrAccCommunication WITH(ROWLOCK)
                                (
                                    CurrAccCode,
                                    CurrAccTypeCode,
                                    CommunicationTypeCode,
                                    Communication,
                                    IsDefault,
                                    CreatedDate,
                                    CreatedBy
                                )
                                VALUES
                                (
                                    @CustomerCode,
                                    3,
                                    @CommunicationTypeCode,
                                    @Communication,
                                    @IsDefault,
                                    GETDATE(),
                                    @CreatedBy
                                )";

                            foreach (var communication in request.Communications)
                            {
                                await connection.ExecuteAsync(communicationSql, new
                                {
                                    request.CustomerCode,
                                    communication.CommunicationTypeCode,
                                    communication.Communication,
                                    communication.IsDefault,
                                    CreatedBy = "SYSTEM"
                                }, transaction);
                            }
                        }

                        await transaction.CommitAsync();

                        // Return the created customer
                        return await GetCustomerByCodeAsync(request.CustomerCode);
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
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
                            Communication,
                            IsDefault
                        FROM prCurrAccCommunication WITH(NOLOCK)
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
                            AddressTypeDesc
                        FROM prAddressType WITH(NOLOCK)";

                    var addressTypes = await connection.QueryAsync<AddressTypeResponse>(sql);
                    return addressTypes.ToList();
                }
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
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        INSERT INTO prAddressType WITH(ROWLOCK)
                        (
                            AddressTypeCode,
                            AddressTypeDesc,
                            CreatedDate,
                            CreatedBy
                        )
                        VALUES
                        (
                            @AddressTypeCode,
                            @AddressTypeDesc,
                            GETDATE(),
                            @CreatedBy
                        )";

                    await connection.ExecuteAsync(sql, new
                    {
                        request.AddressTypeCode,
                        request.AddressTypeDesc,
                        CreatedBy = "SYSTEM"
                    });

                    return new AddressTypeResponse
                    {
                        AddressTypeCode = request.AddressTypeCode,
                        AddressTypeDescription = request.AddressTypeDesc
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating address type. Request: {@Request}", request);
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

                    var addresses = await connection.QueryAsync<AddressResponse>(sql, new { CustomerCode = customerCode });
                    return addresses.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer addresses. CustomerCode: {CustomerCode}", customerCode);
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
                        AND CurrAccCode = @CustomerCode
                        AND AddressTypeCode = @AddressId";

                    var address = await connection.QueryFirstOrDefaultAsync<AddressResponse>(sql, new { CustomerCode = customerCode, AddressId = addressId });
                    return address;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer address. CustomerCode: {CustomerCode}, AddressId: {AddressId}", customerCode, addressId);
                throw;
            }
        }

        public async Task<AddressResponse> CreateAddressAsync(AddressCreateRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        INSERT INTO prCurrAccAddress WITH(ROWLOCK)
                        (
                            CurrAccCode,
                            CurrAccTypeCode,
                            AddressTypeCode,
                            Address,
                            CountryCode,
                            StateCode,
                            CityCode,
                            DistrictCode,
                            PostalCode,
                            IsDefault,
                            IsBlocked,
                            CreatedDate,
                            CreatedBy
                        )
                        VALUES
                        (
                            @CustomerCode,
                            3,
                            @AddressTypeCode,
                            @Address,
                            @CountryCode,
                            @StateCode,
                            @CityCode,
                            @DistrictCode,
                            @PostalCode,
                            @IsDefault,
                            @IsBlocked,
                            GETDATE(),
                            @CreatedBy
                        )";

                    await connection.ExecuteAsync(sql, new
                    {
                        request.CustomerCode,
                        request.AddressTypeCode,
                        request.Address,
                        request.CountryCode,
                        request.StateCode,
                        request.CityCode,
                        request.DistrictCode,
                        request.PostalCode,
                        request.IsDefault,
                        request.IsBlocked,
                        CreatedBy = "SYSTEM"
                    });

                    return await GetAddressByIdAsync(request.CustomerCode, request.AddressTypeCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer address. Request: {@Request}", request);
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
                            Contact,
                            IsDefault
                        FROM prCurrAccContact WITH(NOLOCK)
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
                            Contact,
                            IsDefault
                        FROM prCurrAccContact WITH(NOLOCK)
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

        public async Task<ContactResponse> CreateContactAsync(ContactCreateRequest request)
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
                        request.CustomerCode,
                        request.ContactTypeCode,
                        request.Contact,
                        request.IsDefault,
                        CreatedBy = "SYSTEM"
                    });

                    return await GetContactByIdAsync(request.CustomerCode, request.ContactTypeCode.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating customer contact. Request: {@Request}", request);
                throw;
            }
        }

        public async Task<List<CustomerTypeResponse>> GetCustomerTypesAsync()
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                const string sql = @"
                    SELECT 
                        TypeCode = ISNULL(CurrAccTypeCode, 0),
                        TypeDescription = ISNULL(CurrAccTypeDesc, '')
                    FROM prCurrAccType WITH(NOLOCK)
                    WHERE LangCode = 'TR'
                    ORDER BY CurrAccTypeDesc";

                var result = await connection.QueryAsync<CustomerTypeResponse>(sql);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer types");
                throw;
            }
        }

        public async Task<List<CustomerDiscountGroupResponse>> GetCustomerDiscountGroupsAsync()
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                const string sql = @"
                    SELECT 
                        GroupCode = ISNULL(DiscountGroupCode, ''),
                        GroupDescription = ISNULL(DiscountGroupDesc, '')
                    FROM prDiscountGroup WITH(NOLOCK)
                    WHERE LangCode = 'TR'
                    ORDER BY DiscountGroupDesc";

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
                        GroupCode = ISNULL(PaymentPlanGroupCode, ''),
                        GroupDescription = ISNULL(PaymentPlanGroupDesc, '')
                    FROM prPaymentPlanGroup WITH(NOLOCK)
                    WHERE LangCode = 'TR'
                    ORDER BY PaymentPlanGroupDesc";

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

                    var sql = @"
                        SELECT 
                            RegionCode,
                            RegionDesc AS RegionDescription
                        FROM prRegion WITH(NOLOCK)";

                    var regions = await connection.QueryAsync<RegionResponse>(sql);
                    return regions.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting regions");
                throw;
            }
        }

        public async Task<List<CityResponse>> GetCitiesAsync(string regionCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            CityCode,
                            CityDesc AS CityDescription
                        FROM prCity WITH(NOLOCK)
                        WHERE RegionCode = @RegionCode";

                    var cities = await connection.QueryAsync<CityResponse>(sql, new { RegionCode = regionCode });
                    return cities.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting cities for region {RegionCode}", regionCode);
                throw;
            }
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
                            DistrictDesc AS DistrictDescription
                        FROM prDistrict WITH(NOLOCK)
                        WHERE CityCode = @CityCode";

                    var districts = await connection.QueryAsync<DistrictResponse>(sql, new { CityCode = cityCode });
                    return districts.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting districts for city {CityCode}", cityCode);
                throw;
            }
        }

        public async Task<List<CityResponse>> GetCitiesByRegionAsync(string regionCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            CityCode,
                            CityDesc AS CityDescription
                        FROM prCity WITH(NOLOCK)
                        WHERE RegionCode = @RegionCode";

                    var cities = await connection.QueryAsync<CityResponse>(sql, new { RegionCode = regionCode });
                    return cities.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting cities by region. RegionCode: {RegionCode}", regionCode);
                throw;
            }
        }

        public async Task<List<DistrictResponse>> GetDistrictsByCityAsync(string cityCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            DistrictCode,
                            DistrictDesc AS DistrictDescription
                        FROM prDistrict WITH(NOLOCK)
                        WHERE CityCode = @CityCode";

                    var districts = await connection.QueryAsync<DistrictResponse>(sql, new { CityCode = cityCode });
                    return districts.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting districts by city. CityCode: {CityCode}", cityCode);
                throw;
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
                            AddressTypeDesc AS AddressTypeDescription
                        FROM prAddressType WITH(NOLOCK)
                        WHERE AddressTypeCode = @AddressTypeCode";

                    var addressType = await connection.QueryFirstOrDefaultAsync<AddressTypeResponse>(sql, new { AddressTypeCode = code });
                    return addressType;
                }
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
                            c.ContactTypeCode,
                            d.ContactTypeDescription
                        FROM cdContactType c WITH(NOLOCK)
                        JOIN cdContactTypeDesc d WITH(NOLOCK) ON c.ContactTypeCode = d.ContactTypeCode
                        WHERE d.LangCode = 'TR'
                        AND c.IsBlocked = 0
                        ORDER BY d.ContactTypeDescription";

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
                            c.ContactTypeCode,
                            d.ContactTypeDescription
                        FROM cdContactType c WITH(NOLOCK)
                        JOIN cdContactTypeDesc d WITH(NOLOCK) ON c.ContactTypeCode = d.ContactTypeCode
                        WHERE d.LangCode = 'TR'
                        AND c.ContactTypeCode = @Code";

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
    }
} 