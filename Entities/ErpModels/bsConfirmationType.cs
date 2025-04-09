using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsConfirmationType")]
    public partial class bsConfirmationType
    {
        public bsConfirmationType()
        {
            bsConfirmationTypeDescs = new HashSet<bsConfirmationTypeDesc>();
        }

        [Key]
        [Required]
        public byte ConfirmationTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsConfirmationTypeDesc> bsConfirmationTypeDescs { get; set; }
    }
}
