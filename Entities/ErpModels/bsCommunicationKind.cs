using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCommunicationKind")]
    public partial class bsCommunicationKind
    {
        public bsCommunicationKind()
        {
            bsCommunicationKindDescs = new HashSet<bsCommunicationKindDesc>();
            bsEditMasks = new HashSet<bsEditMask>();
            cdCommunicationTypes = new HashSet<cdCommunicationType>();
        }

        [Key]
        [Required]
        public byte CommunicationKindCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsCommunicationKindDesc> bsCommunicationKindDescs { get; set; }
        public virtual ICollection<bsEditMask> bsEditMasks { get; set; }
        public virtual ICollection<cdCommunicationType> cdCommunicationTypes { get; set; }
    }
}
