using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdUniversityLevel")]
    public partial class cdUniversityLevel
    {
        public cdUniversityLevel()
        {
            cdUniversityLevelDescs = new HashSet<cdUniversityLevelDesc>();
            prEmployeeEducations = new HashSet<prEmployeeEducation>();
        }

        [Key]
        [Required]
        public byte UniversityLevelCode { get; set; }

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

        public virtual ICollection<cdUniversityLevelDesc> cdUniversityLevelDescs { get; set; }
        public virtual ICollection<prEmployeeEducation> prEmployeeEducations { get; set; }
    }
}
