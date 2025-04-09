using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderOpticalProduct")]
    public partial class trOrderOpticalProduct
    {
        public trOrderOpticalProduct()
        {
            trOrderOpticalProductLines = new HashSet<trOrderOpticalProductLine>();
        }

        [Key]
        [Required]
        public Guid OrderOpticalProductID { get; set; }

        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProtocolNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string InsuranceAgencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InstitutionCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PrescriptionNum { get; set; }

        [Required]
        public DateTime PrescriptionSpecifiedDate { get; set; }

        [Required]
        public DateTime PrescriptionDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DoctorFirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DoctorLastName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DoctorRegistrationNumber { get; set; }

        [Required]
        public double VertexDistance { get; set; }

        [Required]
        public double PantoscopicAngle { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DiagnosticCode { get; set; }

        public byte? InsuredType { get; set; }

        public byte? PrescriptionType { get; set; }

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
        public virtual cdInsuranceAgency cdInsuranceAgency { get; set; }
        public virtual trOrderHeader trOrderHeader { get; set; }
        public virtual cdDiagnostic cdDiagnostic { get; set; }

        public virtual ICollection<trOrderOpticalProductLine> trOrderOpticalProductLines { get; set; }
    }
}
