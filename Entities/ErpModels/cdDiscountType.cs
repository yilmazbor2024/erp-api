using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDiscountType")]
    public partial class cdDiscountType
    {
        public cdDiscountType()
        {
            cdDiscountTypeDescs = new HashSet<cdDiscountTypeDesc>();
            dfCompanyMarkups = new HashSet<dfCompanyMarkup>();
            prCustomerDiscountGrAtts = new HashSet<prCustomerDiscountGrAtt>();
            prDiscountTypeGLAccss = new HashSet<prDiscountTypeGLAccs>();
            prItemDiscountGrAtts = new HashSet<prItemDiscountGrAtt>();
            prProcessDiscounts = new HashSet<prProcessDiscount>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        public virtual ICollection<cdDiscountTypeDesc> cdDiscountTypeDescs { get; set; }
        public virtual ICollection<dfCompanyMarkup> dfCompanyMarkups { get; set; }
        public virtual ICollection<prCustomerDiscountGrAtt> prCustomerDiscountGrAtts { get; set; }
        public virtual ICollection<prDiscountTypeGLAccs> prDiscountTypeGLAccss { get; set; }
        public virtual ICollection<prItemDiscountGrAtt> prItemDiscountGrAtts { get; set; }
        public virtual ICollection<prProcessDiscount> prProcessDiscounts { get; set; }
    }
}
