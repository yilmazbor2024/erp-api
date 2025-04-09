using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceLinePickingDetails")]
    public partial class tpInvoiceLinePickingDetails
    {
        public tpInvoiceLinePickingDetails()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceLinePickingDetailID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public DateTime PickingDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PackageNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PackagingTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PackageBrandCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PackageVolumeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WeightUnitOfMeasureCode { get; set; }

        [Required]
        public float PackedWeight { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode { get; set; }

        [Required]
        public double Qty { get; set; }

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
        public virtual trInvoiceLine trInvoiceLine { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual cdPackageBrand cdPackageBrand { get; set; }
        public virtual bsPackagingType bsPackagingType { get; set; }
        public virtual cdPackageVolume cdPackageVolume { get; set; }
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }

    }
}
