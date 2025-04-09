using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBudgetType")]
    public partial class cdBudgetType
    {
        public cdBudgetType()
        {
            cdBudgetTypeDescs = new HashSet<cdBudgetTypeDesc>();
            trBudgets = new HashSet<trBudget>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BudgetTypeCode { get; set; }

        [Required]
        public short ValidYear { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public byte BudgetDetailCode { get; set; }

        public string GLAccFilterString { get; set; }

        public string DetailFilterString { get; set; }

        public string GLAccFilterStringSQL { get; set; }

        public string DetailFilterStringSQL { get; set; }

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
        public virtual bsBudgetDetail bsBudgetDetail { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<cdBudgetTypeDesc> cdBudgetTypeDescs { get; set; }
        public virtual ICollection<trBudget> trBudgets { get; set; }
    }
}
