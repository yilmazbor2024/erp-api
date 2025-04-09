using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceadditionalDeliveryProcessesDistance")]
    public partial class tpInvoiceadditionalDeliveryProcessesDistance
    {
        public tpInvoiceadditionalDeliveryProcessesDistance()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCityCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DeliveryDistrictCode { get; set; }

        [Required]
        public double Distance { get; set; }

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

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public bool IsDeliveryFromStore { get; set; }

        // Navigation Properties
        public virtual cdCity cdCity { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdDistrict cdDistrict { get; set; }

    }
}
