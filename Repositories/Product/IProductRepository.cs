using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Product;

namespace ErpMobile.Api.Repositories.Product
{
    public interface IProductRepository
    {
        Task<List<ProductVariantModel>> GetProductVariantsByBarcodeAsync(string barcode);
        Task<List<ProductPriceListModel>> GetProductPriceListAsync(string productCode);
    }
}
