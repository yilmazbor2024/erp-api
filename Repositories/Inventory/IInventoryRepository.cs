using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Inventory;

namespace ErpMobile.Api.Repositories.Inventory
{
    public interface IInventoryRepository
    {
        Task<List<InventoryStockModel>> GetInventoryStockByBarcodeAsync(string barcode);
    }
}
