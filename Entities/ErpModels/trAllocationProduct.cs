using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAllocationProduct")]
    public partial class trAllocationProduct
    {
        public trAllocationProduct()
        {
            trAllocationProductQtys = new HashSet<trAllocationProductQty>();
        }

        [Key]
        [Required]
        public Guid AllocationProductID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte ProductTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

 
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [Required]
        public double AllocationQty1 { get; set; }

        public Guid? OrderLineID { get; set; }

        public Guid? OrderAsnLineID { get; set; }

        [Required]
        public Guid AllocationID { get; set; }

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

        // Navigation Properties
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual trOrderAsnLine trOrderAsnLine { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual trAllocation trAllocation { get; set; }
        public virtual cdColor cdColor { get; set; }

        public virtual ICollection<trAllocationProductQty> trAllocationProductQtys { get; set; }
    }
}
