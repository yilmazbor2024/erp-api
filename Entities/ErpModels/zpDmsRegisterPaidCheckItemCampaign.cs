using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsRegisterPaidCheckItemCampaign")]
    public partial class zpDmsRegisterPaidCheckItemCampaign
    {
        public zpDmsRegisterPaidCheckItemCampaign()
        {
        }

        [Key]
        [Required]
        public Guid RegisterPaidCheckItemCampaignID { get; set; }

        [Required]
        public Guid RegisterPaidCheckItemID { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string CampaignCode { get; set; }

        [Required]
        public decimal ProductDiscount { get; set; }

        [Required]
        public decimal ProductDiscountedPrice { get; set; }

        public string ApplicationName { get; set; }

        [Required]
        public bool IsZubizuCampaign { get; set; }

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
