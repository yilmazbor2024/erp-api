using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpPaynetDistanceSalePaymentURL")]
    public partial class zpPaynetDistanceSalePaymentURL
    {
        public zpPaynetDistanceSalePaymentURL()
        {
        }

        [Key]
        [Required]
        public Guid CorrelationID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ID { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object URL { get; set; }

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

    }
}
