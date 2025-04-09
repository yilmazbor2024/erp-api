using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSalesPlanProductQty")]
    public partial class trSalesPlanProductQty
    {
        public trSalesPlanProductQty()
        {
        }

        [Key]
        [Required]
        public Guid SalesPlanProductQtyID { get; set; }

        [Required]
        public Guid SalesPlanChannelID { get; set; }

        [Required]
        public Guid SalesPlanProductID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [Required]
        public int LotQty { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

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
        public virtual trSalesPlanChannel trSalesPlanChannel { get; set; }
        public virtual trSalesPlanProduct trSalesPlanProduct { get; set; }
        public virtual cdLot cdLot { get; set; }
        public virtual trSalesPlan trSalesPlan { get; set; }
        public virtual cdColor cdColor { get; set; }

    }
}
