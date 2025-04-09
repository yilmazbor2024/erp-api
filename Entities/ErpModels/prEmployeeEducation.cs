using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEmployeeEducation")]
    public partial class prEmployeeEducation
    {
        public prEmployeeEducation()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public byte UniversityLevelCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UniversityCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UniversityFacultyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UniversityFacultyDepCode { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndingDate { get; set; }

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
        public virtual cdUniversityLevel cdUniversityLevel { get; set; }
        public virtual cdUniversity cdUniversity { get; set; }
        public virtual cdUniversityFaculty cdUniversityFaculty { get; set; }
        public virtual cdUniversityFacultyDep cdUniversityFacultyDep { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
