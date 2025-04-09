using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBarcodeType")]
    public partial class cdBarcodeType
    {
        public cdBarcodeType()
        {
            cdBarcodeTypeDescs = new HashSet<cdBarcodeTypeDesc>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdDiscountVoucherTypes = new HashSet<cdDiscountVoucherType>();
            cdLabelTypes = new HashSet<cdLabelType>();
            dfUnifreeCompanys = new HashSet<dfUnifreeCompany>();
            prItemBarcodes = new HashSet<prItemBarcode>();
            prItemBatchBarcodes = new HashSet<prItemBatchBarcode>();
            prPosTerminalFiscalPrinters = new HashSet<prPosTerminalFiscalPrinter>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCode { get; set; }

        [Required]
        public byte StandardBarcodeTypeCode { get; set; }

        [Required]
        public byte FixedLen { get; set; }

        [Required]
        public decimal FixedNumber { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

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
        public virtual bsStandardBarcodeType bsStandardBarcodeType { get; set; }

        public virtual ICollection<cdBarcodeTypeDesc> cdBarcodeTypeDescs { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdDiscountVoucherType> cdDiscountVoucherTypes { get; set; }
        public virtual ICollection<cdLabelType> cdLabelTypes { get; set; }
        public virtual ICollection<dfUnifreeCompany> dfUnifreeCompanys { get; set; }
        public virtual ICollection<prItemBarcode> prItemBarcodes { get; set; }
        public virtual ICollection<prItemBatchBarcode> prItemBatchBarcodes { get; set; }
        public virtual ICollection<prPosTerminalFiscalPrinter> prPosTerminalFiscalPrinters { get; set; }
    }
}
