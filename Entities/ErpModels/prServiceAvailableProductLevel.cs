using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prServiceAvailableProductLevel")]
    public partial class prServiceAvailableProductLevel
    {
        public prServiceAvailableProductLevel()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel01 { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel02 { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel03 { get; set; }

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
        public virtual cdItem cdItem { get; set; }

    }
}
