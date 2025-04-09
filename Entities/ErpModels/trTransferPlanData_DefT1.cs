using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTransferPlanData_DefT1")]
    public partial class trTransferPlanData_DefT1
    {
        public trTransferPlanData_DefT1()
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
        public double WarehouseInventory { get; set; }

        [Required]
        public double PeriodicalSales { get; set; }

        [Required]
        public double NetSales { get; set; }

        [Required]
        public double ShipmentQty1 { get; set; }

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

        public string Sizes { get; set; }

        [Required]
        public double PeriodicalSalesAsColor { get; set; }

        [Required]
        public double NetSalesAsColor { get; set; }

        [Required]
        public float PeriodicalSalesPercentAsColor { get; set; }

        [Required]
        public float NetSalesPercentAsColor { get; set; }

        [Required]
        public double AvailableInventoryQty1AsColor { get; set; }

        [Required]
        public double TargetChannelInventoryQty1AsColor { get; set; }

        [Required]
        public DateTime FirstIncomingDateAsColor { get; set; }

        [Required]
        public DateTime LastIncomingDateAsColor { get; set; }

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
