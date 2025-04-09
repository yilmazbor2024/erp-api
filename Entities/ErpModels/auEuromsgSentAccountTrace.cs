using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auEuromsgSentAccountTrace")]
    public partial class auEuromsgSentAccountTrace
    {
        public auEuromsgSentAccountTrace()
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
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurraccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Email { get; set; }

    }
}
