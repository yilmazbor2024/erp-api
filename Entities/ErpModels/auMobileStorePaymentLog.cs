using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auMobileStorePaymentLog")]
    public partial class auMobileStorePaymentLog
    {
        public auMobileStorePaymentLog()
        {
        }

        [Key]
        [Required]
        public Guid MobileStorePaymentLogID { get; set; }

        [Required]
        public Guid CorrelationID { get; set; }

        [Required]
        public Guid TransactionID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Source { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EventType { get; set; }

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
