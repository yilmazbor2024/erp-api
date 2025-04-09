using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBadDebtTransHeader")]
    public partial class trBadDebtTransHeader
    {
        public trBadDebtTransHeader()
        {
            tpBadDebtLawyerHistorys = new HashSet<tpBadDebtLawyerHistory>();
            trBadDebtTransLines = new HashSet<trBadDebtTransLine>();
        }

        [Key]
        [Required]
        public Guid BadDebtTransHeaderID { get; set; }

        [Required]
        public byte BadDebtTransTypeCode { get; set; }

        [Required]
        public object BadDebtNumber { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LawyerCode { get; set; }

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

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

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
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdLawyer cdLawyer { get; set; }
        public virtual bsBadDebtTransType bsBadDebtTransType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpBadDebtLawyerHistory> tpBadDebtLawyerHistorys { get; set; }
        public virtual ICollection<trBadDebtTransLine> trBadDebtTransLines { get; set; }
    }
}
