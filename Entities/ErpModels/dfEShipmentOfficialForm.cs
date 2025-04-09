using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfEShipmentOfficialForm")]
    public partial class dfEShipmentOfficialForm
    {
        public dfEShipmentOfficialForm()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public byte ProcessFlowCode { get; set; }

        [Key]
        [Required]
        public bool UseForMobileStore { get; set; }

        [Key]
        [Required]
        public bool UseForV3Mobile { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string XsltName { get; set; }

        [Required]
        public object XsltData { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ResponseXsltName { get; set; }

        [Required]
        public object ResponseXsltData { get; set; }

        [Required]
        public bool UseOrderInfo { get; set; }

        [Required]
        public bool CombineInvoiceLines { get; set; }

        [Required]
        public bool PrintOfficialForm { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevel { get; set; }

        [Required]
        public bool ZeroQtyForExpense { get; set; }

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
        public virtual bsProcess bsProcess { get; set; }
        public virtual bsProcessFlow bsProcessFlow { get; set; }
        public virtual cdOffice cdOffice { get; set; }

    }
}
