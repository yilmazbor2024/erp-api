using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpParoPuanIade")]
    public partial class zpParoPuanIade
    {
        public zpParoPuanIade()
        {
        }

        [Key]
        [Required]
        public Guid ParoPuanIadeID { get; set; }

        [Required]
        public Guid ParoAlisverisIptalID { get; set; }

        [Required]
        public decimal PuanIadeTip { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string PuanYtl { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string Puan { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PuanAd { get; set; }

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
