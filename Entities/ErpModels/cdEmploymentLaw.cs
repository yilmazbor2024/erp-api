using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEmploymentLaw")]
    public partial class cdEmploymentLaw
    {
        public cdEmploymentLaw()
        {
            cdEmploymentLawDescs = new HashSet<cdEmploymentLawDesc>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
            hrSGKMonthlyDocuments = new HashSet<hrSGKMonthlyDocument>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EmploymentLawCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeclarationLawNo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EmploymentLawCodeForOffDay { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EmploymentLawCodeNext { get; set; }

        [Required]
        public bool IsMinWageAssessment { get; set; }

        [Required]
        public float ReductionRate { get; set; }

        [Required]
        public int ApplicableDayCount { get; set; }

        [Required]
        public bool ApplyEmployer { get; set; }

        [Required]
        public bool ApplyEmployee { get; set; }

        [Required]
        public float ShortTermInsuranceReductionRate { get; set; }

        [Required]
        public DateTime LawRepealDate { get; set; }

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

        public virtual ICollection<cdEmploymentLawDesc> cdEmploymentLawDescs { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
        public virtual ICollection<hrSGKMonthlyDocument> hrSGKMonthlyDocuments { get; set; }
    }
}
