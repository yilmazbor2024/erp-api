using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBOM")]
    public partial class cdBOM
    {
        public cdBOM()
        {
            cdBOMDescs = new HashSet<cdBOMDesc>();
            prBOMContents = new HashSet<prBOMContent>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMTemplateCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

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

        // Navigation Properties
        public virtual cdBOMTemplate cdBOMTemplate { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }

        public virtual ICollection<cdBOMDesc> cdBOMDescs { get; set; }
        public virtual ICollection<prBOMContent> prBOMContents { get; set; }
    }
}
