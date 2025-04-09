using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prSubCurrAccAttribute")]
    public partial class prSubCurrAccAttribute
    {
        public prSubCurrAccAttribute()
        {
        }

        [Key]
        [Required]
        public Guid SubCurrAccID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

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
        public virtual cdSubCurrAccAttribute cdSubCurrAccAttribute { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

    }
}
