using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAllocationParameterValue")]
    public partial class trAllocationParameterValue
    {
        public trAllocationParameterValue()
        {
        }

        [Key]
        [Required]
        public Guid AllocationID { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string AllocationRuleCode { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        // Navigation Properties
        public virtual trAllocation trAllocation { get; set; }

    }
}
