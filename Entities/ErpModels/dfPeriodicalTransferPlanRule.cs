using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPeriodicalTransferPlanRule")]
    public partial class dfPeriodicalTransferPlanRule
    {
        public dfPeriodicalTransferPlanRule()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

   
        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TransferPlanTemplateCode { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [Required]
        public int RepetitionPeriod { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Activated { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdTransferPlanTemplate cdTransferPlanTemplate { get; set; }

    }
}
