using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdStoreClimateZone")]
    public partial class cdStoreClimateZone
    {
        public cdStoreClimateZone()
        {
            cdStoreClimateZoneDescs = new HashSet<cdStoreClimateZoneDesc>();
            prStorePropertiess = new HashSet<prStoreProperties>();
        }

        [Key]
        [Required]
        public byte StoreClimateZoneCode { get; set; }

        [Required]
        public double MinimumLimit { get; set; }

        [Required]
        public double MaximumLimit { get; set; }

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

        public virtual ICollection<cdStoreClimateZoneDesc> cdStoreClimateZoneDescs { get; set; }
        public virtual ICollection<prStoreProperties> prStorePropertiess { get; set; }
    }
}
