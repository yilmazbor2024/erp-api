using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpRegisteredEmailForPayrollSendEvidence")]
    public partial class rpRegisteredEmailForPayrollSendEvidence
    {
        public rpRegisteredEmailForPayrollSendEvidence()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string KepEvidenceMessageID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string KepMessageID { get; set; }

        [Required]
        public DateTime EvidenceDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EvidenceType { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EventCode { get; set; }

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

    }
}
