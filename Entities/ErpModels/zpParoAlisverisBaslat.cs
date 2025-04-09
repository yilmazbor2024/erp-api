using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpParoAlisverisBaslat")]
    public partial class zpParoAlisverisBaslat
    {
        public zpParoAlisverisBaslat()
        {
        }

        [Key]
        [Required]
        public Guid ParoAlisverisBaslatID { get; set; }

        [Required]
        public Guid ParoUyeKontrolID { get; set; }

        [Required]
        public Guid FaturaID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IsyeriKod { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SubeKod { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string YetkiliKod { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PcId { get; set; }

        public string KartNo { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ad { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Soyad { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DogumTarihi { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Ceptel { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IsTel { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string EvTel { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string Yasakli { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Marka { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Model { get; set; }

        public string Urun { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string IslemTip { get; set; }

        public string Param1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Sonuc { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Durum { get; set; }

        public string Aciklama { get; set; }

        public string TrxNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string KParoPuan { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string TParoPuan { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string HarcamaDurum { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string VerdeKod { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IsyeriPuan { get; set; }

        [Required]
        public decimal IslemSonuc { get; set; }

        [Required]
        public decimal AlisverisDurum { get; set; }

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
