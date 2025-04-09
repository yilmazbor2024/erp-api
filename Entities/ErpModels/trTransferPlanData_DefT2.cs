using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTransferPlanData_DefT2")]
    public partial class trTransferPlanData_DefT2
    {
        public trTransferPlanData_DefT2()
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
        public double TargetChannelInventoryQty1 { get; set; }

        [Required]
        public double Donem_Satis { get; set; }

        [Required]
        public DateTime LastIncomingDate { get; set; }

        [Required]
        public double Merkez_Depo_Envanteri { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string InventoryLimitCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Sizes { get; set; }

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
