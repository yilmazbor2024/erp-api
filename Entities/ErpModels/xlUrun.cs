using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("xlUrun")]
    public partial class xlUrun
    {
        public xlUrun()
        {
        }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UrunKodu { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunAdi { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RenkKodu { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RenkAdi { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Boyut1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Boyut2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Boyut3 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UreticiRenkKodu { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BirimCinsi { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MaddeVergiGrubu { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MuhasebeHesapGrubu { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MuhasebeHesapGrubuAdi { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MaddeTedarikciGrubu { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MaddeTedarikciGrubuAdi { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BarkodTipi { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Barkod { get; set; }

        public bool? SeriNumarasiTakibi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik01 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik01Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik02 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik02Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik03 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik03Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik04 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik04Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik05 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik05Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik06 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik06Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik07 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik07Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik08 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik08Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik09 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik09Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik10 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik10Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik11 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik11Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik12 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik12Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik13 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik13Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik14 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik14Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik15 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik15Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik16 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik16Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik17 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik17Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik18 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik18Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik19 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik19Adi { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Ozellik20 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Ozellik20Adi { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye01 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye02 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye03 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye04 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye05 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye06 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye07 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye08 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye09 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UrunHiyerarsiSeviye10 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SezonKodu { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string KolleksiyonKodu { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OykuPanosuKodu { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RenkTemaKodu { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RenkTemaAdi { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MaliyetFiyatiParaBirimi { get; set; }

        public decimal? MaliyetFiyati { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SatinAlmaFiyatiParaBirimi { get; set; }

        public decimal? SatinAlmaFiyati { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SatisFiyatiParaBirimi { get; set; }

        public decimal? SatisFiyati { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IlkSatisFiyatiParaBirimi { get; set; }

        public decimal? IlkSatisFiyati { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string HedefAlimFiyatiParaBirimi { get; set; }

        public decimal? HedefAlimFiyati { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string HedefSatisFiyatiParaBirimi { get; set; }

        public decimal? HedefSatisFiyati { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PerakendeSatisFiyatiParaBirimi { get; set; }

        public decimal? PerakendeSatisFiyati { get; set; }

    }
}
