using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdFabric")]
    public partial class cdFabric
    {
        public cdFabric()
        {
            cdFabricDescs = new HashSet<cdFabricDesc>();
            prItemColorFabricBlends = new HashSet<prItemColorFabricBlend>();
            prProductPartAvailableFabrics = new HashSet<prProductPartAvailableFabric>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string FabricCode { get; set; }

        [Required]
        public bool IsNatural { get; set; }

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

        public virtual ICollection<cdFabricDesc> cdFabricDescs { get; set; }
        public virtual ICollection<prItemColorFabricBlend> prItemColorFabricBlends { get; set; }
        public virtual ICollection<prProductPartAvailableFabric> prProductPartAvailableFabrics { get; set; }
    }
}
