using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trDebitHeader")]
    public partial class trDebitHeader
    {
        public trDebitHeader()
        {
            tpAgentContractDeservedDebits = new HashSet<tpAgentContractDeservedDebit>();
            tpAgentContractVehicleDebits = new HashSet<tpAgentContractVehicleDebit>();
            tpAgentContractVisitFrequencyDebits = new HashSet<tpAgentContractVisitFrequencyDebit>();
            tpAgentPerformanceBonusDebits = new HashSet<tpAgentPerformanceBonusDebit>();
            tpAgentPerformanceDebits = new HashSet<tpAgentPerformanceDebit>();
            tpDebitATAttributes = new HashSet<tpDebitATAttribute>();
            trDebitLines = new HashSet<trDebitLine>();
            trVirementLines = new HashSet<trVirementLine>();
        }

        [Key]
        [Required]
        public Guid DebitHeaderID { get; set; }

        [Required]
        public byte DebitTypeCode { get; set; }

        [Required]
        public object DebitNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public object RefNumber { get; set; }

        public byte? CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual bsDebitType bsDebitType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpAgentContractDeservedDebit> tpAgentContractDeservedDebits { get; set; }
        public virtual ICollection<tpAgentContractVehicleDebit> tpAgentContractVehicleDebits { get; set; }
        public virtual ICollection<tpAgentContractVisitFrequencyDebit> tpAgentContractVisitFrequencyDebits { get; set; }
        public virtual ICollection<tpAgentPerformanceBonusDebit> tpAgentPerformanceBonusDebits { get; set; }
        public virtual ICollection<tpAgentPerformanceDebit> tpAgentPerformanceDebits { get; set; }
        public virtual ICollection<tpDebitATAttribute> tpDebitATAttributes { get; set; }
        public virtual ICollection<trDebitLine> trDebitLines { get; set; }
        public virtual ICollection<trVirementLine> trVirementLines { get; set; }
    }
}
