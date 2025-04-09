using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPARO")]
    public partial class dfPARO
    {
        public dfPARO()
        {
        }

        [Key]
        [Required]
        public byte PAROID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TuketiciIslemleriUrl { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AlisverisIslemleriUrl { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DuyuruSorgulamaUrl { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PuanSorgulamaUrl { get; set; }

        [Required]
        public bool ParosuzAlisverislerGonderilsin { get; set; }

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
