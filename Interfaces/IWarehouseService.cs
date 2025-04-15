using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using erp_api.Models.Responses;

namespace erp_api.Services
{
    public interface IWarehouseService
    {
        /// <summary>
        /// Gets all warehouses.
        /// </summary>
        Task<List<WarehouseResponse>> GetWarehousesAsync();

        /// <summary>
        /// Gets a warehouse by its code.
        /// </summary>
        /// <param name="warehouseCode">The warehouse code.</param>
        Task<WarehouseResponse> GetWarehouseByCodeAsync(string warehouseCode);

        /// <summary>
        /// Gets all tax offices.
        /// </summary>
        Task<List<TaxOfficeResponse>> GetTaxOfficesAsync();

        /// <summary>
        /// Gets all offices.
        /// </summary>
        Task<List<OfficeResponse>> GetOfficesAsync();
    }
} 