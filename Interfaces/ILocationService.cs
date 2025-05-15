using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;
using System.Collections.Generic;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Provides hierarchical location data: Country -> State -> City -> District
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Gets location hierarchy for given country and language
        /// </summary>
        Task<LocationHierarchyResponse> GetLocationHierarchyAsync(string langCode, string countryCode);
    }
}
