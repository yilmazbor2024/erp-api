using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prBOMTemplateItemFilter")]
    public partial class prBOMTemplateItemFilter
    {
        public prBOMTemplateItemFilter()
        {
            prBOMTemplateItemConditionalFilters = new HashSet<prBOMTemplateItemConditionalFilter>();
        }

        [Key]
        [Required]
        public int FilterID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMTemplateCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ContentEntityCode { get; set; }

        public string FilterString { get; set; }

        public string QtyCondition { get; set; }

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

        public virtual ICollection<prBOMTemplateItemConditionalFilter> prBOMTemplateItemConditionalFilters { get; set; }
    }
}
