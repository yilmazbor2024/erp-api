using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prImportFileIndirectExpense")]
    public partial class prImportFileIndirectExpense
    {
        public prImportFileIndirectExpense()
        {
        }

        [Key]
        [Required]
        public Guid IndirectExpensesID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LineDescription { get; set; }

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
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdItem cdItem { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
