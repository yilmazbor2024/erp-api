using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auMarketPlaceServiceTrace")]
    public partial class auMarketPlaceServiceTrace
    {
        public auMarketPlaceServiceTrace()
        {
        }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Required]
        public DateTime OperationStartDate { get; set; }

        [Required]
        public TimeSpan OperationStartTime { get; set; }

        [Required]
        public DateTime OperationEndDate { get; set; }

        [Required]
        public TimeSpan OperationEndTime { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ServiceType { get; set; }

    }
}
