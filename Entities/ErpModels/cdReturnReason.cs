using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdReturnReason")]
    public partial class cdReturnReason
    {
        public cdReturnReason()
        {
            cdReturnReasonDescs = new HashSet<cdReturnReasonDesc>();
            prReturnReasonAvailableProcesss = new HashSet<prReturnReasonAvailableProcess>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ReturnReasonCode { get; set; }

        public string ProductHierarchyFilter { get; set; }

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

        public virtual ICollection<cdReturnReasonDesc> cdReturnReasonDescs { get; set; }
        public virtual ICollection<prReturnReasonAvailableProcess> prReturnReasonAvailableProcesss { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
    }
}
