using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trContract")]
    public partial class trContract
    {
        public trContract()
        {
            tpContractATAttributes = new HashSet<tpContractATAttribute>();
            tpContractITAttributes = new HashSet<tpContractITAttribute>();
            trContractProducts = new HashSet<trContractProduct>();
        }

        [Key]
        [Required]
        public Guid ContractID { get; set; }

        [Required]
        public byte ContractTypeCode { get; set; }

     

        [Required]
        public object ContractNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContractContentCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

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
        public decimal MonthlyFee { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public byte InvoicePeriod { get; set; }

        [Required]
        public byte LastInvoicedMonth { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        public bool? IsClosed { get; set; }

        public bool? Canceled { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CancelReason { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual bsContractType bsContractType { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdContractContent cdContractContent { get; set; }
        public virtual cdPaymentPlan cdPaymentPlan { get; set; }

        public virtual ICollection<tpContractATAttribute> tpContractATAttributes { get; set; }
        public virtual ICollection<tpContractITAttribute> tpContractITAttributes { get; set; }
        public virtual ICollection<trContractProduct> trContractProducts { get; set; }
    }
}
