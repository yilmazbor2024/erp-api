using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prImportFileStatusHistory")]
    public partial class prImportFileStatusHistory
    {
        public prImportFileStatusHistory()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [Key]
        [Required]
        public DateTime OperationDate { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ForeignTradeStatusCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdForeignTradeStatus cdForeignTradeStatus { get; set; }

    }
}
