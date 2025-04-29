using System.Collections.Generic;

namespace erp_api.Models.Responses
{
    public class AttributeListResponse
    {
        public List<AttributeResponse> Attributes { get; set; } = new List<AttributeResponse>();
        public int TotalCount { get; set; }
    }

    public class AttributeResponse
    {
        public string AttributeCode { get; set; }
        public string AttributeDescription { get; set; }
        public string AttributeTypeCode { get; set; }
        public bool IsBlocked { get; set; }
    }
} 