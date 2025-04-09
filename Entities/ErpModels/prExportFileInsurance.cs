using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prExportFileInsurance")]
    public partial class prExportFileInsurance
    {
        public prExportFileInsurance()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [Key]
        [Required]
        public Guid InsuranceLineID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InsuranceTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PolicyNo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal PolicyCoverage { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LineDescription { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdInsuranceType cdInsuranceType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
