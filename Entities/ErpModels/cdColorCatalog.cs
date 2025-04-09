using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdColorCatalog")]
    public partial class cdColorCatalog
    {
        public cdColorCatalog()
        {
            cdColors = new HashSet<cdColor>();
            cdColorCatalogDescs = new HashSet<cdColorCatalogDesc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCatalogCode { get; set; }

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

        public virtual ICollection<cdColor> cdColors { get; set; }
        public virtual ICollection<cdColorCatalogDesc> cdColorCatalogDescs { get; set; }
    }
}
