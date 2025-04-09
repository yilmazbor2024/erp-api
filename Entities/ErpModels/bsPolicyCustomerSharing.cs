using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPolicyCustomerSharing")]
    public partial class bsPolicyCustomerSharing
    {
        public bsPolicyCustomerSharing()
        {
            bsPolicyCustomerSharingDescs = new HashSet<bsPolicyCustomerSharingDesc>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
        }

        [Key]
        [Required]
        public byte PolicyCustomerSharing { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPolicyCustomerSharingDesc> bsPolicyCustomerSharingDescs { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
    }
}
