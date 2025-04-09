using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPaynetBankIDConvert")]
    public partial class dfPaynetBankIDConvert
    {
        public dfPaynetBankIDConvert()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankID { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BdID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

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
        public virtual cdCreditCardType cdCreditCardType { get; set; }

    }
}
