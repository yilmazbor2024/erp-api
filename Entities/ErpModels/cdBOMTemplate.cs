using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBOMTemplate")]
    public partial class cdBOMTemplate
    {
        public cdBOMTemplate()
        {
            cdBOMs = new HashSet<cdBOM>();
            cdBOMTemplateDescs = new HashSet<cdBOMTemplateDesc>();
            prBOMTemplateAttributes = new HashSet<prBOMTemplateAttribute>();
            prBOMTemplateContents = new HashSet<prBOMTemplateContent>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMTemplateCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMEntityCode { get; set; }

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
        public virtual cdBOMEntity cdBOMEntity { get; set; }

        public virtual ICollection<cdBOM> cdBOMs { get; set; }
        public virtual ICollection<cdBOMTemplateDesc> cdBOMTemplateDescs { get; set; }
        public virtual ICollection<prBOMTemplateAttribute> prBOMTemplateAttributes { get; set; }
        public virtual ICollection<prBOMTemplateContent> prBOMTemplateContents { get; set; }
    }
}
