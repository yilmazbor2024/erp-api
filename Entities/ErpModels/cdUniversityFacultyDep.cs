using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdUniversityFacultyDep")]
    public partial class cdUniversityFacultyDep
    {
        public cdUniversityFacultyDep()
        {
            cdUniversityFacultyDepDescs = new HashSet<cdUniversityFacultyDepDesc>();
            prEmployeeEducations = new HashSet<prEmployeeEducation>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UniversityFacultyDepCode { get; set; }

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

        public virtual ICollection<cdUniversityFacultyDepDesc> cdUniversityFacultyDepDescs { get; set; }
        public virtual ICollection<prEmployeeEducation> prEmployeeEducations { get; set; }
    }
}
