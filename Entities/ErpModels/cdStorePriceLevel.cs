using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdStorePriceLevel")]
    public partial class cdStorePriceLevel
    {
        public cdStorePriceLevel()
        {
            cdItems = new HashSet<cdItem>();
            cdStorePriceLevelDescs = new HashSet<cdStorePriceLevelDesc>();
            prStorePropertiess = new HashSet<prStoreProperties>();
        }

        [Key]
        [Required]
        public byte StorePriceLevelCode { get; set; }

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

        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdStorePriceLevelDesc> cdStorePriceLevelDescs { get; set; }
        public virtual ICollection<prStoreProperties> prStorePropertiess { get; set; }
    }
}
