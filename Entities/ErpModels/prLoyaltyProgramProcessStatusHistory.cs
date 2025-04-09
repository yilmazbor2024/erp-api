using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prLoyaltyProgramProcessStatusHistory")]
    public partial class prLoyaltyProgramProcessStatusHistory
    {
        public prLoyaltyProgramProcessStatusHistory()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramCode { get; set; }

        [Key]
        [Required]
        public short LoyaltyProgramProcessCode { get; set; }

        [Key]
        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramStatusCode { get; set; }

        [Required]
        public bool IsUnchangeable { get; set; }

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
        public virtual bsLoyaltyProgramProcess bsLoyaltyProgramProcess { get; set; }
        public virtual cdLoyaltyProgram cdLoyaltyProgram { get; set; }
        public virtual cdLoyaltyProgramStatus cdLoyaltyProgramStatus { get; set; }

    }
}
