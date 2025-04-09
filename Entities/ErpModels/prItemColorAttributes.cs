using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemColorAttributes")]
    public partial class prItemColorAttributes
    {
        public prItemColorAttributes()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ColorThemeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PantoneCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ManufacturerColorCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomsTariffNumberCode { get; set; }

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
        public virtual cdPantone cdPantone { get; set; }
        public virtual cdColorTheme cdColorTheme { get; set; }
        public virtual cdItem cdItem { get; set; }
        public virtual cdColorGroup cdColorGroup { get; set; }
        public virtual cdCustomsTariffNumber cdCustomsTariffNumber { get; set; }
        public virtual cdColorType cdColorType { get; set; }
        public virtual cdColor cdColor { get; set; }

    }
}
