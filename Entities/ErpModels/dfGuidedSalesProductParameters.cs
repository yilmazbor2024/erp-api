using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfGuidedSalesProductParameters")]
    public partial class dfGuidedSalesProductParameters
    {
        public dfGuidedSalesProductParameters()
        {
        }

        [Key]
        [Required]
        public Guid GuidedSalesProductParametersID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductSearchLevel01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductSearchLevel02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductSearchLevel03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductSearchLevel04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductSearchLevel05 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string FirstSalePriceGroupCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string AlternativeProductItemLikeTypeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CombinedProductItemLikeTypeCode { get; set; }

        public bool? ShowProductInformation { get; set; }

        public bool? CanEditProductInformation { get; set; }

        public bool? CanAddProductInformation { get; set; }

        public string ProductRecommendationFilterStringSQL { get; set; }

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

    }
}
