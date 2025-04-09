using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsGetCustomerByCodeBenefits")]
    public partial class zpDmsGetCustomerByCodeBenefits
    {
        public zpDmsGetCustomerByCodeBenefits()
        {
        }

        [Key]
        [Required]
        public Guid BenefitID { get; set; }

        [Required]
        public Guid GetCustomerByCodeID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CampaignCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExternalCampaignCode { get; set; }

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
