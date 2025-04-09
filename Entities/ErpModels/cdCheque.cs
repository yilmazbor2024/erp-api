using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCheque")]
    public partial class cdCheque
    {
        public cdCheque()
        {
            cdChequeDescs = new HashSet<cdChequeDesc>();
            prChequeAttributes = new HashSet<prChequeAttribute>();
            trChequeLines = new HashSet<trChequeLine>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ChequeCode { get; set; }

        [Key]
        [Required]
        public byte ChequeTypeCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankBranchCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime OrganizedDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsEndorsed { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Drawer { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [Required]
        public bool IsIndividualAcc { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StateCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DistrictCode { get; set; }

        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankAccNo { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string IBAN { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExtraNumber1 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExtraNumber2 { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public DateTime ClosingDate { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual bsChequeType bsChequeType { get; set; }
        public virtual cdCity cdCity { get; set; }
        public virtual cdState cdState { get; set; }
        public virtual prBankBranch prBankBranch { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdDistrict cdDistrict { get; set; }
        public virtual cdTaxOffice cdTaxOffice { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdChequeDesc> cdChequeDescs { get; set; }
        public virtual ICollection<prChequeAttribute> prChequeAttributes { get; set; }
        public virtual ICollection<trChequeLine> trChequeLines { get; set; }
    }
}
