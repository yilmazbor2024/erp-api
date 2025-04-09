using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsAccountDetail")]
    public partial class bsAccountDetail
    {
        public bsAccountDetail()
        {
            bsAccountDetailDescs = new HashSet<bsAccountDetailDesc>();
        }

        [Key]
        [Required]
        public byte AccountDetail { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsAccountDetailDesc> bsAccountDetailDescs { get; set; }
    }
}
