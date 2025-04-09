using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsAllocationSourceType")]
    public partial class bsAllocationSourceType
    {
        public bsAllocationSourceType()
        {
            bsAllocationSourceTypeDescs = new HashSet<bsAllocationSourceTypeDesc>();
            cdAllocationTemplates = new HashSet<cdAllocationTemplate>();
            trAllocations = new HashSet<trAllocation>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AllocationSourceTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsAllocationSourceTypeDesc> bsAllocationSourceTypeDescs { get; set; }
        public virtual ICollection<cdAllocationTemplate> cdAllocationTemplates { get; set; }
        public virtual ICollection<trAllocation> trAllocations { get; set; }
    }
}
