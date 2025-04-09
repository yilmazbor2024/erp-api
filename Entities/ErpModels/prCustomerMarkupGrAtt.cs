using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCustomerMarkupGrAtt")]
    public partial class prCustomerMarkupGrAtt
    {
        public prCustomerMarkupGrAtt()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerMarkupGrCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode01 { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode02 { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode03 { get; set; }

        [Required]
        public float Rate { get; set; }

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
        public virtual cdCustomerMarkupGr cdCustomerMarkupGr { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
