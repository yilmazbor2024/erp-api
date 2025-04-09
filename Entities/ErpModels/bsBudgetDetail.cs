using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBudgetDetail")]
    public partial class bsBudgetDetail
    {
        public bsBudgetDetail()
        {
            bsBudgetDetailDescs = new HashSet<bsBudgetDetailDesc>();
            cdBudgetTypes = new HashSet<cdBudgetType>();
        }

        [Key]
        [Required]
        public byte BudgetDetailCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBudgetDetailDesc> bsBudgetDetailDescs { get; set; }
        public virtual ICollection<cdBudgetType> cdBudgetTypes { get; set; }
    }
}
