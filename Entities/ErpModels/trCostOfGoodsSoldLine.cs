using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCostOfGoodsSoldLine")]
    public partial class trCostOfGoodsSoldLine
    {
        public trCostOfGoodsSoldLine()
        {
        }

        [Key]
        [Required]
        public Guid CostOfGoodsSoldLineID { get; set; }

        [Required]
        public Guid StockID { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal CostAmount { get; set; }

        [Required]
        public decimal CostPriceWithInflation { get; set; }

        [Required]
        public decimal CostAmountWithInflation { get; set; }

        [Required]
        public Guid CostOfGoodsSoldHeaderID { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trCostOfGoodsSoldHeader trCostOfGoodsSoldHeader { get; set; }

    }
}
