using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsConsentSource")]
    public partial class bsConsentSource
    {
        public bsConsentSource()
        {
            cdConfirmationFormTypes = new HashSet<cdConfirmationFormType>();
            prConsentSourceConverts = new HashSet<prConsentSourceConvert>();
        }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Category { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Source { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdConfirmationFormType> cdConfirmationFormTypes { get; set; }
        public virtual ICollection<prConsentSourceConvert> prConsentSourceConverts { get; set; }
    }
}
