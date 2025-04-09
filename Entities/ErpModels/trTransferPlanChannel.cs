using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTransferPlanChannel")]
    public partial class trTransferPlanChannel
    {
        public trTransferPlanChannel()
        {
        }

        [Key]
        [Required]
        public Guid TransferPlanChannelID { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public int SortOrder { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
