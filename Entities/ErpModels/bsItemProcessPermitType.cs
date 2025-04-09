using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsItemProcessPermitType")]
    public partial class bsItemProcessPermitType
    {
        public bsItemProcessPermitType()
        {
            bsItemProcessPermitTypeDescs = new HashSet<bsItemProcessPermitTypeDesc>();
            prItemProcessPermitss = new HashSet<prItemProcessPermits>();
        }

        [Key]
        [Required]
        public byte ItemProcessPermitTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsItemProcessPermitTypeDesc> bsItemProcessPermitTypeDescs { get; set; }
        public virtual ICollection<prItemProcessPermits> prItemProcessPermitss { get; set; }
    }
}
