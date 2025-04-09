using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Microsoft.Extensions.Logging;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ErpDbContext1 _context;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ErpDbContext1 context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 10, 
            [FromQuery] string? searchTerm = null, [FromQuery] string? sortField = null, [FromQuery] string? sortDirection = null)
        {
            try
            {
                var orderByClause = string.Empty;
                if (!string.IsNullOrEmpty(sortField))
                {
                    orderByClause = $"ORDER BY {sortField} {(sortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC")}";
                }
                else
                {
                    orderByClause = "ORDER BY CustomerName ASC";
                }

                var searchCondition = string.Empty;
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchCondition = @"AND (
                        cdCurrAcc.CurrAccCode LIKE @SearchTerm + '%' OR 
                        CASE 
                            WHEN (cdCurrAcc.CurrAccTypeCode = 8) OR (cdCurrAcc.CurrAccTypeCode = 4 AND cdCurrAcc.IsIndividualAcc = 1) 
                            THEN ISNULL(cdCurrAcc.FirstLastName, SPACE(0)) 
                            ELSE ISNULL(cdCurrAccDesc.CurrAccDescription, SPACE(0))
                        END LIKE '%' + @SearchTerm + '%'
                    )";
                }

                var countQuery = $@"
                    SELECT COUNT(*)
                    FROM cdCurrAcc WITH(NOLOCK)
                        LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) 
                            ON cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode 
                            AND cdCurrAccDesc.LangCode = N'TR'
                    WHERE cdCurrAcc.CurrAccTypeCode = 3
                    AND cdCurrAcc.CurrAccCode <> SPACE(0)
                    {searchCondition}";

                var parameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    parameters.Add(new SqlParameter("@SearchTerm", searchTerm));
                }

                var totalCount = await _context.Database.ExecuteSqlRawAsync(countQuery, parameters.ToArray());

                var query = $@"
                    SELECT 
                        CustomerCode = CAST(cdCurrAcc.CurrAccCode AS NVARCHAR(30)),
                        CustomerName = CASE 
                            WHEN (cdCurrAcc.CurrAccTypeCode = 8) OR (cdCurrAcc.CurrAccTypeCode = 4 AND cdCurrAcc.IsIndividualAcc = 1) 
                            THEN ISNULL(cdCurrAcc.FirstLastName, SPACE(0)) 
                            ELSE ISNULL(cdCurrAccDesc.CurrAccDescription, SPACE(0))
                        END,
                        cdCurrAcc.CustomerTypeCode,
                        CustomerTypeDescription = ISNULL((
                            SELECT CustomerTypeDescription 
                            FROM bsCustomerTypeDesc 
                            WHERE bsCustomerTypeDesc.CustomerTypeCode = cdCurrAcc.CustomerTypeCode 
                            AND bsCustomerTypeDesc.LangCode = N'TR'
                        ), SPACE(0)),
                        CreatedDate = cdCurrAcc.Createddate,
                        CreatedUsername = CAST(cdCurrAcc.CreatedUsername AS NVARCHAR(20)),
                        CurrencyCode = CAST(cdCurrAcc.CurrencyCode AS NVARCHAR(10)),
                        IsVIP = cdCurrAcc.IsVIP,
                        PromotionGroupDescription = ISNULL(cdPromotionGroupDesc.PromotionGroupDescription, SPACE(0)),
                        CompanyCode = CAST(cdCurrAcc.CompanyCode AS NVARCHAR(10)),
                        OfficeCode = CAST(cdCurrAcc.OfficeCode AS NVARCHAR(10)),
                        OfficeDescription = ISNULL((
                            SELECT OfficeDescription 
                            FROM cdOfficeDesc WITH(NOLOCK) 
                            WHERE cdOfficeDesc.OfficeCode = cdCurrAcc.OfficeCode 
                            AND cdOfficeDesc.LangCode = N'TR'
                        ), SPACE(0)),
                        OfficeCountryCode = ISNULL((
                            SELECT CAST(CountryCode AS NVARCHAR(10))
                            FROM dfOfficeDefault WITH(NOLOCK) 
                            WHERE cdCurrAcc.OfficeCode = dfOfficeDefault.OfficeCode
                        ), SPACE(0)),
                        CityDescription = ISNULL((
                            SELECT CityDescription 
                            FROM cdCityDesc WITH(NOLOCK) 
                            WHERE cdCityDesc.CityCode = prCurrAccPostalAddress.CityCode 
                            AND cdCityDesc.LangCode = N'TR'
                        ), SPACE(0)),
                        DistrictDescription = ISNULL((
                            SELECT DistrictDescription 
                            FROM cdDistrictDesc WITH(NOLOCK) 
                            WHERE cdDistrictDesc.DistrictCode = prCurrAccPostalAddress.DistrictCode 
                            AND cdDistrictDesc.LangCode = N'TR'
                        ), SPACE(0)),
                        IdentityNum = CAST(cdCurrAcc.IdentityNum AS NVARCHAR(20)),
                        TaxNumber = CAST(cdCurrAcc.TaxNumber AS NVARCHAR(20)),
                        VendorCode = ISNULL(CAST(prCustomerVendorAccount.VendorCode AS NVARCHAR(30)), SPACE(0)),
                        IsSubjectToEInvoice = cdCurrAcc.IsSubjectToEInvoice,
                        UseDBSIntegration = cdCurrAcc.UseDBSIntegration,
                        cdCurrAcc.IsBlocked
                    FROM cdCurrAcc WITH(NOLOCK)
                        LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) 
                            ON cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode 
                            AND cdCurrAccDesc.LangCode = N'TR'
                        LEFT OUTER JOIN cdPromotionGroupDesc WITH(NOLOCK) 
                            ON cdPromotionGroupDesc.PromotionGroupCode = cdCurrAcc.PromotionGroupCode 
                            AND cdPromotionGroupDesc.LangCode = N'TR'
                        LEFT OUTER JOIN prCustomerVendorAccount WITH(NOLOCK) 
                            ON prCustomerVendorAccount.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                            AND prCustomerVendorAccount.CurrAccCode = cdCurrAcc.CurrAccCode
                        LEFT OUTER JOIN prCurrAccDefault WITH(NOLOCK) 
                            ON prCurrAccDefault.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                            AND prCurrAccDefault.CurrAccCode = cdCurrAcc.CurrAccCode
                        LEFT OUTER JOIN prCurrAccPostalAddress WITH(NOLOCK) 
                            ON prCurrAccPostalAddress.PostalAddressID = prCurrAccDefault.PostalAddressID
                    WHERE cdCurrAcc.CurrAccTypeCode = 3
                    AND cdCurrAcc.CurrAccCode <> SPACE(0)
                    {searchCondition}
                    {orderByClause}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY";

                parameters.Add(new SqlParameter("@Offset", (page - 1) * pageSize));
                parameters.Add(new SqlParameter("@PageSize", pageSize));

                var customers = await _context.CustomerList.FromSqlRaw(query, parameters.ToArray()).ToListAsync();

                return Ok(new { 
                    customers = customers,
                    totalCount = totalCount
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Müşteri listesi alınırken hata oluştu: {ex.Message}");
                return StatusCode(500, "Müşteri listesi alınırken bir hata oluştu.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerListDto>> GetCustomer(string id)
        {
            try
            {
                var query = @"
                    SELECT 
                        CustomerCode = cdCurrAcc.CurrAccCode,
                        CustomerName = CASE 
                            WHEN (cdCurrAcc.CurrAccTypeCode = 8) OR (cdCurrAcc.CurrAccTypeCode = 4 AND cdCurrAcc.IsIndividualAcc = 1) 
                            THEN ISNULL(cdCurrAcc.FirstLastName, SPACE(0)) 
                            ELSE ISNULL(cdCurrAccDesc.CurrAccDescription, SPACE(0))
                        END,
                        cdCurrAcc.CustomerTypeCode,
                        CustomerTypeDescription = ISNULL((
                            SELECT CustomerTypeDescription 
                            FROM bsCustomerTypeDesc 
                            WHERE bsCustomerTypeDesc.CustomerTypeCode = cdCurrAcc.CustomerTypeCode 
                            AND bsCustomerTypeDesc.LangCode = N'TR'
                        ), SPACE(0)),
                        CreatedDate = cdCurrAcc.Createddate,
                        CreatedUsername = cdCurrAcc.CreatedUsername,
                        cdCurrAcc.CurrencyCode,
                        IsVIP,
                        PromotionGroupDescription = ISNULL(cdPromotionGroupDesc.PromotionGroupDescription, SPACE(0)),
                        cdCurrAcc.CompanyCode,
                        cdCurrAcc.OfficeCode,
                        OfficeDescription = ISNULL((
                            SELECT OfficeDescription 
                            FROM cdOfficeDesc WITH(NOLOCK) 
                            WHERE cdOfficeDesc.OfficeCode = cdCurrAcc.OfficeCode 
                            AND cdOfficeDesc.LangCode = N'TR'
                        ), SPACE(0)),
                        OfficeCountryCode = ISNULL((
                            SELECT CountryCode 
                            FROM dfOfficeDefault WITH(NOLOCK) 
                            WHERE cdCurrAcc.OfficeCode = dfOfficeDefault.OfficeCode
                        ), SPACE(0)),
                        CityDescription = ISNULL((
                            SELECT CityDescription 
                            FROM cdCityDesc WITH(NOLOCK) 
                            WHERE cdCityDesc.CityCode = prCurrAccPostalAddress.CityCode 
                            AND cdCityDesc.LangCode = N'TR'
                        ), SPACE(0)),
                        DistrictDescription = ISNULL((
                            SELECT DistrictDescription 
                            FROM cdDistrictDesc WITH(NOLOCK) 
                            WHERE cdDistrictDesc.DistrictCode = prCurrAccPostalAddress.DistrictCode 
                            AND cdDistrictDesc.LangCode = N'TR'
                        ), SPACE(0)),
                        IdentityNum = cdCurrAcc.IdentityNum,
                        TaxNumber = cdCurrAcc.TaxNumber,
                        VendorCode = ISNULL(prCustomerVendorAccount.VendorCode, SPACE(0)),
                        IsSubjectToEInvoice = cdCurrAcc.IsSubjectToEInvoice,
                        UseDBSIntegration = cdCurrAcc.UseDBSIntegration,
                        cdCurrAcc.IsBlocked
                    FROM cdCurrAcc WITH(NOLOCK)
                        LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) 
                            ON cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode 
                            AND cdCurrAccDesc.LangCode = N'TR'
                        LEFT OUTER JOIN cdPromotionGroupDesc WITH(NOLOCK) 
                            ON cdPromotionGroupDesc.PromotionGroupCode = cdCurrAcc.PromotionGroupCode 
                            AND cdPromotionGroupDesc.LangCode = N'TR'
                        LEFT OUTER JOIN prCustomerVendorAccount WITH(NOLOCK) 
                            ON prCustomerVendorAccount.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                            AND prCustomerVendorAccount.CurrAccCode = cdCurrAcc.CurrAccCode
                        LEFT OUTER JOIN prCurrAccDefault WITH(NOLOCK) 
                            ON prCurrAccDefault.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                            AND prCurrAccDefault.CurrAccCode = cdCurrAcc.CurrAccCode
                        LEFT OUTER JOIN prCurrAccPostalAddress WITH(NOLOCK) 
                            ON prCurrAccPostalAddress.PostalAddressID = prCurrAccDefault.PostalAddressID
                    WHERE cdCurrAcc.CurrAccCode = @CustomerCode";

                var parameters = new[] { new SqlParameter("@CustomerCode", id) };
                var customer = await _context.CustomerList.FromSqlRaw(query, parameters).FirstOrDefaultAsync();

                if (customer == null)
                {
                    return NotFound($"Müşteri bulunamadı: {id}");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Müşteri detayı alınırken hata oluştu: {ex.Message}");
                return StatusCode(500, "Müşteri detayı alınırken bir hata oluştu.");
            }
        }
    }
} 