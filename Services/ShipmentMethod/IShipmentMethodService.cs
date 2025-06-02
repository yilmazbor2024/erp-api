using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Services.ShipmentMethod
{
    public interface IShipmentMethodService
    {
        Task<ApiResponse<List<ShipmentMethodResponse>>> GetShipmentMethodsAsync();
    }
}
