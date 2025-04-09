using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPurchaseRequisitionHeader")]
    public partial class trPurchaseRequisitionHeader
    {
        public trPurchaseRequisitionHeader()
        {
            tpPurchaseRequisitionATAttributes = new HashSet<tpPurchaseRequisitionATAttribute>();
            trPurchaseRequisitionLines = new HashSet<trPurchaseRequisitionLine>();
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionHeaderID { get; set; }

        [Required]
        public object PurchaseRequisitionNumber { get; set; }

        [Required]
        public DateTime PurchaseRequisitionDate { get; set; }

        [Required]
        public TimeSpan PurchaseRequisitionTime { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CompanyName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RequesterWorkplaceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RequesterJobDepartmentCode { get; set; }

        [Required]
        public byte RequesterEmployeeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RequesterEmployeeCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RequesterFirstLastName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContractStatusCode { get; set; }

        [Required]
        public object DeliveryOfficeCode { get; set; }

        [Required]
        public byte DeliveryStoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DeliveryStoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryWarehouseCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public bool UserLocked { get; set; }

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
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdContractStatus cdContractStatus { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

        public virtual ICollection<tpPurchaseRequisitionATAttribute> tpPurchaseRequisitionATAttributes { get; set; }
        public virtual ICollection<trPurchaseRequisitionLine> trPurchaseRequisitionLines { get; set; }
    }
}
