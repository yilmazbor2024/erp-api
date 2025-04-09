using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTransferPlanProductQty")]
    public partial class trTransferPlanProductQty
    {
        public trTransferPlanProductQty()
        {
        }

        [Key]
        [Required]
        public Guid TransferPlanProductQtyID { get; set; }

        [Required]
        public Guid TransferPlanProductID { get; set; }

        [Required]
        public byte FromStoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FromStoreCode { get; set; }

        [Required]
        public byte ToStoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ToStoreCode { get; set; }

        [Required]
        public double TransferQty1 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [Required]
        public Guid TransferPlanID { get; set; }

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
        public virtual trTransferPlan trTransferPlan { get; set; }
        public virtual trTransferPlanProduct trTransferPlanProduct { get; set; }

    }
}
