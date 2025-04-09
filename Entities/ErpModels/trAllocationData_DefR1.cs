using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAllocationData_DefR1")]
    public partial class trAllocationData_DefR1
    {
        public trAllocationData_DefR1()
        {
        }

        [Required]
        public Guid AllocationID { get; set; }

        [Required]
        public Guid AllocationProductID { get; set; }

        [Required]
        public Guid AllocationChannelID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public double AvailableInventoryQty1 { get; set; }

        [Required]
        public double TransferNotApprovedQty1 { get; set; }

        [Required]
        public double RemainingReserveQty1 { get; set; }

        [Required]
        public double TargetChannelInventoryQty1 { get; set; }

        [Required]
        public double PeriodicalSale { get; set; }

        [Required]
        public double TotalSalesQty1 { get; set; }

        [Required]
        public double In_Qty1 { get; set; }

        [Required]
        public double ReturnQty1 { get; set; }

        [Required]
        public double TransferQty1 { get; set; }

        [Required]
        public DateTime FirstIncomingDate { get; set; }

        [Required]
        public DateTime LastIncomingDate { get; set; }

        [Required]
        public double MinimumQty { get; set; }

        [Required]
        public double MaximumQty { get; set; }

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
