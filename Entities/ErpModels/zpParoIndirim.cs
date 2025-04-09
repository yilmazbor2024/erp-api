using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpParoIndirim")]
    public partial class zpParoIndirim
    {
        public zpParoIndirim()
        {
        }

        [Key]
        [Required]
        public Guid ParoIndirimID { get; set; }

        [Required]
        public Guid ParoAlisverisBaslatID { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string KampanyaId { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string KampanyaAd { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string KampanyaKatkiPayi { get; set; }

        public string UrunKod { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IndirimTutar { get; set; }

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
