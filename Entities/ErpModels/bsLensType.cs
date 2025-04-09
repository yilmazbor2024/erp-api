using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsLensType")]
    public partial class bsLensType
    {
        public bsLensType()
        {
            bsLensTypeDescs = new HashSet<bsLensTypeDesc>();
            prProductLensPropertiess = new HashSet<prProductLensProperties>();
        }

        [Key]
        [Required]
        public byte LensTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsLensTypeDesc> bsLensTypeDescs { get; set; }
        public virtual ICollection<prProductLensProperties> prProductLensPropertiess { get; set; }
    }
}
