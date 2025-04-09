using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemVendorGr")]
    public partial class cdItemVendorGr
    {
        public cdItemVendorGr()
        {
            cdItems = new HashSet<cdItem>();
            cdItemVendorGrDescs = new HashSet<cdItemVendorGrDesc>();
            dfProductHierarchyDefaults = new HashSet<dfProductHierarchyDefault>();
            prItemVendorGrAtts = new HashSet<prItemVendorGrAtt>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemVendorGrCode { get; set; }

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
        public virtual ICollection<cdItemVendorGrDesc> cdItemVendorGrDescs { get; set; }
        public virtual ICollection<dfProductHierarchyDefault> dfProductHierarchyDefaults { get; set; }
        public virtual ICollection<prItemVendorGrAtt> prItemVendorGrAtts { get; set; }
    }
}
