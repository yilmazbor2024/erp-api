using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auGettingDataTransferTraceLine")]
    public partial class auGettingDataTransferTraceLine
    {
        public auGettingDataTransferTraceLine()
        {
        }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Key]
        [Required]
        public Guid TraceLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ColumnName { get; set; }

        public string Record { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ErrorDescription { get; set; }

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
        public virtual auGettingDataTransferTraceHeader auGettingDataTransferTraceHeader { get; set; }

    }
}
