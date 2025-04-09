using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdTransferPlanTemplate")]
    public partial class cdTransferPlanTemplate
    {
        public cdTransferPlanTemplate()
        {
            cdTransferPlanTemplateDescs = new HashSet<cdTransferPlanTemplateDesc>();
            dfPeriodicalTransferPlanRules = new HashSet<dfPeriodicalTransferPlanRule>();
            prTransferPlanTemplateParameterValues = new HashSet<prTransferPlanTemplateParameterValue>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TransferPlanTemplateCode { get; set; }

        [Required]
        public DateTime TemplateDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransferPlanRuleCode { get; set; }

        public string ProductFilterString { get; set; }

        public string ProductFilterStringSQL { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ChannelTemplateCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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
        public virtual bsTransferPlanRule bsTransferPlanRule { get; set; }
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<cdTransferPlanTemplateDesc> cdTransferPlanTemplateDescs { get; set; }
        public virtual ICollection<dfPeriodicalTransferPlanRule> dfPeriodicalTransferPlanRules { get; set; }
        public virtual ICollection<prTransferPlanTemplateParameterValue> prTransferPlanTemplateParameterValues { get; set; }
    }
}
