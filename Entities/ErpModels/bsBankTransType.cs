using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBankTransType")]
    public partial class bsBankTransType
    {
        public bsBankTransType()
        {
            auBankPermits = new HashSet<auBankPermit>();
            bsBankTransTypeDescs = new HashSet<bsBankTransTypeDesc>();
            dfBankDefATAttributes = new HashSet<dfBankDefATAttribute>();
            dfBankOfficialForms = new HashSet<dfBankOfficialForm>();
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
            srRefNumberBankTranss = new HashSet<srRefNumberBankTrans>();
            trBankHeaders = new HashSet<trBankHeader>();
        }

        [Key]
        [Required]
        public byte BankTransTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<auBankPermit> auBankPermits { get; set; }
        public virtual ICollection<bsBankTransTypeDesc> bsBankTransTypeDescs { get; set; }
        public virtual ICollection<dfBankDefATAttribute> dfBankDefATAttributes { get; set; }
        public virtual ICollection<dfBankOfficialForm> dfBankOfficialForms { get; set; }
        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
        public virtual ICollection<srRefNumberBankTrans> srRefNumberBankTranss { get; set; }
        public virtual ICollection<trBankHeader> trBankHeaders { get; set; }
    }
}
