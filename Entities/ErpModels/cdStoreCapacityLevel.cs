using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdStoreCapacityLevel")]
    public partial class cdStoreCapacityLevel
    {
        public cdStoreCapacityLevel()
        {
            cdItems = new HashSet<cdItem>();
            cdStoreCapacityLevelDescs = new HashSet<cdStoreCapacityLevelDesc>();
            prStoreCapacitys = new HashSet<prStoreCapacity>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string StoreCapacityLevelCode { get; set; }

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
        public virtual ICollection<cdStoreCapacityLevelDesc> cdStoreCapacityLevelDescs { get; set; }
        public virtual ICollection<prStoreCapacity> prStoreCapacitys { get; set; }
    }
}
