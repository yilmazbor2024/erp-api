using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPresentCardActivationType")]
    public partial class bsPresentCardActivationType
    {
        public bsPresentCardActivationType()
        {
            bsPresentCardActivationTypeDescs = new HashSet<bsPresentCardActivationTypeDesc>();
            dfStoreDefaults = new HashSet<dfStoreDefault>();
        }

        [Key]
        [Required]
        public byte PresentCardActivationTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPresentCardActivationTypeDesc> bsPresentCardActivationTypeDescs { get; set; }
        public virtual ICollection<dfStoreDefault> dfStoreDefaults { get; set; }
    }
}
