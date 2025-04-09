using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpSelectedProduct")]
    public partial class rpSelectedProduct
    {
        public rpSelectedProduct()
        {
        }

        [Key]
        [Required]
        public Guid SelectedProductID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SessionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClientID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Barcode { get; set; }

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

        [Required]
        public bool IsDeleted { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ApplicationCode { get; set; }

        public Guid? DepartmantReceiptHeaderID { get; set; }

        public Guid? BrowsedProductID { get; set; }

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
        public virtual cdSalesperson cdSalesperson { get; set; }

    }
}
