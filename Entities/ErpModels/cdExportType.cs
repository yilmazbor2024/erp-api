using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdExportType")]
    public partial class cdExportType
    {
        public cdExportType()
        {
            cdExportTypeDescs = new HashSet<cdExportTypeDesc>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExportTypeCode { get; set; }

        [Required]
        public bool CreatePaperOrEArchiveInvoice { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsFreeInvoice { get; set; }

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

        public virtual ICollection<cdExportTypeDesc> cdExportTypeDescs { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
    }
}
