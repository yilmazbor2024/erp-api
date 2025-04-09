using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsFastDeliveryCompanyDesc")]
    public partial class bsFastDeliveryCompanyDesc
    {
        public bsFastDeliveryCompanyDesc()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FastDeliveryCompanyCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string FastDeliveryCompanyDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsFastDeliveryCompany bsFastDeliveryCompany { get; set; }

    }
}
