using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiGetHopiUserInfoCampaignSummary")]
    public partial class zpHopiGetHopiUserInfoCampaignSummary
    {
        public zpHopiGetHopiUserInfoCampaignSummary()
        {
        }

        [Key]
        [Required]
        public Guid CampaingSummaryID { get; set; }

        [Required]
        public Guid GetHopiUserInfoID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Code { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Type { get; set; }

        [Required]
        public double Multiplier { get; set; }

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
