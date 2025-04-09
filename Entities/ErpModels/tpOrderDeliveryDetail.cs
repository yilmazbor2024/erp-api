using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderDeliveryDetail")]
    public partial class tpOrderDeliveryDetail
    {
        public tpOrderDeliveryDetail()
        {
        }

        [Key]
        [Required]
        public Guid OrderDeliveryDetailID { get; set; }

        [Required]
        public Guid OrderDeliveryID { get; set; }

        [Required]
        public Guid OrderLineID { get; set; }

        [Required]
        public byte OrderDeliveryRecordTypeCode { get; set; }

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

        [Required]
        public DateTime OrderDeliveryDate { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual cdOffice cdOffice { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual bsOrderDeliveryRecordType bsOrderDeliveryRecordType { get; set; }

    }
}
