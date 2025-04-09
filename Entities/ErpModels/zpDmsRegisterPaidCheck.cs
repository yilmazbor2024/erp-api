using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsRegisterPaidCheck")]
    public partial class zpDmsRegisterPaidCheck
    {
        public zpDmsRegisterPaidCheck()
        {
        }

        [Key]
        [Required]
        public Guid RegisterPaidCheckID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MerchantID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StationID { get; set; }

        [Required]
        public int RecognitionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DMSNo { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InvoiceNumber { get; set; }

        [Required]
        public decimal PaidTotal { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public string ResponseCode { get; set; }

        [Required]
        public int OperationStatus { get; set; }

        [Required]
        public int DmsTransactionID { get; set; }

        [Required]
        public decimal RewardPoints { get; set; }

        public string ApplicationName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LocalCustomerNo { get; set; }

        [Required]
        public decimal FinancialPaidTotal { get; set; }

        [Required]
        public decimal NonFinancialPaidTotal { get; set; }

        [Required]
        public bool IsRetry { get; set; }

        [Required]
        public decimal OriginalTotalPrice { get; set; }

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
