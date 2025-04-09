using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDBSLimitTEB")]
    public partial class prDBSLimitTEB
    {
        public prDBSLimitTEB()
        {
        }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string KayitTipi { get; set; }

        [Key]
        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BayiCariHesapKodu { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string SubeNo { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string HesapNo { get; set; }

        [Required]
        public decimal KrediLimiti { get; set; }

        [Required]
        public decimal KrediBorcu { get; set; }

        [Required]
        public decimal OdenmeyiBekleyenFaturalar { get; set; }

        [Required]
        public int FaturaAdedi { get; set; }

        [Required]
        public decimal MahsupAlacagi { get; set; }

        [Required]
        public decimal KullanilabilirLimit { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string LimitIsaret { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string TRY { get; set; }

        [Required]
        public double Kur { get; set; }

        [Required]
        public decimal YPKullanilabilirLimit { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string YPCinsi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
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
