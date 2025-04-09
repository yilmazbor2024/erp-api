using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auInteractiveSMSTrace")]
    public partial class auInteractiveSMSTrace
    {
        public auInteractiveSMSTrace()
        {
        }

        [Key]
        [Required]
        public Guid InteractiveSMSTraceTraceID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GSMOperator { get; set; }

        [Required]
        public int PrefixID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Prefix { get; set; }

        [Required]
        public int MsgID { get; set; }

        [Required]
        public DateTime MsgDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string MsgNumber { get; set; }

        public string MsgText { get; set; }

        [Required]
        public bool IsProcessed { get; set; }

    }
}
