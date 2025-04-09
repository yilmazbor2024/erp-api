using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prRetailCustomerSegmentationFilters")]
    public partial class prRetailCustomerSegmentationFilters
    {
        public prRetailCustomerSegmentationFilters()
        {
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerFilterName { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FilterName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Field { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Operator { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public bool InCurrAcc { get; set; }

        public string FilterString { get; set; }

        public string FilterStringForSQL { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string AggregationType { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SourceView { get; set; }

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

    }
}
