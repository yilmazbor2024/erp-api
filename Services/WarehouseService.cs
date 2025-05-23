using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Dapper;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly ILogger<WarehouseService> _logger;
        private readonly IConfiguration _configuration;

        public WarehouseService(ILogger<WarehouseService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<WarehouseResponse>> GetWarehousesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT
                            WarehouseCode,
                            WarehouseOwnerCode,
                            WarehouseTypeCode,
                            WarehouseTypeDescription = (SELECT TOP 1 WarehouseTypeDescription FROM WarehouseType(N'TR') WHERE WarehouseTypeCode = Warehouse.WarehouseTypeCode),
                            WarehouseCategoryCode,
                            WarehouseCategoryDescription = (SELECT TOP 1 WarehouseCategoryDescription FROM WarehouseCategory(N'TR') WHERE WarehouseCategoryCode = Warehouse.WarehouseCategoryCode),
                            OfficeCode,
                            WarehouseDescription,
                            IsDefault,
                            IsBlocked
                        FROM Warehouse(N'TR')
                        WHERE IsBlocked = 0 AND WarehouseOwnerCode = 1
                        ORDER BY WarehouseDescription";

                    var warehouses = await connection.QueryAsync<WarehouseResponse>(sql);
                    return warehouses.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting warehouses");
                throw;
            }
        }

        public async Task<WarehouseResponse> GetWarehouseByCodeAsync(string warehouseCode)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT
                            WarehouseCode,
                            WarehouseOwnerCode,
                            WarehouseTypeCode,
                            WarehouseTypeDescription = (SELECT TOP 1 WarehouseTypeDescription FROM WarehouseType(N'TR') WHERE WarehouseTypeCode = Warehouse.WarehouseTypeCode),
                            WarehouseCategoryCode,
                            WarehouseCategoryDescription = (SELECT TOP 1 WarehouseCategoryDescription FROM WarehouseCategory(N'TR') WHERE WarehouseCategoryCode = Warehouse.WarehouseCategoryCode),
                            OfficeCode,
                            WarehouseDescription,
                            IsDefault,
                            IsBlocked
                        FROM Warehouse(N'TR')
                        WHERE WarehouseCode = @WarehouseCode";

                    var warehouse = await connection.QueryFirstOrDefaultAsync<WarehouseResponse>(sql, new { WarehouseCode = warehouseCode });
                    return warehouse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting warehouse by code. WarehouseCode: {WarehouseCode}", warehouseCode);
                throw;
            }
        }

        public async Task<List<ErpMobile.Api.Models.TaxOfficeResponse>> GetTaxOfficesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT
                            TaxOfficeCode,
                            CityCode,
                            TaxOfficeDescription,
                            IsBlocked
                        FROM TaxOffice(N'TR')
                        WHERE IsBlocked = 0
                        ORDER BY TaxOfficeDescription";

                    var taxOffices = await connection.QueryAsync<ErpMobile.Api.Models.TaxOfficeResponse>(sql);
                    return taxOffices.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting tax offices");
                throw;
            }
        }

        public async Task<List<OfficeResponse>> GetOfficesAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT
                            OfficeCode,
                            CompanyCode,
                            OfficeDescription,
                            IsExecutiveOffice,
                            IsBlocked
                        FROM Office(N'TR')
                        WHERE IsBlocked = 0
                        ORDER BY OfficeDescription";

                    var offices = await connection.QueryAsync<OfficeResponse>(sql);
                    return offices.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting offices");
                throw;
            }
        }
    }
} 