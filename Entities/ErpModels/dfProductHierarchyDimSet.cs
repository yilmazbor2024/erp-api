using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfProductHierarchyDimSet")]
    public partial class dfProductHierarchyDimSet
    {
        public dfProductHierarchyDimSet()
        {
        }

        [Key]
        [Required]
        public int ProductHierarchyID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ProductDimSetCode { get; set; }

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
        public virtual cdProductDimSet cdProductDimSet { get; set; }
        public virtual dfProductHierarchy dfProductHierarchy { get; set; }

    }
}
