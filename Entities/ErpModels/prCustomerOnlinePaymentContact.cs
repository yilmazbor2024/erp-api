using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCustomerOnlinePaymentContact")]
    public partial class prCustomerOnlinePaymentContact
    {
        public prCustomerOnlinePaymentContact()
        {
        }

        [Key]
        [Required]
        public Guid CustomerOnlinePaymentContactID { get; set; }

        [Required]
        public byte CustomerTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        [Required]
        public Guid ContactID { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
