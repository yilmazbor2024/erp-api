using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBOMTemplateAttribute")]
    public partial class cdBOMTemplateAttribute
    {
        public cdBOMTemplateAttribute()
        {
            cdBOMTemplateAttributeDescs = new HashSet<cdBOMTemplateAttributeDesc>();
            prBOMTemplateAttributes = new HashSet<prBOMTemplateAttribute>();
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
        public byte ItemTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemAttributeCode { get; set; }

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
        public virtual cdItemAttribute cdItemAttribute { get; set; }
        public virtual cdBOMTemplateAttributeType cdBOMTemplateAttributeType { get; set; }

        public virtual ICollection<cdBOMTemplateAttributeDesc> cdBOMTemplateAttributeDescs { get; set; }
        public virtual ICollection<prBOMTemplateAttribute> prBOMTemplateAttributes { get; set; }
    }
}
