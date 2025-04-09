using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsSGKWorkPlaceSector")]
    public partial class bsSGKWorkPlaceSector
    {
        public bsSGKWorkPlaceSector()
        {
            bsSGKWorkPlaceSectorDescs = new HashSet<bsSGKWorkPlaceSectorDesc>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SGKWorkPlaceSectorCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsSGKWorkPlaceSectorDesc> bsSGKWorkPlaceSectorDescs { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
    }
}
