using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auGettingDataTransferTraceHeader")]
    public partial class auGettingDataTransferTraceHeader
    {
        public auGettingDataTransferTraceHeader()
        {
            auGettingDataTransferTraceLines = new HashSet<auGettingDataTransferTraceLine>();
        }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Required]
        public DateTime OperationBegginingDate { get; set; }

        [Required]
        public TimeSpan OperationBegginingTime { get; set; }

        [Required]
        public DateTime OperationEndingDate { get; set; }

        [Required]
        public TimeSpan OperationEndingTime { get; set; }

        [Required]
        public bool IsSuccess { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object FileFolder { get; set; }

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

        public virtual ICollection<auGettingDataTransferTraceLine> auGettingDataTransferTraceLines { get; set; }
    }
}
