using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpBankPaymentInstructionFTAttribute")]
    public partial class tpBankPaymentInstructionFTAttribute
    {
        public tpBankPaymentInstructionFTAttribute()
        {
        }

        [Key]
        [Required]
        public Guid BankPaymentInstructionLineID { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

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

        // Navigation Properties
        public virtual trBankPaymentInstructionLine trBankPaymentInstructionLine { get; set; }
        public virtual cdFTAttribute cdFTAttribute { get; set; }

    }
}
