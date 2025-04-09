using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionItemInfo")]
    public partial class tpPurchaseRequisitionItemInfo
    {
        public tpPurchaseRequisitionItemInfo()
        {
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TechnicalResponsibleCode { get; set; }

        [Required]
        public bool UseExistingItemCode { get; set; }

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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ItemDescription { get; set; }

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
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }

    }
}
