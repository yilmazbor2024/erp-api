using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPickingHeader")]
    public partial class trPickingHeader
    {
        public trPickingHeader()
        {
            tpPickingFromSectionTransfers = new HashSet<tpPickingFromSectionTransfer>();
            trPickingLines = new HashSet<trPickingLine>();
        }

        [Key]
        [Required]
        public Guid PickingHeaderID { get; set; }

        [Required]
        public byte PickingTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object PickingNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime PickingDate { get; set; }

        [Required]
        public TimeSpan PickingTime { get; set; }

        public string Description { get; set; }

        public string InternalDescription { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PackageVolumeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WeightUnitOfMeasureCode { get; set; }

        [Required]
        public float PackedWeight { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PackageNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DeliveryCompanyBarcode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LogisticsCompanyBOL { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LogisticsPackageNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PackagingTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PackageBrandCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerASNNumber { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ToWarehouseCode { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

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
        public virtual bsPickingType bsPickingType { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }
        public virtual cdPackageBrand cdPackageBrand { get; set; }
        public virtual bsPackagingType bsPackagingType { get; set; }
        public virtual cdPackageVolume cdPackageVolume { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpPickingFromSectionTransfer> tpPickingFromSectionTransfers { get; set; }
        public virtual ICollection<trPickingLine> trPickingLines { get; set; }
    }
}
