using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpBulutTahsilatCreditCardPaymentEventLog")]
    public partial class zpBulutTahsilatCreditCardPaymentEventLog
    {
        public zpBulutTahsilatCreditCardPaymentEventLog()
        {
        }

        [Key]
        [Required]
        public Guid CorrelationId { get; set; }

        [Key]
        [Required]
        public DateTime OperationDate { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EventType { get; set; }

        public string JsonData { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ApplicationCode { get; set; }

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
