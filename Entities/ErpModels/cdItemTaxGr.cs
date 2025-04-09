using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemTaxGr")]
    public partial class cdItemTaxGr
    {
        public cdItemTaxGr()
        {
            cdItems = new HashSet<cdItem>();
            cdItemTaxGrDescs = new HashSet<cdItemTaxGrDesc>();
            dfProductHierarchyDefaults = new HashSet<dfProductHierarchyDefault>();
            prItemTaxGrAtts = new HashSet<prItemTaxGrAtt>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemTaxGrCode { get; set; }

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

        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdItemTaxGrDesc> cdItemTaxGrDescs { get; set; }
        public virtual ICollection<dfProductHierarchyDefault> dfProductHierarchyDefaults { get; set; }
        public virtual ICollection<prItemTaxGrAtt> prItemTaxGrAtts { get; set; }
    }
}
