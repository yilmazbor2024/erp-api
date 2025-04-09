using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEmployeePayType")]
    public partial class bsEmployeePayType
    {
        public bsEmployeePayType()
        {
            bsEmployeePayTypeDescs = new HashSet<bsEmployeePayTypeDesc>();
            trBankLines = new HashSet<trBankLine>();
            trCashLines = new HashSet<trCashLine>();
            trChequeLines = new HashSet<trChequeLine>();
        }

        [Key]
        [Required]
        public byte EmployeePayTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsEmployeePayTypeDesc> bsEmployeePayTypeDescs { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
        public virtual ICollection<trChequeLine> trChequeLines { get; set; }
    }
}
