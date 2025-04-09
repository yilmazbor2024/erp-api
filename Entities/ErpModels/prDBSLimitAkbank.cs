using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDBSLimitAkbank")]
    public partial class prDBSLimitAkbank
    {
        public prDBSLimitAkbank()
        {
        }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string KayitTipiFlag { get; set; }

        [Required]
        public decimal KayitSiraNo { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BayiBankaKodu { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BayiKodu { get; set; }

        [Required]
        public decimal ToplamKrediLimiti { get; set; }

        [Required]
        public decimal RiskNakdiKredi { get; set; }

        [Required]
        public decimal IskontoluRiskKredisi { get; set; }

        [Required]
        public decimal KullanilabilirLimit { get; set; }

        [Required]
        public decimal GarantiliAlacak { get; set; }

        [Required]
        public decimal GecikmisAlacak { get; set; }

        [Required]
        public decimal GarantisizAlacak { get; set; }

        [Required]
        public decimal TaramasiSonlanmisAlacak { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string ModmEnglDurumKodu { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ModmEnglDurumTarih { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string KodmEnglDurumKodu { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string KodmEnglDurumTarih { get; set; }

        [Required]
        public int ToplamFaturaAdeti { get; set; }

        [Required]
        public decimal ToplamFaturaTutari { get; set; }

        [Required]
        public int IleriFaturaAdeti { get; set; }

        [Required]
        public decimal IleriFaturaTutari { get; set; }

        [Required]
        public decimal ArtiParaLimit { get; set; }

        [Required]
        public decimal ArtiParaKullanilabilirLimit { get; set; }

        [Required]
        public decimal ArtiParaKullanilanLimit { get; set; }

        [Required]
        public decimal TedarikSistemiLimit { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string Unvan { get; set; }

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
