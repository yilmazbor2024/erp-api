using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpMacellanSuperappTransactionInfo")]
    public partial class zpMacellanSuperappTransactionInfo
    {
        public zpMacellanSuperappTransactionInfo()
        {
        }

        [Key]
        [Required]
        public Guid TransactionInfoID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ReferenceCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string OrderID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PaymentID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Status { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object MessageID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? V3HeaderID { get; set; }

        public Guid? OtherPaymentLineID { get; set; }

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
