using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAdjustCostOrder")]
    public partial class trAdjustCostOrder
    {
        public trAdjustCostOrder()
        {
            trAdjustCostOrderLines = new HashSet<trAdjustCostOrderLine>();
        }

        [Key]
        [Required]
        public Guid AdjustCostHeaderID { get; set; }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

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
        public virtual trOrderHeader trOrderHeader { get; set; }
        public virtual trAdjustCostHeader trAdjustCostHeader { get; set; }

        public virtual ICollection<trAdjustCostOrderLine> trAdjustCostOrderLines { get; set; }
    }
}
