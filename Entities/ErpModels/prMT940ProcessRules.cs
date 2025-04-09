using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prMT940ProcessRules")]
    public partial class prMT940ProcessRules
    {
        public prMT940ProcessRules()
        {
            tpBankMT940s = new HashSet<tpBankMT940>();
        }

        [Key]
        [Required]
        public Guid MT940ProcessRulesID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MT940ProcessCode { get; set; }

        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [Required]
        public bool ForDebitProcess { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DescriptionRule { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public byte BankTransTypeCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public byte BankOpTypeCode { get; set; }

        [Required]
        public byte DocumentTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentTypeDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentMethod { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LineDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ATAtt05 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FTAtt05 { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdBankOpType cdBankOpType { get; set; }
        public virtual bsMT940Process bsMT940Process { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdBank cdBank { get; set; }
        public virtual bsDocumentType bsDocumentType { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual bsBankTransType bsBankTransType { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpBankMT940> tpBankMT940s { get; set; }
    }
}
