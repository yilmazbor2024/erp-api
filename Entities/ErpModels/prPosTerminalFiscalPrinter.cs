using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPosTerminalFiscalPrinter")]
    public partial class prPosTerminalFiscalPrinter
    {
        public prPosTerminalFiscalPrinter()
        {
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FiscalPrinterID { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceGroupCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DefaultCreditCardTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string URL { get; set; }

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
        public virtual cdCreditCardType cdCreditCardType { get; set; }
        public virtual cdBarcodeType cdBarcodeType { get; set; }
        public virtual cdPOSTerminal cdPOSTerminal { get; set; }
        public virtual cdPriceGroup cdPriceGroup { get; set; }

    }
}
