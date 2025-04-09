using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdUniversity")]
    public partial class cdUniversity
    {
        public cdUniversity()
        {
            cdUniversityDescs = new HashSet<cdUniversityDesc>();
            prEmployeeEducations = new HashSet<prEmployeeEducation>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UniversityCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UniversityTypeCode { get; set; }

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
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdUniversityType cdUniversityType { get; set; }

        public virtual ICollection<cdUniversityDesc> cdUniversityDescs { get; set; }
        public virtual ICollection<prEmployeeEducation> prEmployeeEducations { get; set; }
    }
}
