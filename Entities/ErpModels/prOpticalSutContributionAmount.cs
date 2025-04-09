using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prOpticalSutContributionAmount")]
    public partial class prOpticalSutContributionAmount
    {
        public prOpticalSutContributionAmount()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OpticalSutCode { get; set; }

        [Key]
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool IsVatIncluded { get; set; }

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
        public virtual cdOpticalSut cdOpticalSut { get; set; }

    }
}
