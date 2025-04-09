using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpOnlineBankServiceErrorLog")]
    public partial class zpOnlineBankServiceErrorLog
    {
        public zpOnlineBankServiceErrorLog()
        {
        }

        [Key]
        [Required]
        public Guid OnlineBankServiceErrorLogID { get; set; }

        [Required]
        public Guid ServiceLogID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ServiceMethod { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ErrorCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ErrorMessage { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentExpCode { get; set; }

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
