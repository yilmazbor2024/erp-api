using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpExpenseSlipTaxLine")]
    public partial class tpExpenseSlipTaxLine
    {
        public tpExpenseSlipTaxLine()
        {
        }

        [Key]
        [Required]
        public Guid ExpenseSlipTaxLineID { get; set; }

        [Required]
        public Guid ExpenseSlipLineID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        // Navigation Properties
        public virtual trExpenseSlipLine trExpenseSlipLine { get; set; }

    }
}
