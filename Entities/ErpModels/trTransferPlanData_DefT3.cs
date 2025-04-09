using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTransferPlanData_DefT3")]
    public partial class trTransferPlanData_DefT3
    {
        public trTransferPlanData_DefT3()
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
        public double AvailableInventoryQty1 { get; set; }

        [Required]
        public double TransferNotApprovedQty1 { get; set; }

        [Required]
        public double RemainingReserveQty1 { get; set; }

        [Required]
        public double TargetChannelInventoryQty1 { get; set; }

        [Required]
        public double PeriodicalSales { get; set; }

        [Required]
        public double PeriodicalSalesPercent { get; set; }

        [Required]
        public double PeriodicalSalesAsColor { get; set; }

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
