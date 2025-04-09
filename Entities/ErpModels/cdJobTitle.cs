using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdJobTitle")]
    public partial class cdJobTitle
    {
        public cdJobTitle()
        {
            cdJobPositions = new HashSet<cdJobPosition>();
            cdJobTitleDescs = new HashSet<cdJobTitleDesc>();
            hrEmployeeJobTitles = new HashSet<hrEmployeeJobTitle>();
            hrJobTitleOrganizationCharts = new HashSet<hrJobTitleOrganizationChart>();
            prCurrAccContacts = new HashSet<prCurrAccContact>();
            prEmployeePrevJobs = new HashSet<prEmployeePrevJob>();
            prEmployeeWorkplaceInformations = new HashSet<prEmployeeWorkplaceInformation>();
            prWorkPlaceOptimalEmployments = new HashSet<prWorkPlaceOptimalEmployment>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTitleCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object BasicSkills { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object TermsOfReference { get; set; }

        [Required]
        public byte JobTitleLevelCode { get; set; }

        [Required]
        public bool IsDepartmentManager { get; set; }

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

        // Navigation Properties
        public virtual cdJobTitleLevel cdJobTitleLevel { get; set; }

        public virtual ICollection<cdJobPosition> cdJobPositions { get; set; }
        public virtual ICollection<cdJobTitleDesc> cdJobTitleDescs { get; set; }
        public virtual ICollection<hrEmployeeJobTitle> hrEmployeeJobTitles { get; set; }
        public virtual ICollection<hrJobTitleOrganizationChart> hrJobTitleOrganizationCharts { get; set; }
        public virtual ICollection<prCurrAccContact> prCurrAccContacts { get; set; }
        public virtual ICollection<prEmployeePrevJob> prEmployeePrevJobs { get; set; }
        public virtual ICollection<prEmployeeWorkplaceInformation> prEmployeeWorkplaceInformations { get; set; }
        public virtual ICollection<prWorkPlaceOptimalEmployment> prWorkPlaceOptimalEmployments { get; set; }
    }
}
