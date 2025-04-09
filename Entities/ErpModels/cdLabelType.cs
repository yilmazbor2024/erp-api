using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLabelType")]
    public partial class cdLabelType
    {
        public cdLabelType()
        {
            cdLabelTypeDescs = new HashSet<cdLabelTypeDesc>();
            dfStoreLabelTypess = new HashSet<dfStoreLabelTypes>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LabelTypeCode { get; set; }

        [Required]
        public bool ProductLabelWithoutBarcode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LabelLangCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string FirstSalePriceGroupCode { get; set; }

        [Required]
        public byte FirstSalePriceBasePriceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string QueryName { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ReportFileName { get; set; }

        [Required]
        public byte PrinterMediaType { get; set; }

        [Required]
        public int LabelLength { get; set; }

        [Required]
        public byte NativeLanguageCode { get; set; }

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
        public virtual cdBarcodeType cdBarcodeType { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsBasePrice bsBasePrice { get; set; }
        public virtual cdPriceGroup cdPriceGroup { get; set; }

        public virtual ICollection<cdLabelTypeDesc> cdLabelTypeDescs { get; set; }
        public virtual ICollection<dfStoreLabelTypes> dfStoreLabelTypess { get; set; }
    }
}
