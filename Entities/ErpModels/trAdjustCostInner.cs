using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAdjustCostInner")]
    public partial class trAdjustCostInner
    {
        public trAdjustCostInner()
        {
            trAdjustCostInnerLines = new HashSet<trAdjustCostInnerLine>();
        }

        [Key]
        [Required]
        public Guid AdjustCostHeaderID { get; set; }

        [Key]
        [Required]
        public Guid InnerHeaderID { get; set; }

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
        public virtual trInnerHeader trInnerHeader { get; set; }
        public virtual trAdjustCostHeader trAdjustCostHeader { get; set; }

        public virtual ICollection<trAdjustCostInnerLine> trAdjustCostInnerLines { get; set; }
    }
}
