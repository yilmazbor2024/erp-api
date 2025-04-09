using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfProductHierarchyLevelNames")]
    public partial class dfProductHierarchyLevelNames
    {
        public dfProductHierarchyLevelNames()
        {
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel01Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel02Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel03Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel04Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel05Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel06Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel07Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel08Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel09Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductHierarchyLevel10Desc { get; set; }

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
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
