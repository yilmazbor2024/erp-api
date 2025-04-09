using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRoll")]
    public partial class cdRoll
    {
        public cdRoll()
        {
            prRollNotess = new HashSet<prRollNotes>();
            stItemRollNumbers = new HashSet<stItemRollNumber>();
            stItemRollNumberPickings = new HashSet<stItemRollNumberPicking>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RollNumber { get; set; }

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

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchGroupCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string OriginalBarcode { get; set; }

        [Required]
        public double Width { get; set; }

        [Required]
        public double M2Gramaj { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SectionCode { get; set; }

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
        public virtual cdBatchGroup cdBatchGroup { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }

        public virtual ICollection<prRollNotes> prRollNotess { get; set; }
        public virtual ICollection<stItemRollNumber> stItemRollNumbers { get; set; }
        public virtual ICollection<stItemRollNumberPicking> stItemRollNumberPickings { get; set; }
    }
}
