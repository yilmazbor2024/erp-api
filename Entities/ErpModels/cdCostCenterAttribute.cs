using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCostCenterAttribute")]
    public partial class cdCostCenterAttribute
    {
        public cdCostCenterAttribute()
        {
            cdCostCenterAttributeDescs = new HashSet<cdCostCenterAttributeDesc>();
            prCostCenterAttributes = new HashSet<prCostCenterAttribute>();
        }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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

        // Navigation Properties
        public virtual cdCostCenterAttributeType cdCostCenterAttributeType { get; set; }

        public virtual ICollection<cdCostCenterAttributeDesc> cdCostCenterAttributeDescs { get; set; }
        public virtual ICollection<prCostCenterAttribute> prCostCenterAttributes { get; set; }
    }
}
