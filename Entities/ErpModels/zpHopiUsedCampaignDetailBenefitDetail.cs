using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiUsedCampaignDetailBenefitDetail")]
    public partial class zpHopiUsedCampaignDetailBenefitDetail
    {
        public zpHopiUsedCampaignDetailBenefitDetail()
        {
        }

        [Key]
        [Required]
        public Guid BenefitDetailID { get; set; }

        [Required]
        public Guid UsedCampaignDetailID { get; set; }

        [Required]
        public byte Percent { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Coins { get; set; }

        public string ApplicationName { get; set; }

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
