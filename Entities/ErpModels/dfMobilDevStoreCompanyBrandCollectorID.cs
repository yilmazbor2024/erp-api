using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfMobilDevStoreCompanyBrandCollectorID")]
    public partial class dfMobilDevStoreCompanyBrandCollectorID
    {
        public dfMobilDevStoreCompanyBrandCollectorID()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string OptInCollectorID { get; set; }

        [Required]
        public byte OptInType { get; set; }

        [Required]
        public byte OptInMethod { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string OptOutCollectorID { get; set; }

        [Required]
        public byte OptOutType { get; set; }

        [Required]
        public byte OptOutMethod { get; set; }

        [Required]
        public int BrandID { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
