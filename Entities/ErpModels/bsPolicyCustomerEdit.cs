using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPolicyCustomerEdit")]
    public partial class bsPolicyCustomerEdit
    {
        public bsPolicyCustomerEdit()
        {
            bsPolicyCustomerEditDescs = new HashSet<bsPolicyCustomerEditDesc>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
        }

        [Key]
        [Required]
        public byte PolicyCustomerEdit { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPolicyCustomerEditDesc> bsPolicyCustomerEditDescs { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
    }
}
