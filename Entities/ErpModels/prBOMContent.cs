using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prBOMContent")]
    public partial class prBOMContent
    {
        public prBOMContent()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMCode { get; set; }

        [Key]
        [Required]
        public int ContentID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ContentBOMCode { get; set; }

        [Required]
        public byte ContentItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ContentItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContentColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContentItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContentItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContentItemDim3Code { get; set; }

        [Required]
        public double Qty1 { get; set; }

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

        // Navigation Properties
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdBOM cdBOM { get; set; }

    }
}
