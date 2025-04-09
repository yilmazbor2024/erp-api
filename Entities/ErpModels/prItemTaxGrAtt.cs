using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemTaxGrAtt")]
    public partial class prItemTaxGrAtt
    {
        public prItemTaxGrAtt()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemTaxGrCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PurcVatCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PurcPCTCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SellVatCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SellPCTCode { get; set; }

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
        public virtual cdItemTaxGr cdItemTaxGr { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdVat cdVat { get; set; }
        public virtual cdPCT cdPCT { get; set; }

    }
}
