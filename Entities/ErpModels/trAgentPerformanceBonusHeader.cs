using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAgentPerformanceBonusHeader")]
    public partial class trAgentPerformanceBonusHeader
    {
        public trAgentPerformanceBonusHeader()
        {
            tpAgentPerformanceBonusDebits = new HashSet<tpAgentPerformanceBonusDebit>();
            trAgentPerformanceBonusLines = new HashSet<trAgentPerformanceBonusLine>();
        }

        [Key]
        [Required]
        public Guid AgentPerformanceBonusHeaderID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public DateTime FirstDate { get; set; }

        [Required]
        public DateTime LastDate { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpAgentPerformanceBonusDebit> tpAgentPerformanceBonusDebits { get; set; }
        public virtual ICollection<trAgentPerformanceBonusLine> trAgentPerformanceBonusLines { get; set; }
    }
}
