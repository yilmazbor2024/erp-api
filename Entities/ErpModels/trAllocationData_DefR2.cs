using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAllocationData_DefR2")]
    public partial class trAllocationData_DefR2
    {
        public trAllocationData_DefR2()
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

        [Required]
        public double AvailableInventoryQty1 { get; set; }

        [Required]
        public double TransferNotApprovedQty1 { get; set; }

        [Required]
        public double RemainingReserveQty1 { get; set; }

        [Required]
        public double TargetChannelInventoryQty1 { get; set; }

        [Required]
        public double TargetChannelInventoryQty1AsColor { get; set; }

        [Required]
        public double PeriodicalSales { get; set; }

        [Required]
        public double PeriodicalSalesAsColor { get; set; }

        [Required]
        public double NetSales { get; set; }

        [Required]
        public double NetSalesAsColor { get; set; }

        public string Sizes { get; set; }

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
