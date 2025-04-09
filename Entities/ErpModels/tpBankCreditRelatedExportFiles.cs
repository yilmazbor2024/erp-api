using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpBankCreditRelatedExportFiles")]
    public partial class tpBankCreditRelatedExportFiles
    {
        public tpBankCreditRelatedExportFiles()
        {
        }

        [Key]
        [Required]
        public Guid BankCreditHeaderID { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

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
        public virtual trBankCreditHeader trBankCreditHeader { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }

    }
}
