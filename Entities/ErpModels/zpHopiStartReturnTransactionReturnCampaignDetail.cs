using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiStartReturnTransactionReturnCampaignDetail")]
    public partial class zpHopiStartReturnTransactionReturnCampaignDetail
    {
        public zpHopiStartReturnTransactionReturnCampaignDetail()
        {
        }

        [Key]
        [Required]
        public Guid ReturnCampaignDetailID { get; set; }

        [Required]
        public Guid StartReturnTransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CampaignCode { get; set; }

        [Required]
        public decimal ReturnPayment { get; set; }

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
