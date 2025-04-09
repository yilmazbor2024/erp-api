using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpCompareTransactionHeader")]
    public partial class rpCompareTransactionHeader
    {
        public rpCompareTransactionHeader()
        {
            rpCompareTransactionLines = new HashSet<rpCompareTransactionLine>();
            rpCompareTransactionSourceFiless = new HashSet<rpCompareTransactionSourceFiles>();
            rpCompareTransactionTargetFiless = new HashSet<rpCompareTransactionTargetFiles>();
        }

        [Key]
        [Required]
        public Guid CompareTransactionHeaderID { get; set; }

        [Required]
        public object CompareListName { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

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

        public virtual ICollection<rpCompareTransactionLine> rpCompareTransactionLines { get; set; }
        public virtual ICollection<rpCompareTransactionSourceFiles> rpCompareTransactionSourceFiless { get; set; }
        public virtual ICollection<rpCompareTransactionTargetFiles> rpCompareTransactionTargetFiless { get; set; }
    }
}
