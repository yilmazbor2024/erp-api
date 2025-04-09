using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpBulutTahsilatCreditCardVPOSReturnLog")]
    public partial class zpBulutTahsilatCreditCardVPOSReturnLog
    {
        public zpBulutTahsilatCreditCardVPOSReturnLog()
        {
        }

        [Key]
        [Required]
        public Guid BulutTahsilatCreditCardVPOSReturnLogID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EventType { get; set; }

        public string JsonData { get; set; }

        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

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
