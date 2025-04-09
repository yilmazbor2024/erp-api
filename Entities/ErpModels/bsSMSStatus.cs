using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsSMSStatus")]
    public partial class bsSMSStatus
    {
        public bsSMSStatus()
        {
            bsSMSStatusDescs = new HashSet<bsSMSStatusDesc>();
            trSMSPoolLines = new HashSet<trSMSPoolLine>();
        }

        [Key]
        [Required]
        public byte SMSStatusCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsSMSStatusDesc> bsSMSStatusDescs { get; set; }
        public virtual ICollection<trSMSPoolLine> trSMSPoolLines { get; set; }
    }
}
