using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiStartCoinTransaction")]
    public partial class zpHopiStartCoinTransaction
    {
        public zpHopiStartCoinTransaction()
        {
        }

        [Key]
        [Required]
        public Guid StartCoinTransactionID { get; set; }

        [Required]
        public Guid GetHopiUserInfoID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MerchantCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string StoreCode { get; set; }

        [Required]
        public long BirdID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string ResponseCode { get; set; }

        [Required]
        public decimal ProvisionID { get; set; }

        [Required]
        public bool OtpNeeded { get; set; }

        [Required]
        public int OperationStatus { get; set; }

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
