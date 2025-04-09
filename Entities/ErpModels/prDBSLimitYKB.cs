using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDBSLimitYKB")]
    public partial class prDBSLimitYKB
    {
        public prDBSLimitYKB()
        {
        }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string FirmaKodu { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BayiKodu { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string SubeKodu { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BayiAdi { get; set; }

        [Required]
        public decimal DBSLimit { get; set; }

        [Required]
        public decimal NakitRisk { get; set; }

        [Required]
        public decimal GarantiAltinaAlinanFaturalarToplamTutari { get; set; }

        [Required]
        public decimal GarantiAltinaAlinmayanFaturalarToplamTutari { get; set; }

        [Required]
        public DateTime LimitDate { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

    }
}
