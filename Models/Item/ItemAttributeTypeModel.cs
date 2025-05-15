using System;

namespace ErpMobile.Api.Models.Item
{
    public class ItemAttributeTypeModel
    {
        public int ItemTypeCode { get; set; }
        public string AttributeTypeCode { get; set; }
        public string AttributeTypeDescription { get; set; }
        public string ProductHierarchyFilter { get; set; }
        public bool IsRequired { get; set; }
        public bool IsBlocked { get; set; }
    }
}
