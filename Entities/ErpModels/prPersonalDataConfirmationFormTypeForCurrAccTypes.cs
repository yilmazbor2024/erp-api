using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPersonalDataConfirmationFormTypeForCurrAccTypes")]
    public partial class prPersonalDataConfirmationFormTypeForCurrAccTypes
    {
        public prPersonalDataConfirmationFormTypeForCurrAccTypes()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormTypeCode { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdConfirmationFormType cdConfirmationFormType { get; set; }
        public virtual bsCurrAccType bsCurrAccType { get; set; }

    }
}
