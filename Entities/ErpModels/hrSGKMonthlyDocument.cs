using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("hrSGKMonthlyDocument")]
    public partial class hrSGKMonthlyDocument
    {
        public hrSGKMonthlyDocument()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

   
        [Key]
        [Required]
        public short ValidYear { get; set; }

        [Key]
        [Required]
        public byte ValidMonth { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]

        [Required]
        public DateTime JobStartDate { get; set; }

        [Required]
        public DateTime JobEndDate { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EmploymentLawCode { get; set; }

        [Required]
        public byte PremiumDay { get; set; }

        [Required]
        public byte MissingWorkDay { get; set; }

        [Required]
        public decimal SGKBaseDuePay { get; set; }

        [Required]
        public decimal SGKBase { get; set; }

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
        public virtual hrEmployeeMonthlySum hrEmployeeMonthlySum { get; set; }
        public virtual cdWorkPlace cdWorkPlace { get; set; }
        public virtual cdEmploymentLaw cdEmploymentLaw { get; set; }

    }
}
