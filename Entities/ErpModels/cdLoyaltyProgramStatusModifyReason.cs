using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLoyaltyProgramStatusModifyReason")]
    public partial class cdLoyaltyProgramStatusModifyReason
    {
        public cdLoyaltyProgramStatusModifyReason()
        {
            cdLoyaltyProgramStatusModifyReasonDescs = new HashSet<cdLoyaltyProgramStatusModifyReasonDesc>();
            prCustomerLoyaltyPrograms = new HashSet<prCustomerLoyaltyProgram>();
            prCustomerLoyaltyProgramHistorys = new HashSet<prCustomerLoyaltyProgramHistory>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramStatusModifyReasonCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        public virtual ICollection<cdLoyaltyProgramStatusModifyReasonDesc> cdLoyaltyProgramStatusModifyReasonDescs { get; set; }
        public virtual ICollection<prCustomerLoyaltyProgram> prCustomerLoyaltyPrograms { get; set; }
        public virtual ICollection<prCustomerLoyaltyProgramHistory> prCustomerLoyaltyProgramHistorys { get; set; }
    }
}
