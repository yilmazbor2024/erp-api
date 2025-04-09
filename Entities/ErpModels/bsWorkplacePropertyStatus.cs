using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsWorkplacePropertyStatus")]
    public partial class bsWorkplacePropertyStatus
    {
        public bsWorkplacePropertyStatus()
        {
            bsWorkplacePropertyStatusDescs = new HashSet<bsWorkplacePropertyStatusDesc>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
        }

        [Key]
        [Required]
        public byte WorkplacePropertyStatusCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsWorkplacePropertyStatusDesc> bsWorkplacePropertyStatusDescs { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
    }
}
