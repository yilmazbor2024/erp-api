using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItemAccountGr")]
    public partial class cdItemAccountGr
    {
        public cdItemAccountGr()
        {
            cdItems = new HashSet<cdItem>();
            cdItemAccountGrDescs = new HashSet<cdItemAccountGrDesc>();
            dfProductHierarchyDefaults = new HashSet<dfProductHierarchyDefault>();
            prDiscountTypeGLAccss = new HashSet<prDiscountTypeGLAccs>();
            prItemAccountGrGLAccss = new HashSet<prItemAccountGrGLAccs>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemAccountGrCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

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

        // Navigation Properties
        public virtual bsItemType bsItemType { get; set; }

        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdItemAccountGrDesc> cdItemAccountGrDescs { get; set; }
        public virtual ICollection<dfProductHierarchyDefault> dfProductHierarchyDefaults { get; set; }
        public virtual ICollection<prDiscountTypeGLAccs> prDiscountTypeGLAccss { get; set; }
        public virtual ICollection<prItemAccountGrGLAccs> prItemAccountGrGLAccss { get; set; }
    }
}
