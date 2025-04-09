using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfUserPosUISettings")]
    public partial class dfUserPosUISettings
    {
        public dfUserPosUISettings()
        {
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [Key]
        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCurrAccCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PosUICode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SkinName { get; set; }

        [Required]
        public byte TimeOutSpan { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DiscountPassword { get; set; }

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

        // Navigation Properties
        public virtual dfUserPosition dfUserPosition { get; set; }
        public virtual dfPosUI dfPosUI { get; set; }

    }
}
