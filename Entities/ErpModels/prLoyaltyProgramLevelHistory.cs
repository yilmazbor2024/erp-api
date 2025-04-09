using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prLoyaltyProgramLevelHistory")]
    public partial class prLoyaltyProgramLevelHistory
    {
        public prLoyaltyProgramLevelHistory()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramLevelCode { get; set; }

        [Key]
        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public decimal MinAmountBracket { get; set; }

        [Required]
        public decimal MaxAmountBracket { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

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
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object LineDescription { get; set; }

        // Navigation Properties
        public virtual cdLoyaltyProgram cdLoyaltyProgram { get; set; }
        public virtual cdLoyaltyProgramLevel cdLoyaltyProgramLevel { get; set; }

    }
}
