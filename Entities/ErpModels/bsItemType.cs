using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsItemType")]
    public partial class bsItemType
    {
        public bsItemType()
        {
            auBasePricePermits = new HashSet<auBasePricePermit>();
            bsItemTypeDescs = new HashSet<bsItemTypeDesc>();
            cdItems = new HashSet<cdItem>();
            cdItemAccountGrs = new HashSet<cdItemAccountGr>();
            cdItemAttributeTypes = new HashSet<cdItemAttributeType>();
            cdItemOTAttributeTypes = new HashSet<cdItemOTAttributeType>();
            cdRequisitions = new HashSet<cdRequisition>();
            cdRequisitionTypes = new HashSet<cdRequisitionType>();
            dfCompanyCostOfGoodsSolds = new HashSet<dfCompanyCostOfGoodsSold>();
            dfGlobalItemSizes = new HashSet<dfGlobalItemSize>();
            prInnerProcessInfos = new HashSet<prInnerProcessInfo>();
            prInnerProcessItemTypes = new HashSet<prInnerProcessItemType>();
            prProcessInfos = new HashSet<prProcessInfo>();
            prProcessItemTypes = new HashSet<prProcessItemType>();
            srCodeNumberItems = new HashSet<srCodeNumberItem>();
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<auBasePricePermit> auBasePricePermits { get; set; }
        public virtual ICollection<bsItemTypeDesc> bsItemTypeDescs { get; set; }
        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdItemAccountGr> cdItemAccountGrs { get; set; }
        public virtual ICollection<cdItemAttributeType> cdItemAttributeTypes { get; set; }
        public virtual ICollection<cdItemOTAttributeType> cdItemOTAttributeTypes { get; set; }
        public virtual ICollection<cdRequisition> cdRequisitions { get; set; }
        public virtual ICollection<cdRequisitionType> cdRequisitionTypes { get; set; }
        public virtual ICollection<dfCompanyCostOfGoodsSold> dfCompanyCostOfGoodsSolds { get; set; }
        public virtual ICollection<dfGlobalItemSize> dfGlobalItemSizes { get; set; }
        public virtual ICollection<prInnerProcessInfo> prInnerProcessInfos { get; set; }
        public virtual ICollection<prInnerProcessItemType> prInnerProcessItemTypes { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
        public virtual ICollection<prProcessItemType> prProcessItemTypes { get; set; }
        public virtual ICollection<srCodeNumberItem> srCodeNumberItems { get; set; }
    }
}
