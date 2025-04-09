using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auInnerTrace")]
    public partial class auInnerTrace
    {
        public auInnerTrace()
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
        public object InnerProcessCode { get; set; }

        [Required]
        public object InnerNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FieldName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OldValue { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string NewValue { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

    }
}
