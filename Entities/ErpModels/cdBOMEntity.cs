using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBOMEntity")]
    public partial class cdBOMEntity
    {
        public cdBOMEntity()
        {
            cdBOMEntityDescs = new HashSet<cdBOMEntityDesc>();
            cdBOMTemplates = new HashSet<cdBOMTemplate>();
            cdItems = new HashSet<cdItem>();
            prBOMTemplateContents = new HashSet<prBOMTemplateContent>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMEntityCode { get; set; }

        [Required]
        public byte BOMEntityLevelCode { get; set; }

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

        public virtual ICollection<cdBOMEntityDesc> cdBOMEntityDescs { get; set; }
        public virtual ICollection<cdBOMTemplate> cdBOMTemplates { get; set; }
        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<prBOMTemplateContent> prBOMTemplateContents { get; set; }
    }
}
