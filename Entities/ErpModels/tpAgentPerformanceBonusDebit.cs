using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpAgentPerformanceBonusDebit")]
    public partial class tpAgentPerformanceBonusDebit
    {
        public tpAgentPerformanceBonusDebit()
        {
        }

        [Key]
        [Required]
        public Guid AgentPerformanceBonusDebitID { get; set; }

        [Required]
        public Guid AgentPerformanceBonusHeaderID { get; set; }

        [Required]
        public Guid DebitHeaderID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

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
        public virtual trDebitHeader trDebitHeader { get; set; }
        public virtual trAgentPerformanceBonusHeader trAgentPerformanceBonusHeader { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }

    }
}
