using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Tax;

namespace ErpMobile.Api.Services.Interfaces
{
    public interface ITaxTypeService
    {
        Task<IEnumerable<TaxTypeModel>> GetAllTaxTypesAsync();
    }
}
