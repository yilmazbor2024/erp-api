using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfProductHierarchy")]
    public partial class dfProductHierarchy
    {
        public dfProductHierarchy()
        {
            cdItems = new HashSet<cdItem>();
            dfProductHierarchyAttributes = new HashSet<dfProductHierarchyAttribute>();
            dfProductHierarchyColorSets = new HashSet<dfProductHierarchyColorSet>();
            dfProductHierarchyDefaults = new HashSet<dfProductHierarchyDefault>();
            dfProductHierarchyDimSets = new HashSet<dfProductHierarchyDimSet>();
            prMarketPlaceProductHierarchyConverts = new HashSet<prMarketPlaceProductHierarchyConvert>();
        }

        [Key]
        [Required]
        public int ProductHierarchyID { get; set; }

        [Required]
        public int ProductHierarchyLevelCode01 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode02 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode03 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode04 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode05 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode06 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode07 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode08 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode09 { get; set; }

        [Required]
        public int ProductHierarchyLevelCode10 { get; set; }

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
        public virtual cdProductHierarchyLevel cdProductHierarchyLevel { get; set; }

        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<dfProductHierarchyAttribute> dfProductHierarchyAttributes { get; set; }
        public virtual ICollection<dfProductHierarchyColorSet> dfProductHierarchyColorSets { get; set; }
        public virtual ICollection<dfProductHierarchyDefault> dfProductHierarchyDefaults { get; set; }
        public virtual ICollection<dfProductHierarchyDimSet> dfProductHierarchyDimSets { get; set; }
        public virtual ICollection<prMarketPlaceProductHierarchyConvert> prMarketPlaceProductHierarchyConverts { get; set; }
    }
}
