using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTransferPlanRuleDesc")]
    public partial class bsTransferPlanRuleDesc
    {
        public bsTransferPlanRuleDesc()
        {
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransferPlanRuleCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TransferPlanRuleDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsTransferPlanRule bsTransferPlanRule { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
