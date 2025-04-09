using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsWarehouseOwner")]
    public partial class bsWarehouseOwner
    {
        public bsWarehouseOwner()
        {
            bsWarehouseOwnerDescs = new HashSet<bsWarehouseOwnerDesc>();
            cdWarehouses = new HashSet<cdWarehouse>();
        }

        [Key]
        [Required]
        public byte WarehouseOwnerCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsWarehouseOwnerDesc> bsWarehouseOwnerDescs { get; set; }
        public virtual ICollection<cdWarehouse> cdWarehouses { get; set; }
    }
}
