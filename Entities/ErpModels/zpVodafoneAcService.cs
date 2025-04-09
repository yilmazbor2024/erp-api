using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpVodafoneAcService")]
    public partial class zpVodafoneAcService
    {
        public zpVodafoneAcService()
        {
        }

        [Key]
        [Required]
        public Guid OperationID { get; set; }

        [Required]
        public Guid TransactionID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ServiceMethod { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirmTransactionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UserName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Password { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string Msisdn { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CampaignPassword { get; set; }

        [Required]
        public decimal SaleAmount { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string StoreReferance { get; set; }

        [Required]
        public int ResultCode { get; set; }

        public string ResultDescription { get; set; }

        [Required]
        public int CampaignID { get; set; }

        [Required]
        public int OperationStatus { get; set; }

        [Required]
        public int RetryCount { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
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
