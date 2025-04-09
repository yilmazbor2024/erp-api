using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpPaynetCreditCardPaymentEventLog")]
    public partial class zpPaynetCreditCardPaymentEventLog
    {
        public zpPaynetCreditCardPaymentEventLog()
        {
        }

        [Key]
        [Required]
        public Guid PaynetCreditCardPaymentEventLogID { get; set; }

        [Required]
        public Guid CorrelationId { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

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
