using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trIncentiveLine")]
    public partial class trIncentiveLine
    {
        public trIncentiveLine()
        {
        }

        [Key]
        [Required]
        public Guid IncentiveLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public decimal IncentiveAmount { get; set; }

        [Required]
        public Guid IncentiveHeaderID { get; set; }

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
        public virtual trIncentiveHeader trIncentiveHeader { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
