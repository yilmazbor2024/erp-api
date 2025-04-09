using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prAmountRuleBracket")]
    public partial class prAmountRuleBracket
    {
        public prAmountRuleBracket()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AmountRuleCode { get; set; }

        [Key]
        [Required]
        public decimal MinAmountBracket { get; set; }

        [Required]
        public float DiscountRate { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

        [Required]
        public decimal Point { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdAmountRule cdAmountRule { get; set; }

    }
}
