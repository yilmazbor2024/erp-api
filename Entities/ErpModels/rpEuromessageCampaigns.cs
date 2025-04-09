using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpEuromessageCampaigns")]
    public partial class rpEuromessageCampaigns
    {
        public rpEuromessageCampaigns()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CampaignID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Name { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SUBJECT { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerTitle { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime DeliveryStart { get; set; }

        [Required]
        public DateTime DeliveryFinish { get; set; }

        [Required]
        public int TotalSent { get; set; }

        [Required]
        public int TotalRead { get; set; }

        [Required]
        public int HardBounce { get; set; }

        [Required]
        public int SoftBounce { get; set; }

        [Required]
        public int Other { get; set; }

        [Required]
        public int UniqueRead { get; set; }

        [Required]
        public int UniqueClick { get; set; }

        [Required]
        public int TotalClick { get; set; }

        [Required]
        public double ReadRatio { get; set; }

        [Required]
        public double ClickRatio { get; set; }

        [Required]
        public int HotmailJunkCount { get; set; }

        [Required]
        public int YahooJunkCount { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Lists { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Status { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Classification { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

    }
}
