using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTransferPlanData_ITR")]
    public partial class trTransferPlanData_ITR
    {
        public trTransferPlanData_ITR()
        {
        }

        [Required]
        public Guid TransferPlanID { get; set; }

        [Required]
        public Guid TransferPlanProductID { get; set; }

        [Required]
        public Guid TransferPlanChannelID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool ProcessFlowDeny { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public double MinimumQty { get; set; }

        [Required]
        public double MaximumQty { get; set; }

        [Required]
        public double SalesQty1 { get; set; }

        [Required]
        public DateTime FirstIncomingDate { get; set; }

        [Required]
        public DateTime LastIncomingDate { get; set; }

        [Required]
        public double AvailableInventoryQty1 { get; set; }

        [Required]
        public double RemainingInOrderQty1 { get; set; }

        [Required]
        public double RemainingOutOrderQty1 { get; set; }

        [Required]
        public double TransferNotApprovedQty1 { get; set; }

        [Required]
        public double RemainingReserveQty1 { get; set; }

        [Required]
        public double TargetChannelInventoryQty1 { get; set; }

        [Required]
        public float InventoryTurnoverRate { get; set; }

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

    }
}
