using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpPaynetDistanceSalePayment")]
    public partial class zpPaynetDistanceSalePayment
    {
        public zpPaynetDistanceSalePayment()
        {
        }

        [Key]
        [Required]
        public Guid CorrelationID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public bool PaymentCompleted { get; set; }

        public string JsonData { get; set; }

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
