using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Models.Tax;
using ErpMobile.Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ErpMobile.Api.Services
{
    public class TaxTypeService : ITaxTypeService
    {
        private readonly string _connectionString;

        public TaxTypeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<TaxTypeModel>> GetAllTaxTypesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                SELECT
                    bsTaxType.TaxTypeCode,
                    bsTaxType.IsBlocked,
                    LangCode = 'tr',
                    TaxTypeDescription = RTRIM(LTRIM(ISNULL(TaxTypeDescription, SPACE(0))))
                FROM bsTaxType WITH (NOLOCK)
                    LEFT OUTER JOIN bsTaxTypeDesc WITH (NOLOCK)
                        ON bsTaxTypeDesc.TaxTypeCode = bsTaxType.TaxTypeCode
                        AND bsTaxTypeDesc.LangCode = 'tr'
                ORDER BY TaxTypeDescription";

                return await connection.QueryAsync<TaxTypeModel>(query);
            }
        }
    }
}
