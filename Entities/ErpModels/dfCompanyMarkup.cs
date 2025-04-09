using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCompanyMarkup")]
    public partial class dfCompanyMarkup
    {
        public dfCompanyMarkup()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel02 { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdDiscountType cdDiscountType { get; set; }

    }
}
