using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsCancelByProduct")]
    public partial class zpDmsCancelByProduct
    {
        public zpDmsCancelByProduct()
        {
        }

        [Key]
        [Required]
        public Guid CancelByProductID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MerchantID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StationID { get; set; }

        [Required]
        public int DmsTransactionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime CancellationDate { get; set; }

        public string ResponseCode { get; set; }

        [Required]
        public int OperationStatus { get; set; }

        public string ApplicationName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CancellationInvoiceNumber { get; set; }

        [Required]
        public decimal FinancialCancelTotal { get; set; }

        [Required]
        public decimal NonFinancialCancelTotal { get; set; }

        [Required]
        public bool IsRetry { get; set; }

        [Required]
        public int RetryCount { get; set; }

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
