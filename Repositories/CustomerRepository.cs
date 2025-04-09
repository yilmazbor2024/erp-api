using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.Models.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ErpDbContext1 _context;

        public CustomerRepository(ErpDbContext1 context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerListDto>> GetCustomerList()
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
                WHERE cdCurrAcc.CurrAccTypeCode = 3
                AND cdCurrAcc.CurrAccCode <> SPACE(0)";

            return await _context.CustomerList.FromSqlRaw(query).ToListAsync();
        }

        public async Task<CustomerListDto> GetCustomerById(string id)
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
            return await _context.CustomerList.FromSqlRaw(query, parameters).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerTypeResult>> GetCustomerTypes(string langCode)
        {
            var query = "SELECT * FROM CustomerType(@LangCode)";
            var parameter = new SqlParameter("@LangCode", langCode);
            return await _context.Set<CustomerTypeResult>().FromSqlRaw(query, parameter).ToListAsync();
        }

        public async Task<IEnumerable<CustomerDiscountGrResult>> GetCustomerDiscountGroups(string langCode)
        {
            var query = "SELECT * FROM CustomerDiscountGr(@LangCode)";
            var parameter = new SqlParameter("@LangCode", langCode);
            return await _context.Set<CustomerDiscountGrResult>().FromSqlRaw(query, parameter).ToListAsync();
        }

        public async Task<IEnumerable<CustomerPaymentPlanGrResult>> GetCustomerPaymentPlanGroups(string langCode)
        {
            var query = "SELECT * FROM CustomerPaymentPlanGr(@LangCode)";
            var parameter = new SqlParameter("@LangCode", langCode);
            return await _context.Set<CustomerPaymentPlanGrResult>().FromSqlRaw(query, parameter).ToListAsync();
        }
    }
} 