using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSalesPlan")]
    public partial class trSalesPlan
    {
        public trSalesPlan()
        {
            trSalesPlanChannels = new HashSet<trSalesPlanChannel>();
            trSalesPlanProducts = new HashSet<trSalesPlanProduct>();
            trSalesPlanProductQtys = new HashSet<trSalesPlanProductQty>();
        }

        [Key]
        [Required]
        public Guid SalesPlanID { get; set; }

        [Required]
        public object SalesPlanNumber { get; set; }

        [Required]
        public DateTime PlanDate { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public short SalesYear { get; set; }

        [Required]
        public byte SalesMonth { get; set; }

        [Required]
        public byte SalesIsoWeek { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalesPlanTypeCode { get; set; }

        [Required]
        public byte SalesWeek { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [Required]
        public bool IsPostingOrder { get; set; }

        [Required]
        public bool IsPostingSaleOrder { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdSalesPlanType cdSalesPlanType { get; set; }

        public virtual ICollection<trSalesPlanChannel> trSalesPlanChannels { get; set; }
        public virtual ICollection<trSalesPlanProduct> trSalesPlanProducts { get; set; }
        public virtual ICollection<trSalesPlanProductQty> trSalesPlanProductQtys { get; set; }
    }
}
