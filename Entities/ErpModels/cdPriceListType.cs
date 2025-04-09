using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPriceListType")]
    public partial class cdPriceListType
    {
        public cdPriceListType()
        {
            cdPriceListTypeDescs = new HashSet<cdPriceListTypeDesc>();
            prRelationalPriceGroupss = new HashSet<prRelationalPriceGroups>();
            trPriceListHeaders = new HashSet<trPriceListHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceListTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdPriceListTypeDesc> cdPriceListTypeDescs { get; set; }
        public virtual ICollection<prRelationalPriceGroups> prRelationalPriceGroupss { get; set; }
        public virtual ICollection<trPriceListHeader> trPriceListHeaders { get; set; }
    }
}
