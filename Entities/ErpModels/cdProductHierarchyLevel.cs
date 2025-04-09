using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdProductHierarchyLevel")]
    public partial class cdProductHierarchyLevel
    {
        public cdProductHierarchyLevel()
        {
            cdProductHierarchyLevelDescs = new HashSet<cdProductHierarchyLevelDesc>();
            dfProductHierarchys = new HashSet<dfProductHierarchy>();
        }

        [Key]
        [Required]
        public int ProductHierarchyLevelCode { get; set; }

        [Required]
        public int LevelNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ProductCodeParameter { get; set; }

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

        public virtual ICollection<cdProductHierarchyLevelDesc> cdProductHierarchyLevelDescs { get; set; }
        public virtual ICollection<dfProductHierarchy> dfProductHierarchys { get; set; }
    }
}
