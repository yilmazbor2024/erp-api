using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsUBLExtensions")]
    public partial class bsUBLExtensions
    {
        public bsUBLExtensions()
        {
            prCurrAccUBLExtensionss = new HashSet<prCurrAccUBLExtensions>();
            tpInvoiceUBLExtensionss = new HashSet<tpInvoiceUBLExtensions>();
            tpShipmentUBLExtensionss = new HashSet<tpShipmentUBLExtensions>();
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UBLExtensionField { get; set; }

        [Required]
        public bool UseWithSchemeID { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<prCurrAccUBLExtensions> prCurrAccUBLExtensionss { get; set; }
        public virtual ICollection<tpInvoiceUBLExtensions> tpInvoiceUBLExtensionss { get; set; }
        public virtual ICollection<tpShipmentUBLExtensions> tpShipmentUBLExtensionss { get; set; }
    }
}
