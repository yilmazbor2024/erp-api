using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsWorkDangerLevel")]
    public partial class bsWorkDangerLevel
    {
        public bsWorkDangerLevel()
        {
            bsWorkDangerLevelDescs = new HashSet<bsWorkDangerLevelDesc>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
        }

        [Key]
        [Required]
        public byte WorkDangerLevelCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsWorkDangerLevelDesc> bsWorkDangerLevelDescs { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
    }
}
