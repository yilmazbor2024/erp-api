using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPantone")]
    public partial class cdPantone
    {
        public cdPantone()
        {
            cdPantoneDescs = new HashSet<cdPantoneDesc>();
            prItemColorAttributess = new HashSet<prItemColorAttributes>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PantoneCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ColorName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ColorHex { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

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
        public virtual cdColor cdColor { get; set; }

        public virtual ICollection<cdPantoneDesc> cdPantoneDescs { get; set; }
        public virtual ICollection<prItemColorAttributes> prItemColorAttributess { get; set; }
    }
}
