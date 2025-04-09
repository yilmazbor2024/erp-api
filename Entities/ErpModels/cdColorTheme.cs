using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdColorTheme")]
    public partial class cdColorTheme
    {
        public cdColorTheme()
        {
            cdColorThemeDescs = new HashSet<cdColorThemeDesc>();
            prColorThemeAttributes = new HashSet<prColorThemeAttribute>();
            prItemColorAttributess = new HashSet<prItemColorAttributes>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ColorThemeCode { get; set; }

        [Required]
        public DateTime FirstIncomingDate { get; set; }

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

        public virtual ICollection<cdColorThemeDesc> cdColorThemeDescs { get; set; }
        public virtual ICollection<prColorThemeAttribute> prColorThemeAttributes { get; set; }
        public virtual ICollection<prItemColorAttributes> prItemColorAttributess { get; set; }
    }
}
