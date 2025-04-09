using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsIncoterm")]
    public partial class bsIncoterm
    {
        public bsIncoterm()
        {
            bsIncotermDescs = new HashSet<bsIncotermDesc>();
            cdExportFiles = new HashSet<cdExportFile>();
            cdImportFiles = new HashSet<cdImportFile>();
            prExportFileShippingInfos = new HashSet<prExportFileShippingInfo>();
            prImportFileShippingInfos = new HashSet<prImportFileShippingInfo>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GroupCode { get; set; }

        [Required]
        public bool OnlyWaterway { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsIncotermDesc> bsIncotermDescs { get; set; }
        public virtual ICollection<cdExportFile> cdExportFiles { get; set; }
        public virtual ICollection<cdImportFile> cdImportFiles { get; set; }
        public virtual ICollection<prExportFileShippingInfo> prExportFileShippingInfos { get; set; }
        public virtual ICollection<prImportFileShippingInfo> prImportFileShippingInfos { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
    }
}
