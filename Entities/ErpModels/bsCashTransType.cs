using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCashTransType")]
    public partial class bsCashTransType
    {
        public bsCashTransType()
        {
            auCashPermits = new HashSet<auCashPermit>();
            bsCashTransTypeDescs = new HashSet<bsCashTransTypeDesc>();
            dfCashDefATAttributes = new HashSet<dfCashDefATAttribute>();
            dfCashOfficialForms = new HashSet<dfCashOfficialForm>();
            trCashHeaders = new HashSet<trCashHeader>();
        }

        [Key]
        [Required]
        public byte CashTransTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<auCashPermit> auCashPermits { get; set; }
        public virtual ICollection<bsCashTransTypeDesc> bsCashTransTypeDescs { get; set; }
        public virtual ICollection<dfCashDefATAttribute> dfCashDefATAttributes { get; set; }
        public virtual ICollection<dfCashOfficialForm> dfCashOfficialForms { get; set; }
        public virtual ICollection<trCashHeader> trCashHeaders { get; set; }
    }
}
