using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpEuromessageCampaignDeliveryStatusDetail")]
    public partial class rpEuromessageCampaignDeliveryStatusDetail
    {
        public rpEuromessageCampaignDeliveryStatusDetail()
        {
        }

        [Key]
        [Required]
        public Guid BulkMailServiceProviderAccountID { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CampaignID { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Email { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DELIVERY_STATUS { get; set; }

        [Required]
        public DateTime LAST_CHANGE_TIME { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string MARKED_SPAM { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CLICK_STATUS { get; set; }

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
