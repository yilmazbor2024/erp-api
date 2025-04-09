using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsAllocationRule")]
    public partial class bsAllocationRule
    {
        public bsAllocationRule()
        {
            bsAllocationRuleDescs = new HashSet<bsAllocationRuleDesc>();
            cdAllocationTemplates = new HashSet<cdAllocationTemplate>();
            prAllocationRuleScripts = new HashSet<prAllocationRuleScript>();
            trAllocations = new HashSet<trAllocation>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string AllocationRuleCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ParameteredFields { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClassName { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsAllocationRuleDesc> bsAllocationRuleDescs { get; set; }
        public virtual ICollection<cdAllocationTemplate> cdAllocationTemplates { get; set; }
        public virtual ICollection<prAllocationRuleScript> prAllocationRuleScripts { get; set; }
        public virtual ICollection<trAllocation> trAllocations { get; set; }
    }
}
