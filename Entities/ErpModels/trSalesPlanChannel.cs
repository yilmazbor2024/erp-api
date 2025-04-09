using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSalesPlanChannel")]
    public partial class trSalesPlanChannel
    {
        public trSalesPlanChannel()
        {
            trSalesPlanProductQtys = new HashSet<trSalesPlanProductQty>();
        }

        [Key]
        [Required]
        public Guid SalesPlanChannelID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [Required]
        public int SortOrder { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trSalesPlanProductQty> trSalesPlanProductQtys { get; set; }
    }
}
