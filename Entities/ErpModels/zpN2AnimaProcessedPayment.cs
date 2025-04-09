using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpN2AnimaProcessedPayment")]
    public partial class zpN2AnimaProcessedPayment
    {
        public zpN2AnimaProcessedPayment()
        {
        }

        [Key]
        [Required]
        public Guid LogID { get; set; }

        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ID { get; set; }

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
