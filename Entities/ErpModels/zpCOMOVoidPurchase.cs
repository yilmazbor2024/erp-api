using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpCOMOVoidPurchase")]
    public partial class zpCOMOVoidPurchase
    {
        public zpCOMOVoidPurchase()
        {
        }

        [Key]
        [Required]
        public Guid VoidPurchaseID { get; set; }

        [Required]
        public Guid TransactionID { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ResponseCode { get; set; }

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
