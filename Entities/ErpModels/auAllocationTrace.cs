using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auAllocationTrace")]
    public partial class auAllocationTrace
    {
        public auAllocationTrace()
        {
        }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [Required]
        public byte ProcessType { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ProcessMessage { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AllocationTemplateCode { get; set; }

        [Required]
        public Guid AllocationID { get; set; }

        [Required]
        public object AllocationNumber { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

    }
}
