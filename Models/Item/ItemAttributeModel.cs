using System;

namespace ErpMobile.Api.Models.Item
{
    public class ItemAttributeModel
    {
        public string AttributeTypeCode { get; set; }
        public string AttributeTypeDescription { get; set; }
        public string AttributeCode { get; set; }
        public string AttributeDescription { get; set; }
        public string ProductHierarchyFilter { get; set; }
        public bool IsBlocked { get; set; }
    }
}
