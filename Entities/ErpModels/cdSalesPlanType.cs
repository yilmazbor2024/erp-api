using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSalesPlanType")]
    public partial class cdSalesPlanType
    {
        public cdSalesPlanType()
        {
            cdSalesPlanTypeDescs = new HashSet<cdSalesPlanTypeDesc>();
            trSalesPlans = new HashSet<trSalesPlan>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalesPlanTypeCode { get; set; }

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

        public virtual ICollection<cdSalesPlanTypeDesc> cdSalesPlanTypeDescs { get; set; }
        public virtual ICollection<trSalesPlan> trSalesPlans { get; set; }
    }
}
