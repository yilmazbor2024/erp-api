using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auTransactionCheckInOutTrace")]
    public partial class auTransactionCheckInOutTrace
    {
        public auTransactionCheckInOutTrace()
        {
        }

        [Key]
        [Required]
        public long TransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TableName { get; set; }

        [Required]
        public Guid HeaderID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CheckOutReasonCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Comment { get; set; }

        public bool? CheckIn { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        // Navigation Properties
        public virtual cdCheckOutReason cdCheckOutReason { get; set; }

    }
}
