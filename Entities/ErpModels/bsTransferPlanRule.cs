using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTransferPlanRule")]
    public partial class bsTransferPlanRule
    {
        public bsTransferPlanRule()
        {
            bsTransferPlanRuleDescs = new HashSet<bsTransferPlanRuleDesc>();
            cdTransferPlanTemplates = new HashSet<cdTransferPlanTemplate>();
            prTransferPlanRuleScripts = new HashSet<prTransferPlanRuleScript>();
            trTransferPlans = new HashSet<trTransferPlan>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransferPlanRuleCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClassName { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsTransferPlanRuleDesc> bsTransferPlanRuleDescs { get; set; }
        public virtual ICollection<cdTransferPlanTemplate> cdTransferPlanTemplates { get; set; }
        public virtual ICollection<prTransferPlanRuleScript> prTransferPlanRuleScripts { get; set; }
        public virtual ICollection<trTransferPlan> trTransferPlans { get; set; }
    }
}
