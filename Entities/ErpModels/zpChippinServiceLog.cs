using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpChippinServiceLog")]
    public partial class zpChippinServiceLog
    {
        public zpChippinServiceLog()
        {
        }

        [Key]
        [Required]
        public Guid ServiceLogID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ServiceMethodName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentID { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

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

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TransactionID { get; set; }

    }
}
