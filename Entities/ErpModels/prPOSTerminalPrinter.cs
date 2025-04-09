using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPOSTerminalPrinter")]
    public partial class prPOSTerminalPrinter
    {
        public prPOSTerminalPrinter()
        {
        }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Key]
        [Required]
        public short POSTerminalID { get; set; }

        [Key]
        [Required]
        public byte FormType { get; set; }

        [Required]
        public bool PrintForm { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PrinterName { get; set; }

        [Required]
        public bool OpenPrintPreview { get; set; }

        [Required]
        public bool CanCancelPrint { get; set; }

        [Required]
        public byte NumberOfCopies { get; set; }

        [Required]
        public bool WaitOkPerPage { get; set; }

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
        public virtual cdPOSTerminal cdPOSTerminal { get; set; }

    }
}
