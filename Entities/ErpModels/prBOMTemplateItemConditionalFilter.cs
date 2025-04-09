using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prBOMTemplateItemConditionalFilter")]
    public partial class prBOMTemplateItemConditionalFilter
    {
        public prBOMTemplateItemConditionalFilter()
        {
        }

        [Key]
        [Required]
        public int ConditionalFilterID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMTemplateCode { get; set; }

  

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ContentEntityCode { get; set; }

        [Required]
        public int FilterID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SubContentEntityCode { get; set; }

        [Required]
        public int SubFilterID { get; set; }

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
        public virtual prBOMTemplateContent prBOMTemplateContent { get; set; }
        public virtual prBOMTemplateItemFilter prBOMTemplateItemFilter { get; set; }

    }
}
