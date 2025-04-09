using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsExpenseSlipType")]
    public partial class bsExpenseSlipType
    {
        public bsExpenseSlipType()
        {
            auExpenseSlipPermits = new HashSet<auExpenseSlipPermit>();
            bsExpenseSlipTypeDescs = new HashSet<bsExpenseSlipTypeDesc>();
            dfExpenseSlipForms = new HashSet<dfExpenseSlipForm>();
            trExpenseSlipHeaders = new HashSet<trExpenseSlipHeader>();
        }

        [Key]
        [Required]
        public byte ExpenseSlipTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<auExpenseSlipPermit> auExpenseSlipPermits { get; set; }
        public virtual ICollection<bsExpenseSlipTypeDesc> bsExpenseSlipTypeDescs { get; set; }
        public virtual ICollection<dfExpenseSlipForm> dfExpenseSlipForms { get; set; }
        public virtual ICollection<trExpenseSlipHeader> trExpenseSlipHeaders { get; set; }
    }
}
