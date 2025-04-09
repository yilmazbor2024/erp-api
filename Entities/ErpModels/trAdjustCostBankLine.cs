using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAdjustCostBankLine")]
    public partial class trAdjustCostBankLine
    {
        public trAdjustCostBankLine()
        {
        }

        [Key]
        [Required]
        public Guid AdjustCostHeaderID { get; set; }

        [Key]
        [Required]
        public Guid BankLineID { get; set; }

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
        public virtual trBankLine trBankLine { get; set; }
        public virtual trAdjustCostHeader trAdjustCostHeader { get; set; }

    }
}
