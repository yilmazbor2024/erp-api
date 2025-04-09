using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prFixedAssetReassessmentRates")]
    public partial class prFixedAssetReassessmentRates
    {
        public prFixedAssetReassessmentRates()
        {
            prFixedAssetExpenses = new HashSet<prFixedAssetExpense>();
        }

        [Key]
        [Required]
        public Guid ReassessmentRateLineID { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public short ValidYear { get; set; }

        [Required]
        public byte ValidMonth { get; set; }

        [Required]
        public byte SortOrder { get; set; }

        [Required]
        public double ReassessmentRate { get; set; }

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

        // Navigation Properties
        public virtual cdItem cdItem { get; set; }

        public virtual ICollection<prFixedAssetExpense> prFixedAssetExpenses { get; set; }
    }
}
