using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiCancelReturnTransaction")]
    public partial class zpHopiCancelReturnTransaction
    {
        public zpHopiCancelReturnTransaction()
        {
        }

        [Key]
        [Required]
        public Guid CancelReturnTransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MerchantCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string StoreCode { get; set; }

        [Required]
        public decimal ReturnTrxId { get; set; }

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
