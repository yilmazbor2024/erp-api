using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpParoUyeKontrol")]
    public partial class zpParoUyeKontrol
    {
        public zpParoUyeKontrol()
        {
        }

        [Key]
        [Required]
        public Guid ParoUyeKontrolID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IsyeriKod { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string YetkiliKod { get; set; }

        public string KartNo { get; set; }

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

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string ParoID { get; set; }

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
