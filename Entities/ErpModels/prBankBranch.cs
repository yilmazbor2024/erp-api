using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prBankBranch")]
    public partial class prBankBranch
    {
        public prBankBranch()
        {
            cdCheques = new HashSet<cdCheque>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            prCurrAccBankAccNos = new HashSet<prCurrAccBankAccNo>();
            trBankPaymentInstructionLines = new HashSet<trBankPaymentInstructionLine>();
            trBankPaymentListLines = new HashSet<trBankPaymentListLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankBranchCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

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
        public virtual cdDistrict cdDistrict { get; set; }
        public virtual cdBank cdBank { get; set; }
        public virtual cdState cdState { get; set; }
        public virtual cdCity cdCity { get; set; }
        public virtual cdCountry cdCountry { get; set; }

        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<prCurrAccBankAccNo> prCurrAccBankAccNos { get; set; }
        public virtual ICollection<trBankPaymentInstructionLine> trBankPaymentInstructionLines { get; set; }
        public virtual ICollection<trBankPaymentListLine> trBankPaymentListLines { get; set; }
    }
}
