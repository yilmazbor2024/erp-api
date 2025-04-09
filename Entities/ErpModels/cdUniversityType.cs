using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdUniversityType")]
    public partial class cdUniversityType
    {
        public cdUniversityType()
        {
            cdUniversitys = new HashSet<cdUniversity>();
            cdUniversityTypeDescs = new HashSet<cdUniversityTypeDesc>();
        }

        [Key]
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

        public virtual ICollection<cdUniversity> cdUniversitys { get; set; }
        public virtual ICollection<cdUniversityTypeDesc> cdUniversityTypeDescs { get; set; }
    }
}
