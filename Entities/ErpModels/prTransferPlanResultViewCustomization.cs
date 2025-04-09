using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prTransferPlanResultViewCustomization")]
    public partial class prTransferPlanResultViewCustomization
    {
        public prTransferPlanResultViewCustomization()
        {
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransferPlanRuleCode { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ColumnName { get; set; }

        [Required]
        public byte ColumnIndex { get; set; }

        [Required]
        public bool IsGroupedColumn { get; set; }

        [Required]
        public byte SortIndex { get; set; }

        [Required]
        public byte SortType { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        [Required]
        public bool IsVisibleForSumLines { get; set; }

        [Required]
        public bool IsVisibleDetailForSumLines { get; set; }

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
