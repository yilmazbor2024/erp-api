using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    public class ItemDimensionTypeListResponse
    {
        public List<ItemDimensionTypeResponse> DimensionTypes { get; set; } = new List<ItemDimensionTypeResponse>();
        public int TotalCount { get; set; }
    }

    public class ItemDimensionTypeResponse
    {
        public string DimensionTypeCode { get; set; }
        public string DimensionTypeDescription { get; set; }
        public int OrderIndex { get; set; }
        public bool IsBlocked { get; set; }
    }
} 