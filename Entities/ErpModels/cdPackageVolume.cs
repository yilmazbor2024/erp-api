using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPackageVolume")]
    public partial class cdPackageVolume
    {
        public cdPackageVolume()
        {
            cdPackageVolumeDescs = new HashSet<cdPackageVolumeDesc>();
            tpInvoiceLinePickingDetailss = new HashSet<tpInvoiceLinePickingDetails>();
            tpShipmentLinePickingDetailss = new HashSet<tpShipmentLinePickingDetails>();
            trPickingHeaders = new HashSet<trPickingHeader>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PackageVolumeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SizeUnitOfMeasureCode { get; set; }

        [Required]
        public float PackedWidth { get; set; }

        [Required]
        public float PackedLength { get; set; }

        [Required]
        public float PackedHeight { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }

        public virtual ICollection<cdPackageVolumeDesc> cdPackageVolumeDescs { get; set; }
        public virtual ICollection<tpInvoiceLinePickingDetails> tpInvoiceLinePickingDetailss { get; set; }
        public virtual ICollection<tpShipmentLinePickingDetails> tpShipmentLinePickingDetailss { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
    }
}
