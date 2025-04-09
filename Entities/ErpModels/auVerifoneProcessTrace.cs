using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auVerifoneProcessTrace")]
    public partial class auVerifoneProcessTrace
    {
        public auVerifoneProcessTrace()
        {
        }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InvoiceNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string TranId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string TesisatNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IslemTipi { get; set; }

        [Required]
        public byte TaksitSayisi { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string SicilNo { get; set; }

        [Required]
        public decimal Tutar { get; set; }

        [Required]
        public decimal Puan { get; set; }

        [Required]
        public TimeSpan IslemZamani { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IptalIslemSiraNumarasi { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string DefaultBanka { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IptalIslemOnayKodu { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IptalIslemIsyeriKodu { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IptalIslemRefNumarası { get; set; }

        [Required]
        public DateTime IptalIslemTarihi { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IptalIslemGunSonuNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IslemYapilanBank { get; set; }

        [Required]
        public decimal IptalIslemOrjinalTutar { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BankaCevapKodu { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BankaOtorizasyonNumarasi { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BankaMerchantId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BankaGunSonuNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BankaIslemSiraNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BankaReferansNo { get; set; }

        [Required]
        public DateTime BankaTarihSaat { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string KartNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IslemYapilanBanka { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string KartSahibiBanka { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string BankaKartTipi { get; set; }

        public string BankaHataAciklama { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

    }
}
