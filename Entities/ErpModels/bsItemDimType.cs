using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsItemDimType")]
    public partial class bsItemDimType
    {
        public bsItemDimType()
        {
            bsItemDimTypeDescs = new HashSet<bsItemDimTypeDesc>();
            cdItems = new HashSet<cdItem>();
            cdLots = new HashSet<cdLot>();
            cdProductDimSets = new HashSet<cdProductDimSet>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
        }

        [Key]
        [Required]
        public byte ItemDimTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsItemDimTypeDesc> bsItemDimTypeDescs { get; set; }
        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdLot> cdLots { get; set; }
        public virtual ICollection<cdProductDimSet> cdProductDimSets { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
    }
}
