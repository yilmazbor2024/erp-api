using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsNebimV3WindowsServices")]
    public partial class bsNebimV3WindowsServices
    {
        public bsNebimV3WindowsServices()
        {
            bsNebimV3WindowsServicesDescs = new HashSet<bsNebimV3WindowsServicesDesc>();
        }

        [Key]
        [Required]
        public byte NebimV3WindowsServicesCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClassLibraryName { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsNebimV3WindowsServicesDesc> bsNebimV3WindowsServicesDescs { get; set; }
    }
}
