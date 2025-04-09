using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpBasefyCheckOutNew")]
    public partial class zpBasefyCheckOutNew
    {
        public zpBasefyCheckOutNew()
        {
        }

        [Key]
        [Required]
        public Guid CheckOutNewID { get; set; }

        [Required]
        public Guid TransactionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MembershipNumber { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        [Required]
        public int OperationStatus { get; set; }

        [Required]
        public int RetryCount { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
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
