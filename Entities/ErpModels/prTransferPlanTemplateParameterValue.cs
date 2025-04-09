using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prTransferPlanTemplateParameterValue")]
    public partial class prTransferPlanTemplateParameterValue
    {
        public prTransferPlanTemplateParameterValue()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TransferPlanTemplateCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransferPlanTemplateRuleCode { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

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
        public virtual cdTransferPlanTemplate cdTransferPlanTemplate { get; set; }

    }
}
