using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfChippin")]
    public partial class dfChippin
    {
        public dfChippin()
        {
        }

        [Key]
        [Required]
        public byte ChippinID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ServiceURL { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PresentCardTypeCode { get; set; }

        [Required]
        public bool UseV3PointOnChippin { get; set; }

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
        public object BoosterDiscountOfferCodes { get; set; }

        // Navigation Properties
        public virtual cdPresentCardType cdPresentCardType { get; set; }

    }
}
