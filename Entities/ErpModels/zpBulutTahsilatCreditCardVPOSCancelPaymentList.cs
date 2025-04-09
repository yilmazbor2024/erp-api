using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpBulutTahsilatCreditCardVPOSCancelPaymentList")]
    public partial class zpBulutTahsilatCreditCardVPOSCancelPaymentList
    {
        public zpBulutTahsilatCreditCardVPOSCancelPaymentList()
        {
        }

        [Key]
        [Required]
        public long TransactionID { get; set; }

        [Required]
        public long ParentVPosTransactionID { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

    }
}
