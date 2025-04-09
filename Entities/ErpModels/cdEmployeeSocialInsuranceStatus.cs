using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEmployeeSocialInsuranceStatus")]
    public partial class cdEmployeeSocialInsuranceStatus
    {
        public cdEmployeeSocialInsuranceStatus()
        {
            cdEmployeeSocialInsuranceStatusDescs = new HashSet<cdEmployeeSocialInsuranceStatusDesc>();
            dfSocialInsuranceRatess = new HashSet<dfSocialInsuranceRates>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
        }

        [Key]
        [Required]
        public byte EmployeeSocialInsuranceStatusCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SGKDocumentType { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdEmployeeSocialInsuranceStatusDesc> cdEmployeeSocialInsuranceStatusDescs { get; set; }
        public virtual ICollection<dfSocialInsuranceRates> dfSocialInsuranceRatess { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
    }
}
