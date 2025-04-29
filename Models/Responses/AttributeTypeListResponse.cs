using System.Collections.Generic;

namespace erp_api.Models.Responses
{
    public class AttributeTypeListResponse
    {
        public List<AttributeTypeResponse> AttributeTypes { get; set; } = new List<AttributeTypeResponse>();
        public int TotalCount { get; set; }
    }

    public class AttributeTypeResponse
    {
        public string AttributeTypeCode { get; set; }
        public string AttributeTypeDescription { get; set; }
        public bool IsBlocked { get; set; }
    }
} 