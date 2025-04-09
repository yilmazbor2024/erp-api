using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPaymentReturn")]
    public partial class tpPaymentReturn
    {
        public tpPaymentReturn()
        {
        }

        [Key]
        [Required]
        public Guid PaymentHeaderID { get; set; }

        [Required]
        public Guid ReturnPaymentHeaderID { get; set; }

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

        // Navigation Properties
        public virtual trPaymentHeader trPaymentHeader { get; set; }

    }
}
