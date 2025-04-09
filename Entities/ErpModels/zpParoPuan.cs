using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpParoPuan")]
    public partial class zpParoPuan
    {
        public zpParoPuan()
        {
        }

        [Key]
        [Required]
        public Guid ParoPuanID { get; set; }

        [Required]
        public Guid ParoAlisverisBittiID { get; set; }

        [Required]
        public decimal PuanTip { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string KampanyaKod { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string KampanyaAd { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IsyeriKod { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IsyeriAd { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IsyeriPuan { get; set; }

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
