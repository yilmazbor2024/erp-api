using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBankCardType")]
    public partial class bsBankCardType
    {
        public bsBankCardType()
        {
            bsBankCardTypeDescs = new HashSet<bsBankCardTypeDesc>();
            cdCreditCardTypes = new HashSet<cdCreditCardType>();
        }

        [Key]
        [Required]
        public byte BankCardTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBankCardTypeDesc> bsBankCardTypeDescs { get; set; }
        public virtual ICollection<cdCreditCardType> cdCreditCardTypes { get; set; }
    }
}
