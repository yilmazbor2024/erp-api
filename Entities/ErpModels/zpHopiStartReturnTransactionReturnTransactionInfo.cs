using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiStartReturnTransactionReturnTransactionInfo")]
    public partial class zpHopiStartReturnTransactionReturnTransactionInfo
    {
        public zpHopiStartReturnTransactionReturnTransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid ReturnTransactionInfoID { get; set; }

        [Required]
        public Guid StartReturnTranactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Barcode { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Campaign { get; set; }

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
