using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSalesPlanProduct")]
    public partial class trSalesPlanProduct
    {
        public trSalesPlanProduct()
        {
            trSalesPlanProductQtys = new HashSet<trSalesPlanProductQty>();
        }

        [Key]
        [Required]
        public Guid SalesPlanProductID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte ProductTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductCode { get; set; }

        [Required]
        public Guid SalesPlanID { get; set; }

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
        public virtual trSalesPlan trSalesPlan { get; set; }
        public virtual cdItem cdItem { get; set; }

        public virtual ICollection<trSalesPlanProductQty> trSalesPlanProductQtys { get; set; }
    }
}
