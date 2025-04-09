using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfStoreHierarchyLevelNames")]
    public partial class dfStoreHierarchyLevelNames
    {
        public dfStoreHierarchyLevelNames()
        {
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreHierarchyLevel01Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreHierarchyLevel02Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreHierarchyLevel03Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreHierarchyLevel04Desc { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreHierarchyLevel05Desc { get; set; }

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
