using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auSurveySectionPermit")]
    public partial class auSurveySectionPermit
    {
        public auSurveySectionPermit()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

         

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RoleCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SurveyCode { get; set; }

        [Key]
        [Required]
        public int SurveySectionNumber { get; set; }

        [Required]
        public bool CanSelect { get; set; }

        [Required]
        public bool CanUpdate { get; set; }

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
        public virtual cdRole cdRole { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdSurveySection cdSurveySection { get; set; }

    }
}
