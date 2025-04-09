using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpPaynetProcessedPayment")]
    public partial class zpPaynetProcessedPayment
    {
        public zpPaynetProcessedPayment()
        {
        }

        [Key]
        [Required]
        public Guid LogID { get; set; }

        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaynetXactID { get; set; }

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
