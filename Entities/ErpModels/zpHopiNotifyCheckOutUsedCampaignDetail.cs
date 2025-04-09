using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiNotifyCheckOutUsedCampaignDetail")]
    public partial class zpHopiNotifyCheckOutUsedCampaignDetail
    {
        public zpHopiNotifyCheckOutUsedCampaignDetail()
        {
        }

        [Key]
        [Required]
        public Guid UsedCampaignDetailID { get; set; }

        [Required]
        public Guid NotifyCheckOutID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CampaignCode { get; set; }

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
