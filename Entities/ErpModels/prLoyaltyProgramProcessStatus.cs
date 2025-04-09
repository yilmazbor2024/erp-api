using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prLoyaltyProgramProcessStatus")]
    public partial class prLoyaltyProgramProcessStatus
    {
        public prLoyaltyProgramProcessStatus()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramCode { get; set; }

        [Key]
        [Required]
        public short LoyaltyProgramProcessCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramStatusCode { get; set; }

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
        public virtual cdLoyaltyProgramStatus cdLoyaltyProgramStatus { get; set; }
        public virtual cdLoyaltyProgram cdLoyaltyProgram { get; set; }
        public virtual bsLoyaltyProgramProcess bsLoyaltyProgramProcess { get; set; }

    }
}
