using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCustomerLoyaltyProgram")]
    public partial class prCustomerLoyaltyProgram
    {
        public prCustomerLoyaltyProgram()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramLevelCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramStatusCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramStatusModifyReasonCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

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

        [Required]
        public short LoyaltyProgramProcessCode { get; set; }

        [Required]
        public DateTime LoyaltyProgramLevelCodeUpdateDate { get; set; }

        [Required]
        public DateTime LoyaltyProgramStatusCodeUpdateDate { get; set; }

        // Navigation Properties
        public virtual cdLoyaltyProgramStatus cdLoyaltyProgramStatus { get; set; }
        public virtual cdLoyaltyProgramStatusModifyReason cdLoyaltyProgramStatusModifyReason { get; set; }
        public virtual cdLoyaltyProgram cdLoyaltyProgram { get; set; }
        public virtual cdLoyaltyProgramLevel cdLoyaltyProgramLevel { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
