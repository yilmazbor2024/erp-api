using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCustomerCompanyBrandAttribute")]
    public partial class prCustomerCompanyBrandAttribute
    {
        public prCustomerCompanyBrandAttribute()
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
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [Key]
        [Required]
        public DateTime StartDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAtt01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAtt02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAtt03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAtt04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerCompanyBrandAtt05 { get; set; }

        [Required]
        public byte CustomerAlertColorCode { get; set; }

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
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdCustomerAlertColor cdCustomerAlertColor { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
