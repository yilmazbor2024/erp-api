using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiCompleteCoinTransaction")]
    public partial class zpHopiCompleteCoinTransaction
    {
        public zpHopiCompleteCoinTransaction()
        {
        }

        [Key]
        [Required]
        public Guid CompleteCoinTransactionID { get; set; }

        [Required]
        public Guid StartCoinTransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MerchantCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string StoreCode { get; set; }

        [Required]
        public decimal ProvisionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Otp { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ResponseCode { get; set; }

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
