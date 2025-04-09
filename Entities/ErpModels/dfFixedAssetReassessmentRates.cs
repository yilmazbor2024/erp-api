using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfFixedAssetReassessmentRates")]
    public partial class dfFixedAssetReassessmentRates
    {
        public dfFixedAssetReassessmentRates()
        {
        }

        [Key]
        [Required]
        public short ValidYear { get; set; }

        [Required]
        public float YearlyRate { get; set; }

        [Required]
        public float Period1 { get; set; }

        [Required]
        public float Period2 { get; set; }

        [Required]
        public float Period3 { get; set; }

        [Required]
        public float Period4 { get; set; }

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

    }
}
