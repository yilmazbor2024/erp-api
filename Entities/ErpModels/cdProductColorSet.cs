using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdProductColorSet")]
    public partial class cdProductColorSet
    {
        public cdProductColorSet()
        {
            cdProductColorSetDescs = new HashSet<cdProductColorSetDesc>();
            dfProductHierarchyColorSets = new HashSet<dfProductHierarchyColorSet>();
            prProductColorSetContents = new HashSet<prProductColorSetContent>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ProductColorSetCode { get; set; }

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

        public virtual ICollection<cdProductColorSetDesc> cdProductColorSetDescs { get; set; }
        public virtual ICollection<dfProductHierarchyColorSet> dfProductHierarchyColorSets { get; set; }
        public virtual ICollection<prProductColorSetContent> prProductColorSetContents { get; set; }
    }
}
