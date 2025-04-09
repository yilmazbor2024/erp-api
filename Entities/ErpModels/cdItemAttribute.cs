using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemAttribute")]
    public partial class cdItemAttribute
    {
        public cdItemAttribute()
        {
            cdBOMTemplateAttributes = new HashSet<cdBOMTemplateAttribute>();
            cdItemAttributeDescs = new HashSet<cdItemAttributeDesc>();
            dfProductHierarchyAttributes = new HashSet<dfProductHierarchyAttribute>();
            prItemAttributes = new HashSet<prItemAttribute>();
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

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

        public string ProductHierarchyFilter { get; set; }

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
        public virtual cdItemAttributeType cdItemAttributeType { get; set; }

        public virtual ICollection<cdBOMTemplateAttribute> cdBOMTemplateAttributes { get; set; }
        public virtual ICollection<cdItemAttributeDesc> cdItemAttributeDescs { get; set; }
        public virtual ICollection<dfProductHierarchyAttribute> dfProductHierarchyAttributes { get; set; }
        public virtual ICollection<prItemAttribute> prItemAttributes { get; set; }
    }
}
