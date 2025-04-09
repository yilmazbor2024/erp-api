using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInnerOrderHeader")]
    public partial class trInnerOrderHeader
    {
        public trInnerOrderHeader()
        {
            trInnerOrderLines = new HashSet<trInnerOrderLine>();
            trInnerOrderLineSums = new HashSet<trInnerOrderLineSum>();
            trInnerOrderLineSumDetails = new HashSet<trInnerOrderLineSumDetail>();
        }

        [Key]
        [Required]
        public Guid InnerOrderHeaderID { get; set; }

        [Required]
        public byte InnerOrderTypeCode { get; set; }

        [Required]
        public object InnerProcessCode { get; set; }

        [Required]
        public object InnerOrderNumber { get; set; }

        [Required]
        public DateTime InnerOrderDate { get; set; }

        [Required]
        public TimeSpan InnerOrderTime { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string InternalDescription { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

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
        public double SurplusOrderQtyToleranceRate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [Required]
        public bool IsSectionTransfer { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool UserLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsClosed { get; set; }

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
        public virtual bsInnerProcess bsInnerProcess { get; set; }
        public virtual bsInnerOrderType bsInnerOrderType { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trInnerOrderLine> trInnerOrderLines { get; set; }
        public virtual ICollection<trInnerOrderLineSum> trInnerOrderLineSums { get; set; }
        public virtual ICollection<trInnerOrderLineSumDetail> trInnerOrderLineSumDetails { get; set; }
    }
}
