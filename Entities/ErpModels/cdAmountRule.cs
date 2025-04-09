using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdAmountRule")]
    public partial class cdAmountRule
    {
        public cdAmountRule()
        {
            cdAmountRuleDescs = new HashSet<cdAmountRuleDesc>();
            prAmountRuleBrackets = new HashSet<prAmountRuleBracket>();
            prDiscountOfferRuless = new HashSet<prDiscountOfferRules>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AmountRuleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public bool UseMultiplesOfValues { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }

        public virtual ICollection<cdAmountRuleDesc> cdAmountRuleDescs { get; set; }
        public virtual ICollection<prAmountRuleBracket> prAmountRuleBrackets { get; set; }
        public virtual ICollection<prDiscountOfferRules> prDiscountOfferRuless { get; set; }
    }
}
