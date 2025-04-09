using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCreditSurveyor")]
    public partial class cdCreditSurveyor
    {
        public cdCreditSurveyor()
        {
            prCreditSurveyorResponsibilityAreas = new HashSet<prCreditSurveyorResponsibilityArea>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditSurveyorCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MobilePhone { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMail { get; set; }

        [Required]
        public bool IsAuditor { get; set; }

        [Required]
        public byte EmployeeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

        [Required]
        public byte RegionAuthorizationType { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<prCreditSurveyorResponsibilityArea> prCreditSurveyorResponsibilityAreas { get; set; }
    }
}
