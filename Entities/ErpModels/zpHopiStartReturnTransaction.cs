using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiStartReturnTransaction")]
    public partial class zpHopiStartReturnTransaction
    {
        public zpHopiStartReturnTransaction()
        {
        }

        [Key]
        [Required]
        public Guid StartReturnTransactionID { get; set; }

        [Required]
        public Guid SaleInvoiceHeaderID { get; set; }

        [Required]
        public Guid ReturnInvoiceHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MerchantCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string StoreCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TransactionId { get; set; }

        [Required]
        public decimal CampaignFreeAmount { get; set; }

        public string ResponseCode { get; set; }

        [Required]
        public int OperationStatus { get; set; }

        [Required]
        public decimal Residual { get; set; }

        [Required]
        public decimal ReturnTrxId { get; set; }

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
