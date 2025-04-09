using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsLoyaltyProgramProcess")]
    public partial class bsLoyaltyProgramProcess
    {
        public bsLoyaltyProgramProcess()
        {
            bsLoyaltyProgramProcessDescs = new HashSet<bsLoyaltyProgramProcessDesc>();
            prLoyaltyProgramProcessStatuss = new HashSet<prLoyaltyProgramProcessStatus>();
            prLoyaltyProgramProcessStatusHistorys = new HashSet<prLoyaltyProgramProcessStatusHistory>();
        }

        [Key]
        [Required]
        public short LoyaltyProgramProcessCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsLoyaltyProgramProcessDesc> bsLoyaltyProgramProcessDescs { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessStatus> prLoyaltyProgramProcessStatuss { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessStatusHistory> prLoyaltyProgramProcessStatusHistorys { get; set; }
    }
}
