using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    public class OfficeListResponse
    {
        public List<OfficeResponse> Offices { get; set; } = new List<OfficeResponse>();
        public int TotalCount { get; set; }
    }
} 