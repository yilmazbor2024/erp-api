using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Hierarchical response for Country-State-City-District structure
    /// </summary>
    public class LocationHierarchyResponse
    {
        public string CountryCode { get; set; }
        public string CountryDescription { get; set; }
        public List<StateDto> States { get; set; }
    }

    public class StateDto
    {
        public string StateCode { get; set; }
        public string StateDescription { get; set; }
        public List<CityDto> Cities { get; set; }
    }

    public class CityDto
    {
        public string CityCode { get; set; }
        public string CityDescription { get; set; }
        public List<DistrictDto> Districts { get; set; }
    }

    public class DistrictDto
    {
        public string DistrictCode { get; set; }
        public string DistrictDescription { get; set; }
        public bool IsBlocked { get; set; }
    }
}
