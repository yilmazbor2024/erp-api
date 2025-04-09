using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPolicyVendorSharing")]
    public partial class bsPolicyVendorSharing
    {
        public bsPolicyVendorSharing()
        {
            bsPolicyVendorSharingDescs = new HashSet<bsPolicyVendorSharingDesc>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
        }

        [Key]
        [Required]
        public byte PolicyVendorSharing { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPolicyVendorSharingDesc> bsPolicyVendorSharingDescs { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
    }
}
