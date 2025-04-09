using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prInnerProcessInfo")]
    public partial class prInnerProcessInfo
    {
        public prInnerProcessInfo()
        {
        }

        [Key]
        [Required]
        public object InnerProcessCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [Required]
        public bool UseQty2 { get; set; }

        [Required]
        public bool UseSeriesCode { get; set; }

        [Required]
        public bool UseOnlyCompanyBrandedProducts { get; set; }

        [Required]
        public bool UseBatchBarcodeInventoryForLineQty { get; set; }

        [Required]
        public bool SetBatchCode { get; set; }

        [Required]
        public bool OpenPostingEShipmentsAfterEShipmentCreated { get; set; }

        [Required]
        public bool OpenPostingEShipmentsAfterEShipmentCreatedOnStore { get; set; }

        [Required]
        public bool UseInvoiceForSupportOutToCustomer { get; set; }

        [Required]
        public bool AllowCountingGiftCard { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsInnerProcess bsInnerProcess { get; set; }
        public virtual bsItemType bsItemType { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
