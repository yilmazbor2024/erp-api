using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsLinkedProductType")]
    public partial class bsLinkedProductType
    {
        public bsLinkedProductType()
        {
            bsLinkedProductTypeDescs = new HashSet<bsLinkedProductTypeDesc>();
            prLinkedProductPropertiess = new HashSet<prLinkedProductProperties>();
        }

        [Key]
        [Required]
        public byte LinkedProductTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsLinkedProductTypeDesc> bsLinkedProductTypeDescs { get; set; }
        public virtual ICollection<prLinkedProductProperties> prLinkedProductPropertiess { get; set; }
    }
}
