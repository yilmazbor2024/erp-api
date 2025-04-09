using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpAgentContractDeservedDebit")]
    public partial class tpAgentContractDeservedDebit
    {
        public tpAgentContractDeservedDebit()
        {
        }

        [Key]
        [Required]
        public Guid AgentContractDeservedDebitID { get; set; }

        [Required]
        public Guid AgentReservationHeaderID { get; set; }

        [Required]
        public Guid DebitHeaderID { get; set; }

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
        public virtual trAgentReservationHeader trAgentReservationHeader { get; set; }

    }
}
