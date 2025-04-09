using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsGlassIndex")]
    public partial class bsGlassIndex
    {
        public bsGlassIndex()
        {
            prProductLensPropertiess = new HashSet<prProductLensProperties>();
        }

        [Key]
        [Required]
        public double GlassIndex { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<prProductLensProperties> prProductLensPropertiess { get; set; }
    }
}
