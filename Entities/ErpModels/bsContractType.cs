using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsContractType")]
    public partial class bsContractType
    {
        public bsContractType()
        {
            bsContractTypeDescs = new HashSet<bsContractTypeDesc>();
            srRefNumberContracts = new HashSet<srRefNumberContract>();
            trContracts = new HashSet<trContract>();
        }

        [Key]
        [Required]
        public byte ContractTypeCode { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsContractTypeDesc> bsContractTypeDescs { get; set; }
        public virtual ICollection<srRefNumberContract> srRefNumberContracts { get; set; }
        public virtual ICollection<trContract> trContracts { get; set; }
    }
}
