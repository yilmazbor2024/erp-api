using System.Collections.Generic;

namespace erp_api.Models.Responses
{
    public class OfficeListResponse
    {
        public List<OfficeResponse> Offices { get; set; } = new List<OfficeResponse>();
        public int TotalCount { get; set; }
    }
} 