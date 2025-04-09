using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPaymentMeans")]
    public partial class bsPaymentMeans
    {
        public bsPaymentMeans()
        {
            bsPaymentMeansDescs = new HashSet<bsPaymentMeansDesc>();
            cdExportFiles = new HashSet<cdExportFile>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PaymentMeansCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPaymentMeansDesc> bsPaymentMeansDescs { get; set; }
        public virtual ICollection<cdExportFile> cdExportFiles { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
    }
}
