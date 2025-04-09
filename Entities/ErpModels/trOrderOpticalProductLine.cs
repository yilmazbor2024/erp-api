using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderOpticalProductLine")]
    public partial class trOrderOpticalProductLine
    {
        public trOrderOpticalProductLine()
        {
        }

        [Key]
        [Required]
        public Guid OrderOpticalProductLineID { get; set; }

        [Required]
        public Guid OrderOpticalProductID { get; set; }

        [Required]
        public Guid OrderLineID { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string LeftRightFrame { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DistanceReading { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Sphere { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Cylinder { get; set; }

        [Required]
        public double Axis { get; set; }

        [Required]
        public double Prism { get; set; }

        [Required]
        public double EffectiveDiameter { get; set; }

        [Required]
        public double PupillaryDistance { get; set; }

        [Required]
        public double SegmentHeight { get; set; }

        [Required]
        public double BaseCurveRadius { get; set; }

        [Required]
        public double Addition { get; set; }

        [Required]
        public bool UsePolished { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal ContributionAmount { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProtocolNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OpticalSutCode { get; set; }

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
        public virtual trOrderOpticalProduct trOrderOpticalProduct { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual cdOpticalSut cdOpticalSut { get; set; }

    }
}
