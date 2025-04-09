using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWarehouseType")]
    public partial class cdWarehouseType
    {
        public cdWarehouseType()
        {
            cdWarehouses = new HashSet<cdWarehouse>();
            cdWarehouseTypeDescs = new HashSet<cdWarehouseTypeDesc>();
            srCodeNumberWarehouses = new HashSet<srCodeNumberWarehouse>();
        }

        [Key]
        [Required]
        public byte WarehouseTypeCode { get; set; }

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
        public virtual ICollection<cdWarehouseTypeDesc> cdWarehouseTypeDescs { get; set; }
        public virtual ICollection<srCodeNumberWarehouse> srCodeNumberWarehouses { get; set; }
    }
}
