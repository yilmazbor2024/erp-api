using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPolicyCustomerPayment")]
    public partial class bsPolicyCustomerPayment
    {
        public bsPolicyCustomerPayment()
        {
            bsPolicyCustomerPaymentDescs = new HashSet<bsPolicyCustomerPaymentDesc>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
        }

        [Key]
        [Required]
        public byte PolicyCustomerPayment { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPolicyCustomerPaymentDesc> bsPolicyCustomerPaymentDescs { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
    }
}
