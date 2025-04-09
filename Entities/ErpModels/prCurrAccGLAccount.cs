using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccGLAccount")]
    public partial class prCurrAccGLAccount
    {
        public prCurrAccGLAccount()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

 

        [Key]
        [Required]
        public byte PostAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OrderAdvanceGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EInvoiceSalesGLAccCode { get; set; }

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
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual bsPostAccType bsPostAccType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
