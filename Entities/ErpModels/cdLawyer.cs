using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLawyer")]
    public partial class cdLawyer
    {
        public cdLawyer()
        {
            tpBadDebtLawyerHistorys = new HashSet<tpBadDebtLawyerHistory>();
            tpPaymentBadDebtLawyers = new HashSet<tpPaymentBadDebtLawyer>();
            trBadDebtTransHeaders = new HashSet<trBadDebtTransHeader>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LawyerCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        public string FirstLastName { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpBadDebtLawyerHistory> tpBadDebtLawyerHistorys { get; set; }
        public virtual ICollection<tpPaymentBadDebtLawyer> tpPaymentBadDebtLawyers { get; set; }
        public virtual ICollection<trBadDebtTransHeader> trBadDebtTransHeaders { get; set; }
    }
}
