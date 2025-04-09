using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfOnlineDistributor")]
    public partial class dfOnlineDistributor
    {
        public dfOnlineDistributor()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte CustomerTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        [Key]
        [Required]
        public Guid SubCurrAccID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerWarehouseCode { get; set; }

        [Required]
        public byte CrossVendorTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CrossVendorCode { get; set; }

        [Required]
        public object CrossCompanyCode { get; set; }

        [Required]
        public object CrossOfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CrossStoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CrossWarehouseCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public byte ProcessFlowCode { get; set; }

        [Required]
        public bool UseTransferApproving { get; set; }

        [Required]
        public object ReturnOfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ReturnWarehouseCode { get; set; }

        [Required]
        public object ReturnProcessCode { get; set; }

        [Required]
        public object OrderOfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OrderWarehouseCode { get; set; }

        [Required]
        public object OrderProcessCode { get; set; }

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
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
