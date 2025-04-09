using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBudget")]
    public partial class trBudget
    {
        public trBudget()
        {
        }

        [Key]
        [Required]
        public Guid BudgetID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BudgetTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BudgetItem { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DetailCode { get; set; }

        [Required]
        public byte ValidMonth { get; set; }

        [Required]
        public decimal TargetAmount { get; set; }

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
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdBudgetType cdBudgetType { get; set; }

    }
}
