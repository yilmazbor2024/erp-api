using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsWorkplaceKind")]
    public partial class bsWorkplaceKind
    {
        public bsWorkplaceKind()
        {
            bsWorkplaceKindDescs = new HashSet<bsWorkplaceKindDesc>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
        }

        [Key]
        [Required]
        public byte WorkplaceKindCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsWorkplaceKindDesc> bsWorkplaceKindDescs { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
    }
}
