using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsChequeTransType")]
    public partial class bsChequeTransType
    {
        public bsChequeTransType()
        {
            auChequePermits = new HashSet<auChequePermit>();
            bsChequeTransTypeDescs = new HashSet<bsChequeTransTypeDesc>();
            dfChequeOfficialForms = new HashSet<dfChequeOfficialForm>();
            srRefNumberChequeTranss = new HashSet<srRefNumberChequeTrans>();
            trChequeHeaders = new HashSet<trChequeHeader>();
        }

        [Key]
        [Required]
        public byte ChequeTransTypeCode { get; set; }

        [Required]
        public byte ChequeTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsChequeType bsChequeType { get; set; }

        public virtual ICollection<auChequePermit> auChequePermits { get; set; }
        public virtual ICollection<bsChequeTransTypeDesc> bsChequeTransTypeDescs { get; set; }
        public virtual ICollection<dfChequeOfficialForm> dfChequeOfficialForms { get; set; }
        public virtual ICollection<srRefNumberChequeTrans> srRefNumberChequeTranss { get; set; }
        public virtual ICollection<trChequeHeader> trChequeHeaders { get; set; }
    }
}
