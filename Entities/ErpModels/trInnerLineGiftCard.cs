using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInnerLineGiftCard")]
    public partial class trInnerLineGiftCard
    {
        public trInnerLineGiftCard()
        {
        }

        [Key]
        [Required]
        public Guid InnerLineID { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

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

        // Navigation Properties
        public virtual cdGiftCard cdGiftCard { get; set; }
        public virtual trInnerLine trInnerLine { get; set; }

    }
}
