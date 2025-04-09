using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdAddressType")]
    public partial class cdAddressType
    {
        public cdAddressType()
        {
            cdAddressTypeDescs = new HashSet<cdAddressTypeDesc>();
            dfPDCCurrAccPostalAddresss = new HashSet<dfPDCCurrAccPostalAddress>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AddressTypeCode { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsShippingAddress { get; set; }

        [Required]
        public bool IsBillingAddress { get; set; }

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

        public virtual ICollection<cdAddressTypeDesc> cdAddressTypeDescs { get; set; }
        public virtual ICollection<dfPDCCurrAccPostalAddress> dfPDCCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
    }
}
