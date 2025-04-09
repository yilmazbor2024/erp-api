using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPackageBrand")]
    public partial class cdPackageBrand
    {
        public cdPackageBrand()
        {
            cdPackageBrandDescs = new HashSet<cdPackageBrandDesc>();
            tpInvoiceLinePickingDetailss = new HashSet<tpInvoiceLinePickingDetails>();
            tpShipmentLinePickingDetailss = new HashSet<tpShipmentLinePickingDetails>();
            trPickingHeaders = new HashSet<trPickingHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PackageBrandCode { get; set; }

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

        public virtual ICollection<cdPackageBrandDesc> cdPackageBrandDescs { get; set; }
        public virtual ICollection<tpInvoiceLinePickingDetails> tpInvoiceLinePickingDetailss { get; set; }
        public virtual ICollection<tpShipmentLinePickingDetails> tpShipmentLinePickingDetailss { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
    }
}
