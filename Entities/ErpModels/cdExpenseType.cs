using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdExpenseType")]
    public partial class cdExpenseType
    {
        public cdExpenseType()
        {
            cdExpenseTypeDescs = new HashSet<cdExpenseTypeDesc>();
            prProcessDefaultExpenseTypes = new HashSet<prProcessDefaultExpenseType>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
        }

        [Key]
        [Required]
        public byte ExpenseTypeCode { get; set; }

        [Required]
        public byte DocumentTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentTypeDescription { get; set; }

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
        public virtual bsDocumentType bsDocumentType { get; set; }

        public virtual ICollection<cdExpenseTypeDesc> cdExpenseTypeDescs { get; set; }
        public virtual ICollection<prProcessDefaultExpenseType> prProcessDefaultExpenseTypes { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
    }
}
