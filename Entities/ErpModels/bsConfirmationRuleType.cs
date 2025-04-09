using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsConfirmationRuleType")]
    public partial class bsConfirmationRuleType
    {
        public bsConfirmationRuleType()
        {
            bsConfirmationRuleTypeDescs = new HashSet<bsConfirmationRuleTypeDesc>();
            cdConfirmationRules = new HashSet<cdConfirmationRule>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte ConfirmationRuleTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<bsConfirmationRuleTypeDesc> bsConfirmationRuleTypeDescs { get; set; }
        public virtual ICollection<cdConfirmationRule> cdConfirmationRules { get; set; }
    }
}
