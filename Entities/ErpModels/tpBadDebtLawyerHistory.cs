using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpBadDebtLawyerHistory")]
    public partial class tpBadDebtLawyerHistory
    {
        public tpBadDebtLawyerHistory()
        {
        }

        [Key]
        [Required]
        public Guid BadDebtLawyerHistoryID { get; set; }

        [Required]
        public Guid BadDebtTransHeaderID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OldLawyerCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string NewLawyerCode { get; set; }

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
        public virtual cdLawyer cdLawyer { get; set; }
        public virtual trBadDebtTransHeader trBadDebtTransHeader { get; set; }

    }
}
