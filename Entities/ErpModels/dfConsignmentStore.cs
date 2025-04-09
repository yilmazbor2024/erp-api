using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfConsignmentStore")]
    public partial class dfConsignmentStore
    {
        public dfConsignmentStore()
        {
            dfConsStoreDistributionWarehouses = new HashSet<dfConsStoreDistributionWarehouse>();
            dfStoreConsStores = new HashSet<dfStoreConsStore>();
        }

        [Key]
        [Required]
        public object StoreCompanyCode { get; set; }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

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

        public virtual ICollection<dfConsStoreDistributionWarehouse> dfConsStoreDistributionWarehouses { get; set; }
        public virtual ICollection<dfStoreConsStore> dfStoreConsStores { get; set; }
    }
}
