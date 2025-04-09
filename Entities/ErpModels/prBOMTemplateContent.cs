using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prBOMTemplateContent")]
    public partial class prBOMTemplateContent
    {
        public prBOMTemplateContent()
        {
            prBOMTemplateItemConditionalFilters = new HashSet<prBOMTemplateItemConditionalFilter>();
            prBOMTemplateItemFilters = new HashSet<prBOMTemplateItemFilter>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMTemplateCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ContentEntityCode { get; set; }

        [Required]
        public bool IsOptional { get; set; }

        [Required]
        public byte SortOrder { get; set; }

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
        public virtual cdBOMTemplate cdBOMTemplate { get; set; }
        public virtual cdBOMEntity cdBOMEntity { get; set; }

        public virtual ICollection<prBOMTemplateItemConditionalFilter> prBOMTemplateItemConditionalFilters { get; set; }
        public virtual ICollection<prBOMTemplateItemFilter> prBOMTemplateItemFilters { get; set; }
    }
}
