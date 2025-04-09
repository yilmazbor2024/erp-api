using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auItemCopyTrace")]
    public partial class auItemCopyTrace
    {
        public auItemCopyTrace()
        {
        }

        [Key]
        [Required]
        public Guid ItemCopyTraceID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public byte SourceItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SourceItemCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

    }
}
