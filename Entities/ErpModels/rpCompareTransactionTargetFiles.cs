using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpCompareTransactionTargetFiles")]
    public partial class rpCompareTransactionTargetFiles
    {
        public rpCompareTransactionTargetFiles()
        {
        }

        [Key]
        [Required]
        public Guid CompareTransactionTargetFileID { get; set; }

        [Required]
        public byte FileFormatTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FileName { get; set; }

        [Required]
        public object ItemFileNumber { get; set; }

        [Required]
        public byte ProcessFlowCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object RefNumber { get; set; }

        [Required]
        public Guid CompareTransactionHeaderID { get; set; }

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
        public virtual rpCompareTransactionHeader rpCompareTransactionHeader { get; set; }

    }
}
