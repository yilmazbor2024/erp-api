using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsFastDeliveryCompany")]
    public partial class bsFastDeliveryCompany
    {
        public bsFastDeliveryCompany()
        {
            bsFastDeliveryCompanyDescs = new HashSet<bsFastDeliveryCompanyDesc>();
            tpOrderHeaderExtensions = new HashSet<tpOrderHeaderExtension>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FastDeliveryCompanyCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsFastDeliveryCompanyDesc> bsFastDeliveryCompanyDescs { get; set; }
        public virtual ICollection<tpOrderHeaderExtension> tpOrderHeaderExtensions { get; set; }
    }
}
