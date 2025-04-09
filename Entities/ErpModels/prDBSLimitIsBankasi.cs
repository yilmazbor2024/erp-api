using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDBSLimitIsBankasi")]
    public partial class prDBSLimitIsBankasi
    {
        public prDBSLimitIsBankasi()
        {
        }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string KayitTipi { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AboneNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string Isim { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string SubeKodu { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string HesapNumarasi { get; set; }

        [Required]
        public decimal LimitTutari { get; set; }

        [Required]
        public decimal KullanilanLimitTutari { get; set; }

        [Required]
        public decimal KullanilabilirLimitTutari { get; set; }

        [Required]
        public decimal ToplamFaturaTutari { get; set; }

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
