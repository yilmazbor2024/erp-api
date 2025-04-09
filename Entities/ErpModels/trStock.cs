using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trStock")]
    public partial class trStock
    {
        public trStock()
        {
            stItemRollNumbers = new HashSet<stItemRollNumber>();
            stItemSerialNumbers = new HashSet<stItemSerialNumber>();
            tpInStockDeclarationInfos = new HashSet<tpInStockDeclarationInfo>();
            tpOutStockDeclarationInfos = new HashSet<tpOutStockDeclarationInfo>();
            tpStockCrosss = new HashSet<tpStockCross>();
            tpStockITAttributes = new HashSet<tpStockITAttribute>();
        }

        [Key]
        [Required]
        public Guid StockID { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object InnerProcessCode { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

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

        [Required]
        public double In_Qty1 { get; set; }

        [Required]
        public double In_Qty2 { get; set; }

        [Required]
        public double Out_Qty1 { get; set; }

        [Required]
        public double Out_Qty2 { get; set; }

        [Required]
        public object FromOfficeCode { get; set; }

        [Required]
        public byte FromStoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FromStoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string FromWarehouseCode { get; set; }

        public string LineDescription { get; set; }

        public DateTime? ManufactureDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual bsInnerProcess bsInnerProcess { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual bsTransType bsTransType { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<stItemRollNumber> stItemRollNumbers { get; set; }
        public virtual ICollection<stItemSerialNumber> stItemSerialNumbers { get; set; }
        public virtual ICollection<tpInStockDeclarationInfo> tpInStockDeclarationInfos { get; set; }
        public virtual ICollection<tpOutStockDeclarationInfo> tpOutStockDeclarationInfos { get; set; }
        public virtual ICollection<tpStockCross> tpStockCrosss { get; set; }
        public virtual ICollection<tpStockITAttribute> tpStockITAttributes { get; set; }
    }
}
