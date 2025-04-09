using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfStoreHierarchy")]
    public partial class dfStoreHierarchy
    {
        public dfStoreHierarchy()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
        }

        [Key]
        [Required]
        public int StoreHierarchyID { get; set; }

        [Required]
        public int StoreHierarchyLevelCode01 { get; set; }

        [Required]
        public int StoreHierarchyLevelCode02 { get; set; }

        [Required]
        public int StoreHierarchyLevelCode03 { get; set; }

        [Required]
        public int StoreHierarchyLevelCode04 { get; set; }

        [Required]
        public int StoreHierarchyLevelCode05 { get; set; }

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
        public virtual cdStoreHierarchyLevel cdStoreHierarchyLevel { get; set; }

        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
    }
}
