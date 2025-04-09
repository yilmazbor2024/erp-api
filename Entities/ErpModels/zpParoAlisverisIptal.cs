using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpParoAlisverisIptal")]
    public partial class zpParoAlisverisIptal
    {
        public zpParoAlisverisIptal()
        {
        }

        [Key]
        [Required]
        public Guid ParoAlisverisIptalID { get; set; }

        [Required]
        public Guid ParoAlisverisBaslatID { get; set; }

        public string TrxNo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IsyeriKod { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SubeKod { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string YetkiliKod { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IslemTip { get; set; }

        public string Param1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Sonuc { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Durum { get; set; }

        public string Aciklama { get; set; }

        [Required]
        public decimal IslemSonuc { get; set; }

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
