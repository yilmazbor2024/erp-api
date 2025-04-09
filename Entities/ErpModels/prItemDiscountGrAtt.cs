using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemDiscountGrAtt")]
    public partial class prItemDiscountGrAtt
    {
        public prItemDiscountGrAtt()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDiscountGrCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountTypeCode { get; set; }

        [Required]
        public float DiscountRate { get; set; }

        [Required]
        public float MinDiscountRate { get; set; }

        [Required]
        public float MaxDiscountRate { get; set; }

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
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdItemDiscountGr cdItemDiscountGr { get; set; }
        public virtual cdDiscountType cdDiscountType { get; set; }

    }
}
