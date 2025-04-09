using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWarehouseCategory")]
    public partial class cdWarehouseCategory
    {
        public cdWarehouseCategory()
        {
            cdWarehouses = new HashSet<cdWarehouse>();
            cdWarehouseCategoryDescs = new HashSet<cdWarehouseCategoryDesc>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string WarehouseCategoryCode { get; set; }

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

        public virtual ICollection<cdWarehouse> cdWarehouses { get; set; }
        public virtual ICollection<cdWarehouseCategoryDesc> cdWarehouseCategoryDescs { get; set; }
    }
}
