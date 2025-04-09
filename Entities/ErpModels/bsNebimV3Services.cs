using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsNebimV3Services")]
    public partial class bsNebimV3Services
    {
        public bsNebimV3Services()
        {
            bsNebimV3ServicesDescs = new HashSet<bsNebimV3ServicesDesc>();
        }

        [Key]
        [Required]
        public byte NebimV3ServicesCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClassLibraryName { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsNebimV3ServicesDesc> bsNebimV3ServicesDescs { get; set; }
    }
}
