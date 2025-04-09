using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpOrderDeliveryAssignmentCollectedItems")]
    public partial class rpOrderDeliveryAssignmentCollectedItems
    {
        public rpOrderDeliveryAssignmentCollectedItems()
        {
        }

        [Key]
        [Required]
        public Guid OrderDeliveryAssignmentCollectedItemsID { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VehicleCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public DateTime OrderDeliveryDate { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? ShippingPostalAddressID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SectionCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [Required]
        public double CollectedQty { get; set; }

        public Guid? OrderLineID { get; set; }

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

        // Navigation Properties
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdVehicle cdVehicle { get; set; }
        public virtual prCurrAccPostalAddress prCurrAccPostalAddress { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
