using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAllocationProductQty")]
    public partial class trAllocationProductQty
    {
        public trAllocationProductQty()
        {
        }

        [Key]
        [Required]
        public Guid AllocationProductQtyID { get; set; }

        [Required]
        public Guid AllocationChannelID { get; set; }

        [Required]
        public Guid AllocationProductID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [Required]
        public int LotQty { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

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
        public virtual trAllocationProduct trAllocationProduct { get; set; }
        public virtual trAllocationChannel trAllocationChannel { get; set; }
        public virtual cdLot cdLot { get; set; }
        public virtual trAllocation trAllocation { get; set; }

    }
}
