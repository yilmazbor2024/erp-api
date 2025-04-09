using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prTransferPlanRuleScript")]
    public partial class prTransferPlanRuleScript
    {
        public prTransferPlanRuleScript()
        {
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransferPlanRuleCode { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string ScriptLanguage { get; set; }

        public string ScriptText { get; set; }

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

    }
}
