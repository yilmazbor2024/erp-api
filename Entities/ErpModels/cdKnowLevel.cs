using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdKnowLevel")]
    public partial class cdKnowLevel
    {
        public cdKnowLevel()
        {
            cdKnowLevelDescs = new HashSet<cdKnowLevelDesc>();
            prEmployeeForeignLanguages = new HashSet<prEmployeeForeignLanguage>();
            prEmployeeSoftwares = new HashSet<prEmployeeSoftware>();
        }

        [Key]
        [Required]
        public byte KnowLevelCode { get; set; }

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

        public virtual ICollection<cdKnowLevelDesc> cdKnowLevelDescs { get; set; }
        public virtual ICollection<prEmployeeForeignLanguage> prEmployeeForeignLanguages { get; set; }
        public virtual ICollection<prEmployeeSoftware> prEmployeeSoftwares { get; set; }
    }
}
