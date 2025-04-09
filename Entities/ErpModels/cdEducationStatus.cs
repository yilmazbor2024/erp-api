using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEducationStatus")]
    public partial class cdEducationStatus
    {
        public cdEducationStatus()
        {
            cdEducationStatusDescs = new HashSet<cdEducationStatusDesc>();
            prCurrAccPersonalInfos = new HashSet<prCurrAccPersonalInfo>();
        }

        [Key]
        [Required]
        public byte EducationStatusCode { get; set; }

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

        public virtual ICollection<cdEducationStatusDesc> cdEducationStatusDescs { get; set; }
        public virtual ICollection<prCurrAccPersonalInfo> prCurrAccPersonalInfos { get; set; }
    }
}
