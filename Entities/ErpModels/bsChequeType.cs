using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsChequeType")]
    public partial class bsChequeType
    {
        public bsChequeType()
        {
            auChequeDenys = new HashSet<auChequeDeny>();
            auChequePermits = new HashSet<auChequePermit>();
            bsChequeTransTypes = new HashSet<bsChequeTransType>();
            bsChequeTypeDescs = new HashSet<bsChequeTypeDesc>();
            cdCheques = new HashSet<cdCheque>();
            cdChequeAttributeTypes = new HashSet<cdChequeAttributeType>();
            dfChequeDefATAttributes = new HashSet<dfChequeDefATAttribute>();
            dfChequeOfficialForms = new HashSet<dfChequeOfficialForm>();
            srChequesSerialNumbers = new HashSet<srChequesSerialNumber>();
            srCodeNumberCheques = new HashSet<srCodeNumberCheque>();
        }

        [Key]
        [Required]
        public byte ChequeTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<auChequeDeny> auChequeDenys { get; set; }
        public virtual ICollection<auChequePermit> auChequePermits { get; set; }
        public virtual ICollection<bsChequeTransType> bsChequeTransTypes { get; set; }
        public virtual ICollection<bsChequeTypeDesc> bsChequeTypeDescs { get; set; }
        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdChequeAttributeType> cdChequeAttributeTypes { get; set; }
        public virtual ICollection<dfChequeDefATAttribute> dfChequeDefATAttributes { get; set; }
        public virtual ICollection<dfChequeOfficialForm> dfChequeOfficialForms { get; set; }
        public virtual ICollection<srChequesSerialNumber> srChequesSerialNumbers { get; set; }
        public virtual ICollection<srCodeNumberCheque> srCodeNumberCheques { get; set; }
    }
}
